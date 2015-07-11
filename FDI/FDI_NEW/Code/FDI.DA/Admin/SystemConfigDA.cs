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
    public class SystemConfigDA : BaseDA
    {
        #region Constructer
        public SystemConfigDA()
        {
        }

        public SystemConfigDA(string pathPaging)
        {
            PathPaging = pathPaging;
        }

        public SystemConfigDA(string pathPaging, string pathPagingExt)
        {
            PathPaging = pathPaging;
            PathPagingext = pathPagingExt;
        }
        #endregion

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public List<SystemConfigItem> GetListSimpleAll()
        {
            var query = from c in FDIDB.System_Config
                        orderby c.Name
                        select new SystemConfigItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name.Trim()
                                   };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <param name="isShow">Kiểm tra hiển thị</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<SystemConfigItem> GetListSimpleAll(bool isShow)
        {
            var query = from c in FDIDB.System_Config
                        where (c.IsShow == isShow)
                        orderby c.Name
                        select new SystemConfigItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name.Trim()
                                   };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về dưới dạng Autocomplete
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="showLimit"></param>
        /// <returns></returns>
        public List<SystemConfigItem> GetListSimpleByAutoComplete(string keyword, int showLimit)
        {
            var query = from c in FDIDB.System_Config
                        orderby c.Name
                        where c.Name.StartsWith(keyword)
                        select new SystemConfigItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name.Trim()
                                   };
            return query.Take(showLimit).ToList();
        }

        /// <summary>
        /// Lấy về dưới dạng Autocomplete
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="showLimit"></param>
        /// <param name="isShow"> </param>
        /// <returns></returns>
        public List<SystemConfigItem> GetListSimpleByAutoComplete(string keyword, int showLimit, bool isShow)
        {
            var query = from c in FDIDB.System_Config
                        orderby c.Name
                        where c.IsShow == isShow
                        && c.Name.StartsWith(keyword)
                        select new SystemConfigItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name.Trim()
                                   };
            return query.Take(showLimit).ToList();
        }

        /// <summary>
        /// Lấy về kiểu đơn giản, phân trang
        /// </summary>
        /// <param name="httpRequest"> </param>
        /// <returns>Danh sách bản ghi</returns>
        public List<SystemConfigItem> GetListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from o in FDIDB.System_Config
                        select new SystemConfigItem
                                   {
                                       ID = o.ID,
                                       Name = o.Name.Trim(),
                                       IsShow = o.IsShow,
                                       Email = o.Email,
                                       Address = o.Address

                                   };
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }

        /// <summary>
        /// Lấy về mảng đơn giản qua mảng ID
        /// </summary>
        /// <param name="ltsArrID"></param>
        /// <returns></returns>
        public List<SystemConfigItem> GetListSimpleByArrID(List<int> ltsArrID)
        {
            var query = from o in FDIDB.System_Config
                        where ltsArrID.Contains(o.ID)
                        orderby o.ID descending
                        select new SystemConfigItem
                                   {
                                       ID = o.ID,
                                       Name = o.Name.Trim(),
                                       IsShow = o.IsShow,
                                       Email = o.Email,
                                       Address = o.Address,
                                       EmailSend = o.EmailSend,
                                       EmailSendPwd = o.EmailSendPwd,
                                       EmailReceive = o.EmailReceive,
                                   };
            TotalRecord = query.Count();
            return query.ToList();
        }

        /// <summary>
        /// Lấy về mảng đơn giản qua mảng ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SystemConfigItem GetSystemConfigItemById(int id)
        {
            var query = from o in FDIDB.System_Config
                        where o.ID == id
                        orderby o.ID descending
                        select new SystemConfigItem
                                   {
                                       ID = o.ID,
                                       Name = o.Name.Trim(),
                                       IsShow = o.IsShow,
                                       Email = o.Email,
                                       Address = o.Address,
                                       EmailSend = o.EmailSend,
                                       EmailSendPwd = o.EmailSendPwd,
                                       EmailReceive = o.EmailReceive,
                                   };
            return query.FirstOrDefault();
        }

        #region Check Exits, Add, Update, Delete
        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="id">ID bản ghi</param>
        /// <returns>Bản ghi</returns>
        public System_Config GetById(int id)
        {
            var query = from c in FDIDB.System_Config where c.ID == id select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<System_Config> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.System_Config where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Kiểm tra bản ghi đã tồn tại hay chưa
        /// </summary>
        /// <param name="systemConfig">Đối tượng kiểm tra</param>
        /// <returns>Trạng thái tồn tại</returns>
        public bool CheckExits(System_Config systemConfig)
        {
            var query = from c in FDIDB.System_Config where ((c.Name == systemConfig.Name) && (c.ID != systemConfig.ID)) select c;
            return query.Any();
        }

        /// <summary>
        /// Lấy về bản ghi qua tên
        /// </summary>
        /// <param name="name">Tên bản ghi</param>
        /// <returns>Bản ghi</returns>
        public System_Config GetByName(string name)
        {
            var query = from c in FDIDB.System_Config where ((c.Name == name)) select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="systemConfig"> bản ghi cần thêm</param>
        public void Add(System_Config systemConfig)
        {
            FDIDB.System_Config.Add(systemConfig);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="systemConfig">Xóa bản ghi</param>
        public void Delete(System_Config systemConfig)
        {
            FDIDB.System_Config.Remove(systemConfig);
        }

        /// <summary>
        /// save bản ghi vào DB
        /// </summary>
        public void Save()
        {
            FDIDB.SaveChanges();
        }
        #endregion
    }
}
