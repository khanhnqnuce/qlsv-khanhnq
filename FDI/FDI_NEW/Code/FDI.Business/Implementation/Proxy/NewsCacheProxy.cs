using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Business.Facade;
using FDI.Business.Reposity;
using FDI.Simple;
using FDI.Utils;

namespace FDI.Business.Implementation.Proxy
{
    class NewsCacheProxy: NewsFacade, IProxyNews
    {
        #region fields
        private static readonly Lazy<NewsCacheProxy> lazy = new Lazy<NewsCacheProxy>(() => new NewsCacheProxy());
        readonly NewsProxy _newsProxy = NewsProxy.GetInstance;
        #endregion

        #region properties
        public static NewsCacheProxy GetInstance { get { return lazy.Value; } }

        #endregion

        #region constructor
        private NewsCacheProxy()
        {
        }

        #endregion
        public List<NewsItem> GetList()
        {
            const string param = "NewsCacheProxyGetList";
            if (Cache.KeyExistsCache(param))
            {
                var lst = (List<NewsItem>)Cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    Cache.DeleteCache(param);
                    // get new
                    var retval = _newsProxy.GetList();
                    // insert new cache
                    Cache.Set(param, retval, ConfigCache.TimeExpire);
                    return retval;
                }
                return lst;
            }
            else
            {
                var lst = _newsProxy.GetList();
                // insert new cache
                Cache.Set(param, lst, ConfigCache.TimeExpire);
                return lst;
            }
        }

        public List<NewsItem> GetListHots()
        {
            const string param = "NewsCacheProxyGetListHots";
            if (Cache.KeyExistsCache(param))
            {
                var lst = (List<NewsItem>)Cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    Cache.DeleteCache(param);
                    // get new
                    var retval = _newsProxy.GetListHots();
                    // insert new cache
                    Cache.Set(param, retval, ConfigCache.TimeExpire);
                    return retval;
                }
                return lst;
            }
            else
            {
                var lst = _newsProxy.GetListHots();
                // insert new cache
                Cache.Set(param, lst, ConfigCache.TimeExpire);
                return lst;
            }
        }

        public NewsItem GetNewsByNameAscii(string nameAscii)
        {
            var param = string.Format("NewsCacheProxyGetNewsByNameAscii{0}", nameAscii);
            
            if (Cache.KeyExistsCache(param))
            {
                var lst = (NewsItem)Cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    Cache.DeleteCache(param);
                    // get new
                    var retval = _newsProxy.GetNewsByNameAscii(nameAscii);
                    // insert new cache
                    Cache.Set(param, retval, ConfigCache.TimeExpire);
                    return retval;
                }
                return lst;
            }
            else
            {
                var lst = _newsProxy.GetNewsByNameAscii(nameAscii);
                // insert new cache
                Cache.Set(param, lst, ConfigCache.TimeExpire);
                return lst;
            }
        }
        public List<NewsItem> NewsPage(int currentPage, int rowPerPage, string type, List<NewsItem> listNewsItem)
        {
            var param = string.Format("NewsCacheProxyNewsPage{0}-{1}-{2}", currentPage, rowPerPage, type);

            if (Cache.KeyExistsCache(param))
            {
                var lst = (List<NewsItem>)Cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    Cache.DeleteCache(param);
                    // get new
                    var retval = _newsProxy.NewsPage(currentPage, rowPerPage, type, listNewsItem);
                    // insert new cache
                    Cache.Set(param, retval, ConfigCache.TimeExpire);
                    return retval;
                }
                return lst;
            }
            else
            {
                var lst = _newsProxy.NewsPage(currentPage, rowPerPage, type, listNewsItem);
                // insert new cache
                Cache.Set(param, lst, ConfigCache.TimeExpire);
                return lst;
            }
        }
    }
}
