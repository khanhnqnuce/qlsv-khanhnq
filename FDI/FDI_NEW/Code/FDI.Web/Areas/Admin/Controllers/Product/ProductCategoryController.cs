using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using FDI.Base;
using FDI.DA.Admin;
using FDI.Entities;
using FDI.Simple;
using FDI.Utils;

namespace FDI.Areas.Admin.Controllers
{
    public class ProductCategoryController : BaseController
    {
        readonly Shop_CategoryDA _categoryDA = new Shop_CategoryDA("#");
        readonly System_TagDA _systemTagDa = new System_TagDA("#");

        /// <summary>
        /// Trang chủ, index. Load ra grid dưới dạng ajax
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(systemActionItem);
        }

        [HttpGet]
        public ActionResult AjaxTreeSelect()
        {
            var ltsSourceCategory = _categoryDA.GetAllListSimple();
            var orderlst = ltsSourceCategory.OrderBy(x => x.ID).ToList();
            var ltsValues = MyBase.ConvertStringToListInt(Request["ValuesSelected"]);
            var stbHtml = new StringBuilder();
            _categoryDA.BuildTreeViewCheckBoxNoRoot(orderlst, 0, true, ltsValues, ref stbHtml);
            var model = new ModelProductCategoryItem
                            {
                                SystemActionItem = systemActionItem,
                                StbHtml = stbHtml.ToString(),
                                Container = Request["Container"],
                                SelectMutil = Convert.ToBoolean(Request["SelectMutil"])
                            };

            ViewData.Model = model;
            return View();
        }

        [HttpGet]
        public ActionResult AjaxTreeSelectForTag()
        {
            var ltsSourceCategory = _categoryDA.GetAllListSimple();
            var ltsValues = _systemTagDa.GetValuesArrayForSystemTag(Request["ValuesSelected"]);
            var stbHtml = new StringBuilder();
            _categoryDA.BuildTreeViewCheckBox(ltsSourceCategory, 0, true, ltsValues, ref stbHtml);

            var model = new ModelProductCategoryItem
            {
                SystemActionItem = systemActionItem,
                StbHtml = stbHtml.ToString(),
                Container = Request["Container"],
                SelectMutil = Convert.ToBoolean(Request["SelectMutil"])
            };

            ViewData.Model = model;
            return View();
        }

        public ActionResult AjaxSort()
        {
            var ltsSourceCategory = _categoryDA.GetAllListSimpleByParentID(ArrID.FirstOrDefault());
            ViewData.Model = ltsSourceCategory;
            return View();
        }

        /// <summary>
        /// Load danh sách bản ghi dưới dạng bảng
        /// </summary>
        /// <returns></returns>
        public ActionResult ListItems()
        {
            var ltsSourceCategory = _categoryDA.GetAllListSimple();
            var orderlst = ltsSourceCategory.OrderBy(x => x.ID).ToList();
            var stbHtml = new StringBuilder();
            _categoryDA.BuildTreeView(orderlst, 0, true, ref stbHtml);
            var model = new ModelProductCategoryItem
            {
                SystemActionItem = systemActionItem,
                ListItem = _categoryDA.GetListSimpleByRequest(Request),
                PageHtml = _categoryDA.GridHtmlPage,
                Container = Request["Container"],
                SelectMutil = Convert.ToBoolean(Request["SelectMutil"]),
                StbHtml = stbHtml.ToString()
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
            var categoryModel = _categoryDA.GetById(ArrID.FirstOrDefault());
            ViewData.Model = categoryModel;
            return View();
        }

        /// <summary>
        /// Form dùng cho thêm mới, sửa. Load bằng Ajax dialog
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxForm()
        {
            var categoryModel = new Shop_Category
            {
                Order = 0,
                ParentID = (ArrID.Any()) ? ArrID.FirstOrDefault() : 0
            };

            if (DoAction == ActionType.Edit)
                categoryModel = _categoryDA.GetById(ArrID.FirstOrDefault());


            var ltsAllItems = _categoryDA.GetAllListSimple();
            ViewBag.CategoryParentID = _categoryDA.GetAllSelectList(ltsAllItems, categoryModel.ID, true);
            ViewData.Model = categoryModel;
            ViewBag.Action = DoAction;
            ViewBag.ActionText = ActionText;
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
            var category = new Shop_Category();
            List<Shop_Category> ltsCategoryItems;
            StringBuilder stbMessage;

            switch (DoAction)
            {
                case ActionType.Add:
                    try
                    {
                        if (systemActionItem.Add != true)
                        {
                            msg = new JsonMessage
                            {
                                Erros = true,
                                Message = "Bạn chưa được phân quyền cho chức năng này!"
                            };

                            return Json(msg, JsonRequestBehavior.AllowGet);
                        }

                        UpdateModel(category);

                        #region properties
                        //category.Name = Convert.ToString(category.Name);
                        category.NameAscii = String.IsNullOrEmpty(category.Name) == false ? MyString.Slug(category.Name) : string.Empty;
                        category.IsDelete = false;
                        category.Rows = category.Rows.HasValue ? category.Rows.Value : 0;
                        //var description = Request["Description"];
                        //category.Description = description;
                        //var policy = Request["Policy"];
                        //category.Description = policy;
                        //category.IsPublish = Request["IsPublish"] == null || Request["IsPublish"] == string.Empty ? false : true;
                        //category.IsShowInTab = Request["IsShowInTab"] == null || Request["IsShowInTab"] == string.Empty ? false : true;
                        //category.IsAllowFilter = Request["IsAllowFilter"] == null || Request["IsAllowFilter"] == string.Empty ? false : true;
                        //category.IsShowOnBestSeller = Request["IsShowOnBestSeller"] == null || Request["IsShowOnBestSeller"] == string.Empty ? false : true;
                        //category.FrtRoundingNumber = Convert.ToInt32(category.FrtRoundingNumber);
                        //category.ParentID = Convert.ToInt32(category.ParentID);
                        //category.Order = Convert.ToInt32(category.Order);
                        //category.MetaDescription = Convert.ToString(category.MetaDescription);
                        //category.MetaKeyword = Convert.ToString(category.MetaKeyword);
                        //category.Description = Convert.ToString(category.Description);
                        //category.Policy = Convert.ToString(category.Policy);
                        #endregion

                        _categoryDA.Add(category);
                        _categoryDA.Save();

                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = category.ID.ToString(),
                            Message = string.Format("Đã thêm mới <b>{0}</b>", Server.HtmlEncode(category.Name))
                        };
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Instance.LogError(GetType(), ex);
                    }
                    break;

                case ActionType.Edit:
                    try
                    {
                        if (systemActionItem.Edit != true)
                        {
                            msg = new JsonMessage
                            {
                                Erros = true,
                                Message = "Bạn chưa được phân quyền cho chức năng này!"
                            };

                            return Json(msg, JsonRequestBehavior.AllowGet);
                        }

                        category = _categoryDA.GetById(ArrID.FirstOrDefault());
                        UpdateModel(category);
                        category.Rows = category.Rows.HasValue ? category.Rows.Value : 0;
                        //var description = Request["Description"];
                        //category.Description = description;
                        //var policy = Request["Policy"];
                        //category.Description = policy;
                        //#region properties
                        //category.Name = Convert.ToString(category.Name);
                        //category.NameAscii = String.IsNullOrEmpty(category.Name) == false ? FDIUtils.MyString.Slug(category.Name) : string.Empty;
                        ////category.IsDelete = false;
                        ////category.IsPublish = Request["IsPublish"] == null || Request["IsPublish"] == string.Empty ? false : true;
                        ////category.IsShowInTab = Request["IsShowInTab"] == null || Request["IsShowInTab"] == string.Empty ? false : true;
                        ////category.IsAllowFilter = Request["IsAllowFilter"] == null || Request["IsAllowFilter"] == string.Empty ? false : true;
                        ////category.IsShowOnBestSeller = Request["IsShowOnBestSeller"] == null || Request["IsShowOnBestSeller"] == string.Empty ? false : true;
                        //category.FrtRoundingNumber = Convert.ToInt32(category.FrtRoundingNumber);
                        //category.ParentID = Convert.ToInt32(category.ParentID);
                        //category.Order = Convert.ToInt32(category.Order);
                        //category.MetaDescription = Convert.ToString(category.MetaDescription);
                        //category.MetaKeyword = Convert.ToString(category.MetaKeyword);
                        //category.Description = Convert.ToString(category.Description);
                        //category.Policy = Convert.ToString(category.Policy);
                        //#endregion

                        _categoryDA.Save();
                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = category.ID.ToString(),
                            Message = string.Format("Đã cập nhật <b>{0}</b>", Server.HtmlEncode(category.Name))
                        };
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Instance.LogError(GetType(), ex);
                    }
                    break;

                case ActionType.Delete:
                    if (systemActionItem.Delete != true)
                    {
                        msg = new JsonMessage
                        {
                            Erros = true,
                            Message = "Bạn chưa được phân quyền cho chức năng này!"
                        };

                        return Json(msg, JsonRequestBehavior.AllowGet);
                    }

                    ltsCategoryItems = _categoryDA.GetListByArrID(ArrID);
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsCategoryItems)
                    {
                        if (item.Shop_Product.Any())
                        {
                            stbMessage.AppendFormat("<b>{0}</b> đang được sử dụng, không được phép xóa.<br />", Server.HtmlEncode(item.Name));
                            //msg.Erros = true;
                        }
                        else
                        {
                            item.IsDelete = true;
                            _categoryDA.Save();
                            stbMessage.AppendFormat("Đã xóa <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                        }
                    }
                    msg.ID = string.Join(",", ArrID);
                    _categoryDA.Save();
                    msg.Message = stbMessage.ToString();
                    break;

                case ActionType.Show:
                    if (systemActionItem.Show != true)
                    {
                        msg = new JsonMessage
                        {
                            Erros = true,
                            Message = "Bạn chưa được phân quyền cho chức năng này!"
                        };

                        return Json(msg, JsonRequestBehavior.AllowGet);
                    }

                    ltsCategoryItems = _categoryDA.GetListByArrID(ArrID);
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsCategoryItems)
                    {
                        item.IsPublish = true;
                        _categoryDA.Save();
                        stbMessage.AppendFormat("Đã hiển thị <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                    }
                    _categoryDA.Save();
                    msg.ID = string.Join(",", ltsCategoryItems.Select(o => o.ID));
                    msg.Message = stbMessage.ToString();
                    break;

                case ActionType.Hide:
                    if (systemActionItem.Hide != true)
                    {
                        msg = new JsonMessage
                        {
                            Erros = true,
                            Message = "Bạn chưa được phân quyền cho chức năng này!"
                        };

                        return Json(msg, JsonRequestBehavior.AllowGet);
                    }

                    ltsCategoryItems = _categoryDA.GetListByArrID(ArrID);
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsCategoryItems)
                    {
                        item.IsPublish = false;
                        _categoryDA.Save();
                        stbMessage.AppendFormat("Đã ẩn <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                    }
                    _categoryDA.Save();
                    msg.ID = string.Join(",", ltsCategoryItems.Select(o => o.ID));
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
            var term = Request["term"];
            var ltsResults = _categoryDA.GetListSimpleByAutoComplete(term, 10, true);
            return Json(ltsResults, JsonRequestBehavior.AllowGet);
        }
    }
}
