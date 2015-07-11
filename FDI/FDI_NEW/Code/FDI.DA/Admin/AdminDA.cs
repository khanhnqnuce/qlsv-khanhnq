using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Base;
using FDI.Simple;

namespace FDI.DA.Admin
{
    public class AdminDA : BaseDA
    {
        #region Constructer
        public AdminDA()
        {
        }

        public AdminDA(string pathPaging)
        {
            this.PathPaging = pathPaging;
        }

        public AdminDA(string pathPaging, string pathPagingExt)
        {
            this.PathPaging = pathPaging;
            this.PathPagingext = pathPagingExt;
        }
        #endregion
        public List<ModuleadminItem> getAllListSimple()
        {
            var query = from c in FDIDB.Modules
                        where c.ID > 1 && c.IsShow == true
                        orderby c.Ord
                        select new ModuleadminItem()
                        {
                            ID = c.ID,
                            NameModule = c.NameModule,
                            Url = c.Tag,
                            ClassCss = c.ClassCss,
                            Ord = c.Ord,
                            PrarentID = c.PrarentID.Value,
                            Content = c.Content,
                            IsShow = c.IsShow != null && c.IsShow.Value,
                            Active = 0
                        };
            return query.ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public aspnet_Roles getUser_Role_ModuleList(string nameRole)
        {
            var query = (from c in FDIDB.aspnet_Roles
                        where c.RoleName == nameRole
                        select c).FirstOrDefault();

            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public aspnet_Users getUser_ModuleById(Guid userId)
        {
            var query = (from c in FDIDB.aspnet_Users
                         where c.UserId == userId
                         select c
                        ).FirstOrDefault();

            return query;
        }
    }
}
