using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Base;
using FDI.DA.EndUser.Reposity;
using FDI.Security;
using FDI.Simple;
using FDI.Utils;

namespace FDI.DA.EndUser.Implementation
{
    public class CustomerImplement : InitDB, IReposityCustomer
    {
        #region get data

        public List<CustomerItem> GetList()
        {
            var query = from c in Instance.Customers
                        where c.IsActive == true && c.IsDelete == false && c.CreatedOnUtc.HasValue
                        select new CustomerItem
                               {
                                   ID = c.ID,
                                   UserName = c.UserName,
                                   Fullname = c.FirstName + " " + c.LastName,
                                   CreatedOnUtc = c.CreatedOnUtc,                                 
                                   Avatar = new PictureItem
                                   {

                                       Folder = c.Gallery_Picture.Folder,
                                       Name = c.Gallery_Picture.Name,
                                       Url = c.Gallery_Picture.Url

                                   },
                                 
                                  
                               };
            return query.ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer GetCustomerByID(int id)
        {
            var query = from c in Instance.Customers
                        where c.ID == id
                        select c;
            return query.FirstOrDefault();
        }
        public bool GetCustomeByEmail(string email)
        {
            var query = from c in Instance.Customers
                        where c.Email.ToLower().Equals(email.ToLower()) && c.IsDelete == false
                        select c;
            return query.Any();
        }

        public CustomerItem GetCustomeByIdEmailItem(string email)
        {
            var query = from c in Instance.Customers
                        where c.Email.ToLower().Equals(email.ToLower()) && c.IsDelete == false
                        select new CustomerItem
                        {
                            UserName = c.UserName,
                            Email = c.Email,
                            ID = c.ID,
                            Mobile = c.Mobile,
                            PeoplesIdentity = c.PeoplesIdentity
                        };
            return query.FirstOrDefault();
        }
        public Customer GetCustomeByIdEmail(string email)
        {
            var query = from c in Instance.Customers
                        where c.Email.ToLower().Equals(email.ToLower()) && c.IsDelete == false
                        select c;
            return query.FirstOrDefault();
        }

        public CustomerItem GetCustomerID(int id)
        {
            var query = (from c in Instance.Customers
                         where c.ID == id
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
                             CustomerAvatarId = c.CustomerAvatarID.HasValue ? c.CustomerAvatarID.Value : 0,
                             Avatar = new PictureItem
                             {
                                 Name = c.Gallery_Picture.Name,
                                 Url = c.Gallery_Picture.Url
                             },
                             PaymentMethodId = c.PaymentMethodID.HasValue ? c.PaymentMethodID.Value : 0,
                           
                             IsReceiveEmail = c.IsReceiveEmail,
                             IsReceiveSms = c.IsReceiveSms,
                             CustomerType = new CustomerTypeItem
                             {
                                 Name = c.Customer_Type.Name
                             },
                             Device = new CustomerDeviceItem
                             {
                                 Name = c.Customer_Device.Name
                             }

                         }).FirstOrDefault();
            return query;
        }

        public CustomerItem GetByID(int id)
        {
            var query = (from c in Instance.Customers
                         where c.ID == id
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
                             Address = c.Address,
                             LastIpAddress = c.LastIpAddress,
                             MacAddress = c.MacAddress,
                             CreatedOnUtc = c.CreatedOnUtc,
                             LastLoginDateUtc = c.LastLoginDateUtc,
                             LastActivityDateUtc = c.LastActivityDateUtc,
                             BillingAddress = c.BillingAddress,
                             ShippingAddress = c.ShippingAddress,
                             CustomerAvatarId = c.CustomerAvatarID.HasValue ? c.CustomerAvatarID.Value : 0,
                           
                             Avatar = new PictureItem
                             {
                                 Folder = c.Gallery_Picture.Folder,
                                 Name = c.Gallery_Picture.Name,
                                 Url = c.Gallery_Picture.Url
                             },
                             PaymentMethodId = c.PaymentMethodID.HasValue ? c.PaymentMethodID.Value : 0,
                           
                             IsReceiveEmail = c.IsReceiveEmail,
                             IsReceiveSms = c.IsReceiveSms,
                             CustomerType = new CustomerTypeItem
                             {
                                 Name = c.Customer_Type.Name
                             },
                             Device = new CustomerDeviceItem
                             {
                                 Name = c.Customer_Device.Name
                             }
                             ,
                         
                            
                         }).FirstOrDefault();
            return query;
        }


        #endregion

        #region check
        public int CheckCustomerByPhoneAndEmail(string phone, string email)
        {
            var customer = Instance.Customers.FirstOrDefault(c => c.Mobile.Equals(phone.Trim()) && c.Email.Equals(email.Trim()));
            if (customer != null)
            {
                return customer.ID;
            }
            return 0;
        }

        public int CheckCustomerByPhone(string phone)
        {
            var customer = Instance.Customers.FirstOrDefault(c => c.Mobile.Equals(phone.Trim()));
            if (customer != null)
            {
                return customer.ID;
            }
            return 0;
        }
        #endregion

        #region nghiatc
        /// <summary>
        /// Get Provider.
        /// </summary>
        ///// <param name="idCustomer"></param>
        ///// <param name="providerName"></param>
        ///// <param name="providerId"></param>
        /// <returns>ExternalProvider</returns>
        //public ExternalProvider GetProvider(int idCustomer, string providerName, string providerId)
        //{
        //    var provider = (from s in Instance.ExternalProviders
        //                    where s.Provider == providerName && s.ProviderID == providerId && s.CustomerId == idCustomer
        //                    select s).FirstOrDefault();
        //    return provider;
        //}

        /// <summary>
        /// Get customer by telephone.
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public List<Customer> GetListCustomerByPhone(string phone)
        {
            var customers = (from c in Instance.Customers
                             where c.Mobile == phone
                             select c).ToList();

            return customers;
        }

      public CustomerItem GetCustomerByPhone(string phone)
        {
            var customers = (from c in Instance.Customers
                             where c.Mobile.Trim() == phone.Trim()
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
                                 CustomerAvatarId = c.CustomerAvatarID.HasValue ? c.CustomerAvatarID.Value : 0,
                                 Avatar = new PictureItem
                                 {
                                     Name = c.Gallery_Picture.Name,
                                     Url = c.Gallery_Picture.Url
                                 },
                                 PaymentMethodId = c.PaymentMethodID.HasValue ? c.PaymentMethodID.Value : 0,
                                
                                 IsReceiveEmail = c.IsReceiveEmail,
                                 IsReceiveSms = c.IsReceiveSms,
                                 CustomerType = new CustomerTypeItem
                                 {
                                     Name = c.Customer_Type.Name
                                 }
                             }).FirstOrDefault();
            return customers;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="keywordEmail"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public bool GetCustomerByEmailMobile(string keywordEmail, out Customer customer)
        {
            var query = (from c in Instance.Customers
                         where c.IsDelete == false && c.Email.ToLower().Equals(keywordEmail.ToLower()) //|| c.Email.Contains(KeywordEmail)
                         select c);
            customer = query.FirstOrDefault();
            return query.Any();
        }

        public bool GetCustomerBymakh(string makh, out Customer customer)
        {
            var query = (from c in Instance.Customers
                         where c.IsDelete == false && c.CustomerIDCRM.ToLower().Equals(makh) //|| c.Email.Contains(KeywordEmail)
                         select c);
            customer = query.FirstOrDefault();
            return query.Any();
        }
        #endregion nghiatc
        #region Insert, Update, Delete

        /// <summary>
        /// Edit by nghiatc2.
        /// 12-05-14.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public int InsertCustomer(Customer customer)
        {
            var password = MyBase.RandomString(6).ToLower();
            var saltKey = CryptDecrypt.CreateSaltKey(5);
            customer.PasswordSalt = saltKey;
            customer.Password = CryptDecrypt.CreatePasswordHash(password, saltKey);
            Instance.Customers.Add(customer);
            Instance.SaveChanges();
            //var adminUtilityDa = new AdminUtilityDA();
            //adminUtilityDa.SendEmailWithTemplate("User.SendInformationAccountAuto", customer.Email, new Dictionary<string, string>
            //                                                                               {
            //                                                                                   {"User.UserName", customer.UserName},
            //                                                                                   {"User.Email", customer.Email},
            //                                                                                   {"User.password", password}
            //                                                                               });
            // Utils.SendEmail(customer.Email, customer.LastName + " " + customer.FirstName, password);
            return customer.ID;
        }

        /// <summary>
        /// DongDT 27/12/2013
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public int AddCustomer(Customer customer)
        {
            Instance.Customers.Add(customer);
            Instance.SaveChanges();
            return customer.ID;
        }

        //public bool InsertPromotionEmail(string name, string email)
        //{
        //    var obj = Instance.Shop_Email_Promotion.FirstOrDefault(o => o.Email == email);
        //    if (obj != null)
        //    {
        //        return false;
        //    }

        //    Instance.Shop_Email_Promotion.Add(new Shop_Email_Promotion()
        //    {
        //        FullName = name,
        //        Email = email,
        //        DateCreate = DateTime.UtcNow,
        //        IsDeleted = false
        //    });

        //    Instance.SaveChanges();

        //    return true;
        //}

        //public bool InsertReportHighPrice(string name, bool gender, decimal price, string phone, string email, string note)
        //{
        //    Instance.System_Report_High_Price.Add(new System_Report_High_Price()
        //    {
        //        Name = name,
        //        Email = email,
        //        DateCreate = DateTime.UtcNow,
        //        CustomerNote = note,
        //        Price = price,
        //        Phone = phone,
        //        Gender = gender,
        //        IsDelete = false,
        //        ContactStatus = false
        //    });

        //    Instance.SaveChanges();

        //    return true;
        //}

        //public bool InsertContact(string name, string phone, string email, string content)
        //{
        //    Instance.Contacts.Add(new Contact()
        //    {
        //        Name = name,
        //        Email = email,
        //        DateCreated = DateTime.UtcNow,
        //        Content = content,
        //        Phone = phone,
        //        Viewed = false
        //    });

        //    Instance.SaveChanges();

        //    return true;
        //}

        public void Save()
        {
            Instance.SaveChanges();
        }

        //public void InsertProvider(int idCustomer, string providerName, string idProviderUser)
        //{
        //    var provider = new ExternalProvider()
        //    {
        //        Provider = providerName,
        //        ProviderID = idProviderUser,
        //        CustomerId = idCustomer
        //    };
        //    Instance.ExternalProviders.Add(provider);
        //    Instance.SaveChanges();
        //}

        #endregion
    }
}
