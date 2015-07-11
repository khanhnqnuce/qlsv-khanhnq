using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FDI.Business.Implementation.Manager;
using FDI.Simple;

namespace FDI.Web.Controllers
{
    public class AdvController : Controller
    {
        //
        // GET: /Advertising/
        private readonly HtmlSettingManager _htmlSettingManager = HtmlSettingManager.GetInstance();
        //[HandleError]
        //[WhitespaceFilter] // nén nội dung html
        public ActionResult Index()
        {
            var cate = new NewsCategoryManager();
            var catemodle = cate.GetNewsCategoryByNameAscii("doi-tac");
            if(catemodle!=null)
            return View(catemodle);
            return View(new NewsCategoryItem());
        }

        public ActionResult Details(string key)
        {
            ViewBag.Key = key;
            return View();
        }

        //public ActionResult DoiTac()
        //{
        //    var obj = _htmlSettingManager.GetList().SingleOrDefault(s => s.Key == "doitac");
        //    return PartialView(obj);
        //}
        //public ActionResult DongHanh()
        //{
        //    var obj = _htmlSettingManager.GetList().SingleOrDefault(s => s.Key == "donghanh_FDISoft");
        //    return PartialView(obj);
        //}

        ////
        //// GET: /Advertising/Details/5

        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        ////
        //// GET: /Advertising/Create

        //public ActionResult Create()
        //{
        //    return View();
        //}

        ////
        //// POST: /Advertising/Create

        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        ////
        //// GET: /Advertising/Edit/5

        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /Advertising/Edit/5

        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        ////
        //// GET: /Advertising/Delete/5

        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /Advertising/Delete/5

        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
