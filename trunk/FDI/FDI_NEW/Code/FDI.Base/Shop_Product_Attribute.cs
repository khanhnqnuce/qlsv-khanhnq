//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FDI.Base
{
    using System;
    using System.Collections.Generic;
    
    public partial class Shop_Product_Attribute
    {
        public Shop_Product_Attribute()
        {
            this.Shop_Product_Attribute_Specification = new HashSet<Shop_Product_Attribute_Specification>();
        }
    
        public int ID { get; set; }
        public Nullable<int> GroupAttributeID { get; set; }
        public string Name { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<bool> IsAllowFilter { get; set; }
        public Nullable<bool> IsAllowCompare { get; set; }
    
        public virtual Shop_Product_Group_Attribute Shop_Product_Group_Attribute { get; set; }
        public virtual ICollection<Shop_Product_Attribute_Specification> Shop_Product_Attribute_Specification { get; set; }
    }
}
