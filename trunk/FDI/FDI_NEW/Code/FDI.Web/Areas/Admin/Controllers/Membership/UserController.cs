using System;
using System.Linq;
using System.Web.Mvc;
using FDI.DA.Admin;
using FDI.Entities;
using FDI.Utils;

namespace FDI.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        //
        // GET: /Admin/User/

        private readonly UserDA _userDA;
        public UserController()
        {
            _userDA = new UserDA("#");
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListItems()
        {
            var listcustomer = _userDA.GetListSimpleByRequest(Request);
            //ViewData.Model = listcustomer;
            ViewBag.PageHtml = _userDA.GridHtmlPage;
            return View(listcustomer);
        }

        public ActionResult AjaxForm()
        {
            //ViewBag.Edit = systemActionItem.Edit;
            //ViewBag.Delete = systemActionItem.Delete;
            var userId = GuiID.FirstOrDefault();
            var user = _userDA.GetById(userId);
            var actionRole = _userDA.GetActiveRoleAll();
            ViewBag.ItemId = userId;
            ViewBag.ActiveRoles = actionRole;
            return View(user);
        }

        public ActionResult DeleteRoleAction()
        {
            JsonMessage msg;
            try
            {
                int moduleid = Convert.ToInt16(Request["moduleid"]);
                var userId = Guid.Parse(Request["ItemID"]);
                var role = _userDA.GetById(userId);
                var module = role.Modules.FirstOrDefault(m => m.ID == moduleid);
                if (module != null)
                {
                    var namemodule = module.NameModule;
                    role.Modules.Remove(module);
                    _userDA.Save();
                    var userModuleActive = _userDA.GetListUserModuleActivekt(userId, moduleid);
                    foreach (var moduleActive in userModuleActive)
                    {
                        _userDA.Delete(moduleActive);
                        _userDA.Save();
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
            int moduleid = Convert.ToInt16(Request["moduleid"]);
            var userId = Guid.Parse(Request["ItemID"]);

            var module = moduleid != 0
                            ? _userDA.GetListUserModuleActivekt(userId, moduleid)
                            : _userDA.GetListUserModuleActivekt(userId);
            if (module.Count > 0)
            {
                foreach (var user in module.Select(t => _userDA.GetByUserModuleActiveId(t.ID)))
                {
                    var check = Request[user.ID.ToString()];
                    user.Active = check != null;
                    _userDA.Save();
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

        [HttpPost]
        public ActionResult GetMessenger(string str)
        {
            var msg = new JsonMessage { Message = str, Erros = true };
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
    }
}
