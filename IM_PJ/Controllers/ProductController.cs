using IM_PJ.Models;
using MB.Extensions;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using WebUI.Business;

namespace IM_PJ.Controllers
{
    public class ProductController
    {
        static string checkSlug(string slug)
        {
            using (var con = new inventorymanagementEntities())
            {
                var product = con.tbl_Product.Where(x => x.Slug == slug).FirstOrDefault();
                if (product != null)
                {
                    return checkSlug(slug + "-1");
                }
                else
                {
                    return slug;
                }
            }
        }

        #region CRUD
        public static string Insert(int CategoryID, int ProductOldID, string ProductTitle, string ProductContent, string ProductSKU, double ProductStock,
            int StockStatus, bool ManageStock, double Regular_Price, double CostOfGood, double Retail_Price, string ProductImage, int ProductType,
            bool IsHidden, DateTime CreatedDate, string CreatedBy, int SupplierID, string SupplierName, string Materials,
            double MinimumInventoryLevel, double MaximumInventoryLevel, int ProductStyle, int ShowHomePage, string mainColor)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Product ui = new tbl_Product();
                ui.CategoryID = CategoryID;
                ui.ProductOldID = ProductOldID;
                ui.ProductTitle = ProductTitle;
                ui.ProductContent = ProductContent;
                ui.ProductSKU = ProductSKU;
                ui.ProductStock = ProductStock;
                ui.StockStatus = StockStatus;
                ui.ManageStock = ManageStock;
                ui.Regular_Price = Regular_Price;
                ui.CostOfGood = CostOfGood;
                ui.Retail_Price = Retail_Price;
                ui.ProductImage = ProductImage;
                ui.ProductType = ProductType;
                ui.IsHidden = IsHidden;
                ui.CreatedDate = CreatedDate;
                ui.CreatedBy = CreatedBy;
                ui.SupplierID = SupplierID;
                ui.SupplierName = SupplierName;
                ui.Materials = Materials;
                ui.MinimumInventoryLevel = MinimumInventoryLevel;
                ui.MaximumInventoryLevel = MaximumInventoryLevel;
                ui.ProductStyle = ProductStyle;
                ui.ShowHomePage = ShowHomePage;
                ui.WebPublish = true;
                ui.WebUpdate = CreatedDate;
                ui.UnSignedTitle = UnSign.convert(ProductTitle);
                ui.Slug = checkSlug(Slug.ConvertToSlug(ProductTitle));
                ui.Color = mainColor;

                dbe.tbl_Product.Add(ui);
                dbe.SaveChanges();
                int kq = ui.ID;
                return kq.ToString();
            }
        }
        public static string Update(int ID, int CategoryID, int ProductOldID, string ProductTitle, string ProductContent, string ProductSKU, double ProductStock,
            int StockStatus, bool ManageStock, double Regular_Price, double CostOfGood, double Retail_Price, string ProductImage, int ProductType,
            bool IsHidden, DateTime ModifiedDate, string ModifiedBy, int SupplierID, string SupplierName, string Materials,
            double MinimumInventoryLevel, double MaximumInventoryLevel, string ProductImageClean, string mainColor)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Product ui = dbe.tbl_Product.Where(a => a.ID == ID).SingleOrDefault();
                if (ui != null)
                {
                    ui.CategoryID = CategoryID;
                    ui.ProductOldID = ProductOldID;
                    ui.ProductTitle = ProductTitle;
                    ui.ProductContent = ProductContent;
                    ui.ProductSKU = ProductSKU;
                    if (ProductStock > 0)
                        ui.ProductStock = ProductStock;
                    if (StockStatus > 0)
                        ui.StockStatus = StockStatus;
                    ui.ManageStock = ManageStock;
                    ui.Regular_Price = Regular_Price;
                    ui.CostOfGood = CostOfGood;
                    ui.Retail_Price = Retail_Price;
                    ui.ProductImage = ProductImage;
                    ui.ProductType = ProductType;
                    ui.IsHidden = IsHidden;
                    ui.ModifiedBy = ModifiedBy;
                    ui.ModifiedDate = ModifiedDate;
                    ui.SupplierID = SupplierID;
                    ui.SupplierName = SupplierName;
                    ui.Materials = Materials;
                    ui.MinimumInventoryLevel = MinimumInventoryLevel;
                    ui.MaximumInventoryLevel = MaximumInventoryLevel;
                    ui.ProductImageClean = ProductImageClean;
                    ui.UnSignedTitle = UnSign.convert(ProductTitle);
                    ui.Color = mainColor;

                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }
        public static string updateSKU(int ID, string newSKU)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Product ui = dbe.tbl_Product.Where(a => a.ID == ID).SingleOrDefault();
                if (ui != null)
                {
                    ui.ProductSKU = newSKU;
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }
        public static string UpdateImage(int ID, string ProductImage)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Product ui = dbe.tbl_Product.Where(a => a.ID == ID).SingleOrDefault();
                if (ui != null)
                {
                    ui.ProductImage = ProductImage;
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }

        public static string UpdateImageClean(int ID, string ProductImageClean)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Product ui = dbe.tbl_Product.Where(a => a.ID == ID).SingleOrDefault();
                if (ui != null)
                {
                    ui.ProductImageClean = ProductImageClean;
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }

        public static string UpdateStockStatus(string SKU, int StockStatus, bool IsHidden, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Product ui = dbe.tbl_Product.Where(a => a.ProductSKU == SKU).SingleOrDefault();
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
        public static string updateShowHomePage(int id, int value)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Product ui = dbe.tbl_Product.Where(a => a.ID == id).SingleOrDefault();
                if (ui != null)
                {
                    ui.ShowHomePage = value;
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }
        public static string updateWebPublish(int id, bool value)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Product ui = dbe.tbl_Product.Where(a => a.ID == id).SingleOrDefault();
                if (ui != null)
                {
                    ui.WebPublish = value;
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }
        public static string updateWebUpdate(int id)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Product ui = dbe.tbl_Product.Where(a => a.ID == id).SingleOrDefault();
                if (ui != null)
                {
                    ui.WebUpdate = DateTime.Now;
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }
        public static string deleteProduct(int id)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Product ui = dbe.tbl_Product.Where(a => a.ID == id).SingleOrDefault();
                if (ui != null)
                {
                    dbe.tbl_Product.Remove(ui);
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static List<tbl_Product> GetByTextSearchIsHidden(string s, bool IsHidden)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_Product> ags = new List<tbl_Product>();
                ags = dbe.tbl_Product.Where(p => p.ProductTitle.Contains(s) && p.IsHidden == IsHidden).OrderByDescending(o => o.ID).ToList();
                return ags;
            }
        }
        public static tbl_Product GetByID(int ID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Product ai = dbe.tbl_Product.Where(a => a.ID == ID).SingleOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }
        public static tbl_Product GetByVariableID(int ID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_ProductVariable variable = dbe.tbl_ProductVariable.Where(a => a.ID == ID).SingleOrDefault();
                tbl_Product ai = dbe.tbl_Product.Where(a => a.ID == variable.ProductID).SingleOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }
        public static tbl_Product GetByVariableSKU(string SKU)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_ProductVariable variable = dbe.tbl_ProductVariable.Where(a => a.SKU == SKU).SingleOrDefault();
                tbl_Product ai = dbe.tbl_Product.Where(a => a.ID == variable.ProductID).SingleOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }
        public static tbl_Product GetBySKU(string SKU)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Product ai = dbe.tbl_Product.Where(a => a.ProductSKU == SKU).SingleOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }
        public static List<tbl_Product> GetAll(string s)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_Product> ags = new List<tbl_Product>();
                ags = dbe.tbl_Product.Where(p => p.ProductTitle.Contains(s)).OrderByDescending(o => o.ID).ToList();
                return ags;
            }
        }
        public static List<tbl_Product> GetByCategoryID(int CategoryID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_Product> ags = new List<tbl_Product>();
                ags = dbe.tbl_Product.Where(p => p.CategoryID == CategoryID).OrderByDescending(o => o.ID).ToList();
                return ags;
            }
        }
        
        public static List<ProductSQL> GetProductReport(int categoryID)
        {
            var list = new List<ProductSQL>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("BEGIN");

            if (categoryID > 0)
            {
                sql.AppendLine(String.Empty);
                sql.AppendLine("WITH category AS(");
                sql.AppendLine("    SELECT");
                sql.AppendLine("            ID");
                sql.AppendLine("    ,       CategoryName");
                sql.AppendLine("    ,       ParentID");
                sql.AppendLine("    FROM");
                sql.AppendLine("            tbl_Category");
                sql.AppendLine("    WHERE");
                sql.AppendLine("            1 = 1");
                sql.AppendLine("    AND     ID = " + categoryID);
                sql.AppendLine(")");
                sql.AppendLine("SELECT");
                sql.AppendLine("        ID");
                sql.AppendLine(",       CategoryName");
                sql.AppendLine(",       ParentID");
                sql.AppendLine("INTO #category");
                sql.AppendLine("FROM category");
            }

            sql.AppendLine(String.Empty);
            sql.AppendLine("    SELECT");
            sql.AppendLine("            PRD.*");
            sql.AppendLine("    INTO #Product");
            sql.AppendLine("    FROM");
            sql.AppendLine("            tbl_Product AS PRD");
            sql.AppendLine("    WHERE");
            sql.AppendLine("            1 = 1");

            if (categoryID > 0)
            {
                sql.AppendLine("    AND EXISTS(");
                sql.AppendLine("            SELECT");
                sql.AppendLine("                    NULL AS DUMMY");
                sql.AppendLine("            FROM");
                sql.AppendLine("                    #category");
                sql.AppendLine("            WHERE");
                sql.AppendLine("                    ID = PRD.CategoryID");
                sql.AppendLine("    )");
            }

            sql.AppendLine("     ORDER BY");
            sql.AppendLine("             PRD.ProductStyle");
            sql.AppendLine("     ,       PRD.ID");
            sql.AppendLine("     ;");
            sql.AppendLine(String.Empty);
            sql.AppendLine("     CREATE INDEX [ID_PROCDUCT] ON #Product([ProductStyle], [ID])");
            sql.AppendLine(String.Empty);
            sql.AppendLine("     SELECT");
            sql.AppendLine("             STM.ProductID");
            sql.AppendLine("     ,       STM.Quantity");
            sql.AppendLine("     ,       STM.QuantityCurrent");
            sql.AppendLine("     ,       STM.Type");
            sql.AppendLine("     ,       STM.CreatedDate");
            sql.AppendLine("     ,       STM.ParentID");
            sql.AppendLine("     INTO #StockProduct");
            sql.AppendLine("     FROM");
            sql.AppendLine("             #Product AS PRD");
            sql.AppendLine("     INNER JOIN tbl_StockManager AS STM");
            sql.AppendLine("         ON  PRD.ProductStyle = 1");
            sql.AppendLine("         AND PRD.ID = STM.ParentID");
            sql.AppendLine("     ORDER BY");
            sql.AppendLine("             STM.ProductID");
            sql.AppendLine("     ,       STM.CreatedDate");
            sql.AppendLine("     ;");
            sql.AppendLine(String.Empty);
            sql.AppendLine("     CREATE INDEX [ID_PROCDUCT] ON #StockProduct(");
            sql.AppendLine("             [ProductID] ASC");
            sql.AppendLine("     ,       [CreatedDate] DESC");
            sql.AppendLine("     )");
            sql.AppendLine(String.Empty);
            sql.AppendLine("     SELECT");
            sql.AppendLine("             STM.ProductVariableID");
            sql.AppendLine("     ,       STM.Quantity");
            sql.AppendLine("     ,       STM.QuantityCurrent");
            sql.AppendLine("     ,       STM.Type");
            sql.AppendLine("     ,       STM.CreatedDate");
            sql.AppendLine("     ,       STM.ParentID");
            sql.AppendLine("     INTO #StockProductVariable");
            sql.AppendLine("     FROM");
            sql.AppendLine("             #Product AS PRD");
            sql.AppendLine("     INNER JOIN tbl_StockManager AS STM");
            sql.AppendLine("         ON  PRD.ProductStyle = 2");
            sql.AppendLine("         AND PRD.ID = STM.ParentID");
            sql.AppendLine("     ORDER BY");
            sql.AppendLine("             STM.ProductVariableID");
            sql.AppendLine("     ,       STM.CreatedDate");
            sql.AppendLine("     ;");
            sql.AppendLine(String.Empty);
            sql.AppendLine("     CREATE INDEX [ID_PROCDUCT] ON #StockProductVariable(");
            sql.AppendLine("             [ProductVariableID] ASC");
            sql.AppendLine("     ,       [CreatedDate] DESC");
            sql.AppendLine("     )");
            sql.AppendLine(String.Empty);
            sql.AppendLine("     SELECT");
            sql.AppendLine("             PRQ.ProductStyle");
            sql.AppendLine("     ,       PRQ.ParentID");
            sql.AppendLine("     ,       SUM(ISNULL(PRQ.QuantityLeft, 0)) AS QuantityLeft");
            sql.AppendLine("     INTO #ProductQuantity");
            sql.AppendLine("     FROM (");
            sql.AppendLine("         SELECT");
            sql.AppendLine("                 1 AS ProductStyle");
            sql.AppendLine("         ,       STP.ParentID");
            sql.AppendLine("         ,       (");
            sql.AppendLine("                     CASE STP.Type");
            sql.AppendLine("                         WHEN 1");
            sql.AppendLine("                             THEN STP.QuantityCurrent + STP.Quantity");
            sql.AppendLine("                         WHEN 2");
            sql.AppendLine("                             THEN STP.QuantityCurrent - STP.Quantity");
            sql.AppendLine("                         ELSE");
            sql.AppendLine("                             0");
            sql.AppendLine("                     END");
            sql.AppendLine("                 ) AS QuantityLeft");
            sql.AppendLine("         FROM ");
            sql.AppendLine("                 #StockProduct AS STP");
            sql.AppendLine("         INNER JOIN (");
            sql.AppendLine("                 SELECT");
            sql.AppendLine("                         ProductID");
            sql.AppendLine("                 ,       MAX(CreatedDate) AS CreatedDate");
            sql.AppendLine("                 FROM");
            sql.AppendLine("                         #StockProduct");
            sql.AppendLine("                 GROUP BY");
            sql.AppendLine("                         ProductID");
            sql.AppendLine("             ) AS SPM");
            sql.AppendLine("             ON  STP.ProductID = SPM.ProductID");
            sql.AppendLine("             AND STP.CreatedDate = SPM.CreatedDate");
            sql.AppendLine(String.Empty);
            sql.AppendLine("         UNION ALL");
            sql.AppendLine(String.Empty);
            sql.AppendLine("         SELECT");
            sql.AppendLine("                 2 AS ProductStyle");
            sql.AppendLine("         ,       STP.ParentID");
            sql.AppendLine("         ,       (");
            sql.AppendLine("                     CASE STP.Type");
            sql.AppendLine("                         WHEN 1");
            sql.AppendLine("                             THEN STP.QuantityCurrent + STP.Quantity");
            sql.AppendLine("                         WHEN 2");
            sql.AppendLine("                             THEN STP.QuantityCurrent - STP.Quantity");
            sql.AppendLine("                         ELSE");
            sql.AppendLine("                             0");
            sql.AppendLine("                     END");
            sql.AppendLine("                 ) AS QuantityLeft");
            sql.AppendLine("         FROM ");
            sql.AppendLine("                 #StockProductVariable AS STP");
            sql.AppendLine("         INNER JOIN (");
            sql.AppendLine("                 SELECT");
            sql.AppendLine("                         ProductVariableID");
            sql.AppendLine("                 ,       MAX(CreatedDate) AS CreatedDate");
            sql.AppendLine("                 FROM");
            sql.AppendLine("                         #StockProductVariable");
            sql.AppendLine("                 GROUP BY");
            sql.AppendLine("                         ProductVariableID");
            sql.AppendLine("             ) AS SPM");
            sql.AppendLine("             ON  STP.ProductVariableID = SPM.ProductVariableID");
            sql.AppendLine("             AND STP.CreatedDate = SPM.CreatedDate");
            sql.AppendLine("     ) AS PRQ");
            sql.AppendLine("     GROUP BY");
            sql.AppendLine("             PRQ.ProductStyle");
            sql.AppendLine("     ,       PRQ.ParentID");
            sql.AppendLine("     ;");
            sql.AppendLine(String.Empty);
            sql.AppendLine("     CREATE INDEX [ID_PROCDUCT] ON #ProductQuantity([ProductStyle], [ParentID])");
            sql.AppendLine(String.Empty);
            sql.AppendLine("     SELECT");
            sql.AppendLine("             p.ProductStyle AS ProductStyle");
            sql.AppendLine("     ,       c.CategoryName");
            sql.AppendLine("     ,       p.*");
            sql.AppendLine("     ,       PRQ.QuantityLeft");
            sql.AppendLine("     FROM");
            sql.AppendLine("             #Product AS p");
            sql.AppendLine("     LEFT JOIN #ProductQuantity AS PRQ");
            sql.AppendLine("         ON  p.ProductStyle = PRQ.ProductStyle");
            sql.AppendLine("         AND p.ID = PRQ.ParentID");
            sql.AppendLine("     LEFT JOIN (");
            sql.AppendLine("             SELECT");
            sql.AppendLine("                     ID");
            sql.AppendLine("             ,       CategoryName");
            sql.AppendLine("             FROM");
            sql.AppendLine("                     dbo.tbl_Category");
            sql.AppendLine("     ) AS c");
            sql.AppendLine("     ON c.ID = p.CategoryID");
            sql.AppendLine("     ORDER BY");
            sql.AppendLine("             p.ID");
            sql.AppendLine("     ;");
            sql.AppendLine(String.Empty);
            sql.AppendLine(" END");

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql.ToString());

            while (reader.Read())
            {
                double quantityLeft = 0;

                var entity = new ProductSQL();

                entity.ID = Convert.ToInt32(reader["ID"]);

                if (reader["QuantityLeft"] != DBNull.Value)
                {
                    quantityLeft = Convert.ToDouble(reader["QuantityLeft"]);
                }

                entity.TotalProductInstockQuantityLeft = quantityLeft;

                if (reader["Regular_Price"] != DBNull.Value)
                    entity.RegularPrice = Convert.ToDouble(reader["Regular_Price"].ToString());
                if (reader["CostOfGood"] != DBNull.Value)
                    entity.CostOfGood = Convert.ToDouble(reader["CostOfGood"].ToString());

                list.Add(entity);
            }

            reader.Close();
            return list.ToList();
        }
        public static List<ProductSQL> GetProductAPI(int categoryID, int limit, int showHomePage, int minQuantity, int changeProductName)
        {
            var list = new List<ProductSQL>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("BEGIN");

            if (categoryID > 0)
            {
                sql.AppendLine(String.Empty);
                sql.AppendLine("WITH category AS(");
                sql.AppendLine("    SELECT");
                sql.AppendLine("            ID");
                sql.AppendLine("    ,       CategoryName");
                sql.AppendLine("    ,       ParentID");
                sql.AppendLine("    FROM");
                sql.AppendLine("            tbl_Category");
                sql.AppendLine("    WHERE");
                sql.AppendLine("            1 = 1");
                sql.AppendLine("    AND     ID = " + categoryID);
                sql.AppendLine(")");
                sql.AppendLine("SELECT");
                sql.AppendLine("        ID");
                sql.AppendLine(",       CategoryName");
                sql.AppendLine(",       ParentID");
                sql.AppendLine("INTO #category");
                sql.AppendLine("FROM category");
            }

            sql.AppendLine(String.Empty);
            sql.AppendLine("    SELECT");
            sql.AppendLine("            PRD.*");
            sql.AppendLine("    INTO #Product");
            sql.AppendLine("    FROM");
            sql.AppendLine("            tbl_product AS PRD");
            sql.AppendLine("    WHERE");
            sql.AppendLine("            1 = 1");

            if (categoryID > 0)
            {
                sql.AppendLine("    AND EXISTS(");
                sql.AppendLine("            SELECT");
                sql.AppendLine("                    NULL AS DUMMY");
                sql.AppendLine("            FROM");
                sql.AppendLine("                    #category");
                sql.AppendLine("            WHERE");
                sql.AppendLine("                    ID = PRD.CategoryID");
                sql.AppendLine("    )");
            }

            sql.AppendLine("     ORDER BY");
            sql.AppendLine("             PRD.ProductStyle");
            sql.AppendLine("     ,       PRD.ID");
            sql.AppendLine("     ;");
            sql.AppendLine(String.Empty);
            sql.AppendLine("     CREATE INDEX [ID_PROCDUCT] ON #Product([ProductStyle], [ID])");
            sql.AppendLine(String.Empty);
            sql.AppendLine("     SELECT");
            sql.AppendLine("             STM.ProductID");
            sql.AppendLine("     ,       STM.Quantity");
            sql.AppendLine("     ,       STM.QuantityCurrent");
            sql.AppendLine("     ,       STM.Type");
            sql.AppendLine("     ,       STM.CreatedDate");
            sql.AppendLine("     ,       STM.ParentID");
            sql.AppendLine("     INTO #StockProduct");
            sql.AppendLine("     FROM");
            sql.AppendLine("             #Product AS PRD");
            sql.AppendLine("     INNER JOIN tbl_StockManager AS STM");
            sql.AppendLine("         ON  PRD.ProductStyle = 1");
            sql.AppendLine("         AND PRD.ID = STM.ParentID");
            sql.AppendLine("     ORDER BY");
            sql.AppendLine("             STM.ProductID");
            sql.AppendLine("     ,       STM.CreatedDate");
            sql.AppendLine("     ;");
            sql.AppendLine(String.Empty);
            sql.AppendLine("     CREATE INDEX [ID_PROCDUCT] ON #StockProduct(");
            sql.AppendLine("             [ProductID] ASC");
            sql.AppendLine("     ,       [CreatedDate] DESC");
            sql.AppendLine("     )");
            sql.AppendLine(String.Empty);
            sql.AppendLine("     SELECT");
            sql.AppendLine("             STM.ProductVariableID");
            sql.AppendLine("     ,       STM.Quantity");
            sql.AppendLine("     ,       STM.QuantityCurrent");
            sql.AppendLine("     ,       STM.Type");
            sql.AppendLine("     ,       STM.CreatedDate");
            sql.AppendLine("     ,       STM.ParentID");
            sql.AppendLine("     INTO #StockProductVariable");
            sql.AppendLine("     FROM");
            sql.AppendLine("             #Product AS PRD");
            sql.AppendLine("     INNER JOIN tbl_StockManager AS STM");
            sql.AppendLine("         ON  PRD.ProductStyle = 2");
            sql.AppendLine("         AND PRD.ID = STM.ParentID");
            sql.AppendLine("     ORDER BY");
            sql.AppendLine("             STM.ProductVariableID");
            sql.AppendLine("     ,       STM.CreatedDate");
            sql.AppendLine("     ;");
            sql.AppendLine(String.Empty);
            sql.AppendLine("     CREATE INDEX [ID_PROCDUCT] ON #StockProductVariable(");
            sql.AppendLine("             [ProductVariableID] ASC");
            sql.AppendLine("     ,       [CreatedDate] DESC");
            sql.AppendLine("     )");
            sql.AppendLine(String.Empty);
            sql.AppendLine("     SELECT");
            sql.AppendLine("             PRQ.ProductStyle");
            sql.AppendLine("     ,       PRQ.ParentID");
            sql.AppendLine("     ,       SUM(ISNULL(PRQ.QuantityLeft, 0)) AS QuantityLeft");
            sql.AppendLine("     INTO #ProductQuantity");
            sql.AppendLine("     FROM (");
            sql.AppendLine("         SELECT");
            sql.AppendLine("                 1 AS ProductStyle");
            sql.AppendLine("         ,       STP.ParentID");
            sql.AppendLine("         ,       (");
            sql.AppendLine("                     CASE STP.Type");
            sql.AppendLine("                         WHEN 1");
            sql.AppendLine("                             THEN STP.QuantityCurrent + STP.Quantity");
            sql.AppendLine("                         WHEN 2");
            sql.AppendLine("                             THEN STP.QuantityCurrent - STP.Quantity");
            sql.AppendLine("                         ELSE");
            sql.AppendLine("                             0");
            sql.AppendLine("                     END");
            sql.AppendLine("                 ) AS QuantityLeft");
            sql.AppendLine("         FROM ");
            sql.AppendLine("                 #StockProduct AS STP");
            sql.AppendLine("         INNER JOIN (");
            sql.AppendLine("                 SELECT");
            sql.AppendLine("                         ProductID");
            sql.AppendLine("                 ,       MAX(CreatedDate) AS CreatedDate");
            sql.AppendLine("                 FROM");
            sql.AppendLine("                         #StockProduct");
            sql.AppendLine("                 GROUP BY");
            sql.AppendLine("                         ProductID");
            sql.AppendLine("             ) AS SPM");
            sql.AppendLine("             ON  STP.ProductID = SPM.ProductID");
            sql.AppendLine("             AND STP.CreatedDate = SPM.CreatedDate");
            sql.AppendLine(String.Empty);
            sql.AppendLine("         UNION ALL");
            sql.AppendLine(String.Empty);
            sql.AppendLine("         SELECT");
            sql.AppendLine("                 2 AS ProductStyle");
            sql.AppendLine("         ,       STP.ParentID");
            sql.AppendLine("         ,       (");
            sql.AppendLine("                     CASE STP.Type");
            sql.AppendLine("                         WHEN 1");
            sql.AppendLine("                             THEN STP.QuantityCurrent + STP.Quantity");
            sql.AppendLine("                         WHEN 2");
            sql.AppendLine("                             THEN STP.QuantityCurrent - STP.Quantity");
            sql.AppendLine("                         ELSE");
            sql.AppendLine("                             0");
            sql.AppendLine("                     END");
            sql.AppendLine("                 ) AS QuantityLeft");
            sql.AppendLine("         FROM ");
            sql.AppendLine("                 #StockProductVariable AS STP");
            sql.AppendLine("         INNER JOIN (");
            sql.AppendLine("                 SELECT");
            sql.AppendLine("                         ProductVariableID");
            sql.AppendLine("                 ,       MAX(CreatedDate) AS CreatedDate");
            sql.AppendLine("                 FROM");
            sql.AppendLine("                         #StockProductVariable");
            sql.AppendLine("                 GROUP BY");
            sql.AppendLine("                         ProductVariableID");
            sql.AppendLine("             ) AS SPM");
            sql.AppendLine("             ON  STP.ProductVariableID = SPM.ProductVariableID");
            sql.AppendLine("             AND STP.CreatedDate = SPM.CreatedDate");
            sql.AppendLine("     ) AS PRQ");
            sql.AppendLine("     GROUP BY");
            sql.AppendLine("             PRQ.ProductStyle");
            sql.AppendLine("     ,       PRQ.ParentID");
            sql.AppendLine("     ;");
            sql.AppendLine(String.Empty);
            sql.AppendLine("     CREATE INDEX [ID_PROCDUCT] ON #ProductQuantity([ProductStyle], [ParentID])");
            sql.AppendLine(String.Empty);
            sql.AppendLine("     SELECT");
            sql.AppendLine("             p.ProductStyle AS ProductStyle");
            sql.AppendLine("     ,       c.CategoryName");
            sql.AppendLine("     ,       p.*");
            sql.AppendLine("     ,       PRQ.QuantityLeft");
            sql.AppendLine("     FROM");
            sql.AppendLine("             #Product AS p");
            sql.AppendLine("     LEFT JOIN #ProductQuantity AS PRQ");
            sql.AppendLine("         ON  p.ProductStyle = PRQ.ProductStyle");
            sql.AppendLine("         AND p.ID = PRQ.ParentID");
            sql.AppendLine("     LEFT JOIN (");
            sql.AppendLine("             SELECT");
            sql.AppendLine("                     ID");
            sql.AppendLine("             ,       CategoryName");
            sql.AppendLine("             FROM");
            sql.AppendLine("                     dbo.tbl_Category");
            sql.AppendLine("     ) AS c");
            sql.AppendLine("     ON c.ID = p.CategoryID");
            sql.AppendLine("     ORDER BY");
            sql.AppendLine("             p.ID");
            sql.AppendLine("     ;");
            sql.AppendLine(String.Empty);
            sql.AppendLine(" END");

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql.ToString());

            while (reader.Read())
            {
                double quantityLeft = 0;

                var entity = new ProductSQL();

                entity.ID = Convert.ToInt32(reader["ID"]);

                bool check = true;

                // check min stock

                if (reader["QuantityLeft"] != DBNull.Value)
                {
                    quantityLeft = Convert.ToDouble(reader["QuantityLeft"]);

                    if (quantityLeft >= minQuantity)
                    {
                        // check show homepage

                        if (showHomePage == 1)
                        {
                            if (string.IsNullOrEmpty(reader["ProductImage"].ToString()) || reader["ShowHomePage"].ToString().ToInt(0) == 0)
                            {
                                check = false;
                            }
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(reader["ProductImage"].ToString()))
                            {
                                check = false;
                            }
                        }
                    }
                    else
                    {
                        check = false;
                    }
                }
                else
                {
                    
                    // check show homepage

                    if (showHomePage == 1)
                    {
                        if (string.IsNullOrEmpty(reader["ProductImage"].ToString()) || reader["ShowHomePage"].ToString().ToInt(0) == 0)
                        {
                            check = false;
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(reader["ProductImage"].ToString()))
                        {
                            check = false;
                        }
                    }
                }
                

                if (check == true)
                {
                    entity.ProductImage = reader["ProductImage"].ToString();

                    if (!string.IsNullOrEmpty(reader["ProductImageClean"].ToString()))
                    {
                        entity.ProductImageClean = reader["ProductImageClean"].ToString();
                    }

                    if (changeProductName == 1)
                    {
                        if (reader["CategoryName"] != DBNull.Value)
                        {
                            entity.ProductTitle = reader["CategoryName"].ToString() + ' ' + reader["ProductSKU"].ToString();
                        }
                        else
                        {
                            if (reader["ProductTitle"] != DBNull.Value)
                            {
                                entity.ProductTitle = reader["ProductTitle"].ToString() + ' ' + reader["ProductSKU"].ToString();
                            }
                        }
                            
                    }
                    else
                    {
                        if (reader["ProductTitle"] != DBNull.Value)
                        {
                            entity.ProductTitle = reader["ProductTitle"].ToString();
                        }
                    }
                    
                    // get SKU
                    if (reader["ProductSKU"] != DBNull.Value)
                        entity.ProductSKU = reader["ProductSKU"].ToString();

                    // sản phẩm đã nhập kho
                    if (reader["QuantityLeft"] != DBNull.Value)
                    {
                        quantityLeft = Convert.ToDouble(reader["QuantityLeft"]);

                        if (quantityLeft > 0)
                        {
                            entity.StockStatus = 1;
                        }
                        else
                        {
                            entity.StockStatus = 2;
                        }
                    }
                    else
                    {
                        // Sản phẩm chưa nhập kho thì cho kho = 1 để khi đồng bộ sản phẩm thì trạng thái là còn hàng
                        quantityLeft = 1;
                        entity.StockStatus = 1;
                    }

                    entity.TotalProductInstockQuantityLeft = quantityLeft;

                    if (reader["Regular_Price"] != DBNull.Value)
                        entity.RegularPrice = Convert.ToDouble(reader["Regular_Price"].ToString());
                    if (reader["CostOfGood"] != DBNull.Value)
                        entity.CostOfGood = Convert.ToDouble(reader["CostOfGood"].ToString());
                    if (reader["Retail_Price"] != DBNull.Value)
                        entity.RetailPrice = Convert.ToDouble(reader["Retail_Price"].ToString());
                    if (reader["CategoryName"] != DBNull.Value)
                        entity.CategoryName = reader["CategoryName"].ToString();
                    if (reader["CategoryID"] != DBNull.Value)
                        entity.CategoryID = reader["CategoryID"].ToString().ToInt(0);
                    if (reader["CreatedDate"] != DBNull.Value)
                        entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                    if (reader["ProductContent"] != DBNull.Value)
                        entity.ProductContent = reader["ProductContent"].ToString();
                    if (reader["ProductStyle"] != DBNull.Value)
                        entity.ProductStyle = reader["ProductStyle"].ToString().ToInt(0);

                    list.Add(entity);
                }
            }

            reader.Close();
            return list.OrderByDescending(x => x.ID).Take(limit).ToList();
        }

        public static List<ProductSQL> GetAllSql(int categoryID, string textsearch, string CreatedDate = "", string ShowHomePage = "", string WebPublish = "", string strColor = "", string strSize = "")
        {
            var list = new List<ProductSQL>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("BEGIN");

            if (categoryID > 0)
            {
                sql.AppendLine(String.Empty);
                sql.AppendLine("WITH category AS(");
                sql.AppendLine("    SELECT");
                sql.AppendLine("            ID");
                sql.AppendLine("    ,       CategoryName");
                sql.AppendLine("    ,       ParentID");
                sql.AppendLine("    FROM");
                sql.AppendLine("            tbl_Category");
                sql.AppendLine("    WHERE");
                sql.AppendLine("            1 = 1");
                sql.AppendLine("    AND     ID = " + categoryID);
                sql.AppendLine("");
                sql.AppendLine("    UNION ALL");
                sql.AppendLine("");
                sql.AppendLine("    SELECT");
                sql.AppendLine("            CHI.ID");
                sql.AppendLine("    ,       CHI.CategoryName");
                sql.AppendLine("    ,       CHI.ParentID");
                sql.AppendLine("    FROM");
                sql.AppendLine("            category AS PAR");
                sql.AppendLine("    INNER JOIN tbl_Category AS CHI");
                sql.AppendLine("        ON PAR.ID = CHI.ParentID");
                sql.AppendLine(")");
                sql.AppendLine("SELECT");
                sql.AppendLine("        ID");
                sql.AppendLine(",       CategoryName");
                sql.AppendLine(",       ParentID");
                sql.AppendLine("INTO #category");
                sql.AppendLine("FROM category;");
            }

            // Filter by color product
            if (!String.IsNullOrEmpty(strColor))
            {
                sql.AppendLine(String.Empty);
                sql.AppendLine("    With VariableValue AS (");
                sql.AppendLine("        SELECT");
                sql.AppendLine("            *");
                sql.AppendLine("        FROM");
                sql.AppendLine("            tbl_VariableValue");
                sql.AppendLine("        WHERE");
                sql.AppendLine(String.Format("        VariableID = 1 AND  LOWER(VariableValue) LIKE N'%{0}%'", strColor.ToLower()));
                sql.AppendLine("    )");
                sql.AppendLine("    SELECT");
                sql.AppendLine("        PVA.ProductID AS ID");
                sql.AppendLine("    INTO #ProductColor");
                sql.AppendLine("    FROM ");
                sql.AppendLine("    (");
                sql.AppendLine("        SELECT");
                sql.AppendLine("            PVV.ProductVariableID");
                sql.AppendLine("        FROM");
                sql.AppendLine("            VariableValue AS VAV");
                sql.AppendLine("        INNER JOIN tbl_ProductVariableValue AS PVV");
                sql.AppendLine("        ON  VAV.ID = PVV.VariableValueID");
                sql.AppendLine("        GROUP BY PVV.ProductVariableID");
                sql.AppendLine("    ) AS PVF");
                sql.AppendLine("    INNER JOIN tbl_ProductVariable AS PVA");
                sql.AppendLine("    ON  PVF.ProductVariableID = PVA.ID");
                sql.AppendLine("    GROUP BY PVA.ProductID;");
            }

            // Filter by size product
            if (!String.IsNullOrEmpty(strSize))
            {
                sql.AppendLine(String.Empty);
                sql.AppendLine("    With VariableValue AS (");
                sql.AppendLine("        SELECT");
                sql.AppendLine("            *");
                sql.AppendLine("        FROM");
                sql.AppendLine("            tbl_VariableValue");
                sql.AppendLine("        WHERE");
                sql.AppendLine(String.Format("        VariableID = 2 AND  LOWER(VariableValue) LIKE N'%{0}%'", strSize.ToLower()));
                sql.AppendLine("    )");
                sql.AppendLine("    SELECT");
                sql.AppendLine("        PVA.ProductID AS ID");
                sql.AppendLine("    INTO #ProductSize");
                sql.AppendLine("    FROM ");
                sql.AppendLine("    (");
                sql.AppendLine("        SELECT");
                sql.AppendLine("            PVV.ProductVariableID");
                sql.AppendLine("        FROM");
                sql.AppendLine("            VariableValue AS VAV");
                sql.AppendLine("        INNER JOIN tbl_ProductVariableValue AS PVV");
                sql.AppendLine("        ON  VAV.ID = PVV.VariableValueID");
                sql.AppendLine("        GROUP BY PVV.ProductVariableID");
                sql.AppendLine("    ) AS PVF");
                sql.AppendLine("    INNER JOIN tbl_ProductVariable AS PVA");
                sql.AppendLine("    ON  PVF.ProductVariableID = PVA.ID");
                sql.AppendLine("    GROUP BY PVA.ProductID;");
            }

            sql.AppendLine(String.Empty);
            sql.AppendLine("    SELECT");
            sql.AppendLine("            PRD.*");
            sql.AppendLine("    INTO #Product");
            sql.AppendLine("    FROM");
            sql.AppendLine("            tbl_Product AS PRD");
            sql.AppendLine("    WHERE");
            sql.AppendLine("            1 = 1");

            if (!string.IsNullOrEmpty(textsearch))
            {
                sql.AppendLine("    AND (");
                sql.AppendLine(String.Format("        PRD.ProductSKU like N'%{0}%'", textsearch));
                sql.AppendLine(String.Format("        OR PRD.ProductTitle like N'%{0}%'", textsearch));
                sql.AppendLine(String.Format("        OR PRD.UnSignedTitle like N'%{0}%'", textsearch));
                sql.AppendLine("    )");
            }

            if (!string.IsNullOrEmpty(ShowHomePage))
            {
                sql.AppendLine("    AND (");
                sql.AppendLine(String.Format("        PRD.ShowHomePage = {0}", ShowHomePage));
                sql.AppendLine("    )");
            }

            if (!string.IsNullOrEmpty(WebPublish))
            {
                sql.AppendLine("    AND (");
                sql.AppendLine(String.Format("        PRD.WebPublish = {0}", WebPublish));
                sql.AppendLine("    )");
            }

            if (!string.IsNullOrEmpty(CreatedDate))
            {
                DateTime fromdate = DateTime.Today;
                DateTime todate = DateTime.Now;
                switch (CreatedDate)
                {
                    case "today":
                        fromdate = DateTime.Today;
                        todate = DateTime.Now;
                        break;
                    case "yesterday":
                        fromdate = fromdate.AddDays(-1);
                        todate = DateTime.Today;
                        break;
                    case "beforeyesterday":
                        fromdate = DateTime.Today.AddDays(-2);
                        todate = DateTime.Today.AddDays(-1);
                        break;
                    case "week":
                        int days = DateTime.Today.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)DateTime.Today.DayOfWeek;
                        fromdate = fromdate.AddDays(-days + 1);
                        todate = DateTime.Now;
                        break;
                    case "thismonth":
                        fromdate = new DateTime(fromdate.Year, fromdate.Month, 1);
                        todate = DateTime.Now;
                        break;
                    case "lastmonth":
                        var thismonth = new DateTime(fromdate.Year, fromdate.Month, 1);
                        fromdate = thismonth.AddMonths(-1);
                        todate = thismonth;
                        break;
                    case "7days":
                        fromdate = DateTime.Today.AddDays(-6);
                        todate = DateTime.Now;
                        break;
                    case "30days":
                        fromdate = DateTime.Today.AddDays(-29);
                        todate = DateTime.Now;
                        break;
                }
                sql.AppendLine(String.Format("	AND	(CONVERT(datetime, PRD.CreatedDate, 103) BETWEEN CONVERT(datetime, '{0}', 103) AND CONVERT(datetime, '{1}', 103))", fromdate.ToString(), todate.ToString()));
            }

            if (!String.IsNullOrEmpty(strColor))
            {
                sql.AppendLine("    AND (");
                sql.AppendLine(String.Format("        LOWER(PRD.ProductTitle) like N'%{0}%'", strColor.ToLower()));
                sql.AppendLine(String.Format("        OR LOWER(PRD.Color) like N'%{0}%'", strColor.ToLower()));
                sql.AppendLine("        OR EXISTS (");
                sql.AppendLine("            SELECT");
                sql.AppendLine("                NULL AS DUMMY");
                sql.AppendLine("            FROM");
                sql.AppendLine("                #ProductColor AS PRC");
                sql.AppendLine("            WHERE");
                sql.AppendLine("                PRD.ID = PRC.ID");
                sql.AppendLine("        )");
                sql.AppendLine("    )");
            }

            if (!String.IsNullOrEmpty(strSize))
            {
                sql.AppendLine("    AND EXISTS (");
                sql.AppendLine("        SELECT");
                sql.AppendLine("            NULL AS DUMMY");
                sql.AppendLine("        FROM");
                sql.AppendLine("            #ProductSize AS PRS");
                sql.AppendLine("        WHERE");
                sql.AppendLine("            PRD.ID = PRS.ID");
                sql.AppendLine("    )");
            }

            if (categoryID > 0)
            {
                sql.AppendLine("    AND EXISTS(");
                sql.AppendLine("            SELECT");
                sql.AppendLine("                    NULL AS DUMMY");
                sql.AppendLine("            FROM");
                sql.AppendLine("                    #category");
                sql.AppendLine("            WHERE");
                sql.AppendLine("                    ID = PRD.CategoryID");
                sql.AppendLine("    )");
            }

            sql.AppendLine("     ORDER BY");
            sql.AppendLine("             PRD.ProductStyle");
            sql.AppendLine("     ,       PRD.ID");
            sql.AppendLine("     ;");
            sql.AppendLine(String.Empty);
            sql.AppendLine("     CREATE INDEX [ID_PROCDUCT] ON #Product([ProductStyle], [ID])");
            sql.AppendLine(String.Empty);
            sql.AppendLine("     SELECT");
            sql.AppendLine("             STM.ProductID");
            sql.AppendLine("     ,       STM.Quantity");
            sql.AppendLine("     ,       STM.QuantityCurrent");
            sql.AppendLine("     ,       STM.Type");
            sql.AppendLine("     ,       STM.CreatedDate");
            sql.AppendLine("     ,       STM.ParentID");
            sql.AppendLine("     INTO #StockProduct");
            sql.AppendLine("     FROM");
            sql.AppendLine("             #Product AS PRD");
            sql.AppendLine("     INNER JOIN tbl_StockManager AS STM");
            sql.AppendLine("         ON  PRD.ProductStyle = 1");
            sql.AppendLine("         AND PRD.ID = STM.ParentID");
            sql.AppendLine("     ORDER BY");
            sql.AppendLine("             STM.ProductID");
            sql.AppendLine("     ,       STM.CreatedDate");
            sql.AppendLine("     ;");
            sql.AppendLine(String.Empty);
            sql.AppendLine("     CREATE INDEX [ID_PROCDUCT] ON #StockProduct(");
            sql.AppendLine("             [ProductID] ASC");
            sql.AppendLine("     ,       [CreatedDate] DESC");
            sql.AppendLine("     )");
            sql.AppendLine(String.Empty);
            sql.AppendLine("     SELECT");
            sql.AppendLine("             STM.ProductVariableID");
            sql.AppendLine("     ,       STM.Quantity");
            sql.AppendLine("     ,       STM.QuantityCurrent");
            sql.AppendLine("     ,       STM.Type");
            sql.AppendLine("     ,       STM.CreatedDate");
            sql.AppendLine("     ,       STM.ParentID");
            sql.AppendLine("     INTO #StockProductVariable");
            sql.AppendLine("     FROM");
            sql.AppendLine("             #Product AS PRD");
            sql.AppendLine("     INNER JOIN tbl_StockManager AS STM");
            sql.AppendLine("         ON  PRD.ProductStyle = 2");
            sql.AppendLine("         AND PRD.ID = STM.ParentID");
            sql.AppendLine("     ORDER BY");
            sql.AppendLine("             STM.ProductVariableID");
            sql.AppendLine("     ,       STM.CreatedDate");
            sql.AppendLine("     ;");
            sql.AppendLine(String.Empty);
            sql.AppendLine("     CREATE INDEX [ID_PROCDUCT] ON #StockProductVariable(");
            sql.AppendLine("             [ProductVariableID] ASC");
            sql.AppendLine("     ,       [CreatedDate] DESC");
            sql.AppendLine("     )");
            sql.AppendLine(String.Empty);
            sql.AppendLine("     SELECT");
            sql.AppendLine("             PRQ.ProductStyle");
            sql.AppendLine("     ,       PRQ.ParentID");
            sql.AppendLine("     ,       SUM(ISNULL(PRQ.QuantityLeft, 0)) AS QuantityLeft");
            sql.AppendLine("     INTO #ProductQuantity");
            sql.AppendLine("     FROM (");
            sql.AppendLine("         SELECT");
            sql.AppendLine("                 1 AS ProductStyle");
            sql.AppendLine("         ,       STP.ParentID");
            sql.AppendLine("         ,       (");
            sql.AppendLine("                     CASE STP.Type");
            sql.AppendLine("                         WHEN 1");
            sql.AppendLine("                             THEN STP.QuantityCurrent + STP.Quantity");
            sql.AppendLine("                         WHEN 2");
            sql.AppendLine("                             THEN STP.QuantityCurrent - STP.Quantity");
            sql.AppendLine("                         ELSE");
            sql.AppendLine("                             0");
            sql.AppendLine("                     END");
            sql.AppendLine("                 ) AS QuantityLeft");
            sql.AppendLine("         FROM ");
            sql.AppendLine("                 #StockProduct AS STP");
            sql.AppendLine("         INNER JOIN (");
            sql.AppendLine("                 SELECT");
            sql.AppendLine("                         ProductID");
            sql.AppendLine("                 ,       MAX(CreatedDate) AS CreatedDate");
            sql.AppendLine("                 FROM");
            sql.AppendLine("                         #StockProduct");
            sql.AppendLine("                 GROUP BY");
            sql.AppendLine("                         ProductID");
            sql.AppendLine("             ) AS SPM");
            sql.AppendLine("             ON  STP.ProductID = SPM.ProductID");
            sql.AppendLine("             AND STP.CreatedDate = SPM.CreatedDate");
            sql.AppendLine(String.Empty);
            sql.AppendLine("         UNION ALL");
            sql.AppendLine(String.Empty);
            sql.AppendLine("         SELECT");
            sql.AppendLine("                 2 AS ProductStyle");
            sql.AppendLine("         ,       STP.ParentID");
            sql.AppendLine("         ,       (");
            sql.AppendLine("                     CASE STP.Type");
            sql.AppendLine("                         WHEN 1");
            sql.AppendLine("                             THEN STP.QuantityCurrent + STP.Quantity");
            sql.AppendLine("                         WHEN 2");
            sql.AppendLine("                             THEN STP.QuantityCurrent - STP.Quantity");
            sql.AppendLine("                         ELSE");
            sql.AppendLine("                             0");
            sql.AppendLine("                     END");
            sql.AppendLine("                 ) AS QuantityLeft");
            sql.AppendLine("         FROM ");
            sql.AppendLine("                 #StockProductVariable AS STP");
            sql.AppendLine("         INNER JOIN (");
            sql.AppendLine("                 SELECT");
            sql.AppendLine("                         ProductVariableID");
            sql.AppendLine("                 ,       MAX(CreatedDate) AS CreatedDate");
            sql.AppendLine("                 FROM");
            sql.AppendLine("                         #StockProductVariable");
            sql.AppendLine("                 GROUP BY");
            sql.AppendLine("                         ProductVariableID");
            sql.AppendLine("             ) AS SPM");
            sql.AppendLine("             ON  STP.ProductVariableID = SPM.ProductVariableID");
            sql.AppendLine("             AND STP.CreatedDate = SPM.CreatedDate");
            sql.AppendLine("     ) AS PRQ");
            sql.AppendLine("     GROUP BY");
            sql.AppendLine("             PRQ.ProductStyle");
            sql.AppendLine("     ,       PRQ.ParentID");
            sql.AppendLine("     ;");
            sql.AppendLine(String.Empty);
            sql.AppendLine("     CREATE INDEX [ID_PROCDUCT] ON #ProductQuantity([ProductStyle], [ParentID])");
            sql.AppendLine(String.Empty);
            sql.AppendLine("     SELECT");
            sql.AppendLine("             p.ProductStyle AS ProductStyle");
            sql.AppendLine("     ,       c.CategoryName");
            sql.AppendLine("     ,       p.*");
            sql.AppendLine("     ,       PRQ.QuantityLeft");
            sql.AppendLine("     FROM");
            sql.AppendLine("             #Product AS p");
            sql.AppendLine("     LEFT JOIN #ProductQuantity AS PRQ");
            sql.AppendLine("         ON  p.ProductStyle = PRQ.ProductStyle");
            sql.AppendLine("         AND p.ID = PRQ.ParentID");
            sql.AppendLine("     LEFT JOIN (");
            sql.AppendLine("             SELECT");
            sql.AppendLine("                     ID");
            sql.AppendLine("             ,       CategoryName");
            sql.AppendLine("             FROM");
            sql.AppendLine("                     dbo.tbl_Category");
            sql.AppendLine("     ) AS c");
            sql.AppendLine("     ON c.ID = p.CategoryID");
            sql.AppendLine("     ORDER BY");
            sql.AppendLine("             p.ID");
            sql.AppendLine("     ;");
            sql.AppendLine(String.Empty);
            sql.AppendLine(" END");

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql.ToString());

            while (reader.Read())
            {
                double quantityLeft = 0;

                var entity = new ProductSQL();

                entity.ID = Convert.ToInt32(reader["ID"]);

                entity.ProductImage = reader["ProductImage"].ToString();

                if (!string.IsNullOrEmpty(reader["ProductImageClean"].ToString()))
                {
                    entity.ProductImageClean = reader["ProductImageClean"].ToString();
                }

                if (reader["ProductTitle"] != DBNull.Value)
                    entity.ProductTitle = reader["ProductTitle"].ToString();
                if (reader["ProductSKU"] != DBNull.Value)
                    entity.ProductSKU = reader["ProductSKU"].ToString();


                if (reader["QuantityLeft"] != DBNull.Value)
                {
                    quantityLeft = Convert.ToDouble(reader["QuantityLeft"]);

                    if (quantityLeft > 0)
                    {
                        entity.ProductInstockStatus = "<span class=\"bg-green\">Còn hàng</span>";
                        entity.StockStatus = 1;
                    }
                    else
                    {
                        entity.ProductInstockStatus = "<span class=\"bg-red\">Hết hàng</span>";
                        entity.StockStatus = 2;
                    }
                }
                else
                {
                    entity.ProductInstockStatus = "<span class=\"bg-yellow\">Nhập hàng</span>";
                    entity.StockStatus = 3;
                }

                entity.TotalProductInstockQuantityLeft = quantityLeft;

                if (reader["Regular_Price"] != DBNull.Value)
                    entity.RegularPrice = Convert.ToDouble(reader["Regular_Price"].ToString());
                if (reader["CostOfGood"] != DBNull.Value)
                    entity.CostOfGood = Convert.ToDouble(reader["CostOfGood"].ToString());
                if (reader["Retail_Price"] != DBNull.Value)
                    entity.RetailPrice = Convert.ToDouble(reader["Retail_Price"].ToString());
                if (reader["CategoryName"] != DBNull.Value)
                    entity.CategoryName = reader["CategoryName"].ToString();
                if (reader["CategoryID"] != DBNull.Value)
                    entity.CategoryID = reader["CategoryID"].ToString().ToInt(0);
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                if (reader["ProductContent"] != DBNull.Value)
                    entity.ProductContent = reader["ProductContent"].ToString();
                if (reader["ProductStyle"] != DBNull.Value)
                    entity.ProductStyle = reader["ProductStyle"].ToString().ToInt(0);
                if (reader["ShowHomePage"] != DBNull.Value)
                    entity.ShowHomePage = reader["ShowHomePage"].ToString().ToInt(0);
                if (reader["Materials"] != DBNull.Value)
                    entity.Materials = reader["Materials"].ToString();
                if (reader["WebPublish"] != DBNull.Value)
                    entity.WebPublish = reader["WebPublish"].ToString().ToBool();
                if (reader["WebUpdate"] != DBNull.Value)
                    entity.WebUpdate = Convert.ToDateTime(reader["WebUpdate"]);
                list.Add(entity);
            }
            reader.Close();
            return list.OrderByDescending(x => x.ID).ToList();
        }

        public static ProductStockReport getProductStockReport(string SKU, int CategoryID)
        {
            var list = new List<ProductStockReport>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("BEGIN");

            if (CategoryID > 0)
            {
                sql.AppendLine(String.Empty);
                sql.AppendLine("WITH category AS(");
                sql.AppendLine("    SELECT");
                sql.AppendLine("            ID");
                sql.AppendLine("    ,       CategoryName");
                sql.AppendLine("    ,       ParentID");
                sql.AppendLine("    FROM");
                sql.AppendLine("            tbl_Category");
                sql.AppendLine("    WHERE");
                sql.AppendLine("            1 = 1");
                sql.AppendLine("    AND     ID = " + CategoryID);
                sql.AppendLine("");
                sql.AppendLine("    UNION ALL");
                sql.AppendLine("");
                sql.AppendLine("    SELECT");
                sql.AppendLine("            CHI.ID");
                sql.AppendLine("    ,       CHI.CategoryName");
                sql.AppendLine("    ,       CHI.ParentID");
                sql.AppendLine("    FROM");
                sql.AppendLine("            category AS PAR");
                sql.AppendLine("    INNER JOIN tbl_Category AS CHI");
                sql.AppendLine("        ON PAR.ID = CHI.ParentID");
                sql.AppendLine(")");
                sql.AppendLine("SELECT");
                sql.AppendLine("        ID");
                sql.AppendLine(",       CategoryName");
                sql.AppendLine(",       ParentID");
                sql.AppendLine("INTO #category");
                sql.AppendLine("FROM category;");
            }

            sql.AppendLine(String.Empty);
            sql.AppendLine("    SELECT");
            sql.AppendLine("            PRD.*");
            sql.AppendLine("    INTO #Product");
            sql.AppendLine("    FROM");
            sql.AppendLine("            tbl_Product AS PRD");
            sql.AppendLine("    WHERE");
            sql.AppendLine("            1 = 1");
            sql.AppendLine("    AND (PRD.ProductSKU like '" + SKU + "%')");

            if (CategoryID > 0)
            {
                sql.AppendLine("    AND EXISTS(");
                sql.AppendLine("            SELECT");
                sql.AppendLine("                    NULL AS DUMMY");
                sql.AppendLine("            FROM");
                sql.AppendLine("                    #category");
                sql.AppendLine("            WHERE");
                sql.AppendLine("                    ID = PRD.CategoryID");
                sql.AppendLine("    )");
            }

            sql.AppendLine("     ORDER BY");
            sql.AppendLine("             PRD.ProductStyle");
            sql.AppendLine("     ,       PRD.ID");
            sql.AppendLine("     ;");
            sql.AppendLine(String.Empty);
            sql.AppendLine("     CREATE INDEX [ID_PROCDUCT] ON #Product([ProductStyle], [ID])");
            sql.AppendLine(String.Empty);
            sql.AppendLine("     SELECT");
            sql.AppendLine("             STM.ProductID");
            sql.AppendLine("     ,       STM.Quantity");
            sql.AppendLine("     ,       STM.QuantityCurrent");
            sql.AppendLine("     ,       STM.Type");
            sql.AppendLine("     ,       STM.CreatedDate");
            sql.AppendLine("     ,       STM.ParentID");
            sql.AppendLine("     INTO #StockProduct");
            sql.AppendLine("     FROM");
            sql.AppendLine("             #Product AS PRD");
            sql.AppendLine("     INNER JOIN tbl_StockManager AS STM");
            sql.AppendLine("         ON  PRD.ProductStyle = 1");
            sql.AppendLine("         AND PRD.ID = STM.ParentID");
            sql.AppendLine("     ORDER BY");
            sql.AppendLine("             STM.ProductID");
            sql.AppendLine("     ,       STM.CreatedDate");
            sql.AppendLine("     ;");
            sql.AppendLine(String.Empty);
            sql.AppendLine("     CREATE INDEX [ID_PROCDUCT] ON #StockProduct(");
            sql.AppendLine("             [ProductID] ASC");
            sql.AppendLine("     ,       [CreatedDate] DESC");
            sql.AppendLine("     )");
            sql.AppendLine(String.Empty);
            sql.AppendLine("     SELECT");
            sql.AppendLine("             STM.ProductVariableID");
            sql.AppendLine("     ,       STM.Quantity");
            sql.AppendLine("     ,       STM.QuantityCurrent");
            sql.AppendLine("     ,       STM.Type");
            sql.AppendLine("     ,       STM.CreatedDate");
            sql.AppendLine("     ,       STM.ParentID");
            sql.AppendLine("     INTO #StockProductVariable");
            sql.AppendLine("     FROM");
            sql.AppendLine("             #Product AS PRD");
            sql.AppendLine("     INNER JOIN tbl_StockManager AS STM");
            sql.AppendLine("         ON  PRD.ProductStyle = 2");
            sql.AppendLine("         AND PRD.ID = STM.ParentID");
            sql.AppendLine("     ORDER BY");
            sql.AppendLine("             STM.ProductVariableID");
            sql.AppendLine("     ,       STM.CreatedDate");
            sql.AppendLine("     ;");
            sql.AppendLine(String.Empty);
            sql.AppendLine("     CREATE INDEX [ID_PROCDUCT] ON #StockProductVariable(");
            sql.AppendLine("             [ProductVariableID] ASC");
            sql.AppendLine("     ,       [CreatedDate] DESC");
            sql.AppendLine("     )");
            sql.AppendLine(String.Empty);
            sql.AppendLine("     SELECT");
            sql.AppendLine("             PRQ.ProductStyle");
            sql.AppendLine("     ,       PRQ.ParentID");
            sql.AppendLine("     ,       SUM(ISNULL(PRQ.QuantityLeft, 0)) AS QuantityLeft");
            sql.AppendLine("     INTO #ProductQuantity");
            sql.AppendLine("     FROM (");
            sql.AppendLine("         SELECT");
            sql.AppendLine("                 1 AS ProductStyle");
            sql.AppendLine("         ,       STP.ParentID");
            sql.AppendLine("         ,       (");
            sql.AppendLine("                     CASE STP.Type");
            sql.AppendLine("                         WHEN 1");
            sql.AppendLine("                             THEN STP.QuantityCurrent + STP.Quantity");
            sql.AppendLine("                         WHEN 2");
            sql.AppendLine("                             THEN STP.QuantityCurrent - STP.Quantity");
            sql.AppendLine("                         ELSE");
            sql.AppendLine("                             0");
            sql.AppendLine("                     END");
            sql.AppendLine("                 ) AS QuantityLeft");
            sql.AppendLine("         FROM ");
            sql.AppendLine("                 #StockProduct AS STP");
            sql.AppendLine("         INNER JOIN (");
            sql.AppendLine("                 SELECT");
            sql.AppendLine("                         ProductID");
            sql.AppendLine("                 ,       MAX(CreatedDate) AS CreatedDate");
            sql.AppendLine("                 FROM");
            sql.AppendLine("                         #StockProduct");
            sql.AppendLine("                 GROUP BY");
            sql.AppendLine("                         ProductID");
            sql.AppendLine("             ) AS SPM");
            sql.AppendLine("             ON  STP.ProductID = SPM.ProductID");
            sql.AppendLine("             AND STP.CreatedDate = SPM.CreatedDate");
            sql.AppendLine(String.Empty);
            sql.AppendLine("         UNION ALL");
            sql.AppendLine(String.Empty);
            sql.AppendLine("         SELECT");
            sql.AppendLine("                 2 AS ProductStyle");
            sql.AppendLine("         ,       STP.ParentID");
            sql.AppendLine("         ,       (");
            sql.AppendLine("                     CASE STP.Type");
            sql.AppendLine("                         WHEN 1");
            sql.AppendLine("                             THEN STP.QuantityCurrent + STP.Quantity");
            sql.AppendLine("                         WHEN 2");
            sql.AppendLine("                             THEN STP.QuantityCurrent - STP.Quantity");
            sql.AppendLine("                         ELSE");
            sql.AppendLine("                             0");
            sql.AppendLine("                     END");
            sql.AppendLine("                 ) AS QuantityLeft");
            sql.AppendLine("         FROM ");
            sql.AppendLine("                 #StockProductVariable AS STP");
            sql.AppendLine("         INNER JOIN (");
            sql.AppendLine("                 SELECT");
            sql.AppendLine("                         ProductVariableID");
            sql.AppendLine("                 ,       MAX(CreatedDate) AS CreatedDate");
            sql.AppendLine("                 FROM");
            sql.AppendLine("                         #StockProductVariable");
            sql.AppendLine("                 GROUP BY");
            sql.AppendLine("                         ProductVariableID");
            sql.AppendLine("             ) AS SPM");
            sql.AppendLine("             ON  STP.ProductVariableID = SPM.ProductVariableID");
            sql.AppendLine("             AND STP.CreatedDate = SPM.CreatedDate");
            sql.AppendLine("     ) AS PRQ");
            sql.AppendLine("     GROUP BY");
            sql.AppendLine("             PRQ.ProductStyle");
            sql.AppendLine("     ,       PRQ.ParentID");
            sql.AppendLine("     ;");
            sql.AppendLine(String.Empty);
            sql.AppendLine("     CREATE INDEX [ID_PROCDUCT] ON #ProductQuantity([ProductStyle], [ParentID])");
            sql.AppendLine(String.Empty);
            sql.AppendLine("     SELECT");
            sql.AppendLine("             p.ProductStyle AS ProductStyle");
            sql.AppendLine("     ,       p.*");
            sql.AppendLine("     ,       PRQ.QuantityLeft");
            sql.AppendLine("     FROM");
            sql.AppendLine("             #Product AS p");
            sql.AppendLine("     LEFT JOIN #ProductQuantity AS PRQ");
            sql.AppendLine("         ON  p.ProductStyle = PRQ.ProductStyle");
            sql.AppendLine("         AND p.ID = PRQ.ParentID");
            sql.AppendLine("     ORDER BY");
            sql.AppendLine("             p.ID");
            sql.AppendLine("     ;");
            sql.AppendLine(String.Empty);
            sql.AppendLine(" END");

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql.ToString());
            

            while (reader.Read())
            {
                var entity = new ProductStockReport();

                double quantityLeft = 0;

                if (reader["QuantityLeft"] != DBNull.Value)
                {
                    quantityLeft = Convert.ToDouble(reader["QuantityLeft"]);
                }

                entity.totalStock = Convert.ToInt32(quantityLeft);

                if (reader["CostOfGood"] != DBNull.Value)
                {
                    entity.totalStockValue = quantityLeft *  Convert.ToDouble(reader["CostOfGood"]);
                }
                list.Add(entity);
            }
            reader.Close();

            return new ProductStockReport()
            {
                totalStock = list.Sum(x => x.totalStock),
                totalStockValue = list.Sum(x => x.totalStockValue),
            };
        }


        public static ProductStock GetStock(int ProductVariableID)
        {
            var entity = new ProductStock();
            var sql = @"select t.ID,t.SKU,t.ID, d.quantiyIN as InProduct, k.quantiyIN as OutProduct, (d.quantiyIN - k.quantiyIN) as leftProduct from tbl_ProductVariable as t";
            sql += " left outer join (select  ProductVariableID, sum(quantity) as quantiyIN from tbl_StockManager where [Type]= 1 group by ProductVariableID) as d ON t.ID = d.ProductVariableID ";
            sql += " left outer join (select ProductVariableID, sum(quantity) as quantiyIN from tbl_StockManager where [Type]= 2 group by ProductVariableID) as k ON t.ID = k.ProductVariableID ";
            if (ProductVariableID > 0)
            {
                sql += " Where t.ID = " + ProductVariableID;
            }
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);

            while (reader.Read())
            {
                int quantityIn = reader["InProduct"].ToString().ToInt(0);
                int quantityOut = reader["OutProduct"].ToString().ToInt(0);
                int quantityLeft = quantityIn - quantityOut;


                if (quantityIn > 0)
                {
                    if (quantityLeft > 0)
                    {
                        entity.ProductInstockStatus = "<span class=\"bg-green\">Còn hàng</span>";

                    }
                    else
                    {
                        entity.ProductInstockStatus = "<span class=\"bg-red\">Hết hàng</span>";
                    }
                }
                else
                {
                    entity.ProductInstockStatus = "<span class=\"bg-yellow\">Nhập hàng</span>";

                }
                entity.quantityLeft = quantityLeft;
            }
            reader.Close();
            return entity;
        }
        #endregion
        #region Class
        public class ProductSQL
        {
            public int ID { get; set; }
            public string ProductImage { get; set; }
            public string ProductTitle { get; set; }
            public string ProductSKU { get; set; }
            public string ProductInstockStatus { get; set; }
            public double TotalProductInstockQuantityLeft { get; set; }
            public string ProductContent { get; set; }
            public double RegularPrice { get; set; }
            public double CostOfGood { get; set; }
            public double RetailPrice { get; set; }
            public int CategoryID { get; set; }
            public string CategoryName { get; set; }
            public DateTime CreatedDate { get; set; }
            public int StockStatus { get; set; }
            public int ProductStyle { get; set; }
            public int ShowHomePage { get; set; }
            public string Materials { get; set; }
            public string ProductImageClean { get; set; }
            public bool WebPublish { get; set; }
            public DateTime WebUpdate { get; set; }
        }

        public class ProductStockReport
        {
            public int totalStock { get; set; }
            public double totalStockValue { get; set; }
        }

        public class ProductStock
        {
            public string ProductInstockStatus { get; set; }
            public int quantityLeft { get; set; }
        }
        #endregion
    }
}