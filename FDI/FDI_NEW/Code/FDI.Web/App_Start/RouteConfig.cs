using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FDI.Utils;

namespace FDI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Home", "Home", new { controller = "Home", action = "Index", id = UrlParameter.Optional });
            //tim kiem
            routes.MapRoute("search", "search/{name}", new { controller = "News", action = "Search" });
            routes.MapRoute("search2", "search/{name}/{page}", new { controller = "News", action = "Search", page = UrlParameter.Optional }, new { page = @"\d+" });
            routes.MapRoute("News2", "tin-tuc", new { controller = "News", action = "index", name = "" });
            routes.MapRoute("tin-tuc-page", "tin-tuc/{page}", new { controller = "News", action = "index", name = "", page = UrlParameter.Optional }, new { page = @"\d+" });
            routes.MapRoute("thiet-ke-website-tin", "thiet-ke-website/{name}", new { controller = "Design", action = "DesignWebList", id = UrlParameter.Optional });
            routes.MapRoute("thiet-ke-website", "thiet-ke-website", new { controller = "Design", action = "DesignWeb", id = UrlParameter.Optional });
            routes.MapRoute("thiet-ke-phan-mem-item", "thiet-ke-phan-mem/{name}", new { controller = "Design", action = "DesignSoft", id = UrlParameter.Optional });
            routes.MapRoute("thiet-ke-phan-mem", "thiet-ke-phan-mem", new { controller = "Design", action = "DesignSoft", id = UrlParameter.Optional });
            routes.MapRoute("cho-thue-hosting", "cho-thue-hosting", new { controller = "Design", action = "Hosting", id = UrlParameter.Optional });

            routes.MapRoute("cho-thue-hosting-detail", "cho-thue-hosting/{name}", new { controller = "Design", action = "ListHosting", id = UrlParameter.Optional });


            routes.MapRoute("Kien-thuc-chia-se-website", WebConfig.KienThucChiaSeWebsite, new { controller = "Design", action = "DesignWebList", id = UrlParameter.Optional });
            routes.MapRoute("Kien-thuc-chia-se-website-page", WebConfig.KienThucChiaSeWebsite + "/{page}", new { controller = "Design", action = "DesignWebList", page = UrlParameter.Optional });

            routes.MapRoute("Kien-thuc-chia-se-hosting", WebConfig.KienThucChiaSeHosting, new { controller = "Design", action = "ListHosting", id = UrlParameter.Optional });
            routes.MapRoute("Kien-thuc-chia-se-hosting-page", WebConfig.KienThucChiaSeHosting + "/{page}", new { controller = "Design", action = "ListHosting", page = UrlParameter.Optional });

            routes.MapRoute("Kien-thuc-chia-se-phan-mem", WebConfig.KienThucChiaSePhanMem, new { controller = "Design", action = "DesignSoftListNew", id = UrlParameter.Optional });
            routes.MapRoute("Kien-thuc-chia-se-phan-mem-page", WebConfig.KienThucChiaSePhanMem + "/{page}", new { controller = "Design", action = "DesignSoftListNew", page = UrlParameter.Optional });
            routes.MapRoute("Dich-vu", WebConfig.DichVu, new { controller = "News", action = "DichVu", id = UrlParameter.Optional });

            //routes.MapRoute("dich-vu-seo", WebConfig.DichVuSeo, new { controller = "Design", action = "DichVuSeo", id = UrlParameter.Optional });
            routes.MapRoute("dich-vu-seo", "{name}", new { controller = "Design", action = "DichVuSeo", id = UrlParameter.Optional }, new { name = @"" + WebConfig.DichVuSeo });
            routes.MapRoute("dich-vu-seo-page", "{name}/{page}", new { controller = "Design", action = "DichVuSeoList", page = UrlParameter.Optional }, new { name = @"" + WebConfig.DichVuSeo + "|" + WebConfig.KinhNghiemSeo });
            //  routes.MapRoute("dich-vu-seo-item", "dich-vu-seo/{name}", new { controller = "Design", action = "DichVuSeo", id = UrlParameter.Optional });

            routes.MapRoute("HTML", "HtmlSettings", new { controller = "HtmlSettings", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute("tag", "Tag/{name}", new { controller = "Tag", action = "List", id = UrlParameter.Optional });
            routes.MapRoute("tag-page", "Tag/{name}/{page}", new { controller = "Tag", action = "List", id = UrlParameter.Optional, page = UrlParameter.Optional });

            routes.MapRoute("About", "gioi-thieu/{name}", new { controller = "Home", action = "About", name = UrlParameter.Optional });

            // routes.MapRoute("Customer", "doi-tac/{name}", new { controller = "Adv", action = "list", id = UrlParameter.Optional });

            routes.MapRoute("Customer1", "doi-tac", new { controller = "Adv", action = "index", id = UrlParameter.Optional });

            routes.MapRoute("CustomerDetails", "doi-tac/{key}", new { controller = "Adv", action = "Details", id = UrlParameter.Optional });
            routes.MapRoute("NewsCategory3", "{name}", new { controller = "News", action = "index", id = UrlParameter.Optional }, new { name = @"thong-bao|tuyen-dung|su-kien|noi-bo|thiet-ke-website-chuyen-nghiep" });
            routes.MapRoute("NewsCategory2", "{name}/{page}", new { controller = "News", action = "index", id = UrlParameter.Optional }, new { name = @"thong-bao|tuyen-dung|su-kien|noi-bo|thiet-ke-website-chuyen-nghiep" });

            routes.MapRoute("NewsCategory", "tin-tuc/{name}", new { controller = "News", action = "index", id = UrlParameter.Optional });

            routes.MapRoute("Contact", "lien-he", new { controller = "Home", action = "Contact", id = UrlParameter.Optional });


            routes.MapRoute("KhoGiaoDien", "kho-giao-dien", new { controller = "KhoGiaoDien", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute("NewsDetail", "{name}", new { controller = "News", action = "Detail", id = UrlParameter.Optional });

            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute("Ajax", "Ajax/{Controller}/{action}", new { controller = "Home", action = "Index" }, new[] { "FDI.Controllers" });

        }
    }
}