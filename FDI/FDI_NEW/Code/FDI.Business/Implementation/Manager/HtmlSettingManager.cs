using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Business.Implementation.Proxy;
using FDI.Business.Reposity;
using FDI.Simple;
using FDI.Utils;

namespace FDI.Business.Implementation.Manager
{
    public class HtmlSettingManager
    {
        readonly IProxyHtmlSetting _htmlSetting;
        readonly IProxyHtmlSetting _htmlSettingCache;
        private static readonly HtmlSettingManager Instance = new HtmlSettingManager();

        public static HtmlSettingManager GetInstance()
        {
            return Instance;
        }

        public HtmlSettingManager()
        {
            _htmlSetting = HtmlSettingProxy.GetInstance;
            _htmlSettingCache = ConfigCache.EnableCache == 1 ? HtmlSettingCacheProxy.GetInstance : null;
        }

        public List<HtmlSettingItem> GetList()
        {
            if (ConfigCache.EnableCache == 1 && _htmlSettingCache != null)
            {
                return _htmlSettingCache.GetList();
            }
            return _htmlSetting.GetList();
        }

        //public List<HtmlSettingItem> GetListHots()
        //{
        //    if (ConfigCache.EnableCache == 1 && _htmlSettingCache != null)
        //    {
        //        return _htmlSettingCache.GetListHots();
        //    }
        //    return _htmlSetting.GetListHots();
        //}

        public List<HtmlSettingItem> GetHtmlSettingByKey(string nameAcsii)
        {
            if (ConfigCache.EnableCache == 1 && _htmlSettingCache != null)
            {
                return _htmlSettingCache.GetHtmlSettingByKey(nameAcsii);
            }
            return _htmlSetting.GetHtmlSettingByKey(nameAcsii);
        }
        //public List<HtmlSettingItem> HtmlSettingPage(int currentPage, int rowPerPage, string type, List<HtmlSettingItem> listHtmlSettingItem)
        //{
        //    if (ConfigCache.EnableCache == 1 && _htmlSettingCache != null)
        //    {
        //        return _htmlSettingCache.HtmlSettingPage(currentPage, rowPerPage, type, listHtmlSettingItem);
        //    }
        //    return _htmlSetting.HtmlSettingPage(currentPage, rowPerPage, type, listHtmlSettingItem);
        //}


      

        
    }
}
