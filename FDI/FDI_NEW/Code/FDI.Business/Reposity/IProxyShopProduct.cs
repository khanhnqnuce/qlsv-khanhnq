using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Simple;

namespace FDI.Business.Reposity
{
    interface IProxyShopProduct
    {
        ProductItem GetProductItemByID(int id);
        ProductItem GetProductItemByNameAscii(string nameAscii, string lang);

        List<ProductItem> RelatedProducts(int currentPage, int rowPerPage, string type,
                                          List<ProductItem> listroductItem);
        List<ProductCategoryItem> GetAllListSimple(string lang);

        List<ProductItem> ProductsPage(int currentPage, int rowPerPage, string type, List<ProductItem> listroductItem);

    }
}
