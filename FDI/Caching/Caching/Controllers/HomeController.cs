using System.Collections.Generic;
using System.Web.Mvc;
using Caching.Common;
using Caching.Models;

namespace Caching.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public IVehicleRepository Repository { get; set; }

        public HomeController()
            : this(new VehicleRepository())
        {
        }

        public HomeController(IVehicleRepository repository)
        {
            this.Repository = repository;
        }

        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to my caching demo!";
            return View((IEnumerable<Vehicle>)Repository.GetVehicles());
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            Repository.ClearCache();
            return RedirectToAction("Index");
        }
    }
}
