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
    
    public partial class Shop_Product_PreOrder
    {
        public Shop_Product_PreOrder()
        {
            this.Shop_Product_Variant = new HashSet<Shop_Product_Variant>();
        }
    
        public int ID { get; set; }
        public string PreOrderShortDescription { get; set; }
        public string PreOrderDescription { get; set; }
    
        public virtual ICollection<Shop_Product_Variant> Shop_Product_Variant { get; set; }
    }
}