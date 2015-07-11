using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Base;
using FDI.Business.Facade;
using FDI.Simple;

namespace FDI.Business.Reposity
{
    interface IProxyCustomer
    {
        List<CustomerItem> GetList();

        CustomerItem GetByID(int id);

        CustomerItem GetCustomerID(int id);

        int CheckCustomerByPhoneAndEmail(string phone, string email);

        int CheckCustomerByPhone(string phone);

        int InsertCustomer(Customer customer);

        int AddCustomer(Customer customer);

        void Save();

        //bool InsertPromotionEmail(string name, string email);

        //bool InsertReportHighPrice(string name, bool gender, decimal price, string phone, string email, string note);

        //bool InsertContact(string name, string phone, string email, string content);
        //CustomerItem GetCustomeByUserNameItem(string userName);
        Customer GetCustomerByID(int id);
        CustomerItem GetCustomerIDOrder(int ID);

        bool GetCustomeByEmail(string email);

        CustomerItem GetCustomeByIdEmailItem(string Email);

        Customer GetCustomeByIdEmail(string Email);

        //void InsertProvider(int idCustomer, string providerName, string idProviderUser);

        //ExternalProvider GetProvider(int idCustomer, string providerName, string providerId);

        List<Customer> GetListCustomerByPhone(string phone);

        bool GetCustomerBymakh(string keywordEmail, out Customer customer);

        bool GetCustomerByEmailMobile(string keywordEmail, out Customer customer);

        CustomerItem GetCustomerByPhone(string phone);
    }
    
}
