using IM_PJ.Models;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Controllers
{
    public class CheckWarehouseController
    {
        #region CRUD
        public static string Insert(int AgentID, int Type, string Note, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_CheckWarehouse ui = new tbl_CheckWarehouse();
                ui.AgentID = AgentID;
                ui.Type = Type;
                ui.Note = Note;
                ui.CreatedDate = CreatedDate;
                ui.CreatedBy = CreatedBy;
                dbe.tbl_CheckWarehouse.Add(ui);
                int kq = dbe.SaveChanges();
                return kq.ToString();
            }
        }       
        #endregion
        #region Select
        public static tbl_CheckWarehouse GetByID(int ID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_CheckWarehouse ai = dbe.tbl_CheckWarehouse.Where(a => a.ID == ID).SingleOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }
        public static List<tbl_CheckWarehouse> GetAll(string s)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_CheckWarehouse> ags = new List<tbl_CheckWarehouse>();
                ags = dbe.tbl_CheckWarehouse.ToList();
                return ags;
            }
        }
        public static List<tbl_CheckWarehouse> GetByAgentID(int AgentID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_CheckWarehouse> ags = new List<tbl_CheckWarehouse>();
                ags = dbe.tbl_CheckWarehouse.Where(a => a.AgentID == AgentID).ToList();
                return ags;
            }
        }        
        #endregion

        public static List<CheckWarehouse> getAll()
        {
            using (var con = new inventorymanagementEntities())
            {
                return con.CheckWarehouses
                    .OrderByDescending(o => o.CreatedDate)
                    .ToList();
            }
        }

        public static bool closeCheckHouse(int id, DateTime modifiedDate, string modifiedBy)
        {
            using (var con = new inventorymanagementEntities())
            {
                var checkHouse = con.CheckWarehouses.Where(x => x.ID == id).FirstOrDefault();

                if (checkHouse != null)
                {
                    checkHouse.Active = false;
                    checkHouse.ModifiedDate = modifiedDate;
                    checkHouse.ModifiedBy = modifiedBy;
                    con.SaveChanges();

                    return true;
                }

                return false;
            }
        }
    }
}