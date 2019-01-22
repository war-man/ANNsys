using IM_PJ.Models;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Controllers
{
    public class AgentProductVariableController
    {
        #region CRUD
        public static string Insert(int AgentID, int ProductVariableID, double Quantity, bool IsHidden, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_AgentProductVariable ui = new tbl_AgentProductVariable();
                ui.AgentID = AgentID;
                ui.ProductVariableID = ProductVariableID;
                ui.Quantity = Quantity;
                ui.IsHidden = IsHidden;
                ui.CreatedDate = CreatedDate;
                ui.CreatedBy = CreatedBy;
                dbe.tbl_AgentProductVariable.Add(ui);
                int kq = dbe.SaveChanges();
                return kq.ToString();
            }
        }
        public static string Update(int ID, int AgentID, int ProductVariableID, double Quantity, bool IsHidden, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_AgentProductVariable ui = dbe.tbl_AgentProductVariable.Where(a => a.ID == ID).SingleOrDefault();
                if (ui != null)
                {
                    ui.AgentID = AgentID;
                    ui.ProductVariableID = ProductVariableID;
                    ui.Quantity = Quantity;
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
        public static tbl_AgentProductVariable GetByID(int ID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_AgentProductVariable ai = dbe.tbl_AgentProductVariable.Where(a => a.ID == ID).SingleOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }
        public static List<tbl_AgentProductVariable> GetAll(string s)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_AgentProductVariable> ags = new List<tbl_AgentProductVariable>();
                ags = dbe.tbl_AgentProductVariable.ToList();
                return ags;
            }
        }
        public static List<tbl_AgentProductVariable> GetAllWithIsHidden(bool IsHidden)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_AgentProductVariable> ags = new List<tbl_AgentProductVariable>();
                ags = dbe.tbl_AgentProductVariable.Where(a => a.IsHidden == IsHidden).ToList();
                return ags;
            }
        }
        public static List<tbl_AgentProductVariable> GetByAgentID(int AgentID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_AgentProductVariable> ags = new List<tbl_AgentProductVariable>();
                ags = dbe.tbl_AgentProductVariable.Where(a => a.AgentID == AgentID).ToList();
                return ags;
            }
        }
        #endregion
    }
}