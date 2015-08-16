using System.Web.Mvc;

namespace TaskList.Controllers
{
    public class TaskListController : Controller
    {
        //
        // GET: /TaskList/

        public ActionResult Index()
        {
            return View();
        }

    }
}
