using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FDI.Areas.Admin.Models.UserAdministration;
using FDI.DA.Admin;
using FDI.Entities;
using FDI.Utils;
using FDI.MvcMembership;
using FDI.MvcMembership.Settings;

namespace FDI.Areas.Admin.Controllers
{
    public class UserAdministrationController : BaseController
    {
        //
        // GET: /Admin/UserAdministration/        

        private const int PageSize = 100;
        private const string ResetPasswordBody = "Your new password is: ";
        private const string ResetPasswordSubject = "Your New Password";
        private readonly IRolesService _rolesService;
        private readonly IMembershipSettings _membershipSettings;
        private readonly IUserService _userService;
        private readonly IPasswordService _passwordService;
        public UserAdministrationController()
            : this(new AspNetMembershipProviderWrapper(), new AspNetRoleProviderWrapper())
        {
        }

        public UserAdministrationController(AspNetMembershipProviderWrapper membership, IRolesService roles)
            : this(membership.Settings, membership, membership, roles)
        {
        }

        public UserAdministrationController(
            IMembershipSettings membershipSettings,
            IUserService userService,
            IPasswordService passwordService,
            IRolesService rolesService)
        {
            _membershipSettings = membershipSettings;
            _userService = userService;
            _passwordService = passwordService;
            _rolesService = rolesService;
        }

        public ActionResult Roles(int? page, string search)
        {
            var users = string.IsNullOrWhiteSpace(search)
                ? _userService.FindAll(page ?? 1, PageSize)
                : search.Contains("@")
                    ? _userService.FindByEmail(search, page ?? 1, PageSize)
                    : _userService.FindByUserName(search, page ?? 1, PageSize);

            if (!string.IsNullOrWhiteSpace(search) && users.Count == 1)
            {
                var providerUserKey = users[0].ProviderUserKey;
                if (providerUserKey != null)
                    return RedirectToAction("Details", new { id = providerUserKey.ToString() });
            }

            return View(new IndexViewModel
            {
                Search = search,
                Users = users,
                Roles = _rolesService.Enabled
                    ? _rolesService.FindAll()
                    : Enumerable.Empty<string>(),
                IsRolesEnabled = _rolesService.Enabled
            });
        }

        public ActionResult Index(int? page, string search)
        {
            ViewBag.Add = systemActionItem.Add;
            ViewBag.Viewfull = systemActionItem.ViewFull;
            ViewBag.Edit = systemActionItem.Edit;

            var users = string.IsNullOrWhiteSpace(search)
                ? _userService.FindAll(page ?? 1, PageSize)
                : search.Contains("@")
                    ? _userService.FindByEmail(search, page ?? 1, PageSize)
                    : _userService.FindByUserName(search, page ?? 1, PageSize);

            if (!string.IsNullOrWhiteSpace(search) && users.Count == 1)
            {
                var providerUserKey = users[0].ProviderUserKey;
                if (providerUserKey != null)
                    return RedirectToAction("Details", new { id = providerUserKey.ToString() });
            }

            return View(new IndexViewModel
            {
                Search = search,
                Users = users,
                Roles = _rolesService.Enabled
                    ? _rolesService.FindAll()
                    : Enumerable.Empty<string>(),
                IsRolesEnabled = _rolesService.Enabled
            });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public RedirectToRouteResult CreateRole(string id)
        {
            if (_rolesService.Enabled)
                _rolesService.Create(id);
            return RedirectToAction("Index");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public RedirectToRouteResult DeleteRole(string id)
        {
            _rolesService.Delete(id);
            return RedirectToAction("Index");
        }

        public ViewResult Role(string id)
        {
            return View(new RoleViewModel
            {
                Role = id,
                Users = _rolesService.FindUserNamesByRole(id)
                                     .ToDictionary(
                                        k => k,
                                        v => _userService.Get(v)
                                     )
            });
        }

        public ViewResult Details(Guid id)
        {
            ViewBag.Edit = systemActionItem.Edit;
            ViewBag.Delete = systemActionItem.Delete;
            ViewBag.Hide = systemActionItem.Hide;

            var user = _userService.Get(id);
            var userRoles = _rolesService.Enabled
                ? _rolesService.FindByUser(user)
                : Enumerable.Empty<string>();
            ViewBag.IsLockedOut = user.IsLockedOut;
            var dictionary = _rolesService.FindAll().ToDictionary(s => s, userRoles.Contains);
            return View(new DetailsViewModel
            {
                CanResetPassword = _membershipSettings.Password.ResetOrRetrieval.CanReset,
                RequirePasswordQuestionAnswerToResetPassword = _membershipSettings.Password.ResetOrRetrieval.RequiresQuestionAndAnswer,
                DisplayName = user.UserName,
                User = user,
                Roles = _rolesService.Enabled
                    ? dictionary
                    : new Dictionary<string, bool>(0),
                IsRolesEnabled = _rolesService.Enabled,
                Status = user.IsOnline
                            ? DetailsViewModel.StatusEnum.Online
                            : !user.IsApproved
                                ? DetailsViewModel.StatusEnum.Unapproved
                                : user.IsLockedOut
                                    ? DetailsViewModel.StatusEnum.LockedOut
                                    : DetailsViewModel.StatusEnum.Offline
            });
        }

        public ViewResult Password(Guid id)
        {
            var user = _userService.Get(id);
            var userRoles = _rolesService.Enabled ? _rolesService.FindByUser(user) : Enumerable.Empty<string>();
            return View(new DetailsViewModel
            {
                CanResetPassword = _membershipSettings.Password.ResetOrRetrieval.CanReset,
                RequirePasswordQuestionAnswerToResetPassword = _membershipSettings.Password.ResetOrRetrieval.RequiresQuestionAndAnswer,
                DisplayName = user.UserName,
                User = user,
                Roles = _rolesService.Enabled ? _rolesService.FindAll().ToDictionary(role => role, userRoles.Contains) : new Dictionary<string, bool>(0),
                IsRolesEnabled = _rolesService.Enabled,
                Status = user.IsOnline
                            ? DetailsViewModel.StatusEnum.Online
                            : !user.IsApproved
                                ? DetailsViewModel.StatusEnum.Unapproved
                                : user.IsLockedOut
                                    ? DetailsViewModel.StatusEnum.LockedOut
                                    : DetailsViewModel.StatusEnum.Offline
            });
        }

        public ViewResult UsersRoles(Guid id)
        {
            var user = _userService.Get(id);
            var userRoles = _rolesService.FindByUser(user);
            return View(new DetailsViewModel
            {
                CanResetPassword = _membershipSettings.Password.ResetOrRetrieval.CanReset,
                RequirePasswordQuestionAnswerToResetPassword = _membershipSettings.Password.ResetOrRetrieval.RequiresQuestionAndAnswer,
                DisplayName = user.UserName,
                User = user,
                Roles = _rolesService.FindAll().ToDictionary(role => role, userRoles.Contains),
                IsRolesEnabled = true,
                Status = user.IsOnline
                            ? DetailsViewModel.StatusEnum.Online
                            : !user.IsApproved
                                ? DetailsViewModel.StatusEnum.Unapproved
                                : user.IsLockedOut
                                    ? DetailsViewModel.StatusEnum.LockedOut
                                    : DetailsViewModel.StatusEnum.Offline
            });
        }

        public ViewResult CreateUser()
        {
            var model = new CreateUserViewModel
            {
                InitialRoles = _rolesService.FindAll().ToDictionary(k => k, v => false)
            };
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateUser(CreateUserViewModel createUserViewModel)
        {
            if (!systemActionItem.Add)
            {
                var msg = new JsonMessage
                {
                    Erros = true,
                    Message = "Bạn chưa được phân quyền cho chức năng này!"
                };

                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            if (!ModelState.IsValid)
                return View(createUserViewModel);

            try
            {
                if (createUserViewModel.Password != createUserViewModel.ConfirmPassword)
                    throw new MembershipCreateUserException("Passwords do not match.");

                var user = _userService.Create(
                    createUserViewModel.Username,
                    createUserViewModel.Password,
                    createUserViewModel.Email,
                    createUserViewModel.PasswordQuestion,
                    createUserViewModel.PasswordAnswer,
                    true);

                if (createUserViewModel.InitialRoles != null)
                {
                    var rolesToAddUserTo = createUserViewModel.InitialRoles.Where(x => x.Value).Select(x => x.Key);
                    foreach (var usersInRoles in rolesToAddUserTo)
                    {
                        _rolesService.AddToRole(user, usersInRoles);
                    }

                }

                return RedirectToAction("Details", new { id = user.ProviderUserKey });
            }
            catch (MembershipCreateUserException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(createUserViewModel);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public RedirectToRouteResult Details(Guid id, string email, string comments)
        {
            var user = _userService.Get(id);
            user.Email = email;
            user.Comment = comments;
            _userService.Update(user);
            return RedirectToAction("Details", new { id });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public RedirectToRouteResult DeleteUser(Guid id)
        {
            if (!systemActionItem.Delete)
            {
                return RedirectToAction("Index");
            }

            var user = _userService.Get(id);
            _userService.Delete(user);
            return RedirectToAction("Index");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public RedirectToRouteResult ChangeApproval(Guid id, bool isApproved)
        {
            if (!systemActionItem.Delete)
            {
                return RedirectToAction("Details");
            }
            var user = _userService.Get(id);
            user.IsApproved = isApproved;
            _userService.Update(user);
            return RedirectToAction("Details", new { id });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public RedirectToRouteResult Unlock(Guid id)
        {
            if (!systemActionItem.Delete)
            {
                return RedirectToAction("Details");
            }
            var rolerDA = new RoleDA("#");
            var membership = rolerDA.GetListByMembership(id);
            membership.IsLockedOut = membership.IsLockedOut != true;
            rolerDA.Save();
            return RedirectToAction("Details", new { id });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public RedirectToRouteResult ResetPassword(Guid id)
        {
            if (!systemActionItem.Edit)
            {
                return RedirectToAction("Password");
            }
            var user = _userService.Get(id);
            var pass = MyBase.RandomString(6).ToLower();
            _passwordService.ChangePassword(user, pass);
            return RedirectToAction("Password", new { id });
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public RedirectToRouteResult ResetPasswordWithAnswer(Guid id, string answer)
        {
            var user = _userService.Get(id);
            _passwordService.ResetPassword(user, answer);
            return RedirectToAction("Password", new { id });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public RedirectToRouteResult SetPassword(Guid id, string password)
        {
            var user = _userService.Get(id);
            _passwordService.ChangePassword(user, password);

            var body = ResetPasswordBody + password;
            var msg = new MailMessage();
            msg.To.Add(user.Email);
            msg.Subject = ResetPasswordSubject;
            msg.Body = body;
            return RedirectToAction("Password", new { id });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public RedirectToRouteResult AddToRole(Guid id, string role)
        {
            var user = _userService.Get(id);
            _rolesService.AddToRole(user, role);

            return RedirectToAction("UsersRoles", new { id });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public RedirectToRouteResult RemoveFromRole(Guid id, string role)
        {
            var user = _userService.Get(id);
            _rolesService.RemoveFromRole(user, role);
            return RedirectToAction("UsersRoles", new { id });
        }

    }
}
