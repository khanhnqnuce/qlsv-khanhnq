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
    public class SystemConfigController : BaseController
    {
        //
        // GET: /Admin/Customer/
        //readonly AdminUtilityDA _adminUtilityDA = new AdminUtilityDA();
        private readonly SystemConfigDA _systemConfigDA;


        public SystemConfigController()
        {
            _systemConfigDA = new SystemConfigDA("#");

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
            var model = new ModelSystemConfigItem
            {
                SystemActionItem = systemActionItem,
                ListItem = _systemConfigDA.GetListSimpleByRequest(Request),
                PageHtml = _systemConfigDA.GridHtmlPage
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
            var customerType = _systemConfigDA.GetById(ArrID.FirstOrDefault());
            ViewData.Model = customerType;
            return View();
        }


        /// <summary>
        /// Form dùng cho thêm mới, sửa. Load bằng Ajax dialog
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxForm()
        {
            var systemConfig = new System_Config();

            //list dropdownlist type customer

            if (DoAction == ActionType.Edit)
            {
                systemConfig = _systemConfigDA.GetById(ArrID.FirstOrDefault());
            }

            ViewData.Model = systemConfig;
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
            var systemConfig = new System_Config();
            List<System_Config> ltsSystemConfigs;
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
                        UpdateModel(systemConfig);
                        systemConfig.DateCreate = DateTime.Now;
                        _systemConfigDA.Add(systemConfig);
                        _systemConfigDA.Save();
                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = systemConfig.ID.ToString(),
                            Message = string.Format("Đã thêm mới hành động: <b>{0}</b>", Server.HtmlEncode(systemConfig.Name))
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
                        systemConfig = _systemConfigDA.GetById(ArrID.FirstOrDefault());
                        UpdateModel(systemConfig);
                        _systemConfigDA.Save();

                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = systemConfig.ID.ToString(),
                            Message = string.Format("Đã cập nhật chuyên mục: <b>{0}</b>", Server.HtmlEncode(systemConfig.Name))
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
                    ltsSystemConfigs = _systemConfigDA.GetListByArrID(ArrID);
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsSystemConfigs)
                    {
                        _systemConfigDA.Delete(item);
                        stbMessage.AppendFormat("Đã xóa chuyên mục <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));

                    }
                    msg.ID = string.Join(",", ArrID);
                    _systemConfigDA.Save();
                    msg.Message = stbMessage.ToString();
                    break;
                case ActionType.Show:
                    ltsSystemConfigs = _systemConfigDA.GetListByArrID(ArrID).Where(o => o.IsShow != null && !o.IsShow.Value).ToList(); //Chỉ lấy những đối tượng ko được hiển thị
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsSystemConfigs)
                    {
                        item.IsShow = true;
                        stbMessage.AppendFormat("Đã hiển thị chuyên mục <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                    }
                    _systemConfigDA.Save();
                    msg.ID = string.Join(",", ltsSystemConfigs.Select(o => o.ID));
                    msg.Message = stbMessage.ToString();
                    break;

                case ActionType.Hide:
                    ltsSystemConfigs = _systemConfigDA.GetListByArrID(ArrID).Where(o => o.IsShow != null && o.IsShow.Value).ToList(); //Chỉ lấy những đối tượng được hiển thị
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsSystemConfigs)
                    {
                        item.IsShow = false;
                        stbMessage.AppendFormat("Đã ẩn chuyên mục <b>{0}</b>.<br />", Server.HtmlEncode(item.Name));
                    }
                    _systemConfigDA.Save();
                    msg.ID = string.Join(",", ltsSystemConfigs.Select(o => o.ID));
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
