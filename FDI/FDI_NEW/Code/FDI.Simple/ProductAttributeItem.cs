using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class ProductAttributeItem : BaseSimple
    {
        public int GroupAttributeID { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsAllowFilter { get; set; }
        public bool IsAllowCompare { get; set; }
        public bool IsDeleted { get; set; }

        public string GroupAttributeName { get; set; }
        public List<ProductAttributeSpecificationItem> LstProductAttrSpecItem { get; set; }
    }
    public class ModelProductAttributeItem : BaseModelSimple
    {
      
        public IEnumerable<ProductAttributeItem> ListItem { get; set; }
        
    }
}
