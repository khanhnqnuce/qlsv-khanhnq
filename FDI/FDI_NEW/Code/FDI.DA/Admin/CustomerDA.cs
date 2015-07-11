using System.Text;
using FDI.Base;
using FDI.Simple;
using FDI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FDI.DA.Admin
{
    public partial class CustomerDA : BaseDA
    {
        private readonly System_CountryDA _systemCountryDA;
        private readonly System_CityDA _systemCityDA;
        private readonly System_DistrictDA _systemDistrictDA;


        #region Constructer
        public CustomerDA()
        {
            _systemCountryDA = new System_CountryDA("#");
            _systemCityDA = new System_CityDA("#");
            _systemDistrictDA = new System_DistrictDA("#");

        }

        public CustomerDA(string pathPaging)
        {
            PathPaging = pathPaging;
            _systemCountryDA = new System_CountryDA("#");
            _systemCityDA = new System_CityDA("#");
            _systemDistrictDA = new System_DistrictDA("#");

        }

        public CustomerDA(string pathPaging, string pathPagingExt)
        {
            PathPaging = pathPaging;
            PathPagingext = pathPagingExt;
            _systemCountryDA = new System_CountryDA("#");
            _systemCityDA = new System_CityDA("#");
            _systemDistrictDA = new System_DistrictDA("#");

        }
        #endregion




        /// <summary>
        /// get all list customer with delete dafault is false
        /// </summary>
        /// <param name="delete"></param>
        /// <returns></returns>
        public List<CustomerItem> GetAll(bool delete = false)
        {
            var query = from c in FDIDB.Customers
                        where c.IsDelete == delete
                        select new CustomerItem
                                   {
                                       ID = c.ID,
                                       FirstName = c.FirstName,
                                       LastName = c.LastName,
                                       Fullname = c.FirstName + " " + c.LastName,
                                       UserName = c.UserName,
                                       Email = c.Email,
                                   };
            return query.Take(30).ToList();
        }

   
        public IEnumerable<CustomerItem> GetCustomerByEmail(string keyword, int showlimit)
        {
            var query = from c in FDIDB.Customers
                        where c.IsDelete == false && c.Email.Contains(keyword)
                        select new CustomerItem
                                   {
                                       ID = c.ID,
                                       FirstName = c.FirstName,
                                       LastName = c.LastName,
                                       Fullname = c.FirstName + " " + c.LastName,
                                       UserName = c.UserName,
                                       Email = c.Email
                                   };
            return query.ToList().Take(showlimit);
        }

        public bool GetCustomerByEmailMobile(string keywordEmail, out Customer customer)
        {
            var query = (from c in FDIDB.Customers
                         where c.IsDelete == false && c.Email.ToLower().Equals(keywordEmail.ToLower()) //|| c.Email.Contains(KeywordEmail)
                         select c);
            customer = query.FirstOrDefault();
            return query.Any();
        }

        public bool GetBoolByEmail(string keywordEmail)
        {
            var query = (from c in FDIDB.Customers
                         where c.IsDelete == false && c.Email.ToLower().Equals(keywordEmail.ToLower()) //|| c.Email.Contains(KeywordEmail)
                         select c).ToList();
            return query.Any();
        }

        public bool GetBoolByMakh(string makh)
        {
            var query = (from c in FDIDB.Customers
                         where c.IsDelete == false && c.CustomerIDCRM.ToLower().Equals(makh) //|| c.Email.Contains(KeywordEmail)
                         select c).ToList();
            return query.Any();
        }

        public bool GetCustomerBymakh(string makh, out Customer customer)
        {
            var query = (from c in FDIDB.Customers
                         where c.IsDelete == false && c.CustomerIDCRM.ToLower().Equals(makh) //|| c.Email.Contains(KeywordEmail)
                         select c);
            customer = query.FirstOrDefault();
            return query.Any();
        }

        public Customer GetCustomerByMobile(string keywordMobile)
        {
            var query = (from c in FDIDB.Customers
                         where c.IsDelete == false && c.Mobile.ToLower().Equals(keywordMobile) //|| c.Email.Contains(KeywordEmail)
                         select c).FirstOrDefault();
            return query;
        }

     
        public bool CheckUserName(string username)
        {
            var query = from c in FDIDB.Customers
                        where c.UserName.ToLower().Equals(username.ToLower())
                        select c;
            return query.Any();

        }

        /// <summary>
        /// Lấy về kiểu đơn giản, phân trang
        /// </summary>
        /// <param name="httpRequest"> </param>
        /// <param name="delete"> </param>
        /// <param name="isActive"></param>
        /// <returns>Danh sách bản ghi</returns>
        public List<CustomerItem> GetListSimpleByRequest(HttpRequestBase httpRequest, bool isActive)
        {
            Request = new ParramRequest(httpRequest);
            var query = from c in FDIDB.Customers
                        where c.IsDelete == false && c.IsActive == isActive
                        orderby c.ID descending
                        select new CustomerItem
                        {
                            ID = c.ID,
                            Fullname = c.FirstName + " " + c.LastName,
                            Mobile = c.Mobile,
                            UserName = c.UserName,
                            Email = c.Email,
                            CustomerTypeID = c.CustomerTypeID,
                            CustomerType = new CustomerTypeItem
                            {
                                Name = c.Customer_Type.Name
                            },

                           
                            CreatedOnUtc = c.CreatedOnUtc,
                            LastLoginDateUtc = c.LastLoginDateUtc
                        };

            try
            {
                if (!string.IsNullOrEmpty(httpRequest["fromCreateDate"]))
                {
                    var formDate = Convert.ToDateTime(httpRequest["fromCreateDate"]);
                    query = query.Where(c => c.CreatedOnUtc >= formDate);
                }

                if (!string.IsNullOrEmpty(httpRequest["toCreateDate"]))
                {
                    var toDate = Convert.ToDateTime(httpRequest["toCreateDate"]);
                    query = query.Where(c => c.CreatedOnUtc <= toDate);
                }

                if (!string.IsNullOrEmpty(httpRequest["typeCustomer"]))
                {
                    var type = Int32.Parse(httpRequest["typeCustomer"]);
                    query = query.Where(c => c.CustomerTypeID == type);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Instance.LogError(GetType(), ex);
            }

            query = query.SelectByRequest(Request, ref TotalRecord);
            return query.ToList();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<Customer> GetListByArrID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Customers where ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }
        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<Customer> GetListNotEqByLtsID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Customers where !ltsArrID.Contains(c.ID) select c;
            return query.ToList();
        }

        /// <summary>
        /// Lấy về danh sách qua mảng id
        /// </summary>
        /// <param name="ltsArrID">Mảng ID</param>
        /// <returns>Danh sách bản ghi</returns>
        public List<CustomerItem> GetListByLtsID(List<int> ltsArrID)
        {
            var query = from c in FDIDB.Customers
                        where !ltsArrID.Contains(c.ID)
                        select new CustomerItem()
                               {

                               };
            return query.ToList();
        }

        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="customerId">ID bản ghi</param>
        /// <returns>Bản ghi</returns>
        public Customer GetById(int customerId)
        {
            var query = from c in FDIDB.Customers where c.ID == customerId select c;
            return query.FirstOrDefault();
        }

        public bool? GetActiveById(int customerId)
        {
            var query = from c in FDIDB.Customers where c.ID == customerId select c.IsActive;
            return query.FirstOrDefault();
        }

        public string GetUserNameById(int customerId)
        {
            var query = from c in FDIDB.Customers where c.ID == customerId select c.UserName;
            return query.FirstOrDefault();
        }

        public Customer GetByUserName(string username)
        {
            var query = from c in FDIDB.Customers where c.UserName.ToLower() == username.ToLower() select c;
            return query.FirstOrDefault();
        }



        public string GetUserById(int id)
        {
            var query = from c in FDIDB.Customers where c.ID == id select c.UserName;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Lấy về bản ghi qua khóa chính
        /// </summary>
        /// <param name="email"> </param>
        /// <returns>Bản ghi</returns>
        public CustomerItem GetCustomerItemByEmail(string email)
        {
            var query = from c in FDIDB.Customers
                        where c.Email.Contains(email)
                        select new CustomerItem
                                   {
                                       ID = c.ID,
                                       FirstName = c.FirstName,
                                       LastName = c.LastName,
                                       UserName = c.UserName,
                                       Email = c.Email,
                                       Gender = c.Gender,
                                       Birthday = c.Birthday,
                                       Password = c.Password,
                                       Mobile = c.Mobile,
                                       LastIpAddress = c.LastIpAddress,
                                       MacAddress = c.MacAddress,
                                       CreatedOnUtc = c.CreatedOnUtc,
                                       LastLoginDateUtc = c.LastLoginDateUtc,
                                       LastActivityDateUtc = c.LastActivityDateUtc,
                                       BillingAddress = c.BillingAddress,
                                       ShippingAddress = c.ShippingAddress,
                                       CustomerAvatarId = c.CustomerAvatarID,
                                       Avatar = new PictureItem
                                                    {
                                                        Name = c.Gallery_Picture.Name,
                                                        Url = c.Gallery_Picture.Url
                                                    },
                                       PaymentMethodId = c.PaymentMethodID,

                                       DistrictId = c.DistrictID,
                                       CityId = c.CityID,
                                       CountryId = c.CountryID,
                                       IsActive = c.IsActive,
                                       IsDelete = c.IsDelete,
                                       IsReceiveEmail = c.IsReceiveEmail,
                                       IsReceiveSms = c.IsReceiveSms,
                                       CustomerTypeID = c.CustomerTypeID,
                                       CustomerType = new CustomerTypeItem
                                                          {
                                                              Name = c.Customer_Type.Name
                                                          },
                                       DeviceId = c.DeviceID,
                                       Device = new CustomerDeviceItem
                                                    {
                                                        Name = c.Customer_Device.Name
                                                    },
                                   };

            var result = query.FirstOrDefault();

            if (result != null && result.CountryId.HasValue)
                result.Country = _systemCountryDA.GetCountryItemById(result.CountryId.Value);
            if (result != null && result.CityId.HasValue)
                result.City = _systemCityDA.GetCityItemById(result.CityId.Value);
            if (result != null && result.DistrictId.HasValue)
                result.District = _systemDistrictDA.GetDistrictItemById(result.DistrictId.Value);
            return result;
        }

        public bool CheckExitsByUserName(string username, int customer)
        {
            var query = from c in FDIDB.Customers
                        where c.UserName.Equals(username) && c.IsDelete == false && c.ID != customer
                        select c.ID;
            return query.Any();
        }


        

        public int GetIdByUserName(string username)
        {
            var query = from c in FDIDB.Customers
                        where c.UserName.Equals(username) && c.IsDelete == false
                        select c.ID;
            return query.Any() ? query.FirstOrDefault() : 0;

        }

        public bool CheckExitsByUserName(string username)
        {
            var query = from c in FDIDB.Customers
                        where c.PeoplesIdentity.Equals(username) && c.IsDelete == false
                        select c.ID;
            return query.Any();

        }
        public bool CheckExitsByEmail(string email, int customer)
        {
            var query = from c in FDIDB.Customers
                        where c.Email.Equals(email) && c.IsDelete == false && c.ID != customer
                        select c;
            return query.Any();
        }
        public bool CheckExitsByMobile(string mobile, int customerID)
        {
            var query = from c in FDIDB.Customers
                        where c.Mobile.Equals(mobile) && c.IsDelete == false && c.ID != customerID
                        select c;
            return query.Any();
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="customer"> </param>
        public void Add(Customer customer)
        {
            FDIDB.Customers.Add(customer);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        public void Delete(Customer customer)
        {
            customer.IsDelete = true;
            FDIDB.Customers.Attach(customer);
            var entry = FDIDB.Entry(customer);
            entry.Property(e => e.IsDelete).IsModified = true;
            // DB.Customers.Remove(customer);
        }

        /// <summary>
        /// save bản ghi vào DB
        /// </summary>
        public void Save()
        {

            FDIDB.SaveChanges();
        }


        /// <summary>
        /// Lấy về Autocomplete
        /// </summary>
        /// <param name="values"></param>
        /// <param name="ltsIdAdded"> </param>
        /// <param name="numberLimit"></param>
        /// <returns></returns>
        public List<CustomerItem> GetAutocompleteData(string values, List<int> ltsIdAdded, int numberLimit)
        {

            var query = from c in FDIDB.Customers

                        select new CustomerItem
                                   {
                                       Email = c.Email,
                                       ID = c.ID
                                   };

            var result = query.ToList();


            return result;
        }

        public List<CustomerItem> GetCustomerIsAdmin()
        {
            var query = from c in FDIDB.Customers
                        where c.IsAdmin == true
                        select new CustomerItem
                        {
                            Email = c.Email,
                            ID = c.ID
                        };
            return query.ToList();
        }
    
        #region Đệ quy lấy cấp cha

        public List<CustomerItem> GetListLevelBy(List<CustomerItem> ltsSource, int id)
        {

            var ltsConvert = new List<CustomerItem>();
            BuildTreeGetListLevelBy(ltsSource, id, 1, ref ltsConvert);
            return ltsConvert;
        }

        /// <summary>
        /// Build cây đệ quy
        /// </summary>
        /// <param name="ltsItems"></param>
        /// <param name="rootID"> </param>
        /// <param name="space"></param>
        /// <param name="idRemove"> </param>
        /// <param name="ltsConvert"></param>
        private void BuildTreeGetListLevelBy(List<CustomerItem> ltsItems, int rootID, int idRemove, ref List<CustomerItem> ltsConvert)
        {

            var ltsChils = ltsItems.Where(o => o.ID == rootID && o.ID != idRemove).ToList();
            foreach (var currentItem in ltsChils)
            {
                ltsConvert.Add(currentItem);
                BuildTreeGetListLevelBy(ltsItems, currentItem.ParentID.Value, idRemove, ref ltsConvert);
            }
        }
        #endregion

    }
}
