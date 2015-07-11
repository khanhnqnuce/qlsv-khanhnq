using System.Collections.Generic;
using System.Linq;
using System.Web;
using FDI.Base;
using FDI.Simple;

namespace FDI.DA.Admin
{
    public partial class Customer_CustomerPointDA : BaseDA
    {
        #region Constructer
        public Customer_CustomerPointDA()
        {

        }

        public Customer_CustomerPointDA(string pathPaging)
        {
            PathPaging = pathPaging;
        }

        public Customer_CustomerPointDA(string pathPaging, string pathPagingExt)
        {
            PathPaging = pathPaging;
            PathPagingext = pathPagingExt;
        }
        #endregion

        /// <summary>
        /// Lấy về kiểu đơn giản, phân trang
        /// </summary>
        /// <param name="httpRequest"> </param>
        /// <returns>Danh sách bản ghi</returns>
        public List<CustomerPointItem> GetListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from t in FDIDB.Customer_Point
                        select new CustomerPointItem
                                   {
                                       ID = t.ID,
                                       CustomerId = t.CustomerID,
                                       Point = t.Point,
                                       Note = t.Note,
                                       Customer = new CustomerItem
                                                      {
                                                          Email = t.Customer.Email
                                                      }
                                   };
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }

        /// <summary>
        /// Lấy về kiểu đơn giản, phân trang
        /// </summary>
        /// <param name="httpRequest"> </param>
        /// <param name="customerId"> </param>
        /// <returns>Danh sách bản ghi</returns>
        public List<CustomerPointItem> GetListSimpleByRequestAndCustomer(HttpRequestBase httpRequest, int customerId)
        {
            Request = new ParramRequest(httpRequest);
            var query = from t in FDIDB.Customer_Point
                        where t.CustomerID == customerId
                        select new CustomerPointItem
                                   {
                                       ID = t.ID,
                                       CustomerId = t.CustomerID,
                                       Point = t.Point,
                                       Note = t.Note
                                   };
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<Customer_Point> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Customer_Point where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="customerId">ID của khách hàng</param>
        /// <returns>Bản ghi</returns>
        public Customer_Point GetByCustomerId(int customerId)
        {
            var query = from t in FDIDB.Customer_Point
                        where t.CustomerID == customerId
                        select t;

            return query.FirstOrDefault();
        }


        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="pointId">ID bản ghi</param>
        /// <returns>Bản ghi</returns>
        public Customer_Point GetById(int pointId)
        {
            var query = from c in FDIDB.Customer_Point where c.ID == pointId select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="customerPoint">bản ghi cần thêm</param>
        public void Add(Customer_Point customerPoint)
        {
            FDIDB.Customer_Point.Add(customerPoint);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="customerPoint">Xóa bản ghi</param>
        public void Delete(Customer_Point customerPoint)
        {
            FDIDB.Customer_Point.Remove(customerPoint);
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
