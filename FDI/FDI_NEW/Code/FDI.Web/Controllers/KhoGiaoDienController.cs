using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FDI.Business.Implementation.Manager;
using FDI.Utils;

namespace FDI.Web.Controllers
{
	public class KhoGiaoDienController : Controller
    {
        //
        // GET: /KhoGiaoDien/
		private readonly GalleryPictureManager _galleryPictureManager = GalleryPictureManager.GetInstance();
        [HandleError]
        [WhitespaceFilter] // nén nội dung html
        public ActionResult Index()
        {
			var catName = ConfigurationManager.AppSettings["KhoGiaoDien"];

			var lst = _galleryPictureManager.GetByCateName(catName).OrderByDescending(m => m.DateCreated);
			
			var urlCurrent = Request.Url != null ? Request.Url.AbsolutePath : "";
			int currentPage = 1;
			int countPage = 8;

            int page = int.TryParse(Request["Page"], out page) ? page : 0;
            if (page > 0)
            {
                currentPage = page;
            }

			var htmlPage = new HtmlPager().getHtmlPageForum(urlCurrent + "?Page=", 3, currentPage, countPage, lst.Count(), "Focus");
			ViewBag.HtmlPage = htmlPage;
			return View(lst.Skip((currentPage - 1) * countPage).Take(countPage));
        }


    }
}
