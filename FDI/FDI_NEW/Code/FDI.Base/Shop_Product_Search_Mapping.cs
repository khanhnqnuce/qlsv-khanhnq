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
    
    public partial class Shop_Product_Search_Mapping
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public string Description { get; set; }
    
        public virtual Shop_Product Shop_Product { get; set; }
    }
}