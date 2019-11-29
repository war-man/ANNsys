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

        public static List<ProductTag> insert(List<ProductTag> prodTags)
        {
            using (var con = new inventorymanagementEntities())
            {
                var tagNews = new List<ProductTag>();

                foreach (var item in prodTags)
                {
                    var tagOld = con.ProductTags
                        .Where(x => x.SKU == item.SKU)
                        .Where(x => x.TagID == item.TagID)
                        .FirstOrDefault();

                    if (tagOld == null)
                        tagNews.Add(item);
                }

                if (tagNews.Count > 0)
                {
                    con.ProductTags.AddRange(tagNews);
                    con.SaveChanges();
                    return tagNews;
                }
                else
                {
                    return null;
                }
            }
        }

        public static List<ProductTag> update(int productID, List<ProductTag> prodTags)
        {
            using (var con = new inventorymanagementEntities())
            {
                var tagDB = con.ProductTags
                    .Where(x => x.ProductID == productID)
                    .Where(x => x.ProductVariableID == 0)
                    .ToList();

                var tagOld = tagDB
                    .Join(
                        prodTags,
                        tdb => tdb.TagID,
                        pt => pt.TagID,
                        (tdb, pt) => tdb
                    )
                    .ToList();

                var tagDelete = tagDB.Except(tagOld).ToList();
                if (tagDelete.Count > 0)
                {
                    con.ProductTags.RemoveRange(tagDelete);
                    con.SaveChanges();
                    tagDB = tagDB.Except(tagDelete).ToList();
                }

                var tagInsert = prodTags
                    .Where(x => !tagOld.Any(y => y.ProductID == x.ProductID && y.TagID == x.TagID))
                    .ToList();

                if (tagInsert.Count > 0)
                {
                    con.ProductTags.AddRange(tagInsert);
                    con.SaveChanges();
                    tagDB.AddRange(tagInsert);
                }

                return tagDB;
            }
        }

        public static List<ProductTag> delete(int productID)
        {
            using (var con = new inventorymanagementEntities())
            {
                var tagDelete = con.ProductTags
                    .Where(x => x.ProductID == productID)
                    .Where(x => x.ProductVariableID == 0)
                    .ToList();

                if (tagDelete.Count > 0)
                {
                    con.ProductTags.RemoveRange(tagDelete);
                    con.SaveChanges();
                }

                return tagDelete;
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
                        name = x.Name,
                        slug = x.Slug
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