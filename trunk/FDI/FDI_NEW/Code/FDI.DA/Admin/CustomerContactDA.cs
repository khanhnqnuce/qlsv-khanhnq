using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Base;
using FDI.Simple;
using System.Web;
using FDI.Utils;

namespace FDI.DA.Admin
{
    public partial class CustomerContactDA:BaseDA
    {
        #region Constructer
        public CustomerContactDA()
        {
        }

        public CustomerContactDA(string pathPaging)
        {
            PathPaging = pathPaging;
        }

        public CustomerContactDA(string pathPaging, string pathPagingExt)
        {
            PathPaging = pathPaging;
            PathPagingext = pathPagingExt;
        }
        #endregion

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public List<CustomerContactItem> GetAllListSimple()
        {
            var query = from c in FDIDB.CustomerContacts
                        where !c.IsDelete.Value
                        orderby c.Id descending

                        select new CustomerContactItem
                        {
                            ID = c.Id,
                            Name=c.Name,
                            Email = c.Email,
                            Subject = c.Subject,
                            Message = c.Message,
                            IsShow = c.IsShow.Value,
                            IsDelete = c.IsDelete.Value,
                            Status = c.Status,
                            CreatedOnUtc = c.CreatedOnUtc,
                            Phone = c.Phone,
                            TypeContact=c.TypeContact,
                        };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về tất cả kiểu đơn giản
        /// </summary>
        /// <param name="isDelete">Kiểm tra trạng thái</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<CustomerContactItem> GetListSimpleAll(bool isDelete)
        {
            var query = from c in FDIDB.CustomerContacts
                        where (c.IsDelete == isDelete) && !c.IsDelete.Value
                        orderby c.Id descending
                        select new CustomerContactItem
                        {
                            ID = c.Id,
                            Name=c.Name,
                            Email = c.Email,
                            Subject = c.Subject,
                            Message = c.Message,
                            IsShow = c.IsShow.Value,
                            IsDelete = c.IsDelete.Value,
                            Status = c.Status,
                            CreatedOnUtc = c.CreatedOnUtc,
                            Phone = c.Phone,
                            TypeContact = c.TypeContact,
                        };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về dưới dạng Autocomplete
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="showLimit"></param>
        /// <returns></returns>
        public List<CustomerContactItem> GetListSimpleByAutoComplete(string keyword, int showLimit)
        {
            var query = from c in FDIDB.CustomerContacts

                        where c.Name.StartsWith(keyword) && !c.IsDelete.Value
                        orderby c.Id descending
                        select new CustomerContactItem
                        {
                            ID = c.Id,
                            Name=c.Name,
                            Email = c.Email,
                            Subject=c.Subject,
                            Message=c.Message,
                            IsShow=c.IsShow.Value,
                            IsDelete=c.IsDelete.Value,
                            Status = c.Status,
                            CreatedOnUtc = c.CreatedOnUtc,
                            Phone = c.Phone,
                            TypeContact = c.TypeContact,
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
        public List<CustomerContactItem> GetListSimpleByAutoComplete(string keyword, int showLimit, bool isShow)
        {
            var query = from c in FDIDB.CustomerContacts
                        orderby c.Id descending
                        where c.IsShow == isShow
                        && c.Name.StartsWith(keyword) && !c.IsDelete.Value

                        select new CustomerContactItem
                        {
                            ID = c.Id,
                            Name = c.Name,
                            Email = c.Email,
                            Subject = c.Subject,
                            Message = c.Message,
                            IsShow = c.IsShow.Value,
                            IsDelete = c.IsDelete.Value,
                            Status = c.Status,
                            CreatedOnUtc = c.CreatedOnUtc,
                            Phone = c.Phone,
                            TypeContact = c.TypeContact,

                        };
            return query.Take(showLimit).ToList();
        }

        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="typeID">ID bản ghi</param>
        /// <returns>Bản ghi</returns>
        public CustomerContact GetById(int id)
        {
            var query = from c in FDIDB.CustomerContacts where c.Id == id select c;
            return query.FirstOrDefault();
        }

        public CustomerContact GetByEmail(string email)
        {
            var query = from c in FDIDB.CustomerContacts where c.Email == email && c.IsDelete == false select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<CustomerContact> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.CustomerContacts where ltsArrID.Contains(c.Id) select c;
            return query.ToList();
        }

        /// <summary>
        /// Lấy về kiểu đơn giản, phân trang
        /// </summary>
        /// <param name="httpRequest"></param>        
        /// <returns></returns>
        public List<CustomerContactItem> GetListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from t in FDIDB.CustomerContacts
                        where t.IsDelete == false
                        select new CustomerContactItem
                        {

                            ID = t.Id,
                            Name = t.Name,
                            Email=t.Email,
                            Subject=t.Subject,
                            Message=t.Message,
                            IsShow=t.IsShow.Value,
                            IsDelete=t.IsDelete.Value,
                            Status = t.Status,
                            CreatedOnUtc = t.CreatedOnUtc,
                            Phone = t.Phone,
                            TypeContact = t.TypeContact,
                        };
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }

        #region Add, Delete, Save
        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="customerContact">bản ghi cần thêm</param>
        public void Add(CustomerContact customerContact)
        {
            FDIDB.CustomerContacts.Add(customerContact);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="customerContact">Xóa bản ghi</param>
        public void Delete(CustomerContact customerContact)
        {
            FDIDB.CustomerContacts.Remove(customerContact);
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
