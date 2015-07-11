using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class ShopProductToExcelItem
    {
        public string Name { get; set; }
        public string NameAscii { get; set; }
        public string Sku { get; set; }
        public string Brand { get; set; }
        public decimal? Price { get; set; }
    }
    public class ModelShopProductToExcelItem : BaseModelSimple
    {
        public IEnumerable<ShopProductToExcelItem> ListItem { get; set; }
    }
}
