using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using FDI.Business.Implementation.Manager;
using FDI.Simple;
using FDI.Utils;

namespace FDI.Web.Views.Home
{
    public class DesignController : Controller
    {
        private int _categoryNews = 2;
        readonly NewsManager _newItems = NewsManager.GetInstance();
        private SystemMenuManager _menuManager = SystemMenuManager.GetInstance();
        //
        // GET: /Design/
        private readonly NewsCategoryManager _newsCategoryManager = NewsCategoryManager.GetInstance();

        public ActionResult DesignSoft(string name)
        {
            var list = _newsCategoryManager.GetNewsCategoryByNameAscii(WebConfig.designsoft);
            ViewBag.Name = name ?? "thiet-ke-phan-mem-theo-yeu-cau";
            return View(list);
        }

        public ActionResult DesignSoftListNew(string name, int page = 1)
        {
            var cate = _newsCategoryManager.GetNewsCategoryByNameAscii(WebConfig.KienThucChiaSePhanMem);
            ViewBag.CatName = cate.Name;
            ViewBag.CatNameAscii = cate.NameAscii;
            ViewBag.CatTitle = cate.SEOTitle;
            ViewBag.CatDescription = cate.SEODescription;
            ViewBag.Keyword = cate.SEOKeyword;
            var currentPage = page == null ? 1 : Convert.ToInt32(page);
            var lstitem = cate.ListNewsItem.OrderByDescending(m => m.DateCreated).ToList();
            var lst = _newItems.NewsPage(currentPage, 6, "", lstitem);
            ViewBag.HtmlPage = new HtmlPager().getHtmlPageForum("/" + WebConfig.KienThucChiaSePhanMem + "/", 3, currentPage, 6, lstitem.Count);
            return View(lst);
        }
        public ActionResult DesignSoftNewDetail(string name)
        {
            var cate = _newItems.GetNewsByNameAscii(name);
            if (cate != null)
                return View(cate);
            else
            {
                return View(new NewsItem());
            }
        }

        public ActionResult DichVuSeo(string name, int page = 1)
        {
            var lst = _newsCategoryManager.GetNewsCategoryByNameAscii(name);
            return View(lst);

        }
        public ActionResult DichVuSeoList(string name, int page = 1)
        {
            var lst = _newsCategoryManager.GetNewsCategoryByNameAscii(name);
            if (lst != null && lst.ListNewsItem.Any())
            {
                ViewBag.CatName = lst.Name;
                ViewBag.CatNameAscii = lst.NameAscii;
                ViewBag.CatTitle = lst.SEOTitle;
                ViewBag.CatDescription = lst.SEODescription;
                ViewBag.CatKeyword = lst.SEOKeyword;
                var currentPage = page;
                var lstitem = _newItems.NewsPage(currentPage, 6, "", lst.ListNewsItem.ToList());
                ViewBag.HtmlPage = new HtmlPager().getHtmlPageForum("/" + name + "/", 3, currentPage, 6, lst.ListNewsItem.Count());
                return View(lstitem);
            }
            return View(new List<NewsItem>());
        }
        
        public ActionResult DichVuSeoLeftMenu()
        {
            var lst = _menuManager.GetAllChildByMenuId(19);
            return View(lst);
        }

        public ActionResult ChiTietDichVuSeo(string name)
        {
            return View();
        }
        
        public ActionResult DesignWeb()
        {
            var list = _newsCategoryManager.GetNewsCategoryByNameAscii(WebConfig.designweb);

            return View(list);
            return View();
        }

        public ActionResult DesignWebList(string name, int page = 1)
        {
            name = WebConfig.KienThucChiaSeWebsite;
            var lst = _newsCategoryManager.GetNewsCategoryByNameAscii(name);
            var modelnew = new ModelNewsINewsRelated();
            if (lst != null && lst.ListNewsItem.Any())
            {
                ViewBag.CatName = lst.Name;
                ViewBag.CatNameAscii = lst.NameAscii;
                ViewBag.CatTitle = lst.SEOTitle;
                ViewBag.CatDescription = lst.SEODescription;
                ViewBag.CatKeyword = lst.SEOKeyword;
                var currentPage = page == null ? 1 : Convert.ToInt32(page);
                var lstitem = _newItems.NewsPage(currentPage, 6, "", lst.ListNewsItem.OrderByDescending(m => m.DateCreated).ToList());
                ViewBag.HtmlPage = new HtmlPager().getHtmlPageForum("/" + WebConfig.KienThucChiaSeWebsite + "/", 3, currentPage, 6, lst.ListNewsItem.Count());
                return View(lstitem);
            }
            //neu la tin chi tiet
            else
            {
                var tagM = new TagManager();
                var newitem = _newItems.GetNewsByNameAscii(name);
                modelnew.Item = newitem;
                //    modelnew.ListItems = _newItems.GetNewsRelated(newitem.NewTag);
                return View("~/Views/Design/DesignWebNew.cshtml", modelnew);

            }

            return View();
        }
        public ActionResult DesignHostingNew()
        {
            return View();
        }

        public ActionResult DesignWebNew()
        {

            return View();
        }

        public ActionResult Hosting()
        {
            var cate = _newsCategoryManager.GetNewsCategoryByNameAscii(WebConfig.hosting);
            return View(cate);
        }

        public ActionResult ListNewSoft()
        {
            var cate = _newsCategoryManager.GetNewsCategoryByNameAscii(WebConfig.designsoft);

            return View(cate);
        }

        public ActionResult ListHosting(string name, int page = 1)
        {
            name = WebConfig.KienThucChiaSeHosting;
            var lst = _newsCategoryManager.GetNewsCategoryByNameAscii(name);

            ViewBag.CatName = lst.Name;
            ViewBag.CatNameAscii = lst.NameAscii;
            ViewBag.CatTitle = lst.SEOTitle;
            ViewBag.CatDescription = lst.SEODescription;
            ViewBag.CatKeyword = lst.SEOKeyword;
            var currentPage = page;
            var lstitem = _newItems.NewsPage(currentPage, 6, "", lst.ListNewsItem.ToList());
            ViewBag.HtmlPage = new HtmlPager().getHtmlPageForum("/" + WebConfig.KienThucChiaSeHosting + "/", 3, currentPage, 6, lst.ListNewsItem.Count());
            return View(lstitem);

        }
    }
}
