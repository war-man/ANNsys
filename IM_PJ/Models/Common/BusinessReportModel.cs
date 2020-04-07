using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Models
{
    public class BusinessReportModel
    {
        // Tổng đơn hàng
        public int totalOrders { get; set; }
        // Tổng đơn hàng bán lẻ
        public int totalRetails { get; set; }
        // Tổng đơn hàng  bán sỉ
        public int totalWholesales { get; set; }
        // Tổng số lượng hàng đã bán
        public int totalProducts { get; set; }
        // Tổng số số đơn bán tại shop
        public int totalFace { get; set; }
        // Tổng số đơn giao bằng Bưu Điện
        public int totalPostOffice { get; set; }
        // Tổng số đơn giao bằng dịch vụ Proship
        public int totalProship { get; set; }
        // Tổng số đơn giao tới chành xe
        public int totalTransferStation { get; set; }
        // Tổng số tiền các đơn hàng
        public double totalMoney { get; set; }
        // Tổng số tiền nhận được
        public double totalTransferMoney { get; set; }
        // Tổng số tiền chiết khấu sản phẩm
        public double totalDiscount { get; set; }
        // Tổng tiền trả cho dịch vụ giao hàng
        public double totalFeeShipping { get; set; }
        // Tổng tiền trả cho các phí khác
        public double totalFeeOther { get; set; }
    }
}