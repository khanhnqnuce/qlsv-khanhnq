using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    public class ActiveRoleItem : BaseSimple
    {
        public string NameActive { get; set; }
        public int? Ord { get; set; }
        public virtual ICollection<RolesItem> Roles { get; set; }

    }
    public class ModelActiveRoleItem : BaseModelSimple
    {

        public IEnumerable<ActiveRoleItem> ListItem { get; set; }

    }
}
