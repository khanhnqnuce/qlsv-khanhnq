using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class MenuItem : BaseSimple
    {
        public int MenuID { get; set; }
        public int MenuParentID { get; set; }
        public string MenuTitle { get; set; }
        public string MenuLink { get; set; }
        public bool MenuTaget { get; set; }
        public bool MenuShow { get; set; }
        public int MenuOrder { get; set; }
        public string MenuDescription { get; set; }
        public virtual IEnumerable<MenuItem> System_Menu1 { get; set; }
        public virtual MenuItem System_Menu2 { get; set; }
    }
    public class ModelMenuItem : BaseModelSimple
    {
        public IEnumerable<MenuItem> ListItem { get; set; }
    }
}
