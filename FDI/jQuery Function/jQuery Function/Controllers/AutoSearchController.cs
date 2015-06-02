using System.Collections.Generic;
using System.Web.Mvc;
using jQuery_Function.Models;

namespace jQuery_Function.Controllers
{
    public class AutoSearchController : Controller
    {
        //
        // GET: /AutoSearch/

        public ActionResult Index()
        {
            const string a = "Thiết kết web";
            var b = a.GenerateUrl();
            return PartialView();
        }
        public ActionResult GetSource()
        {
            var stuffs = new List<Stuff>()
            {
                new Stuff{Name = "Computer", Price = 100000},
                new Stuff{Name = "Ability", Price = 100000},
                new Stuff{Name = "Pavement", Price = 100000},
                new Stuff{Name = "Short of Money", Price = 100000},
                new Stuff{Name = "Dead", Price = 100000}
            };
            return Json(stuffs,JsonRequestBehavior.AllowGet);
        }

    }
    public class Stuff
    {
        public string Name { get; set; }
        public double Price { get; set; }

    }
}
