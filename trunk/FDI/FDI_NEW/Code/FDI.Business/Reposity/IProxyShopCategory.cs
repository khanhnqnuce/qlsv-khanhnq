using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Simple;

namespace FDI.Business.Reposity
{
    interface IProxyShopCategory
    {
        List<ProductCategoryItem> GetList(string lang);
        ProductCategoryItem GetProductCategoryItemByID(int id);
        ProductCategoryItem GetProductCategoryItemByUrl(string NameAscii);
        List<ProductCategoryItem> GetAllListSimple(string lang);
    }
}
