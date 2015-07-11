using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using FDI.Business.Implementation.Manager;
using FDI.Simple;
using FDI.Utils;

namespace FDI.Web.Controllers.News
{
    public class NewsController : Controller
    {
        readonly NewsManager _newItems = NewsManager.GetInstance();
        private readonly NewsCategoryManager _newsCategoryManager = NewsCategoryManager.GetInstance();
        private readonly NewsManager _newsManager = NewsManager.GetInstance();
        private readonly HtmlSettingManager _htmlSettingManager = HtmlSettingManager.GetInstance();
        private int _categoryNews = 2;

        public ActionResult Index(string name, int page = 1)
        {
            string seach = Request.QueryString["search"] ?? "";
            //    var lstCategory = (name != null || name != "") ? _newsCategoryManager.GetList().Where(m => m.NameAscii == name) : _newsCategoryManager.GetList().Where(m => m.ParentID == _categoryNews);
            var lstnew = new List<NewsItem>();
            var model = new ModelCategoryItemListNews();
            if (name != "")
            {
                var lst = _newsCategoryManager.GetNewsCategoryByNameAscii(name);
                if (lst != null)
                {
                    ViewBag.DanhMuc = lst.Name;
                    ViewBag.DanhMucAscii =  lst.NameAscii;
                    model.SeoDescription = lst.SEODescription ;
                    model.SeoTitle = lst.SEOTitle;
                    model.SeoKeywords = lst.SEOKeyword ;
                    lstnew = lst.ListNewsItem.ToList();
                }
            }

            else
            {
                name = "tin-tuc";
                var lst = _newsCategoryManager.GetList().Where(m => m.ParentID == _categoryNews || m.ID == _categoryNews);
                lstnew = lst.SelectMany(m => m.ListNewsItem).ToList();
                var lstcate = _newsCategoryManager.GetNewsCategoryByNameAscii(name);
                model.SeoDescription = lstcate.SEODescription ?? "";
                model.SeoTitle = lstcate.SEOTitle ?? "";
                model.SeoKeywords = lstcate.SEOKeyword ?? "";


            }

            var urlCurrent = Request.Url != null ? Request.Url.AbsolutePath : "";
            int currentPage = 1;
            const int countPage = 5;
            currentPage = page;
            var htmlPage = "";
            htmlPage = new HtmlPager().getHtmlPageForum("/" + name + "/", 3, currentPage, countPage, lstnew.Count(), "");

            model.SeoDescription = "";
            model.ListItem = lstnew.Skip((currentPage - 1) * countPage).Take(countPage);
            ViewBag.HtmlPage = htmlPage;
            return View(model);
        }

        [ChildActionOnly]
        [ActionOutputCache(600)] // Caches for 600 seconds
        public ActionResult NewsHot()
        {
            NewsCategoryItem parent = _newsCategoryManager.GetNewsCategoryByNameAscii("tin-tuc");
            var list = _newsCategoryManager.GetList().Where(s => s.ParentID == parent.ID || s.ID == parent.ID).ToList();

            //var list = _newsCategoryManager.GetList();
            var listNewsItem = list.SelectMany(newsCategoryItem => newsCategoryItem.ListNewsItem).Where(s => s.IsHot != true).OrderByDescending(m => m.DateCreated).Take(5).ToList();
            if (listNewsItem.Any())
            {
                return PartialView(listNewsItem);
            }
            return PartialView();
        }

        [ChildActionOnly]
        [ActionOutputCache(600)] // Caches for 600 seconds
        public ActionResult TinTamDiem()
        {
            var list = _newsCategoryManager.GetList();
            var listNewsItem = list.SelectMany(newsCategoryItem => newsCategoryItem.ListNewsItem).Where(s => s.IsHot == true).OrderByDescending(m => m.DateCreated).Take(3).ToList();
            if (listNewsItem.Any())
            {
                return PartialView(listNewsItem);
            }
            return PartialView();
        }

        public ActionResult NewReadMore(string name)
        {
            {
                if (name == "")
                    name = "tin-tuc";
                var listNewsItem = _newsManager.GetNewIsHostByCateAcsii(name);

                if (listNewsItem.Any())
                {
                    return PartialView(listNewsItem);
                }
                return PartialView();
            }
        }

        //public ActionResult DichVu()
        //{
        //    var DMDichVu = ConfigurationManager.AppSettings["DMDichVu"] ?? "";
        //    string name = DMDichVu;
        //    var lstCategory = _newsCategoryManager.GetList().Where(m => m.NameAscii.Equals(DMDichVu));
        //    var lstNews = lstCategory.SelectMany(item => item.ListNewsItem).OrderBy(m => m.DateCreated).ToList();
        //    var modelDichVuNews = new ModelNewsDichVuItem();
        //    if (lstNews.Any())
        //    {
        //        modelDichVuNews.TenDanhMuc = DMDichVu;
        //        modelDichVuNews.ListNewsItem = lstNews;
        //        modelDichVuNews.DanhMuc = lstCategory.FirstOrDefault().Name;
        //    }

        //    var BVMDDichVu = ConfigurationManager.AppSettings["BaiVietMacDinhDMDichVu"] ?? "";
        //    if (!string.IsNullOrEmpty(name))
        //    {
        //        BVMDDichVu = name;
        //    }
        //    if (!string.IsNullOrEmpty(BVMDDichVu))
        //    {
        //        var news = _newsManager.GetNewsByNameAscii(BVMDDichVu);

        //        if (news != null && ConvertUtil.ToInt32(news.ID) > 0)
        //        {
        //            modelDichVuNews.NewsItem = news;
        //        }
        //    }
        //    if (!string.IsNullOrEmpty(name))
        //        ViewBag.url = modelDichVuNews.TenDanhMuc + "/" + name;
        //   // return View("~/Views/News/Dichvu.cshtml", modelDichVuNews);
        //    return View(modelDichVuNews);
        //}
        public ActionResult Detail(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return View("Index");
            }
            var objNews = _newsManager.GetNewsByNameAscii(name);

            if (objNews != null)
            {

                //neu la danh muc dich vu
                if (objNews.CateAscii == WebConfig.DichVu)
                {
                    var DMDichVu = ConfigurationManager.AppSettings["DMDichVu"] ?? "";
                    var lstCategory = _newsCategoryManager.GetList().Where(m => m.NameAscii.Equals(DMDichVu));
                    var lstNews = lstCategory.SelectMany(item => item.ListNewsItem).OrderBy(m => m.DateCreated).ToList();
                    var modelDichVuNews = new ModelNewsDichVuItem();
                    if (lstNews.Any())
                    {
                        modelDichVuNews.TenDanhMuc = DMDichVu;
                        modelDichVuNews.ListNewsItem = lstNews;
                        modelDichVuNews.DanhMuc = lstCategory.FirstOrDefault().Name;
                    }
                    return View("~/Views/News/Dichvu.cshtml", modelDichVuNews);
                }
                //neu la con cua gioi thieu
                if (objNews.CateAscii == WebConfig.GioiThieu)
                {
                    var DMGioiThieu = ConfigurationManager.AppSettings["DMGioiThieu"] ?? string.Empty;
                    var lstCategory = _newsCategoryManager.GetList().Where(m => m.NameAscii.Equals(DMGioiThieu));
                    var lstNews = lstCategory.SelectMany(item => item.ListNewsItem).OrderBy(m => m.DateCreated);
                    var modelDichVuNews = new ModelNewsDichVuItem();
                    if (lstNews.Any())
                    {
                        modelDichVuNews.TenDanhMuc = DMGioiThieu;
                        modelDichVuNews.ListNewsItem = lstNews;
                        modelDichVuNews.DanhMuc = lstCategory.FirstOrDefault().Name;
                    }
                    var BVMDGioiThieu = ConfigurationManager.AppSettings["BaiVietMacDinhDMGioiThieu"];
                    if (!string.IsNullOrEmpty(name))
                    {
                        BVMDGioiThieu = name;
                    }
                    if (!string.IsNullOrEmpty(BVMDGioiThieu))
                    {
                        var news = _newsManager.GetNewsByNameAscii(BVMDGioiThieu);
                        if (news != null)
                        {
                            modelDichVuNews.NewsItem = news;
                        }
                    }
                    return View("~/Views/Home/About.cshtml", modelDichVuNews);
                }
                //new la con thiet ke phan mem
                if (objNews.CateAscii == WebConfig.designsoft || objNews.CateAscii == WebConfig.KienThucChiaSePhanMem)
                {
                    var cate = _newItems.GetNewsByNameAscii(name);
                    if (cate != null)
                    {
                        ViewBag.Keywords = cate.SeoKeyword;
                        ViewBag.Title = cate.SeoTitle;
                        ViewBag.Description = cate.SeoDescription;

                        return View("~/Views/Design/DesignSoftNewDetail.cshtml", cate);
                    }
                    else
                    {
                        return View("~/Views/Design/DesignSoftNewDetail.cshtml", new NewsItem());
                    }
                }

                //new la con cua thiet ke website
                if (objNews.CateAscii == WebConfig.KienThucChiaSeWebsite || objNews.CateAscii == WebConfig.designweb)
                {
                    var cate = _newItems.GetNewsByNameAscii(name);
                    if (cate != null)
                    {
                        var modelnew = new ModelNewsINewsRelated();
                        var tagM = new TagManager();
                        var newitem = _newItems.GetNewsByNameAscii(name);
                        ViewBag.Keywords = cate.SeoKeyword;
                        ViewBag.Title = cate.SeoTitle;
                        ViewBag.Description = cate.SeoDescription;
                        modelnew.Item = newitem;
                        //   modelnew.ListItems = _newItems.GetNewsRelated(newitem.NewTag);
                        return View("~/Views/Design/DesignWebNew.cshtml", modelnew);
                    }
                    else
                    {
                        return View("~/Views/Design/DesignWebNew.cshtml", new ModelNewsINewsRelated());
                    }
                }
                //new la con cua hosting
                if (objNews.CateAscii == WebConfig.KienThucChiaSeHosting || objNews.CateAscii == WebConfig.hosting)
                {
                    var cate = _newItems.GetNewsByNameAscii(name);
                    if (cate != null)
                    {

                        ViewBag.Keywords = cate.SeoKeyword;
                        ViewBag.Title = cate.SeoTitle;
                        ViewBag.Description = cate.SeoDescription;
                        return View("~/Views/Design/DesignHostingNew.cshtml", cate);
                    }
                    else
                    {
                        return View("~/Views/Design/DesignHostingNew.cshtml", new NewsItem());
                    }
                }
                //neu la co dich vu seo
                if (objNews.CateAscii == WebConfig.DichVuSeo)
                {
                    var cate = _newItems.GetNewsByNameAscii(name);
                    if (cate != null)
                    {
                        ViewBag.Keywords = cate.SeoKeyword;
                        ViewBag.Title = cate.SeoTitle;
                        ViewBag.Description = cate.SeoDescription;

                        return View("~/Views/Design/ChiTietDichVuSeo.cshtml", cate);
                    }
                    else
                    {
                        return View("~/Views/Design/ChiTietDichVuSeo.cshtml", new NewsItem());
                    }
                }
                return View(objNews);
            }
            return RedirectToAction("Index", "News");
        }

        public ActionResult Menu()
        {
            var lstMenuNews = _newsCategoryManager.GetList().Where(m => m.ParentID == _categoryNews);
            return PartialView(lstMenuNews);
        }

        public ActionResult Header(string name)
        {
            if (name != null)
            {
                var obj = _newsCategoryManager.GetNewsCategoryByNameAscii(name);
                if (obj != null)
                {
                    ViewBag.title = obj.Name;
                }
                else
                {
                    NewsItem objNews = _newsManager.GetNewsByNameAscii(name);
                    if (objNews != null)
                    {
                        ViewBag.title = objNews.CateName;
                    }
                }
            }
            return PartialView();
        }

        public ActionResult NewsHotCategory()
        {
            var lstCategory = _newsCategoryManager.GetList().Where(m => m.ParentID == _categoryNews);
            var ltrNews = lstCategory.SelectMany(item => item.ListNewsItem.Where(m => m.IsHot == true)).OrderByDescending(m => m.DateCreated).Take(3).ToList();
            return PartialView(ltrNews);
        }

        public PartialViewResult NewHostByCateAcsii(string cateAcsii)
        {
            var lst = _newsManager.GetNewIsHostByCateAcsii(cateAcsii);
            lst = lst.Take(5).ToList();
            return PartialView("~/Views/_news/newhost.cshtml", lst);
        }
        public ActionResult Tags()
        {
            return PartialView();
        }

        public ActionResult Share()
        {
            return PartialView();
        }

        public ActionResult TinNoiBat(string name)
        {
            if (name == "")
                name = "tin-tuc";
            var listNewsItem = _newsManager.GetNewIsHostByCateAcsii(name);

            if (listNewsItem.Any())
            {
                return PartialView(listNewsItem);
            }
            return PartialView();
        }

        //public ActionResult News_Top()
        //{
        //    var obj = _htmlSettingManager.GetList().SingleOrDefault(s => s.Key == "News_Top");
        //    obj.Ishow = User.Identity.IsAuthenticated;
        //    return PartialView(obj);
        //}

        #region Dịch Vụ


        public ActionResult DichVu(string name)
        {
            var DMDichVu = ConfigurationManager.AppSettings["DMDichVu"] ?? "";
            var lstCategory = _newsCategoryManager.GetList().Where(m => m.NameAscii.Equals(DMDichVu));
            var lstNews = lstCategory.SelectMany(item => item.ListNewsItem).OrderBy(m => m.DateCreated).ToList();
            var modelDichVuNews = new ModelNewsDichVuItem();
            if (lstNews.Any())
            {
                var lstCategorynew = _newsCategoryManager.GetNewsCategoryByNameAscii(DMDichVu);
                modelDichVuNews.Cate = lstCategorynew;
                modelDichVuNews.SeoDescription = lstCategorynew.SEODescription;
                modelDichVuNews.SeoTitle = lstCategorynew.SEOTitle;
                modelDichVuNews.SeoKeywords = lstCategorynew.SEOKeyword;
                modelDichVuNews.TenDanhMuc = DMDichVu;
                modelDichVuNews.ListNewsItem = lstNews;
                modelDichVuNews.DanhMuc = lstCategory.FirstOrDefault().Name;
            }

            //var BVMDDichVu = ConfigurationManager.AppSettings["BaiVietMacDinhDMDichVu"] ?? "";
            //if (!string.IsNullOrEmpty(name))
            //{
            //    BVMDDichVu = name;
            //}
            //if (!string.IsNullOrEmpty(BVMDDichVu))
            //{
            //    var news = _newsManager.GetNewsByNameAscii(BVMDDichVu);

            //    if (news != null && Utils.ConvertUtil.ToInt32(news.ID) > 0)
            //    {
            //        modelDichVuNews.NewsItem = news;
            //    }
            //}
            if (!string.IsNullOrEmpty(name))
                ViewBag.url = modelDichVuNews.TenDanhMuc + "/" + name;
            return PartialView(modelDichVuNews);
        }

        //public ActionResult Dichvu_slide()
        //{
        //    var obj = _htmlSettingManager.GetList().SingleOrDefault(s => s.Key == "dichvu_slides");
        //    return PartialView(obj);
        //}

        //public ActionResult ThongTinHoTro()
        //{
        //    var obj = _htmlSettingManager.GetList().SingleOrDefault(s => s.Key == "ThongTinHoTro");
        //    obj.Ishow = User.Identity.IsAuthenticated;
        //    return PartialView(obj);
        //}

        #endregion

        //phuocnh 21052015
        //search
        public ActionResult Search(string name, int page = 1)
        {
            @ViewBag.Title = name;
            name = MyString.Slug(name.Trim()).ToUpper();
            //    ltrNews = ltrNews.Where(m => m.TitleAscii.Contains(seach)).ToList();

            var lst = _newsManager.GetList();
            lst = lst.Where(m => m.TitleAscii.ToUpper().Contains(name)).ToList();
            int currentPage = 1;
            const int countPage = 5;
            //if (Request["page"] != null && ConvertUtil.ToInt32(Request["page"]) > 0)
            //{
            //    currentPage = int.Parse(Request["page"]);
            //}
            currentPage = page;
            var htmlPage = "";
            htmlPage = new HtmlPager().getHtmlPageForum("/Search/" + name + "/", 3, currentPage, countPage, lst.Count(), "");

            //}
            ViewBag.HtmlPage = htmlPage;
            var model = new ModelCategoryItemListNews();
            model.ListItem = lst.Skip((currentPage - 1) * countPage).Take(countPage);
            return View("~/Views/News/Index.cshtml", model);
        }



        //phuocnh 23052015
        //lay tin lien quan theo tag
        public ActionResult NewsRelated()
        {
            return View();
        }

    }
}
