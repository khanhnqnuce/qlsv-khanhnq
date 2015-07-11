using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using FDI.DA;
using FDI.DA.Admin;
using FDI.Simple;
using FDI.Utils;

namespace FDI.Areas.Admin.Controllers
{
    public class GalleryVideoSelectController : BaseController
    {
        //
        // GET: /Admin/GalleryVideoSelect/

        readonly Gallery_VideoDA _videoDA = new Gallery_VideoDA("#");

        /// <summary>
        /// Hiển thị select ảnh
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var albumDA = new Gallery_AlbumDA("#") { Request = new ParramRequest(Request) };
            var ltsAllItems = albumDA.GetAllListSimple(true);
            var stbHtml = new StringBuilder();
            stbHtml.Append("<li title=\"Thư mục gốc\" class=\"select\" id=\"0\"><span class=\"file\">");
            stbHtml.Append("<a href=\"#" + albumDA.Request.GetCategoryString() + "0\">Thư mục gốc</a>");
            stbHtml.Append("</span></li>");

            var model = new ModelAlbumItem
            {
                SystemActionItem = systemActionItem,
                Container = Request["Container"],
                ListItem = ltsAllItems,
                StbHtml = stbHtml.ToString(),
                SelectMutil = Convert.ToBoolean(Request["SelectMutil"]),
                ValuesSelected = Request["ValuesSelected"]
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
            var ltsValues = MyBase.ConvertStringToListInt(Request["ValuesSelected"]);
            var listVideoItem = _videoDA.GetListSimpleByRequest(Request, ltsValues);

            var model = new ModelVideoItem
            {
                SystemActionItem = systemActionItem,
                Container = Request["Container"],
                ListItem = listVideoItem,
                PageHtml = _videoDA.GridHtmlPage
            };
            ViewData.Model = model;
            return View();
        }


    }
}
