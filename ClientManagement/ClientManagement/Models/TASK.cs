//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClientManagement.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TASK
    {
        public int Id { get; set; }
        public Nullable<int> ID_CLIENT { get; set; }
        public Nullable<int> ID_USER { get; set; }
        public string NAME { get; set; }
        public Nullable<int> DELETED { get; set; }
    }
}