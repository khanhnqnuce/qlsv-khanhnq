using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Base;
using FDI.DA.EndUser.Reposity;
using FDI.Simple;
using FDI.Utils;

namespace FDI.DA.EndUser.Implementation
{
    public class ShopCategoryImplementation : InitDB, IReposityShopCategory
    {
        #region get data
        public List<ProductItem> GetPageBySPQuery(IQueryable<ProductItem> query, int currentPage, int rowPerPage)
        {
            #region lấy về bảng tạm & danh sách ID

            var ltsProductItem = query.ToList(); //Lấy về tất cả
            var ltsIdSelect = new List<int>();
            if (ltsProductItem.Any())
            {
                TongSoBanGhiSauKhiQuery = ltsProductItem.Count(); // Tổng số lấy về        
                int intBeginFor = (currentPage - 1) * rowPerPage; //index Bản ghi đầu tiên cần lấy trong bảng
                int intEndFor = (currentPage * rowPerPage) - 1; ; //index bản ghi cuối
                intEndFor = (intEndFor > (TongSoBanGhiSauKhiQuery - 1)) ? (TongSoBanGhiSauKhiQuery - 1) : intEndFor; //nếu vượt biên lấy row cuối

                for (int rowIndex = intBeginFor; rowIndex <= intEndFor; rowIndex++)
                {
                    ltsIdSelect.Add(Convert.ToInt32(ltsProductItem[rowIndex].ID));
                }

            }
            else
                TongSoBanGhiSauKhiQuery = 0;
            #endregion
            //Query listItem theo listID
            var iquery = from c in ltsProductItem
                         where ltsIdSelect.Contains(c.ID)
                         select c;

            return iquery.ToList();
        }
        public List<ProductItem> GetPageBySPQuery2(List<ProductItem> query, int currentPage, int rowPerPage)
        {
            #region lấy về bảng tạm & danh sách ID

            var ltsProductItem = query; //Lấy về tất cả
            var ltsIdSelect = new List<int>();
            if (ltsProductItem.Any())
            {
                TongSoBanGhiSauKhiQuery = ltsProductItem.Count(); // Tổng số lấy về        
                int intBeginFor = (currentPage - 1) * rowPerPage; //index Bản ghi đầu tiên cần lấy trong bảng
                int intEndFor = (currentPage * rowPerPage) - 1; ; //index bản ghi cuối
                intEndFor = (intEndFor > (TongSoBanGhiSauKhiQuery - 1)) ? (TongSoBanGhiSauKhiQuery - 1) : intEndFor; //nếu vượt biên lấy row cuối

                for (int rowIndex = intBeginFor; rowIndex <= intEndFor; rowIndex++)
                {
                    ltsIdSelect.Add(Convert.ToInt32(ltsProductItem[rowIndex].ID));
                }

            }
            else
                TongSoBanGhiSauKhiQuery = 0;
            #endregion
            //Query listItem theo listID
            var iquery = from c in ltsProductItem
                         where ltsIdSelect.Contains(c.ID)

                         select c;

            return iquery.ToList();
        }
        public List<ProductCategoryItem> GetList(string lang)
        {
            return new List<ProductCategoryItem>();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductCategoryItem GetProductCategoryItemByID(int id)
        {
            var query = from c in Instance.Shop_Category
                        where c.ID == id
                        select new ProductCategoryItem
                                   {
                                       ID = c.ID,
                                       Name = c.Name,
                                       NameAscii = c.NameAscii,
                                       Description = c.Description,
                                       SEOTitle = c.SEOTitle,
                                       MetaKeyword = c.MetaKeyword,
                                       ParentID =  c.ParentID.Value,
                                       Policy = c.Policy,
                                       listProductItem = c.Shop_Product.Where(o => o.IsDelete == false && o.IsShow == true).OrderBy(m => m.CreatedOnUtc).Select(p => new ProductItem
                                       {
                                           ID = p.ID,
                                           Name = p.Name,
                                           NameAscii = c.NameAscii + "/" + p.NameAscii,
                                           UrlPicture = p.Gallery_Picture.Folder + p.Gallery_Picture.Url,
                                           ShortDescription = p.ShortDescription,
                                           Code = p.Code
                                       }),
                                       TotalItems = c.Shop_Category1.Count
                                   };
            return query.FirstOrDefault();
        }
        public List<ProductCategoryItem> GetAllListSimple(string lang)
        {
          
            var query = from c in Instance.Shop_Category
                        where c.ID >= 1 && c.IsDelete == false && c.IsPublish == true
                        orderby c.Shop_Category1.Count() descending
                        orderby c.Order
                        select new ProductCategoryItem
                                   {
                                       ID = c.ID,
                                       IsPublish = c.IsPublish.Value,
                                       //IsDelete = c.IsDelete.Value,
                                       //MetaDescription = c.MetaDescription,
                                       //MetaKeyword = c.MetaKeyword,
                                       Name = c.Name,
                                       NameAscii = c.NameAscii,
                                       ParentID = c.ParentID.Value,
                                       Description = c.Description,
                                       Order = c.Order,
                                       IsShowInTab = c.IsShowInTab,
                                       IsShowInNavFilter = c.IsShowInNavFilter,
                                       IsShowOnBestSeller = c.IsShowOnBestSeller,
                                       FrtRoundingNumber = c.FrtRoundingNumber,
                                       Rows = c.Rows,
                                       TotalItems = c.Shop_Product.Count(),
                                      
                                       listProductItem = c.Shop_Product.Where(o => o.IsDelete == false && o.IsShow == true).OrderBy(m => m.CreatedOnUtc).Select(p => new ProductItem
                                       {
                                           ID = p.ID,
                                           Name = p.Name,
                                           NameAscii = c.NameAscii + "/" + p.NameAscii,
                                           UrlPicture = p.Gallery_Picture.Folder + p.Gallery_Picture.Url,
                                           ShortDescription = p.ShortDescription,
                                           Code = p.Code,
                                           CreateBy = p.CreateBy
                                           
                                       }),

                                   };
            return query.ToList();
        }
        public ProductCategoryItem GetProductCategoryItemByUrl(string url)
        {
            var query = from c in Instance.Shop_Category
                where c.NameAscii.ToLower().Equals(url.ToLower()) && c.IsDelete == false
                select new ProductCategoryItem
                {
                    ID = c.ID,
                    Name = c.Name,
                    NameAscii = c.NameAscii
                };
            return query.FirstOrDefault();
        }

        //public CustomerItem GetCustomeByIdEmailItem(string email)
        //{
        //    var query = from c in Instance.Customers
        //                where c.Email.ToLower().Equals(email.ToLower()) && c.IsDelete == false
        //                select new CustomerItem
        //                {
        //                    UserName = c.UserName,
        //                    Email = c.Email,
        //                    ID = c.ID,
        //                    Mobile = c.Mobile,
        //                    PeoplesIdentity = c.PeoplesIdentity
        //                };
        //    return query.FirstOrDefault();
        //}
        //public Customer GetCustomeByIdEmail(string email)
        //{
        //    var query = from c in Instance.Customers
        //                where c.Email.ToLower().Equals(email.ToLower()) && c.IsDelete == false
        //                select c;
        //    return query.FirstOrDefault();
        //}

        //public CustomerItem GetCustomerID(int id)
        //{
        //    var query = (from c in Instance.Customers
        //                 where c.ID == id
        //                 select new CustomerItem
        //                 {
        //                     ID = c.ID,
        //                     FirstName = c.FirstName,
        //                     LastName = c.LastName,
        //                     UserName = c.UserName,
        //                     Email = c.Email,
        //                     Gender = c.Gender,
        //                     Birthday = c.Birthday,
        //                     Password = c.Password,
        //                     Mobile = c.Mobile,
        //                     LastIpAddress = c.LastIpAddress,
        //                     MacAddress = c.MacAddress,
        //                     CreatedOnUtc = c.CreatedOnUtc,
        //                     LastLoginDateUtc = c.LastLoginDateUtc,
        //                     LastActivityDateUtc = c.LastActivityDateUtc,
        //                     BillingAddress = c.BillingAddress,
        //                     ShippingAddress = c.ShippingAddress,
        //                     CustomerAvatarId = c.CustomerAvatarID.HasValue ? c.CustomerAvatarID.Value : 0,
        //                     Avatar = new PictureItem
        //                     {
        //                         Name = c.Gallery_Picture.Name,
        //                         Url = c.Gallery_Picture.Url
        //                     },
        //                     PaymentMethodId = c.PaymentMethodID.HasValue ? c.PaymentMethodID.Value : 0,
        //                     PaymentMethod = new CustomerPaymentMethodItem
        //                     {
        //                         Name = c.Payment_Method.Name,
        //                         Descripton = c.Payment_Method.Description
        //                     },
        //                     IsReceiveEmail = c.IsReceiveEmail,
        //                     IsReceiveSms = c.IsReceiveSms,
        //                     CustomerType = new CustomerTypeItem
        //                     {
        //                         Name = c.Customer_Type.Name
        //                     },
        //                     Device = new CustomerDeviceItem
        //                     {
        //                         Name = c.Customer_Device.Name
        //                     }

        //                 }).FirstOrDefault();
        //    return query;
        //}

        //public CustomerItem GetByID(int id)
        //{
        //    var query = (from c in Instance.Customers
        //                 where c.ID == id
        //                 select new CustomerItem
        //                 {
        //                     ID = c.ID,
        //                     FirstName = c.FirstName,
        //                     LastName = c.LastName,
        //                     UserName = c.UserName,
        //                     Email = c.Email,
        //                     Gender = c.Gender,
        //                     Birthday = c.Birthday,
        //                     Password = c.Password,
        //                     Mobile = c.Mobile,
        //                     Address = c.Address,
        //                     LastIpAddress = c.LastIpAddress,
        //                     MacAddress = c.MacAddress,
        //                     CreatedOnUtc = c.CreatedOnUtc,
        //                     LastLoginDateUtc = c.LastLoginDateUtc,
        //                     LastActivityDateUtc = c.LastActivityDateUtc,
        //                     BillingAddress = c.BillingAddress,
        //                     ShippingAddress = c.ShippingAddress,
        //                     CustomerAvatarId = c.CustomerAvatarID.HasValue ? c.CustomerAvatarID.Value : 0,
        //                     Avatar = new PictureItem
        //                     {
        //                         Folder = c.Gallery_Picture.Folder,
        //                         Name = c.Gallery_Picture.Name,
        //                         Url = c.Gallery_Picture.Url
        //                     },
        //                     PaymentMethodId = c.PaymentMethodID.HasValue ? c.PaymentMethodID.Value : 0,
        //                     PaymentMethod = new CustomerPaymentMethodItem
        //                     {
        //                         Name = c.Payment_Method.Name,
        //                         Descripton = c.Payment_Method.Description
        //                     },
        //                     IsReceiveEmail = c.IsReceiveEmail,
        //                     IsReceiveSms = c.IsReceiveSms,
        //                     CustomerType = new CustomerTypeItem
        //                     {
        //                         Name = c.Customer_Type.Name
        //                     },
        //                     Device = new CustomerDeviceItem
        //                     {
        //                         Name = c.Customer_Device.Name
        //                     }

        //                 }).FirstOrDefault();
        //    return query;
        //}


        #endregion

        #region check

        //public int CheckCustomerByPhoneAndEmail(string phone, string email)
        //{
        //    var customer = Instance.Customers.FirstOrDefault(c => c.Mobile.Equals(phone.Trim()) && c.Email.Equals(email.Trim()));
        //    if (customer != null)
        //    {
        //        return customer.ID;
        //    }
        //    return 0;
        //}

        //public int CheckCustomerByPhone(string phone)
        //{
        //    var customer = Instance.Customers.FirstOrDefault(c => c.Mobile.Equals(phone.Trim()));
        //    if (customer != null)
        //    {
        //        return customer.ID;
        //    }
        //    return 0;
        //}

        #endregion
    }
}
