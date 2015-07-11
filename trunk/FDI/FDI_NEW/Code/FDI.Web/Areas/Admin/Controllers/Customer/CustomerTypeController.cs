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
    public class CustomerTypeController : BaseController
    {
        //
        // GET: /Admin/CustomerType/

        private readonly CustomerTypeDA _customerTypeDA;

        public CustomerTypeController()
        {
            _customerTypeDA = new CustomerTypeDA("#");
        }

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
            var listactiveRoleItem = _customerTypeDA.GetListSimpleByRequest(Request);
            var model = new ModelCustomerTypeItem
            {
                SystemActionItem = systemActionItem,
                ListItem = listactiveRoleItem,
                PageHtml = _customerTypeDA.GridHtmlPage
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
            var customerType = _customerTypeDA.GetById(ArrID.FirstOrDefault());
            ViewData.Model = customerType;
            return View();
        }

        /// <summary>
        /// Form dùng cho thêm mới, sửa. Load bằng Ajax dialog
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxForm()
        {
            var customerType = new Customer_Type();           
            if (DoAction == ActionType.Edit)
                customerType = _customerTypeDA.GetById(ArrID.FirstOrDefault());

            ViewData.Model = customerType;
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
            var customerType = new Customer_Type();

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
                        customerType.IsDelete = false;
                        UpdateModel(customerType);
                        _customerTypeDA.Add(customerType);
                        _customerTypeDA.Save();
                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = customerType.ID.ToString(),
                            Message =
                                string.Format("Đã thêm mới hành động: <b>{0}</b>",
                                              Server.HtmlEncode(customerType.Name))
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
                        customerType = _customerTypeDA.GetById(ArrID.FirstOrDefault());
                        UpdateModel(customerType);
                        _customerTypeDA.Save();
                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = customerType.ID.ToString(),
                            Message =
                                string.Format("Đã cập nhật chuyên mục: <b>{0}</b>",
                                              Server.HtmlEncode(customerType.Name))
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
                    var ltsCustomerTypeItems = _customerTypeDA.GetListByArrID(ArrID);
                    var stbMessage = new StringBuilder();
                    foreach (var item in ltsCustomerTypeItems)
                    {
                        if (item.Customers.Any())
                        {
                            stbMessage.AppendFormat("Chuyên mục <b>{0}</b> đang được sử dụng, không được phép xóa.<br />", Server.HtmlEncode(item.Name));
                            //msg.Erros = true;
                        }
                        else
                        {
                            item.IsDelete = true;
                            UpdateModel(customerType);
                            stbMessage.AppendFormat("Đã xóa chuyên mục <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                        }
                    }
                    msg.ID = string.Join(",", ArrID);
                    _customerTypeDA.Save();
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
