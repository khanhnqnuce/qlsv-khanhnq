using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Base;
using FDI.Business.Facade;
using FDI.Business.Reposity;
using FDI.Simple;
using FDI.Utils;

namespace FDI.Business.Implementation.Proxy
{
    public class CustomerCacheProxy : CustomerFacade, IProxyCustomer
    {
        #region fields
        private static readonly Lazy<CustomerCacheProxy> lazy = new Lazy<CustomerCacheProxy>(() => new CustomerCacheProxy());
        readonly CustomerProxy customerProxy = CustomerProxy.GetInstance;
        #endregion

        #region properties
        public static CustomerCacheProxy GetInstance { get { return lazy.Value; } }

        #endregion

        #region constructor
        private CustomerCacheProxy()
        {
        }

        #endregion
        public List<CustomerItem> GetList()
        {
            const string param = "CustomerCacheProxyCustomerCacheProxyGetList";
            if (cache.KeyExistsCache(param))
            {
                var lst = (List<CustomerItem>)cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    cache.DeleteCache(param);
                    // get new
                    var retval = customerProxy.GetList();
                    // insert new cache
                    cache.Set(param, retval, ConfigCache.TimeExpire);
                    return retval;
                }
                return lst;
            }
            else
            {
                var lst = customerProxy.GetList();
                // insert new cache
                cache.Set(param, lst, ConfigCache.TimeExpire);
                return lst;
            }
        }

        public int CheckCustomerByPhoneAndEmail(string phone, string email)
        {
            return 0;
        }

        public int CheckCustomerByPhone(string phone)
        {
            return 0;
        }

        public int InsertCustomer(Customer customer)
        {
            return 0;
        }

        public int AddCustomer(Customer customer)
        {
            return 0;
        }

        public void Save()
        {

        }

        public bool InsertPromotionEmail(string name, string email)
        {
            return true;
        }

    
        public CustomerItem GetByID(int id)
        {
            var param = string.Format("CustomerCacheProxyGetByID{0}", id);
            if (cache.KeyExistsCache(param))
            {
                var lst = (CustomerItem)cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    cache.DeleteCache(param);
                    // get new
                    var retval = customerProxy.GetByID(id);
                    // insert new cache
                    cache.Set(param, retval, ConfigCache.TimeExpire);
                    return retval;
                }
                return lst;
            }
            else
            {
                var lst = customerProxy.GetByID(id);
                // insert new cache
                cache.Set(param, lst, ConfigCache.TimeExpire);
                return lst;
            }
            //throw new NotImplementedException();
        }

        public Customer GetCustomerByID(int Id)
        {
            var param = string.Format("CustomerCacheProxyGetCustomerByID{0}", Id);
            if (cache.KeyExistsCache(param))
            {
                var lst = (Customer)cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    cache.DeleteCache(param);
                    // get new
                    var retval = customerProxy.GetCustomerByID(Id);
                    // insert new cache
                    cache.Set(param, retval, ConfigCache.TimeExpire);
                    return retval;
                }
                return lst;
            }
            else
            {
                var lst = customerProxy.GetCustomerByID(Id);
                // insert new cache
                cache.Set(param, lst, ConfigCache.TimeExpire);
                return lst;
            }
        }

        public bool GetCustomeByEmail(string email)
        {
            var param = string.Format("CustomerCacheProxyCustomerEmail{0}", email.Substring(0, email.IndexOf('@') - 1));
            if (cache.KeyExistsCache(param))
            {
                var lst = (bool)cache.GetCache(param);
                if (!lst)
                {
                    // delete old cache
                    cache.DeleteCache(param);
                    // get new
                    var retval = customerProxy.GetCustomeByEmail(email);
                    // insert new cache
                    cache.Set(param, retval, ConfigCache.TimeExpire);
                    return retval;
                }
                return true;
            }
            else
            {
                var lst = customerProxy.GetCustomeByEmail(email);
                // insert new cache
                cache.Set(param, lst, ConfigCache.TimeExpire);
                return lst;
            }
        }

        public Customer GetCustomeByIdEmail(string Email)
        {
            var param = string.Format("CustomerCacheProxyCustomersIdEmail{0}", Email.Substring(0, Email.IndexOf('@') - 1));
            if (cache.KeyExistsCache(param))
            {
                var lst = (Customer)cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    cache.DeleteCache(param);
                    // get new
                    var retval = customerProxy.GetCustomeByIdEmail(Email);
                    // insert new cache
                    cache.Set(param, retval, ConfigCache.TimeExpire);
                    return retval;
                }
                return lst;
            }
            else
            {
                var lst = customerProxy.GetCustomeByIdEmail(Email);
                // insert new cache
                cache.Set(param, lst, ConfigCache.TimeExpire);
                return lst;
            }
        }


        public CustomerItem GetCustomeByIdEmailItem(string Email)
        {
            var param = string.Format("CustomerCacheProxyCustomerIdEmail{0}", Email.Substring(0, Email.IndexOf('@')));
            if (cache.KeyExistsCache(param))
            {
                var lst = (CustomerItem)cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    cache.DeleteCache(param);
                    // get new
                    var retval = customerProxy.GetCustomeByIdEmailItem(Email);
                    // insert new cache
                    cache.Set(param, retval, ConfigCache.TimeExpire);
                    return retval;
                }
                return lst;
            }
            else
            {
                var lst = customerProxy.GetCustomeByIdEmailItem(Email);
                // insert new cache
                cache.Set(param, lst, ConfigCache.TimeExpire);
                return lst;
            }
        }

        public CustomerItem GetCustomerIDOrder(int ID)
        {
            var param = string.Format("CustomerCacheProxyCustomerIdOrder{0}", ID);
            if (cache.KeyExistsCache(param))
            {
                var lst = (CustomerItem)cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    cache.DeleteCache(param);
                    // get new
                    var retval = customerProxy.GetCustomerIDOrder(ID);
                    // insert new cache
                    cache.Set(param, retval, ConfigCache.TimeExpire);
                    return retval;
                }
                return lst;
            }
            else
            {
                var lst = customerProxy.GetCustomerIDOrder(ID);
                // insert new cache
                cache.Set(param, lst, ConfigCache.TimeExpire);
                return lst;
            }
        }



        //public bool InsertReportHighPrice(string name, bool gender, decimal price, string phone, string email, string note)
        //{
        //    throw new NotImplementedException();
        //}


        //public bool InsertContact(string name, string phone, string email, string content)
        //{
        //    throw new NotImplementedException();
        //}

        //public void InsertProvider(int idCustomer, string providerName, string idProviderUser)
        //{

        //}

        //public ExternalProvider GetProvider(int idCustomer, string providerName, string providerId)
        //{
        //    var param = string.Format("CustomerCacheProxyGetProvider{0}{1}{2}", idCustomer, providerName, providerId);
        //    if (cache.KeyExistsCache(param))
        //    {
        //        var lst = (ExternalProvider)cache.GetCache(param);
        //        if (lst == null)
        //        {
        //            // delete old cache
        //            cache.DeleteCache(param);
        //            // get new
        //            var retval = customerProxy.GetProvider(idCustomer, providerName, providerId);
        //            // insert new cache
        //            cache.Set(param, retval, ConfigCache.TimeExpire);
        //            return retval;
        //        }
        //        return lst;
        //    }
        //    else
        //    {
        //        var lst = customerProxy.GetProvider(idCustomer, providerName, providerId);
        //        // insert new cache
        //        cache.Set(param, lst, ConfigCache.TimeExpire);
        //        return lst;
        //    }
        //}

        public List<Customer> GetListCustomerByPhone(string phone)
        {
            var param = string.Format("CustomerCacheProxyGetListCustomerByPhone{0}", phone);
            if (cache.KeyExistsCache(param))
            {
                var lst = (List<Customer>)cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    cache.DeleteCache(param);
                    // get new
                    var retval = customerProxy.GetListCustomerByPhone(phone);
                    // insert new cache
                    cache.Set(param, retval, ConfigCache.TimeExpire);
                    return retval;
                }
                return lst;
            }
            else
            {
                var lst = customerProxy.GetListCustomerByPhone(phone);
                // insert new cache
                cache.Set(param, lst, ConfigCache.TimeExpire);
                return lst;
            }

        }

        public bool GetCustomerBymakh(string keywordEmail, out Customer customer)
        {
            var param = string.Format("CustomerCacheProxyCustomerkeywordMaKh{0}", keywordEmail.Substring(0, keywordEmail.IndexOf('@') - 1));

            if (cache.KeyExistsCache(param))
            {
                var lst = (bool)cache.GetCache(param);
                if (!lst)
                {
                    // delete old cache
                    cache.DeleteCache(param);
                    // get new
                    var retval = customerProxy.GetCustomerBymakh(keywordEmail, out customer);
                    // insert new cache
                    cache.Set(param, retval, ConfigCache.TimeExpire);
                    return retval;
                }
                customer = (Customer)cache.GetCache(param);
                return true;
            }
            else
            {
                var lst = customerProxy.GetCustomerBymakh(keywordEmail, out customer);
                // insert new cache
                cache.Set(param, lst, ConfigCache.TimeExpire);
                return lst;
            }
        }

        public bool GetCustomerByEmailMobile(string keywordEmail, out Customer customer)
        {
            var param = string.Format("CustomerCacheProxyCustomerkeywordEmail{0}", keywordEmail.Substring(0, keywordEmail.IndexOf('@') - 1));

            if (cache.KeyExistsCache(param))
            {
                var lst = (bool)cache.GetCache(param);
                if (!lst)
                {
                    // delete old cache
                    cache.DeleteCache(param);
                    // get new
                    var retval = customerProxy.GetCustomerByEmailMobile(keywordEmail, out customer);
                    // insert new cache
                    cache.Set(param, retval, ConfigCache.TimeExpire);
                    return retval;
                }
                customer = (Customer)cache.GetCache(param);
                return true;
            }
            else
            {
                var lst = customerProxy.GetCustomerByEmailMobile(keywordEmail, out customer);
                // insert new cache
                cache.Set(param, lst, ConfigCache.TimeExpire);
                return lst;
            }
        }

        public CustomerItem GetCustomerID(int id)
        {
            var param = string.Format("CustomerCacheProxyGetCustomerID{0}", id);

            if (cache.KeyExistsCache(param))
            {
                var lst = (CustomerItem)cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    cache.DeleteCache(param);
                    // get new
                    var retval = customerProxy.GetCustomerID(id);
                    // insert new cache
                    cache.Set(param, retval, ConfigCache.TimeExpire);
                    return retval;
                }
                return lst;
            }
            var item = customerProxy.GetCustomerID(id);
            // insert new cache
            cache.Set(param, item, 10);
            return item;
        }

        public CustomerItem GetCustomerByPhone(string phone)
        {
            var param = string.Format("CustomerCacheProxyGetCustomerByPhone{0}", phone);

            if (cache.KeyExistsCache(param))
            {
                var lst = (CustomerItem)cache.GetCache(param);
                if (lst == null)
                {
                    // delete old cache
                    cache.DeleteCache(param);
                    // get new
                    var retval = customerProxy.GetCustomerByPhone(phone);
                    // insert new cache
                    cache.Set(param, retval, ConfigCache.TimeExpire);
                    return retval;
                }
                return lst;

            }
            var item = customerProxy.GetCustomerByPhone(phone);
            // insert new cache
            cache.Set(param, item, 10);
            return item;
        }

    }
}
