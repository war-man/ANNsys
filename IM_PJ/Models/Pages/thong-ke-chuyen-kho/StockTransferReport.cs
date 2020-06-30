using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Models.Pages.thong_ke_chuyen_kho
{
    public class StockTransferReport
    {
        public int categoryID { get; set; }
        public string categoryName { get; set; }
        public int productID { get; set; }
        public int variableID { get; set; }
        public string sku { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public int quantityTransfer { get; set; }
        public int quantityAvailable { get; set; }
        public DateTime transferDate { get; set; }
        public bool isVariable { get; set; }
        public IList<SubStockTransferReport> children { get; set; }
    }
}