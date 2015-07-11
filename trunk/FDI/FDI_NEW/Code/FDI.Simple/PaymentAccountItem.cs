using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    public class PaymentAccountItem : BaseSimple
    {
        public string Name { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsShow { get; set; }
        public int? CustomerId { get; set; }
        public string AccountNumber { get; set; }
        public decimal? Price { get; set; }
        public DateTime? DateCreated { get; set; }
        public string NameCustomer { get; set; }
        public string EmailCustomer { get; set; }
        public string MobileCustomer { get; set; }
        public string AddressAccount { get; set; }
        public decimal? PricePV { get; set; }

        public virtual CustomerItem Customer { get; set; }
        public virtual IEnumerable<CheckAccountrItem> CheckAccounts { get; set; }
    }
    public class ModelPaymentAccountItem : BaseModelSimple
    {
        
        public IEnumerable<PaymentAccountItem> ListItem { get; set; }
    }
}
