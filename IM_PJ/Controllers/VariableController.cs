using IM_PJ.Models;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Controllers
{
    public class VariableController
    {
        #region CRUD
        public static string Insert(string VariableName, string VariableDescription, bool IsHidden,
            DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Variable ui = new tbl_Variable();
                ui.VariableName = VariableName;
                ui.VariableDescription = VariableDescription;
                ui.IsHidden = IsHidden;
                ui.CreatedDate = CreatedDate;
                ui.CreatedBy = CreatedBy;
                dbe.tbl_Variable.Add(ui);
                int kq = dbe.SaveChanges();
                return kq.ToString();
            }
        }
        public static string Update(int ID, string VariableName, string VariableDescription, bool IsHidden,
            DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Variable ui = dbe.tbl_Variable.Where(a => a.ID == ID).SingleOrDefault();
                if (ui != null)
                {
                    ui.VariableName = VariableName;
                    ui.VariableDescription = VariableDescription;
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
        public static tbl_Variable GetByName(string name)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Variable ai = dbe.tbl_Variable.Where(a => a.VariableName == name).SingleOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }
        public static tbl_Variable GetByID(int ID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Variable ai = dbe.tbl_Variable.Where(a => a.ID == ID).SingleOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }

        public static List<tbl_Variable> GetAll(string s)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_Variable> ags = new List<tbl_Variable>();
                ags = dbe.tbl_Variable.Where(v => v.VariableName.Contains(s)).OrderBy(o => o.ID).ToList();
                return ags;
            }
        }
        public static List<tbl_Variable> GetAllIsHidden(bool IsHidden)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_Variable> ags = new List<tbl_Variable>();
                ags = dbe.tbl_Variable.Where(v => v.IsHidden == IsHidden).OrderBy(o => o.VariableName).ToList();
                return ags;
            }
        }
        #endregion
    }
}