using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Base;
using FDI.Simple;

namespace FDI.DA.EndUser.Reposity
{
    public interface IReposityCustomer
    {
        List<CustomerItem> GetList();

        CustomerItem GetByID(int id);

        int CheckCustomerByPhoneAndEmail(string phone, string email);
        int CheckCustomerByPhone(string phone);

        int InsertCustomer(Customer customer);

        int AddCustomer(Customer customer);

        Customer GetCustomerByID(int id);

        bool GetCustomeByEmail(string email);
        CustomerItem GetCustomeByIdEmailItem(string email);
        Customer GetCustomeByIdEmail(string email);
        //CustomerItem GetCustomeByUserNameItem(string userName);
        //bool InsertPromotionEmail(string name, string email);

        //bool InsertReportHighPrice(string name, bool gender, decimal price, string phone, string email, string note);

        //bool InsertContact(string name, string phone, string email, string content);

        CustomerItem GetCustomerID(int id);

        bool GetCustomerByEmailMobile(string keywordEmail, out Customer customer);

        bool GetCustomerBymakh(string makh, out Customer customer);

        void Save();

        //void InsertProvider(int idCustomer, string providerName, string idProviderUser);

        //ExternalProvider GetProvider(int idCustomer, string providerName, string providerId);

        List<Customer> GetListCustomerByPhone(string phone);

        CustomerItem GetCustomerByPhone(string phone);
    }
}
