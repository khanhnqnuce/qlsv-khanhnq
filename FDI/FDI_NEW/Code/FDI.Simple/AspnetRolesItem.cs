using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Base;

namespace FDI.Simple
{
    public class AspnetRolesItem
    {
        public Guid ApplicationId { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public string LoweredRoleName { get; set; }
        public string Description { get; set; }

        //public virtual aspnet_Applications aspnet_Applications { get; set; }
        //public virtual ICollection<Role_ModuleActive> Role_ModuleActive { get; set; }
        public virtual IEnumerable<AspnetUsersItem> aspnet_Users { get; set; }
        public virtual IEnumerable<ActiveRoleItem> ActiveRoles { get; set; }
        public virtual IEnumerable<ModuleItem> Modules { get; set; }
    }
    public class ModelAspnetRolesItem : BaseModelSimple
    {
        public IEnumerable<AspnetRolesItem> ListItem { get; set; }
        public aspnet_Roles Roles { get; set; }
        public IEnumerable<ActiveRoleItem> ListActiveRoleItem { get; set; }
    }
}
