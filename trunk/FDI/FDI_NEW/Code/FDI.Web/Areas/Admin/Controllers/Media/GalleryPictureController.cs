using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CarMarket.Web;
using FDI.Base;
using FDI.Business.Reposity;
using FDI.DA.Admin;
using FDI.Entities;
using FDI.Simple;
using FDI.Utils;

namespace FDI.Areas.Admin.Controllers
{
    public class GalleryPictureController : BaseController
    {
        //
        // GET: /Admin/GalleryPicture/

        readonly Gallery_PictureDA _pictureDA = new Gallery_PictureDA("#");
        readonly Gallery_CategoryDA _categoryDA = new Gallery_CategoryDA();
        readonly Gallery_AlbumDA _albumDA = new Gallery_AlbumDA();


        #region Dùng trong Upload ảnh
        /// <summary>
        /// Dùng khi submit
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AjaxFormPictureSubmit()
        {
            var msg = new JsonMessage { Erros = false };
            var intTotalFile = Convert.ToInt32(Request["NumberOfImage"]);
            var albumId = Convert.ToInt32(Request["AlbumID"]);
            var categoryId = Convert.ToInt32(Request["CategoryID"]);
            var thumbWidth = Convert.ToInt32(ConfigurationManager.AppSettings["ThumbWidth"]);
            var thumbHeight = Convert.ToInt32(ConfigurationManager.AppSettings["ThumbHeight"]);
            var folder = DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString() + "\\" +
                            DateTime.Now.Day.ToString() + "\\";
            var folderinsert = DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" +
                            DateTime.Now.Day.ToString() + "/";

            for (var idx = 0; idx < intTotalFile; idx++)
            {

                var dateFileCreated = DateTime.Now;
                var fileNameLocal = Request["ImageFile_" + idx + ""];
                var fileNameServer = dateFileCreated.ToString("yyyyMMddHHmmssffff") + "_" + fileNameLocal;
                //Copy ảnh gốc 
                //ImageProcess.createForder(ConfigData.IMAGE_UPLOAD_TEMP_FOLDER); // tạo forder Năm / Tháng / Ngày
                ImageProcess.CreateForder(ConfigData.IMAGE_UPLOAD_ORIGINAL_FOLDER); // tạo forder Năm / Tháng / Ngày

                System.IO.File.Copy(ConfigData.IMAGE_UPLOAD_TEMP_FOLDER + fileNameLocal, ConfigData.IMAGE_UPLOAD_ORIGINAL_FOLDER + folder + fileNameServer);
                var catchuoi = fileNameLocal.Split('.');
                var countcatchuoi = catchuoi.Count();
                var duoiimg = catchuoi[countcatchuoi - 1].Trim().ToLower();
                if (countcatchuoi > 0)
                {
                    if (duoiimg != "png" && duoiimg != "gif")
                    {
                        //Nếu ko đóng dấu ảnh resize ảnh upload vào thư mục medium
                        Image imageSource = Image.FromFile(ConfigData.IMAGE_UPLOAD_TEMP_FOLDER + fileNameLocal);
                        //Đọc file 
                        imageSource = ImageProcess.ResizeImage(imageSource, ConfigData.IMAGE_MEDIUM_FILE);
                        //Resize ảnh 640
                        ImageProcess.CreateForder(ConfigData.IMAGE_UPLOAD_MEDIUM_FOLDER); // tạo forder Năm / Tháng / Ngày
                        ImageProcess.SaveJpeg(ConfigData.IMAGE_UPLOAD_MEDIUM_FOLDER + folder + fileNameServer,
                                              new Bitmap(imageSource), 92L); // Save file Medium

                        //Resize ảnh 255x295
                        imageSource = ImageProcess.ResizeImage(imageSource, ConfigData.IMAGE_AVARTAR_FILE);
                        ImageProcess.CreateForder(ConfigData.IMAGE_UPLOAD_AVATAR_FOLDER); // tạo forder Năm / Tháng / Ngày
                        ImageProcess.SaveJpeg(ConfigData.IMAGE_UPLOAD_AVATAR_FOLDER + folder + fileNameServer,
                                              new Bitmap(imageSource),92L); // Save file Medium
                        imageSource.Dispose();
                        // Tạo ảnh resize 200x200px
                        //imageSource = Image.FromFile(ConfigData.IMAGE_UPLOAD_MEDIUM_FOLDER + fileNameServer); //Đọc file 
                        imageSource = Image.FromFile(ConfigData.IMAGE_UPLOAD_TEMP_FOLDER + fileNameLocal); //Đọc file 

                        imageSource = ImageProcess.ResizeImagebackgroupWhite(imageSource, ConfigData.IMAGE_MEDIUM_FILE);
                        //file có size < 146x155 thì giữ nguyên size gốc và lưu vào Thumbs
                        if (imageSource.Height < thumbHeight || imageSource.Width < thumbWidth)
                        {
                            ImageProcess.CreateForder(ConfigData.IMAGE_UPLOAD_THUMBS_FOLDER); // tạo forder Năm / Tháng / Ngày
                            ImageProcess.SaveJpeg(ConfigData.IMAGE_UPLOAD_THUMBS_FOLDER + folder + fileNameServer,
                                                  new Bitmap(imageSource), 92L); // Save file Thumbs
                        }
                        //file có size > 146x155 thì rezise và lưu vào Thumbs
                        else
                        {
                            imageSource = ImageProcess.ResizeImage(imageSource, ConfigData.IMAGE_THUMBS_SIZE);

                            //Resize ảnh
                            ImageProcess.CreateForder(ConfigData.IMAGE_UPLOAD_THUMBS_FOLDER); // tạo forder Năm / Tháng / Ngày
                            ImageProcess.SaveJpeg(ConfigData.IMAGE_UPLOAD_THUMBS_FOLDER + folder + fileNameServer,
                                                  new Bitmap(imageSource), 92L); // Save file Thumbs
                        }
                        imageSource.Dispose();
                    }
                    else
                    {


                        ImageProcess.CreateForder(ConfigData.IMAGE_UPLOAD_MEDIUM_FOLDER); // tạo forder Năm / Tháng / Ngày
                        ImageProcess.CreateForder(ConfigData.IMAGE_UPLOAD_THUMBS_FOLDER); // tạo forder Năm / Tháng / Ngày
                        //System.IO.File.Copy(ConfigData.IMAGE_UPLOAD_THUMBS_FOLDER + fileNameLocal, ConfigData.IMAGE_UPLOAD_ORIGINAL_FOLDER + fileNameServer);
                        System.IO.File.Copy(ConfigData.IMAGE_UPLOAD_TEMP_FOLDER + fileNameLocal, ConfigData.IMAGE_UPLOAD_MEDIUM_FOLDER + folder + fileNameServer);
                        System.IO.File.Copy(ConfigData.IMAGE_UPLOAD_TEMP_FOLDER + fileNameLocal, ConfigData.IMAGE_UPLOAD_THUMBS_FOLDER + folder + fileNameServer);
                    }
                }


                //Lấy thông tin cần thiết
                var picture = new Gallery_Picture();
                if (albumId > 0)
                    picture.AlbumID = albumId;
                if (categoryId > 0)
                    picture.CategoryID = categoryId;

                picture.DateCreated = DateTime.Now;
                picture.Folder = folderinsert;
                picture.Description = Request["ImageDescription_" + idx + ""];
                picture.Name = Request["ImageName_" + idx + ""];
                picture.IsShow = Convert.ToBoolean(Request["ImageShow_" + idx + ""]);
                picture.Url = fileNameServer;
                picture.IsDeleted = false;
                picture.CreateBy = User.Identity.Name;
                picture.UpdateBy = User.Identity.Name;
                _pictureDA.Add(picture);
                msg.Message += string.Format("Đã thêm hình ảnh: <b>{0}</b><br/>", picture.Name);
            }
            _pictureDA.Save();

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Sau khi upload xong, cập nhật thông tin cho picture
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxFormPictureUpdate()
        {
            string arrFile = Request["fileData"];
            var oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            var ltsFileobj = oSerializer.Deserialize<List<FDI.Entities.FileObj>>(arrFile);
            ViewBag.FileArray = ltsFileobj;

            ViewBag.AlbumID = Convert.ToInt32(Request["AlbumID"]);
            ViewBag.CategoryID = Convert.ToInt32(Request["CategoryID"]);

            var ltsAllCategory = _categoryDA.GetListSimpleAll(true);
            ltsAllCategory = _categoryDA.GetAllSelectList(ltsAllCategory, 0);
            ltsAllCategory.RemoveAt(0);
            ViewBag.PictureCategoryID = ltsAllCategory;
            ViewBag.PictureAlbumID = _albumDA.GetAllListSimple();
            return View();
        }

        /// <summary>
        /// Load ra khung upload mutils
        /// </summary>
        public ActionResult AjaxFormPicture()
        {
            try
            {
                var albumID = Convert.ToInt32(Request["AlbumID"]);
                ViewBag.AlbumID = albumID;
            }
            catch
            {
                ViewBag.AlbumID = 0;
            }
            try
            {
                var categoryID = Convert.ToInt32(Request["CategoryID"]);
                ViewBag.CategoryID = categoryID;
            }
            catch
            {
                ViewBag.CategoryID = 0;
            }
            return View();
        }
        #endregion


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
            var listPictureItem = _pictureDA.GetListSimpleByRequest(Request); ;
            var model = new ModelPictureItem
            {
                SystemActionItem = systemActionItem,
                ListItem = listPictureItem,
                PageHtml = _pictureDA.GridHtmlPage
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
            var pictureModel = _pictureDA.GetById(ArrID.FirstOrDefault());
            ViewData.Model = pictureModel;
            return View();
        }

        /// <summary>
        /// Form dùng cho thêm mới, sửa. Load bằng Ajax dialog
        /// </summary>
        /// <returns></returns>

        public ActionResult AjaxForm()
        {
            var pictureModel = new Gallery_Picture
            {
                IsShow = true,
            };

            if (DoAction == ActionType.Edit)
                pictureModel = _pictureDA.GetById(ArrID.FirstOrDefault());


            var ltsAllCategory = _categoryDA.GetListSimpleAll(true);
            ltsAllCategory = _categoryDA.GetAllSelectList(ltsAllCategory, 0);
            ltsAllCategory.RemoveAt(0);
            ViewBag.PictureCategoryID = ltsAllCategory;
            ViewBag.PictureAlbumID = _albumDA.GetAllListSimple();

            ViewData.Model = pictureModel;
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
            var picture = new Gallery_Picture();
            List<Gallery_Picture> ltsPictureItems;
            StringBuilder stbMessage;
            var pictureId = Convert.ToInt32(Request["Value_DefaultImages"]);
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
                        UpdateModel(picture);
                        picture.DateCreated = DateTime.Now;
                        picture.CreateBy = User.Identity.Name;
                        picture.UpdateBy = User.Identity.Name;
                        picture.IsDeleted = false;
                        #region cập nhật ảnh đại diện
                        if (pictureId > 0)
                            picture.ID = pictureId;
                        #endregion

                        _pictureDA.Add(picture);
                        _pictureDA.Save();
                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = picture.ID.ToString(),
                            Message = string.Format("Đã thêm mới hình ảnh: <b>{0}</b>", Server.HtmlEncode(picture.Name))
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
                        picture = _pictureDA.GetById(ArrID.FirstOrDefault());
                        picture.UpdateBy = User.Identity.Name;
                        UpdateModel(picture);
                        #region cập nhật ảnh đại diện
                        if (pictureId > 0)
                            picture.ID = pictureId;
                        #endregion

                        _pictureDA.Save();
                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = picture.ID.ToString(),
                            Message = string.Format("Đã cập nhật hình ảnh: <b>{0}</b>", Server.HtmlEncode(picture.Name))
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
                    ltsPictureItems = _pictureDA.GetListByArrID(ArrID);
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsPictureItems)
                    {
                        try
                        {
                            item.IsDeleted = true;
                            stbMessage.AppendFormat("Đã xóa hình ảnh <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                        }
                        catch (Exception ex)
                        {

                            LogHelper.Instance.LogError(GetType(), ex);
                        }

                    }
                    msg.ID = string.Join(",", ArrID);

                    _pictureDA.Save();
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
                    ltsPictureItems = _pictureDA.GetListByArrID(ArrID).Where(o => !o.IsShow).ToList(); //Chỉ lấy những đối tượng ko được hiển thị
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsPictureItems)
                    {
                        try
                        {
                            item.IsShow = true;
                            stbMessage.AppendFormat("Đã hiển thị hình ảnh <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                        }
                        catch (Exception ex)
                        {

                            LogHelper.Instance.LogError(GetType(), ex);
                        }

                    }
                    _pictureDA.Save();
                    msg.ID = string.Join(",", ltsPictureItems.Select(o => o.ID));
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
                    ltsPictureItems = _pictureDA.GetListByArrID(ArrID).Where(o => o.IsShow).ToList(); //Chỉ lấy những đối tượng được hiển thị
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsPictureItems)
                    {
                        try
                        {
                            item.IsShow = false;
                            stbMessage.AppendFormat("Đã ẩn hình ảnh <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                        }
                        catch (Exception ex)
                        {

                            LogHelper.Instance.LogError(GetType(), ex);
                        }

                    }
                    _pictureDA.Save();
                    msg.ID = string.Join(",", ltsPictureItems.Select(o => o.ID));
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
            var ltsResults = _pictureDA.GetListSimpleByAutoComplete(term, 10, true);
            return Json(ltsResults, JsonRequestBehavior.AllowGet);
        }

    }
}
