using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Base;
using FDI.DA.EndUser.Reposity;
using FDI.Simple;

namespace FDI.Business.Abstraction
{
    public class CustomerAbstraction
    {
        IReposityCustomer bridge;

        internal CustomerAbstraction(IReposityCustomer implementation)
        {
            this.bridge = implementation;
        }
        public List<CustomerItem> GetListAbstract()
        {
            return this.bridge.GetList();
        }


        public CustomerItem GetByIdAbstract(int id)
        {
            return this.bridge.GetByID(id);
        }

        

        public int CheckCustomerByPhoneAndEmailAbstract(string phone, string email)
        {
            return this.bridge.CheckCustomerByPhoneAndEmail(phone, email);
        }

        public int CheckCustomerByPhoneAbstract(string phone)
        {
            return this.bridge.CheckCustomerByPhone(phone);
        }

        public int InsertCustomerAbstract(Customer customer)
        {
            return this.bridge.InsertCustomer(customer);
        }

        public int AddCustomerAbstract(Customer customer)
        {
            return this.bridge.AddCustomer(customer);
        }

        //public bool InsertPromotionEmailAbstract(string name, string email)
        //{
        //    return this.bridge.InsertPromotionEmail(name, email);
        //}

        //public bool InsertReportHighPriceAbstract(string name, bool gender, decimal price, string phone, string email, string note)
        //{
        //    return this.bridge.InsertReportHighPrice(name, gender, price, phone, email, note);
        //}

        //public bool InsertContactAbstract(string name, string phone, string email, string content)
        //{
        //    return this.bridge.InsertContact(name, phone, email, content);
        //}

        public Customer GetCustomerByIDAbstract(int id)
        {
            return this.bridge.GetCustomerByID(id);
        }


        public bool GetCustomeByEmailAbstract(string email)
        {
            return bridge.GetCustomeByEmail(email);
        }
        public bool GetCustomerBymakhAbstract(string makh, out Customer customer)
        {
            return bridge.GetCustomerBymakh(makh, out customer);
        }

        public bool GetCustomerByEmailMobile(string keywordEmail, out Customer customer)
        {
            return bridge.GetCustomerBymakh(keywordEmail, out customer);
        }

        public Customer GetCustomeByIdEmailAbstract(string Email)
        {
            return bridge.GetCustomeByIdEmail(Email);
        }
        public CustomerItem GetCustomeByIdEmailItemAbstract(string Email)
        {
            return bridge.GetCustomeByIdEmailItem(Email);
        }
        public CustomerItem GetCustomerIDAbstraction(int ID)
        {
            return this.bridge.GetCustomerID(ID);
        }

        public void Save()
        {
            this.bridge.Save();
        }

        //public void InsertProviderAbstraction(int idCustomer, string providerName, string idProviderUser)
        //{
        //    this.bridge.InsertProvider(idCustomer, providerName, idProviderUser);
        //}

        //public ExternalProvider GetProviderAbstraction(int idCustomer, string providerName, string providerId)
        //{
        //    return this.bridge.GetProvider(idCustomer, providerName, providerId);
        //}

        public List<Customer> GetListCustomerByPhoneAbstraction(string phone)
        {
            return this.bridge.GetListCustomerByPhone(phone);
        }

        public bool GetCustomerBymakhAbstraction(string keywordEmail, out Customer customer)
        {
            return bridge.GetCustomerBymakh(keywordEmail, out customer);
        }

        public bool GetCustomerByEmailMobileAbstraction(string keywordEmail, out Customer customer)
        {
            return bridge.GetCustomerByEmailMobile(keywordEmail, out customer);
        }

        public CustomerItem GetCustomerByPhoneAsbtract(string phone)
        {
            return bridge.GetCustomerByPhone(phone);
        }
    }
}
