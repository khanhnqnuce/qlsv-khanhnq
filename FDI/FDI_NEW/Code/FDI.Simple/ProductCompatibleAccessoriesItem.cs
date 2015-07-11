using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class ProductCompatibleAccessoriesItem : BaseSimple
    {
        public int ProductID { get; set; }
        public int CompatibleAccessoriesID { get; set; }
        public string ProductName { get; set; }
        public string CompatibleAccessoriesName { get; set; }
        public string IsAllowFilter { get; set; }
        public bool IsDeleted { get; set; }
        public decimal CompatibleAccessoriesPrice { get; set; }
    }
    public class ModelProductCompatibleAccessoriesItem : BaseModelSimple
    {
        public IEnumerable<ProductCompatibleAccessoriesItem> ListItem { get; set; }
    }
}
