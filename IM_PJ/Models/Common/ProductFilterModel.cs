using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Models
{
    public class ProductFilterModel
    {
        // Lọc theo category
        public int category { get; set; } = 0;
        // Tìm kiếm theo key ( tên sản phẩm, SKU, màu )
        public string search { get; set; } = String.Empty;
        // Tìm kiếu theo một số màu mặc định
        public string color { get; set; } = String.Empty;
        // Tìm kiếm theo một số size mặc định
        public string size { get; set; } = String.Empty;
        public int stockStatus { get; set; } = 0;
        public string quantity { get; set; } = String.Empty;
        public int quantityFrom { get; set; } = 0;
        public int quantityTo { get; set; } = 0;
        // Ngày khởi tạo sản phẩm
        public string productDate { get; set; } = String.Empty;
        // Lọc những sản phẩm cho phép show ở trang chính
        public string showHomePage { get; set; } = String.Empty;
        // Lọc những sản phẩm cho phép show ở trang quảng cáo
        public string webPublish { get; set; } = String.Empty;
    }
}