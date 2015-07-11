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
    public class NewsCategoryCacheProxy: NewsCategoryFacade, IProxyNewsCategory
    {
        #region fields
        private static readonly Lazy<NewsCategoryCacheProxy> lazy = new Lazy<NewsCategoryCacheProxy>(() => new NewsCategoryCacheProxy());
        readonly NewsCategoryProxy _newsCategoryProxy = NewsCategoryProxy.GetInstance;
        #endregion

        #region properties
        public static NewsCategoryCacheProxy GetInstance { get { return lazy.Value; } }

        #endregion

        #region constructor
        private NewsCategoryCacheProxy()
        {
        }

        #endregion
        public List<NewsCategoryItem> GetList()
        {
            const string param = "NewsCategoryCacheProxyGetList";
            if (Cache.KeyExistsCache(param))
            {
                var lst = (List<NewsCategoryItem>)Cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    Cache.DeleteCache(param);
                    // get new
                    var retval = _newsCategoryProxy.GetList();
                    // insert new cache
                    Cache.Set(param, retval, ConfigCache.TimeExpire);
                    return retval;
                }
                return lst;
            }
            else
            {
                var lst = _newsCategoryProxy.GetList();
                // insert new cache
                Cache.Set(param, lst, ConfigCache.TimeExpire);
                return lst;
            }
        }

        public NewsCategoryItem GetNewsCategoryByNameAscii(string nameAscii)
        {
            var param = string.Format("ShopCategoryCacheProxyGetNewsCategoryByNameAscii{0}", nameAscii);
            
            if (Cache.KeyExistsCache(param))
            {
                var lst = (NewsCategoryItem)Cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    Cache.DeleteCache(param);
                    // get new
                    var retval = _newsCategoryProxy.GetNewsCategoryByNameAscii(nameAscii);
                    // insert new cache
                    Cache.Set(param, retval, ConfigCache.TimeExpire);
                    return retval;
                }
                return lst;
            }
            else
            {
                var lst = _newsCategoryProxy.GetNewsCategoryByNameAscii(nameAscii);
                // insert new cache
                Cache.Set(param, lst, ConfigCache.TimeExpire);
                return lst;
            }
        }

        public NewsCategoryItem GetNewsCategoryById(int id)
        {
            var param = string.Format("ShopCategoryCacheProxyGetNewsCategoryById{0}", id);

            if (Cache.KeyExistsCache(param))
            {
                var lst = (NewsCategoryItem)Cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    Cache.DeleteCache(param);
                    // get new
                    var retval = _newsCategoryProxy.GetNewsCategoryById(id);
                    // insert new cache
                    Cache.Set(param, retval, ConfigCache.TimeExpire);
                    return retval;
                }
                return lst;
            }
            else
            {
                var lst = _newsCategoryProxy.GetNewsCategoryById(id);
                // insert new cache
                Cache.Set(param, lst, ConfigCache.TimeExpire);
                return lst;
            }
        }

        public int InsertNewsCategory(News_Category newsCategory)
        {
            return 0;
        }

        public int AddNewsCategory(News_Category newsCategory)
        {
            return 0;
        }
        public void Save()
        {
            
        }
    }
}
