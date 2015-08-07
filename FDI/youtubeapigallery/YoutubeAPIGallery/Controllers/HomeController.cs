using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YoutubeAPIGallery.Models;

namespace YoutubeAPIGallery.Controllers
{
	public class HomeController : Controller
	{
		// GET: Home
		public ActionResult Index()
		{
			ViewBag.Title = "VideoGallery";

			var videos = new List<Video>{
											new Video { Id = "jU5i1WjRBhE", Width = "640", Height = "390", Title="Ice Climbing Frozen Niagara Falls - Will Gadd's First Ascent" },
											new Video { Id = "XdlmoLAbbiQ", Width = "640", Height = "390", Title="This is the most amazing drone we've seen yet" },
											new Video { Id = "WUaQPUhVG9M", Width = "640", Height = "390", Title="Vine Compilation February 2015 Episode 27 - Best Vines - Funny Vines - New Vines - Vines February" }
										};

			return View(videos);
		}
	}
}