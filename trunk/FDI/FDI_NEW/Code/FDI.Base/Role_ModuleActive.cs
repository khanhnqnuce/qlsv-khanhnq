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
    
    public partial class Role_ModuleActive
    {
        public int ID { get; set; }
        public Nullable<System.Guid> RoleId { get; set; }
        public Nullable<int> ActiveRoleId { get; set; }
        public Nullable<int> ModuleId { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<bool> Check { get; set; }
    
        public virtual ActiveRole ActiveRole { get; set; }
        public virtual aspnet_Roles aspnet_Roles { get; set; }
        public virtual Module Module { get; set; }
    }
}
