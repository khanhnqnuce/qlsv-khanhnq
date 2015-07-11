using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FDI.Base;
using FDI.DA.Admin;
using FDI.Entities;
using FDI.Simple;
using FDI.Utils;

namespace FDI.Areas.Admin.Controllers
{
    public class NewsContentController : BaseController
    {

        //GET: /Admin/NewsContent/
        readonly NewsDA _newsDA = new NewsDA("#");

        /// <summary>
        /// Trang chủ, index. Load ra grid dưới dạng ajax
        /// </summary>
        /// <returns></returns>


        public ActionResult Index()
        {
            ViewBag.ViewFull = systemActionItem.ViewFull;
            return View(systemActionItem);
        }

        /// <summary>
        /// Load danh sách bản ghi dưới dạng bảng
        /// </summary>
        /// <returns></returns>
        public ActionResult ListItems()
        {
            var listitem = _newsDA.GetListSimpleByRequest(Request);
            var model = new ModelNewsItem
            {
                SystemActionItem = systemActionItem,
                Container = Request["Container"],
                ListItem = listitem,
                PageHtml = _newsDA.GridHtmlPage
            };
            ViewData.Model = model;
            return View();
        }


        /// <summary>
        /// Trang xem chi tiết trong model
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxView()
        {
            var newsModel = _newsDA.GetById(ArrID.FirstOrDefault());
            ViewData.Model = newsModel;
            return View();
        }

        /// <summary>
        /// Form dùng cho thêm mới, sửa. Load bằng Ajax dialog
        /// </summary>
        /// <returns></returns>

        public ActionResult AjaxForm()
        {
            var newsModel = new News_News
            {
                IsShow = true,
                IsAllowComment = true,
                StartDateDisplay = DateTime.Now
            };

            if (DoAction == ActionType.Edit)
                newsModel = _newsDA.GetById(ArrID.FirstOrDefault());

            ViewData.Model = newsModel;
            ViewBag.Action = DoAction;
            ViewBag.ActionText = ActionText;
            var membershipUser = Membership.GetUser();
            if (membershipUser != null)
                ViewBag.User = membershipUser.UserName;

            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                ViewBag.IP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            else if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.UserHostAddress))
            {
                ViewBag.IP = System.Web.HttpContext.Current.Request.UserHostAddress;
            }
            return View();
        }


        /// <summary>
        /// Hứng các giá trị, phục vụ cho thêm, sửa, xóa, ẩn, hiện
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Actions()
        {
            var msg = new JsonMessage();
            var news = new News_News();
            List<News_News> ltsNewsItems;
            StringBuilder stbMessage;
            List<int> idValues;
            List<News_Category> categorySelected;
            List<System_Tag> tagSelected;
            List<Shop_Product> productSelected;
            List<string> idValuesTag;

            var pictureId = string.IsNullOrEmpty(Request["Value_DefaultImages"]) ? 0 : Convert.ToInt32(Request["Value_DefaultImages"]); //Convert.ToInt32(Request["Value_DefaultImages"]);
            var picturePromotion = string.IsNullOrEmpty(Request["Value_DefaultImagesPromotion"]) ? 0 : Convert.ToInt32(Request["Value_DefaultImagesPromotion"]);

            switch (DoAction)
            {
                case ActionType.Add:
                    if (!systemActionItem.Add)
                    {
                        msg = new JsonMessage
                        {
                            Erros = true,
                            Message = "Bạn chưa được phân quyền cho chức năng này!"
                        };

                        return Json(msg, JsonRequestBehavior.AllowGet);
                    }
                    try
                    {
                        UpdateModel(news);
                        //News.TitleAscii = FDIUtils.MyString.Slug(News.Title);
                        var membershipUser = Membership.GetUser();
                        if (membershipUser != null)
                        {
                            var providerUserKey = membershipUser.ProviderUserKey;
                            if (providerUserKey != null)
                                news.Author = (Guid)providerUserKey;
                        }

                        if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                        {
                            news.IP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        }
                        else if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.UserHostAddress))
                        {
                            news.IP = System.Web.HttpContext.Current.Request.UserHostAddress;
                        }

                        news.DateCreated = DateTime.Now;
                        if (news.StartDateDisplay == null)
                        {
                            news.StartDateDisplay = DateTime.Now;
                        }
                        #region Cập nhật category
                        idValues = MyBase.ConvertStringToListInt(Request["Value_CategoryValues"]);
                        var a = _newsDA.GetListCategoryByArrID(idValues);
                        categorySelected = _newsDA.GetListCategoryByArrID(idValues);
                        foreach (var category in categorySelected)
                        {
                            news.News_Category.Add(category);
                        }
                        #endregion
                        #region cập nhật ảnh đại diện
                        if (pictureId > 0)
                            news.PictureID = pictureId;
                        #endregion
                        if (picturePromotion > 0)
                            news.PromotionPictureID = picturePromotion;
                        #region thêm tag
                        idValues = MyBase.ConvertStringToListInt(Request["TagValues"]);
                        tagSelected = _newsDA.GetListIntTagByArrID(idValues);
                        foreach (var tag in tagSelected)
                        {
                            tag.IsDelete = false;
                            tag.IsShow = true;
                            tag.NameAscii = MyString.Slug(tag.Name);
                            news.System_Tag.Add(tag);
                        }
                        #endregion

                        #region thêm sản phẩm

                        idValuesTag = MyBase.ConvertStringToListString(Request["NewsProduct"]);
                        productSelected = _newsDA.GetListProductByArrID(idValuesTag);
                        foreach (var product in productSelected)
                        {
                            news.Shop_Product.Add(product);
                        }
                        #endregion

                        #region Thêm file đính kèm
                        foreach (var fileItem in ListFileUpload)
                        {
                            news.System_File.Add(_newsDA.GetFileData(fileItem));
                        }
                        #endregion
                        news.IsDeleted = false;
                        _newsDA.Add(news);

                        _newsDA.Save();
                        if (news.IsAppendID == true)
                        {
                            news.TitleAscii = news.TitleAscii + "-" + news.ID;
                            _newsDA.Save();
                        }
                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = news.ID.ToString(),
                            Message = string.Format("Đã thêm mới bài viết: <b>{0}</b>", Server.HtmlEncode(news.Title))
                        };
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Instance.LogError(GetType(), ex);
                    }

                    break;

                case ActionType.Edit:
                    if (!systemActionItem.Edit)
                    {
                        msg = new JsonMessage
                        {
                            Erros = true,
                            Message = "Bạn chưa được phân quyền cho chức năng này!"
                        };

                        return Json(msg, JsonRequestBehavior.AllowGet);
                    }
                    try
                    {
                        news = _newsDA.GetById(ArrID.FirstOrDefault());
                        UpdateModel(news);
                        news.StartDateDisplay = MyDateTime.ToDate(Request["StartDateDisplay_"]);

                        var membershipUser = Membership.GetUser();
                        if (membershipUser != null)
                        {
                            var providerUserKey = membershipUser.ProviderUserKey;
                            if (providerUserKey != null)
                                news.Modifier = (Guid)providerUserKey;
                        }
                        if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                        {
                            news.IP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        }
                        else if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.UserHostAddress))
                        {
                            news.IP = System.Web.HttpContext.Current.Request.UserHostAddress;
                        }

                        #region cập nhật category
                        news.News_Category.Clear();
                        idValues = MyBase.ConvertStringToListInt(Request["Value_CategoryValues"]);
                        categorySelected = _newsDA.GetListCategoryByArrID(idValues);
                        foreach (var category in categorySelected)
                        {
                            news.News_Category.Add(category);
                        }
                        #endregion
                        #region cập nhật ảnh đại diện
                        if (pictureId > 0)
                            news.PictureID = pictureId;
                        #endregion

                        if (picturePromotion > 0)
                            news.PromotionPictureID = picturePromotion;

                        #region thêm và xóa tag
                        news.System_Tag.Clear();
                        idValuesTag = MyBase.ConvertStringToListString(Request["NewsTag"]);
                        tagSelected = _newsDA.GetListTagByArrID(idValuesTag);
                        foreach (var tag in tagSelected)
                        {
                            tag.IsDelete = false;
                            tag.IsShow = true;
                            tag.NameAscii = MyString.Slug(tag.Name);
                            news.System_Tag.Add(tag);
                        }
                        idValues = MyBase.ConvertStringToListInt(Request["TagValues"]);
                        tagSelected = _newsDA.GetListIntTagByArrID(idValues);
                        foreach (var tag in tagSelected)
                        {
                            tag.IsDelete = false;
                            tag.IsShow = true;
                            tag.NameAscii = MyString.Slug(tag.Name);
                            news.System_Tag.Add(tag);
                        }
                        #endregion

                        #region thêm sản phẩm
                        news.Shop_Product.Clear();
                        idValuesTag = MyBase.ConvertStringToListString(Request["NewsProduct"]);
                        productSelected = _newsDA.GetListProductByArrID(idValuesTag);
                        foreach (var product in productSelected)
                        {
                            news.Shop_Product.Add(product);
                        }

                        idValues = MyBase.ConvertStringToListInt(Request["ProductValues"]);
                        productSelected = _newsDA.GetListIntProductByArrID(idValues);
                        foreach (var product in productSelected)
                        {
                            news.Shop_Product.Add(product);
                        }
                        #endregion

                        _newsDA.Save();
                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = news.ID.ToString(),
                            Message = string.Format("Đã cập nhật bài viết: <b>{0}</b>", Server.HtmlEncode(news.Title))
                        };
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Instance.LogError(GetType(), ex);
                    }

                    break;

                case ActionType.Delete:
                    if (!systemActionItem.Delete)
                    {
                        msg = new JsonMessage
                        {
                            Erros = true,
                            Message = "Bạn chưa được phân quyền cho chức năng này!"
                        };

                        return Json(msg, JsonRequestBehavior.AllowGet);
                    }
                    ltsNewsItems = _newsDA.GetListByArrID(ArrID);
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsNewsItems)
                    {
                        try
                        {
                            var providerUserKey = Membership.GetUser().ProviderUserKey;
                            if (providerUserKey != null)
                                item.Modifier = (Guid)providerUserKey;
                            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                            {
                                item.IP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                            }
                            else if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.UserHostAddress))
                            {
                                item.IP = System.Web.HttpContext.Current.Request.UserHostAddress;
                            }
                            item.IsDeleted = true;
                            stbMessage.AppendFormat("Đã xóa bài viết <b>{0}</b>.<br />", Server.HtmlEncode(item.Title));
                        }
                        catch (Exception ex)
                        {
                            LogHelper.Instance.LogError(GetType(), ex);
                        }
                    }
                    msg.ID = string.Join(",", ArrID);
                    _newsDA.Save();
                    msg.Message = stbMessage.ToString();
                    break;

                case ActionType.Show:
                    if (!systemActionItem.Show)
                    {

                        msg = new JsonMessage
                        {
                            Erros = true,
                            Message = "Bạn chưa được phân quyền cho chức năng này!"
                        };

                        return Json(msg, JsonRequestBehavior.AllowGet);
                    }
                    ltsNewsItems = _newsDA.GetListByArrID(ArrID).Where(o => o.IsShow != true).ToList(); //Chỉ lấy những đối tượng ko được hiển thị
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsNewsItems)
                    {
                        try
                        {
                            var providerUserKey = Membership.GetUser().ProviderUserKey;
                            if (providerUserKey != null)
                                item.Modifier = (Guid)providerUserKey;
                            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                            {
                                item.IP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                            }
                            else if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.UserHostAddress))
                            {
                                item.IP = System.Web.HttpContext.Current.Request.UserHostAddress;
                            }
                            item.IsShow = true;
                            stbMessage.AppendFormat("Đã hiển thị bài viết <b>{0}</b>.<br />", Server.HtmlEncode(item.Title));
                        }
                        catch (Exception ex)
                        {
                            LogHelper.Instance.LogError(GetType(), ex);
                        }

                    }

                    _newsDA.Save();
                    msg.ID = string.Join(",", ltsNewsItems.Select(o => o.ID));
                    msg.Message = stbMessage.ToString();
                    break;

                case ActionType.Hide:
                    if (!systemActionItem.Hide)
                    {
                        msg = new JsonMessage
                        {
                            Erros = true,
                            Message = "Bạn chưa được phân quyền cho chức năng này!"
                        };

                        return Json(msg, JsonRequestBehavior.AllowGet);
                    }
                    ltsNewsItems = _newsDA.GetListByArrID(ArrID).Where(o => o.IsShow == true).ToList(); //Chỉ lấy những đối tượng được hiển thị
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsNewsItems)
                    {
                        try
                        {
                            var providerUserKey = Membership.GetUser().ProviderUserKey;
                            if (providerUserKey != null)
                                item.Modifier = (Guid)providerUserKey;
                            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                            {
                                item.IP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                            }
                            else if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.UserHostAddress))
                            {
                                item.IP = System.Web.HttpContext.Current.Request.UserHostAddress;
                            }
                            item.IsShow = false;
                            stbMessage.AppendFormat("Đã ẩn bài viết <b>{0}</b>.<br />", Server.HtmlEncode(item.Title));
                        }
                        catch (Exception ex)
                        {
                            LogHelper.Instance.LogError(GetType(), ex);
                        }

                    }

                    _newsDA.Save();
                    msg.ID = string.Join(",", ltsNewsItems.Select(o => o.ID));
                    msg.Message = stbMessage.ToString();
                    break;
            }

            if (string.IsNullOrEmpty(msg.Message))
            {
                msg.Message = "Không có hành động nào được thực hiện.";
                msg.Erros = true;
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Dùng cho tra cứu nhanh
        /// </summary>
        /// <returns></returns>
        public ActionResult AutoComplete()
        {
            var term = Request["query"];
            var ltsResults = _newsDA.GetListSimpleByAutoComplete(term, 10, true);
            return Json(ltsResults, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public string CheckTitleAsciiExits(string titleAscii, int id)
        {
            var ascii = !string.IsNullOrEmpty(titleAscii) ? titleAscii : string.Empty;
            var result = _newsDA.CheckTitleAsciiExits(ascii, id);

            return result ? "false" : "true";
        }
    }
}
