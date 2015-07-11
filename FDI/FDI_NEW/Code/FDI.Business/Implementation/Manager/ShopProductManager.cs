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
    public class ShopProductManager
    {
        readonly IProxyShopProduct _shopCategory;
        readonly IProxyShopProduct _shopCategoryCache;
        private static readonly ShopProductManager Instance = new ShopProductManager();

        public static ShopProductManager GetInstance()
        {
            return Instance;
        }

        public ShopProductManager()
        {
            _shopCategory = ShopProductProxy.GetInstance;
            _shopCategoryCache = ConfigCache.EnableCache == 1 ? ShopProductCacheProxy.GetInstance : null;
        }
        public ProductItem GetProductItemByNameAscii(string nameAscii, string lang)
        {
            if (ConfigCache.EnableCache == 1 && _shopCategoryCache != null)
            {
                return _shopCategoryCache.GetProductItemByNameAscii(nameAscii, lang);
            }
            return _shopCategory.GetProductItemByNameAscii(nameAscii, lang);
        }

        public ProductItem GetProductItemByID(int id)
        {
            if (ConfigCache.EnableCache == 1 && _shopCategoryCache != null)
            {
                return _shopCategoryCache.GetProductItemByID(id);
            }
            return _shopCategory.GetProductItemByID(id);
        }
        public List<ProductItem> RelatedProducts(int currentPage, int rowPerPage, string type, List<ProductItem> listroductItem)
        {
            if (ConfigCache.EnableCache == 1 && _shopCategoryCache != null)
            {
                return _shopCategoryCache.RelatedProducts(currentPage, rowPerPage, type, listroductItem);
            }
            return _shopCategory.RelatedProducts(currentPage, rowPerPage, type, listroductItem);
        }

        public List<ProductCategoryItem> GetAllListSimple(string lang)
        {
            if (ConfigCache.EnableCache == 1 && _shopCategoryCache != null)
            {
                return _shopCategoryCache.GetAllListSimple(lang);
            }
            return _shopCategory.GetAllListSimple(lang);
        }

        public List<ProductItem> ProductsPage(int currentPage, int rowPerPage, string type, List<ProductItem> listNewsItem)
        {
            if (ConfigCache.EnableCache == 1 && _shopCategoryCache != null)
            {
                return _shopCategoryCache.ProductsPage(currentPage, rowPerPage, type, listNewsItem);
            }
            return _shopCategory.ProductsPage(currentPage, rowPerPage, type, listNewsItem);
        }
    }
}
