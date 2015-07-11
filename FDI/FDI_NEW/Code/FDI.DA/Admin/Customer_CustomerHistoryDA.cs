using System.Collections.Generic;
using System.Linq;
using System.Web;
using FDI.Base;
using FDI.Simple;

namespace FDI.DA.Admin
{
    public partial class Customer_CustomerHistoryDA : BaseDA
    {
        #region Constructer
        public Customer_CustomerHistoryDA()
        {

        }

        public Customer_CustomerHistoryDA(string pathPaging)
        {
            PathPaging = pathPaging;
        }

        public Customer_CustomerHistoryDA(string pathPaging, string pathPagingExt)
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
        public List<CustomerHistoryItem> GetListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from t in FDIDB.Customer_History
                        select new CustomerHistoryItem
                                   {
                            ID = t.ID,
                            CreatedOnUtc = t.CreatedOnUtc,
                            CustomerActionID = t.CustomerActionID,
                            CustomerID = t.CustomerID,
                            CustomerLink = t.CustomerLink,
                            Customer = new CustomerItem
                                           {
                                ID = t.Customer.ID,
                                Fullname = t.Customer.FirstName + t.Customer.LastName
                            },
                            CustomerAction = new CustomerActionItem
                                                 {
                                ID = t.Customer_Action.ID,
                                Name = t.Customer_Action.Name,
                                Point = t.Customer_Action.Point
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
        public List<CustomerHistoryItem> GetListSimpleByRequestAndCustomer(HttpRequestBase httpRequest, int customerId)
        {
            Request = new ParramRequest(httpRequest);
            var query = from t in FDIDB.Customer_History
                        where t.CustomerID == customerId
                        orderby t.CreatedOnUtc descending
                        select new CustomerHistoryItem
                                   {
                            ID = t.ID,
                            CreatedOnUtc = t.CreatedOnUtc,
                            CustomerActionID = t.CustomerActionID,
                            CustomerID = t.CustomerID,
                            CustomerLink = t.CustomerLink,
                            Customer = new CustomerItem
                                           {
                                ID = t.Customer.ID,
                                Fullname = t.Customer.FirstName + t.Customer.LastName
                            },
                            CustomerAction = new CustomerActionItem
                                                 {
                                ID = t.Customer_Action.ID,
                                Name = t.Customer_Action.Name,
                                Point = t.Customer_Action.Point
                            }
                        };
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<Customer_History> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Customer_History where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="customerId">ID của khách hàng</param>
        /// <returns>Bản ghi</returns>
        public List<CustomerHistoryItem> GetByCustomerId(int customerId)
        {
            var query = from t in FDIDB.Customer_History
                        where t.CustomerID == customerId
                        orderby t.CreatedOnUtc descending
                        select new CustomerHistoryItem
                                   {
                            ID = t.ID,
                            CreatedOnUtc = t.CreatedOnUtc,
                            CustomerActionID = t.CustomerActionID,
                            CustomerID = t.CustomerID,
                            CustomerLink = t.CustomerLink,
                            Customer = new CustomerItem
                                           {
                                ID = t.Customer.ID,
                                Fullname = t.Customer.FirstName + t.Customer.LastName
                            },
                            CustomerAction = new CustomerActionItem
                                                 {
                                ID = t.Customer_Action.ID,
                                Name = t.Customer_Action.Name,
                                Point = t.Customer_Action.Point
                            }
                        };

            return query.ToList();
        }


        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="historyId">ID bản ghi</param>
        /// <returns>Bản ghi</returns>
        public Customer_History GetById(int historyId)
        {
            var query = from c in FDIDB.Customer_History where c.ID == historyId select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="customerHistory"> </param>
        public void Add(Customer_History customerHistory)
        {
            FDIDB.Customer_History.Add(customerHistory);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="customerHistory"> </param>
        public void Delete(Customer_History customerHistory)
        {
            FDIDB.Customer_History.Remove(customerHistory);
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
