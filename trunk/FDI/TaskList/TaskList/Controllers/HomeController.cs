using System.Web.Mvc;

namespace TaskList.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public string Sortable()
        {
            var request = Request["usename"];
            return request;
        }

    }
}
