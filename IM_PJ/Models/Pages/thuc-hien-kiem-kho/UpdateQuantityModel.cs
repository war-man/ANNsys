using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Models.Pages.thuc_hien_kiem_kho
{
    public class UpdateQuantityModel
    {
        public int checkID { get; set; }
        public string sku { get; set; }
        public int quantity { get; set; }
        public DateTime updateDate { get; set; }
        public string staff { get; set; }
    }
}