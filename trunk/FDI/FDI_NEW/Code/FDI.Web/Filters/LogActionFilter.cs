using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using FDI.Base;
using FDI.DA;
using Newtonsoft.Json;

namespace FDI.Filters
{
    public class LogActionFilter : ActionFilterAttribute
    {
        readonly System_LogDA _systemLogDA = new System_LogDA("#");

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Log("OnActionExecuting", filterContext.RouteData);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //Log("Thuc thi", filterContext.RouteData);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            //Log("Thuc thi:", filterContext.RouteData);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            Log(filterContext.RouteData, filterContext);
        }


        private void Log(RouteData routeData, ResultExecutedContext context)
        {
            try
            {
                var controllerName = routeData.Values["controller"];
                var actionName = routeData.Values["action"];

                var output = context.Result is JsonResult ? JsonConvert.SerializeObject(context.Result) : string.Empty;

                if (Membership.GetUser() == null)
                {
                    HttpContext.Current.Response.Redirect(@"~/Admin/AccountAdmin/LogOn");
                }
                var membershipUser = Membership.GetUser();
                if (membershipUser != null)
                {
                    var providerUserKey = membershipUser.ProviderUserKey;
                    if (providerUserKey != null)
                        _systemLogDA.Add(new Shop_Log_System
                        {
                            ActionModule = controllerName.ToString(),
                            ActionType = GetActionType(context.HttpContext.Request["do"]),
                            ActionTypeName = context.HttpContext.Request["do"],
                            DateCreated = DateTime.Now,
                            UrlLink = HttpContext.Current.Request.Url.AbsolutePath,
                            ActionSubModule = actionName.ToString(),
                            UserName = HttpContext.Current.User.Identity.Name,
                            AddressIP = GetIPUser(),
                            UserID = (Guid)providerUserKey,
                            Input = GetInputValue(),
                            Output = output
                        });
                }
                _systemLogDA.Save();
            }
            catch (Exception ex)
            {
                //Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Elmah.Error(ex));
            }
        }

        private int GetActionType(string action)
        {
            var result = 0;
            var str = string.IsNullOrEmpty(action) ? "" : action.ToLower();
            switch (str)
            {
                case "view":
                    result = 0;
                    break;
                case "add":
                    result = 1;
                    break;
                case "edit":
                    result = 2;
                    break;
                case "delete":
                    result = 3;
                    break;
                case "show":
                    result = 4;
                    break;
                case "hide":
                    result = 5;
                    break;
                case "order":
                    result = 6;
                    break;
                case "active":
                    result = 7;
                    break;
                case "public":
                    result = 8;
                    break;
                case "complete":
                    result = 9;
                    break;
            }
            return result;
        }

        private string GetIPUser()
        {
            var currentRequest = HttpContext.Current.Request;
            var ipAddress = currentRequest.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (ipAddress == null || ipAddress.ToLower() == "unknown")
                ipAddress = currentRequest.ServerVariables["REMOTE_ADDR"];

            return ipAddress;
        }

        private string GetInputValue()
        {
            var result = HttpContext.Current.Request.QueryString.AllKeys.Aggregate("<table>", (current, item) => current + ("<tr><td style='width:150px'>" + item + "</td><td>" + HttpContext.Current.Request.QueryString[item] + "</td></tr>"));

            result = HttpContext.Current.Request.Params.AllKeys.Aggregate(result, (current, item) => current + ("<tr><td style='width:150px'>" + item + "</td><td>" + HttpContext.Current.Request.Params[item] + "</td></tr>"));

            result += "</table>";

            return result;
        }

    }
}