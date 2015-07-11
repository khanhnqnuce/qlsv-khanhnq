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
    public class SystemDistrictController : BaseController
    {
        readonly System_DistrictDA _districtDA = new System_DistrictDA("#");
        readonly System_CityDA _cityDA = new System_CityDA("#");


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
            var model = new ModelDistrictItem
            {
                SystemActionItem = systemActionItem,
                ListItem = _districtDA.GetListSimpleByRequest(Request),
                PageHtml = _districtDA.GridHtmlPage
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
            var districtModel = _districtDA.GetById(ArrID.FirstOrDefault());
            ViewData.Model = districtModel;
            return View();
        }

        /// <summary>
        /// Form dùng cho thêm mới, sửa. Load bằng Ajax dialog
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxForm()
        {
            var districtModel = new System_District
            {
                Show = true
            };

            if (DoAction == ActionType.Edit)
                districtModel = _districtDA.GetById(ArrID.FirstOrDefault());

            ViewBag.DistrictCityID = _cityDA.GetListSimpleAll(true);
            ViewData.Model = districtModel;
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
            var district = new System_District();
            List<System_District> ltsDistrictItems;
            StringBuilder stbMessage;

            switch (DoAction)
            {
                case ActionType.Add:
                    UpdateModel(district);
                    _districtDA.Add(district);
                    _districtDA.Save();
                    msg = new JsonMessage
                    {
                        Erros = false,
                        ID = district.ID.ToString(),
                        Message = string.Format("Đã thêm mới quận huyện: <b>{0}</b>", Server.HtmlEncode(district.Name))
                    };
                    break;

                case ActionType.Edit:
                    district = _districtDA.GetById(ArrID.FirstOrDefault());
                    UpdateModel(district);
                    _districtDA.Save();
                    msg = new JsonMessage
                    {
                        Erros = false,
                        ID = district.ID.ToString(),
                        Message = string.Format("Đã cập nhật quận huyện: <b>{0}</b>", Server.HtmlEncode(district.Name))
                    };
                    break;

                case ActionType.Delete:
                    ltsDistrictItems = _districtDA.GetListByArrID(ArrID);
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsDistrictItems)
                    {
                        //if (item.System_District.Count() > 0)
                        //{
                        //    stbMessage.AppendFormat("Quận huyện <b>{0}</b> đang được sử dụng, không được phép xóa.<br />", Server.HtmlEncode(item.DistrictName));
                        //    //msg.Erros = true;
                        //}
                        //else
                        //{
                        _districtDA.Delete(item);
                        stbMessage.AppendFormat("Đã xóa quận huyện <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                        //}
                    }
                    msg.ID = string.Join(",", ArrID);
                    _districtDA.Save();
                    msg.Message = stbMessage.ToString();
                    break;

                case ActionType.Show:
                    ltsDistrictItems = _districtDA.GetListByArrID(ArrID).Where(o => !o.Show).ToList(); //Chỉ lấy những đối tượng ko được hiển thị
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsDistrictItems)
                    {
                        item.Show = true;
                        stbMessage.AppendFormat("Đã hiển thị quận huyện <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                    }
                    _districtDA.Save();
                    msg.ID = string.Join(",", ltsDistrictItems.Select(o => o.ID));
                    msg.Message = stbMessage.ToString();
                    break;

                case ActionType.Hide:
                    ltsDistrictItems = _districtDA.GetListByArrID(ArrID).Where(o => o.Show).ToList(); //Chỉ lấy những đối tượng được hiển thị
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsDistrictItems)
                    {
                        item.Show = false;
                        stbMessage.AppendFormat("Đã ẩn quận huyện <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                    }
                    _districtDA.Save();
                    msg.ID = string.Join(",", ltsDistrictItems.Select(o => o.ID));
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

        [HttpPost]
        public ActionResult getDistrictByCity(int cityId)
        {
            var list = _districtDA.GetListByCity(cityId, true);
            return Json(new { list }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Dùng cho tra cứu nhanh
        /// </summary>
        /// <returns></returns>
        public ActionResult AutoComplete()
        {
            var term = Request["term"];
            var ltsResults = _districtDA.GetListSimpleByAutoComplete(term, 10, true);
            return Json(ltsResults, JsonRequestBehavior.AllowGet);
        }
    }
}
