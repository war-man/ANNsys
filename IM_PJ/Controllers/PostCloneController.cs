using IM_PJ.Models;
using MB.Extensions;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using WebUI.Business;

namespace IM_PJ.Controllers
{
    public class PostCloneController
    {
        public static string getFeesJSON(int PostPublicID)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var result = String.Empty;

            using (var con = new inventorymanagementEntities())
            {
                var post = con.PostClones
                    .Where(x => x.PostPublicID == PostPublicID)
                    .ToArray();
                result = serializer.Serialize(post);
            }

            return result;
        }

        public static PostClone Insert(PostClone data)
        {
            using (var con = new inventorymanagementEntities())
            {
                con.PostClones.Add(data);
                con.SaveChanges();

                return data;
            }
        }

        public static PostClone Update(PostClone data)
        {
            using (var con = new inventorymanagementEntities())
            {
                var post = con.PostClones.Where(o => o.ID == data.ID).FirstOrDefault();
                if (post != null)
                {
                    post.PostPublicID = data.PostPublicID;
                    post.Web = data.Web;
                    post.PostWebID = data.PostWebID;
                    post.CategoryID = data.CategoryID;
                    post.CategoryName = data.CategoryName;
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
                PostClone ui = dbe.PostClones.Where(o => o.ID == ID).FirstOrDefault();
                if (ui != null)
                {
                    dbe.PostClones.Remove(ui);
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }
        public static PostClone GetByID(int ID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                PostClone ai = dbe.PostClones.Where(a => a.ID == ID).SingleOrDefault();
                if (ai != null)
                {
                    return ai;
                }

                return null;
            }
        }
        public static List<PostClone> GetAll(int postPublicID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<PostClone> result = new List<PostClone>();
                result = dbe.PostClones.Where(p => p.PostPublicID == postPublicID).OrderByDescending(o => o.ID).ToList();
                if (result.Count > 0)
                {
                    return result;
                }
                return null;
            }
        }
        public static PostClone Get(int postPublicID, string web)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                PostClone result = dbe.PostClones.Where(p => p.Web == web && p.PostPublicID == postPublicID).FirstOrDefault();
                if (result != null)
                {
                    return result;
                }
                return null;
            }
        }
        public static List<PostClone> GetAll(string web)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<PostClone> ags = new List<PostClone>();
                ags = dbe.PostClones.Where(p => p.Web == web).OrderByDescending(o => o.ID).ToList();
                return ags;
            }
        }
    }
}