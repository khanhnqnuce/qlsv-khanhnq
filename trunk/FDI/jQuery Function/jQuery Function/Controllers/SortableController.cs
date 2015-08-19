using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jQuery_Function.Controllers
{
    public class SortableController : Controller
    {
        //
        // GET: /Sortable/

        public ActionResult Index()
        {
            return View();
        }
        
        public String demo()
        {
            var c = Request["positions"].Split(',');
            return null;
        }

    }
}
