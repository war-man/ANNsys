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
    
    public partial class DeliverySaveAddress
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> PID { get; set; }
        public int Type { get; set; }
        public Nullable<int> Region { get; set; }
        public string Alias { get; set; }
        public bool IsPicked { get; set; }
        public bool IsDelivered { get; set; }
    }
}
