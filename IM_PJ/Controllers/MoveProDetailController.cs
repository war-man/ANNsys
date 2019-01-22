using IM_PJ.Models;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Controllers
{
    public class MoveProDetailController
    {
        #region CRUD
        public static string Insert(int MoveProID, string SKU, int ProductID, int ProductVariableID, int ProductType, string ProductVariableDescription,
            double QuantiySend, double QuantityReceive, string Note, int SupplierID, string SupplierName, string ProductVariableName,
            string ProductVariableValue,string ProductName,string ProductImage,bool IsCount, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_MoveProdDetail ui = new tbl_MoveProdDetail();
                ui.MoveProID = MoveProID;
                ui.SKU = SKU;
                ui.ProductID = ProductID;
                ui.ProductVariableID = ProductVariableID;
                ui.ProductType = ProductType;
                ui.ProductVariableDescription = ProductVariableDescription;
                ui.QuantiySend = QuantiySend;
                ui.QuantityReceive = QuantityReceive;
                ui.Note = Note;
                ui.SupplierID = SupplierID;
                ui.SupplierName = SupplierName;

                ui.ProductVariableName = ProductVariableName;
                ui.ProductVariableValue = ProductVariableValue;
                ui.ProductName = ProductName;
                ui.ProductImage = ProductImage;
                ui.IsCount = IsCount;
                ui.CreatedDate = CreatedDate;
                ui.CreatedBy = CreatedBy;

                dbe.tbl_MoveProdDetail.Add(ui);
                int kq = dbe.SaveChanges();
                return kq.ToString();
            }
        }
        public static string UpdateIscount(int ID, bool IsCount, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_MoveProdDetail ui = dbe.tbl_MoveProdDetail.Where(a => a.ID == ID).SingleOrDefault();
                if (ui != null)
                {
                    ui.IsCount = IsCount;
                    ui.ModifiedBy = ModifiedBy;
                    ui.ModifiedDate = ModifiedDate;
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }
        public static string UpdateQuantitySend(int ID, double QuantiySend, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_MoveProdDetail ui = dbe.tbl_MoveProdDetail.Where(a => a.ID == ID).SingleOrDefault();
                if (ui != null)
                {
                    ui.QuantiySend = QuantiySend;
                    ui.ModifiedBy = ModifiedBy;
                    ui.ModifiedDate = ModifiedDate;
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }
        public static string UpdateQuantityReceive(int ID, double QuantityReceive, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_MoveProdDetail ui = dbe.tbl_MoveProdDetail.Where(a => a.ID == ID).SingleOrDefault();
                if (ui != null)
                {
                    ui.QuantityReceive = QuantityReceive;
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
                tbl_MoveProdDetail ai = dbe.tbl_MoveProdDetail.Where(a => a.ID == ID).FirstOrDefault();
                if (ai != null)
                {
                    dbe.tbl_MoveProdDetail.Remove(ai);
                    dbe.SaveChanges();
                    return "1";
                }
                else return "0";

            }
        }
        #endregion
        #region Select
        public static tbl_MoveProdDetail GetByID(int ID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_MoveProdDetail ai = dbe.tbl_MoveProdDetail.Where(a => a.ID == ID).SingleOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }
        public static List<tbl_MoveProdDetail> GetAll()
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_MoveProdDetail> ags = new List<tbl_MoveProdDetail>();
                ags = dbe.tbl_MoveProdDetail.ToList();
                return ags;
            }
        }
        public static List<tbl_MoveProdDetail> GetByMoveProID(int MoveProID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_MoveProdDetail> ags = new List<tbl_MoveProdDetail>();
                ags = dbe.tbl_MoveProdDetail.Where(c => c.MoveProID == MoveProID).ToList();
                return ags;
            }
        }        
        #endregion       
    }
}