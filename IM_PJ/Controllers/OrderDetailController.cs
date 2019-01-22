using IM_PJ.Models;
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
    public class OrderDetailController
    {
        #region CRUD
        public static string Insert(int AgentID, int OrderID, string SKU, int ProductID, int ProductVariableID, string ProductVariableDescrition, double Quantity,
            double Price, int Status, double DiscountPrice, int ProductType, DateTime CreatedDate, string CreatedBy, bool IsCount)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_OrderDetail ui = new tbl_OrderDetail();
                ui.AgentID = AgentID;
                ui.OrderID = OrderID;
                ui.SKU = SKU;
                ui.ProductID = ProductID;
                ui.ProductVariableID = ProductVariableID;
                ui.ProductVariableDescrition = ProductVariableDescrition;
                ui.Quantity = Quantity;
                ui.Price = Convert.ToDouble(Price);
                ui.Status = Status;
                ui.DiscountPrice = DiscountPrice;
                ui.ProductType = ProductType;
                ui.CreatedDate = CreatedDate;
                ui.CreatedBy = CreatedBy;
                ui.IsCount = IsCount;
                dbe.tbl_OrderDetail.Add(ui);
                int kq = dbe.SaveChanges();
                return kq.ToString();
            }
        }

        public static void Insert(tbl_OrderDetail orderDetail)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_OrderDetail ui = new tbl_OrderDetail();
                ui.AgentID = orderDetail.AgentID;
                ui.OrderID = orderDetail.OrderID;
                ui.SKU = orderDetail.SKU;
                ui.ProductID = orderDetail.ProductID;
                ui.ProductVariableID = orderDetail.ProductVariableID;
                ui.ProductVariableDescrition = orderDetail.ProductVariableDescrition;
                ui.Quantity = orderDetail.Quantity;
                ui.Price = orderDetail.Price;
                ui.Status = orderDetail.Status;
                ui.DiscountPrice = orderDetail.DiscountPrice;
                ui.ProductType = orderDetail.ProductType;
                ui.CreatedDate = orderDetail.CreatedDate;
                ui.CreatedBy = orderDetail.CreatedBy;
                ui.IsCount = orderDetail.IsCount;
                dbe.tbl_OrderDetail.Add(ui);
                dbe.SaveChanges();
            }
        }

        public static void Insert(List<tbl_OrderDetail> orderDetails)
        {
            using (var con = new inventorymanagementEntities())
            {
                var index = 1;

                foreach(tbl_OrderDetail order in orderDetails)
                {
                    if (index >= 100)
                    {
                        index = 1;
                        con.tbl_OrderDetail.Add(order);
                        con.SaveChanges();
                    }
                    else
                    {
                        con.tbl_OrderDetail.Add(order);
                    }

                    index++;
                }

                con.SaveChanges();
            }
        }

        public static string UpdateQuantity(int ID, double Quantity, double Price, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                var ui = dbe.tbl_OrderDetail.Where(o => o.ID == ID).FirstOrDefault();
                if (ui != null)
                {
                    ui.Quantity = Quantity;
                    ui.Price = Price;
                    ui.CreatedDate = CreatedDate;
                    ui.CreatedBy = CreatedBy;
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                return "0";
            }
        }
        public static string UpdateIsCount(int ID, bool isCount)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                var ui = dbe.tbl_OrderDetail.Where(o => o.ID == ID).FirstOrDefault();
                if (ui != null)
                {
                    ui.IsCount = isCount;
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                return "0";
            }
        }
        public static string Delete(int ID,string SKU)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                var ui = dbe.tbl_OrderDetail.Where(o => o.ID == ID && o.SKU == SKU).FirstOrDefault();
                var od = dbe.tbl_OrderDetail.ToList();
                if (ui != null)
                {
                    dbe.tbl_OrderDetail.Remove(ui);
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                return "0";
            }
        }
        #endregion
        #region Select
        public static tbl_OrderDetail GetByID(int ID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_OrderDetail ai = dbe.tbl_OrderDetail.Where(a => a.ID == ID).SingleOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }
        public static List<tbl_OrderDetail> GetAll()
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_OrderDetail> ags = new List<tbl_OrderDetail>();
                ags = dbe.tbl_OrderDetail.OrderByDescending(o => o.ID).ToList();
                return ags;
            }
        }
        public static List<tbl_OrderDetail> GetAllByAgentID(int AgentID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_OrderDetail> ags = new List<tbl_OrderDetail>();
                ags = dbe.tbl_OrderDetail.Where(o => o.AgentID == AgentID).OrderByDescending(o => o.ID).ToList();
                return ags;
            }
        }
        public static List<tbl_OrderDetail> GetByOrderID(int OrderID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_OrderDetail> ags = new List<tbl_OrderDetail>();
                ags = dbe.tbl_OrderDetail.Where(o => o.OrderID == OrderID).ToList();
                return ags;
            }
        }
        public static List<tbl_OrderDetail> GetByIDSortBySKU(int OrderID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_OrderDetail> ags = new List<tbl_OrderDetail>();
                ags = dbe.tbl_OrderDetail.Where(o => o.OrderID == OrderID).OrderBy(o => o.SKU).ThenByDescending(o => o.Price).ToList();
                return ags;
            }
        }
        public static List<tbl_Order> GetByPaymentStatus(int PaymentStatus)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_Order> ags = new List<tbl_Order>();
                ags = dbe.tbl_Order.Where(o => o.PaymentStatus == PaymentStatus).OrderByDescending(o => o.ID).ToList();
                return ags;
            }
        }
        #endregion
        public static List<tbl_OrderDetail> GetAllToday()
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_OrderDetail> ags = new List<tbl_OrderDetail>();
                ags = dbe.tbl_OrderDetail.OrderByDescending(o => o.ID).ToList();
                return ags;
            }
        }

        public static List<orderReport> Report(string fromdate, string todate)
        {

            var list = new List<orderReport>();
            var sql = new StringBuilder();

            sql.AppendLine(String.Format("SELECT"));
            sql.AppendLine(String.Format("		ODD.ID"));
            sql.AppendLine(String.Format(",		ODD.SKU"));
            sql.AppendLine(String.Format(",		SUM(ISNULL(ODD.Quantity, 0)) AS Quantity"));
            sql.AppendLine(String.Format(",		SUM(ISNULL(ODD.Price, 0)) AS Price"));
            sql.AppendLine(String.Format(",		2 AS ExcuteStatus"));
            sql.AppendLine(String.Format(",		3 AS PaymentStatus"));
            sql.AppendLine(String.Format("FROM"));
            sql.AppendLine(String.Format("		tbl_Order AS ORD"));
            sql.AppendLine(String.Format("INNER JOIN tbl_OrderDetail AS ODD"));
            sql.AppendLine(String.Format("	ON 	ORD.ID = ODD.OrderID"));
            sql.AppendLine(String.Format("WHERE"));
            sql.AppendLine(String.Format("		CONVERT(datetime, ORD.DateDone, 121) BETWEEN CONVERT(datetime, '{0}', 121) AND CONVERT(datetime, '{1}', 121)", fromdate, todate));
            sql.AppendLine(String.Format("	AND ORD.ExcuteStatus = 2"));
            sql.AppendLine(String.Format("	AND ORD.PaymentStatus = 3"));
            sql.AppendLine(String.Format("GROUP BY"));
            sql.AppendLine(String.Format("		ODD.ID"));
            sql.AppendLine(String.Format(",		ODD.SKU"));
            sql.AppendLine(String.Format(";"));

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql.ToString());
            while (reader.Read())
            {
                var entity = new orderReport();

                entity.ID = Convert.ToInt32(reader["ID"]);

                entity.SKU = reader["SKU"].ToString();

                entity.Quantity = Convert.ToDouble(reader["Quantity"]);

                entity.Price = Convert.ToDouble(reader["Price"]);

                entity.ExcuteStatus = Convert.ToInt32(reader["ExcuteStatus"]);

                entity.ExcuteStatus = Convert.ToInt32(reader["PaymentStatus"]);

                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public class orderReport
        {
            public int ID { get; set; }
            public string SKU { get; set; }
            public double Quantity { get; set; }
            public double Price { get; set; }
            public DateTime DateDone { get; set; }
            public int ExcuteStatus { get; set; }
            public int PaymentStatus { get; set; }

        }
    }
}