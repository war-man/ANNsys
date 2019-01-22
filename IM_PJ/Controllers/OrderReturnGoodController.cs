using IM_PJ.Models;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Controllers
{
    public class OrderReturnGoodController
    {
        #region CRUD
        public static string Insert(int AgentID, int OrderID, int OrderDetailID, int ProductVariableValueID, double Quantity, string TotalPrice, string Note,
            DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_OrderReturnGood ui = new tbl_OrderReturnGood();
                ui.AgentID = AgentID;
                ui.OrderID = OrderID;
                ui.OrderDetailID = OrderDetailID;
                ui.ProductVariableValueID = ProductVariableValueID;
                ui.Quantity = Quantity;
                ui.TotalPrice = TotalPrice;
                ui.Note = Note;
                ui.CreatedDate = CreatedDate;
                ui.CreatedBy = CreatedBy;
                dbe.tbl_OrderReturnGood.Add(ui);
                int kq = dbe.SaveChanges();
                return kq.ToString();
            }
        }

        #endregion
        #region Select
        public static tbl_OrderReturnGood GetByID(int ID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_OrderReturnGood ai = dbe.tbl_OrderReturnGood.Where(a => a.ID == ID).SingleOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }
        public static List<tbl_OrderReturnGood> GetAll()
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_OrderReturnGood> ags = new List<tbl_OrderReturnGood>();
                ags = dbe.tbl_OrderReturnGood.OrderByDescending(o => o.ID).ToList();
                return ags;
            }
        }
        #endregion
    }
}