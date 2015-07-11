using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using FDI;

namespace FDI
{
    public static class RenderAds
    {
        /// <summary>
        /// Render string quảng cáo
        /// </summary>
        /// <param name="ltsAds">Danh sách ID của các quảng cáo</param>
        /// <returns>String các quảng cáo</returns>
        public static string RenderListAds(List<Simple.AdvertisingItem> ltsAds)
        {
            var stb = new StringBuilder();
            foreach (var ads in ltsAds.OrderByDescending(ads => ads.ID))
            {
                switch (ads.TypeID)
                {
                    default:
                    case 1: //Thay kiểu ảnh
                        stb.AppendFormat("<div><a href=\\\"{0}\\\" target=\\\"_blank\\\" title=\\\"{1}\\\">{1}</a></div>", ads.Link, ads.Name);
                        break;

                    case 2: //Thay kiểu video
                        stb.AppendFormat("<div><a href=\\\"{0}\\\" target=\\\"_blank\\\" title=\\\"{1}\\\"><img width=\\\"{2}\\\" height=\\\"{3}\\\" src=\\\"{4}\\\"/></a></div>", ads.Link, ads.Name, ads.Width, ads.Height, ads.Url);
                        break;

                    case 3: //Flash
                        stb.AppendFormat("<object classid=\\\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\\\" codebase=\\\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,40,0\\\" width=\\\"{0}\\\" height=\\\"{1}\\\">", ads.Width, ads.Height);
                        stb.AppendFormat("<param name=\\\"movie\\\" value=\\\"{0}\\\" />", ads.Url);
                        stb.Append("<param name=\\\"quality\\\" value=\\\"high\\\" />");
                        stb.Append("<param name=\\\"bgcolor\\\" value=\\\"#ffffff\\\" />");
                        stb.AppendFormat("<embed src=\\\"{0}\\\" quality=\\\"high\\\" bgcolor=\\\"#ffffff\\\"width=\\\"{1}\\\" height=\\\"{2}\\\" name=\\\"mymoviename\\\" align=\\\"\\\" type=\\\"application/x-shockwave-flash\\\" pluginspage=\\\"http://www.macromedia.com/go/getflashplayer\\\"></embed>", ads.Url, ads.Width, ads.Height);
                        stb.AppendFormat("</object>");
                        break;


                }
            }
            return stb.ToString();
        }

        /// <summary>
        /// Render string quảng cáo
        /// </summary>
        /// <param name="ltsAds">Danh sách ID của các quảng cáo</param>
        /// <returns>String các quảng cáo</returns>
        public static string RenderSingleAds(FDI.Simple.AdvertisingItem ads)
        {
            var stb = new StringBuilder();
            switch (ads.TypeID)
            {
                default:
                case 1: //Thay kiểu ảnh
                    stb.AppendFormat("<a href=\"{0}\"  title=\"{2}\"><img src=\"{1}\" class=\"banner_click\" id= \"{3}\" /></a>", ads.Link, FDI.Web.Utility.GetUrlOriginalPicture(ads.PictureUrl), ads.Name, ads.ID);
                    break;
                case 2: //Flash
                    //stb.AppendFormat("<object classid=\\\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\\\" codebase=\\\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,40,0\\\" width=\\\"{0}\\\" height=\\\"{1}\\\">", ads.Width, ads.Height);
                    //stb.AppendFormat("<param name=\\\"movie\\\" value=\\\"{0}\\\" />", ads.Url);
                    //stb.Append("<param name=\\\"quality\\\" value=\\\"high\\\" />");
                    //stb.Append("<param name=\\\"bgcolor\\\" value=\\\"#ffffff\\\" />");
                    stb.AppendFormat("<embed src=\"{0}\" quality=\"high\" bgcolor=\\\"#ffffff\\\"width=\"{1}\" height=\\\"{2}\\\" name=\"{3}\" align=\\\"\\\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\"></embed>", ads.Url, ads.Width, ads.Height, ads.Name);
                    //stb.AppendFormat("</object>");
                    break;
                case 3: //Thay kiểu Video
                    stb.AppendFormat("<a href=\"{0}\" target=\\\"_blank\\\" title=\"{2}\"><iframe src=\"//www.youtube.com/embed/{1}\" frameborder=\"0\" allowfullscreen></iframe></a>", ads.Link, ads.Url, ads.Name);
                    break;    
            }
            return stb.ToString();
        }
    }
}