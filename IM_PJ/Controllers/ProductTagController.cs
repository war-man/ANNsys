using IM_PJ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Controllers
{
    public class ProductTagController
    {
        public static ProductTag insert(ProductTag data)
        {
            using (var con = new inventorymanagementEntities())
            {
                con.ProductTags.Add(data);
                con.SaveChanges();

                return data;
            }
        }

        private static List<TagModel> get(string sku, int? productID, int? productVariableID)
        {
            using (var con = new inventorymanagementEntities())
            {
                var prodTag = con.ProductTags.Where(x => 1 == 1);

                if (!String.IsNullOrEmpty(sku))
                    prodTag = prodTag.Where(x => x.SKU == sku);

                if (productID.HasValue && productVariableID.HasValue)
                    prodTag = prodTag.Where(x => x.ProductID == productID.Value && x.ProductVariableID == productVariableID.Value);

                var result = prodTag
                    .Join(
                        con.Tags,
                        pt => pt.TagID,
                        t => t.ID,
                        (pt, t) => t
                    )
                    .Select(x => new TagModel()
                    {
                        id = x.ID,
                        name = x.Name
                    })
                    .OrderBy(o => o.name)
                    .ToList();

                return result;
            }
        }

        public static List<TagModel> get(int productID, int productVariableID)
        {
            return get(String.Empty, productID, productVariableID);
        }

        public static List<TagModel> get(string sku)
        {
            return get(sku);
        }
    }
}