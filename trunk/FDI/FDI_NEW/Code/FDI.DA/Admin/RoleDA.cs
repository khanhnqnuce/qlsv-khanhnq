using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using FDI.Base;
using FDI.Simple;

namespace FDI.DA.Admin
{
    public class RoleDA:BaseDA
    {
        #region Constructer
        public RoleDA()
        {
        }

        public RoleDA(string pathPaging)
        {
            PathPaging = pathPaging;
        }

        public RoleDA(string pathPaging, string pathPagingExt)
        {
            PathPaging = pathPaging;
            PathPagingext = pathPagingExt;
        }
        #endregion

        public aspnet_Membership GetListByMembership(Guid userId)
        {
            var query = from c in FDIDB.aspnet_Membership
                        where c.UserId == userId
                        select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        public List<RolesItem> GetListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from c in FDIDB.aspnet_Roles                        
                        select new RolesItem
                                   {
                                       ApplicationId = c.ApplicationId,
                                       RoleId = c.RoleId,
                                       RoleName = c.RoleName,
                                       Description = c.Description,
                                       //LoweredRoleName = c.LoweredRoleName
                                   };
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }

        public List<aspnet_Roles> GetListAll()
        {
            var query = from c in FDIDB.aspnet_Roles
                        select c;
            return query.ToList();
        }

        public List<AspnetRolesItem> GetListItemAll()
        {
            var query = from c in FDIDB.aspnet_Roles
                        select new AspnetRolesItem()
                                   {
                                       RoleId = c.RoleId,
                                       RoleName = c.RoleName
                                   };
            return query.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public RolesItem GetRoleItemById(Guid roleId)
        {
            var query = from c in FDIDB.aspnet_Roles
                        where c.RoleId == roleId
                        select new RolesItem
                                   {
                                       RoleId = c.RoleId,
                                       RoleName = c.RoleName,
                                       Description = c.Description
                                   };
            var result = query.FirstOrDefault();
            return result;
        }
        public List<ActiveRoleItem> GetActiveRoleAll()
        {
            var query = from c in FDIDB.ActiveRoles
                        select new ActiveRoleItem
                                   {
                                       ID = c.Id,
                                       NameActive = c.NameActive
                                   };
            return query.ToList();
        }

        public List<ActiveRole> GetlistActiveRole()
        {
            var query = from c in FDIDB.ActiveRoles
                        select c;
            return query.ToList();
        }

        public aspnet_Roles GetById(Guid roleId)
        {
            var query = from c in FDIDB.aspnet_Roles
                        where c.RoleId == roleId
                        select c;
            return query.FirstOrDefault();
        }
        public aspnet_Applications GetByApplicationId(Guid applicationId)
        {
            var query = from c in FDIDB.aspnet_Applications
                        where c.ApplicationId == applicationId
                        select c;
            return query.FirstOrDefault();
        }

        public List<aspnet_Roles> getListByArrID(List<System.Guid> LtsArrID)
        {
            var query = from c in FDIDB.aspnet_Roles where LtsArrID.Contains(c.RoleId) select c;
            return query.ToList();
        }

        //public List<aspnet_UsersInRoles> getListByGuidID(List<System.Guid> LtsArrID)
        //{
        //    var query = from c in FDIDB.aspnet_UsersInRoles where LtsArrID.Contains(c.RoleId) select c;
        //    return query.ToList();
        //}
        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="roles"> </param>
        public void Add(aspnet_Roles roles)
        {
            FDIDB.aspnet_Roles.Add(roles);
        }

        //public void AddRole_ActiveRole(ActiveRole ActiveRole)
        //{
        //    FDIDB.aspnet_Roles.ActiveRole.Add(ActiveRole);
        //}

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="roles"> </param>
        public void Delete(aspnet_Roles roles)
        {
            FDIDB.aspnet_Roles.Remove(roles);
        }
        //public void Deleterole_ActiveRole(ActiveRole activeRole)
        //{
        //    FDIDB.aspnet_Roles.Remove.(role_ActiveRole);
        //}

        /// <summary>
        /// save bản ghi vào DB
        /// </summary>
        public void Save()
        {
            FDIDB.SaveChanges();
        }

    }
}
