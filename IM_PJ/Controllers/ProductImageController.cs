using IM_PJ.Models;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IM_PJ.Controllers
{
    public class ProductImageController
    {
        #region CRUD
        public static string Insert(int ProductID, string ProductImage, bool IsHidden, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_ProductImage ui = new tbl_ProductImage();
                ui.ProductID = ProductID;
                ui.ProductImage = ProductImage;
                ui.IsHidden = IsHidden;
                ui.CreatedDate = CreatedDate;
                ui.CreatedBy = CreatedBy;
                dbe.tbl_ProductImage.Add(ui);
                int kq = dbe.SaveChanges();
                return kq.ToString();
            }
        }
        public static string Update(int ID, string ProductImage, bool IsHidden, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_ProductImage ui = dbe.tbl_ProductImage.Where(a => a.ID == ID).SingleOrDefault();
                if (ui != null)
                {
                    ui.ProductImage = ProductImage;
                    ui.IsHidden = IsHidden;
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
                var ui = dbe.tbl_ProductImage.Where(o => o.ID == ID).FirstOrDefault();
                if (ui != null)
                {
                    dbe.tbl_ProductImage.Remove(ui);
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                return "0";
            }
        }
        #endregion
        #region Select
        public static tbl_ProductImage GetByID(int ID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_ProductImage ai = dbe.tbl_ProductImage.Where(a => a.ID == ID).SingleOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }
        public static List<tbl_ProductImage> GetAll(string s)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_ProductImage> ags = new List<tbl_ProductImage>();
                ags = dbe.tbl_ProductImage.OrderByDescending(o => o.ID).ToList();
                return ags;
            }
        }
        public static List<tbl_ProductImage> GetByProductID(int ProductID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_ProductImage> ags = new List<tbl_ProductImage>();
                ags = dbe.tbl_ProductImage.Where(p => p.ProductID == ProductID).OrderByDescending(o => o.ID).ToList();
                return ags;
            }
        }
        public static tbl_ProductImage GetFirstByProductID(int ProductID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                var ags = dbe.tbl_ProductImage.Where(p => p.ProductID == ProductID).FirstOrDefault();
                if (ags != null)
                    return ags;
                else return null;
            }
        }
        #endregion
    }
}