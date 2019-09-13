using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Models.Common
{
    public class CustomerModel
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Nick { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Zalo { get; set; }
        public string Facebook { get; set; }
        public string StaffName { get; set; }
        public DiscountGroupModel DiscountGroup { get; set; }
    }
}