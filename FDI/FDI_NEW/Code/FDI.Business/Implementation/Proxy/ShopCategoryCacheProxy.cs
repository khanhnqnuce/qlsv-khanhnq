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
    public class ShopCategoryCacheProxy: ShopCategoryFacade, IProxyShopCategory
    {
        #region fields
        private static readonly Lazy<ShopCategoryCacheProxy> lazy = new Lazy<ShopCategoryCacheProxy>(() => new ShopCategoryCacheProxy());
        readonly ShopCategoryProxy _shopCategoryProxy = ShopCategoryProxy.GetInstance;
        #endregion

        #region properties
        public static ShopCategoryCacheProxy GetInstance { get { return lazy.Value; } }

        #endregion

        #region constructor
        private ShopCategoryCacheProxy()
        {
        }

        #endregion
        public List<ProductCategoryItem> GetList(string lang)
        {
            const string param = "ShopCategoryCacheProxyGetList";
            if (Cache.KeyExistsCache(param))
            {
                var lst = (List<ProductCategoryItem>)Cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    Cache.DeleteCache(param);
                    // get new
                    var retval = _shopCategoryProxy.GetList(lang);
                    // insert new cache
                    Cache.Set(param, retval, ConfigCache.TimeExpire);
                    return retval;
                }
                return lst;
            }
            else
            {
                var lst = _shopCategoryProxy.GetList(lang);
                // insert new cache
                Cache.Set(param, lst, ConfigCache.TimeExpire);
                return lst;
            }
        }
        
        public ProductCategoryItem GetProductCategoryItemByID(int id)
        {
            var param = string.Format("ShopCategoryCacheProxyGetProductCategoryItemByID{0}", id);
            
            if (Cache.KeyExistsCache(param))
            {
                var lst = (ProductCategoryItem)Cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    Cache.DeleteCache(param);
                    // get new
                    var retval = _shopCategoryProxy.GetProductCategoryItemByID(id);
                    // insert new cache
                    Cache.Set(param, retval, ConfigCache.TimeExpire);
                    return retval;
                }
                return lst;
            }
            else
            {
                var lst = _shopCategoryProxy.GetProductCategoryItemByID(id);
                // insert new cache
                Cache.Set(param, lst, ConfigCache.TimeExpire);
                return lst;
            }
        }

        public ProductCategoryItem GetProductCategoryItemByUrl(string NameAscii)
        {
            var param = string.Format("ShopCategoryCacheProxyGetProductCategoryItemByUrl{0}", NameAscii);

            if (Cache.KeyExistsCache(param))
            {
                var lst = (ProductCategoryItem)Cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    Cache.DeleteCache(param);
                    // get new
                    var retval = _shopCategoryProxy.GetProductCategoryItemByUrl(NameAscii);
                    // insert new cache
                    Cache.Set(param, retval, ConfigCache.TimeExpire);
                    return retval;
                }
                return lst;
            }
            else
            {
                var lst = _shopCategoryProxy.GetProductCategoryItemByUrl(NameAscii);
                // insert new cache
                Cache.Set(param, lst, ConfigCache.TimeExpire);
                return lst;
            }
        }


        public List<ProductCategoryItem> GetAllListSimple(string lang)
        {
            const string param = "ShopCategoryCacheProxyGetAllListSimple";
            if (Cache.KeyExistsCache(param))
            {
                var lst = (List<ProductCategoryItem>)Cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    Cache.DeleteCache(param);
                    // get new
                    var retval = _shopCategoryProxy.GetAllListSimple(lang);
                    // insert new cache
                    Cache.Set(param, retval, ConfigCache.TimeExpire360);
                    return retval;
                }
                return lst;
            }
            else
            {
                var lst = _shopCategoryProxy.GetAllListSimple(lang);
                // insert new cache
                Cache.Set(param, lst, ConfigCache.TimeExpire360);
                return lst;
            }
        }
    }
}
