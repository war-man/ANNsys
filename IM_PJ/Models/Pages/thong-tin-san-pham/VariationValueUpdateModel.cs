using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Models.Pages.thong_tin_san_pham
{
    public class VariationValueUpdateModel
    {
        public int productVariationID { get; set; }
        public string productVariationSKU { get; set; }
        public string variationValueID { get; set; }
        public string variationName { get; set; }
        public string variationValueName { get; set; }
        public bool isHidden { get; set; }
        public DateTime createdDate { get; set; }
        public string createdBy { get; set; }
    }
}