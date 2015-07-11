using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class CustomerContactItem : BaseSimple
    {

        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool IsShow { get; set; }
        public bool IsDelete { get; set; }
        public int? TypeContact { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public bool? Status { get; set; }
    }
    public class ModelCustomerContactItem : BaseModelSimple
    {
        public IEnumerable<CustomerContactItem> ListItem { get; set; }
    }
}
