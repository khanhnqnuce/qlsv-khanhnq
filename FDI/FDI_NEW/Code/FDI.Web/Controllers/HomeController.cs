using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using FDI.Business.Implementation.Manager;
using FDI.Entities;
using FDI.Simple;
using FDI.Utils;
using FDI.DA.Admin;
using FDI.Base;
using System.Net.Mail;
using System.Net;
using System.Text; 

namespace FDI.Controllers
{
    public class HomeController : Controller
    {
        private readonly NewsCategoryManager _newsCategoryManager = NewsCategoryManager.GetInstance();
        private readonly SystemMenuManager _systemMenuManager = SystemMenuManager.GetInstance();
        private readonly HtmlSettingManager _htmlSettingManager = HtmlSettingManager.GetInstance();
        private readonly HtmlSettingDA _htmlSettingManagerDa = new HtmlSettingDA(); 
        private readonly NewsManager _newsManager = NewsManager.GetInstance();
        private readonly CustomerContactDA _customerContactDA = new CustomerContactDA();
        private readonly SystemConfigManager _systemConfigContact = SystemConfigManager.GetInstance();
	    private readonly GalleryPictureManager _galleryPictureManager = GalleryPictureManager.GetInstance();
        private readonly AdvertisingManager _advertisingManager = AdvertisingManager.GetInstance();

        [HandleError]
        [WhitespaceFilter] // nén nội dung html
        [ActionOutputCache(600)] // Caches for 600 seconds
        public ActionResult Index()
        {
            //lay title seo trang chủ
            var newcate = _newsCategoryManager.GetNewsCategoryByNameAscii("trang-chu");
            ViewBag.Title = newcate.SEOTitle;
            ViewBag.Description = newcate.SEODescription;
            ViewBag.Keywords = newcate.SEOKeyword;
            return View();
        }

        public ActionResult Share()
        {
            string name = Request["url"];
            ViewBag.url = name;
            return View();
        }
        public ActionResult MenuLeft()
		{
			var listMenu = _systemMenuManager.GetAllListSimple();
			return PartialView(listMenu);
		}
		public ActionResult Menu()
		{
			var listMenu = _systemMenuManager.GetAllListSimple();
			return PartialView(listMenu);
		}

        public ActionResult Youtube()
        {
            return View();
        }

        public ActionResult Product()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }
       
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            var cate = new NewsCategoryManager();
            var catemodle = cate.GetNewsCategoryByNameAscii("lien-he");
            if (catemodle != null)
                return View(catemodle);
            return View(new NewsCategoryItem());
        }

        public ActionResult PopupAdv()
        {
            return View();
        }

        public  void SendEmail(string email, string subject, string bodyContent)
        {
            string emailFromAddress = "";
            string emailSubject = subject;
            var emailBody = bodyContent;
            const int portServer = 587;
            const string smtpServer = "smtp.gmail.com";

            try
            {
                var mailMessage = new MailMessage();
                var smtpServerClient = new SmtpClient(smtpServer);

                mailMessage.From = new MailAddress(emailFromAddress);
                mailMessage.To.Add(email);
                mailMessage.Subject = emailSubject;
                mailMessage.Body = emailBody;
                mailMessage.IsBodyHtml = true;

                smtpServerClient.Port = portServer;
                smtpServerClient.Credentials = new NetworkCredential("forcustomeradc@gmail.com", "@#ADC@#1182");
                smtpServerClient.EnableSsl = true;

                smtpServerClient.Send(mailMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
                
        public ActionResult SendContact()
        {
            JsonMessage msg;
            try
            {
                #region Thêm mới liên hệ
                var contact = new CustomerContact();
                UpdateModel(contact);
                contact.Name = Request["sender_name"];
                contact.Email = Request["sender_email"];
                contact.Subject = Request["letter_subject"];
                contact.Message = Request["letter_text"];
                contact.TypeContact = 0;
                contact.IsShow = true;
                contact.Status = false;
                contact.Phone = Request["sender_phone"];
                contact.CreatedOnUtc = DateTime.Now;
                contact.IsDelete = false;
                _customerContactDA.Add(contact);
                _customerContactDA.Save();

                #endregion

                #region Gửi Email

                var emailInfo = _systemConfigContact.GetSystemConfigItemByID(Convert.ToInt32(ConfigurationManager.AppSettings["IDConfig"]));
                string content = "<div><h2>" + contact.Subject + "</h2></div>" +
                                 "<div>Họ tên: " + contact.Name + "</div>" +
                                 "<div>Email: " + contact.Email + "</div>" +
                                 "<div>Số điện thoại: " + contact.Phone + "</div>" +
                                 "<div>Tiêu đề: " + contact.Subject + "</div>" +
                                 "<div>Nội dung: " + contact.Message + "</div>";
                if(emailInfo != null)
                   Utility.SendEmail(emailInfo.EmailSend,emailInfo.EmailSendPwd,Request["sender_email"].ToString(),Request["letter_subject"].ToString(),content.ToString());
                #endregion
                msg = new JsonMessage
                {
                    Erros = false,
                    Message = "0",
                };
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex) {
                msg = new JsonMessage
                {
                    Erros = true,
                    Message = "1",
                };
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult RegisterContact(string info)
        {
            var msg = new JsonMessage();
            try
            {
                if (MyBase.IsValidEmail(info))
                {
                    var objcontact = _customerContactDA.GetByEmail(info);
                    var contact = new CustomerContact();
                    if (objcontact == null)
                    {
                        UpdateModel(contact);
                        contact.Email = info;
                        contact.TypeContact = 1;
                        contact.IsShow = true;
                        contact.IsDelete = false;
                        contact.Status = false;
                        contact.Name = info;
                        contact.CreatedOnUtc = DateTime.Now;
                        _customerContactDA.Add(contact);
                        _customerContactDA.Save();
                        msg.Erros = false;
                        msg.Message = "Bạn đăng ký thành công ! Cảm ơn bạn đã sử dụng dịch vụ của chúng tôi.";
                    }
                    else
                    {
                        UpdateModel(objcontact);
                        objcontact.CreatedOnUtc = DateTime.Now;
                        _customerContactDA.Save();
                        msg.Erros = false;
                        msg.Message = "Bạn đăng ký thành công ! Cảm ơn bạn đã sử dụng dịch vụ của chúng tôi.";
                    }
                }
                else
                {
                    msg.Erros = false;
                    msg.Message = "Email không hợp lệ.";
                }
                
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                msg = new JsonMessage
                {
                    Erros = true,
                    Message = "Đăng ký thất bại, vui lòng thử lại sau.",
                };
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }

        [ChildActionOnly]
        [ActionOutputCache(600)] // Caches for 600 seconds
        public ActionResult FooterGioiThieu()
        {
			var DMGioiThieu = ConfigurationManager.AppSettings["DMGioiThieu"] ?? string.Empty;
			var lstCategory = _newsCategoryManager.GetList().Where(m => m.NameAscii.Equals(DMGioiThieu)).ToList();
			var lstNews = lstCategory.SelectMany(item => item.ListNewsItem).OrderBy(m => m.DateCreated).ToList().Take(5);
		
			var BVMDGioiThieu = ConfigurationManager.AppSettings["BaiVietMacDinhDMGioiThieu"];
	        var news = new NewsItem();
			if (!string.IsNullOrEmpty(BVMDGioiThieu))
			{
				news = _newsManager.GetNewsByNameAscii(BVMDGioiThieu);
			}

			var DMDichVu = ConfigurationManager.AppSettings["DMDichVu"] ?? string.Empty;
			var lstCategoryDV = _newsCategoryManager.GetList().Where(m => m.NameAscii.Equals(DMDichVu)).ToList();
			var lstNewsDV = lstCategoryDV.SelectMany(item => item.ListNewsItem).OrderBy(m => m.DateCreated).ToList().Take(5);
			var modelGioiThieuNews = new ModelNewsGioiThieuItem();
			modelGioiThieuNews.TenDanhMucDichVu = DMDichVu;
			modelGioiThieuNews.ListNewsDichVu = lstNewsDV;
			modelGioiThieuNews.TenDanhMucGioiThieu = DMGioiThieu;
			modelGioiThieuNews.ListNewsGioiThieu = lstNews;
			modelGioiThieuNews.NewsItem = news;

			return PartialView(modelGioiThieuNews);
        }

        //public ActionResult FooterCopyRight(string key, string mode)
        //{
        //    var obj = _htmlSettingManager.GetList().SingleOrDefault(s => s.Key == key);
        //    obj.Ishow = User.Identity.IsAuthenticated;
        //    return PartialView("~/Views/_ModuleHTML/FooterCopyRight.cshtml", obj);
        //}

        //public ActionResult ThongTinLienHe()
        //{
        //    var obj = _htmlSettingManager.GetList().SingleOrDefault(s => s.Key == "ThongTinLienHe");
        //    return PartialView(obj);
        //}

        public ActionResult DangKyNhanTin()
        {
            return PartialView();
        }

        //public ActionResult LienHe()
        //{
        //    var obj = _htmlSettingManager.GetList().SingleOrDefault(s => s.Key == "LienHe");
        //    return PartialView(obj);
        //}

        //public ActionResult Facebook()
        //{
        //    var obj = _htmlSettingManager.GetList().SingleOrDefault(s => s.Key == "FaceBook");
        //    return PartialView(obj);
        //}

        public ActionResult FacebookFDI()
        {
            return View();
        }
        [HandleError]
        [WhitespaceFilter] // nén nội dung html
		public ActionResult About(string name)
		{
			var DMGioiThieu = ConfigurationManager.AppSettings["DMGioiThieu"] ?? string.Empty;
			var lstCategory = _newsCategoryManager.GetList().Where(m => m.NameAscii.Equals(DMGioiThieu));
            var lstNews = lstCategory.SelectMany(item => item.ListNewsItem).OrderBy(m => m.StartDateDisplay);
			var modelDichVuNews = new ModelNewsDichVuItem();
			if (lstNews.Any())
			{
                var lstCategorynew = _newsCategoryManager.GetNewsCategoryByNameAscii(DMGioiThieu);
                modelDichVuNews.SeoDescription = lstCategorynew.SEODescription;
                modelDichVuNews.SeoTitle = lstCategorynew.SEOTitle;
                modelDichVuNews.SeoKeywords = lstCategorynew.SEOKeyword;

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

			return PartialView(modelDichVuNews);
		}

		public ActionResult KhoGiaoDien()
		{
			var pageLink = ConfigurationManager.AppSettings["LinkKhoGiaoDien"];

			var catName = ConfigurationManager.AppSettings["KhoGiaoDien"];

			var lst = _galleryPictureManager.GetByCateName(catName).Take(8).OrderByDescending(m => m.DateCreated);
            
			ViewBag.pageLink = pageLink;
			return PartialView(lst);
		}

        public ActionResult GiaoDienTieuBieu()
        {
            var lstAdv = _advertisingManager.GetAdvertisingItemByID(Convert.ToInt32(ConfigurationManager.AppSettings["AdvertisingPositionInterface"]));
            var model = new ModelAdvertisingItem
            {
                ListItem = lstAdv,
            };
            return View(model);
        }


        [ChildActionOnly]
        [ActionOutputCache(600)] // Caches for 600 seconds
        public ActionResult ModulHTML(string key, string mode)
        {
            var obj = _htmlSettingManager.GetHtmlSettingByKey(key).FirstOrDefault();
            if (obj != null)
            {
                obj.Ishow = User.Identity.IsAuthenticated && mode=="edit";
                return PartialView("~/Views/_ModuleHTML/modulehtml.cshtml", obj);
            }
            return PartialView("~/Views/_ModuleHTML/modulehtml.cshtml", null);
        }

        public ActionResult ModulHTMLSlide(string key,string url, string mode)
        {
            var obj = _htmlSettingManagerDa.GetByKeyAndUrl(key,url);
            if (obj != null)
            {
                ViewBag.Ishow = User.Identity.IsAuthenticated && mode == "edit";
                ViewBag.url = url;
                return PartialView("~/Views/_ModuleHTML/ModulHTMLSlide.cshtml", obj);
            }
            return PartialView("~/Views/_ModuleHTML/ModulHTMLSlide.cshtml", null);
        }

    }
}
