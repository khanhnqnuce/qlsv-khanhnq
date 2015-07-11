using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FDI.DA.Admin;
using FDI.Entities;
using FDI.Simple;
using FDI.Utils;
using FDI.Web;
using Resources;

namespace FDI.Areas.Admin.Controllers
{
    public class BaseController : FDI.Utils.BaseController
    {
        public static SystemActionItem systemActionItem { get; set; }
        public static HtmlSettingItem HtmlSettingItem { get; set; }
        public static List<ActionActiveItem> ltsModuleActive { get; set; }
        public static List<ModuleItem> ltsModuleAllActive { get; set; }

        /// <summary>
        /// 1.  kiem tra phan quyen khi hien len view - object ltsModuleActive
        /// 2.  kiem tra phan quyen khi thuc hien action - object systemActionItem
        /// </summary>
        /// <author> linhtx </author>
        /// <datemodified> 15-Jan-2014 </datemodified>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (Request.Url != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    //var nameu = User.Identity.Name;
                    var membershipUser = Membership.GetUser();
                    if (membershipUser != null)
                    {
                        var providerUserKey = membershipUser.ProviderUserKey;
                        if (providerUserKey != null)
                        {
                            var userId = (Guid)providerUserKey;
                            var path = Request.Url.AbsolutePath.ToLower();
                            path = path.Replace(WebConfig.AdminUrl, "");
                            string[] moduleArr = path.Split('/');
                            var module = moduleArr[0]; // ProductAttribute
                            var keyCacheModule = "ltsPermission" + userId; // ltsPermissionProductAttribute
                            var keyCacheAction = "ltsTriggerAction" + module + userId; //ltsTriggerActionProductAttribute
                            const string keyCacheModuleall = "ltsModuleallAction"; //ltsTriggerActionProductAttribute

                            #region user module active

                            bool isAdmin;
                            if (HttpRuntime.Cache[keyCacheModule] == null && HttpRuntime.Cache[keyCacheModule + "isadmin"] == null)
                            {
                                ltsModuleActive = UserRoleModule(userId, out isAdmin);
                                HttpRuntime.Cache[keyCacheModule] = ltsModuleActive;
                                HttpRuntime.Cache[keyCacheModule + "isadmin"] = isAdmin;
                            }
                            else
                            {
                                isAdmin = (bool)HttpRuntime.Cache[keyCacheModule + "isadmin"];
                                ltsModuleActive = HttpRuntime.Cache[keyCacheModule] as List<ActionActiveItem>;
                            }

                            #endregion

                            #region module
                            if (HttpRuntime.Cache[keyCacheModuleall] == null)
                            {
                                var moduleDA = new ModuleDA("#");
                                ltsModuleAllActive = moduleDA.GetAllListSimple();
                                HttpRuntime.Cache[keyCacheModuleall] = ltsModuleAllActive;
                            }
                            else
                            {
                                ltsModuleAllActive = HttpRuntime.Cache[keyCacheModuleall] as List<ModuleItem>;
                            }

                            #endregion

                            #region trigger action
                            if (HttpRuntime.Cache[keyCacheAction] == null)
                            {
                                systemActionItem = new SystemActionItem();

                                if (ltsModuleAllActive != null && ltsModuleAllActive.Count > 0)
                                {
                                    if (ltsModuleAllActive.Any(m => m.Tag == module))
                                    {
                                        var listModule = ltsModuleAllActive.FirstOrDefault(c => c.PrarentID == 1 && c.Tag == module) ??
                                                         ltsModuleAllActive.FirstOrDefault(c => c.PrarentID > 1 && c.Tag == module);
                                        if (ltsModuleActive != null && ltsModuleActive.Count > 0)
                                        {
                                            var listModuleActive = ltsModuleActive.Where(c => c.ModuleId == listModule.ID);
                                            if (!listModuleActive.Any())
                                            {
                                                listModule = ltsModuleAllActive.FirstOrDefault(c => c.PrarentID > 1 && c.Tag == module);
                                                listModuleActive = ltsModuleActive.Where(c => listModule != null && c.ModuleId == listModule.ID);
                                                if (!listModuleActive.Any())
                                                {
                                                    listModuleActive = ltsModuleActive.Where(c => listModule != null && c.ModuleId == listModule.PrarentID);
                                                }
                                            }
                                            if (listModuleActive.Any())
                                            {
                                                systemActionItem.ViewFull = true;
                                                systemActionItem.IsAdmin = isAdmin;
                                            }
                                            foreach (var item in listModuleActive)
                                            {
                                                if (item.NameActive == "Add")
                                                    systemActionItem.Add = true;
                                                if (item.NameActive == "View")
                                                    systemActionItem.View = true;
                                                if (item.NameActive == "Edit")
                                                    systemActionItem.Edit = true;
                                                if (item.NameActive == "Delete")
                                                    systemActionItem.Delete = true;
                                                if (item.NameActive == "Show")
                                                    systemActionItem.Show = true;
                                                if (item.NameActive == "Active")
                                                    systemActionItem.Active = true;
                                                if (item.NameActive == "Hide")
                                                    systemActionItem.Hide = true;
                                                if (item.NameActive == "Order")
                                                    systemActionItem.Order = true;
                                                if (item.NameActive == "Public")
                                                    systemActionItem.Public = true;
                                                if (item.NameActive == "Complete")
                                                    systemActionItem.Complete = true;
                                            }
                                        }

                                    }
                                }
                                HttpRuntime.Cache[keyCacheAction] = systemActionItem;
                            }
                            else
                            {
                                systemActionItem = HttpRuntime.Cache[keyCacheAction] as SystemActionItem;
                            }
                            #endregion
                        }
                    }
                }
            }

        }

        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    Elmah.ErrorSignal.FromCurrentContext().Raise(filterContext.Exception);

        //    filterContext.Result = Content("Có lỗi xảy ra! bạn hãy thử lại <br />" + filterContext.Exception.Message + "<br />" + filterContext.Exception.StackTrace);

        //    filterContext.ExceptionHandled = true;
        //    filterContext.HttpContext.Response.Clear();

        //    //base.OnException(filterContext);
        //}

        public string ActionText
        {
            get
            {
                if (!string.IsNullOrEmpty(Request["do"]))
                {
                    switch (Request["do"].Trim().ToLower())
                    {
                        default:
                        case "add":
                            return CSResourceString.Add;
                        case "delete":
                            return CSResourceString.Delete;

                        case "edit":
                            return CSResourceString.Edit;

                        case "update":
                            return CSResourceString.Update;

                        case "usermodule":
                            return CSResourceString.UserModule;

                        case "ronemodule":
                            return CSResourceString.RoleModule;

                        case "show":
                            return CSResourceString.Show;

                        case "hide":
                            return CSResourceString.Hide;

                        case "order":
                            return CSResourceString.Order;

                        case "active":
                            return CSResourceString.Active;

                        case "public":
                            return CSResourceString.Public;

                        case "complete":
                            return CSResourceString.Complete;
                    }
                }
                return CSResourceString.View;
            }
        }

        public ActionType DoAction
        {
            get
            {
                if (!string.IsNullOrEmpty(Request["do"]))
                {
                    switch (Request["do"].Trim().ToLower())
                    {
                        default:
                        case "add":
                            return ActionType.Add;

                        case "delete":
                            return ActionType.Delete;

                        case "update":
                            return ActionType.Update;
                        case "edit":
                            return ActionType.Edit;

                        case "show":
                            return ActionType.Show;

                        case "usermodule":
                            return ActionType.UserModule;

                        case "rolemodule":
                            return ActionType.RoleModule;

                        case "hide":
                            return ActionType.Hide;

                        case "order":
                            return ActionType.Order;

                        case "active":
                            return ActionType.Active;

                        case "public":
                            return ActionType.Public;

                        case "complete":
                            return ActionType.Complete;
                        case "excel":
                            return ActionType.Excel;
                    }
                }
                return ActionType.View;
            }
        }

        public List<int> ArrID
        {
            get
            {
                if (!string.IsNullOrEmpty(Request["itemID"]))
                {
                    if (Request["ItemID"].Contains(","))
                    {
                        return Request["ItemID"].Trim().Split(',').Select(o => Convert.ToInt32(o)).ToList();
                    }
                    var ltsTemp = new List<int> { Convert.ToInt32(Request["ItemID"]) };
                    return ltsTemp;
                }
                return new List<int>();
            }
        }

        // Dongdt 22/11/2013
        public List<Guid> GuiID
        {
            get
            {
                if (!string.IsNullOrEmpty(Request["itemID"]))
                {
                    if (Request["ItemID"].Contains(","))
                    {
                        return Request["ItemID"].Trim().Split(',').Select(Guid.Parse).ToList();
                        //return Request["ItemID"].Trim().Split(',').Select(o => o).ToList();
                    }
                    var ltsTemp = new List<Guid> { Guid.Parse(Request["ItemID"]) };
                    return ltsTemp;
                }
                return new List<Guid>();
            }
        }


        public List<long> ArrLongID
        {
            get
            {
                if (!string.IsNullOrEmpty(Request["itemID"]))
                {
                    if (Request["ItemID"].Contains(","))
                    {
                        return Request["ItemID"].Trim().Split(',').Select(o => Convert.ToInt64(o)).ToList();
                    }
                    var ltsTemp = new List<long> { Convert.ToInt64(Request["ItemID"]) };
                    return ltsTemp;
                }
                return new List<long>();
            }
        }

        public List<FileItem> ListFileUpload
        {
            get
            {
                var ltsFileItem = new List<FileItem>();
                if (!string.IsNullOrEmpty(Request["listValueFileAttach"]))
                {
                    var strListFileAttach = Request["listValueFileAttach"];
                    var oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    var ltsFileForm = oSerializer.Deserialize<List<FileAttachForm>>(strListFileAttach);
                    const string filePath = "/Uploads/ajaxUpload/";
                    ltsFileItem.AddRange(ltsFileForm.Select(fileForm => new FileItem
                    {
                        Name = fileForm.FileName,
                        Data = MyBase.ReadFile(Server.MapPath(filePath + fileForm.FileServer)),
                        Url = Server.MapPath(filePath + fileForm.FileServer)
                    }));
                }
                return ltsFileItem;
            }
        }

        public List<int> ListFileRemove
        {
            get
            {
                if (!string.IsNullOrEmpty(Request["listValueFileAttachRemove"]))
                {
                    if (Request["listValueFileAttachRemove"].Contains(","))
                    {
                        return Request["listValueFileAttachRemove"].Trim().Split(',').Select(o => Convert.ToInt32(o)).ToList();
                    }
                    var ltsTemp = new List<int> { Convert.ToInt32(Request["listValueFileAttachRemove"]) };
                    return ltsTemp;
                }
                return new List<int>();
            }
        }

        // Phân quyền DongDT
        //public List<ActionActiveItem> RoleUser(Guid userId, int moduleId, string tag)
        //{

        //    var listRoleUser = new List<User_ModuleActiveViewItem>();
        //    var userModuleActiveDA = new User_ModuleActiveDA("#");
        //    var adminDA = new AdminDA("#");
        //    var moduleDA = new ModuleDA("#");
        //    var ltsSourceModule = adminDA.getAllListSimple();

        //    //Kiểm tra module có trong danh sách isshow == true
        //    var listt = ltsSourceModule.Where(m => m.Id == moduleId).ToList();
        //    if (listt.Count == 0)
        //    {
        //        return new List<User_ModuleActiveViewItem>();
        //    }
        //    var prarentID = listt[0].PrarentID;

        //    //Kiểm tra cha của Module
        //    var ltsSourceModulecha = ltsSourceModule.OrderBy(o => o.Ord).Where(m => m.PrarentID == moduleId).ToList();
        //    var listm = ltsSourceModulecha.Where(m => m.Tag.ToLower() == tag).ToList();
        //    if (listm.Count > 0)
        //    {
        //        moduleId = listm[0].Id;
        //        prarentID = listm[0].PrarentID;
        //    }

        //    //Lấy các tính năng của User trong bảng User_ModuleActive tướng ứng với Module
        //    var listuma = userModuleActiveDA.getListUser_ModuleActiveM(userId, moduleId);
        //    if (listuma.Count == 0)
        //    {
        //        listuma = userModuleActiveDA.getListUser_ModuleActiveM(userId, prarentID);
        //        if (listuma.Count > 0)
        //        {
        //            foreach (var t in listuma)
        //            {
        //                t.ModuleId = moduleId;
        //                t.Tag = tag;
        //                t.NameModule = "";
        //            }
        //        }
        //    }

        //    // Kiểm tra User đã gán quyền Module chưa theo tính ưu tiên Gán User trước Role
        //    if (userModuleActiveDA.checkListUser_ModuleActiveM(userId))
        //    {
        //        return listuma.Count > 0 ? listuma.Where(m => m.Active).ToList() : new List<User_ModuleActiveViewItem>();
        //    }

        //    //Lấy các Tính năng của User với Role trong bảng Role_Module
        //    var adminUserRoleModule = adminDA.getListRole_ModuleActive(userId, moduleId);

        //    if (adminUserRoleModule.Count == 0)
        //    {
        //        adminUserRoleModule = adminDA.getListRole_ModuleActive(userId, prarentID);
        //    }

        //    if (adminUserRoleModule.Count > 0)
        //    {
        //        //Lấy ra danh sách Tính năng của các Module với Role
        //        var rolemoduleactive1 = moduleDA.GetlistRoleActiveRole(userId);
        //        if (rolemoduleactive1.Count > 0)
        //        {
        //            var rolemoduleac1 = rolemoduleactive1.Where(m => m.RoleId == rolemoduleactive1[0].RoleId).ToList();
        //            foreach (var roleActiveItem in adminUserRoleModule.Where(roleActiveItem => rolemoduleac1.All(m => m.ActiveRoleId != roleActiveItem.ActiveRoleId)))
        //            {
        //                roleActiveItem.Active = false;
        //            }
        //            listRoleUser.AddRange(adminUserRoleModule.Where(m => m.Active).Select(rolemoduleac => new User_ModuleActiveViewItem
        //            {
        //                Id = 1,
        //                ModuleId = rolemoduleac.ModuleId,
        //                Tag = tag,
        //                UserId = userId,
        //                UserName = "",
        //                NameModule = "",
        //                Active = rolemoduleac.Active,
        //                ActiveRoleId = rolemoduleac.ActiveRoleId,
        //                NameActive = rolemoduleac.NameActive,
        //                Check = 1
        //            }));
        //        }
        //    }
        //    return listRoleUser;
        //}

        public List<ActionActiveItem> UserRoleModule(Guid userId, out bool isAdmin)
        {
            var userDA = new UserDA("#");
            var user = userDA.GetById(userId);
            if (user.aspnet_Roles.Any())
            {
                var role = user.aspnet_Roles.FirstOrDefault();

                if (role != null)
                {
                    isAdmin = CheckAdmin(role.RoleName);
                    if (user.User_ModuleActive.Any())
                    {
                        var query = (from c in user.User_ModuleActive
                                     where c.Active == true
                                     select new ActionActiveItem
                                     {
                                         ModuleId = c.ModuleId,
                                         ActiveRoleId = c.ActiveRoleId,
                                         NameActive = c.ActiveRole.NameActive,
                                         Active = true
                                     }).ToList();
                        return query;
                    }
                    var queryrole = (from c in role.Role_ModuleActive
                                     where c.Active == true
                                     select new ActionActiveItem
                                     {
                                         ModuleId = c.ModuleId,
                                         ActiveRoleId = c.ActiveRoleId,
                                         NameActive = c.ActiveRole.NameActive,
                                         Active = true
                                     }).ToList();
                    return queryrole;
                }
            }
            isAdmin = false;
            return new List<ActionActiveItem>();
        }


        ///// <summary>
        ///// add by DuongNT
        ///// 06-05-2014
        ///// use render partial view to string
        ///// </summary>
        ///// <param name="viewName"></param>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //public string RenderPartialViewToString(string viewName, object model)
        //{
        //    if (string.IsNullOrEmpty(viewName))
        //        viewName = ControllerContext.RouteData.GetRequiredString("action");

        //    ViewData.Model = model;

        //    using (var sw = new StringWriter())
        //    {
        //        ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
        //        var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
        //        viewResult.View.Render(viewContext, sw);

        //        return sw.GetStringBuilder().ToString();
        //    }
        //}

    }
}
