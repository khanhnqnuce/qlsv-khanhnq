using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple 
{
    public class UserModuleActiveItem :BaseSimple
    {
        public Guid? UserId { get; set; }
        public int? ActiveRoleId { get; set; }
        public int? ModuleId { get; set; }
        public bool? Active { get; set; }
        public int? Check { get; set; }

        public virtual AspnetUsersItem AspnetUsers { get; set; }
        public virtual ModuleItem Module { get; set; }
    }
    public class ModelUserModuleActiveItem : BaseModelSimple
    {
        public IEnumerable<UserModuleActiveItem> ListItem { get; set; }
    }
}
