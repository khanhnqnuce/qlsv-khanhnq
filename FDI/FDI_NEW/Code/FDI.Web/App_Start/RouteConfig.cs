using System.Web.Mvc;
using System.Web.Routing;

namespace FDI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("Home", "Home", new { controller = "Home", action = "Index", id = UrlParameter.Optional });
            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });
            routes.MapRoute("Ajax", "Ajax/{Controller}/{action}", new { controller = "Home", action = "Index" }, new[] { "FDI.Controllers" });

        }
    }
}