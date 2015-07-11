using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class ProductRefItem
    {
        public long ID { get; set; }
        public int ProductID { get; set; }
        public int PruductRefID { get; set; }
        public string ProductName { get; set; }
        public string PruductRefName { get; set; }
        public bool IsDeleted { get; set; }
    }
    public class ModelProductRefItem : BaseModelSimple
    {
        public IEnumerable<ProductRefItem> ListItem { get; set; }
    }
}
