using IM_PJ.Models;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IM_PJ.Controllers
{
    public class PostPublicImageController
    {
        #region CRUD
        public static string Insert(int PostID, string Image, string CreatedBy, DateTime CreatedDate)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                PostPublicImage ui = new PostPublicImage();
                ui.PostID = PostID;
                ui.Image = Image;
                ui.CreatedDate = CreatedDate;
                ui.CreatedBy = CreatedBy;
                dbe.PostPublicImages.Add(ui);
                int kq = dbe.SaveChanges();
                return kq.ToString();
            }
        }
        public static string Update(int ID, string Image, string ModifiedBy, DateTime ModifiedDate)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                PostPublicImage ui = dbe.PostPublicImages.Where(a => a.ID == ID).SingleOrDefault();
                if (ui != null)
                {
                    ui.Image = Image;
                    ui.ModifiedBy = ModifiedBy;
                    ui.ModifiedDate = ModifiedDate;
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }
        public static string Delete(int ID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                var ui = dbe.PostPublicImages.Where(o => o.ID == ID).FirstOrDefault();
                if (ui != null)
                {
                    dbe.PostPublicImages.Remove(ui);
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static PostPublicImage GetByID(int ID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                PostPublicImage ai = dbe.PostPublicImages.Where(a => a.ID == ID).SingleOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }
        public static List<PostPublicImage> GetAll(string s)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<PostPublicImage> ags = new List<PostPublicImage>();
                ags = dbe.PostPublicImages.OrderByDescending(o => o.ID).ToList();
                return ags;
            }
        }
        public static List<PostPublicImage> GetByPostID(int ProductID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<PostPublicImage> ags = new List<PostPublicImage>();
                ags = dbe.PostPublicImages.Where(p => p.PostID == ProductID).OrderByDescending(o => o.ID).ToList();
                return ags;
            }
        }
        public static PostPublicImage GetFirstByPostID(int ProductID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                var ags = dbe.PostPublicImages.Where(p => p.PostID == ProductID).FirstOrDefault();
                if (ags != null)
                    return ags;
                else return null;
            }
        }
        #endregion
    }
}