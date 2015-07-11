using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    public class TransactionHistoryItem : BaseSimple
    {
        public string Name { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsShow { get; set; }
        public string CustomerName { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateEnd { get; set; }
        public int? DynamicId { get; set; }
        public decimal? PricePV { get; set; }
        public string NameCustomer { get; set; }
        public string EmailCustomer { get; set; }
        public string MobileCustomer { get; set; }
        public string Code { get; set; }

        public virtual CustomerItem Customer { get; set; }
        public virtual DynamicCustomerItem DynamicCustomer { get; set; }
    }
    public class ModelTransactionHistoryItem : BaseModelSimple
    {
        public IEnumerable<TransactionHistoryItem> ListItem { get; set; }
    }
}
