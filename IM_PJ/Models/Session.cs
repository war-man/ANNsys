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
    
    public partial class Session
    {
        public int ID { get; set; }
        public int AccountID { get; set; }
        public string Page { get; set; }
        public string Value { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    }
}
