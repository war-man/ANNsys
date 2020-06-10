using IM_PJ.Models;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Controllers
{
    public class CustomerLevelController
    {
        #region CRUD
        public static string Insert(string CustomerLevelName, bool IsHidden, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_CustomerLevel ui = new tbl_CustomerLevel();
                ui.CustomerLevelName = CustomerLevelName;
                ui.IsHidden = IsHidden;
                ui.CreatedDate = CreatedDate;
                ui.CreatedBy = CreatedBy;
                dbe.tbl_CustomerLevel.Add(ui);
                int kq = dbe.SaveChanges();
                return kq.ToString();
            }
        }
        public static string Update(int ID, string CustomerLevelName, bool IsHidden, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_CustomerLevel ui = dbe.tbl_CustomerLevel.Where(a => a.ID == ID).SingleOrDefault();
                if (ui != null)
                {
                    ui.CustomerLevelName = CustomerLevelName;
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
        public static tbl_CustomerLevel GetByID(int ID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_CustomerLevel ai = dbe.tbl_CustomerLevel.Where(a => a.ID == ID).SingleOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }
        public static List<tbl_CustomerLevel> GetAll(string s)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_CustomerLevel> ags = new List<tbl_CustomerLevel>();
                ags = dbe.tbl_CustomerLevel.Where(c => c.CustomerLevelName.Contains(s)).ToList();
                return ags;
            }
        }
        #endregion
    }
}