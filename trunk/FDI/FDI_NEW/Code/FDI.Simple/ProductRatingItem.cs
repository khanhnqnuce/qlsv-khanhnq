using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class ProductRatingItem : BaseSimple
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string RateNote { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public bool IdDelete { get; set; }
        public double RateAvg { get; set; }
        public DateTime DateCreated { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public CustomerItem Customer { get; set; }
    }
    public class ModelProductRatingItem : BaseModelSimple
    {
        public IEnumerable<ProductRatingItem> ListItem { get; set; }
    }

}
