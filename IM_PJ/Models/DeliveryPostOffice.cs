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
    
    public partial class DeliveryPostOffice
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public string NumberID { get; set; }
        public string Customer { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
        public string Ward { get; set; }
        public string Address { get; set; }
        public string DeliveryStatus { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime ExpectDate { get; set; }
        public decimal COD { get; set; }
        public decimal OrderCOD { get; set; }
        public decimal Fee { get; set; }
        public decimal OrderFee { get; set; }
        public int Review { get; set; }
        public string Staff { get; set; }
    }
}
