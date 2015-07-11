using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class ActionActiveItem:BaseSimple
    {
        public int? ModuleId { get; set; }
        public int? ActiveRoleId { get; set; }
        public string NameActive { get; set; }
        public bool Active { get; set; }
    }

    public class ModelActionActiveItem : BaseModelSimple
    {
        public IEnumerable<ActionActiveItem> ListItem { get; set; }
    }

}
