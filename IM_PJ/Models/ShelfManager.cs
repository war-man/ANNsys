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
    
    public partial class ShelfManager
    {
        public int ID { get; set; }
        public int Floor { get; set; }
        public int Row { get; set; }
        public int Shelf { get; set; }
        public int FloorShelf { get; set; }
        public int ProductID { get; set; }
        public int ProductVariableID { get; set; }
        public int Quantity { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    }
}