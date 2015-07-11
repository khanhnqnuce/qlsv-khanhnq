using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.DA.EndUser.Reposity;
using FDI.Simple;

namespace FDI.Business.Abstraction
{
    public class ShopCategoryAbstraction
    {
        readonly IReposityShopCategory _bridge;

        internal ShopCategoryAbstraction(IReposityShopCategory implementation)
        {
            _bridge = implementation;
        }
        public List<ProductCategoryItem> GetListAbstract(string lang)
        {
            return _bridge.GetList(lang);
        }
        public ProductCategoryItem GetProductCategoryItemByIDAbstract(int id)
        {
            return _bridge.GetProductCategoryItemByID(id);
        }

        public ProductCategoryItem GetProductCategoryItemByIDAbstract(string NameAscii)
        {
            return _bridge.GetProductCategoryItemByUrl(NameAscii);
        }

        public List<ProductCategoryItem> GetAllListSimpleAbstract(string lang)
        {
            return _bridge.GetAllListSimple(lang);
        }
    }
}
