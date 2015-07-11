using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FDI.Areas.Admin.Models;
using FDI.Entities;

namespace FDI.Areas.Admin.Controllers
{
    public class AccountAdminController : Controller
    {
        //
        // GET: /Admin/AccountAdmin/
        //
        // GET: /Account/LogOn

        [AllowAnonymous]
        public ActionResult LogOn()
        {
            var returnUrl = Request["url"];
            ViewBag.url = returnUrl;
            return ContextDependentView();
        }

        //
        // POST: /Account/JsonLogOn

        [AllowAnonymous]
        [HttpPost]
        public JsonResult JsonLogOn(LogOnModel model, string returnUrl)
        {
            //model.UserName = "Forum" + model.UserName;
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    return Json(new { success = true, redirect = returnUrl });
                }
                ModelState.AddModelError("", string.Format("The user {0}name or password provided is incorrect.", ""));
            }

            // If we got this far, something failed
            return Json(new { errors = GetErrorsFromModelState() });
        }
        private ActionResult ContextDependentView()
        {
            var actionName = ControllerContext.RouteData.GetRequiredString("action");
            if (Request.QueryString["content"] != null)
            {
                ViewBag.FormAction = "Json" + actionName;
                return PartialView();
            }
            ViewBag.FormAction = actionName;
            return View();
        }

        private IEnumerable<string> GetErrorsFromModelState()
        {
            return ModelState.SelectMany(x => x.Value.Errors
                .Select(error => error.ErrorMessage));
        }
        //
        // POST: /Account/LogOn
        //AdminAccountDA accoutAdmin = new AdminAccountDA("#");

        [AllowAnonymous]
        [HttpPost]
        public ActionResult LogOn(LogOnModel model)
        {
            //model.UserName = "Forum" + model.UserName;
            var returnUrl = Request["itemUrl"];
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    var user = Membership.GetUser(model.UserName);
                    if (user != null && user.IsLockedOut)
                    {
                        ModelState.AddModelError("", string.Format("{0}Your accout is locked. Please contact Administrator!", ""));
                    }
                    else
                    {
                        //Gọi stored báo login thành công
                        FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                        return Redirect(Url.IsLocalUrl(returnUrl) ? returnUrl : "~/Admin/Admin");
                    }
                }
                else
                {
                    ModelState.AddModelError("", string.Format("{0}The user name or password provided is incorrect.", ""));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Admin");
        }
        //
        // GET: /Account/ChangePassword

        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword
        [HttpPost]
        public ActionResult ActionChangePassword(ChangePasswordModel model)
        {
            var msg = new JsonMessage();
            
            // ChangePassword will throw an exception rather
            // than return false in certain failure scenarios.
            var changePasswordSucceeded = false;
            try
            {
                var currentUser = Membership.GetUser(User.Identity.Name, userIsOnline: true);
                if (currentUser != null)
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
            }
            catch (Exception)
            {
                changePasswordSucceeded = false;
            }

            if (changePasswordSucceeded)
            {
                msg = new JsonMessage
                {
                    Erros = false,
                    Message = "Đổi mật khẩu thành công !"
                };
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            //ModelState.AddModelError("", string.Format("{0}Sai mật khẩu hoặc mật khẩu mới không đúng định dạng. Vui lòng kiểm tra lại!.", ""));
            msg = new JsonMessage
            {
                Erros = true,
                Message = "Sai mật khẩu hoặc mật khẩu mới không đúng định dạng.</br> Vui lòng kiểm tra lại!"
            };
            return Json(msg, JsonRequestBehavior.AllowGet); 
        }

        //
        // GET: /Account/ChangePasswordSuccess
        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        //#region Status Codes
        //private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        //{
        //    // See http://go.microsoft.com/fwlink/?LinkID=177550 for
        //    // a full list of status codes.
        //    switch (createStatus)
        //    {
        //        case MembershipCreateStatus.DuplicateUserName:
        //            return "User name already exists. Please enter a different user name.";

        //        case MembershipCreateStatus.DuplicateEmail:
        //            return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

        //        case MembershipCreateStatus.InvalidPassword:
        //            return "The password provided is invalid. Please enter a valid password value.";

        //        case MembershipCreateStatus.InvalidEmail:
        //            return "The e-mail address provided is invalid. Please check the value and try again.";

        //        case MembershipCreateStatus.InvalidAnswer:
        //            return "The password retrieval answer provided is invalid. Please check the value and try again.";

        //        case MembershipCreateStatus.InvalidQuestion:
        //            return "The password retrieval question provided is invalid. Please check the value and try again.";

        //        case MembershipCreateStatus.InvalidUserName:
        //            return "The user name provided is invalid. Please check the value and try again.";

        //        case MembershipCreateStatus.ProviderError:
        //            return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

        //        case MembershipCreateStatus.UserRejected:
        //            return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

        //        default:
        //            return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
        //    }
        //}
        //#endregion

    }
}
