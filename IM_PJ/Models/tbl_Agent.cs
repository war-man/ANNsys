//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IM_PJ.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Agent
    {
        public int ID { get; set; }
        public string AgentName { get; set; }
        public string AgentDescription { get; set; }
        public string AgentAddress { get; set; }
        public string AgentPhone { get; set; }
        public string AgentEmail { get; set; }
        public string AgentLeader { get; set; }
        public Nullable<bool> IsHidden { get; set; }
        public string AgentAPIID { get; set; }
        public string AgentAPICode { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string AgentFacebook { get; set; }
    }
}
