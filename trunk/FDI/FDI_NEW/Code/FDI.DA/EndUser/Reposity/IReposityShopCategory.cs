using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Simple;

namespace FDI.DA.EndUser.Reposity
{
    public interface IReposityShopCategory
    {
        List<ProductCategoryItem> GetList(string lang);
        ProductCategoryItem GetProductCategoryItemByID(int id);
        ProductCategoryItem GetProductCategoryItemByUrl(string NameAscii);
        List<ProductCategoryItem> GetAllListSimple(string lang);
    }
}
