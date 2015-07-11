using System.Collections.Generic;
using System.Web;
using FDI.Base;
using System;
using System.Linq;
using FDI.Simple;

namespace FDI.DA.Admin
{
    public partial class UserDA : BaseDA
    {
        #region User

        public UserDA(string pathPaging)
        {
            PathPaging = pathPaging;
        }

        public UserDA(string pathPaging, string pathPagingExt)
        {
            PathPaging = pathPaging;
            PathPagingext = pathPagingExt;
        }

        #endregion

        public List<ActiveRole> GetActiveRoleAll()
        {
            var query = from c in FDIDB.ActiveRoles
                        select c;
            return query.ToList();
        }

        public List<User_ModuleActive> GetListUserModuleActivekt(Guid id, int moduleid)
        {
            var query = from c in FDIDB.User_ModuleActive
                        where c.UserId == id && c.ModuleId == moduleid
                        select c;
            return query.ToList();
        }

        public string GetUserNameById(Guid id)
        {
            var query = from c in FDIDB.aspnet_Users
                        where c.UserId == id
                        select c.UserName;
            return query.FirstOrDefault();
        }

        public Guid GetCustomerByUserName(string userName)
        {
            var query = from c in FDIDB.aspnet_Users
                        where c.UserName == userName
                        select c.UserId;
            return query.FirstOrDefault();
        }

        public List<UserItem> GetListSimpleByAutoComplete(string keyword, int showLimit)
        {
            var query = from c in FDIDB.aspnet_Users
                        orderby c.UserName
                        where c.UserName.Contains(keyword) && c.aspnet_Roles.Any(m => m.RoleName.ToLower().Equals("agency"))

                        select new UserItem
                        {
                            UserId = c.UserId,
                            UserName = c.UserName
                        };
            return query.Take(showLimit).ToList();
        }

        public List<AspnetUsersItem> GetListUser()
        {
            var query = from c in FDIDB.aspnet_Users
                        where c.aspnet_Membership.IsApproved == true
                        select new AspnetUsersItem
                               {
                                   UserId = c.UserId,
                                   UserName = c.UserName,
                               };
            return query.ToList();
        }

        public List<User_ModuleActive> GetListUserModuleActivekt(Guid id)
        {
            var query = from c in FDIDB.User_ModuleActive
                        where c.UserId == id
                        select c;
            return query.ToList();
        }

        public User_ModuleActive GetByUserModuleActiveId(int id)
        {
            var query = from c in FDIDB.User_ModuleActive
                        where c.ID == id
                        select c;
            return query.FirstOrDefault();
        }

        public List<aspnet_Users> GetListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from c in FDIDB.aspnet_Users
                        where c.aspnet_Roles.Any() && c.Modules.Any()
                        select c;
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }

        public List<aspnet_Users> GetListAll()
        {
            var query = from c in FDIDB.aspnet_Users
                        where c.aspnet_Roles.Any()
                        select c;
            return query.ToList();
        }
        public aspnet_Users GetById(Guid id)
        {
            var query = from c in FDIDB.aspnet_Users
                        where c.UserId == id
                        select c;
            return query.FirstOrDefault();
        }

        public AspnetUsersItem GetByUserId(Guid id)
        {
            var query = from c in FDIDB.aspnet_Users
                        where c.UserId == id
                        select new AspnetUsersItem
                                   {
                                       UserId = id,

                                   };
            return query.FirstOrDefault();
        }

        public void Delete(User_ModuleActive userModuleActive)
        {
            FDIDB.User_ModuleActive.Remove(userModuleActive);
        }

        public void Add(aspnet_Users user)
        {
            FDIDB.aspnet_Users.Add(user);
        }

        public void Save()
        {
            FDIDB.SaveChanges();
        }
    }
}
