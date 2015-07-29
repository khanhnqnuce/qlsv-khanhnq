using System.Net;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml.Linq;
using jQuery_Function.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

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

        [HttpPost]
        public ActionResult File001A()
        {
            var t = Request["t"];
            var username = Request["UserName"];
            var password = Request["Password"];
            ViewBag.value = "username: " + username + ", password: " + password + ", t:" + t;
            return PartialView();
        }
        public string FileJson()
        {
            var obj = new Student();
            for (var i = 0; i < 3; i++)
            {
                obj.ID = i;
                obj.FirtName = "FirtName" + i;
                obj.LastName = "LastName" + i;
            }
            var js = new JavaScriptSerializer();
            var json = js.Serialize(obj);
            return json;
        }
    }
}
