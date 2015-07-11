using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class PromotionGroupOfProductItem : BaseSimple
    {
        public string PromotionCode { get; set; }
        public List<PromotionOfProductItem> Promotion { get; set; }
    }
    public class ModelPromotionGroupOfProductItem : BaseModelSimple
    {
        public IEnumerable<PromotionGroupOfProductItem> ListItem { get; set; }
    }
}
