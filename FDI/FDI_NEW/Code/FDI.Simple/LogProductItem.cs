using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    public class LogProductItem : BaseSimple
    {
        public int ProductId { get; set; }
        public string UserEdited { get; set; }
        public string PropertiesChanged { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime? DateChanged { get; set; }
        public String TypeActionName { get; set; }
    }
    public class ModelLogProductItem : BaseModelSimple
    {
        public IEnumerable<LogProductItem> ListItem { get; set; }
    }
}
