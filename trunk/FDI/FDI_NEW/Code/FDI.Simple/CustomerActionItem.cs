using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class CustomerActionItem : BaseSimple
    {
        public string Name { get; set; }
        public int? Point { get; set; }
    }

    public class ModelCustomerActionItem : BaseModelSimple
    {
        public IEnumerable<CustomerActionItem> ListItem { get; set; }
    }
}
