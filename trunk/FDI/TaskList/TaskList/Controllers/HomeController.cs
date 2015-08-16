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
            var request1 = Request["array1"];
            var request2 = Request["array2"];
            return request1 + "\n" + request2;
        }

    }
}
