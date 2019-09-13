using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Models.Common
{
    public class DiscountGroupModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime DateJoined { get; set; }
        public string StaffName { get; set; }
        public int VerifyOrder { get; set; }
    }
}