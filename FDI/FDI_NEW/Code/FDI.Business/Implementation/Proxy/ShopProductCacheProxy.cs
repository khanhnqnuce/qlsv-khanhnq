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
    public class ShopProductCacheProxy : ShopProductFacade, IProxyShopProduct
    {
        #region fields
        private static readonly Lazy<ShopProductCacheProxy> lazy = new Lazy<ShopProductCacheProxy>(() => new ShopProductCacheProxy());
        readonly ShopProductProxy _shopProductProxy = ShopProductProxy.GetInstance;
        #endregion

        #region properties
        public static ShopProductCacheProxy GetInstance { get { return lazy.Value; } }

        #endregion

        #region constructor
        private ShopProductCacheProxy()
        {
        }

        #endregion
        public ProductItem GetProductItemByNameAscii(string nameAscii, string lang)
        {
            var param = string.Format("ShopProductCacheProxyGetProductItemByNameAscii{0}", nameAscii);
            if (Cache.KeyExistsCache(param))
            {
                var lst = (ProductItem)Cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    Cache.DeleteCache(param);
                    // get new
                    var retval = _shopProductProxy.GetProductItemByNameAscii(nameAscii, lang);
                    // insert new cache
                    Cache.Set(param, retval, ConfigCache.TimeExpire);
                    return retval;
                }
                return lst;
            }
            else
            {
                var lst = _shopProductProxy.GetProductItemByNameAscii(nameAscii, lang);
                // insert new cache
                Cache.Set(param, lst, ConfigCache.TimeExpire);
                return lst;
            }
        }

        public List<ProductItem> RelatedProducts(int currentPage, int rowPerPage, string type, List<ProductItem> listroductItem)
        {
            var param = string.Format("ShopProductCacheProxyRelatedProducts{0}-{1}-{2}", currentPage, rowPerPage, type);
            if (Cache.KeyExistsCache(param))
            {
                var lst = (List<ProductItem>)Cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    Cache.DeleteCache(param);
                    // get new
                    var retval = _shopProductProxy.RelatedProducts(currentPage, rowPerPage, type, listroductItem);
                    // insert new cache
                    Cache.Set(param, retval, ConfigCache.TimeExpire);
                    return retval;
                }
                return lst;
            }
            else
            {
                var lst = _shopProductProxy.RelatedProducts(currentPage, rowPerPage, type, listroductItem);
                // insert new cache
                Cache.Set(param, lst, ConfigCache.TimeExpire);
                return lst;
            }
        }

        public ProductItem GetProductItemByID(int id)
        {
            var param = string.Format("ShopProductCacheProxyGetProductItemByID{0}", id);

            if (Cache.KeyExistsCache(param))
            {
                var lst = (ProductItem)Cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    Cache.DeleteCache(param);
                    // get new
                    var retval = _shopProductProxy.GetProductItemByID(id);
                    // insert new cache
                    Cache.Set(param, retval, ConfigCache.TimeExpire);
                    return retval;
                }
                return lst;
            }
            else
            {
                var lst = _shopProductProxy.GetProductItemByID(id);
                // insert new cache
                Cache.Set(param, lst, ConfigCache.TimeExpire);
                return lst;
            }
        }

        public List<ProductCategoryItem> GetAllListSimple(string lang)
        {
            const string param = "ShopProductCacheProxyGetAllListSimple";
            if (Cache.KeyExistsCache(param))
            {
                var lst = (List<ProductCategoryItem>)Cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    Cache.DeleteCache(param);
                    // get new
                    var retval = _shopProductProxy.GetAllListSimple(lang);
                    // insert new cache
                    Cache.Set(param, retval, ConfigCache.TimeExpire60);
                    return retval;
                }
                return lst;
            }
            else
            {
                var lst = _shopProductProxy.GetAllListSimple(lang);
                // insert new cache
                Cache.Set(param, lst, ConfigCache.TimeExpire60);
                return lst;
            }
        }

        public List<ProductItem> ProductsPage(int currentPage, int rowPerPage, string type, List<ProductItem> listroductItem)
        {
            var param = string.Format("ShopProductCacheProxyProductsPage{0}-{1}-{2}", currentPage, rowPerPage, type);

            if (Cache.KeyExistsCache(param))
            {
                var lst = (List<ProductItem>)Cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    Cache.DeleteCache(param);
                    // get new
                    var retval = _shopProductProxy.ProductsPage(currentPage, rowPerPage, type, listroductItem);
                    // insert new cache
                    Cache.Set(param, retval, ConfigCache.TimeExpire);
                    return retval;
                }
                return lst;
            }
            else
            {
                var lst = _shopProductProxy.ProductsPage(currentPage, rowPerPage, type, listroductItem);
                // insert new cache
                Cache.Set(param, lst, ConfigCache.TimeExpire);
                return lst;
            }
        }
    }
}
