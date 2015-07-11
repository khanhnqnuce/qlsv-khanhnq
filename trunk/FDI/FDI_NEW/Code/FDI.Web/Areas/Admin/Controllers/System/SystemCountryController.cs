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
    public class SystemCountryController : BaseController
    {
        readonly System_CountryDA _countryDA = new System_CountryDA("#");

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

            var model = new ModelCountryItem
            {
                SystemActionItem = systemActionItem,
                ListItem = _countryDA.GetListSimpleByRequest(Request),
                PageHtml = _countryDA.GridHtmlPage
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
            var countryModel = _countryDA.GetById(ArrID.FirstOrDefault());
            ViewData.Model = countryModel;
            return View();
        }

        /// <summary>
        /// Form dùng cho thêm mới, sửa. Load bằng Ajax dialog
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxForm()
        {
            var countryModel = new System_Country
            {
                Show = true
            };

            if (DoAction == ActionType.Edit)
                countryModel = _countryDA.GetById(ArrID.FirstOrDefault());

            ViewData.Model = countryModel;
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
            var country = new System_Country();
            List<System_Country> ltsCountryItems;
            StringBuilder stbMessage;

            switch (DoAction)
            {
                case ActionType.Add:
                    UpdateModel(country);
                    _countryDA.Add(country);
                    _countryDA.Save();
                    msg = new JsonMessage
                    {
                        Erros = false,
                        ID = country.ID.ToString(),
                        Message = string.Format("Đã thêm mới quốc gia: <b>{0}</b>", Server.HtmlEncode(country.Name))
                    };
                    break;

                case ActionType.Edit:
                    country = _countryDA.GetById(ArrID.FirstOrDefault());
                    UpdateModel(country);
                    _countryDA.Save();
                    msg = new JsonMessage
                    {
                        Erros = false,
                        ID = country.ID.ToString(),
                        Message = string.Format("Đã cập nhật quốc gia: <b>{0}</b>", Server.HtmlEncode(country.Name))
                    };
                    break;

                case ActionType.Delete:
                    ltsCountryItems = _countryDA.GetListByArrID(ArrID);
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsCountryItems)
                    {
                        //if (item.Shop_Product.Count() > 0)
                        //{
                        //    stbMessage.AppendFormat("Quốc gia <b>{0}</b> đang được sử dụng, không được phép xóa.<br />", Server.HtmlEncode(item.CountryName));
                        //    //msg.Erros = true;
                        //}
                        //else
                        //{
                        //    countryDA.Delete(item);
                        //    stbMessage.AppendFormat("Đã xóa quốc gia <b>{0}</b>.<br />", Server.HtmlEncode(item.CountryName));
                        //}
                    }
                    msg.ID = string.Join(",", ArrID);
                    _countryDA.Save();
                    msg.Message = stbMessage.ToString();
                    break;

                case ActionType.Show:
                    ltsCountryItems = _countryDA.GetListByArrID(ArrID).Where(o => !o.Show).ToList(); //Chỉ lấy những đối tượng ko được hiển thị
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsCountryItems)
                    {
                        item.Show = true;
                        stbMessage.AppendFormat("Đã hiển thị quốc gia <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                    }
                    _countryDA.Save();
                    msg.ID = string.Join(",", ltsCountryItems.Select(o => o.ID));
                    msg.Message = stbMessage.ToString();
                    break;

                case ActionType.Hide:
                    ltsCountryItems = _countryDA.GetListByArrID(ArrID).Where(o => o.Show).ToList(); //Chỉ lấy những đối tượng được hiển thị
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsCountryItems)
                    {
                        item.Show = false;
                        stbMessage.AppendFormat("Đã ẩn quốc gia <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                    }
                    _countryDA.Save();
                    msg.ID = string.Join(",", ltsCountryItems.Select(o => o.ID));
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
            var ltsResults = _countryDA.GetListSimpleByAutoComplete(term, 10, true);
            return Json(ltsResults, JsonRequestBehavior.AllowGet);
        }
    }
}
