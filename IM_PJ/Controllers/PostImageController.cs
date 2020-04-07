using IM_PJ.Models;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IM_PJ.Controllers
{
    public class PostImageController
    {
        #region CRUD
        public static string Insert(int PostID, string Image, string CreatedBy, DateTime CreatedDate)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_PostImage ui = new tbl_PostImage();
                ui.PostID = PostID;
                ui.Image = Image;
                ui.CreatedDate = CreatedDate;
                ui.CreatedBy = CreatedBy;
                dbe.tbl_PostImage.Add(ui);
                int kq = dbe.SaveChanges();
                return kq.ToString();
            }
        }
        public static string Update(int ID, string Image, string ModifiedBy, DateTime ModifiedDate)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_PostImage ui = dbe.tbl_PostImage.Where(a => a.ID == ID).SingleOrDefault();
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
                var ui = dbe.tbl_PostImage.Where(o => o.ID == ID).FirstOrDefault();
                if (ui != null)
                {
                    dbe.tbl_PostImage.Remove(ui);
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static tbl_PostImage GetByID(int ID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_PostImage ai = dbe.tbl_PostImage.Where(a => a.ID == ID).SingleOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }
        public static List<tbl_PostImage> GetAll(string s)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_PostImage> ags = new List<tbl_PostImage>();
                ags = dbe.tbl_PostImage.OrderByDescending(o => o.ID).ToList();
                return ags;
            }
        }
        public static List<tbl_PostImage> GetByPostID(int ProductID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_PostImage> ags = new List<tbl_PostImage>();
                ags = dbe.tbl_PostImage.Where(p => p.PostID == ProductID).OrderByDescending(o => o.ID).ToList();
                return ags;
            }
        }
        public static List<tbl_PostImage> GetToCopyByPostID(int ProductID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_PostImage> ags = new List<tbl_PostImage>();
                ags = dbe.tbl_PostImage.Where(p => p.PostID == ProductID).OrderBy(o => o.ID).ToList();
                return ags;
            }
        }
        public static tbl_PostImage GetFirstByPostID(int ProductID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                var ags = dbe.tbl_PostImage.Where(p => p.PostID == ProductID).FirstOrDefault();
                if (ags != null)
                    return ags;
                else return null;
            }
        }
        #endregion
    }
}