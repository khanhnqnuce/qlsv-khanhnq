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
    
    public partial class NewsRelated
    {
        public NewsRelated()
        {
            this.NewsRelatedNewsMaps = new HashSet<NewsRelatedNewsMap>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<bool> Isdeleted { get; set; }
        public Nullable<bool> IsShow { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
    
        public virtual ICollection<NewsRelatedNewsMap> NewsRelatedNewsMaps { get; set; }
    }
}
