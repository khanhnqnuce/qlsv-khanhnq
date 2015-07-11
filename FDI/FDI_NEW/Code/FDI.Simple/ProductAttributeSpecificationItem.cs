using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class ProductAttributeSpecificationItem : BaseSimple
    {
        public int? AttributeID { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsAllowFilter { get; set; }
        public bool IsDeleted { get; set; }

        public string productAttributeName { get; set; }

        public string attributeGroupName { get; set; }

        public int? productId { get; set; }

        public string sttCheck { get; set; }
    }
    public class ModelProductAttributeSpecificationItem : BaseModelSimple
    {
        
        public IEnumerable<ProductAttributeSpecificationItem> ListItem { get; set; }
        
    }
}
