using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Base;
using FDI.Business.Facade;
using FDI.Business.Reposity;
using FDI.Simple;

namespace FDI.Business.Implementation.Proxy
{
    public class CustomerProxy : CustomerFacade, IProxyCustomer
    {
        #region fields
        private static readonly Lazy<CustomerProxy> lazy = new Lazy<CustomerProxy>(() => new CustomerProxy());
        #endregion

        #region properties
        public static CustomerProxy GetInstance { get { return lazy.Value; } }

        #endregion

        #region constructor
        private CustomerProxy()
        {
        }

        #endregion

        public List<CustomerItem> GetList()
        {
            return InstanceCustomer.customerDal.GetListAbstract();
        }
       

        public CustomerItem GetByID(int id)
        {
            return InstanceCustomer.customerDal.GetByIdAbstract(id);
        }

        public int CheckCustomerByPhoneAndEmail(string phone, string email)
        {
            return InstanceCustomer.customerDal.CheckCustomerByPhoneAndEmailAbstract(phone, email);
        }

        public int CheckCustomerByPhone(string phone)
        {
            return InstanceCustomer.customerDal.CheckCustomerByPhoneAbstract(phone);
        }

        public int InsertCustomer(Customer customer)
        {
            return InstanceCustomer.customerDal.InsertCustomerAbstract(customer);
        }

        public int AddCustomer(Customer customer)
        {
            return InstanceCustomer.customerDal.AddCustomerAbstract(customer);
        }

        //public bool InsertPromotionEmail(string name, string email)
        //{
        //    return InstanceCustomer.customerDal.InsertPromotionEmailAbstract(name, email);
        //}

        //public bool InsertReportHighPrice(string name, bool gender, decimal price, string phone, string email, string note)
        //{
        //    return InstanceCustomer.customerDal.InsertReportHighPriceAbstract(name, gender, price, phone, email, note);
        //}

        //public bool InsertContact(string name, string phone, string email, string content)
        //{
        //    return InstanceCustomer.customerDal.InsertContactAbstract(name, phone, email, content);
        //}

        public Customer GetCustomerByID(int id)
        {
            return InstanceCustomer.customerDal.GetCustomerByIDAbstract(id);
        }

        public CustomerItem GetCustomerID(int id)
        {
            return InstanceCustomer.customerDal.GetCustomerIDAbstraction(id);
        }

        public bool GetCustomeByEmail(string email)
        {
            return InstanceCustomer.customerDal.GetCustomeByEmailAbstract(email);
        }
        public Customer GetCustomeByIdEmail(string Email)
        {
            return InstanceCustomer.customerDal.GetCustomeByIdEmailAbstract(Email);
        }
        public CustomerItem GetCustomeByIdEmailItem(string Email)
        {
            return InstanceCustomer.customerDal.GetCustomeByIdEmailItemAbstract(Email);
        }

        public CustomerItem GetCustomerIDOrder(int ID)
        {
            return InstanceCustomer.customerDal.GetCustomerIDAbstraction(ID);
        }

        public void Save()
        {
            InstanceCustomer.customerDal.Save();
        }

        //public void InsertProvider(int idCustomer, string providerName, string idProviderUser)
        //{
        //    InstanceCustomer.customerDal.InsertProviderAbstraction(idCustomer, providerName, idProviderUser);
        //}

        //public ExternalProvider GetProvider(int idCustomer, string providerName, string providerId)
        //{
        //    return InstanceCustomer.customerDal.GetProviderAbstraction(idCustomer, providerName, providerId);
        //}

        public List<Customer> GetListCustomerByPhone(string phone)
        {
            return InstanceCustomer.customerDal.GetListCustomerByPhoneAbstraction(phone);
        }
        public bool GetCustomerByEmailMobile(string keywordEmail, out Customer customer)
        {
            return InstanceCustomer.customerDal.GetCustomerByEmailMobileAbstraction(keywordEmail, out customer);
        }
        public bool GetCustomerBymakh(string keywordEmail, out Customer customer)
        {
            return InstanceCustomer.customerDal.GetCustomerBymakhAbstraction(keywordEmail, out customer);
        }

        public CustomerItem GetCustomerByPhone(string phone)
        {
            return InstanceCustomer.customerDal.GetCustomerByPhoneAsbtract(phone);
        }
    }
}
