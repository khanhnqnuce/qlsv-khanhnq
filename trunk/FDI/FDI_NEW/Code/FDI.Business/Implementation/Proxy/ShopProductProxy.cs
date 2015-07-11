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
    public class ShopProductProxy: ShopProductFacade, IProxyShopProduct
    {
        #region fields
        private static readonly Lazy<ShopProductProxy> lazy = new Lazy<ShopProductProxy>(() => new ShopProductProxy());
        #endregion

        #region properties
        public static ShopProductProxy GetInstance { get { return lazy.Value; } }

        #endregion

        #region constructor
        private ShopProductProxy()
        {
        }

        #endregion

        //public List<ProductItem> GetList()
        //{
        //    return InstanceShopProduct.ShopProductDal.GetListAbstract();
        //}
        public ProductItem GetProductItemByID(int id)
        {
            return InstanceShopProduct.ShopProductDal.GetProductItemByIDAbstract(id);
        }
        public List<ProductCategoryItem> GetAllListSimple(string lang)
        {
            return InstanceShopProduct.ShopProductDal.GetAllListSimpleAbstract(lang);
        }
        public List<ProductItem> RelatedProducts(int currentPage, int rowPerPage, string type, List<ProductItem> listroductItem)
        {
            return InstanceShopProduct.ShopProductDal.RelatedProducts(currentPage, rowPerPage, type, listroductItem);
        }
        public ProductItem GetProductItemByNameAscii(string nameAscii, string lang)
        {
            return InstanceShopProduct.ShopProductDal.GetProductItemByNameAsciiAbstract(nameAscii, lang);
        }

        public List<ProductItem> ProductsPage(int currentPage, int rowPerPage, string type, List<ProductItem> listroductItem)
        {
            return InstanceShopProduct.ShopProductDal.ProductPageAsciiAbstract(currentPage, rowPerPage, type, listroductItem);
        }
    }
}
