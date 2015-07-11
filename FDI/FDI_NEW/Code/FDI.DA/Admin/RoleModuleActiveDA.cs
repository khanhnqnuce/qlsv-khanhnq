using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Base;

namespace FDI.DA.Admin
{
    public class RoleModuleActiveDA :BaseDA
    {
        #region Constructer
        public RoleModuleActiveDA()
        {
        }

        public RoleModuleActiveDA(string pathPaging)
        {
            PathPaging = pathPaging;
        }

        public RoleModuleActiveDA(string pathPaging, string pathPagingExt)
        {
            PathPaging = pathPaging;
            PathPagingext = pathPagingExt;
        }
        #endregion
        public List<Role_ModuleActive> GetListRoleModuleActivekt(Guid id, int moduleid)
        {
            var query = from c in FDIDB.Role_ModuleActive
                        where c.RoleId == id && c.ModuleId == moduleid
                        select c;
            return query.ToList();
        }

        public List<Role_ModuleActive> GetListRoleModuleActivekt(Guid id)
        {
            var query = from c in FDIDB.Role_ModuleActive
                        where c.RoleId == id
                        select c;
            return query.ToList();
        }
        public Role_ModuleActive GetByRoleModuleActiveId(int id)
        {
            var query = from c in FDIDB.Role_ModuleActive
                        where c.ID == id
                        select c;
            return query.FirstOrDefault();
        }


        public void Delete(Role_ModuleActive roleModuleActive)
        {
            FDIDB.Role_ModuleActive.Remove(roleModuleActive);
        }

        public void Save()
        {
            FDIDB.SaveChanges();
        }
    }
}
