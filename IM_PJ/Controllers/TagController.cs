using IM_PJ.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace IM_PJ.Controllers
{
    public class TagController
    {
        public static Tag insert(Tag data)
        {
            using (var con = new inventorymanagementEntities())
            {
                con.Tags.Add(data);
                con.SaveChanges();

                return data;
            }
        }

        public static List<Tag> insert(List<TagModel> tagList, tbl_Account acc)
        {
            using (var con = new inventorymanagementEntities())
            {
                var now = DateTime.Now;
                var textInfo = new CultureInfo("vi-VN", false).TextInfo;

                var tagNew = tagList.Where(x => 
                    x.slug.StartsWith(String.Format("tag-new-{0:YYYYMMDD}", now))
                )
                .Select(x => new Tag
                {
                    Name = textInfo.ToTitleCase(x.name.Trim()),
                    Slug = Slug.ConvertToSlug(x.name.Trim()),
                    CreatedBy = acc.ID,
                    CreatedDate = now
                })
                .ToList();

                // Check unique slug
                foreach (var tag in tagNew)
                {
                    var tagLast = con.Tags.Where(x => x.Slug.StartsWith(tag.Slug))
                        .OrderByDescending(o => o.ID)
                        .FirstOrDefault();

                    if (tagLast != null)
                    {
                        var strIndex = Regex.Match(tagLast.Slug, @"\d+$").Value;
                        if (!String.IsNullOrEmpty(strIndex))
                            tag.Slug = String.Format("{0}-{1}", tag.Slug, Convert.ToInt32(strIndex) + 1);
                        else
                            tag.Slug = String.Format("{0}-{1}", tag.Slug, 1);
                    }
                }

                con.Tags.AddRange(tagNew);
                con.SaveChanges();

                return tagNew;
            }
        }

        public static List<TagModel> get(string tagName)
        {
            using (var con = new inventorymanagementEntities())
            {
                var tags = con.Tags.Where(x => x.Name.Trim().ToLower().StartsWith(tagName.Trim().ToLower()))
                    .Select(x => new TagModel() {
                        id = x.ID,
                        name = x.Name,
                        slug = x.Slug
                    })
                    .OrderBy(o => o.name)
                    .ToList();

                return tags;
            }
        }
    }
}