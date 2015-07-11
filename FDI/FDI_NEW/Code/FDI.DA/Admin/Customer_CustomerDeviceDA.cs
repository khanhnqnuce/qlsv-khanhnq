using System.Collections.Generic;
using System.Linq;
using System.Web;
using FDI.Base;
using FDI.Simple;

namespace FDI.DA.Admin
{
    public partial class Customer_CustomerDeviceDA : BaseDA
    {
        #region Constructer
        public Customer_CustomerDeviceDA()
        {
        }

        public Customer_CustomerDeviceDA(string pathPaging)
        {
            PathPaging = pathPaging;
        }

        public Customer_CustomerDeviceDA(string pathPaging, string pathPagingExt)
        {
            PathPaging = pathPaging;
            PathPagingext = pathPagingExt;
        }
        #endregion

        /// <summary>
        /// Lấy toàn bộ danh sách bảng ghi
        /// </summary>
        /// <returns></returns>
        public List<Customer_Device> GetAllList()
        {
            var query = from c in FDIDB.Customer_Device orderby c.Name ascending select c;
            return query.ToList();
        }

        /// <summary>
        /// Lấy về kiểu đơn giản, phân trang
        /// </summary>
        /// <param name="httpRequest"> </param>
        /// <returns>Danh sách bản ghi</returns>
        public List<CustomerDeviceItem> GetListSimpleByRequest(HttpRequestBase httpRequest)
        {
            Request = new ParramRequest(httpRequest);
            var query = from t in FDIDB.Customer_Device
                        select new CustomerDeviceItem
                                   {
                            ID = t.ID,
                            Name = t.Name
                        };
            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<Customer_Device> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Customer_Device where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }


        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="deviceID">ID bản ghi</param>
        /// <returns>Bản ghi</returns>
        public Customer_Device GetById(int deviceID)
        {
            var query = from c in FDIDB.Customer_Device where c.ID == deviceID select c;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="deviceID">ID bản ghi</param>
        /// <returns>Bản ghi</returns>
        public CustomerDeviceItem GetDeviceItemById(int deviceID)
        {
            var query = from c in FDIDB.Customer_Device
                        where c.ID == deviceID
                        select new CustomerDeviceItem
                                   {
                            ID = c.ID,
                            Name = c.Name
                        };
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="customerDevice"> </param>
        public void Add(Customer_Device customerDevice)
        {
            FDIDB.Customer_Device.Add(customerDevice);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="customerDevice"> </param>
        public void Delete(Customer_Device customerDevice)
        {
            FDIDB.Customer_Device.Remove(customerDevice);
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
