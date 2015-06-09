using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing;

namespace CarMarket.Web
{
    /// <summary>
    /// Summary description for upload
    /// </summary>
    public class uploadify : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpPostedFile file = context.Request.Files["Filedata"];
            //string targetDirectory = ConfigData.IMAGE_UPLOAD + context.Request["folder"].Replace("/", "");
            string targetDirectory = ConfigData.IMAGE_UPLOAD_TEMP_FOLDER;
            string targetFilePath = Path.Combine(targetDirectory, file.FileName);

            // Uncomment the following line if you want to make the directory if it doesn't exist
            //if (!Directory.Exists(targetDirectory)) Directory.CreateDirectory(targetDirectory);

            file.SaveAs(targetFilePath);
            context.Response.Write("1");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }


    public static class ConfigData
    {
        //public static WebsiteConfig LoadConfig()
        //{
        //    string jsonData = ConfigSettings.ReadSetting("DataConfig");
        //    WebsiteConfig webConfig = new WebsiteConfig();
        //    MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonData));
        //    System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(webConfig.GetType());
        //    webConfig = serializer.ReadObject(ms) as WebsiteConfig;
        //    ms.Close();

        //    #region SetConfig
        //    IMAGE_MEDIUM_FILE = new Size(webConfig.ImageMediumWidth, webConfig.ImageMediumWidth);
        //    IMAGE_RESIZE_FILE = new Size(webConfig.ImageThumsbWidth, webConfig.ImageThumsbWidth);

        //    IMAGE_UPLOAD_FOLDER = System.Web.HttpContext.Current.Server.MapPath(webConfig.ImageUploadFolder);
        //    IMAGE_UPLOAD_TEMP_FOLDER = System.Web.HttpContext.Current.Server.MapPath(webConfig.ImageUploadFolderTemp);
        //    IMAGE_UPLOAD_THUMBS_FOLDER = System.Web.HttpContext.Current.Server.MapPath(webConfig.ImageUploadFolderThumbs);
        //    IMAGE_UPLOAD_ORIGINAL_FOLDER = System.Web.HttpContext.Current.Server.MapPath(webConfig.ImageUploadFolderOriginal);
        //    IMAGE_UPLOAD_MEDIUM_FOLDER = System.Web.HttpContext.Current.Server.MapPath(webConfig.ImageUploadFolderMedium);

        //    IMAGE_UPLOAD_URL = webConfig.ImageUploadFolder;
        //    IMAGE_UPLOAD_TEMP_URL = webConfig.ImageUploadFolder;
        //    IMAGE_UPLOAD_THUMBS_URL = webConfig.ImageUploadFolderThumbs;
        //    IMAGE_UPLOAD_ORIGINAL_URL = webConfig.ImageUploadFolderOriginal;
        //    IMAGE_UPLOAD_MEDIUM_URL = webConfig.ImageUploadFolderMedium;

        //    IMAGE_WATERMARK = webConfig.ImageWaterMark;

        //    COPY_RIGHT = webConfig.CopyRight;
        //    WEB_TITLE = webConfig.WebsiteTitle;
        //    WEB_DESCRIPTION = webConfig.WebsiteDescription;
        //    WEB_VERSION = webConfig.WebsiteVersion;

        //    ROW_PER_PAGE = webConfig.AdminRowPerPage;
        //    PAGE_STEP = webConfig.AdminPageStep;

        //    NEWS_PER_PAGE = webConfig.NewsPerPage;
        //    NEWS_PAGE_STEP = webConfig.NewsPageStep;
        //    NEWS_HOT_BOX = webConfig.NewsHotBox;
        //    NEWS_OTHER_BOX = webConfig.NewsOtherBox;
        //    NEWS_RIGHT_BOX = webConfig.NewsRightBox;

        //    MOBILE_PER_PAGE = webConfig.MobilePerPage;
        //    MOBILE_PAGE_STEP = webConfig.MobilePageStep;
        //    MOBILE_HOT_BOX = webConfig.MobileHotBox;
        //    MOBILE_HOT_BOX_LEVEL2 = webConfig.MobileHotBoxLevel2;
        //    MOBILE_OTHER_BOX = webConfig.MobileOtherBox;
        //    MOBILE_OTHER = webConfig.MobileOther;

        //    DEVICE_PER_PAGE = webConfig.DevicePerPage;
        //    DEVICE_PAGE_STEP = webConfig.DevicePageStep;
        //    DEVICE_HOT_BOX = webConfig.DeviceHotBox;
        //    DEVICE_OTHER_BOX = webConfig.DeviceOtherBox;

        //    COMMENT_PER_PAGE = webConfig.CommentPageStep;
        //    COMMENT_PAGE_STEP = webConfig.CommentPerPage;

        //    EMAIL_ACCOUNT = webConfig.EmailAccount;
        //    EMAIL_PASSWORD = webConfig.EmailPassword;
        //    EMAIL_SMTP = webConfig.EmailSMTP;
        //    EMAIL_PORT = webConfig.EmailPort;
        //    EMAIL_SSL = webConfig.EmailSsl;
        //    EMAIL_TO = webConfig.EmailTo;
        //    URLR_REWRITE_EXT = webConfig.ExtUrlRewrite;


        //    USERGUIDE_OTHER = webConfig.UserGuideOther;
        //    USERGUIDE_PAGE_STEP = webConfig.UserGuidePageStep;
        //    USERGUIDE_PERPAGE = webConfig.UserGuidePerPage;

        //    return webConfig;
        //    #endregion
        //}

        //public static void SaveConfig(WebsiteConfig webConfig)
        //{
        //    System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(webConfig.GetType());
        //    MemoryStream ms = new MemoryStream();
        //    serializer.WriteObject(ms, webConfig);
        //    string jsonData = Encoding.UTF8.GetString(ms.ToArray());
        //    ConfigSettings.WriteSetting("DataConfig", jsonData);
        //    LoadConfig();
        //}

        public static int USERGUIDE_PERPAGE = 30;
        public static int USERGUIDE_OTHER = 10;
        public static int USERGUIDE_PAGE_STEP;


        public static Size IMAGE_RESIZE_FILE = new Size(100, 100);
        public static Size IMAGE_THUMBS_SIZE_AVARTAR = new Size(200, 200);
        public static Size IMAGE_MEDIUM_FILE = new Size(640, 640);
        public static Size IMAGE_IMAGES_FILE = new Size(640, 640);
        public static Size IMAGE_THUMBS_SIZE = new Size(192, 192);

        public static string IMAGE_UPLOAD_FOLDER = HttpContext.Current.Server.MapPath("/Uploads/");
        public static string IMAGE_UPLOAD_TEMP_FOLDER = HttpContext.Current.Server.MapPath("/Uploads/Temp/");
        public static string IMAGE_UPLOAD_THUMBS_FOLDER = HttpContext.Current.Server.MapPath("/Uploads/Thumbs/");
        public static string IMAGE_UPLOAD_ORIGINAL_FOLDER = HttpContext.Current.Server.MapPath("/Uploads/Originals/");
        public static string IMAGE_UPLOAD_MEDIUM_FOLDER = HttpContext.Current.Server.MapPath("/Uploads/Mediums/");
        public static string IMAGE_UPLOAD_IMAGES_FOLDER = HttpContext.Current.Server.MapPath("/Uploads/images/");
        public static string IMAGE_UPLOAD_OUTOFSOTCK_FOLDER = HttpContext.Current.Server.MapPath("/Uploads/OutOfStocks/");

        public static string IMAGE_UPLOAD_URL = "/Uploads/";
        public static string IMAGE_UPLOAD_TEMP_URL = "/Uploads/Temp/";
        public static string IMAGE_UPLOAD_THUMBS_URL = "/Uploads/Thumbs/";
        public static string IMAGE_UPLOAD_ORIGINAL_URL = "/Uploads/Originals/";
        public static string IMAGE_UPLOAD_MEDIUM_URL = "/Uploads/Mediums/";
        public static string IMAGE_UPLOAD_IMAGES_URL = "/Uploads/images/";
        public static string IMAGE_UPLOAD_OUTOFSOTCK_URL = "/Uploads/OutOfStocks/";
        public static string IMAGE_WATERMARK = "Hoàng Hà Mobile";

        public static string COPY_RIGHT = "Copyright © 2014  ";
        public static string WEB_TITLE = " .com.vn";
        public static string WEB_DESCRIPTION = " .com.vn - ĐTDD, Laptop, Máy tính bảng, ...Chính hãng.";
        public static string WEB_VERSION = " ";

        public static int ROW_PER_PAGE = 25;
        public static int PAGE_STEP = 3;

        public static int NEWS_PER_PAGE = 10;
        public static int NEWS_PAGE_STEP = 3;
        public static int NEWS_HOT_BOX = 10;
        public static int NEWS_OTHER_BOX = 10;
        public static int NEWS_RIGHT_BOX = 8;

        public static int MOBILE_PER_PAGE = 15;
        public static int MOBILE_PAGE_STEP = 3;
        public static int MOBILE_HOT_BOX = 5;
        public static int MOBILE_HOT_BOX_LEVEL2 = 3;
        public static int MOBILE_OTHER = 10;
        public static int MOBILE_OTHER_BOX = 10;


        public static int DEVICE_PER_PAGE = 10;
        public static int DEVICE_PAGE_STEP = 3;
        public static int DEVICE_HOT_BOX = 10;
        public static int DEVICE_OTHER_BOX = 10;

        public static int COMMENT_PER_PAGE = 10;
        public static int COMMENT_PAGE_STEP = 3;

        //public static string EMAIL_ACCOUNT = "noreply@hoanghamobile.com";
        //public static string EMAIL_PASSWORD = "!!!@@@###zxc";
        //public static string EMAIL_SMTP = "smtp.google.com";
        //public static int EMAIL_PORT = 465;
        //public static bool EMAIL_SSL = true;
        //public static string EMAIL_TO = "contact@hoanghamobile.com";

        public static string URLR_REWRITE_EXT = ".aspx";

        public static string HEADER_FORM_REQUIRED = "Các trường có dấu <span class=\"star\">*</span> bắt buộc phải nhập.";
    }
}