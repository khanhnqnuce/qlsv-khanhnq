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
    
    public partial class Shop_Product_Variant_Recurring
    {
        public int ID { get; set; }
        public Nullable<int> ProductVariantID { get; set; }
        public Nullable<double> MustRepaidPercent { get; set; }
        public string RecurringLength { get; set; }
        public Nullable<double> InterestRate { get; set; }
        public string MetaKeywordRecurring { get; set; }
        public string MetaDescriptionRecurring { get; set; }
        public string RecurringTitle { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    
        public virtual Shop_Product_Variant Shop_Product_Variant { get; set; }
    }
}