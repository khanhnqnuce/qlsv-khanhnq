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
    public class ActiveRoleDA : BaseDA
    {
        #region Constructer
        public ActiveRoleDA()
        {
        }

        public ActiveRoleDA(string pathPaging)
        {
            PathPaging = pathPaging;
        }

        public ActiveRoleDA(string pathPaging, string pathPagingExt)
        {
            PathPaging = pathPaging;
            PathPagingext = pathPagingExt;
        }
        #endregion

        /// <summary>
        /// Lấy về kiểu đơn giản, phân trang
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public List<ActiveRoleItem> GetListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from c in FDIDB.ActiveRoles                        
                        select new ActiveRoleItem
                                   {
                                       ID = c.Id,
                                       NameActive = c.NameActive,
                                       Ord = c.Ord
                                   };
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }

        public List<ActiveRoleItem> GetByAll()
        {
            var query = from c in FDIDB.ActiveRoles
                        select new ActiveRoleItem
                        {
                            ID = c.Id,
                            NameActive = c.NameActive,
                            Ord = c.Ord
                        };
            return query.ToList();
        }

        public ActiveRole GetById(int id)
        {
            var query = from c in FDIDB.ActiveRoles
                        where c.Id == id
                        select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <returns>Bản ghi</returns>
        public ActiveRoleItem GetRoleItemById(int id)
        {
            var query = from c in FDIDB.ActiveRoles
                        where c.Id == id
                        select new ActiveRoleItem
                        {
                            ID = c.Id,
                            NameActive = c.NameActive,
                            Ord = c.Ord
                        };
            var result = query.FirstOrDefault();
            return result;
        }

        public List<ActiveRoleItem> GetAll()
        {
            var query = from c in FDIDB.ActiveRoles
                        select new ActiveRoleItem
                                   {
                                       ID = c.Id,
                                       NameActive = c.NameActive,
                                       Ord = c.Ord
                                   };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<ActiveRole> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.ActiveRoles where ltsArrID.Contains(c.Id) select c;
            return query.ToList();
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="activeRole">bản ghi cần thêm</param>
        public void Add(ActiveRole activeRole)
        {
            FDIDB.ActiveRoles.Add(activeRole);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        public void Delete(ActiveRole activeRole)
        {
            FDIDB.ActiveRoles.Remove(activeRole);
        }

        /// <summary>
        /// save bản ghi vào DB
        /// </summary>
        public void Save()
        {
            FDIDB.SaveChanges();
        }
    }
}
