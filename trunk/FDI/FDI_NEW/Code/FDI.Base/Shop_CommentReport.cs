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
    
    public partial class Shop_CommentReport
    {
        public int ID { get; set; }
        public Nullable<int> Report { get; set; }
        public int ShopCommentID { get; set; }
        public string ListCustomerID { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
    
        public virtual Shop_Comment Shop_Comment { get; set; }
    }
}
