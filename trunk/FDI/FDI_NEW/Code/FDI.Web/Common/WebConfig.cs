using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace FDI
{
    public class WebConfig
    {
        public static string AdminUrl = ConfigurationManager.AppSettings["AdminUrl"].ToLower();
        //public static string Adminphucnhien = ConfigurationManager.AppSettings["Adminphucnhien"].ToLower();
        ////public static string AdminKt = ConfigurationManager.AppSettings["AdminKT"].ToLower();
        //public static string UrlOriginalImage = ConfigurationManager.AppSettings["URLOriginalImage"];
        //public static string UrlImagePromotion = ConfigurationManager.AppSettings["URLImagePromotion"];
        //public static string UrlImage = ConfigurationManager.AppSettings["URLImage"];
        //public static string Urltail = ConfigurationManager.AppSettings["URLtail"];
        public static string IDConfig = ConfigurationManager.AppSettings["IDConfig"];
        public static string CustomerID = ConfigurationManager.AppSettings["customerID"];
        public static string UserName = ConfigurationManager.AppSettings["UserName"];
        public static string NumberPv = ConfigurationManager.AppSettings["NumberPV"];
        public static string DichVu = ConfigurationManager.AppSettings["DMDichVu"];
        public static string GioiThieu = ConfigurationManager.AppSettings["BaiVietMacDinhDMGioiThieu"];
        public static string CustomerAvatar = ConfigurationManager.AppSettings["customerAvatar"];
        public static string SessionTimeout = ConfigurationManager.AppSettings["sessionTimeout"];
        public static string designsoft = ConfigurationManager.AppSettings["designsoft"];
        public static string designweb = ConfigurationManager.AppSettings["designweb"];
        public static string hosting = ConfigurationManager.AppSettings["hosting"];
        public static string ChiaSeKienThuc = ConfigurationManager.AppSettings["ChiaSeKienThuc"];
        public static string KienThucChiaSeWebsite = ConfigurationManager.AppSettings["Kienthucchiasewebsite"];
        public static string KienThucChiaSeHosting = ConfigurationManager.AppSettings["Kienthucchiasehosting"];
        public static string KienThucChiaSePhanMem = ConfigurationManager.AppSettings["Kienthucchiasephanmem"];
        public static string DichVuSeo = ConfigurationManager.AppSettings["DichVuSeo"];
        public static string KinhNghiemSeo = ConfigurationManager.AppSettings["KinhNghiemSeo"];
        public static string dvseo = ConfigurationManager.AppSettings["dvseo"];

        public static string GetAppSettings(string name)
        {
            return ConfigurationManager.AppSettings[name];
        }

    }
}