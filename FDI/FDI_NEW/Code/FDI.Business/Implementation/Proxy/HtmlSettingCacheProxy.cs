using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Base;
using FDI.Business.Facade;
using FDI.Business.Reposity;
using FDI.Simple;
using FDI.Utils;

namespace FDI.Business.Implementation.Proxy
{
    public class HtmlSettingCacheProxy: HtmlSettingFacade, IProxyHtmlSetting
    {
        #region fields
        private static readonly Lazy<HtmlSettingCacheProxy> lazy = new Lazy<HtmlSettingCacheProxy>(() => new HtmlSettingCacheProxy());
        readonly HtmlSettingProxy _htmlSettingProxy = HtmlSettingProxy.GetInstance;
        #endregion

        #region properties
        public static HtmlSettingCacheProxy GetInstance { get { return lazy.Value; } }

        #endregion

        #region constructor
        private HtmlSettingCacheProxy()
        {
        }

        #endregion
        public List<HtmlSettingItem> GetList()
        {
            const string param = "HtmlSettingCacheProxyGetList";
            if (Cache.KeyExistsCache(param))
            {
                var lst = (List<HtmlSettingItem>)Cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    Cache.DeleteCache(param);
                    // get new
                    var retval = _htmlSettingProxy.GetList();
                    // insert new cache
                    Cache.Set(param, retval, ConfigCache.TimeExpire);
                    return retval;
                }
                return lst;
            }
            else
            {
                var lst = _htmlSettingProxy.GetList();
                // insert new cache
                Cache.Set(param, lst, ConfigCache.TimeExpire);
                return lst;
            }
        }

        public List<HtmlSettingItem> GetHtmlSettingByKey(string key)
        {
            var param = string.Format("ShopCategoryCacheProxyGetHtmlSettingByKey{0}", key);

            if (Cache.KeyExistsCache(param))
            {
                var lst = (List<HtmlSettingItem>)Cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    Cache.DeleteCache(param);
                    // get new
                    var retval =_htmlSettingProxy.GetHtmlSettingByKey(key);
                    // insert new cache
                    Cache.Set(param, retval, ConfigCache.TimeExpire);
                    return retval;
                }
                return lst;
            }
            else
            {
                var lst = _htmlSettingProxy.GetHtmlSettingByKey(key);
                // insert new cache
                Cache.Set(param, lst, ConfigCache.TimeExpire);
                return lst;
            }
        }

        //public HtmlSettingItem GetHtmlSettingById(int id)
        //{
        //    var param = string.Format("ShopCategoryCacheProxyGetHtmlSettingById{0}", id);

        //    if (Cache.KeyExistsCache(param))
        //    {
        //        var lst = (HtmlSettingItem)Cache.GetCache(param);
        //        if (lst == null)
        //        {
        //            // delete old cache
        //            Cache.DeleteCache(param);
        //            // get new
        //            var retval = _HtmlSettingProxy.GetHtmlSettingById(id);
        //            // insert new cache
        //            Cache.Set(param, retval, ConfigCache.TimeExpire);
        //            return retval;
        //        }
        //        return lst;
        //    }
        //    else
        //    {
        //        var lst = _HtmlSettingProxy.GetHtmlSettingById(id);
        //        // insert new cache
        //        Cache.Set(param, lst, ConfigCache.TimeExpire);
        //        return lst;
        //    }
        //}

        //public int InsertHtmlSetting(News_Category HtmlSetting)
        //{
        //    return 0;
        //}

        //public int AddHtmlSetting(News_Category HtmlSetting)
        //{
        //    return 0;
        //}
        //public void Save()
        //{
            
        //}
    }
}
