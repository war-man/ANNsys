using IM_PJ.Models;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Controllers
{
    public class DiscountGroupController
    {
        #region CRUD
        public static string Insert(string DiscountName, double DiscountAmount, double DiscountAmountPercent, string DiscountNote, bool IsHidden,
            DateTime CreatedDate, string CreatedBy, double FeeRefund, double NumOfDateToChangeProduct,double NumOfProductCanChange)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_DiscountGroup ui = new tbl_DiscountGroup();
                ui.DiscountName = DiscountName;
                ui.DiscountAmount = DiscountAmount;
                ui.DiscountAmountPercent = DiscountAmountPercent;
                ui.DiscountNote = DiscountNote;
                ui.IsHidden = IsHidden;
                ui.CreatedDate = CreatedDate;
                ui.CreatedBy = CreatedBy;
                ui.FeeRefund = FeeRefund;
                ui.NumOfDateToChangeProduct = NumOfDateToChangeProduct;
                ui.NumOfProductCanChange = NumOfProductCanChange;
                dbe.tbl_DiscountGroup.Add(ui);
                dbe.SaveChanges();
                int kq = ui.ID;
                return kq.ToString();
            }
        }
        public static string Update(int ID, string DiscountName, double DiscountAmount, double DiscountAmountPercent, string DiscountNote, bool IsHidden,
            DateTime ModifiedDate, string ModifiedBy, double FeeRefund, double NumOfDateToChangeProduct, double NumOfProductCanChange)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_DiscountGroup ui = dbe.tbl_DiscountGroup.Where(a => a.ID == ID).SingleOrDefault();
                if (ui != null)
                {
                    ui.DiscountName = DiscountName;
                    ui.DiscountAmount = DiscountAmount;
                    ui.DiscountAmountPercent = DiscountAmountPercent;
                    ui.DiscountNote = DiscountNote;
                    ui.IsHidden = IsHidden;
                    ui.FeeRefund = FeeRefund;
                    ui.NumOfDateToChangeProduct = NumOfDateToChangeProduct;
                    ui.NumOfProductCanChange = NumOfProductCanChange;
                    ui.ModifiedBy = ModifiedBy;
                    ui.ModifiedDate = ModifiedDate;
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select        
        public static tbl_DiscountGroup GetByID(int ID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_DiscountGroup ai = dbe.tbl_DiscountGroup.Where(a => a.ID == ID).FirstOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }
        public static List<tbl_DiscountGroup> GetAll(string s)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_DiscountGroup> ags = new List<tbl_DiscountGroup>();
                ags = dbe.tbl_DiscountGroup.Where(c => c.DiscountName.Contains(s) || c.DiscountAmountPercent.ToString().Contains(s)).OrderByDescending(x => x.DiscountAmount).ToList();
                return ags;
            }
        }

     
        #endregion
    }
}