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
    
    public partial class Shop_Content
    {
        public Shop_Content()
        {
            this.Shop_Product = new HashSet<Shop_Product>();
        }
    
        public int ContentID { get; set; }
        public string ContentData { get; set; }
    
        public virtual ICollection<Shop_Product> Shop_Product { get; set; }
    }
}