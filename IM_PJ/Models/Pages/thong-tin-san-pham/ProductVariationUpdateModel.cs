using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Models.Pages.thong_tin_san_pham
{
    public class ProductVariationUpdateModel
    {
        public int productVariationID { get; set; }
        public string variationID { get; set; }
        public string variationValueID { get; set; }
        public string variationName { get; set; }
        public string variationValueName { get; set; }
        public string sku { get; set; }
        public double regularPrice { get; set; }
        public double costOfGood { get; set; }
        public double retailPrice { get; set; }
        public string variationValue { get; set; }
        public int maximumInventoryLevel { get; set; }
        public int minimumInventoryLevel { get; set; }
        public int stockStatus { get; set; }
        public bool @checked { get; set; }
        public string image { get; set; }
    }
}