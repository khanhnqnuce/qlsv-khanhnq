using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    public class UserItem :BaseSimple
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }

        public string NameAcsii { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }

        public string Mobile { get; set; }

        public DateTime? CreateDate { get; set; }
    }
    public class ModelUserItem : BaseModelSimple
    {
        public IEnumerable<UserItem> ListItem { get; set; }
    }
}
