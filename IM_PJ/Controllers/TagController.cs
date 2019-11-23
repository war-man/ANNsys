using IM_PJ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public static List<TagModel> get(string tagName)
        {
            using (var con = new inventorymanagementEntities())
            {
                var tags = con.Tags.Where(x => x.Name.Trim().ToLower().StartsWith(tagName.Trim().ToLower()))
                    .Select(x => new TagModel() {
                        id = x.ID,
                        name = x.Name
                    })
                    .OrderBy(o => o.name)
                    .ToList();

                return tags;
            }
        }
    }
}