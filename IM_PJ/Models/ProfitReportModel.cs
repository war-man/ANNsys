using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Models
{
    public class ProfitReportModel
    {
        public int TotalNumberOfOrder { get; set; }
        public int TotalSoldQuantity { get; set; }
        public int TotalRefundQuantity { get; set; }
        public double TotalSalePrice { get; set; }
        public double TotalSaleCost { get; set; }
        public double TotalSaleDiscount { get; set; }
        public double TotalOtherFee { get; set; }
        public double TotalShippingFee { get; set; }
        public double TotalRefundPrice { get; set; }
        public double TotalRefundCost { get; set; }
        public double TotalRefundFee { get; set; }
    }
}