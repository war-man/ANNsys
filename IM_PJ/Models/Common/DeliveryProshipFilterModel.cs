using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Models
{
    public class DeliveryProshipFilterModel
    {
        // Mã đơn hàng: OrderID 
        // Delivery: Mã code dịch vụ shipping
        public string search { get; set; } = String.Empty;
        // Trạng thái đơn hàng
        public int orderStatus { get; set; }
        // Trạng thái giao hàng của Bưu Điện
        public string deliveryStatus { get; set; }
        // Trạng thái review thông tin của bưa điện với thông tin order hệ thống
        public int review { get; set; }
        // Tình trạng phí giao hàng
        public int feeStatus { get; set; }
        // Thời gian khởi tạo order
        public DateTime? orderFromDate { get; set; }
        public DateTime? orderToDate { get; set; }
    }
}