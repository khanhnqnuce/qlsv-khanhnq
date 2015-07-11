using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FDI.Business.Implementation.Manager;
using FDI.DA.Admin;
using FDI.Simple;
using FDI.Utils;

namespace FDI.Web.Controllers
{
    public class TagController : Controller
    {
        System_TagDA tagDA=new System_TagDA();
        TagManager _tagManager=new TagManager();
        //
        // GET: /Tag/

        public ActionResult Index()
        {
            var lst = _tagManager.GetAllHomeListSimple();
            return View(lst);
        }

        //lay tin tag cua bai viet
        public ActionResult ListByNewAssi()
        {
            string name = Request.Url.AbsoluteUri;
           string[] strlst = name.Split('/');
           var lst = _tagManager.GetAllListSimpleByUrl(strlst.LastOrDefault());
           return View(lst);
        }



        public ActionResult List(string name, int page=1)
        {
            var item = _tagManager.GetByNamAcssi(name);
            if (item != null)
            {
                var model = new ModelTagItemListNewItem();
                model.tagitem = item;
                model.ListItem = item.Newitems.Skip((page - 1) * 6).Take(6).ToList();
                model.HtmlPage = new HtmlPager().getHtmlPageForum("/Tag/" + name + "/", 3, page, 6, item.Newitems.Count());
                return View(model);
            }
            
            return View(new ModelTagItemListNewItem());
        }


        //lay tin lien quan
        public ActionResult ListNewByNewAssi(string nameassi)
        {
            var lst = _tagManager.GetByNamAssi(nameassi);
            var lstNews = lst.SelectMany(m => m.Newitems).ToList();
           // lstNews=
            var lstNews2 = lstNews.Where(m=>m.TitleAscii!=nameassi).GroupBy(m => m.TitleAscii).Select(m => m.FirstOrDefault()).Take(5).ToList();
            return View(lstNews2);
        }
    }
}
