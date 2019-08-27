using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Models.Pages.thong_ke_doanh_thu_khach_hang
{
    public class OrderInfoModel
    {
        public DateTime dateDone { get; set; }
        public int quantityOrder { get; set; }
        public int quantityProduct { get; set; }
        public Double costOfGoods { get; set; }
        public Double price { get; set; }
        public Double discount { get; set; }
        public Double feeShipping { get; set; }
        public int quantityRefund { get; set; }
        public int quantityProductRefund { get; set; }
        public Double refundCapital { get; set; }
        public Double refundMoney { get; set; }
        public Double refundFee { get; set; }
    }
}