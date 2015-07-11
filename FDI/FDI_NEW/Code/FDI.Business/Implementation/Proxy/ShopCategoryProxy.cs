using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Business.Facade;
using FDI.Business.Reposity;
using FDI.Simple;

namespace FDI.Business.Implementation.Proxy
{
    public class ShopCategoryProxy : ShopCategoryFacade, IProxyShopCategory
    {
        #region fields
        private static readonly Lazy<ShopCategoryProxy> lazy = new Lazy<ShopCategoryProxy>(() => new ShopCategoryProxy());
        #endregion

        #region properties
        public static ShopCategoryProxy GetInstance { get { return lazy.Value; } }

        #endregion

        #region constructor
        private ShopCategoryProxy()
        {
        }

        #endregion

        public List<ProductCategoryItem> GetList(string lang)
        {
            return InstanceShopCategory.ShopCategoryDal.GetListAbstract(lang);
        }
        public ProductCategoryItem GetProductCategoryItemByID(int id)
        {
            return InstanceShopCategory.ShopCategoryDal.GetProductCategoryItemByIDAbstract(id);
        }

        public ProductCategoryItem GetProductCategoryItemByUrl(string NameAscii)
        {
            return InstanceShopCategory.ShopCategoryDal.GetProductCategoryItemByIDAbstract(NameAscii);
        }

        public List<ProductCategoryItem> GetAllListSimple(string lang)
        {
            return InstanceShopCategory.ShopCategoryDal.GetAllListSimpleAbstract(lang);
        }
    }
}
