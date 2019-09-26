using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Models.Pages.cron_job_product_status
{
    public class ProductModel
    {
        public int categoryID { get; set; }
        public string categoryName { get; set; }
        public int id { get; set; }
        public string sku { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public double costOfGood { get; set; }
        public double regularPrice { get; set; }
        public double retailPrice { get; set; }
        public string web { get; set; }
        public bool isHidden { get; set; }
        public int cronJobStatus { get; set; }
        public DateTime startDate { get; set; }
        public string note { get; set; }
    }
}