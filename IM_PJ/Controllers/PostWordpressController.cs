using IM_PJ.Models;
using MB.Extensions;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using WebUI.Business;

namespace IM_PJ.Controllers
{
    public class PostWordpressController
    {
        public static PostWordpress Insert(PostWordpress data)
        {
            using (var con = new inventorymanagementEntities())
            {
                con.PostWordpress.Add(data);
                con.SaveChanges();

                return data;
            }
        }

        public static PostWordpress Update(PostWordpress data)
        {
            using (var con = new inventorymanagementEntities())
            {
                var post = con.PostWordpress.Where(o => o.ID == data.ID).FirstOrDefault();
                if (post != null)
                {
                    post.PostPublicID = data.PostPublicID;
                    post.WebWordpress = data.WebWordpress;
                    post.PostWordpressID = data.PostWordpressID;
                    post.CategoryID = data.CategoryID;
                    post.Title = data.Title;
                    post.Summary = data.Summary;
                    post.Content = data.Content;
                    post.Thumbnail = data.Thumbnail;
                    post.CreatedBy = data.CreatedBy;
                    post.CreatedDate = data.CreatedDate;
                    post.ModifiedBy = data.ModifiedBy;
                    post.ModifiedDate = data.ModifiedDate;
                    
                    con.SaveChanges();

                    return post;
                }
                return null;
            }
        }
        public static string Delete(int ID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                PostWordpress ui = dbe.PostWordpress.Where(o => o.ID == ID).FirstOrDefault();
                if (ui != null)
                {
                    dbe.PostWordpress.Remove(ui);
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }
        public static PostWordpress GetByID(int ID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                PostWordpress ai = dbe.PostWordpress.Where(a => a.ID == ID).SingleOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }
        public static List<PostWordpress> GetAllByPostPublicID(int postPublicID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<PostWordpress> ags = new List<PostWordpress>();
                ags = dbe.PostWordpress.Where(p => p.PostPublicID == postPublicID).OrderByDescending(o => o.ID).ToList();
                return ags;
            }
        }
        public static List<PostWordpress> GetAllByPostWebWordpress(string webWordpres)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<PostWordpress> ags = new List<PostWordpress>();
                ags = dbe.PostWordpress.Where(p => p.WebWordpress == webWordpres).OrderByDescending(o => o.ID).ToList();
                return ags;
            }
        }
    }
}