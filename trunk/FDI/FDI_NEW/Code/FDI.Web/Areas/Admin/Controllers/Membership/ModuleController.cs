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
    public class ModuleController : BaseController
    {
        //
        // GET: /Admin/Module/

        private readonly ModuleDA _moduleDA;
        private readonly RoleDA _roleDA;
        private readonly UserDA _userDA;

        public ModuleController()
        {
            _userDA = new UserDA("#");
            _roleDA = new RoleDA("#");
            _moduleDA = new ModuleDA("#");
        }

        public ActionResult Index()
        {            
            return View(systemActionItem);
        }
        public ActionResult AjaxTreeSelect()
        {
            var ltsSourceModule = _moduleDA.GetAllListSimple();
            var ltsValues = MyBase.ConvertStringToListInt(Request["ValuesSelected"]);
            var stbHtml = new StringBuilder();
            _moduleDA.BuildTreeViewCheckBox(ltsSourceModule, 1, true, ltsValues, ref stbHtml);

            var model = new ModelModuleItem
            {
                Container = Request["Container"],
                SelectMutil = Convert.ToBoolean(Request["SelectMutil"]),
                SystemActionItem = systemActionItem,
                StbHtml = stbHtml.ToString()
            };
            ViewData.Model = model;
            return View();
        }

        public ActionResult AjaxSort()
        {
            var ltsSourceCategory = _moduleDA.GetAllListSimpleByParentID(ArrID.FirstOrDefault());
            ViewData.Model = ltsSourceCategory;
            return View();
        }

        /// <summary>
        /// Load danh sách bản ghi dưới dạng bảng
        /// </summary>
        /// <returns></returns>
        public ActionResult ListItems()
        {
            var ltsSourceModule = _moduleDA.GetAllListSimple();
            var stbHtml = new StringBuilder();
            _moduleDA.BuildTreeView(ltsSourceModule, 1, false, ref stbHtml, systemActionItem.Add, systemActionItem.Delete, systemActionItem.Edit, systemActionItem.Show, systemActionItem.Order);
            ViewData.Model = stbHtml.ToString();
            return View();
        }

        /// <summary>
        /// Form dùng cho thêm mới, sửa. Load bằng Ajax dialog
        /// </summary>
        /// <returns></returns>
        /// 

        //[ValidateInput(false)]
        public ActionResult AjaxForm()
        {
            var moduleModel = new Module
            {
                IsShow = true,
                Ord = 0,
                PrarentID = (ArrID.Any()) ? ArrID.FirstOrDefault() : 0
            };

            if (DoAction == ActionType.Edit)
                moduleModel = _moduleDA.GetById(ArrID.FirstOrDefault());
            var ltsAllItems = _moduleDA.GetAllListSimple();
            ltsAllItems = _moduleDA.GetAllSelectList(ltsAllItems, moduleModel.ID, true);
            ViewBag.PrarentID = ltsAllItems;
            ViewData.Model = moduleModel;
            ViewBag.Action = DoAction;
            ViewBag.ActionText = ActionText;
            return View();
        }

        public ActionResult AjaxRoleForm()
        {
            var id = ArrID.FirstOrDefault();
            var role = _roleDA.GetListItemAll();
            ViewBag.Id = id;
            return View(role);
        }

        public ActionResult AjaxUserForm()
        {
            var id = ArrID.FirstOrDefault();
            var user = _userDA.GetListAll();
            ViewBag.Id = id;
            return View(user);
        }
        /// <summary>
        /// Hứng các giá trị, phục vụ cho thêm, sửa, xóa, ẩn, hiện
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Actions()
        {
            var msg = new JsonMessage();
            var module = new Module();
            List<Module> ltsModuleItems;
            StringBuilder stbMessage;

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
                        UpdateModel(module);
                        _moduleDA.Add(module);
                        module.IsShow = true;
                        _moduleDA.Save();
                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = module.ID.ToString(),
                            Message = string.Format("Đã thêm mới chuyên mục: <b>{0}</b>", Server.HtmlEncode(module.NameModule))
                        };
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Instance.LogError(GetType(), ex);
                    }
                    break;
                case ActionType.RoleModule:
                    try
                    {
                        if (!systemActionItem.IsAdmin)
                        {
                            msg = new JsonMessage
                            {
                                Erros = true,
                                Message = "Bạn chưa được phân quyền cho chức năng này!"
                            };

                            return Json(msg, JsonRequestBehavior.AllowGet);
                        }
                        var rId = Guid.Parse(Request["UserID"]);
                        int moduleId = Convert.ToInt16(Request["ItemID"]);
                        module = _moduleDA.GetById(moduleId);
                        var list = module.aspnet_Roles.Any(m => m.RoleId == rId);
                        if (!list)
                        {
                            var rolemo = _moduleDA.GetByRoleId(rId);
                            module.aspnet_Roles.Add(rolemo);
                            _moduleDA.Save();
                            foreach (var roleM in rolemo.ActiveRoles.Select(role => new Role_ModuleActive
                            {
                                ModuleId = moduleId,
                                RoleId = rId,
                                Check = false,
                                ActiveRoleId = role.Id,
                                Active = true
                            }))
                            {
                                module.Role_ModuleActive.Add(roleM);
                                _moduleDA.Save();
                            }
                        }

                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = module.ID.ToString(),
                            Message = string.Format("Đã Gán Module Cho Role: <b>{0}</b>", Server.HtmlEncode(rId.ToString()))
                        };
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Instance.LogError(GetType(), ex);
                    }
                    break;

                case ActionType.UserModule:
                    try
                    {
                        if (!systemActionItem.IsAdmin)
                        {
                            msg = new JsonMessage
                            {
                                Erros = true,
                                Message = "Bạn chưa được phân quyền cho chức năng này!"
                            };

                            return Json(msg, JsonRequestBehavior.AllowGet);
                        }
                        var userId = Guid.Parse(Request["PrarentID"]);
                        int id = Convert.ToInt16(Request["ItemID"]);
                        module = _moduleDA.GetById(id);
                        var user = _moduleDA.GetByUserId(userId);
                        var kiemtra = module.aspnet_Users.Select(c => c.UserId == userId);
                        if (!kiemtra.Any())
                        {

                            module.aspnet_Users.Add(user);
                            _moduleDA.Save();
                            var firstOrDefault = user.aspnet_Roles.FirstOrDefault();
                            if (firstOrDefault != null)
                                foreach (var userModuleActive in firstOrDefault.ActiveRoles.Select(moduleactive => new User_ModuleActive
                                {
                                    ID = 1,
                                    ModuleId = id,
                                    UserId = userId,
                                    ActiveRoleId = moduleactive.Id,
                                    Active = true,
                                    Check = 1
                                }))
                                {
                                    module.User_ModuleActive.Add(userModuleActive);
                                    _moduleDA.Save();
                                }
                            else
                            {
                                msg = new JsonMessage
                                {
                                    Erros = true,
                                    ID = module.ID.ToString(),
                                    Message = string.Format("<b>{0}</b> Phải gán Role: ", Server.HtmlEncode(user.UserName))
                                };
                                break;
                            }
                        }
                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = module.ID.ToString(),
                            Message = string.Format("Đã Gán Module: <b>{0}</b>", Server.HtmlEncode(user.UserName))
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
                        if (!systemActionItem.Edit)
                        {
                            msg = new JsonMessage
                            {
                                Erros = true,
                                Message = "Bạn chưa được phân quyền cho chức năng này!"
                            };

                            return Json(msg, JsonRequestBehavior.AllowGet);
                        }
                        module = _moduleDA.GetById(ArrID.FirstOrDefault());
                        UpdateModel(module);
                        _moduleDA.Save();
                        msg = new JsonMessage
                        {
                            Erros = false,
                            ID = module.ID.ToString(),
                            Message = string.Format("Đã cập nhật chuyên mục: <b>{0}</b>", Server.HtmlEncode(module.NameModule))
                        };
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Instance.LogError(GetType(), ex);
                    }
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
                    ltsModuleItems = _moduleDA.GetListByArrID(ArrID);
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsModuleItems)
                    {
                        if (item.aspnet_Roles.Any() || item.aspnet_Users.Any())
                        {
                            stbMessage.AppendFormat("Chuyên mục <b>{0}</b> đang được sử dụng, không được phép xóa.<br />", Server.HtmlEncode(item.NameModule));
                        }
                        else
                        {
                            _moduleDA.Delete(item);
                            stbMessage.AppendFormat("Đã xóa chuyên mục <b>{0}</b>.<br />", Server.HtmlEncode(item.NameModule));
                        }
                    }
                    msg.ID = string.Join(",", ArrID);
                    _moduleDA.Save();
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
                    ltsModuleItems = _moduleDA.GetListByArrID(ArrID).Where(o => o.IsShow != null && !o.IsShow.Value).ToList(); //Chỉ lấy những đối tượng ko được hiển thị
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsModuleItems)
                    {
                        item.IsShow = true;
                        stbMessage.AppendFormat("Đã hiển thị chuyên mục <b>{0}</b>.<br />", Server.HtmlEncode(item.NameModule));
                    }
                    _moduleDA.Save();
                    msg.ID = string.Join(",", ltsModuleItems.Select(o => o.ID));
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
                    ltsModuleItems = _moduleDA.GetListByArrID(ArrID).Where(o => o.IsShow != null && o.IsShow.Value).ToList(); //Chỉ lấy những đối tượng được hiển thị
                    stbMessage = new StringBuilder();
                    foreach (var item in ltsModuleItems)
                    {
                        item.IsShow = false;
                        stbMessage.AppendFormat("Đã ẩn chuyên mục <b>{0}</b>.<br />", Server.HtmlEncode(item.NameModule));
                    }
                    _moduleDA.Save();
                    msg.ID = string.Join(",", ltsModuleItems.Select(o => o.ID));
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
                                    var tempCategory = _moduleDA.GetById(Convert.ToInt32(keyValue.Split('_')[0]));
                                    tempCategory.Ord = Convert.ToInt32(keyValue.Split('_')[1]);
                                    _moduleDA.Save();
                                }
                            }
                        }
                        msg.ID = string.Join(",", orderValues);
                        msg.Message = "Đã cập nhật lại thứ tự chuyên mục";
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
            var ltsResults = _moduleDA.GetListSimpleByAutoComplete(term, 10, true);
            return Json(ltsResults, JsonRequestBehavior.AllowGet);
        }
    }
}
