using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class ProductGroupAttributeMapItem
    {
        public string GroupName { get; set; }
        public int? AttributeID { get; set; }
        public string AttributeName { get; set; }
        public bool? IsAllowFilter { get; set; }
        public int? SpecID { get; set; }
        public string SpecName { get; set; }

        public List<ProductGroupAttributeMapItem> Attributes { get; set; }
    }
    public class ModelProductGroupAttributeMapItem : BaseModelSimple
    {
        public IEnumerable<ProductGroupAttributeMapItem> ListItem { get; set; }
    }
}
