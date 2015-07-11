using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class CustomerTypeItem : BaseSimple
    {
        public string Name { get; set; }
        public bool? IsDeleted { get; set; }
    }
    public class ModelCustomerTypeItem : BaseModelSimple
    {
        
        public IEnumerable<CustomerTypeItem> ListItem { get; set; }
    }
}
