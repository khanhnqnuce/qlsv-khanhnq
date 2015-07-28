using System.Net;
using System.Web.Mvc;
using System.Xml.Linq;

namespace jQuery_Function.Controllers
{
    public class AjaxController : Controller
    {
        //
        // GET: /Ajax/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult File001()
        {
            var t = Request["t"];
            var id = Request["id"];
            var name = Request["name"];
            ViewBag.value = "id=" + id + " name =" + name + " t=" + t;
            return PartialView();
        }

        public ActionResult File001A(string title, string value, string mode)
        {
            var t = Request["t"];
            var modenew = Request["mode"];
            var titlenew = Request["title"];
            var valuenew = Request["value"];
            ViewBag.mode = t;
            return View();
        }
    }
}
