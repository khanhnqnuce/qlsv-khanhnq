using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.DA.EndUser.Reposity;
using FDI.Simple;

namespace FDI.Business.Abstraction
{
    public class ShopProductAbstraction
    {
        readonly IReposityShopProduct _bridge;

        internal ShopProductAbstraction(IReposityShopProduct implementation)
        {
            _bridge = implementation;
        }
        public ProductItem GetProductItemByNameAsciiAbstract(string nameAscii, string lang)
        {
            return _bridge.GetProductItemByNameAscii(nameAscii, lang);
        }
        public ProductItem GetProductItemByIDAbstract(int id)
        {
            return _bridge.GetProductItemByID(id);
        }
        public List<ProductCategoryItem> GetAllListSimpleAbstract(string lang)
        {
            return _bridge.GetAllListSimple(lang);
        }
        public List<ProductItem> RelatedProducts(int currentPage, int rowPerPage, string type, List<ProductItem> listroductItem)
        {
            return _bridge.RelatedProducts(currentPage, rowPerPage, type, listroductItem);
        }

        public List<ProductItem> ProductPageAsciiAbstract(int currentPage, int rowPerPage, string type, List<ProductItem> listroductItem)
        {
            return _bridge.ProductsPage(currentPage, rowPerPage, type, listroductItem);
            ;
        }
    }
}
