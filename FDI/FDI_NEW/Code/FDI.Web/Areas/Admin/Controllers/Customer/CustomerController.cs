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
using FDI.Security;

namespace FDI.Areas.Admin.Controllers
{
    public class CustomerController : BaseController
    {
        //
        // GET: /Admin/Customer/
        //readonly AdminUtilityDA _adminUtilityDA = new AdminUtilityDA();
        private readonly CustomerDA _customerDA;
        private readonly CustomerTypeDA _customerTypeDA;

        private readonly Customer_CustomerDeviceDA _customerDeviceDA;
        private readonly Customer_CustomerHistoryDA _customerHistoryDA;
        private readonly System_CountryDA _systemCountryDA;
        private readonly System_CityDA _systemCityDA;
        private readonly System_DistrictDA _systemDistrictDA;
        //private Forum_Customer_RankingDA Forum_Customer_RankingDA = new Forum_Customer_RankingDA("#");
        //Forum_Customer_Ranking ranking = new Forum_Customer_Ranking();


        public CustomerController()
        {
            _customerDA = new CustomerDA("#");
            _customerTypeDA = new CustomerTypeDA("#");

            _customerDeviceDA = new Customer_CustomerDeviceDA("#");
            _customerHistoryDA = new Customer_CustomerHistoryDA("#");
            _systemCountryDA = new System_CountryDA("#");
            _systemCityDA = new System_CityDA("#");
            _systemDistrictDA = new System_DistrictDA("#");
        }
        public ActionResult Index()
        {
            var listcustome = _customerTypeDA.GetAll();
            var model = new ModelCustomerTypeItem
            {
                SystemActionItem = systemActionItem,
                ListItem = listcustome
            };

            return View(model);
        }

        /// <summary>
        /// Load danh sách bản ghi dưới dạng bảng
        /// </summary>
        /// <returns></returns>
        public ActionResult ListItems()
        {
            var listcustome = _customerDA.GetListSimpleByRequest(Request, true);
            var model = new ModelCustomerItem
            {
                SystemActionItem = systemActionItem,
                ListItem = listcustome,
                PageHtml = _customerDA.GridHtmlPage
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
            //var customerType = _customerDA.GetCustomerItemById(ArrID.FirstOrDefault());
            //ViewData.Model = customerType;
            return View();
        }

        /// <summary>
        /// Trang xem chi tiết trong model
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxHistory()
        {
            ViewData.Model = _customerHistoryDA.GetListSimpleByRequestAndCustomer(Request, ArrID.FirstOrDefault());
            ViewBag.PageHtml = _customerHistoryDA.GridHtmlPage;
            return View();
        }

        /// <summary>
        /// Form dùng cho thêm mới, sửa. Load bằng Ajax dialog
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxForm()
        {
            var customer = new Customer();

            //list dropdownlist type customer
            ViewBag.ListCustomerType = _customerTypeDA.GetAll();
            //list dropdownlist payment method

            //list dropdownlist device customer access website
            ViewBag.ListCustomerDevice = _customerDeviceDA.GetAllList();
            //list dropdownlist country
            ViewBag.ListCountry = _systemCountryDA.GetListSimpleAll(true);
            //list dropdownlist city
            ViewBag.ListCity = _systemCityDA.GetListSimpleAll(true);
            //ViewBag.ListCapDo = Forum_Customer_RankingDA.getListSimpleAll(true);
            //list dropdownlist district
            ViewBag.ListDistrict = new List<DistrictItem>();

            if (DoAction == ActionType.Edit)
            {
                customer = _customerDA.GetById(ArrID.FirstOrDefault());
                if (customer.CityID.HasValue)
                    ViewBag.ListDistrict = _systemDistrictDA.GetListByCity(customer.CityID.Value, true);
            }

            ViewData.Model = customer;
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
            var customer = new Customer();
            List<Customer> ltsCustomerItems;
            StringBuilder stbMessage;
            //List<Forum_Customer_Ranking> RankingSelected;
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
                        UpdateModel(customer);
                        customer.CreatedOnUtc = DateTime.Now;
                        customer.LastLoginDateUtc = DateTime.Now;
                        customer.LastActivityDateUtc = DateTime.Now;
                        customer.IsDelete = false;
                        _customerDA.Add(customer);
                        _customerDA.Save();
                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = customer.ID.ToString(),
                            Message = string.Format("Đã thêm mới hành động: <b>{0}</b>", Server.HtmlEncode(customer.UserName))
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
                        customer = _customerDA.GetById(ArrID.FirstOrDefault());
                        UpdateModel(customer);
                        _customerDA.Save();
                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = customer.ID.ToString(),
                            Message = string.Format("Đã cập nhật chuyên mục: <b>{0}</b>", Server.HtmlEncode(customer.UserName))
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
                    ltsCustomerItems = _customerDA.GetListByArrID(ArrID);
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsCustomerItems)
                    {
                        if (item.Customer_History.Any())
                        {
                            stbMessage.AppendFormat("Chuyên mục <b>{0}</b> đang được sử dụng, không được phép xóa.<br />", Server.HtmlEncode(item.UserName));
                            //msg.Erros = true;
                        }
                        else
                        {
                            item.IsDelete = true;
                            stbMessage.AppendFormat("Đã xóa chuyên mục <b>{0}</b>.<br />", Server.HtmlEncode(item.UserName));
                        }
                    }
                    msg.ID = string.Join(",", ArrID);
                    _customerDA.Save();
                    msg.Message = stbMessage.ToString();
                    break;
                case ActionType.Show:
                    ltsCustomerItems = _customerDA.GetListByArrID(ArrID).Where(o => o.IsActive != null && !o.IsActive.Value).ToList(); //Chỉ lấy những đối tượng ko được hiển thị
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsCustomerItems)
                    {
                        item.IsActive = true;
                        stbMessage.AppendFormat("Đã hiển thị chuyên mục <b>{0}</b>.<br />", Server.HtmlEncode(item.UserName));
                    }
                    _customerDA.Save();
                    msg.ID = string.Join(",", ltsCustomerItems.Select(o => o.ID));
                    msg.Message = stbMessage.ToString();
                    break;

                case ActionType.Hide:
                    ltsCustomerItems = _customerDA.GetListByArrID(ArrID).Where(o => o.IsActive != null && o.IsActive.Value).ToList(); //Chỉ lấy những đối tượng được hiển thị
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsCustomerItems)
                    {
                        item.IsActive = false;
                        stbMessage.AppendFormat("Đã ẩn chuyên mục <b>{0}</b>.<br />", Server.HtmlEncode(item.UserName));
                    }
                    _customerDA.Save();
                    msg.ID = string.Join(",", ltsCustomerItems.Select(o => o.ID));
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

        [HttpGet]
        public string CheckByUserName(string userName, int customer)
        {
            var result = _customerDA.CheckExitsByUserName(userName, customer);
            return result ? "false" : "true";
        }

        [HttpGet]
        public string CheckByEmail(string email, int customer)
        {
            var result = _customerDA.CheckExitsByEmail(email, customer);
            return result ? "false" : "true";
        }

        [ValidateInput(false)]
        public ActionResult ResetPassword(int id)
        {

            var msg = new JsonMessage();
            var customer = _customerDA.GetById(id);
            var pass = MyBase.RandomString(4).ToLower();
            var sha1PasswordHash = CryptDecrypt.CreatePasswordHash(pass, customer.PasswordSalt);
            customer.Password = sha1PasswordHash;
            const string stringRegexCheckEmailOrWith = @"^(([^<>()[\]\\.,;:\s@\""]+"
                                                       + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
                                                       + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
                                                       + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
                                                       + @"[a-zA-Z]{2,}))$";
            if (!string.IsNullOrEmpty(customer.Email) && Regex.IsMatch(customer.Email, stringRegexCheckEmailOrWith))
            {
                _customerDA.Save();
                msg = new JsonMessage
                {
                    Erros = false,
                    ID = customer.ID.ToString(),
                    Message = string.Format("Đã Reset lại password: <b>{0}</b>", Server.HtmlEncode(customer.UserName))
                };
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            if (!string.IsNullOrEmpty(customer.Mobile))
            {
                _customerDA.Save();
                msg = new JsonMessage
                {
                    Erros = false,
                    ID = customer.ID.ToString(),
                    Message = string.Format("Đã Reset lại password: <b>{0}</b>", Server.HtmlEncode(customer.UserName))
                };

                //FDIUtils.SendSMSToCustomer(customer.UserName, customer.Mobile, customer.Mobile, pass);
            }

            if (string.IsNullOrEmpty(msg.Message))
            {
                msg.Message = "Email khách hàng không đúng và Sđt không có!";
                msg.Erros = true;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
    }
}
