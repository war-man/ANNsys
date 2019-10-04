using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Models.Pages.cron_job_product_status
{
    public class FilterModel
    {
        public string search { get; set; }
        public string web { get; set; }
        public int status { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public int category { get; set; }
        public Nullable<bool> isHidden { get; set; }
    }
}