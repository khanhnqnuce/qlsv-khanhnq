using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class AspnetUsersInRolesItem
    {
        public int ID { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

    }
    public class ModelAspnetUsersInRolesItem : BaseModelSimple
    {
        public IEnumerable<AspnetUsersInRolesItem> ListItem { get; set; }
    }
}
