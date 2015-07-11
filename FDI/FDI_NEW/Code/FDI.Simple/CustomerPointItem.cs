using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class CustomerPointItem : BaseSimple
    {
        public string Note { get; set; }
        public int? CustomerId { get; set; }
        public int? Point { get; set; }
        public CustomerItem Customer { get; set; }
    }
    public class ModelCustomerPointItem : BaseModelSimple
    {
        public IEnumerable<CustomerPointItem> ListItem { get; set; }
    }
}
