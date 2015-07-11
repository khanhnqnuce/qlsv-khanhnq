using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    public class AspnetUsersItem
    {
        public Guid ApplicationId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string LoweredUserName { get; set; }
        public string MobileAlias { get; set; }
        public bool IsAnonymous { get; set; }
        public DateTime LastActivityDate { get; set; }
        public virtual IEnumerable<UserModuleActiveItem> User_ModuleActive { get; set; }
     
        public virtual IEnumerable<AspnetRolesItem> aspnet_Roles { get; set; }
        public virtual IEnumerable<ModuleItem> Modules { get; set; }
    }
    public class ModelAspnetUsersItem : BaseModelSimple
    {
       
        public IEnumerable<AspnetUsersItem> ListItem { get; set; }
    }
}
