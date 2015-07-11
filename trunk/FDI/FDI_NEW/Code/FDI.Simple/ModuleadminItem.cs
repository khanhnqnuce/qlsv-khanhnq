using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class ModuleadminItem:BaseSimple
    {
        public string NameModule { get; set; }
        public string Url { get; set; }
        public string ClassCss { get; set; }
        public int? Ord { get; set; }
        public int PrarentID { get; set; }
        public string Content { get; set; }
        public bool IsShow { get; set; }
        public int Active { get; set; }
    }
    public class ModelModuleadminItem : BaseModelSimple
    {
        public IEnumerable<ModuleadminItem> ListItem { get; set; }
    }
}
