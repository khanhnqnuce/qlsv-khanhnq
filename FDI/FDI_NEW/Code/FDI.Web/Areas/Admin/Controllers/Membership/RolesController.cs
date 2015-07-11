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
    public class RolesController : BaseController
    {
        //
        // GET: /Admin/Roles/
        private readonly RoleModuleActiveDA _roleModuleActiveDA = new RoleModuleActiveDA("#");
        private readonly RoleDA _rolerDA = new RoleDA("#");

        public ActionResult Index()
        {            
            return View(systemActionItem);
        }

        public ActionResult ListItems()
        {
            var model = new ModelRolesItem
            {
                SystemActionItem = systemActionItem,
                ListItem = _rolerDA.GetListSimpleByRequest(Request).OrderBy(c => c.RoleName),
                PageHtml = _rolerDA.GridHtmlPage
            };

            ViewData.Model = model;
            return View();
        }

        public ActionResult AjaxView()
        {
            var role = _rolerDA.GetRoleItemById(GuiID.FirstOrDefault());
            ViewData.Model = role;
            return View();
        }

        public ActionResult AjaxViewModule()
        {
            var roleId = GuiID.FirstOrDefault();
            var model = new ModelAspnetRolesItem
                            {
                                Roles = _rolerDA.GetById(roleId),
                                ListActiveRoleItem = _rolerDA.GetActiveRoleAll()
                            };          
            ViewBag.Action = DoAction;
            ViewBag.ActionText = ActionText;
            return View(model);
        }

        public ActionResult AjaxViewRoleActive()
        {
            var roleId = Guid.Parse(Request["ItemID"]);
          
            var model = new ModelAspnetRolesItem
            {
                Roles = _rolerDA.GetById(roleId),
                ListActiveRoleItem = _rolerDA.GetActiveRoleAll()
            };

            return View(model);
        }

        public ActionResult AjaxForm()
        {
            var role = new aspnet_Roles();
            if (DoAction == ActionType.Edit)
            {
                role = _rolerDA.GetById(GuiID.FirstOrDefault());
            }

            ViewData.Model = role;
            ViewBag.Action = DoAction;
            ViewBag.ActionText = ActionText;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Actions()
        {
            var msg = new JsonMessage();
            var role = new aspnet_Roles();

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
                        var gid = Guid.NewGuid();
                        UpdateModel(role);
                        role.ApplicationId = Guid.Parse("c6e5894c-95e4-4b21-9b5d-e86f27d7c862");
                        role.RoleId = gid;
                        role.LoweredRoleName = MyString.Slug(role.RoleName);
                        _rolerDA.Add(role);
                        _rolerDA.Save();
                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = role.RoleId.ToString(),
                            Message =
                                string.Format("Đã thêm mới hành động: <b>{0}</b>.<br />",
                                              Server.HtmlEncode(role.RoleName))
                        };
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Instance.LogError(GetType(), ex);
                    }
                    break;

                case ActionType.Update:
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
                        var roleId = Guid.Parse(Request["ItemID"]);
                        role = _rolerDA.GetById(roleId);
                        var roleName = Request["ItemNameRole"];
                        var listActiveRole = _rolerDA.GetlistActiveRole();
                        foreach (var activeRole in listActiveRole)
                        {
                            if (Request[activeRole.NameActive] != null)
                                role.ActiveRoles.Add(activeRole);
                            else
                                role.ActiveRoles.Remove(activeRole);
                            _rolerDA.Save();
                        }
                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = roleId.ToString(),
                            Message =
                                string.Format("Đã cập nhật chuyên mục: <b>{0}</b>.<br />",
                                              Server.HtmlEncode(roleName))
                        };
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Instance.LogError(GetType(), ex);
                    }
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
                    role = _rolerDA.GetById(GuiID.FirstOrDefault());
                    UpdateModel(role);
                    role.LoweredRoleName = MyString.Slug(role.RoleName);
                    _rolerDA.Save();
                    msg = new JsonMessage
                    {
                        Erros = false,
                        ID = role.RoleId.ToString(),
                        Message =
                            string.Format("Đã cập nhật chuyên mục: <b>{0}</b>.<br />",
                                          Server.HtmlEncode(role.RoleName))
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
                    var ltsRolesItems = _rolerDA.getListByArrID(GuiID);
                    var stbMessage = new StringBuilder();
                    foreach (var item in ltsRolesItems)
                    {
                        if (item.ActiveRoles.Any() || item.aspnet_Users.Any() || item.Modules.Any())
                        {
                            stbMessage.AppendFormat(
                                "Chuyên mục <b>{0}</b> đang được sử dụng, không được phép xóa.<br />",
                                Server.HtmlEncode(item.RoleName));
                        }
                        else
                        {
                            _rolerDA.Delete(item);
                            stbMessage.AppendFormat("Đã xóa chuyên mục <b>{0}</b>.<br />",
                                                    Server.HtmlEncode(item.RoleName));
                        }
                    }
                    msg.ID = string.Join(",", GuiID);
                    _rolerDA.Save();
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

        public ActionResult DeleteRoleAction()
        {
            JsonMessage msg;
            if (!systemActionItem.Delete)
            {
                msg = new JsonMessage
                {
                    Erros = true,
                    Message = "Bạn chưa được phân quyền cho chức năng này!"
                };

                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            try
            {
                int moduleid = Convert.ToInt16(Request["moduleid"]);
                var roleId = Guid.Parse(Request["ItemID"]);
                var role = _rolerDA.GetById(roleId);
                var module = role.Modules.FirstOrDefault(m => m.ID == moduleid);
                if (module != null)
                {
                    var namemodule = module.NameModule;
                    role.Modules.Remove(module);
                    _rolerDA.Save();
                    var roleModuleActive = _roleModuleActiveDA.GetListRoleModuleActivekt(roleId, moduleid);
                    foreach (var moduleActive in roleModuleActive)
                    {
                        _roleModuleActiveDA.Delete(moduleActive);
                        _roleModuleActiveDA.Save();
                    }
                    msg = new JsonMessage
                    {
                        Erros = false,
                        ID = moduleid.ToString(),
                        Message = string.Format("Đã xóa chuyên mục <b>{0}</b>.<br />", Server.HtmlEncode(namemodule))
                    };
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }
                msg = new JsonMessage
                {
                    Erros = true,
                    Message = "Không có hành động nào được thực hiện."
                };
            }
            catch (Exception)
            {

                msg = new JsonMessage
                {
                    Erros = true,
                    Message = "Không có hành động nào được thực hiện."
                };
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditRoleAction()
        {

            JsonMessage msg;
            if (!systemActionItem.Edit)
            {
                msg = new JsonMessage()
                {
                    Erros = true,
                    Message = "Bạn chưa được phân quyền cho chức năng này!"
                };

                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            var moduleid = Convert.ToInt16(Request["moduleid"]);
            var roleId = Guid.Parse(Request["ItemID"]);

            var module = moduleid != 0
                             ? _roleModuleActiveDA.GetListRoleModuleActivekt(roleId, moduleid)
                             : _roleModuleActiveDA.GetListRoleModuleActivekt(roleId);
            if (module.Count > 0)
            {
                foreach (var user in module.Select(t => _roleModuleActiveDA.GetByRoleModuleActiveId(t.ID)))
                {
                    var check = Request[user.ID.ToString()];
                    user.Active = check != null;
                    _roleModuleActiveDA.Save();
                }
                msg = new JsonMessage
                {
                    Erros = false,
                    ID = moduleid.ToString(),
                    Message = string.Format("Đã cập nhật chuyên mục: <b>{0}</b>.<br />", Server.HtmlEncode("Thành công!"))
                };
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            msg = new JsonMessage
            {
                Erros = true,
                Message = "Không có hành động nào được thực hiện."
            };
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
    }
}
