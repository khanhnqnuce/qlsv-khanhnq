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
    public class SystemCityController : BaseController
    {
        readonly System_CityDA _cityDA = new System_CityDA("#");
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
            var model = new ModelCityItem
                            {
                                SystemActionItem = systemActionItem,
                                ListItem = _cityDA.GetListSimpleByRequest(Request),
                                PageHtml = _cityDA.GridHtmlPage
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
            var cityModel = _cityDA.GetById(ArrID.FirstOrDefault());
            ViewData.Model = cityModel;
            return View();
        }

        /// <summary>
        /// Form dùng cho thêm mới, sửa. Load bằng Ajax dialog
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxForm()
        {
            var cityModel = new System_City
            {
                Show = true
            };

            if (DoAction == ActionType.Edit)
                cityModel = _cityDA.GetById(ArrID.FirstOrDefault());

            ViewData.Model = cityModel;
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
            var city = new System_City();
            List<System_City> ltsCityItems;
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
                    UpdateModel(city);
                    _cityDA.Add(city);
                    _cityDA.Save();
                    msg = new JsonMessage
                    {
                        Erros = false,
                        ID = city.ID.ToString(),
                        Message = string.Format("Đã thêm mới thành phố: <b>{0}</b>", Server.HtmlEncode(city.Name))
                    };
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
                    city = _cityDA.GetById(ArrID.FirstOrDefault());
                    UpdateModel(city);
                    _cityDA.Save();
                    msg = new JsonMessage
                    {
                        Erros = false,
                        ID = city.ID.ToString(),
                        Message = string.Format("Đã cập nhật thành phố: <b>{0}</b>", Server.HtmlEncode(city.Name))
                    };
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
                    ltsCityItems = _cityDA.GetListByArrID(ArrID);
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsCityItems)
                    {
                        if (item.System_District.Any())
                        {
                            stbMessage.AppendFormat("Thành phố <b>{0}</b> đang được sử dụng, không được phép xóa.<br />", Server.HtmlEncode(item.Name));
                            //msg.Erros = true;
                        }
                        else
                        {
                            _cityDA.Delete(item);
                            stbMessage.AppendFormat("Đã xóa thành phố <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                        }
                    }
                    msg.ID = string.Join(",", ArrID);
                    _cityDA.Save();
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
                    ltsCityItems = _cityDA.GetListByArrID(ArrID).Where(o => !o.Show).ToList(); //Chỉ lấy những đối tượng ko được hiển thị
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsCityItems)
                    {
                        item.Show = true;
                        stbMessage.AppendFormat("Đã hiển thị thành phố <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                    }
                    _cityDA.Save();
                    msg.ID = string.Join(",", ltsCityItems.Select(o => o.ID));
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
                    ltsCityItems = _cityDA.GetListByArrID(ArrID).Where(o => o.Show).ToList(); //Chỉ lấy những đối tượng được hiển thị
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsCityItems)
                    {
                        item.Show = false;
                        stbMessage.AppendFormat("Đã ẩn thành phố <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                    }
                    _cityDA.Save();
                    msg.ID = string.Join(",", ltsCityItems.Select(o => o.ID));
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
            var ltsResults = _cityDA.GetListSimpleByAutoComplete(term, 10, true);
            return Json(ltsResults, JsonRequestBehavior.AllowGet);
        }
    }
}
