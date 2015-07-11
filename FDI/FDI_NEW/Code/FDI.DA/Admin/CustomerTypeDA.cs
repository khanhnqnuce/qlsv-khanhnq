using System.Collections.Generic;
using System.Linq;
using System.Web;
using FDI.Base;
using FDI.Simple;

namespace FDI.DA.Admin
{
    public partial class CustomerTypeDA : BaseDA
    {
        #region Constructer
        public CustomerTypeDA()
        {

        }

        public CustomerTypeDA(string pathPaging)
        {
            PathPaging = pathPaging;
        }

        public CustomerTypeDA(string pathPaging, string pathPagingExt)
        {
            PathPaging = pathPaging;
            PathPagingext = pathPagingExt;
        }
        #endregion

        public List<CustomerTypeItem> GetAll(bool isDelete = false)
        {
            var query = from t in FDIDB.Customer_Type
                        where t.IsDelete == isDelete
                        select new CustomerTypeItem
                                   {
                                       ID = t.ID,
                                       Name = t.Name
                                   };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về kiểu đơn giản, phân trang
        /// </summary>
        /// <param name="httpRequest"> </param>
        /// <param name="delete"> </param>
        /// <returns>Danh sách bản ghi</returns>
        public List<CustomerTypeItem> GetListSimpleByRequest(HttpRequestBase httpRequest, bool delete = false)
        {
            Request = new ParramRequest(httpRequest);
            var query = from t in FDIDB.Customer_Type
                        where t.IsDelete == delete
                        select new CustomerTypeItem
                                   {
                                       ID = t.ID,
                                       Name = t.Name,
                                       IsDeleted = t.IsDelete
                                   };
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<Customer_Type> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Customer_Type where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }


        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="typeID">ID bản ghi</param>
        /// <returns>Bản ghi</returns>
        public Customer_Type GetById(int typeID)
        {
            var query = from c in FDIDB.Customer_Type where c.ID == typeID select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="customerType">bản ghi cần thêm</param>
        public void Add(Customer_Type customerType)
        {
            FDIDB.Customer_Type.Add(customerType);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="customerType"> </param>
        public void Delete(Customer_Type customerType)
        {
            FDIDB.Customer_Type.Remove(customerType);
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
