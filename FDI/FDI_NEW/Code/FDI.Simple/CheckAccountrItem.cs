using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    public class CheckAccountrItem : BaseSimple
    {
        public string Name { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsShow { get; set; }
        public decimal? Price { get; set; }
        public int? Percent { get; set; }
        public decimal? PricePV { get; set; }

        public virtual IEnumerable<PaymentAccountItem> PaymentAccounts { get; set; }
    }
    public class ModelCheckAccountrItem : BaseModelSimple
    {
       
        public IEnumerable<CheckAccountrItem> ListItem { get; set; }
       
    }
}
