using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class ShopProductVaritantRecurringItem :BaseSimple
    {
        public int? ProductVariantID { get; set; }
        public double? MustRepaidPercent { get; set; }
        public string RecurringLength { get; set; }
        public double? InterestRate { get; set; }
        public string MetaKeywordRecurring { get; set; }
        public string MetaDescriptionRecurring { get; set; }
        public string RecurringTitle { get; set; }
        public bool? IsDelete { get; set; }

        public virtual ProductVariantItem ShopProductVariant { get; set; }
    }
    public class ModelShopProductVaritantRecurringItem : BaseModelSimple
    {
        public IEnumerable<ShopProductVaritantRecurringItem> ListItem { get; set; }
    }
}
