using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Models.Pages.thong_ke_chuyen_kho
{
    public class SubStockTransferReport
    {
        public string sku { get; set; }
        public string image { get; set; }
        public int quantityTransfer { get; set; }
        public int quantityAvailable { get; set; }
        public DateTime transferDate { get; set; }
    }
}