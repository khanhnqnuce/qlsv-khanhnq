using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class ModuleItem : BaseSimple
    {
        public string NameModule { get; set; }
        public string Tag { get; set; }
        public string ClassCss { get; set; }
        public int? Ord { get; set; }
        public int? PrarentID { get; set; }
        public string Content { get; set; }
        public bool? IsShow { get; set; }

        public virtual IEnumerable<RoleModuleActiveItem> Role_ModuleActive { get; set; }
        public virtual IEnumerable<UserModuleActiveItem> User_ModuleActive { get; set; }
       
        public virtual IEnumerable<AspnetRolesItem> AspnetRoles { get; set; }
        public virtual IEnumerable<AspnetUsersItem> AspnetUsers { get; set; }
    }
    public class ModelModuleItem : BaseModelSimple
    {
        public string Container { get; set; }
        public bool SelectMutil { get; set; }
        public IEnumerable<ModuleItem> ListItem { get; set; }
    }
}
