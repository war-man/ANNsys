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
    public class ProductVariableController
    {
        #region CRUD
        public static string Insert(int ProductID, string ParentSKU, string SKU, double Stock, int StockStatus, double Regular_Price, double CostOfGood,
            double RetailPrice, string Image, bool ManageStock, bool IsHidden, DateTime CreatedDate, string CreatedBy, int SupplierID, string SupplierName,
            double MinimumInventoryLevel, double MaximumInventoryLevel)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_ProductVariable ui = new tbl_ProductVariable();
                ui.ProductID = ProductID;
                ui.ParentSKU = ParentSKU;
                ui.SKU = SKU;
                ui.Stock = Stock;
                ui.StockStatus = StockStatus;
                ui.Regular_Price = Regular_Price;
                ui.CostOfGood = CostOfGood;
                ui.RetailPrice = RetailPrice;
                ui.Image = Image;
                ui.ManageStock = ManageStock;
                ui.IsHidden = IsHidden;
                ui.CreatedDate = CreatedDate;
                ui.CreatedBy = CreatedBy;
                ui.SupplierID = SupplierID;
                ui.SupplierName = SupplierName;
                ui.MinimumInventoryLevel = MinimumInventoryLevel;
                ui.MaximumInventoryLevel = MaximumInventoryLevel;
                dbe.tbl_ProductVariable.Add(ui);
                dbe.SaveChanges();
                int kq = ui.ID;
                return kq.ToString();
            }
        }
        public static string Update(int ID, int ProductID, string ParentSKU, string SKU, double Stock, int StockStatus, double Regular_Price, double CostOfGood,
            double RetailPrice, string Image, bool ManageStock, bool IsHidden, DateTime ModifiedDate, string ModifiedBy, int SupplierID, string SupplierName,
            double MinimumInventoryLevel, double MaximumInventoryLevel)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_ProductVariable ui = dbe.tbl_ProductVariable.Where(a => a.ID == ID).SingleOrDefault();
                if (ui != null)
                {
                    ui.ProductID = ProductID;
                    ui.ParentSKU = ParentSKU;
                    ui.SKU = SKU;
                    ui.Stock = Stock;
                    ui.StockStatus = StockStatus;
                    ui.Regular_Price = Regular_Price;
                    ui.CostOfGood = CostOfGood;
                    ui.RetailPrice = RetailPrice;
                    ui.Image = Image;
                    ui.ManageStock = ManageStock;
                    ui.IsHidden = IsHidden;
                    ui.ModifiedBy = ModifiedBy;
                    ui.ModifiedDate = ModifiedDate;
                    ui.SupplierID = SupplierID;
                    ui.SupplierName = SupplierName;
                    ui.MinimumInventoryLevel = MinimumInventoryLevel;
                    ui.MaximumInventoryLevel = MaximumInventoryLevel;
                    int kq = dbe.SaveChanges();
                    return ui.ID.ToString();
                }
                else
                    return null;
            }
        }

        public static string UpdateStockStatus(int ID, int StockStatus, bool IsHidden, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_ProductVariable ui = dbe.tbl_ProductVariable.Where(a => a.ID == ID).SingleOrDefault();
                if (ui != null)
                {
                    
                    ui.StockStatus = StockStatus;
                   
                    
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
        public static string UpdateProductID(int ID, int ProductID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_ProductVariable ui = dbe.tbl_ProductVariable.Where(a => a.ID == ID).SingleOrDefault();
                if (ui != null)
                {
                    ui.ProductID = ProductID;
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }
        public static string UpdateColorSize(int ID, string color, string size)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_ProductVariable ui = dbe.tbl_ProductVariable.Where(a => a.ID == ID).SingleOrDefault();
                if (ui != null)
                {
                    ui.color = color;
                    ui.size = size;
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }
        public static void updateSKU(int ID, string newSKU)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                var ui = dbe.tbl_ProductVariable.Where(a => a.ProductID == ID).ToList();
                ui.ForEach(a =>
                {
                    a.SKU = a.SKU.Replace(a.ParentSKU, newSKU);
                    a.ParentSKU = newSKU;
                });

                dbe.SaveChanges();
            }
        }
        public static string deleteVariable(int ParentID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_ProductVariable> ui = dbe.tbl_ProductVariable.Where(a => a.ProductID == ParentID).ToList();
                if (ui.Count() > 0)
                {
                    dbe.tbl_ProductVariable.RemoveRange(ui);
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static tbl_ProductVariable GetByID(int ID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_ProductVariable ai = dbe.tbl_ProductVariable.Where(a => a.ID == ID).SingleOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }
        public static tbl_ProductVariable GetBySKU(string SKU)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_ProductVariable ai = dbe.tbl_ProductVariable.Where(a => a.SKU == SKU).SingleOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }

        public static List<tbl_ProductVariable> GetAllBySKU(string SKU)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                var ai = dbe.tbl_ProductVariable.Where(x => x.SKU.Contains(SKU)).ToList();
                if (ai.Count() > 0)
                    return ai;
                return null;
            }
        }
        public static List<tbl_ProductVariable> GetAllByParentID(int ParentID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_ProductVariable> ai = dbe.tbl_ProductVariable.Where(a => a.ProductID == ParentID).ToList();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }
        public static List<tbl_ProductVariable> GetByParentSKU(string ParentSKU)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_ProductVariable> ai = dbe.tbl_ProductVariable.Where(a => a.ParentSKU == ParentSKU).ToList();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }
        public static List<tbl_ProductVariable> GetAll(string s)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_ProductVariable> ags = new List<tbl_ProductVariable>();
                ags = dbe.tbl_ProductVariable.Where(p => p.SKU.Contains(s)).OrderByDescending(o => o.ID).ToList();
                return ags;
            }
        }
        public static List<tbl_ProductVariable> GetAllWithHidden(bool IsHidden)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_ProductVariable> ags = new List<tbl_ProductVariable>();
                ags = dbe.tbl_ProductVariable.Where(p => p.IsHidden == IsHidden).OrderByDescending(o => o.ID).ToList();
                return ags;
            }
        }
        public static List<tbl_ProductVariable> GetProductID(int ProductID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_ProductVariable> ags = new List<tbl_ProductVariable>();
                ags = dbe.tbl_ProductVariable.Where(p => p.ProductID == ProductID && p.IsHidden == false).OrderByDescending(o => o.ID).ToList();
                return ags;
            }
        }

        public static List<tbl_ProductVariable> SearchProductID(int ProductID, string s)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_ProductVariable> ags = new List<tbl_ProductVariable>();
                ags = dbe.tbl_ProductVariable.Where(p => p.ProductID == ProductID && p.SKU.Contains(s)).OrderByDescending(o => o.ID).ToList();
                return ags;
            }
        }
        public static List<ProductVariableSQL> GetProductNameSQL(string SKU)
        {
            var list = new List<ProductVariableSQL>();
            var sql = @"Select SKU, p.ProductTitle, v.CostOfGood from tbl_ProductVariable as v";            
            sql += " LEFT OUTER JOIN tbl_Product as p ON p.ProductSKU = v.ParentSKU ";            
            sql += " where SKU = '"+SKU+"'";
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                var entity = new ProductVariableSQL();               
                if (reader["SKU"] != DBNull.Value)
                    entity.SKU = reader["SKU"].ToString();
                if (reader["ProductTitle"] != DBNull.Value)
                    entity.ProductTitle = reader["ProductTitle"].ToString();
                if (reader["CostOfGood"] != DBNull.Value)
                    entity.CostOfGood = reader["CostOfGood"].ToString();
                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public class ProductVariableSQL
        {
            public string ProductTitle { get; set; }
            public string SKU { get; set; }
            public string CostOfGood { get; set; }
        }
        #endregion
    }
}