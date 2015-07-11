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
    public class AdvertisingController : BaseController
    {
        //
        // GET: /Admin/Advertising/

        readonly AdvertisingDA _advertisingDA = new AdvertisingDA("#");
        readonly Advertising_TypeDA _typeDA = new Advertising_TypeDA();
        readonly Advertising_PositionDA _positionDA = new Advertising_PositionDA();

        /// <summary>
        /// Trang chủ, index. Load ra grid dưới dạng ajax
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var lstCategory = _positionDA.GetAllListSimple();
            var model = new ModelAdvertisingPositionItem
            {
                SystemActionItem = systemActionItem,
                ListItem = lstCategory,
            };
           
            return View(model);
        }

        /// <summary>
        /// Load danh sách bản ghi dưới dạng bảng
        /// </summary>
        /// <returns></returns>
        public ActionResult ListItems()
        {

            var listactiveRoleItem = _advertisingDA.GetListSimpleByRequest(Request).OrderByDescending(o => o.CreateOnUtc);
            var model = new ModelAdvertisingItem
            {
                SystemActionItem = systemActionItem,
                ListItem = listactiveRoleItem,
                PageHtml = _advertisingDA.GridHtmlPage
            };
            ViewData.Model = model;
            return View();
        }

        public ActionResult AjaxView()
        {
            var advertisingModel = _advertisingDA.GetById(ArrID.FirstOrDefault());
            ViewData.Model = advertisingModel;
            return View();
        }
        /// <summary>
        /// Form dùng cho thêm mới, sửa. Load bằng Ajax dialog
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxForm()
        {
            var advertisingModel = new Advertising
            {
                Show = true,
            };

            if (DoAction == ActionType.Edit)
                advertisingModel = _advertisingDA.GetById(ArrID.FirstOrDefault());
            ViewBag.AdvertisingTypeID = _typeDA.GetAllListSimple();
            ViewBag.AdvertisingPositionID = _positionDA.GetAllListSimple();
            ViewData.Model = advertisingModel;
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
            var advertising = new Advertising();
            List<Advertising> ltsAdvertisingsItems;
            StringBuilder stbMessage;
            List<int> idValues;
            List<Advertising_Zone> zoneSelected;
            List<Shop_Category> categorySelected;

            var pictureId = Convert.ToInt32(Request["Value_DefaultImages"]);

            switch (DoAction)
            {
                case ActionType.Add:
                    try
                    {
                        UpdateModel(advertising);
                        #region cập nhật anh banner đại diện

                        if (pictureId > 0)
                            advertising.PictureID = pictureId;

                        #endregion
                        if (!string.IsNullOrEmpty(Request["PositionID"]))
                            advertising.PositionID = Convert.ToInt32(Request["PositionID"]);
                        advertising.IsDeleted = false;
                        advertising.CreateOnUtc = DateTime.Now;
                        try
                        {

                            if (!string.IsNullOrEmpty(Request["UrlVideo"]))
                                advertising.Url = Request["UrlVideo"];
                            if (!string.IsNullOrEmpty(Request["UrlFlash"]))
                                advertising.Url = Request["UrlFlash"];
                        }
                        catch
                        {
                        }
                        _advertisingDA.Add(advertising);
                        _advertisingDA.Save();
                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = advertising.ID.ToString(),
                            Message =
                                string.Format("Đã thêm mới banner: <b>{0}</b>", Server.HtmlEncode(advertising.Name))
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
                        advertising = _advertisingDA.GetById(ArrID.FirstOrDefault());
                        UpdateModel(advertising);

                        #region cập nhật banner đại diện

                        if (pictureId > 0)
                            advertising.PictureID = pictureId;

                        #endregion

                        if (!string.IsNullOrEmpty(Request["PositionID"]))
                            advertising.PositionID = Convert.ToInt32(Request["PositionID"]);
                        try
                        {
                            if (!string.IsNullOrEmpty(Request["UrlVideo"]))
                                advertising.Url = Request["UrlVideo"];

                            if (!string.IsNullOrEmpty(Request["UrlFlash"]))
                                advertising.Url = Request["UrlFlash"];
                        }
                        catch
                        {
                        }
                        _advertisingDA.Save();
                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = advertising.ID.ToString(),
                            Message =
                                string.Format("Đã cập nhật banner: <b>{0}</b>", Server.HtmlEncode(advertising.Name))
                        };
                    }

                    catch (Exception ex)
                    {
                        LogHelper.Instance.LogError(GetType(), ex);
                    }
                    break;

                case ActionType.Delete:
                    ltsAdvertisingsItems = _advertisingDA.GetListByArrID(ArrID);
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsAdvertisingsItems)
                    {
                        try
                        {
                            item.IsDeleted = true;
                            stbMessage.AppendFormat("Đã xóa banner <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                        }
                        catch (Exception ex)
                        {

                            LogHelper.Instance.LogError(GetType(), ex);
                        }

                    }
                    msg.ID = string.Join(",", ArrID);
                    _advertisingDA.Save();
                    msg.Message = stbMessage.ToString();
                    break;

                case ActionType.Show:
                    ltsAdvertisingsItems = _advertisingDA.GetListByArrID(ArrID).Where(o => !o.Show).ToList(); //Chỉ lấy những đối tượng ko được hiển thị
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsAdvertisingsItems)
                    {
                        try
                        {
                            item.Show = true;
                            stbMessage.AppendFormat("Đã hiển thị banner <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                        }
                        catch (Exception ex)
                        {

                            LogHelper.Instance.LogError(GetType(), ex);
                        }

                    }
                    _advertisingDA.Save();
                    msg.ID = string.Join(",", ltsAdvertisingsItems.Select(o => o.ID));
                    msg.Message = stbMessage.ToString();
                    break;

                case ActionType.Hide:
                    ltsAdvertisingsItems = _advertisingDA.GetListByArrID(ArrID).Where(o => o.Show).ToList(); //Chỉ lấy những đối tượng được hiển thị
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsAdvertisingsItems)
                    {
                        try
                        {
                            item.Show = false;
                            stbMessage.AppendFormat("Đã ẩn banner <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                        }
                        catch (Exception ex)
                        {

                            LogHelper.Instance.LogError(GetType(), ex);
                        }

                    }
                    _advertisingDA.Save();
                    msg.ID = string.Join(",", ltsAdvertisingsItems.Select(o => o.ID));
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
            var ltsResults = _advertisingDA.GetListSimpleByAutoComplete(term, 10, true);
            return Json(ltsResults, JsonRequestBehavior.AllowGet);
        }

    }
}
