using System.Web.Mvc;
using FDI.Base;
using FDI.DA.Admin;

namespace FDI.Web.Controllers
{
    public class HtmlSettingsController : Controller
    {
        readonly HtmlSettingDA _htmlSettingDa = new HtmlSettingDA("#");

        public ActionResult Index(string key, string url)
        {
            if (url==null)
            {
                var obj1 = _htmlSettingDa.GetByKey(key);
                return View(obj1);
            }
            var obj = _htmlSettingDa.GetByKeyAndUrl(key, url);
            return View(obj);
        }

        [ValidateInput(false)]
        public ActionResult Save()
        {
            var htmlSetting = new HtmlSetting();
            UpdateModel(htmlSetting);
            if (htmlSetting.url == null)
            {
                var obj1 = _htmlSettingDa.GetByKey(htmlSetting.Key);
                obj1.Value = htmlSetting.Value;
                _htmlSettingDa.Save();
                return Redirect("/");
            }
            var obj = _htmlSettingDa.GetByKeyAndUrl(htmlSetting.Key,htmlSetting.url);
            obj.Value = htmlSetting.Value;
            _htmlSettingDa.Save();
            return Redirect("/");
        }

        ////
        //// GET: /HtmlSetting/Details/5

        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        ////
        //// GET: /HtmlSetting/Create

        //public ActionResult Create()
        //{
        //    return View();
        //}

        ////
        //// POST: /HtmlSetting/Create

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
        //// GET: /HtmlSetting/Edit/5

        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /HtmlSetting/Edit/5

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
        //// GET: /HtmlSetting/Delete/5

        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        ////
        //// POST: /HtmlSetting/Delete/5

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
