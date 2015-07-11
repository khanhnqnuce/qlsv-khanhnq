using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Base;
using FDI.Business.Implementation.Proxy;
using FDI.Business.Reposity;
using FDI.Simple;
using FDI.Utils;

namespace FDI.Business.Implementation.Manager
{
    public class CustomerManager
    {
        readonly IProxyCustomer _customer;
        readonly IProxyCustomer _customerCache;
        private static readonly CustomerManager Instance = new CustomerManager();

        public static CustomerManager GetInstance()
        {
            return Instance;
        }

        public CustomerManager()
        {
            _customer = CustomerProxy.GetInstance;
            _customerCache = ConfigCache.EnableCache == 1 ? CustomerCacheProxy.GetInstance : null;
        }
        public List<CustomerItem> GetList()
        {
            if (ConfigCache.EnableCache == 1 && _customerCache != null)
            {
                return _customerCache.GetList();
            }
            return _customer.GetList();
        }

        public CustomerItem GetByID(int id)
        {
            if (ConfigCache.EnableCache == 1 && _customerCache != null)
            {
                return _customerCache.GetByID(id);
            }
            return _customer.GetByID(id);
        }

        public int CheckCustomerByPhoneAndEmail(string phone, string email)
        {
            return _customer.CheckCustomerByPhoneAndEmail(phone, email);
        }

        public int CheckCustomerByPhone(string phone)
        {
            return _customer.CheckCustomerByPhone(phone);
        }

        public int InsertCustomer(Customer customerObj)
        {
            return _customer.InsertCustomer(customerObj);
        }

        public int AddCustomer(Customer customerObj)
        {
            return _customer.AddCustomer(customerObj);
        }

        public void Save()
        {
            _customer.Save();
        }

      
        public Customer GetCustomerByID(int id)
        {
            //if (ConfigCache.EnableCache == 1 && customerCache != null)
            //{
            //    return customerCache.GetCustomerByID(id);
            //}
            return _customer.GetCustomerByID(id);

        }
        public Customer GetCustomeByIdEmail(string email)
        {
            //if (ConfigCache.EnableCache == 1 && customerCache != null)
            //{
            //    return customerCache.GetCustomeByIdEmail(Email);
            //}
            return _customer.GetCustomeByIdEmail(email);
        }
        public CustomerItem GetCustomeByIdEmailItem(string email)
        {
            if (ConfigCache.EnableCache == 1 && _customerCache != null)
            {
                return _customerCache.GetCustomeByIdEmailItem(email);
            }
            return _customer.GetCustomeByIdEmailItem(email);
        }
        public bool GetCustomeByEmail(string email)
        {
            if (ConfigCache.EnableCache == 1 && _customerCache != null)
            {
                return _customerCache.GetCustomeByEmail(email);
            }
            return _customer.GetCustomeByEmail(email);
        }

        public CustomerItem GetCustomerIDOrder(int id)
        {
            if (ConfigCache.EnableCache == 1 && _customerCache != null)
            {
                return _customerCache.GetCustomerIDOrder(id);
            }
            return _customer.GetCustomerIDOrder(id);
        }

        //public void InsertProvider(int idCustomer, string providerName, string idProviderUser)
        //{
        //    //if (ConfigCache.EnableCache == 1 && customerCache != null)
        //    //{
        //    //    customerCache.InsertProvider(idCustomer, providerName, idProviderUser);
        //    //}
        //    customer.InsertProvider(idCustomer, providerName, idProviderUser);

        //}

        //public ExternalProvider GetProvider(int idCustomer, string providerName, string providerId)
        //{
        //    //if (ConfigCache.EnableCache == 1 && customerCache != null)
        //    //{
        //    //    return customerCache.GetProvider(idCustomer, providerName, providerId);
        //    //}

        //    return customer.GetProvider(idCustomer, providerName, providerId);

        //}

        public List<Customer> GetListCustomerByPhone(string phone)
        {
            //if (ConfigCache.EnableCache == 1 && customerCache != null)
            //{
            //    return customerCache.GetListCustomerByPhone(phone);
            //}
            return _customer.GetListCustomerByPhone(phone);
        }

        public bool GetCustomerBymakh(string keywordEmail, out Customer customernew)
        {
            if (ConfigCache.EnableCache == 1 && _customerCache != null)
            {
                return _customerCache.GetCustomerBymakh(keywordEmail, out customernew);
            }
            return _customer.GetCustomerBymakh(keywordEmail, out customernew);
        }

        public bool GetCustomerByEmailMobile(string keywordEmail, out Customer customernew)
        {
            if (ConfigCache.EnableCache == 1 && _customerCache != null)
            {
                return _customerCache.GetCustomerByEmailMobile(keywordEmail, out customernew);
            }
            return _customer.GetCustomerByEmailMobile(keywordEmail, out customernew);
        }

        public CustomerItem GetCustomerID(int id)
        {
            if (ConfigCache.EnableCache == 1 && _customerCache != null)
            {
                return _customerCache.GetCustomerID(id);
            }
            return _customer.GetCustomerID(id);
        }

        public CustomerItem GetCustomerByPhone(string phone)
        {
            if (ConfigCache.EnableCache == 1 && _customerCache != null)
            {
                return _customerCache.GetCustomerByPhone(phone);
            }
            return _customer.GetCustomerByPhone(phone);
        }
    }
}
