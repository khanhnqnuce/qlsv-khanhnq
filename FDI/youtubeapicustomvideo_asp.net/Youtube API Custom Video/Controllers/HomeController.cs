using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Youtube_API_Custom_Video.Models;

namespace Youtube_API_Custom_Video.Controllers
{
	public class HomeController : Controller
	{
		// GET: Home
		public ActionResult Index()
		{
			var video = new Video { Id = "jU5i1WjRBhE", Width = "640", Height = "390", Title = "Ice Climbing Frozen Niagara Falls - Will Gadd's First Ascent" };

			return View(video);
		}
	}
}