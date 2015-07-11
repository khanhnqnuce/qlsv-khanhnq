using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class CustomerDeviceItem : BaseSimple
    {
        public string Name { get; set; }
    }
    public class ModelCustomerDeviceItem : BaseModelSimple
    {
        public IEnumerable<CustomerDeviceItem> ListItem { get; set; }
    }
}
