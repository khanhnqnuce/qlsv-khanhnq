using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class CustomerHistoryItem : BaseSimple
    {
        public string CustomerLink { get; set; }
        public int? CustomerID { get; set; }
        public int? CustomerActionID { get; set; }
        public DateTime? CreatedOnUtc { get; set; }

        public CustomerItem Customer { get; set; }
        public CustomerActionItem CustomerAction { get; set; }
    }
    public class ModelCustomerHistoryItem : BaseModelSimple
    {
        public IEnumerable<CustomerHistoryItem> ListItem { get; set; }
    }
}
