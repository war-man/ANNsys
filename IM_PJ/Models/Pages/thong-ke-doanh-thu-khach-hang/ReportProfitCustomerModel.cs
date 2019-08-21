using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Models.Pages.thong_ke_doanh_thu_khach_hang
{
    public class ReportProfitCustomerModel
    {
        public CustomerModel customer { get; set; }
        public List<OrderInfoModel> data { get; set; }
    }
}