using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.DA.EndUser.Reposity;
using FDI.Simple;

namespace FDI.DA.EndUser.Implementation
{
    public class SystemMenuImplementation : InitDB, IRepositySystemMenu
    {
        public List<MenuItem> GetAllListSimple()
        {
            var query = from c in Instance.System_Menu
                        where c.MenuID > 1
                        orderby c.MenuOrder
                        select new MenuItem
                        {
                            MenuID = c.MenuID,
                            MenuTitle = c.MenuTitle,
                            MenuLink = c.MenuLink,
                            MenuOrder = c.MenuOrder,
                            MenuParentID = c.MenuParentID
                        };
            return query.ToList();
        }

        public MenuItem GetAllListSimpleByUrl(string url)
        {
            var query = from c in Instance.System_Menu
                        where c.MenuLink.ToLower().Equals(url.ToLower())
                        select new MenuItem
                        {
                            MenuTitle = c.MenuTitle,
                            MenuDescription = c.MenuDescription,
                            MenuLink = c.MenuLink
                        };
            return query.FirstOrDefault();
        }
    }
}
