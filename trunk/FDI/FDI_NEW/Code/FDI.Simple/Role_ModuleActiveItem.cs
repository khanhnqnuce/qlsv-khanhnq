using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    public class RoleModuleActiveItem :BaseSimple
    {
        
        public Guid? RoleId { get; set; }
        public int? ActiveRoleId { get; set; }
        public int? ModuleId { get; set; }
        public bool? Active { get; set; }
        public bool? Check { get; set; }

        public virtual AspnetRolesItem AspnetRoles { get; set; }
    }
    public class ModelRoleModuleActiveItem : BaseModelSimple
    {
        public IEnumerable<RoleModuleActiveItem> ListItem { get; set; }
    }
}
