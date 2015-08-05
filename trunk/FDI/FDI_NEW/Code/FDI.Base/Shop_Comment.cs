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
    
    public partial class Shop_Comment
    {
        public Shop_Comment()
        {
            this.Shop_Comment1 = new HashSet<Shop_Comment>();
            this.Shop_CommentLike = new HashSet<Shop_CommentLike>();
            this.Shop_CommentReport = new HashSet<Shop_CommentReport>();
        }
    
        public int ID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateIsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public string ModifiedBy { get; set; }
        public string LinkUrl { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string TinhThanh { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<int> ShopCommentID { get; set; }
        public Nullable<bool> DatKPI { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Shop_Comment> Shop_Comment1 { get; set; }
        public virtual Shop_Comment Shop_Comment2 { get; set; }
        public virtual Shop_Product Shop_Product { get; set; }
        public virtual ICollection<Shop_CommentLike> Shop_CommentLike { get; set; }
        public virtual ICollection<Shop_CommentReport> Shop_CommentReport { get; set; }
    }
}