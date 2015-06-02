using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace jQuery_Function.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Serialize()
        {
            return View();
        }

        public ActionResult Attr()
        {
            return View();
        }
        public ActionResult On()
        {
            return View();
        }
        public ActionResult Datepicker()
        {
            return View();
        }

        public ActionResult Tooltip()
        {
            return View();
        }
        public ActionResult Scroll()
        {
            return View();
        }
        public ActionResult Ajaxcomplete()
        {
            return View();
        }
        public string Share()
        {
            Thread.Sleep(1000);
            return "<h1>Thành công</h1>";
        }

    }
}
