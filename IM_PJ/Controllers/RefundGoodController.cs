using IM_PJ.Models;
using MB.Extensions;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using WebUI.Business;

namespace IM_PJ.Controllers
{
    public class RefundGoodController
    {
        #region CRUD
        public static int Insert(int AgentID, string TotalPrice, int Status, int CustomerID, int TotalQuantity, string TotalRefundFee,
            string AgentName, string CustomerName, string CustomerPhone, DateTime CreatedDate, string CreatedBy, string RefundNote)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_RefundGoods a = new tbl_RefundGoods();
                a.AgentID = AgentID;
                a.TotalPrice = TotalPrice;
                a.Status = Status;
                a.CustomerID = CustomerID;
                a.TotalQuantity = TotalQuantity;
                a.TotalRefundFee = TotalRefundFee;
                a.AgentName = AgentName;
                a.CustomerName = CustomerName;
                a.CustomerPhone = CustomerPhone;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                a.RefundNote = RefundNote;
                dbe.tbl_RefundGoods.Add(a);
                dbe.SaveChanges();
                int kq = a.ID;
                return kq;
            }
        }

        public static int Insert(tbl_RefundGoods refund)
        {
            using (var con = new inventorymanagementEntities())
            {
                con.tbl_RefundGoods.Add(refund);
                con.SaveChanges();
                int kq = refund.ID;
                return kq;
            }
        }

        public static string updatestatus(int ID, int status, DateTime ModifiedDate, string ModifiedBy, string Note)
        {
            using (var dbe = new inventorymanagementEntities())
            {

                var a = dbe.tbl_RefundGoods.Where(ac => ac.ID == ID).SingleOrDefault();
                if (a != null)
                {
                    a.Status = status;
                    a.RefundNote = Note;
                    a.ModifiedBy = ModifiedBy;
                    a.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }

        public static string UpdateCustomerPhone(int ID, string CustomerPhone)
        {
            using (var dbe = new inventorymanagementEntities())
            {

                var a = dbe.tbl_RefundGoods.Where(ac => ac.ID == ID).FirstOrDefault();
                if (a != null)
                {
                    a.RefundNote = a.RefundNote + ". Số điện thoại cũ của khách hàng này là " + a.CustomerPhone;
                    a.CustomerPhone = CustomerPhone;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }

        public static bool DeleteByID(int ID)
        {
            using (var con = new inventorymanagementEntities())
            {
                var item = con.tbl_RefundGoods.Where(x => x.ID == ID).SingleOrDefault();

                if (item != null)
                {
                    con.tbl_RefundGoods.Remove(item);
                    con.SaveChanges();
                    return true;
                }

                return false;
            }
        }

        #endregion
        #region Select        
        public static List<tbl_RefundGoods> GetAll(string s)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_RefundGoods> las = new List<tbl_RefundGoods>();
                las = dbe.tbl_RefundGoods.OrderByDescending(r => r.CreatedDate).ToList();
                return las;
            }
        }
        public static List<tbl_RefundGoods> GetByCustomerID(int CustomerID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_RefundGoods> las = new List<tbl_RefundGoods>();
                las = dbe.tbl_RefundGoods.Where(r => r.CustomerID == CustomerID).OrderByDescending(r => r.CreatedDate).ToList();
                return las;
            }
        }

        public static int GetByCustomerID(int CustomerID, int Status)
        {
            using (var con = new inventorymanagementEntities())
            {
                var refundTarget = con.tbl_RefundGoods
                    .Where(x => x.CustomerID == CustomerID)
                    .Where(x => x.Status == Status)
                    .OrderBy(x => x.ID);

                var refundDetailTarget = con.tbl_RefundGoodsDetails.OrderBy(x => x.RefundGoodsID).ThenBy(x => x.ID);

                var infoRefund = refundTarget
                    .Join(
                        refundDetailTarget,
                        refund => refund.ID,
                        detail => detail.RefundGoodsID,
                        (refund, detail) => new
                        {
                            RefundGoodsID = refund.ID,
                            ProductStyle = detail.ProductType.Value,
                            SKU = detail.SKU,
                            SoldPrice = detail.SoldPricePerProduct,
                            Quantity = detail.Quantity.Value,
                            TotalRefundPrice = refund.TotalPrice,
                            TotalRefundFee = refund.TotalRefundFee
                        })
                      .ToList();

                return infoRefund.GroupBy(x => x.RefundGoodsID).Count();
            }
        }

        public static List<tbl_RefundGoods> GetByAgentIDCustomerIDFromDateToDate(int AgentID, int CustomerID, DateTime FromDate, DateTime ToDate)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_RefundGoods> las = new List<tbl_RefundGoods>();
                las = dbe.tbl_RefundGoods.Where(r => r.AgentID == AgentID && r.CustomerID == CustomerID && r.CreatedDate >= FromDate && r.CreatedDate < ToDate).OrderByDescending(r => r.CreatedDate).ToList();
                return las;
            }
        }
        public static List<tbl_RefundGoods> GetByAgentID(int AgentID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_RefundGoods> las = new List<tbl_RefundGoods>();
                las = dbe.tbl_RefundGoods.Where(r => r.AgentID == AgentID).OrderByDescending(r => r.CreatedDate).ToList();
                return las;
            }
        }
        public static List<tbl_RefundGoods> GetByAgent(int AgentID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_RefundGoods> las = new List<tbl_RefundGoods>();

                las = dbe.tbl_RefundGoods.Where(r => r.AgentID == AgentID).OrderByDescending(r => r.CreatedDate).ToList();
                var a = las.GroupBy(r => r.CreatedBy).ToList();
                return las;
            }
        }
        public static tbl_RefundGoods GetByID(int ID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_RefundGoods acc = dbe.tbl_RefundGoods.Where(a => a.ID == ID).FirstOrDefault();
                if (acc != null)
                    return acc;
                else
                    return null;
            }
        }

        public static tbl_RefundGoods GetOrderByID(int ID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_RefundGoods acc = dbe.tbl_RefundGoods.Where(a => a.ID == ID && a.Status == 1).FirstOrDefault();
                if (acc != null)
                    return acc;
                else
                    return null;
            }
        }
        public static tbl_RefundGoods GetByIDAndAgentID(int ID, int AgentID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                var las = dbe.tbl_RefundGoods.Where(r => r.AgentID == AgentID && r.ID == ID).FirstOrDefault();
                if (las != null)
                    return las;
                else return null;
            }
        }

        public static int UpdateStatus(int ID, string createdby, int status, int orderSale)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                var las = dbe.tbl_RefundGoods.Where(r => r.ID == ID).FirstOrDefault();
                if (las != null)
                {
                    las.Status = status;
                    if(orderSale > 0)
                    {
                        las.RefundNote = "Đã trừ tiền trong đơn " + orderSale.ToString();
                    }
                    else
                    {
                        las.RefundNote = "";
                    }
                    las.OrderSaleID = orderSale;
                    las.CreatedBy = createdby;
                    las.ModifiedBy = createdby;
                    las.ModifiedDate = DateTime.Now;
                    int i = dbe.SaveChanges();
                    return i;
                }
                return 0;
            }
        }

        public static List<RefundOrder> Filter(string TextSearch, int Status, string RefundFee, string CreatedBy, string CreatedDate)
        {
            var list = new List<RefundOrder>();
            var sql = new StringBuilder();

            sql.AppendLine(String.Format("SELECT Ord.ID, Ord.CustomerName, Ord.CustomerPhone, Customer.Nick, Ord.CustomerID, Ord.Status, Ord.TotalPrice, Ord.TotalRefundFee, Ord.CreatedBy, Ord.CreatedDate, Ord.RefundNote, Ord.OrderSaleID, SUM(ISNULL(OrdDetail.Quantity, 0)) AS Quantity "));
            sql.AppendLine(String.Format("FROM tbl_RefundGoods AS Ord"));
            sql.AppendLine(String.Format("INNER JOIN tbl_RefundGoodsDetails AS OrdDetail"));
            sql.AppendLine(String.Format("ON 	Ord.ID = OrdDetail.RefundGoodsID"));
            sql.AppendLine(String.Format("INNER JOIN tbl_Customer AS Customer"));
            sql.AppendLine(String.Format("ON 	Ord.CustomerID = Customer.ID"));
            sql.AppendLine(String.Format("WHERE 1 = 1"));

            if (Status > 0)
            {
                sql.AppendLine(String.Format("	AND Ord.Status = {0}", Status));
            }

            if (RefundFee != "")
            {
                if(RefundFee == "yes")
                {
                    sql.AppendLine(String.Format("	AND Ord.TotalRefundFee > 0"));
                }
                else
                {
                    sql.AppendLine(String.Format("	AND Ord.TotalRefundFee = 0"));
                }
            }

            if (TextSearch != "")
            {
                string TextSearchName = '"' + TextSearch + '"';
                sql.AppendLine(String.Format("	AND (  CONTAINS(Ord.CustomerName, '{1}') OR CONTAINS(Customer.Nick, '{1}') OR (Ord.CustomerPhone = '{0}') OR (convert(nvarchar, Ord.ID) = '{0}') OR (convert(nvarchar, Ord.OrderSaleID) = '{0}') OR (OrdDetail.SKU LIKE '{0}%')  )", TextSearch, TextSearchName));
            }

            if (CreatedBy != "")
            {
                sql.AppendLine(String.Format("	AND Ord.CreatedBy = '{0}'", CreatedBy));
            }

            if (CreatedDate != "")
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
                sql.AppendLine(String.Format("	AND	(CONVERT(NVARCHAR(10), Ord.CreatedDate, 121) BETWEEN CONVERT(NVARCHAR(10), '{0:yyyy-MM-dd}', 121) AND CONVERT(NVARCHAR(10), '{1:yyyy-MM-dd}', 121))", fromdate, todate));
            }

            sql.AppendLine(String.Format("GROUP BY Ord.ID, Ord.CustomerName, Ord.CustomerPhone, Customer.Nick, Ord.CustomerID, Ord.Status, Ord.TotalPrice, Ord.TotalRefundFee, Ord.CreatedBy, Ord.CreatedDate, Ord.RefundNote, Ord.OrderSaleID"));
            sql.AppendLine(String.Format("ORDER BY Ord.ID DESC"));

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql.ToString());
            while (reader.Read())
            {
                var entity = new RefundOrder();

                entity.ID = Convert.ToInt32(reader["ID"]);
                entity.CustomerName = reader["CustomerName"].ToString();
                entity.CustomerPhone = reader["CustomerPhone"].ToString();
                entity.CustomerID = Convert.ToInt32(reader["CustomerID"]);
                entity.Nick = reader["Nick"].ToString();
                entity.Status = Convert.ToInt32(reader["Status"]);
                entity.TotalPrice = Convert.ToInt32(reader["TotalPrice"]);
                entity.TotalRefundFee = Convert.ToInt32(reader["TotalRefundFee"]);
                entity.CreatedBy = reader["CreatedBy"].ToString();
                entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                if (reader["RefundNote"] != DBNull.Value)
                    entity.RefundNote = reader["RefundNote"].ToString();
                if (reader["OrderSaleID"] != DBNull.Value)
                    entity.OrderSaleID = Convert.ToInt32(reader["OrderSaleID"]);
                entity.Quantity = Convert.ToInt32(reader["Quantity"]);

                list.Add(entity);
            }
            reader.Close();
            return list;
        }

        public static UserReportModel getUserReport(string CreatedBy, DateTime fromDate, DateTime toDate)
        {
            var list = new List<RefundReport>();
            var sql = new StringBuilder();

            sql.AppendLine(String.Format("SELECT Ord.ID, SUM(ISNULL(OrdDetail.Quantity, 0)) AS Quantity, SUM(OrdDetail.Quantity * ISNULL(Product.CostOfGood, Variable.CostOfGood)) AS TotalCost, SUM(OrdDetail.Quantity * OrdDetail.SoldPricePerProduct) AS TotalRevenue, SUM(OrdDetail.Quantity * OrdDetail.RefundFeePerProduct) AS TotalRefundFee"));
            sql.AppendLine(String.Format("FROM tbl_RefundGoods AS Ord"));
            sql.AppendLine(String.Format("INNER JOIN tbl_RefundGoodsDetails AS OrdDetail"));
            sql.AppendLine(String.Format("ON 	Ord.ID = OrdDetail.RefundGoodsID"));
            sql.AppendLine(String.Format("LEFT JOIN tbl_ProductVariable AS Variable"));
            sql.AppendLine(String.Format("ON 	OrdDetail.SKU = Variable.SKU"));
            sql.AppendLine(String.Format("LEFT JOIN tbl_Product AS Product"));
            sql.AppendLine(String.Format("ON 	OrdDetail.SKU = Product.ProductSKU"));
            sql.AppendLine(String.Format("WHERE 1 = 1"));

            if (!String.IsNullOrEmpty(CreatedBy))
            {
                sql.AppendLine(String.Format("    AND Ord.CreatedBy = '{0}'", CreatedBy));
            }
            sql.AppendLine(String.Format("	AND	(CONVERT(NVARCHAR(10), Ord.CreatedDate, 121) BETWEEN CONVERT(NVARCHAR(10), '{0:yyyy-MM-dd}', 121) AND CONVERT(NVARCHAR(10), '{1:yyyy-MM-dd}', 121))", fromDate, toDate));
            sql.AppendLine(String.Format("GROUP BY Ord.ID, OrdDetail.RefundFeePerProduct"));

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql.ToString());
            while (reader.Read())
            {
                var entity = new RefundReport();

                entity.ID = Convert.ToInt32(reader["ID"]);
                entity.Quantity = Convert.ToInt32(reader["Quantity"]);
                entity.TotalCost = Convert.ToDouble(reader["TotalCost"]);
                entity.TotalRevenue = Convert.ToDouble(reader["TotalRevenue"]);
                entity.TotalRefundFee = Convert.ToInt32(reader["TotalRefundFee"]);
                list.Add(entity);
            }
            reader.Close();

            return new UserReportModel()
            {
                totalRefundQuantity = list.Sum(x => x.Quantity),
                totalRevenue = list.Sum(x => x.TotalRevenue),
                totalCost = list.Sum(x => x.TotalCost),
                totalRefundFee = list.Sum(x => x.TotalRefundFee),
            };
        }

        public class UserReportModel
        {
            public int totalRefundQuantity { get; set; }
            public double totalCost { get; set; }
            public double totalRevenue { get; set; }
            public double totalRefundFee { get; set; }
        }

        public static List<RefundProductReportModel> getRefundProductReport(string SKU, int CategoryID, string CreatedBy, DateTime fromDate, DateTime toDate)
        {
            var list = new List<RefundReport>();
            var sql = new StringBuilder();

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

            sql.AppendLine("SELECT");
            sql.AppendLine("    CONVERT(VARCHAR(10), Ord.CreatedDate, 121) AS CreatedDate,");
            sql.AppendLine("    Ord.ID,");
            sql.AppendLine("    OrdDetail.SKU,");
            sql.AppendLine("    OrdDetail.Quantity,");
            sql.AppendLine("    OrdDetail.SoldPricePerProduct,");
            sql.AppendLine("    OrdDetail.RefundFeePerProduct");
            sql.AppendLine("INTO #data");
            sql.AppendLine("FROM tbl_RefundGoods AS Ord");
            sql.AppendLine("INNER JOIN tbl_RefundGoodsDetails AS OrdDetail");
            sql.AppendLine("ON     Ord.ID = OrdDetail.RefundGoodsID");
            sql.AppendLine("WHERE 1 = 1");

            if (!String.IsNullOrEmpty(CreatedBy))
            {
                sql.AppendLine(String.Format("    AND Ord.CreatedBy = '{0}'", CreatedBy));
            }

            if (!String.IsNullOrEmpty(SKU))
            {
                sql.AppendLine(String.Format("    AND OrdDetail.SKU LIKE '{0}%'", SKU));
            }

            sql.AppendLine(String.Format("    AND    CONVERT(NVARCHAR(10), Ord.CreatedDate, 121) BETWEEN CONVERT(NVARCHAR(10), '{0:yyyy-MM-dd}', 121) AND CONVERT(NVARCHAR(10), '{1:yyyy-MM-dd}', 121)", fromDate, toDate));

            sql.AppendLine("SELECT");
            sql.AppendLine("    DAT.CreatedDate,");
            sql.AppendLine("    DAT.ID,");
            sql.AppendLine("    SUM(ISNULL(DAT.Quantity, 0)) AS Quantity,");
            sql.AppendLine("    SUM(DAT.Quantity * ISNULL(PRO.CostOfGood, 0)) AS TotalCost,");
            sql.AppendLine("    SUM(DAT.Quantity * DAT.SoldPricePerProduct) AS TotalRevenue,");
            sql.AppendLine("    SUM(DAT.Quantity * DAT.RefundFeePerProduct) AS TotalRefundFee");
            sql.AppendLine("FROM #data AS DAT");
            sql.AppendLine("INNER JOIN (");
            sql.AppendLine("    SELECT");
            sql.AppendLine("        Product.CategoryID,");
            sql.AppendLine("        (");
            sql.AppendLine("            CASE Product.ProductStyle");
            sql.AppendLine("                WHEN 1 THEN Product.ProductSKU");
            sql.AppendLine("                ELSE Variable.SKU");
            sql.AppendLine("            END");
            sql.AppendLine("        ) AS SKU,");
            sql.AppendLine("        (");
            sql.AppendLine("            CASE Product.ProductStyle");
            sql.AppendLine("                WHEN 1 THEN Product.CostOfGood");
            sql.AppendLine("                ELSE Variable.CostOfGood");
            sql.AppendLine("            END");
            sql.AppendLine("        ) AS CostOfGood");
            sql.AppendLine("    FROM tbl_Product AS Product");
            sql.AppendLine("    LEFT JOIN tbl_ProductVariable AS Variable");
            sql.AppendLine("    ON Product.ID = Variable.ProductID");
            sql.AppendLine("    WHERE 1 = 1");
            if (CategoryID > 0)
            {
                sql.AppendLine("    AND EXISTS(");
                sql.AppendLine("            SELECT");
                sql.AppendLine("                    NULL AS DUMMY");
                sql.AppendLine("            FROM");
                sql.AppendLine("                    #category");
                sql.AppendLine("            WHERE");
                sql.AppendLine("                    ID = Product.CategoryID");
                sql.AppendLine("    )");
            }
            sql.AppendLine(") AS PRO");
            sql.AppendLine("ON     DAT.SKU = PRO.SKU");
            sql.AppendLine("GROUP BY");
            sql.AppendLine("    DAT.CreatedDate");
            sql.AppendLine(",   DAT.ID");
            sql.AppendLine(" END");

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql.ToString());
            while (reader.Read())
            {
                var entity = new RefundReport();
                entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                entity.ID = Convert.ToInt32(reader["ID"]);
                entity.Quantity = Convert.ToInt32(reader["Quantity"]);
                entity.TotalCost = Convert.ToDouble(reader["TotalCost"]);
                entity.TotalRevenue = Convert.ToDouble(reader["TotalRevenue"]);
                entity.TotalRefundFee = Convert.ToInt32(reader["TotalRefundFee"]);
                list.Add(entity);
            }
            reader.Close();

            var result = list.GroupBy(g => g.CreatedDate)
                .Select(x => new RefundProductReportModel()
                {
                    reportDate = x.Key,
                    totalRefund = x.Sum(s => s.Quantity),
                    totalRevenue = x.Sum(s => s.TotalRevenue),
                    totalCost = x.Sum(s => s.TotalCost),
                    totalRefundFee = x.Sum(s => s.TotalRefundFee),
                })
                .OrderBy(o => o.reportDate)
                .ToList();

            return result;
        }

        public class RefundProductReportModel
        {
            public DateTime reportDate { get; set; }
            public int totalRefund { get; set; }
            public double totalCost { get; set; }
            public double totalRevenue { get; set; }
            public double totalRefundFee { get; set; }
        }

        public static List<tbl_RefundGoods> Search(string s, int n, int status, string by)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_RefundGoods> ags = new List<tbl_RefundGoods>();
                if (!string.IsNullOrEmpty(s))
                {
                    if (n > 0)
                    {
                        if (status > 0)
                        {
                            if (!string.IsNullOrEmpty(by))
                            {
                                ags = dbe.tbl_RefundGoods.Where(x => x.CustomerName.Contains(s) && x.AgentID == n && x.Status == status && x.CreatedBy == by || x.CreatedBy == by && x.CustomerPhone.Contains(s) && x.AgentID == n && x.Status == status || x.ID.ToString().Contains(s) && x.CreatedBy == by && x.AgentID == n && x.Status == status).ToList();
                            }
                            else
                            {
                                ags = dbe.tbl_RefundGoods.Where(x => x.CustomerName.Contains(s) && x.AgentID == n && x.Status == status || x.CustomerPhone.Contains(s) && x.AgentID == n && x.Status == status || x.ID.ToString().Contains(s) && x.AgentID == n && x.Status == status).ToList();
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(by))
                            {
                                ags = dbe.tbl_RefundGoods.Where(x => x.CustomerName.Contains(s) && x.AgentID == n && x.CreatedBy == by || x.CreatedBy == by && x.CustomerPhone.Contains(s) && x.AgentID == n || x.ID.ToString().Contains(s) && x.AgentID == n && x.CreatedBy == by).ToList();

                            }
                            else
                            {
                                ags = dbe.tbl_RefundGoods.Where(x => x.CustomerName.Contains(s) && x.AgentID == n || x.CustomerPhone.Contains(s) && x.AgentID == n || x.ID.ToString().Contains(s) && x.AgentID == n).ToList();
                            }
                        }
                    }
                    else
                    {
                        if (status > 0)
                        {

                            ags = dbe.tbl_RefundGoods.Where(x => x.CustomerName.Contains(s) && x.AgentID == n && x.Status == status || x.CustomerPhone.Contains(s) && x.AgentID == n && x.Status == status || x.ID.ToString().Contains(s) && x.AgentID == n && x.Status == status).ToList();
                        }
                        else
                        {
                            ags = dbe.tbl_RefundGoods.Where(x => x.CustomerName.Contains(s) || x.CustomerPhone.Contains(s) || x.ID.ToString().Contains(s)).ToList();
                        }

                    }
                }
                else
                {
                    if (n > 0)
                    {
                        if (status > 0)
                        {
                            if (!string.IsNullOrEmpty(by))
                            {
                                ags = dbe.tbl_RefundGoods.Where(x => x.AgentID == n && x.Status == status && x.CreatedBy == by).ToList();
                            }
                            else
                            {
                                ags = dbe.tbl_RefundGoods.Where(x => x.AgentID == n && x.Status == status).ToList();
                            }

                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(by))
                            {
                                ags = dbe.tbl_RefundGoods.Where(x => x.AgentID == n && x.CreatedBy == by).ToList();
                            }
                            else
                            {
                                ags = dbe.tbl_RefundGoods.Where(x => x.AgentID == n).ToList();
                            }

                        }
                    }
                    else
                    {
                        if (status > 0)
                        {
                            if (!string.IsNullOrEmpty(by))
                            {
                                ags = dbe.tbl_RefundGoods.Where(x => x.Status == status && x.CreatedBy == by).ToList();
                            }
                            else
                            {
                                ags = dbe.tbl_RefundGoods.Where(x => x.Status == status).ToList();
                            }

                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(by))
                            {
                                ags = dbe.tbl_RefundGoods.Where(x => x.CreatedBy == by).ToList();
                            }
                            else
                            {
                                ags = dbe.tbl_RefundGoods.ToList();
                            }

                        }

                    }
                }
                return ags.OrderByDescending(x => x.CreatedDate).ToList();
            }
        }

        public static List<tbl_RefundGoods> TotalRefund(string fromdate, string todate)
        {
            using (var db = new inventorymanagementEntities())
            {
                List<tbl_RefundGoods> or = new List<tbl_RefundGoods>();
                if (!string.IsNullOrEmpty(fromdate))
                {
                    if (!string.IsNullOrEmpty(todate))
                    {
                        DateTime fd = Convert.ToDateTime(fromdate);
                        DateTime td = Convert.ToDateTime(todate);
                        or = db.tbl_RefundGoods
                            .Where(r => r.CreatedDate >= fd && r.CreatedDate <= td)
                            .OrderByDescending(r => r.ID).ToList();
                    }
                    else
                    {
                        DateTime fd = Convert.ToDateTime(fromdate);
                        or = db.tbl_RefundGoods
                            .Where(r => r.CreatedDate >= fd)
                            .OrderByDescending(r => r.ID).ToList();
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(todate))
                    {
                        DateTime td = Convert.ToDateTime(todate);
                        or = db.tbl_RefundGoods
                            .Where(r => r.CreatedDate <= td)
                            .OrderByDescending(r => r.ID).ToList();
                    }
                    else
                    {
                        or = db.tbl_RefundGoods.ToList();
                    }
                }
                return or;
            }
        }

        public static int GetTotalRefundByAccount(string accountName, DateTime fromdate, DateTime todate)
        {
            using (var db = new inventorymanagementEntities())
            {
                List<tbl_RefundGoods> or = new List<tbl_RefundGoods>();
                or = db.tbl_RefundGoods
                            .Where(x => (fromdate <= x.CreatedDate && x.CreatedDate <= todate)
                                        && x.CreatedBy == accountName)
                            .ToList();
                int tongdoitra = 0;
                if (or != null)
                {
                    foreach (var item in or)
                    {
                        var oddetail = RefundGoodDetailController.GetByRefundGoodsID(item.ID);
                        if (oddetail != null)
                        {
                            foreach (var temp in oddetail)
                            {
                                tongdoitra += Convert.ToInt32(temp.Quantity);
                            }
                        }
                    }
                }
                return tongdoitra;
            }
        }

        public static string getOrderReturnJSON(int customerID)
        {
            using (var con = new inventorymanagementEntities())
            {
                var orders = con.tbl_RefundGoods
                    .Where(x => x.CustomerID == customerID)
                    .Where(x => x.Status == 1)
                    .OrderByDescending(o => o.CreatedDate)
                    .ToArray();

                var serializer = new JavaScriptSerializer();
                return serializer.Serialize(orders);
            }
        }

        public static RefundPromotion getPromotion(int customerID)
        {
            var result = new RefundPromotion
            {
                IsPromotion = false,
                DecreasePrice = 0
            };

            #region Kiểm tra điều kiện để đc khuyến mãi
            var isUserApp = UserController.checkExists(customerID);

            if (!isUserApp)
                return result;

            var now = DateTime.Now;

            if (now < new DateTime(year: 2020, month: 7, day: 1))
                return result;
            if (now > new DateTime(year: 2020, month: 7, day: 31))
                return result;
            #endregion

            result.IsPromotion = true;
            result.DecreasePrice = 10e3;

            return result;
        }

        #endregion
        public class RefundReport
        {
            public DateTime CreatedDate { get; set; }
            public int ID { get; set; }
            public int Quantity { get; set; }
            public double TotalRevenue { get; set; }
            public double TotalCost { get; set; }
            public double TotalRefundFee { get; set; }
            
        }
        public class RefundOrder
        {
            public int ID { get; set; }
            public int CustomerID { get; set; }
            public string CustomerName { get; set; }
            public string CustomerPhone { get; set; }
            public string Nick { get; set; }
            public int Status { get; set; }
            public int Quantity { get; set; }
            public double TotalPrice { get; set; }
            public double TotalRefundFee { get; set; }
            public DateTime CreatedDate { get; set; }
            public string CreatedBy { get; set; }
            public string RefundNote { get; set; }
            public Nullable<int> OrderSaleID { get; set; }
        }

        public class RefundPromotion
        {
            public bool IsPromotion { get; set; }
            public double DecreasePrice { get; set; }
        }
    }
}