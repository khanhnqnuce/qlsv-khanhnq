using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    public class AspnetUsersIpMembershipItem:BaseSimple
    {
        public Guid? UserID { get; set; }
        public string IP { get; set; }
        public bool? IsActive { get; set; }
        public string UserName { get; set; }
        public virtual AspnetUsersItem aspnet_Users { get; set; }
    }
    public class ModelAspnetUsersIpMembershipItem : BaseModelSimple
    {
        public IEnumerable<AspnetUsersIpMembershipItem> ListItem { get; set; }
    }
}
