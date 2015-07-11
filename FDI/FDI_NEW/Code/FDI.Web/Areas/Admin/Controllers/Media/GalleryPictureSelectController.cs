using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using FDI.DA.Admin;
using FDI.Simple;
using FDI.Utils;

namespace FDI.Areas.Admin.Controllers
{
    public class GalleryPictureSelectController : Controller
    {
        //
        // GET: /Admin/GalleryPictureSelect/

        readonly Gallery_PictureDA _pictureDA = new Gallery_PictureDA("#");
        readonly Gallery_AlbumDA _albumDA = new Gallery_AlbumDA("#");
        readonly Gallery_CategoryDA _categoryDA = new Gallery_CategoryDA("#");

        /// <summary>
        /// Hiển thị select ảnh
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //var listAlbum = _albumDA.GetAllListSimple(true);
            var stbHtml = new StringBuilder();
            var ltsSourceCategory = _categoryDA.GetAllListSimple();
            _categoryDA.BuildTreeView(ltsSourceCategory, 1, false, ref stbHtml, false, false, false, false, false, false, false);
            var model = new ModelAlbumItem
            {
                //SystemActionItem = systemActionItem,
                Container = Request["Container"],
                //ListItem = listAlbum,
                ValuesSelected = Request["ValuesSelected"],
                StbHtml = stbHtml.ToString(),
                //PageHtml = _pictureDA.GridHtmlPage,
                SelectMutil = Convert.ToBoolean(Request["MutilFile"])
            };
            ViewData.Model = model;
            return View();
        }


        /// <summary>
        /// Load danh sách bản ghi dưới dạng bảng
        /// </summary>
        /// <returns></returns>
        public ActionResult ListItems()
        {
            var listFAQQuestionItem = _pictureDA.GetListSimpleByRequest(Request);
            var model = new ModelPictureItem
            {
                //SystemActionItem = systemActionItem,
                Container = Request["Container"],
                ListItem = listFAQQuestionItem,
                PageHtml = _pictureDA.GridHtmlPage
            };
            ViewData.Model = model;
            return View();
        }

    }
}
