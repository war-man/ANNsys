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
    
    public partial class tbl_ProductVariableValue
    {
        public int ID { get; set; }
        public Nullable<int> ProductVariableID { get; set; }
        public string ProductvariableSKU { get; set; }
        public Nullable<int> VariableValueID { get; set; }
        public string VariableName { get; set; }
        public string VariableValue { get; set; }
        public Nullable<bool> IsHidden { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
