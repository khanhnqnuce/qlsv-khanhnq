using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class PromotionOfProductItem : BaseSimple
    {
        public string PromotionProgramCode { get; set; }
        public string ProductNames { get; set; }
        public int? ProductVariantId { get; set; }
    }
    public class ModelPromotionOfProductItem : BaseModelSimple
    {
        public IEnumerable<PromotionOfProductItem> ListItem { get; set; }
    }
}
