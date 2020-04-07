using IM_PJ.Models;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Controllers
{
    public class ProductVariableValueController
    {
        #region CRUD
        public static string Insert(int ProductVariableID, string ProductvariableSKU, int VariableValueID, string VariableName, string VariableValue,
           bool IsHidden, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_ProductVariableValue ui = new tbl_ProductVariableValue();
                ui.ProductVariableID = ProductVariableID;
                ui.ProductvariableSKU = ProductvariableSKU;
                ui.VariableValueID = VariableValueID;
                ui.VariableName = VariableName;
                ui.VariableValue = VariableValue;
                ui.IsHidden = IsHidden;
                ui.CreatedDate = CreatedDate;
                ui.CreatedBy = CreatedBy;
                dbe.tbl_ProductVariableValue.Add(ui);
                int kq = dbe.SaveChanges();
                return kq.ToString();
            }
        }
        public static string Update(int ID, int ProductVariableID, string ProductvariableSKU, int VariableValueID, string VariableName, string VariableValue,
           bool IsHidden, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_ProductVariableValue ui = dbe.tbl_ProductVariableValue.Where(a => a.ID == ID).SingleOrDefault();
                if (ui != null)
                {
                    ui.ProductVariableID = ProductVariableID;
                    ui.ProductvariableSKU = ProductvariableSKU;
                    ui.VariableValueID = VariableValueID;
                    ui.VariableName = VariableName;
                    ui.VariableValue = VariableValue;
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
        public static void updateSKU(int productVariableID, string parentSKU, string newSKU)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                var ui = dbe.tbl_ProductVariableValue.Where(a => a.ProductVariableID == productVariableID).ToList();
                ui.ForEach(a => a.ProductvariableSKU = a.ProductvariableSKU.Replace(parentSKU, newSKU));

                dbe.SaveChanges();
            }
        }
        public static bool DeleteByProductVariableID(int ProductVariableID)
        {
            using (var con = new inventorymanagementEntities())
            {
                var listDelete = con.tbl_ProductVariableValue.Where(x => x.ProductVariableID == ProductVariableID);

                if (listDelete != null)
                {
                    con.tbl_ProductVariableValue.RemoveRange(listDelete);
                    con.SaveChanges();
                    return true;
                }

                return false;
            }
        }
        #endregion
        #region Select
        public static tbl_ProductVariableValue GetByID(int ID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_ProductVariableValue ai = dbe.tbl_ProductVariableValue.Where(a => a.ID == ID).SingleOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }

        public static List<tbl_ProductVariableValue> GetAll(string s)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_ProductVariableValue> ags = new List<tbl_ProductVariableValue>();
                ags = dbe.tbl_ProductVariableValue.OrderBy(o => o.VariableName).ToList();
                return ags;
            }
        }
        public static List<tbl_ProductVariableValue> GetByProductVariableID(int ProductVariableID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_ProductVariableValue> ags = new List<tbl_ProductVariableValue>();
                ags = dbe.tbl_ProductVariableValue.Where(p => p.ProductVariableID == ProductVariableID).OrderBy(o => o.VariableName).ToList();
                return ags;
            }
        }

        public static List<tbl_ProductVariableValue> GetByProductVariableIDSortByName(int ProductVariableID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_ProductVariableValue> ags = new List<tbl_ProductVariableValue>();
                ags = dbe.tbl_ProductVariableValue.Where(p => p.ProductVariableID == ProductVariableID).OrderBy(o => o.VariableName).ToList();
                return ags;
            }
        }

        public static List<tbl_ProductVariableValue> GetByProductVariableSKU(string ProductVariableSKU)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_ProductVariableValue> ags = new List<tbl_ProductVariableValue>();
                ags = dbe.tbl_ProductVariableValue.Where(p => p.ProductvariableSKU == ProductVariableSKU).OrderBy(o => o.VariableName).ToList();
                return ags;
            }
        }
        #endregion
    }
}