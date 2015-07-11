using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDI.Simple
{
    [Serializable]
    public class ProductGroupAttributeItem : BaseSimple
    {
        public int ProductTypeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime CreatedBy { get; set; }
        public DateTime UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsAllowFiltering { get; set; }
        public bool IsShowOnProductPage { get; set; }

        public string productTypeName { get; set; }
        public int ProductGroupAttributeToTal { get; set; }

        public List<ProductAttributeSpecificationItem> AttributeSpecification { get; set; }
        public List<ProductAttributeItem> LstProAttrItem { get; set; }
    }
    public class ModelProductGroupAttributeItem:BaseModelSimple
    {
        public IEnumerable<ProductGroupAttributeItem> ListItem { get; set; }
    }
}
