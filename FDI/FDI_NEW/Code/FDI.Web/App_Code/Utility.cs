using System;
using System.Configuration;

namespace FDI.Web
{
    public class Utility
    {
        /// <summary>
        /// get original picture by picture url
        /// </summary>
        /// <param name="pictureName"></param>
        /// <returns></returns>
        public static string GetUrlOriginalPicture(string pictureName)
        {
            return ConfigurationManager.AppSettings["URLOriginalImage"] + pictureName;
        }

        /// <summary>
        /// get picture mediums(size 640) by url
        /// </summary>
        /// <param name="pictureName"></param>
        /// <returns></returns>
        public static string GetUrlPictureMediums(string pictureName)
        {
            return ConfigurationManager.AppSettings["URLImagePromotion"] + pictureName;
        }

        /// <summary>
        /// get picture 
        /// </summary>
        /// <param name="pictureName">name of picture</param>
        /// <returns></returns>
        public static string GetUrlPicture(string pictureName)
        {
            return ConfigurationManager.AppSettings["URLImage"] + pictureName;
        }

        public static bool CheckChamTraLoiComment(DateTime ngayGuiComment, DateTime? ngayTraLoi)
        {

            if (ngayTraLoi.HasValue)
            {
                var minute = (ngayTraLoi.Value - ngayGuiComment).Minutes;
                var day = (ngayTraLoi.Value - ngayGuiComment).Days;
                var hour = (ngayTraLoi.Value - ngayGuiComment).Hours;
                return minute > 15 || hour > 0 || day > 0;
            }
            else
            {
                var minute = (DateTime.Now - ngayGuiComment).Minutes;
                var day = (DateTime.Now - ngayGuiComment).Days;
                var hour = (DateTime.Now - ngayGuiComment).Hours;
                return minute > 15 || hour > 0 || day > 0;
            }
        }
 
    }
}
