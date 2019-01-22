using IM_PJ.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using WebUI.Business;

namespace IM_PJ.Controllers
{
    public class StockManagerController
    {
        #region CRUD
        public static void Insert(tbl_StockManager stock)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                var stockOld = dbe.tbl_StockManager
                    .Where(x => x.AgentID == stock.AgentID
                                && x.ProductID == stock.ProductID
                                && x.ProductVariableID == stock.ProductVariableID)
                    .OrderByDescending(x => x.CreatedDate)
                    .FirstOrDefault();

                // Check exists record stock old
                if (stockOld != null)
                {
                    // Calculator quantity current stock with SKU target
                    var quantityCurrent = 0D;

                    if (stockOld.Type == 1)
                    {
                        quantityCurrent = stockOld.QuantityCurrent.Value + stockOld.Quantity.Value;
                    }
                    else
                    {
                        quantityCurrent = stockOld.QuantityCurrent.Value - stockOld.Quantity.Value;
                    }

                    // Calculator quantity current stock when input SKU target
                    var quantityCurrentNew = 0D;

                    if (stock.Type == 2) // Ouput SKU
                    {
                        quantityCurrentNew = quantityCurrent - stock.Quantity.Value;

                        // Check quantity < 0 when output SKU target
                        if (quantityCurrentNew < 0)
                        {
                            tbl_StockManager stockBalance = new tbl_StockManager();
                            stockBalance.AgentID = stock.AgentID;
                            stockBalance.ProductID = stock.ProductID;
                            stockBalance.ProductVariableID = stock.ProductVariableID;
                            stockBalance.Quantity = quantityCurrentNew * (-1);
                            stockBalance.QuantityCurrent = quantityCurrent;
                            stockBalance.Type = 1;
                            stockBalance.NoteID = "Nhập lệch kho";
                            stockBalance.OrderID = stock.OrderID;
                            stockBalance.Status = stock.Status;
                            stockBalance.SKU = stock.SKU;
                            stockBalance.ParentID = stock.ParentID;
                            stockBalance.CreatedDate = stock.CreatedDate.Value.AddMilliseconds(-10);
                            stockBalance.CreatedBy = stock.CreatedBy;
                            stockBalance.MoveProID = stock.MoveProID;

                            dbe.tbl_StockManager.Add(stockBalance);

                            // Handling balance quantity stock with SKU target
                            stock.QuantityCurrent = stock.Quantity;
                            dbe.tbl_StockManager.Add(stock);
                        }
                        else
                        {
                            // Update quantity stock with SKU target
                            stock.QuantityCurrent = quantityCurrent;
                            dbe.tbl_StockManager.Add(stock);
                        }
                    }
                    else // Input SKU
                    {
                        // Update quantity stock with SKU target
                        stock.QuantityCurrent = quantityCurrent;
                        dbe.tbl_StockManager.Add(stock);
                    }
                }
                else // New SKU 
                {
                    if(stock.Type == 2) // Output SKU
                    {
                        tbl_StockManager stockBalance = new tbl_StockManager();
                        stockBalance.AgentID = stock.AgentID;
                        stockBalance.ProductID = stock.ProductID;
                        stockBalance.ProductVariableID = stock.ProductVariableID;
                        stockBalance.Quantity = stock.Quantity;
                        stockBalance.QuantityCurrent = 0;
                        stockBalance.Type = 1;
                        stockBalance.NoteID = "Nhập kho sản phẩm mới";
                        stockBalance.OrderID = stock.OrderID;
                        stockBalance.Status = stock.Status;
                        stockBalance.SKU = stock.SKU;
                        stockBalance.ParentID = stock.ParentID;
                        stockBalance.CreatedDate = stock.CreatedDate.Value.AddMilliseconds(-10);
                        stockBalance.CreatedBy = stock.CreatedBy;
                        stockBalance.MoveProID = stock.MoveProID;

                        dbe.tbl_StockManager.Add(stockBalance);

                        stock.QuantityCurrent = stockBalance.Quantity;
                        dbe.tbl_StockManager.Add(stock);
                    }
                    else // Input SKU
                    {
                        stock.QuantityCurrent = 0;
                        dbe.tbl_StockManager.Add(stock);
                    }
                }
 
                dbe.SaveChanges();
            }
        }

        public static void Insert(List<tbl_StockManager> stockManagers)
        {
            if (stockManagers.Count == 0)
            {
                return;
            }

            using (var con = new inventorymanagementEntities())
            {
                var stockManagersOrder = stockManagers.OrderBy(x => x.AgentID)
                                                      .ThenBy(x => x.ProductID)
                                                      .ThenBy(x => x.ProductVariableID)
                                                      .ToList();

                StringBuilder sqlString = new StringBuilder();

                sqlString.AppendLine("SELECT");
                sqlString.AppendLine("        DAT.[AgentID]");
                sqlString.AppendLine(",       DAT.[ProductID]");
                sqlString.AppendLine(",       DAT.[ProductVariableID]");
                sqlString.AppendLine("INTO #StockTarget");
                sqlString.AppendLine("FROM (");
                for (var i = 0; i < stockManagersOrder.Count(); i++)
                {
                    var sqlSub = String.Empty;

                    if (i != 0)
                    {
                        sqlSub += String.Format("UNION\n");
                    }

                    sqlSub += String.Format("SELECT\n");
                    sqlSub += String.Format("        {0} AS AgentID\n", stockManagersOrder[i].AgentID.Value);
                    sqlSub += String.Format(",       {0} AS ProductID\n", stockManagersOrder[i].ProductID.Value);
                    sqlSub += String.Format(",       {0} AS ProductVariableID\n", stockManagersOrder[i].ProductVariableID.Value);

                    sqlString.AppendLine(sqlSub);

                }
                sqlString.AppendLine(") AS DAT");
                sqlString.AppendLine(";");
                sqlString.AppendLine("");
                sqlString.AppendLine("SELECT");
                sqlString.AppendLine("        STM.[ID]");
                sqlString.AppendLine(",       STM.[AgentID]");
                sqlString.AppendLine(",       STM.[ProductID]");
                sqlString.AppendLine(",       STM.[ProductVariableID]");
                sqlString.AppendLine(",       STM.[Quantity]");
                sqlString.AppendLine(",       STM.[QuantityCurrent]");
                sqlString.AppendLine(",       STM.[Type]");
                sqlString.AppendLine(",       STM.[CreatedDate]");
                sqlString.AppendLine(",       STM.[CreatedBy]");
                sqlString.AppendLine(",       STM.[ModifiedDate]");
                sqlString.AppendLine(",       STM.[ModifiedBy]");
                sqlString.AppendLine(",       STM.[NoteID]");
                sqlString.AppendLine(",       STM.[OrderID]");
                sqlString.AppendLine(",       STM.[Status]");
                sqlString.AppendLine(",       STM.[SKU]");
                sqlString.AppendLine(",       STM.[MoveProID]");
                sqlString.AppendLine(",       STM.[ParentID]");
                sqlString.AppendLine("FROM");
                sqlString.AppendLine("        tbl_StockManager AS STM");
                sqlString.AppendLine("INNER JOIN #StockTarget AS TAG");
                sqlString.AppendLine("    ON  TAG.[AgentID]= STM.[AgentID]");
                sqlString.AppendLine("    AND TAG.[ProductID] = STM.[ProductID]");
                sqlString.AppendLine("    AND TAG.[ProductVariableID] = STM.[ProductVariableID]");
                sqlString.AppendLine("WHERE");
                sqlString.AppendLine("        CreatedDate = (");
                sqlString.AppendLine("                        SELECT");
                sqlString.AppendLine("                                MAX([CreatedDate])");
                sqlString.AppendLine("                        FROM");
                sqlString.AppendLine("                                tbl_StockManager AS SMM");
                sqlString.AppendLine("                        WHERE");
                sqlString.AppendLine("                                SMM.[AgentID] = STM.[AgentID]");
                sqlString.AppendLine("                        AND     SMM.[ProductID] = STM.[ProductID]");
                sqlString.AppendLine("                        AND     SMM.[ProductVariableID] = STM.[ProductVariableID]");
                sqlString.AppendLine("                    )");
                sqlString.AppendLine("ORDER BY");
                sqlString.AppendLine("        [AgentID]");
                sqlString.AppendLine(",       [ProductID]");
                sqlString.AppendLine(",       [ProductVariableID]");
                sqlString.AppendLine(";");

                var stockOlds = new List<tbl_StockManager>();

                var reader = (IDataReader)SqlHelper.ExecuteDataReader(sqlString.ToString());
                while (reader.Read())
                {
                    var stock = new tbl_StockManager();

                    stock.ID = Convert.ToInt32(reader["ID"]);

                    // AgentID
                    if (reader["AgentID"] != DBNull.Value)
                    {
                        stock.AgentID = Convert.ToInt32(reader["AgentID"]);
                    }

                    // ProductID
                    if (reader["ProductID"] != DBNull.Value)
                    {
                        stock.ProductID = Convert.ToInt32(reader["ProductID"]);
                    }

                    // ProductVariableID
                    if (reader["ProductVariableID"] != DBNull.Value)
                    {
                        stock.ProductVariableID = Convert.ToInt32(reader["ProductVariableID"]);
                    }

                    // Quantity
                    if (reader["Quantity"] != DBNull.Value)
                    {
                        stock.Quantity = Convert.ToDouble(reader["Quantity"]);
                    }

                    // Quantity
                    if (reader["QuantityCurrent"] != DBNull.Value)
                    {
                        stock.QuantityCurrent = Convert.ToDouble(reader["QuantityCurrent"]);
                    }

                    // Type
                    if (reader["Type"] != DBNull.Value)
                    {
                        stock.Type = Convert.ToInt32(reader["Type"]);
                    }

                    // CreatedDate
                    if (reader["CreatedDate"] != DBNull.Value)
                    {
                        stock.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                    }

                    // CreatedBy
                    if (reader["CreatedBy"] != DBNull.Value)
                    {
                        stock.CreatedBy = reader["CreatedBy"].ToString();
                    }

                    // ModifiedDate
                    if (reader["ModifiedDate"] != DBNull.Value)
                    {
                        stock.ModifiedDate = Convert.ToDateTime(reader["ModifiedDate"]);
                    }

                    // ModifiedBy
                    if (reader["ModifiedBy"] != DBNull.Value)
                    {
                        stock.ModifiedBy = reader["ModifiedBy"].ToString();
                    }

                    // NoteID
                    if (reader["NoteID"] != DBNull.Value)
                    {
                        stock.NoteID = reader["NoteID"].ToString();
                    }

                    // OrderID
                    if (reader["OrderID"] != DBNull.Value)
                    {
                        stock.OrderID = Convert.ToInt32(reader["OrderID"]);
                    }

                    // Status
                    if (reader["Status"] != DBNull.Value)
                    {
                        stock.Status = Convert.ToInt32(reader["Status"]);
                    }

                    // SKU
                    if (reader["SKU"] != DBNull.Value)
                    {
                        stock.SKU = reader["SKU"].ToString();
                    }

                    // MoveProID
                    if (reader["MoveProID"] != DBNull.Value)
                    {
                        stock.MoveProID = Convert.ToInt32(reader["MoveProID"]);
                    }

                    // ParentID
                    if (reader["ParentID"] != DBNull.Value)
                    {
                        stock.ParentID = Convert.ToInt32(reader["ParentID"]);
                    }

                    stockOlds.Add(stock);
                }

                var stockInput = stockManagersOrder
                    .Where(x => x.Type == 1)
                    .GroupJoin(
                        stockOlds,
                        stockNew => new { stockNew.AgentID, stockNew.ProductID, stockNew.ProductVariableID },
                        stockOld => new { AgentID = stockOld.AgentID, ProductID = stockOld.ProductID, ProductVariableID = stockOld.ProductVariableID },
                        (stockNew, stockOld) =>
                        {
                            var quantityCurrent = 0D;

                            if (stockOld.Count() > 0)
                            {
                                if (stockOld.FirstOrDefault().Type == 1)
                                {
                                    quantityCurrent = stockOld.FirstOrDefault().QuantityCurrent.Value + stockOld.FirstOrDefault().Quantity.Value;
                                }
                                else
                                {
                                    quantityCurrent = stockOld.FirstOrDefault().QuantityCurrent.Value - stockOld.FirstOrDefault().Quantity.Value;
                                }
                            }

                            return new tbl_StockManager()
                            {
                                AgentID = stockNew.AgentID,
                                ProductID = stockNew.ProductID,
                                ProductVariableID = stockNew.ProductVariableID,
                                Quantity = stockNew.Quantity,
                                QuantityCurrent = quantityCurrent,
                                Type = stockNew.Type,
                                NoteID = stockNew.NoteID,
                                OrderID = stockNew.OrderID,
                                Status = stockNew.Status,
                                SKU = stockNew.SKU,
                                ParentID = stockNew.ParentID,
                                CreatedDate = stockNew.CreatedDate,
                                CreatedBy = stockNew.CreatedBy,
                                MoveProID = stockNew.MoveProID
                            };
                         })
                    .ToList();

                var stockBalance = new List<tbl_StockManager>();
                var stockOut = stockManagersOrder
                    .Where(x => x.Type == 2)
                    .GroupJoin(
                        stockOlds,
                        stockNew => new { stockNew.AgentID, stockNew.ProductID, stockNew.ProductVariableID },
                        stockOld => new { AgentID = stockOld.AgentID, ProductID = stockOld.ProductID, ProductVariableID = stockOld.ProductVariableID },
                        (stockNew, stockOld) =>
                        {
                            // Check exists record stock old
                            if (stockOld.Count() > 0)
                            {
                                var quantityCurrent = 0D;

                                // Calculator quantity current stock with SKU target
                                if (stockOld.FirstOrDefault().Type == 1)
                                {
                                    quantityCurrent = stockOld.FirstOrDefault().QuantityCurrent.Value + stockOld.FirstOrDefault().Quantity.Value;
                                }
                                else
                                {
                                    quantityCurrent = stockOld.FirstOrDefault().QuantityCurrent.Value - stockOld.FirstOrDefault().Quantity.Value;
                                }

                                // Calculator quantity current stock when input SKU target
                                var quantityCurrentNew = 0D;

                                quantityCurrentNew = quantityCurrent - stockNew.Quantity.Value;

                                if (quantityCurrentNew < 0)
                                {
                                    stockBalance.Add(
                                        new tbl_StockManager()
                                        {
                                            AgentID = stockNew.AgentID,
                                            ProductID = stockNew.ProductID,
                                            ProductVariableID = stockNew.ProductVariableID,
                                            Quantity = quantityCurrentNew * (-1),
                                            QuantityCurrent = quantityCurrent,
                                            Type = 1,
                                            NoteID = "Nhập lệch kho",
                                            OrderID = stockNew.OrderID,
                                            Status = stockNew.Status,
                                            SKU = stockNew.SKU,
                                            ParentID = stockNew.ParentID,
                                            CreatedDate = stockNew.CreatedDate.Value.AddMilliseconds(-10),
                                            CreatedBy = stockNew.CreatedBy,
                                            MoveProID = stockNew.MoveProID
                                        });

                                    return new tbl_StockManager()
                                    {
                                        AgentID = stockNew.AgentID,
                                        ProductID = stockNew.ProductID,
                                        ProductVariableID = stockNew.ProductVariableID,
                                        Quantity = stockNew.Quantity,
                                        QuantityCurrent = stockNew.Quantity,
                                        Type = stockNew.Type,
                                        NoteID = stockNew.NoteID,
                                        OrderID = stockNew.OrderID,
                                        Status = stockNew.Status,
                                        SKU = stockNew.SKU,
                                        ParentID = stockNew.ParentID,
                                        CreatedDate = stockNew.CreatedDate,
                                        CreatedBy = stockNew.CreatedBy,
                                        MoveProID = stockNew.MoveProID
                                    };
                                }
                                else
                                {
                                    return new tbl_StockManager()
                                    {
                                        AgentID = stockNew.AgentID,
                                        ProductID = stockNew.ProductID,
                                        ProductVariableID = stockNew.ProductVariableID,
                                        Quantity = stockNew.Quantity,
                                        QuantityCurrent = quantityCurrent,
                                        Type = stockNew.Type,
                                        NoteID = stockNew.NoteID,
                                        OrderID = stockNew.OrderID,
                                        Status = stockNew.Status,
                                        SKU = stockNew.SKU,
                                        ParentID = stockNew.ParentID,
                                        CreatedDate = stockNew.CreatedDate,
                                        CreatedBy = stockNew.CreatedBy,
                                        MoveProID = stockNew.MoveProID
                                    };
                                }
                            }
                            else
                            {
                                stockBalance.Add(
                                    new tbl_StockManager()
                                    {
                                        AgentID = stockNew.AgentID,
                                        ProductID = stockNew.ProductID,
                                        ProductVariableID = stockNew.ProductVariableID,
                                        Quantity = stockNew.Quantity,
                                        QuantityCurrent = 0,
                                        Type = 1,
                                        NoteID = "Nhập kho sản phẩm mới",
                                        OrderID = stockNew.OrderID,
                                        Status = stockNew.Status,
                                        SKU = stockNew.SKU,
                                        ParentID = stockNew.ParentID,
                                        CreatedDate = stockNew.CreatedDate.Value.AddMilliseconds(-10),
                                        CreatedBy = stockNew.CreatedBy,
                                        MoveProID = stockNew.MoveProID
                                    });

                                return new tbl_StockManager()
                                {
                                    AgentID = stockNew.AgentID,
                                    ProductID = stockNew.ProductID,
                                    ProductVariableID = stockNew.ProductVariableID,
                                    Quantity = stockNew.Quantity,
                                    QuantityCurrent = stockNew.Quantity,
                                    Type = stockNew.Type,
                                    NoteID = stockNew.NoteID,
                                    OrderID = stockNew.OrderID,
                                    Status = stockNew.Status,
                                    SKU = stockNew.SKU,
                                    ParentID = stockNew.ParentID,
                                    CreatedDate = stockNew.CreatedDate,
                                    CreatedBy = stockNew.CreatedBy,
                                    MoveProID = stockNew.MoveProID
                                };
                            }
                        })
                    .ToList();

                var stockInsert = new List<tbl_StockManager>();
                stockInsert.AddRange(stockInput);
                stockInsert.AddRange(stockBalance);
                stockInsert.AddRange(stockOut);

                var index = 1;

                foreach (tbl_StockManager stock in stockInsert)
                {
                    if (index >= 100)
                    {
                        index = 1;
                        con.tbl_StockManager.Add(stock);
                        con.SaveChanges();
                    }
                    else
                    {
                        con.tbl_StockManager.Add(stock);
                    }

                    index++;
                }

                con.SaveChanges();

            }
        }

        public static string Update(tbl_StockManager stock)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_StockManager ui = dbe.tbl_StockManager.Where(a => a.ID == stock.ID).SingleOrDefault();
                if (ui != null)
                {
                    ui.AgentID = stock.AgentID;
                    ui.ProductVariableID = stock.ProductVariableID;
                    ui.Quantity = stock.Quantity;
                    ui.QuantityCurrent = stock.QuantityCurrent;
                    ui.Type = stock.Type;
                    ui.NoteID = stock.NoteID;
                    ui.OrderID = stock.OrderID;
                    ui.Status = stock.Status;
                    ui.SKU = stock.SKU;
                    ui.ModifiedBy = stock.ModifiedBy;
                    ui.ModifiedDate = stock.ModifiedDate;
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static tbl_StockManager GetByID(int ID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_StockManager ai = dbe.tbl_StockManager.Where(a => a.ID == ID).SingleOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }
        public static List<tbl_StockManager> GetAll()
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_StockManager> ags = new List<tbl_StockManager>();
                ags = dbe.tbl_StockManager.ToList();
                return ags;
            }
        }
        //public static List<tbl_StockManager> GetByProductName(int AgentID,string ProductName)
        //{
        //    using (var dbe = new inventorymanagementEntities())
        //    {
        //        List<tbl_StockManager> ags = new List<tbl_StockManager>();
        //        ags = dbe.tbl_StockManager.Where(i => i.AgentID == AgentID && i.ProductName.Contains(ProductName)).ToList();
        //        return ags;
        //    }
        //}
        public static List<tbl_StockManager> GetBySKU(int AgentID, string SKU)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_StockManager> ags = new List<tbl_StockManager>();
                ags = dbe.tbl_StockManager.Where(i => i.AgentID == AgentID && i.SKU == SKU).ToList();
                return ags;
            }
        }

        public static List<tbl_StockManager> GetBySKU(string SKU)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_StockManager> ags = new List<tbl_StockManager>();
                ags = dbe.tbl_StockManager.Where(i => i.SKU == SKU).ToList();
                return ags;
            }
        }

        public static tbl_StockManager GetStockID(Nullable<int> productID = null, Nullable<int> productVariableID = null) {
            tbl_StockManager stock = new tbl_StockManager();
            bool exists = false;
            String sql = String.Empty;

            sql += " SELECT TOP 1 ";
            sql += "         ID ";
            sql += " ,       AgentID ";
            sql += " ,       ProductID ";
            sql += " ,       ProductVariableID ";
            sql += " ,       Quantity ";
            sql += " ,       QuantityCurrent ";
            sql += " ,       Type ";
            sql += " FROM ";
            sql += "         tbl_StockManager ";
            sql += " WHERE ";
            sql += "         1 = 1 ";

            if (productID != null)
            {
                sql += "         AND ProductID = " + productID.Value.ToString();
            }

            if (productVariableID != null)
            {
                sql += "         AND ProductVariableID = " + productVariableID.Value.ToString();
            }

            sql += " ORDER BY ";
            sql += "     CreatedDate DESC ";

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);

            while (reader.Read()) {
                exists = true;

                stock.ID = Convert.ToInt32(reader["ID"]);

                if(reader["AgentID"] != DBNull.Value) {
                    stock.AgentID = Convert.ToInt32(reader["AgentID"]);
                }

                if (reader["ProductID"] != DBNull.Value)
                {
                    stock.ProductID = Convert.ToInt32(reader["ProductID"]);
                }

                if (reader["ProductVariableID"] != DBNull.Value)
                {
                    stock.ProductVariableID = Convert.ToInt32(reader["ProductVariableID"]);
                }

                if (reader["Quantity"] != DBNull.Value)
                {
                    stock.Quantity = Convert.ToDouble(reader["Quantity"]);
                }

                if (reader["QuantityCurrent"] != DBNull.Value)
                {
                    stock.QuantityCurrent = Convert.ToDouble(reader["QuantityCurrent"]);
                }

                if (reader["Type"] != DBNull.Value)
                {
                    stock.Type = Convert.ToInt32(reader["Type"]);
                }
            }

            reader.Close();

            if (exists)
            {
                return stock;
            }
            else
            {
                return null;
            }
        }
        public static List<tbl_StockManager> GetStockAll()
        {
            using (var con = new inventorymanagementEntities())
            {

                var stockToDay = con.tbl_StockManager
                    .GroupBy(x => new { x.SKU })
                    .Select(x => new
                    {
                        SKU = x.Key.SKU,
                        CreatedDate = x.Max(row => row.CreatedDate)
                    });

                return con.tbl_StockManager
                            .Join(
                                stockToDay,
                                stock => new { stock.SKU, stock.CreatedDate },
                                stock_max => new { stock_max.SKU, stock_max.CreatedDate },
                                (stock, stock_max) => stock)
                             .ToList();
            }
        }
        public static List<tbl_StockManager> GetStockToDay()
        {
            using (var con = new inventorymanagementEntities())
            {
                var today = DateTime.Today.AddDays(-1);
                var tomorrow = today.AddDays(2);

                var stockToDay = con.tbl_StockManager
                    .Where(x => today <= x.CreatedDate && x.CreatedDate < tomorrow)
                    .GroupBy(x => new {x.SKU})
                    .Select(x => new
                    {
                        SKU = x.Key.SKU,
                        CreatedDate = x.Max(row => row.CreatedDate)
                    });

                return con.tbl_StockManager
                            .Join(
                                stockToDay,
                                stock => new { stock.SKU, stock.CreatedDate },
                                stock_max => new { stock_max.SKU, stock_max.CreatedDate },
                                (stock, stock_max) => stock)
                             .ToList();
            }
        }
        public static List<tbl_StockManager> GetStockProduct()
        {
            using (var con = new inventorymanagementEntities())
            {

                var stockAll = con.tbl_StockManager
                    .Where(x => x.ProductID != 0 && x.ProductVariableID == 0)
                    .GroupBy(x => new { x.ProductID, x.ProductVariableID })
                    .Select(x => new
                    {
                        ProductID = x.Key.ProductID,
                        ProductVariableID = x.Key.ProductVariableID,
                        CreatedDate = x.Max(row => row.CreatedDate)
                    });

                return con.tbl_StockManager
                            .Join(
                                stockAll,
                                stock => new {stock.ProductID, stock.ProductVariableID, stock.CreatedDate },
                                stock_max => new { stock_max.ProductID, stock_max.ProductVariableID, stock_max.CreatedDate },
                                (stock, stock_max) => stock)
                             .ToList();
            }
        }
        public static List<tbl_StockManager> GetStockVariable()
        {
            using (var con = new inventorymanagementEntities())
            {

                var stockAll = con.tbl_StockManager
                    .Where(x => x.ProductID == 0 && x.ProductVariableID != 0)
                    .GroupBy(x => new {x.ProductID, x.ProductVariableID })
                    .Select(x => new
                    {
                        ProductID = x.Key.ProductID,
                        ProductVariableID = x.Key.ProductVariableID,
                        CreatedDate = x.Max(row => row.CreatedDate)
                    });

                return con.tbl_StockManager
                            .Join(
                                stockAll,
                                stock => new {stock.ProductID, stock.ProductVariableID, stock.CreatedDate },
                                stock_max => new { stock_max.ProductID, stock_max.ProductVariableID, stock_max.CreatedDate },
                                (stock, stock_max) => stock)
                             .ToList();
            }
        }
        #endregion
    }
}