using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    public class DynamicCustomerItem : BaseSimple
    {
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsShow { get; set; }
        public string Description { get; set; }
        public decimal? PricePV { get; set; }
        public int? Month { get; set; }

        public virtual IEnumerable<TransactionHistoryItem> TransactionHistories { get; set; }
    }
    public class ModelDynamicCustomerItem : BaseModelSimple
    {
        public IEnumerable<DynamicCustomerItem> ListItem { get; set; }
        
    }
}
