using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class SystemLogItem : BaseSimple
    {
        public Guid? UserID { get; set; }
        public string AddressIP { get; set; }
        public int? ActionType { get; set; }
        public string ActionModule { get; set; }
        public string ActionSubModule { get; set; }
        public string UrlLink { get; set; }
        public DateTime? DateCreated { get; set; }
        public string UserName { get; set; }
        public string ActionTypeName { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }
    }
    public class ModelSystemLogItem : BaseModelSimple
    {
        public IEnumerable<SystemLogItem> ListItem { get; set; }
    }
}
