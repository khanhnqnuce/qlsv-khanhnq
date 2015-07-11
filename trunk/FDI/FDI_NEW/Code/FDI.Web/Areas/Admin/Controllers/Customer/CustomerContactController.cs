using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using FDI.Base;
using FDI.DA.Admin;
using FDI.Entities;
using FDI.Simple;
using FDI.Utils;

namespace FDI.Areas.Admin.Controllers
{
    public class CustomerContactController : BaseController
    {
        //
        // GET: /Admin/CustomerContact/
        private readonly CustomerContactDA _customerContactDA = new CustomerContactDA("#");

        public ActionResult Index()
        {
            var listContact = _customerContactDA.GetAllListSimple();
            var model = new ModelCustomerContactItem
            {
                SystemActionItem = systemActionItem,
                ListItem = listContact
            };

            return View(model);
        }

        /// <summary>
        /// Load danh sách bản ghi dưới dạng bảng
        /// </summary>
        /// <returns></returns>
        public ActionResult ListItems()
        {
            var listitem = _customerContactDA.GetListSimpleByRequest(Request);
            var model = new ModelCustomerContactItem
            {
                SystemActionItem = systemActionItem,
                Container = Request["Container"],
                ListItem = listitem,
                PageHtml = _customerContactDA.GridHtmlPage
            };
            ViewData.Model = model;
            return View();
        }

        /// <summary>
        /// Dùng cho tra cứu nhanh
        /// </summary>
        /// <returns></returns>
        public ActionResult AutoComplete()
        {
            var term = Request["query"];
            var ltsResults = _customerContactDA.GetListSimpleByAutoComplete(term, 10, true);
            return Json(ltsResults, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Trang xem chi tiết trong model
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxView()
        {
            var customerType = _customerContactDA.GetById(ArrID.FirstOrDefault());
            ViewData.Model = customerType;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Actions()
        {
            var msg = new JsonMessage();
            var customerType = new Customer_Type();
            List<CustomerContact> ltsCustomerTypeItems;
            StringBuilder stbMessage;

            switch (DoAction)
            {
                case ActionType.Active:
                    if (systemActionItem.Active != true)
                    {
                        msg = new JsonMessage
                        {
                            Erros = true,
                            Message = "Bạn chưa được phân quyền cho chức năng này!"
                        };

                        return Json(msg, JsonRequestBehavior.AllowGet);
                    }
                    ltsCustomerTypeItems = _customerContactDA.GetListByArrID(ArrID);
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsCustomerTypeItems)
                    {
                        item.Status = true;
                        UpdateModel(customerType);
                        stbMessage.AppendFormat("Đã liên hệ <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                    }
                    msg.ID = string.Join(",", ArrID);
                    _customerContactDA.Save();
                    msg.Message = stbMessage.ToString();
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
                    ltsCustomerTypeItems = _customerContactDA.GetListByArrID(ArrID);
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsCustomerTypeItems)
                    {
                        item.IsDelete = true;
                        UpdateModel(customerType);
                        stbMessage.AppendFormat("Đã xóa liên hệ <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                    }
                    msg.ID = string.Join(",", ArrID);
                    _customerContactDA.Save();
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
    }
}
