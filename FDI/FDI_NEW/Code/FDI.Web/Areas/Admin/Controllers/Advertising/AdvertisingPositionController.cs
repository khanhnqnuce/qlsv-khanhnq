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
    public class AdvertisingPositionController : BaseController
    {
        //
        // GET: /Admin/AdvertisingPosition/


        private readonly Advertising_PositionDA _positionDA = new Advertising_PositionDA();


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
            var listAdvertisingPositionItem = _positionDA.GetListSimpleByRequest(Request);
            var model = new ModelAdvertisingPositionItem
            {
                SystemActionItem = systemActionItem,
                ListItem = listAdvertisingPositionItem,
                PageHtml = _positionDA.GridHtmlPage
            };
            ViewData.Model = model;
           
            return View();
        }

        public ActionResult AjaxView()
        {
            var positionModel = _positionDA.GetById(ArrID.FirstOrDefault());
            ViewData.Model = positionModel;
            return View();
        }

        /// <summary>
        /// Form dùng cho thêm mới, sửa. Load bằng Ajax dialog
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxForm()
        {
            var positionModel = new Advertising_Position();

            if (DoAction == ActionType.Edit)
                positionModel = _positionDA.GetById(ArrID.FirstOrDefault());

            ViewData.Model = positionModel;
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
            var position = new Advertising_Position();

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
                        UpdateModel(position);
                        position.IsDeleted = false;
                        _positionDA.Add(position);
                        _positionDA.Save();
                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = position.ID.ToString(),
                            Message =
                                string.Format("Đã thêm mới vùng hiển thị: <b>{0}</b>",
                                              Server.HtmlEncode(position.Name))
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
                        position = _positionDA.GetById(ArrID.FirstOrDefault());
                        UpdateModel(position);
                        _positionDA.Save();
                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = position.ID.ToString(),
                            Message =
                                string.Format("Đã cập nhật vùng hiển thị: <b>{0}</b>",
                                              Server.HtmlEncode(position.Name))
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
                    var ltsPositionItems = _positionDA.GetListByArrId(ArrID);
                    var stbMessage = new StringBuilder();
                    foreach (var item in ltsPositionItems)
                    {
                        item.IsDeleted = true;
                        stbMessage.AppendFormat("Đã xóa banner <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                    }
                    msg.ID = string.Join(",", ArrID);
                    _positionDA.Save();
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
            var ltsResults = _positionDA.GetListSimpleByAutoComplete(term, 10, true);
            return Json(ltsResults, JsonRequestBehavior.AllowGet);
        }

    }
}
