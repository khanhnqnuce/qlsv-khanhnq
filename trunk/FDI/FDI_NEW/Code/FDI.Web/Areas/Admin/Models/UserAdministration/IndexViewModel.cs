using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using PagedList;

namespace FDI.Areas.Admin.Models.UserAdministration
{
    public class IndexViewModel
    {
        public string Search { get; set; }
        public IPagedList<MembershipUser> Users { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public bool IsRolesEnabled { get; set; }
    }
}