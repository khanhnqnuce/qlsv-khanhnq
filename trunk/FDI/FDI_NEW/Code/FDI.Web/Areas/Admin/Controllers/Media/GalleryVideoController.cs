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
    public class GalleryVideoController : BaseController
    {
        //
        // GET: /Admin/GalleryVideo/

        readonly Gallery_VideoDA _videoDA = new Gallery_VideoDA("#");
        readonly Gallery_CategoryDA _categoryDA = new Gallery_CategoryDA();
        readonly Gallery_AlbumDA _albumDA = new Gallery_AlbumDA();


        /// <summary>
        /// Trang chủ, index. Load ra grid dưới dạng ajax
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var ltsAllCategory = _categoryDA.GetListSimpleAll(true);
            ltsAllCategory = _categoryDA.GetAllSelectList(ltsAllCategory, 0, true);
            ltsAllCategory.RemoveAt(0);

            var model = new ModelGalleryCategoryItem
            {
                SystemActionItem = systemActionItem,
                ListItem = ltsAllCategory,
              
            };

            return View(model);
        }

        /// <summary>
        /// Load danh sách bản ghi dưới dạng bảng
        /// </summary>
        /// <returns></returns>
        public ActionResult ListItems()
        {
            var listVideoItem = _videoDA.GetListSimpleByRequest(Request);
            var model = new ModelVideoItem
            {
                SystemActionItem = systemActionItem,
                ListItem = listVideoItem,
                PageHtml = _videoDA.GridHtmlPage
            };
            ViewData.Model = model;
           
            return View();
        }

        public ActionResult AjaxView()
        {
            var videoModel = _videoDA.GetById(ArrID.FirstOrDefault());
            ViewData.Model = videoModel;
            return View();
        }
        /// <summary>
        /// Form dùng cho thêm mới, sửa. Load bằng Ajax dialog
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxForm()
        {
            var videoModel = new Gallery_Video
            {
                IsShow = true,
            };

            if (DoAction == ActionType.Edit)
                videoModel = _videoDA.GetById(ArrID.FirstOrDefault());
            var ltsAllCategory = _categoryDA.GetListSimpleAll(true);
            ltsAllCategory = _categoryDA.GetAllSelectList(ltsAllCategory, 0);
            ltsAllCategory.RemoveAt(0);
            ViewBag.VideoCategoryID = ltsAllCategory;
            ViewBag.VideoAlbumID = _albumDA.GetAllListVideoSimple();
            ViewBag.Action = DoAction;
            ViewBag.ActionText = ActionText;
            return View(videoModel);
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
            var video = new Gallery_Video();
            List<Gallery_Video> ltsVideoItems;
            StringBuilder stbMessage;
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
                        UpdateModel(video);
                        video.DateCreated = DateTime.Now;
                        video.IsDeleted = false;
                        _videoDA.Add(video);
                        _videoDA.Save();
                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = video.ID.ToString(),
                            Message = string.Format("Đã thêm mới video: <b>{0}</b>", Server.HtmlEncode(video.Name))
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
                        video = _videoDA.GetById(ArrID.FirstOrDefault());
                        UpdateModel(video);

                        //Update IsShow
                        video.IsShow = Request["IsShow"] != null && Convert.ToBoolean(Request["IsShow"]);

                        #region cập nhật ảnh đại diện, category, album
                        //if (pictureId > 0)
                        //    video.PictureID = pictureId;
                        //if (categoryID > 0)
                        //    video.CategoryID = categoryID;
                        //if (albumID > 0)
                        //    video.AlbumID = albumID;
                        #endregion
                        _videoDA.Save();
                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = video.ID.ToString(),
                            Message = string.Format("Đã cập nhật video: <b>{0}</b>", Server.HtmlEncode(video.Name))
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
                    ltsVideoItems = _videoDA.GetListByArrID(ArrID);
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsVideoItems)
                    {
                        try
                        {
                            item.IsDeleted = true;
                            stbMessage.AppendFormat("Đã xóa video <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                        }
                        catch (Exception ex)
                        {

                            LogHelper.Instance.LogError(GetType(), ex);
                        }
                    }
                    msg.ID = string.Join(",", ArrID);
                    _videoDA.Save();
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
                    ltsVideoItems = _videoDA.GetListByArrID(ArrID).Where(o => o.IsShow != null && !o.IsShow.Value).ToList(); //Chỉ lấy những đối tượng ko được hiển thị
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsVideoItems)
                    {
                        try
                        {
                            item.IsShow = true;
                            stbMessage.AppendFormat("Đã hiển thị video <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                        }
                        catch (Exception ex)
                        {
                            LogHelper.Instance.LogError(GetType(), ex);
                        }

                    }
                    _videoDA.Save();
                    msg.ID = string.Join(",", ltsVideoItems.Select(o => o.ID));
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
                    ltsVideoItems = _videoDA.GetListByArrID(ArrID).Where(o => o.IsShow != null && o.IsShow.Value).ToList(); //Chỉ lấy những đối tượng được hiển thị
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsVideoItems)
                    {
                        try
                        {
                            item.IsShow = false;
                            stbMessage.AppendFormat("Đã ẩn video <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                        }
                        catch (Exception ex)
                        {

                            LogHelper.Instance.LogError(GetType(), ex);
                        }

                    }
                    _videoDA.Save();
                    msg.ID = string.Join(",", ltsVideoItems.Select(o => o.ID));
                    msg.Message = stbMessage.ToString();
                    break;
            }

            if (!string.IsNullOrEmpty(msg.Message)) return Json(msg, JsonRequestBehavior.AllowGet);
            msg.Message = "Không có hành động nào được thực hiện.";
            msg.Erros = true;
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Dùng cho tra cứu nhanh
        /// </summary>
        /// <returns></returns>
        public ActionResult AutoComplete()
        {
            var term = Request["term"];
            var ltsResults = _videoDA.GetListSimpleByAutoComplete(term, 10, true);
            return Json(ltsResults, JsonRequestBehavior.AllowGet);
        }

    }
}
