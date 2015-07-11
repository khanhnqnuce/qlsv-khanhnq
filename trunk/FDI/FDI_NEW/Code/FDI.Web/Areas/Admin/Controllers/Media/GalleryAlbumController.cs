using System;
using System.Collections.Generic;
using System.Configuration;
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
    public class GalleryAlbumController : BaseController
    {
        //
        // GET: /Admin/GalleryAlbum/

        readonly Gallery_AlbumDA _albumDA = new Gallery_AlbumDA("#");

        /// <summary>
        /// Xem chi tiết album
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            var albumModel = _albumDA.GetById(id);
            ViewData.Model = albumModel;
            return View();
        }

        /// <summary>
        /// Trang chủ, index. Load ra grid dưới dạng ajax
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {           
            return View(systemActionItem);
        }

        /// <summary>
        /// Load danh sách bản ghi dưới dạng bảng
        /// </summary>
        /// <returns></returns>
        public ActionResult ListItems()
        {
            var listAlbumItem = _albumDA.GetListSimpleByRequest(Request);
            var model = new ModelAlbumItem
            {
                SystemActionItem = systemActionItem,
                ListItem = listAlbumItem,
                PageHtml = _albumDA.GridHtmlPage
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
            var albumModel = _albumDA.GetById(ArrID.FirstOrDefault());
            ViewData.Model = albumModel;
            return View();
        }

        /// <summary>
        /// Form dùng cho thêm mới, sửa. Load bằng Ajax dialog
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxForm()
        {
            var albumModel = new Gallery_Album
            {
                IsShow = true,
            };

            if (DoAction == ActionType.Edit)
                albumModel = _albumDA.GetById(ArrID.FirstOrDefault());

            ViewData.Model = albumModel;
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
            var album = new Gallery_Album();
            List<Gallery_Album> ltsAlbumItems;
            StringBuilder stbMessage;
            List<int> idValues;
            List<Gallery_Category> categorySelected;
            List<System_Tag> tagSelected;
            List<Shop_Product> productSelected;
            //var imagesId = ConvertUtil.ToInt32(Request["Value_DefaultImages"]);

            switch (DoAction)
            {
                case ActionType.Add:
                    try
                    {
                        if (!systemActionItem.Add)
                        {
                            msg = new JsonMessage
                            {
                                Erros = true,
                                Message = "Bạn chưa được phân quyền cho chức năng này!"
                            };

                            return Json(msg, JsonRequestBehavior.AllowGet);
                        }
                        UpdateModel(album);
                        album.NameAscii = MyString.Slug(album.Name);
                        album.DateCreated = DateTime.Now;
                        try
                        {
                            album.IsVideo = Convert.ToBoolean(Request["IsVideo"]);
                        }
                        catch
                        {
                            album.IsVideo = false;
                        }

                        //#region cập nhật ảnh đại diện
                        //if (imagesId > 0)
                        //    Album.ImagesID = imagesId;
                        //#endregion

                        #region Cập nhật category
                        idValues = MyBase.ConvertStringToListInt(Request["Value_CategoryValues"]);
                        categorySelected = _albumDA.GetListCategoryByArrID(idValues);
                        foreach (var category in categorySelected)
                        {
                            album.Gallery_Category.Add(category);
                        }
                        #endregion
                        #region thêm tag
                        idValues = MyBase.ConvertStringToListInt(Request["TagValues"]);
                        tagSelected = _albumDA.GetListIntTagByArrID(idValues);
                        foreach (var tag in tagSelected)
                        {
                            album.System_Tag.Add(tag);
                        } 
                        #endregion

                        #region thêm sản phẩm
                        idValues = MyBase.ConvertStringToListInt(Request["ProductValues"]);
                        productSelected = _albumDA.GetListProductByArrID(idValues);
                        foreach (var product in productSelected)
                        {
                            album.Shop_Product.Add(product);
                        }
                        #endregion

                        album.IsDeleted = false;
                        _albumDA.Add(album);
                        _albumDA.Save();
                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = album.ID.ToString(),
                            Message = string.Format("Đã thêm mới thư viện ảnh: <b>{0}</b>", Server.HtmlEncode(album.Name))
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
                        if (!systemActionItem.Edit)
                        {
                            msg = new JsonMessage
                            {
                                Erros = true,
                                Message = "Bạn chưa được phân quyền cho chức năng này!"
                            };

                            return Json(msg, JsonRequestBehavior.AllowGet);
                        }
                        album = _albumDA.GetById(ArrID.FirstOrDefault());
                        UpdateModel(album);
                        //Update IsShow
                        album.IsShow = Request["IsShow"] != null && Convert.ToBoolean(Request["IsShow"]);

                        //Update IsVideo
                        album.IsVideo = Request["IsVideo"] != null && Convert.ToBoolean(Request["IsShow"]);
                        album.NameAscii = MyString.Slug(album.Name);
                        //try
                        //{
                        //    Album.IsVideo = Convert.ToBoolean(Request["IsVideo"].ToString());
                        //}
                        //catch
                        //{
                        //    Album.IsVideo = false;
                        //}
                        album.Gallery_Category.Clear();
                        idValues = MyBase.ConvertStringToListInt(Request["Value_CategoryValues"]);
                        categorySelected = _albumDA.GetListCategoryByArrID(idValues);
                        foreach (var category in categorySelected)
                        {
                            album.Gallery_Category.Add(category);
                        }
                        #region thêm và xóa tag
                        album.System_Tag.Clear();
                        idValues = MyBase.ConvertStringToListInt(Request["TagValues"]);
                        tagSelected = _albumDA.GetListIntTagByArrID(idValues);
                        foreach (var tag in tagSelected)
                        {
                            album.System_Tag.Add(tag);
                        }
                        #endregion

                        #region thêm sản phẩm
                        album.Shop_Product.Clear();
                        idValues = MyBase.ConvertStringToListInt(Request["ProductValues"]);
                        productSelected = _albumDA.GetListProductByArrID(idValues);
                        foreach (var product in productSelected)
                        {
                            album.Shop_Product.Add(product);
                        }
                        #endregion

                        //Album.ImagesID = imagesId;
                        _albumDA.Save();
                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = album.ID.ToString(),
                            Message = string.Format("Đã cập nhật thư viện ảnh: <b>{0}</b>", Server.HtmlEncode(album.Name))
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
                    ltsAlbumItems = _albumDA.GetListByArrID(ArrID);
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsAlbumItems)
                    {
                        try
                        {
                            item.IsDeleted = true;
                            stbMessage.AppendFormat("Đã xóa thư viện ảnh <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                        }
                        catch (Exception ex)
                        {

                            LogHelper.Instance.LogError(GetType(), ex);
                        }


                    }
                    msg.ID = string.Join(",", ArrID);
                    _albumDA.Save();
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
                    ltsAlbumItems = _albumDA.GetListByArrID(ArrID).Where(o => !o.IsShow).ToList(); //Chỉ lấy những đối tượng ko được hiển thị
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsAlbumItems)
                    {
                        try
                        {
                            item.IsShow = true;
                            stbMessage.AppendFormat("Đã hiển thị thư viện ảnh <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                        }
                        catch (Exception ex)
                        {

                            LogHelper.Instance.LogError(GetType(), ex);
                        }

                    }
                    _albumDA.Save();
                    msg.ID = string.Join(",", ltsAlbumItems.Select(o => o.ID));
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
                    ltsAlbumItems = _albumDA.GetListByArrID(ArrID).Where(o => o.IsShow).ToList(); //Chỉ lấy những đối tượng được hiển thị
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsAlbumItems)
                    {
                        try
                        {
                            item.IsShow = false;
                            stbMessage.AppendFormat("Đã ẩn thư viện ảnh <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                        }
                        catch (Exception ex)
                        {

                            LogHelper.Instance.LogError(GetType(), ex);
                        }

                    }
                    _albumDA.Save();
                    msg.ID = string.Join(",", ltsAlbumItems.Select(o => o.ID));
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
            var ltsResults = _albumDA.GetListSimpleByAutoComplete(term, 10, true);
            return Json(ltsResults, JsonRequestBehavior.AllowGet);
        }

    }
}
