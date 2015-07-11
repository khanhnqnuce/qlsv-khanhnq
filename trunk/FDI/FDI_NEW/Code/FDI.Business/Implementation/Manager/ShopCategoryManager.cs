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
    public class ShopCategoryManager
    {
        readonly IProxyShopCategory _shopCategory;
        readonly IProxyShopCategory _shopCategoryCache;
        private static readonly ShopCategoryManager Instance = new ShopCategoryManager();

        public static ShopCategoryManager GetInstance()
        {
            return Instance;
        }

        public ShopCategoryManager()
        {
            _shopCategory = ShopCategoryProxy.GetInstance;
            _shopCategoryCache = ConfigCache.EnableCache == 1 ? ShopCategoryCacheProxy.GetInstance : null;
        }
        public List<ProductCategoryItem> GetList(string lang)
        {
            if (ConfigCache.EnableCache == 1 && _shopCategoryCache != null)
            {
                return _shopCategoryCache.GetList(lang);
            }
            return _shopCategory.GetList(lang);
        }

        public ProductCategoryItem GetProductCategoryItemByID(int id)
        {
            if (ConfigCache.EnableCache == 1 && _shopCategoryCache != null)
            {
                return _shopCategoryCache.GetProductCategoryItemByID(id);
            }
            return _shopCategory.GetProductCategoryItemByID(id);
        }

        public ProductCategoryItem GetProductCategoryItemByUrl(string NameAscii)
        {
            if (ConfigCache.EnableCache == 1 && _shopCategoryCache != null)
            {
                return _shopCategoryCache.GetProductCategoryItemByUrl(NameAscii);
            }
            return _shopCategory.GetProductCategoryItemByUrl(NameAscii);
        }

        public List<ProductCategoryItem> GetAllListSimple(string lang)
        {
            if (ConfigCache.EnableCache == 1 && _shopCategoryCache != null)
            {
                return _shopCategoryCache.GetAllListSimple(lang);
            }
            return _shopCategory.GetAllListSimple(lang);
        }
    }
}
