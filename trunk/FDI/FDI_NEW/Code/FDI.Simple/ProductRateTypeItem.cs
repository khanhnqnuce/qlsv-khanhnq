using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class ProductRateTypeItem : BaseSimple
    {
        public string RateName { get; set; }
        public bool IsDelete { get; set; }

        public int TotalRate { get; set; }
        public double? MarkAvg { get; set; }
    }
    public class ModelProductRateTypeItem : BaseModelSimple
    {
        public IEnumerable<ProductRateTypeItem> ListItem { get; set; }
    }
}
