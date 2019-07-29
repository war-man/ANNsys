using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Models.Pages.thong_ke_nhap_kho
{
    public class GoodsReceiptReport
    {
        public int categoryID { get; set; }
        public string categoryName { get; set; }
        public int productID { get; set; }
        public int variableID { get; set; }
        public string sku { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public int quantityInput { get; set; }
        public int quantityStock { get; set; }
        public DateTime goodsReceiptDate { get; set; }
        public bool isVariable { get; set; }
    }
}