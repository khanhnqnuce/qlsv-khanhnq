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
    public class SystemMenuController : BaseController
    {
        readonly System_MenuDA _menuDA = new System_MenuDA("#");

        /// <summary>
        /// Trang chủ, index. Load ra grid dưới dạng ajax
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(systemActionItem);
        }

        public ActionResult AjaxSort()
        {
            var ltsSourceMenu = _menuDA.GetAllListSimpleByParentID(ArrID.FirstOrDefault());
            ViewData.Model = ltsSourceMenu;
            return View();
        }

        /// <summary>
        /// Load danh sách bản ghi dưới dạng bảng
        /// </summary>
        /// <returns></returns>
        public ActionResult ListItems()
        {
            var ltsSourceMenu = systemActionItem.ViewFull ? _menuDA.GetAllListSimple() : new List<MenuItem>();
            var stbHtml = new StringBuilder();
            _menuDA.BuildTreeView(ltsSourceMenu, 1, false, ref stbHtml, systemActionItem.Add, systemActionItem.Delete, systemActionItem.Edit, systemActionItem.Show, systemActionItem.Order);
            ViewData.Model = stbHtml.ToString();
            return View();
        }

        /// <summary>
        /// Trang xem chi tiết trong model
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxView()
        {
            var menuModel = _menuDA.GetById(ArrID.FirstOrDefault());
            ViewData.Model = menuModel;
            return View();
        }

        /// <summary>
        /// Form dùng cho thêm mới, sửa. Load bằng Ajax dialog
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxForm()
        {
            var menuModel = new System_Menu
            {
                MenuShow = true,
                MenuOrder = 0,
                MenuParentID = (ArrID.Any()) ? ArrID.FirstOrDefault() : 0
            };

            if (DoAction == ActionType.Edit)
                menuModel = _menuDA.GetById(ArrID.FirstOrDefault());


            var ltsAllItems = _menuDA.GetAllListSimple();
            ViewBag.MenuParentID = _menuDA.GetAllSelectList(ltsAllItems, menuModel.MenuID);
            ViewData.Model = menuModel;
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
            var menu = new System_Menu();
            List<System_Menu> ltsMenuItems;
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
                    UpdateModel(menu);
                    _menuDA.Add(menu);
                    _menuDA.Save();
                    msg = new JsonMessage
                    {
                        Erros = false,
                        ID = menu.MenuID.ToString(),
                        Message = string.Format("Đã thêm mới menu: <b>{0}</b>", Server.HtmlEncode(menu.MenuTitle))
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
                    menu = _menuDA.GetById(ArrID.FirstOrDefault());
                    UpdateModel(menu);
                    _menuDA.Save();
                    msg = new JsonMessage
                    {
                        Erros = false,
                        ID = menu.MenuID.ToString(),
                        Message = string.Format("Đã cập nhật menu: <b>{0}</b>", Server.HtmlEncode(menu.MenuTitle))
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
                    ltsMenuItems = _menuDA.GetListByArrID(ArrID);
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsMenuItems)
                    {
                        //if (item.System_Menu.Count() > 0)
                        //{
                        //    stbMessage.AppendFormat("Menu <b>{0}</b> đang được sử dụng, không được phép xóa.<br />", Server.HtmlEncode(item.MenuTitle));
                        //    //msg.Erros = true;
                        //}
                        //else
                        //{
                        _menuDA.Delete(item);
                        stbMessage.AppendFormat("Đã xóa menu <b>{0}</b>.<br />", Server.HtmlEncode(item.MenuTitle));
                        //}
                    }
                    msg.ID = string.Join(",", ArrID);
                    _menuDA.Save();
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
                    ltsMenuItems = _menuDA.GetListByArrID(ArrID).Where(o => !o.MenuShow).ToList(); //Chỉ lấy những đối tượng ko được hiển thị
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsMenuItems)
                    {
                        item.MenuShow = true;
                        stbMessage.AppendFormat("Đã hiển thị menu <b>{0}</b>.<br />", Server.HtmlEncode(item.MenuTitle));
                    }
                    _menuDA.Save();
                    msg.ID = string.Join(",", ltsMenuItems.Select(o => o.MenuID));
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
                    ltsMenuItems = _menuDA.GetListByArrID(ArrID).Where(o => o.MenuShow).ToList(); //Chỉ lấy những đối tượng được hiển thị
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsMenuItems)
                    {
                        item.MenuShow = false;
                        stbMessage.AppendFormat("Đã ẩn menu <b>{0}</b>.<br />", Server.HtmlEncode(item.MenuTitle));
                    }
                    _menuDA.Save();
                    msg.ID = string.Join(",", ltsMenuItems.Select(o => o.MenuID));
                    msg.Message = stbMessage.ToString();
                    break;

                case ActionType.Order:
                    if (!systemActionItem.Order)
                    {
                        msg = new JsonMessage
                        {
                            Erros = true,
                            Message = "Bạn chưa được phân quyền cho chức năng này!"
                        };

                        return Json(msg, JsonRequestBehavior.AllowGet);
                    }
                    if (!string.IsNullOrEmpty(Request["OrderValues"]))
                    {
                        var orderValues = Request["OrderValues"];
                        if (orderValues.Contains("|"))
                        {
                            foreach (var keyValue in orderValues.Split('|'))
                            {
                                if (keyValue.Contains("_"))
                                {
                                    var tempMenu = _menuDA.GetById(Convert.ToInt32(keyValue.Split('_')[0]));
                                    tempMenu.MenuOrder = Convert.ToInt32(keyValue.Split('_')[1]);
                                    _menuDA.Save();
                                }
                            }
                        }
                        msg.ID = string.Join(",", orderValues);
                        msg.Message = "Đã cập nhật lại thứ tự menu";
                    }
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
            var ltsResults = _menuDA.GetListSimpleByAutoComplete(term, 10, true);
            return Json(ltsResults, JsonRequestBehavior.AllowGet);
        }


    }
}
