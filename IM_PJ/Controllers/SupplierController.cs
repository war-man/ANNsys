using IM_PJ.Models;
using MB.Extensions;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebUI.Business;

namespace IM_PJ.Controllers
{
    public class SupplierController
    {
        #region CRUD
        public static string Insert(string SupplierName,string SupplierDescription,string SupplierPhone,string SupplierAddress,string SupplierEmail, 
            bool IsHidden, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Supplier ui = new tbl_Supplier();
                ui.SupplierName = SupplierName;
                ui.SupplierDescription = SupplierDescription;
                ui.SupplierPhone = SupplierPhone;
                ui.SupplierAddress = SupplierAddress;
                ui.SupplierEmail = SupplierEmail;
                ui.IsHidden = IsHidden;
                ui.CreatedDate = CreatedDate;
                ui.CreatedBy = CreatedBy;
                dbe.tbl_Supplier.Add(ui);
                dbe.SaveChanges();
                int kq = ui.ID;
                return kq.ToString();
            }
        }
        public static string Update(int ID, string SupplierName, string SupplierDescription, string SupplierPhone, string SupplierAddress, 
            string SupplierEmail, bool IsHidden, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Supplier ui = dbe.tbl_Supplier.Where(a => a.ID == ID).SingleOrDefault();
                if (ui != null)
                {
                    ui.SupplierName = SupplierName;
                    ui.SupplierDescription = SupplierDescription;
                    ui.SupplierPhone = SupplierPhone;
                    ui.SupplierAddress = SupplierAddress;
                    ui.SupplierEmail = SupplierEmail;
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
        #endregion
        #region Select
        
        public static tbl_Supplier GetByID(int ID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Supplier ai = dbe.tbl_Supplier.Where(a => a.ID == ID).FirstOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }
        public static List<tbl_Supplier> GetAll(string s)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_Supplier> ags = new List<tbl_Supplier>();
                ags = dbe.tbl_Supplier.Where(c => c.SupplierName.Contains(s) || c.SupplierPhone.Contains(s)).ToList();
                return ags;
            }
        }
     
        public static List<tbl_Supplier> GetAllWithIsHidden(bool IsHidden)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_Supplier> ags = new List<tbl_Supplier>();
                ags = dbe.tbl_Supplier.Where(a => a.IsHidden == IsHidden).ToList();
                return ags;
            }
        }
        #endregion
    }
}