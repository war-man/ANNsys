using IM_PJ.Models;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Controllers
{
    public class CheckWarehouseDetailController
    {
        #region CRUD
        public static string Insert(int AgentID, int CheckwarehouseID,int ProductVariableID,double Quantity_DB,double Quantity_Input, string Note, 
            DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_CheckWarehouseDetail ui = new tbl_CheckWarehouseDetail();
                ui.AgentID = AgentID;
                ui.CheckwarehouseID = CheckwarehouseID;
                ui.ProductVariableID = ProductVariableID;
                ui.Quantity_DB = Quantity_DB;
                ui.Quantity_Input = Quantity_Input;
                ui.Note = Note;
                ui.CreatedDate = CreatedDate;
                ui.CreatedBy = CreatedBy;
                dbe.tbl_CheckWarehouseDetail.Add(ui);
                int kq = dbe.SaveChanges();
                return kq.ToString();
            }
        }
        #endregion
        #region Select
        public static tbl_CheckWarehouseDetail GetByID(int ID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_CheckWarehouseDetail ai = dbe.tbl_CheckWarehouseDetail.Where(a => a.ID == ID).SingleOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }
        public static List<tbl_CheckWarehouseDetail> GetAll(string s)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_CheckWarehouseDetail> ags = new List<tbl_CheckWarehouseDetail>();
                ags = dbe.tbl_CheckWarehouseDetail.ToList();
                return ags;
            }
        }
        public static List<tbl_CheckWarehouseDetail> GetByAgentID(int AgentID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_CheckWarehouseDetail> ags = new List<tbl_CheckWarehouseDetail>();
                ags = dbe.tbl_CheckWarehouseDetail.Where(a => a.AgentID == AgentID).ToList();
                return ags;
            }
        }
        public static List<tbl_CheckWarehouseDetail> GetByCheckwarehouseID(int CheckwarehouseID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_CheckWarehouseDetail> ags = new List<tbl_CheckWarehouseDetail>();
                ags = dbe.tbl_CheckWarehouseDetail.Where(a => a.CheckwarehouseID == CheckwarehouseID).ToList();
                return ags;
            }
        }
        #endregion
    }
}