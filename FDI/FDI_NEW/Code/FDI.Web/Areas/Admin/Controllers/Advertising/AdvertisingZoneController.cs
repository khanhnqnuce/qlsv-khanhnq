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
    public class AdvertisingZoneController : BaseController
    {
        //
        // GET: /Admin/AdvertisingZone/

        readonly Advertising_ZoneDA _zoneDA = new Advertising_ZoneDA("#");

        /// <summary>
        /// Trang chủ, index. Load ra grid dưới dạng ajax
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(systemActionItem);
        }

        public ActionResult AjaxTreeSelect()
        {
            var ltsSourceZone = _zoneDA.GetAllListSimple();
            var ltsValues = MyBase.ConvertStringToListInt(Request["ValuesSelected"]);
            var stbHtml = new StringBuilder();
            _zoneDA.BuildTreeViewCheckBox(ltsSourceZone, 1, true, ltsValues, ref stbHtml);


            var model = new ModelAdvertisingZoneItem
            {
                SystemActionItem = systemActionItem,
                StbHtml = stbHtml.ToString(),
                Container = Request["ZoneContainer"],
                SelectMutil = Convert.ToBoolean(Request["SelectMutil"])
            };
            ViewData.Model = model;
            return View();
        }

        public ActionResult AjaxSort()
        {
            var ltsSourceCategory = _zoneDA.GetAllListSimpleByParentID(ArrID.FirstOrDefault());
            ViewData.Model = ltsSourceCategory;
            return View();
        }

        /// <summary>
        /// Load danh sách bản ghi dưới dạng bảng
        /// </summary>
        /// <returns></returns>
        public ActionResult ListItems()
        {
            var ltsSourceCategory = _zoneDA.GetAllListSimple();
            var stbHtml = new StringBuilder();
            _zoneDA.BuildTreeView(ltsSourceCategory, 1, false, ref stbHtml, systemActionItem.Add, systemActionItem.Delete, systemActionItem.Edit, systemActionItem.View, systemActionItem.Show, systemActionItem.Hide, systemActionItem.Order);
            var model = new ModelAdvertisingZoneItem
            {
                SystemActionItem = systemActionItem,
                StbHtml =stbHtml.ToString()
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
            var zoneModel = _zoneDA.GetById(ArrID.FirstOrDefault());
            ViewData.Model = zoneModel;
            return View();
        }

        /// <summary>
        /// Form dùng cho thêm mới, sửa. Load bằng Ajax dialog
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxForm()
        {
            var zoneModel = new Advertising_Zone
            {
                ParentID = (ArrID.Any()) ? ArrID.FirstOrDefault() : 0,
                Show = true
            };

            if (DoAction == ActionType.Edit)
                zoneModel = _zoneDA.GetById(ArrID.FirstOrDefault());


            var ltsAllItems = _zoneDA.GetAllListSimple();
            ViewBag.ZoneParentID = _zoneDA.GetAllSelectList(ltsAllItems, zoneModel.ID);
            ViewData.Model = zoneModel;
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
            var zone = new Advertising_Zone();
            List<Advertising_Zone> ltsZoneItems;
            StringBuilder stbMessage;

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
                        UpdateModel(zone);
                        zone.PageAscii = MyString.Slug(zone.Page);
                        zone.IsDeleted = false;
                        _zoneDA.Add(zone);
                        _zoneDA.Save();
                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = zone.ID.ToString(),
                            Message = string.Format("Đã thêm mới chuyên mục: <b>{0}</b>", Server.HtmlEncode(zone.Page))
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
                        zone = _zoneDA.GetById(ArrID.FirstOrDefault());
                        zone.PageAscii = MyString.Slug(zone.Page);

                        UpdateModel(zone);
                        _zoneDA.Save();
                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = zone.ID.ToString(),
                            Message = string.Format("Đã cập nhật chuyên mục: <b>{0}</b>", Server.HtmlEncode(zone.Page))
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
                    ltsZoneItems = _zoneDA.GetListByArrID(ArrID);
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsZoneItems)
                    {
                        try
                        {
                            item.IsDeleted = true;
                            stbMessage.AppendFormat("Đã xóa chuyên mục hướng dẫn <b>{0}</b>.<br />", Server.HtmlEncode(item.Page));
                        }
                        catch (Exception ex)
                        {

                            LogHelper.Instance.LogError(GetType(), ex);
                        }

                    }
                    msg.ID = string.Join(",", ArrID);
                    _zoneDA.Save();
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
                    ltsZoneItems = _zoneDA.GetListByArrID(ArrID).Where(o => !o.Show).ToList(); //Chỉ lấy những đối tượng ko được hiển thị
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsZoneItems)
                    {
                        try
                        {
                            item.Show = true;
                            stbMessage.AppendFormat("Đã hiển thị chuyên mục <b>{0}</b>.<br />", Server.HtmlEncode(item.PageAscii));
                        }
                        catch (Exception ex)
                        {

                            LogHelper.Instance.LogError(GetType(), ex);
                        }

                    }
                    _zoneDA.Save();
                    msg.ID = string.Join(",", ltsZoneItems.Select(o => o.ID));
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
                    ltsZoneItems = _zoneDA.GetListByArrID(ArrID).Where(o => o.Show).ToList(); //Chỉ lấy những đối tượng được hiển thị
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsZoneItems)
                    {
                        try
                        {
                            item.Show = false;
                            stbMessage.AppendFormat("Đã ẩn chuyên mục <b>{0}</b>.<br />", Server.HtmlEncode(item.PageAscii));
                        }
                        catch (Exception ex)
                        {

                            LogHelper.Instance.LogError(GetType(), ex);
                        }

                    }
                    _zoneDA.Save();
                    msg.ID = string.Join(",", ltsZoneItems.Select(o => o.ID));
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
            var ltsResults = _zoneDA.GetListSimpleByAutoComplete(term, 10);
            return Json(ltsResults, JsonRequestBehavior.AllowGet);
        }
    }
}
