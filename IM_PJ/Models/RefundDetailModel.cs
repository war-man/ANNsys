using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Models
{
    public class RefundDetailModel
    {
        public int RefundGoodsID { get; set; }
        public int RefundDetailID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int ProductVariableID { get; set; }
        public int ProductStyle { get; set; }
        public string ProductImage { get; set; }
        public string ProductTitle { get; set; }
        public string ParentSKU { get; set; }
        public string ChildSKU { get; set; }
        public string VariableValue { get; set; }
        public double Price { get; set; }
        public double ReducedPrice { get; set; }
        public double QuantityRefund { get; set; }
        public int ChangeType { get; set; }
        public double FeeRefund { get; set; }
        public double TotalFeeRefund { get; set; }
    }
}