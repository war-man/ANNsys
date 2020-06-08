using IM_PJ.Models;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Controllers
{
    public class AgentProductController
    {
        #region CRUD
        public static string Insert(int AgentID, int ProductID, double Quantity, bool IsHidden, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_AgentProduct ui = new tbl_AgentProduct();
                ui.AgentID = AgentID;
                ui.ProductID = ProductID;
                ui.Quantity = Quantity;
                ui.IsHidden = IsHidden;
                ui.CreatedDate = CreatedDate;
                ui.CreatedBy = CreatedBy;
                dbe.tbl_AgentProduct.Add(ui);
                int kq = dbe.SaveChanges();
                return kq.ToString();
            }
        }
        public static string Update(int ID, int AgentID, int ProductID, double Quantity, bool IsHidden, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_AgentProduct ui = dbe.tbl_AgentProduct.Where(a => a.ID == ID).SingleOrDefault();
                if (ui != null)
                {
                    ui.AgentID = AgentID;
                    ui.ProductID = ProductID;
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
        public static tbl_AgentProduct GetByID(int ID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_AgentProduct ai = dbe.tbl_AgentProduct.Where(a => a.ID == ID).SingleOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }
        public static List<tbl_AgentProduct> GetAll(string s)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_AgentProduct> ags = new List<tbl_AgentProduct>();
                ags = dbe.tbl_AgentProduct.ToList();
                return ags;
            }
        }
        public static List<tbl_AgentProduct> GetAllWithIsHidden(bool IsHidden)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_AgentProduct> ags = new List<tbl_AgentProduct>();
                ags = dbe.tbl_AgentProduct.Where(a => a.IsHidden == IsHidden).ToList();
                return ags;
            }
        }
        public static List<tbl_AgentProduct> GetByAgentID(int AgentID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_AgentProduct> ags = new List<tbl_AgentProduct>();
                ags = dbe.tbl_AgentProduct.Where(a => a.AgentID == AgentID).ToList();
                return ags;
            }
        }
        #endregion
    }
}