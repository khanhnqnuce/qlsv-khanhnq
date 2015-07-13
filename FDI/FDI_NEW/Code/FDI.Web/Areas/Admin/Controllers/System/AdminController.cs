using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using FDI.DA.Admin;
using FDI.Simple;
using FDI.Utils;

namespace FDI.Areas.Admin.Controllers
{
    public class AdminController : BaseController
    {
        //
        // GET: /Admin/Admin/   

        private readonly AdminDA _adminDA;
        //private readonly IRolesService _rolesService;
        public AdminController()
        {
            _adminDA = new AdminDA("#");
            //_rolesService = rolesService;
            //_statisticsDa = new Shop_StatisticsDA("#");
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Menu()
        {
            #region  lay list phan quyen
            var ltsSourceModule = _adminDA.getAllListSimple();
            var listcap1 = ltsSourceModule.OrderBy(o => o.Ord).Where(m => m.PrarentID == 1).ToList();
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
                        var userModule = _adminDA.getUser_ModuleById(userId);

                        if (userModule.Modules.Any())
                        {
                            foreach (var umodule in userModule.Modules.OrderBy(m => m.ID))
                            {
                                // lấy tất cả ModuleId trong User_Module gán vào biến Active = 1
                                var listb = ltsSourceModule.Where(c => c.ID == umodule.ID).ToList();
                                if (listb.Count > 0)
                                {
                                    listb[0].Active = (int)PermisionType.InheritFromParent;
                                }

                                foreach (var modu in listcap1)
                                {
                                    // Kiểm tra Module đang được trỏ có phải là cấp cha
                                    if (modu.ID == umodule.ID)
                                    {
                                        modu.Active = (int)PermisionType.InheritFromParent;
                                        // Lấy ra danh sách các cấp con của Module được trỏ
                                        var listPra = ltsSourceModule.OrderBy(o => o.Ord).Where(m => m.PrarentID == umodule.ID).ToList();
                                        if (listPra.Count > 0)
                                        {
                                            foreach (var modul in listPra)
                                            {
                                                modul.Active = (int)PermisionType.InheritFromParent;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // Lấy ra Module đang được trỏ đến để kiểm tra cấp cha
                                        var listlayc = ltsSourceModule.FirstOrDefault(m => m.ID == umodule.ID);

                                        foreach (var module in listcap1.Where(module => listlayc != null && listlayc.PrarentID == module.ID))
                                        {
                                            module.Active = (int)PermisionType.PermisionItself;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            var role = Roles.GetRolesForUser(membershipUser.UserName).FirstOrDefault();
                            var adminUserRoleModule = _adminDA.getUser_Role_ModuleList(role);
                            foreach (var umodule in adminUserRoleModule.Modules.OrderBy(m => m.PrarentID))
                            {
                                // lấy tất cả ModuleId trong Role_Module gán vào biến Active = 1
                                var listb = ltsSourceModule.Where(c => c.ID == umodule.ID).ToList();
                                if (listb.Count > 0)
                                    listb[0].Active = (int)PermisionType.InheritFromParent;

                                foreach (var module in listcap1)
                                {
                                    // Kiểm tra Module đang được trỏ có phải là cấp cha
                                    if (umodule.ID == module.ID)
                                    {
                                        module.Active = (int)PermisionType.InheritFromParent;
                                        // Lấy ra danh sách các cấp con của Module được trỏ
                                        var listPra =
                                            ltsSourceModule.OrderBy(o => o.Ord).Where(m => m.PrarentID == umodule.ID).
                                                ToList();

                                        foreach (var modu in listPra)
                                        {
                                            // Kiểm tra danh sách các cấp con của Module được trỏ
                                            modu.Active = (int)PermisionType.InheritFromParent;
                                        }
                                    }
                                    else
                                    {
                                        // Lấy ra Module đang được trỏ đến để kiểm tra cấp cha
                                        var listlayc = ltsSourceModule.FirstOrDefault(m => m.ID == umodule.ID);

                                        // Lấy ra Module cha đang được trỏ đến
                                        var lista =
                                            listcap1.Where(c => listlayc != null && c.ID == listlayc.PrarentID).ToList();
                                        if (lista.Count > 0)
                                        {
                                            lista[0].Active = (int)PermisionType.PermisionItself;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            #endregion
            var listtest = ltsSourceModule.Where(m => m.Active > 0).ToList();
            var listcapNew1 = listtest.OrderBy(o => o.Ord).Where(m => m.PrarentID == 1).ToList();
            ViewBag.html = "<ul class=\"nav\">" + GetLeftMenu(listtest) + "</ul>";
            return View(listtest);
        }

        public string GetLeftMenu(List<ModuleadminItem> lst)
        {
            string html = "";
            string url = "";
            foreach (var moduleadminItem in lst.Where(m => m.PrarentID == 1))
            {
                url = Url.Action("", moduleadminItem.Url);

                // Ke thua quyen tu cha
                if (moduleadminItem.Active == 1)
                {
                    if (lst.Count(m => m.PrarentID == moduleadminItem.ID) > 0)
                    {
                        html += "<li><a href=\"#" + moduleadminItem.NameModule + "\"><i class=\"fa " + moduleadminItem.ClassCss + " icon\"><b class=\"bg-success\"></b></i><span class=\"pull-right\"><i class=\"fa fa-angle-down text\"></i><i class=\"fa fa-angle-up text-active\"></i></span><span>" + moduleadminItem.NameModule + "</span></a>";
                        html += "<ul class=\"nav lt\">";
                        foreach (var item in lst.Where(m => m.PrarentID == moduleadminItem.ID).ToList())
                        {
                            url = Url.Action("", item.Url);
                            html += "<li><a href=\"" + url + "\"><i class=\"fa fa-angle-right\"></i><span>" + item.NameModule + "</span></a></li><li>";
                        }
                        html += "</ul>";
                    }
                    else
                    {
                        html += "<li><a href=\"" + url + "\"><i class=\"fa " + moduleadminItem.ClassCss + " icon\"><b class=\"bg-warning\"></b></i><span class=\"pull-right\"><i class=\"fa fa-angle-down text\"></i><i class=\"fa fa-angle-up text-active\"></i></span><span>" + moduleadminItem.NameModule + "</span></a>";
                    }
                    html += "</li>";
                }

                //lay menu usre role
                if (moduleadminItem.Active == 2)
                {
                    if (lst.Count(m => m.PrarentID == moduleadminItem.ID) > 0)
                    {
                        html += "<li><a href=\"#" + moduleadminItem.NameModule + "\"><i class=\"fa " + moduleadminItem.ClassCss + " icon\"><b class=\"bg-success\"></b></i><span class=\"pull-right\"><i class=\"fa fa-angle-down text\"></i><i class=\"fa fa-angle-up text-active\"></i></span><span>" + moduleadminItem.NameModule + "</span></a>";
                        html += "<ul class=\"nav lt\">";
                        foreach (var item in lst.Where(m => m.PrarentID == moduleadminItem.ID && m.Active == 1).ToList())
                        {
                            url = Url.Action("", item.Url);
                            html += "<li><a href=\"" + url + "\"><i class=\"fa fa-angle-right\"></i><span>" + item.NameModule + "</span></a></li><li>";
                        }
                        html += "</ul>";
                    }
                    else
                    {
                        html += "<li><a href=\"" + url + "\"><i class=\"fa " + moduleadminItem.ClassCss + " icon\"><b class=\"bg-warning\"></b></i><span class=\"pull-right\"><i class=\"fa fa-angle-down text\"></i><i class=\"fa fa-angle-up text-active\"></i></span><span>" + moduleadminItem.NameModule + "</span></a>";
                    }
                    html += "</li>";
                }
            }
            return html;
        }
    }
}
