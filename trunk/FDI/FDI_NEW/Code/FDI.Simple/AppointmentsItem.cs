using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    public class AppointmentsItem:BaseSimple
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public bool IsActive { get; set; }
        public bool? IsShow { get; set; }
        public bool? IsReply { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? DateOrder { get; set; }
        public int? CustomerId { get; set; }
        public CustomerItem Customer { get; set; }
    }
    public class ModelAppointmentsItem : BaseModelSimple
    {
        public IEnumerable<AppointmentsItem> ListItem { get; set; }
    }
}
