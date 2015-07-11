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
    public class ActiveRoleController : BaseController
    {
        //
        // GET: /Admin/ActiveRole/
        private readonly ActiveRoleDA _activeRoleDA;
        public ActiveRoleController()
        {
            _activeRoleDA = new ActiveRoleDA("#");
        }

        public ActionResult Index()
        {
            return View(systemActionItem);
        }
        public ActionResult ListItems()
        {
            var listactiveRoleItem = _activeRoleDA.GetListSimpleByRequest(Request).OrderBy(m => m.Ord);
            var model = new ModelActiveRoleItem
                            {
                                SystemActionItem = systemActionItem,
                                ListItem = listactiveRoleItem,
                                PageHtml = _activeRoleDA.GridHtmlPage
                            };

            ViewData.Model = model;            
            return View();
        }

        public ActionResult AjaxView()
        {
            var role = _activeRoleDA.GetRoleItemById(ArrID.FirstOrDefault());
            ViewData.Model = role;
            return View();
        }

        public ActionResult AjaxForm()
        {
            var activeRole = new ActiveRole();
            if (DoAction == ActionType.Edit)
            {
                activeRole = _activeRoleDA.GetById(ArrID.FirstOrDefault());
            }

            ViewData.Model = activeRole;
            ViewBag.Action = DoAction;
            ViewBag.ActionText = ActionText;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Actions()
        {
            var msg = new JsonMessage();
            var activeRole = new ActiveRole();

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
                        UpdateModel(activeRole);
                        _activeRoleDA.Add(activeRole);
                        _activeRoleDA.Save();
                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = activeRole.Id.ToString(),
                            Message =
                                string.Format("Đã thêm mới hành động: <b>{0}</b>",
                                              Server.HtmlEncode(activeRole.NameActive))
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
                        activeRole = _activeRoleDA.GetById(ArrID.FirstOrDefault());
                        UpdateModel(activeRole);
                        _activeRoleDA.Save();
                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = activeRole.Id.ToString(),
                            Message = string.Format("Đã cập nhật chuyên mục: <b>{0}</b>", Server.HtmlEncode(activeRole.NameActive))
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
                    var ltsRolesItems = _activeRoleDA.GetListByArrID(ArrID);
                    var stbMessage = new StringBuilder();
                    foreach (var item in ltsRolesItems)
                    {
                        if (item.aspnet_Roles.Count > 0)
                        {
                            stbMessage.AppendFormat("Chuyên mục <b>{0}</b> đang được sử dụng, không được phép xóa.<br />", Server.HtmlEncode(item.NameActive));
                            //msg.Erros = true;
                        }
                        else
                        {
                            _activeRoleDA.Delete(item);
                            stbMessage.AppendFormat("Đã xóa chuyên mục <b>{0}</b>.<br />", Server.HtmlEncode(item.NameActive));
                        }
                    }
                    msg.ID = string.Join(",", ArrID);
                    _activeRoleDA.Save();
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
