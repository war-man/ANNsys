﻿using IM_PJ.Models;
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
    public class OrderController
    {
        #region CRUD
        public static tbl_Order Insert(int AgentID, int OrderType, string AdditionFee, string DisCount, int CustomerID, string CustomerName, string CustomerPhone,
            string CustomerAddress, string CustomerEmail, string TotalPrice, string TotalPriceNotDiscount, int PaymentStatus, int ExcuteStatus, bool IsHidden, int WayIn,
            DateTime CreatedDate, string CreatedBy, double DiscountPerProduct, double TotalDiscount, string FeeShipping, int PaymentType, int ShippingType, string DateDone, double GuestPaid, double GuestChange, int TransportCompanyID, int TransportCompanySubID, string OtherFeeName, double OtherFeeValue, int PostalDeliveryType)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Order ui = new tbl_Order();
                ui.AgentID = AgentID;
                ui.OrderType = OrderType;
                ui.AdditionFee = AdditionFee;
                ui.DisCount = DisCount;
                ui.CustomerID = CustomerID;
                ui.CustomerName = CustomerName;
                ui.CustomerPhone = CustomerPhone;
                ui.CustomerAddress = CustomerAddress;
                if (!string.IsNullOrEmpty(CustomerEmail))
                    ui.CustomerEmail = CustomerEmail;
                ui.TotalPrice = TotalPrice;
                ui.TotalPriceNotDiscount = TotalPriceNotDiscount;
                ui.PaymentStatus = PaymentStatus;
                ui.ExcuteStatus = ExcuteStatus;
                ui.IsHidden = IsHidden;
                ui.WayIn = WayIn;
                ui.IsHidden = IsHidden;
                ui.DiscountPerProduct = DiscountPerProduct;
                ui.TotalDiscount = TotalDiscount;
                ui.FeeShipping = FeeShipping;
                ui.CreatedDate = CreatedDate;
                ui.CreatedBy = CreatedBy;
                ui.PaymentType = PaymentType;
                ui.ShippingType = ShippingType;
                ui.GuestPaid = GuestPaid;
                ui.GuestChange = GuestChange;
                ui.TransportCompanyID = TransportCompanyID;
                ui.TransportCompanySubID = TransportCompanySubID;
                ui.OtherFeeName = OtherFeeName;
                ui.OtherFeeValue = OtherFeeValue;
                ui.PostalDeliveryType = PostalDeliveryType;
                if (DateDone != "")
                {
                    ui.DateDone = Convert.ToDateTime(DateDone);
                }

                dbe.tbl_Order.Add(ui);
                dbe.SaveChanges();
                //int kq = ui.ID;
                return ui;
            }
        }
        public static tbl_Order InsertOnSystem(int AgentID, int OrderType, string AdditionFee, string DisCount, int CustomerID, string CustomerName, string CustomerPhone,
           string CustomerAddress, string CustomerEmail, string TotalPrice, string TotalPriceNotDiscount, int PaymentStatus, int ExcuteStatus, bool IsHidden, int WayIn,
           DateTime CreatedDate, string CreatedBy, double DiscountPerProduct, double TotalDiscount, string FeeShipping, double GuestPaid, double GuestChange, int PaymentType, int ShippingType, string OrderNote, DateTime DateDone, string OtherFeeName, double OtherFeeValue, int PostalDeliveryType)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Order ui = new tbl_Order();
                ui.AgentID = AgentID;
                ui.OrderType = OrderType;
                ui.AdditionFee = AdditionFee;
                ui.DisCount = DisCount;
                ui.CustomerID = CustomerID;
                ui.CustomerName = CustomerName;
                ui.CustomerPhone = CustomerPhone;
                ui.CustomerAddress = CustomerAddress;
                ui.CustomerEmail = CustomerEmail;
                ui.TotalPrice = TotalPrice;
                ui.TotalPriceNotDiscount = TotalPriceNotDiscount;
                ui.PaymentStatus = PaymentStatus;
                ui.ExcuteStatus = ExcuteStatus;
                ui.IsHidden = IsHidden;
                ui.WayIn = WayIn;
                ui.IsHidden = IsHidden;
                ui.DiscountPerProduct = DiscountPerProduct;
                ui.TotalDiscount = TotalDiscount;
                ui.FeeShipping = FeeShipping;
                ui.CreatedDate = CreatedDate;
                ui.CreatedBy = CreatedBy;
                ui.GuestPaid = GuestPaid;
                ui.GuestChange = GuestChange;
                ui.PaymentType = PaymentType;
                ui.ShippingType = ShippingType;
                ui.OrderNote = OrderNote;
                ui.DateDone = DateDone;
                ui.OtherFeeName = OtherFeeName;
                ui.OtherFeeValue = OtherFeeValue;
                ui.PostalDeliveryType = PostalDeliveryType;
                dbe.tbl_Order.Add(ui);
                dbe.SaveChanges();

                return ui;
            }
        }

        public static int UpdateRefund(int ID, int? RefundsGoodsID, string created)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                var ui = dbe.tbl_Order.Where(x => x.ID == ID).FirstOrDefault();
                if (ui != null)
                {
                    ui.RefundsGoodsID = RefundsGoodsID;
                    ui.ModifiedDate = DateTime.Now;
                    ui.ModifiedBy = created;
                    int i = dbe.SaveChanges();
                    return i;
                }
                return 0;
            }
        }
        public static int DeleteOrderRefund(int RefundsGoodsID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                var ui = dbe.tbl_Order.Where(x => x.RefundsGoodsID == RefundsGoodsID).FirstOrDefault();
                if (ui != null)
                {
                    ui.RefundsGoodsID = null;
                    ui.ModifiedDate = DateTime.Now;
                    int i = dbe.SaveChanges();
                    return ui.ID;
                }
                return 0;
            }
        }
        public static string Update(int ID, int OrderType, string AdditionFee, string DisCount, int CustomerID, string CustomerName,
            string CustomerPhone, string CustomerAddress, string CustomerEmail, string TotalPrice, string TotalPriceNotDiscount, int PaymentStatus,
            int ExcuteStatus, DateTime ModifiedDate, string ModifiedBy, double DiscountPerProduct, double TotalDiscount,
            string FeeShipping)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                var ui = dbe.tbl_Order.Where(o => o.ID == ID).FirstOrDefault();
                if (ui != null)
                {
                    ui.OrderType = OrderType;
                    ui.AdditionFee = AdditionFee;
                    ui.DisCount = DisCount;
                    ui.CustomerID = CustomerID;
                    ui.CustomerName = CustomerName;
                    ui.CustomerPhone = CustomerPhone;
                    ui.CustomerAddress = CustomerAddress;
                    ui.CustomerEmail = CustomerEmail;
                    ui.TotalPrice = TotalPrice;
                    ui.TotalPriceNotDiscount = TotalPriceNotDiscount;
                    ui.PaymentStatus = PaymentStatus;
                    ui.ExcuteStatus = ExcuteStatus;
                    ui.DiscountPerProduct = DiscountPerProduct;
                    ui.TotalDiscount = TotalDiscount;
                    ui.FeeShipping = FeeShipping;
                    ui.ModifiedDate = ModifiedDate;
                    ui.ModifiedBy = ModifiedBy;

                    dbe.SaveChanges();
                    int kq = ui.ID;
                    return kq.ToString();
                }
                return null;
            }
        }
        public static string UpdateCustomerPhone(int ID, string CustomerPhone)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                var ui = dbe.tbl_Order.Where(o => o.ID == ID).FirstOrDefault();
                if (ui != null)
                {
                    ui.OrderNote = ui.OrderNote + ". Số điện thoại cũ của khách hàng này là " + ui.CustomerPhone;
                    ui.CustomerPhone = CustomerPhone;
                    dbe.SaveChanges();
                    int kq = ui.ID;
                    return kq.ToString();
                }
                return null;
            }
        }
        public static string UpdateOnSystem(int ID, int OrderType, string AdditionFee, string DisCount, int CustomerID, string CustomerName,
            string CustomerPhone, string CustomerAddress, string CustomerEmail, string TotalPrice, string TotalPriceNotDiscount, int PaymentStatus,
            int ExcuteStatus, DateTime ModifiedDate, string ModifiedBy, double DiscountPerProduct, double TotalDiscount,
            string FeeShipping, double GuestPaid, double GuestChange, int PaymentType, int ShippingType, string OrderNote, string DateDone, int RefundsGoodsID = 0, string ShippingCode = null, int TransportCompanyID = 0, int TransportCompanySubID = 0, string OtherFeeName = "", double OtherFeeValue = 0, int PostalDeliveryType = 1)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                var ui = dbe.tbl_Order.Where(o => o.ID == ID).FirstOrDefault();
                if (ui != null)
                {
                    ui.OrderType = OrderType;
                    ui.AdditionFee = AdditionFee;
                    ui.DisCount = DisCount;
                    ui.CustomerID = CustomerID;
                    ui.CustomerName = CustomerName;
                    ui.CustomerPhone = CustomerPhone;
                    ui.CustomerAddress = CustomerAddress;
                    ui.CustomerEmail = CustomerEmail;
                    ui.TotalPrice = TotalPrice;
                    ui.TotalPriceNotDiscount = TotalPriceNotDiscount;
                    ui.PaymentStatus = PaymentStatus;
                    ui.ExcuteStatus = ExcuteStatus;
                    ui.DiscountPerProduct = DiscountPerProduct;
                    ui.TotalDiscount = TotalDiscount;
                    ui.FeeShipping = FeeShipping;
                    ui.GuestPaid = GuestPaid;
                    ui.GuestChange = GuestChange;
                    ui.ModifiedDate = ModifiedDate;
                    ui.ModifiedBy = ModifiedBy;
                    ui.PaymentType = PaymentType;
                    ui.ShippingType = ShippingType;
                    ui.OrderNote = OrderNote;
                    ui.DateDone = null;
                    if (DateDone != "")
                    {
                        ui.DateDone = Convert.ToDateTime(DateDone);
                    }
                    ui.ShippingCode = ShippingCode;
                    ui.TransportCompanyID = TransportCompanyID;
                    ui.TransportCompanySubID = TransportCompanySubID;
                    ui.OtherFeeName = OtherFeeName;
                    ui.OtherFeeValue = OtherFeeValue;
                    ui.PostalDeliveryType = PostalDeliveryType;
                    dbe.SaveChanges();
                    int kq = ui.ID;
                    return kq.ToString();
                }
                return null;
            }
        }

        public static int UpdateExcuteStatus(int ID, string CreatedBy)
        {
            using (var db = new inventorymanagementEntities())
            {
                var ui = db.tbl_Order.Where(x => x.ID == ID).FirstOrDefault();
                if (ui != null)
                {
                    ui.ExcuteStatus = 3;
                    ui.TotalPrice = "0";
                    ui.TotalDiscount = 0;
                    ui.TotalPriceNotDiscount = "0";
                    ui.GuestChange = 0;
                    ui.ModifiedDate = DateTime.Now;
                    ui.ModifiedBy = CreatedBy;
                    int i = db.SaveChanges();
                    return i;
                }
                return 0;
            }
        }

        public static bool UpdateExcuteStatus4(int ID, string CreatedBy, string OrderNote)
        {
            using (var db = new inventorymanagementEntities())
            {
                var ui = db.tbl_Order.Where(x => x.ID == ID).FirstOrDefault();
                if (ui != null)
                {
                    if (ui.ExcuteStatus != 4)
                    {
                        ui.ModifiedDate = DateTime.Now;
                    }
                    ui.ModifiedBy = CreatedBy;
                    ui.PaymentStatus = 1;
                    ui.ExcuteStatus = 4;
                    ui.OrderNote = OrderNote;
                    int i = db.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        #endregion
        #region Select
        public static tbl_Order GetByID(int ID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Order ai = dbe.tbl_Order.Where(a => a.ID == ID).SingleOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }
        public static List<tbl_Order> GetAll()
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_Order> ags = new List<tbl_Order>();
                ags = dbe.tbl_Order.OrderByDescending(o => o.ID).ToList();
                return ags;
            }
        }
        public static List<tbl_Order> GetAllByAgentID(int AgentID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_Order> ags = new List<tbl_Order>();
                ags = dbe.tbl_Order.Where(o => o.AgentID == AgentID).OrderByDescending(o => o.ID).ToList();
                return ags;
            }
        }
        public static List<tbl_Order> GetAllByOrderType(int OrderType)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_Order> ags = new List<tbl_Order>();
                ags = dbe.tbl_Order.Where(o => o.OrderType == OrderType).OrderByDescending(o => o.ID).ToList();
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
        public static List<tbl_Order> GetByExcuteStatus(int ExcuteStatus)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_Order> ags = new List<tbl_Order>();
                ags = dbe.tbl_Order.Where(o => o.ExcuteStatus == ExcuteStatus).OrderByDescending(o => o.ID).ToList();
                return ags;
            }
        }
        public static List<tbl_Order> GetByCustomerIDFromDateToDate(int AgentID, int CustomerID, DateTime FromDate, DateTime ToDate)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_Order> ags = new List<tbl_Order>();
                ags = dbe.tbl_Order.Where(o => o.AgentID == AgentID && o.CustomerID == CustomerID && o.CreatedDate >= FromDate && o.CreatedDate < ToDate)
                    .OrderByDescending(o => o.ID).ToList();
                return ags;
            }
        }

        public static List<OrderList> Filter(string TextSearch, int OrderType, int ExcuteStatus, int PaymentStatus, int TransferStatus, int PaymentType, int ShippingType, string Discount, string OtherFee, string CreatedBy, string CreatedDate, string TransferDoneAt, int TransportCompany)
        {
            var list = new List<OrderList>();
            var sql = new StringBuilder();

            sql.AppendLine("SELECT");
            sql.AppendLine("    Ord.ID");
            sql.AppendLine(",   Ord.CustomerName");
            sql.AppendLine(",   Ord.CustomerPhone");
            sql.AppendLine(",   Customer.Nick");
            sql.AppendLine(",   Ord.CustomerID");
            sql.AppendLine(",   Ord.OrderType");
            sql.AppendLine(",   Ord.ExcuteStatus");
            sql.AppendLine(",   Ord.PaymentStatus");
            sql.AppendLine(",   Ord.PaymentType");
            sql.AppendLine(",   Ord.ShippingType");
            sql.AppendLine(",   Ord.TotalPrice");
            sql.AppendLine(",   Ord.TotalDiscount");
            sql.AppendLine(",   Ord.FeeShipping");
            sql.AppendLine(",   Ord.OtherFeeName");
            sql.AppendLine(",   Ord.OtherFeeValue");
            sql.AppendLine(",   Ord.CreatedBy");
            sql.AppendLine(",   Ord.CreatedDate");
            sql.AppendLine(",   Ord.DateDone");
            sql.AppendLine(",   Ord.OrderNote");
            sql.AppendLine(",   Ord.RefundsGoodsID");
            sql.AppendLine(",   SUM(ISNULL(OrdDetail.Quantity,0)) AS Quantity");
            sql.AppendLine(",   Ord.ShippingCode");
            sql.AppendLine(",   Ord.TransportCompanyID");
            sql.AppendLine(",   Ord.TransportCompanySubID");
            sql.AppendLine(",   Ord.OrderNote");
            sql.AppendLine(",   Ord.PostalDeliveryType");
            sql.AppendLine(",   Transfer.CusBankID");
            sql.AppendLine(",   CusBank.BankName AS CusBankName");
            sql.AppendLine(",   Transfer.AccBankID");
            sql.AppendLine(",   AccBank.BankName AS AccBankName");
            sql.AppendLine(",   ISNULL(Transfer.Money, 0) AS MoneyReceive");
            sql.AppendLine(",   ISNULL(Transfer.Status, 2) AS TransferStatus");
            sql.AppendLine(",   (CASE ISNULL(Transfer.Status, 2) WHEN 1 THEN N'Đã nhận tiền' ELSE N'Chưa nhận tiền' END) AS StatusName");
            sql.AppendLine(",   Transfer.DoneAt");
            sql.AppendLine(",   Transfer.Note AS TransferNote");
            sql.AppendLine(",   Delivery.StartAt AS DeliveryDate");
            sql.AppendLine(",   ISNULL(Delivery.Status, 2) AS DeliveryStatus"); // Case 2: Chưa giao
            sql.AppendLine(",   Delivery.ShipperID");
            sql.AppendLine(",   Delivery.COD AS CostOfDelivery");
            sql.AppendLine(",   Delivery.COO AS CollectionOfOrder");
            sql.AppendLine(",   Delivery.ShipNote");
            sql.AppendLine(",   Delivery.Image AS InvoiceImage");
            sql.AppendLine(",   Delivery.ShipperName");
            sql.AppendLine("FROM tbl_Order AS Ord");
            sql.AppendLine("INNER JOIN tbl_OrderDetail AS OrdDetail");
            sql.AppendLine("ON    Ord.ID = OrdDetail.OrderID");
            sql.AppendLine("INNER JOIN tbl_Customer AS Customer");
            sql.AppendLine("ON    Ord.CustomerID = Customer.ID");
            sql.AppendLine("LEFT JOIN BankTransfer AS Transfer");
            sql.AppendLine("ON    Ord.ID = Transfer.OrderID");
            sql.AppendLine("LEFT JOIN Bank AS CusBank");
            sql.AppendLine("ON    Transfer.CusBankID = CusBank.ID");
            sql.AppendLine("LEFT JOIN BankAccount AS AccBank");
            sql.AppendLine("ON    Transfer.AccBankID = AccBank.ID");
            sql.AppendLine("LEFT JOIN (");
            sql.AppendLine("    SELECT");
            sql.AppendLine("        DEL.OrderID");
            sql.AppendLine("    ,   DEL.StartAt");
            sql.AppendLine("    ,   DEL.Status");
            sql.AppendLine("    ,   DEL.ShipperID");
            sql.AppendLine("    ,   DEL.COD");
            sql.AppendLine("    ,   DEL.COO");
            sql.AppendLine("    ,   DEL.ShipNote");
            sql.AppendLine("    ,   DEL.Image");
            sql.AppendLine("    ,   SHI.Name AS ShipperName");
            sql.AppendLine("    FROM Delivery AS DEL");
            sql.AppendLine("    INNER JOIN Shipper AS SHI");
            sql.AppendLine("    ON DEL.ShipperID = SHI.ID");
            sql.AppendLine(") AS Delivery");
            sql.AppendLine("ON    Ord.ID = Delivery.OrderID");
            sql.AppendLine("WHERE 1 = 1");

            if (ExcuteStatus > 0)
            {
                sql.AppendLine(String.Format("	AND Ord.ExcuteStatus = {0}", ExcuteStatus));
            }

            if (TextSearch != "")
            {
                string TextSearchName = '"' + TextSearch + '"';
                sql.AppendLine(String.Format("	AND ( (convert(nvarchar, Ord.ID) LIKE '{0}') OR CONTAINS(Ord.CustomerName, '{1}') OR CONTAINS(Customer.Nick, '{1}') OR (Ord.CustomerPhone = '{0}') OR (Ord.CustomerNewPhone = '{0}') OR (Ord.ShippingCode = '{0}') OR (OrdDetail.SKU LIKE '{0}%') OR (Ord.OrderNote LIKE '%{0}%'))", TextSearch, TextSearchName));
            }

            if (TransportCompany > 0)
            {
                sql.AppendLine(String.Format("	AND Ord.TransportCompanyID = {0}", TransportCompany));
            }

            if (OrderType > 0)
            {
                sql.AppendLine(String.Format("	AND Ord.OrderType = {0}", OrderType));
            }

            if (PaymentStatus > 0)
            {
                sql.AppendLine(String.Format("	AND Ord.PaymentStatus = {0}", PaymentStatus));
            }

            if (TransferStatus > 0)
            {
                if(TransferStatus == 1)
                {
                    sql.AppendLine(String.Format("	AND Transfer.Status = 1"));
                }
                else if(TransferStatus == 2)
                {
                    sql.AppendLine(String.Format("	AND Transfer.Status IS NULL"));
                }
            }

            if (PaymentType > 0)
            {
                sql.AppendLine(String.Format("	AND Ord.PaymentType = {0}", PaymentType));
            }

            if (ShippingType > 0)
            {
                sql.AppendLine(String.Format("	AND Ord.ShippingType = {0}", ShippingType));
            }

            if (OtherFee != "")
            {
                if (OtherFee == "yes")
                {
                    sql.AppendLine(String.Format("	AND Ord.OtherFeeValue != 0", OtherFee));
                }
                else
                {
                    sql.AppendLine(String.Format("	AND Ord.OtherFeeValue = 0", OtherFee));
                }
            }

            if (Discount != "")
            {
                if (Discount == "yes")
                {
                    sql.AppendLine(String.Format("	AND Ord.TotalDiscount > 0", OtherFee));
                }
                else
                {
                    sql.AppendLine(String.Format("	AND Ord.TotalDiscount = 0", OtherFee));
                }
            }

            if (TransferDoneAt != "")
            {
                DateTime fromdate = DateTime.Today;
                DateTime todate = DateTime.Now;
                switch (TransferDoneAt)
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
                    case "beforelastmonth":
                        thismonth = new DateTime(fromdate.Year, fromdate.Month, 1);
                        fromdate = thismonth.AddMonths(-2);
                        todate = thismonth.AddMonths(-1);
                        break;
                    case "7days":
                        fromdate = fromdate.AddDays(-6);
                        todate = DateTime.Now;
                        break;
                    case "30days":
                        fromdate = fromdate.AddDays(-29);
                        todate = DateTime.Now;
                        break;
                }
                sql.AppendLine(String.Format("	AND	CONVERT(datetime, Transfer.DoneAt, 121) BETWEEN CONVERT(datetime, '{0}', 121) AND CONVERT(datetime, '{1}', 121)", fromdate.ToString(), todate.ToString()));
            }

            if (CreatedBy != "")
            {
                sql.AppendLine(String.Format("	AND Ord.CreatedBy = '{0}'", CreatedBy));
            }

            if (CreatedDate != "")
            {
                string column = "CreatedDate";
                if (ExcuteStatus == 2)
                {
                    column = "DateDone";
                }
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
                    case "beforelastmonth":
                        thismonth = new DateTime(fromdate.Year, fromdate.Month, 1);
                        fromdate = thismonth.AddMonths(-2);
                        todate = thismonth.AddMonths(-1);
                        break;
                    case "7days":
                        fromdate = fromdate.AddDays(-6);
                        todate = DateTime.Now;
                        break;
                    case "30days":
                        fromdate = fromdate.AddDays(-29);
                        todate = DateTime.Now;
                        break;
                }
                sql.AppendLine(String.Format("	AND	CONVERT(datetime, Ord." + column + ", 121) BETWEEN CONVERT(datetime, '{0}', 121) AND CONVERT(datetime, '{1}', 121)", fromdate.ToString(), todate.ToString()));
            }

            sql.AppendLine("GROUP BY");
            sql.AppendLine("     Ord.ID");
            sql.AppendLine(",    Ord.CustomerName");
            sql.AppendLine(",    Ord.CustomerPhone");
            sql.AppendLine(",    Customer.Nick");
            sql.AppendLine(",    Ord.CustomerID");
            sql.AppendLine(",    Ord.OrderType");
            sql.AppendLine(",    Ord.ExcuteStatus");
            sql.AppendLine(",    Ord.PaymentStatus");
            sql.AppendLine(",    Ord.PaymentType");
            sql.AppendLine(",    Ord.ShippingType");
            sql.AppendLine(",    Ord.TotalPrice");
            sql.AppendLine(",    Ord.TotalDiscount");
            sql.AppendLine(",    Ord.FeeShipping");
            sql.AppendLine(",    Ord.OtherFeeName");
            sql.AppendLine(",    Ord.OtherFeeValue");
            sql.AppendLine(",    Ord.CreatedBy");
            sql.AppendLine(",    Ord.CreatedDate");
            sql.AppendLine(",    Ord.DateDone");
            sql.AppendLine(",    Ord.OrderNote");
            sql.AppendLine(",    Ord.RefundsGoodsID");
            sql.AppendLine(",    Ord.ShippingCode");
            sql.AppendLine(",    Ord.TransportCompanyID");
            sql.AppendLine(",    Ord.TransportCompanySubID");
            sql.AppendLine(",    Ord.OrderNote");
            sql.AppendLine(",    Ord.PostalDeliveryType");
            sql.AppendLine(",    Transfer.CusBankID");
            sql.AppendLine(",    CusBank.BankName");
            sql.AppendLine(",    Transfer.AccBankID");
            sql.AppendLine(",    AccBank.BankName");
            sql.AppendLine(",    Transfer.Money");
            sql.AppendLine(",    Transfer.Status");
            sql.AppendLine(",    Transfer.DoneAt");
            sql.AppendLine(",    Transfer.Note");
            sql.AppendLine(",    Delivery.StartAt");
            sql.AppendLine(",    Delivery.Status");
            sql.AppendLine(",    Delivery.ShipperID");
            sql.AppendLine(",    Delivery.COD");
            sql.AppendLine(",    Delivery.COO");
            sql.AppendLine(",    Delivery.ShipNote");
            sql.AppendLine(",    Delivery.Image");
            sql.AppendLine(",    Delivery.ShipperName");
            sql.AppendLine("ORDER BY Ord.ID DESC");

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql.ToString());
            while (reader.Read())
            {
                var entity = new OrderList();

                entity.ID = Convert.ToInt32(reader["ID"]);
                entity.CustomerName = reader["CustomerName"].ToString();
                entity.CustomerPhone = reader["CustomerPhone"].ToString();
                entity.CustomerID = Convert.ToInt32(reader["CustomerID"]);
                entity.Nick = reader["Nick"].ToString();
                entity.OrderType = Convert.ToInt32(reader["OrderType"]);
                entity.ExcuteStatus = Convert.ToInt32(reader["ExcuteStatus"]);
                entity.PaymentStatus = Convert.ToInt32(reader["PaymentStatus"]);
                entity.PaymentType = Convert.ToInt32(reader["PaymentType"]);
                entity.ShippingType = Convert.ToInt32(reader["ShippingType"]);
                entity.FeeShipping = Convert.ToInt32(reader["FeeShipping"]);
                entity.OtherFeeName = reader["OtherFeeName"].ToString();
                if (reader["OtherFeeValue"] != DBNull.Value)
                    entity.OtherFeeValue = Convert.ToInt32(reader["OtherFeeValue"]);
                else
                    entity.OtherFeeValue = 0;
                entity.TotalPrice = Convert.ToInt32(reader["TotalPrice"]);
                entity.TotalDiscount = Convert.ToInt32(reader["TotalDiscount"]);
                entity.CreatedBy = reader["CreatedBy"].ToString();
                entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                if (reader["DateDone"] != DBNull.Value)
                    entity.DateDone = Convert.ToDateTime(reader["DateDone"]);
                if (reader["RefundsGoodsID"] != DBNull.Value)
                    entity.RefundsGoodsID = Convert.ToInt32(reader["RefundsGoodsID"]);
                entity.Quantity = Convert.ToInt32(reader["Quantity"]);
                entity.ShippingCode = reader["ShippingCode"].ToString();
                if (reader["TransportCompanyID"] != DBNull.Value)
                    entity.TransportCompanyID = Convert.ToInt32(reader["TransportCompanyID"]);
                if (reader["TransportCompanySubID"] != DBNull.Value)
                    entity.TransportCompanySubID = Convert.ToInt32(reader["TransportCompanySubID"]);
                entity.OrderNote = reader["OrderNote"].ToString();
                if (reader["PostalDeliveryType"] != DBNull.Value)
                    entity.PostalDeliveryType = Convert.ToInt32(reader["PostalDeliveryType"]);

                // Custom Transfer Bank
                if (reader["CusBankID"] != DBNull.Value)
                    entity.CusBankID = Convert.ToInt32(reader["CusBankID"]);
                if (reader["CusBankName"] != DBNull.Value)
                    entity.CusBankName = reader["CusBankName"].ToString();
                if (reader["AccBankID"] != DBNull.Value)
                    entity.AccBankID = Convert.ToInt32(reader["AccBankID"]);
                if (reader["AccBankName"] != DBNull.Value)
                    entity.AccBankName = reader["AccBankName"].ToString();
                if (reader["MoneyReceive"] != DBNull.Value)
                    entity.MoneyReceive = Convert.ToDecimal(reader["MoneyReceive"]);
                if (reader["TransferStatus"] != DBNull.Value)
                    entity.TransferStatus = Convert.ToInt32(reader["TransferStatus"]);
                if (reader["StatusName"] != DBNull.Value)
                    entity.StatusName = reader["StatusName"].ToString();
                if (reader["DoneAt"] != DBNull.Value)
                    entity.DoneAt = Convert.ToDateTime(reader["DoneAt"]);
                if (reader["TransferNote"] != DBNull.Value)
                    entity.TransferNote = reader["TransferNote"].ToString();

                // Custom Delivery
                if (reader["DeliveryDate"] != DBNull.Value)
                    entity.DeliveryDate = Convert.ToDateTime(reader["DeliveryDate"]);
                if (reader["DeliveryStatus"] != DBNull.Value)
                    entity.DeliveryStatus = Convert.ToInt32(reader["DeliveryStatus"]);
                if (reader["ShipperID"] != DBNull.Value)
                    entity.ShipperID = Convert.ToInt32(reader["ShipperID"]);
                if (reader["CostOfDelivery"] != DBNull.Value)
                    entity.CostOfDelivery = Convert.ToDecimal(reader["CostOfDelivery"]);
                if (reader["CollectionOfOrder"] != DBNull.Value)
                    entity.CollectionOfOrder = Convert.ToDecimal(reader["CollectionOfOrder"]);
                if (reader["ShipNote"] != DBNull.Value)
                    entity.ShipNote = reader["ShipNote"].ToString();
                if (reader["ShipperName"] != DBNull.Value)
                    entity.ShipperName = reader["ShipperName"].ToString();
                if (reader["InvoiceImage"] != DBNull.Value)
                    entity.InvoiceImage = reader["InvoiceImage"].ToString();
                list.Add(entity);
            }
            reader.Close();
            return list;
        }

        public static List<tbl_Order> SearchByStatical(int orderType, int PaymentStatus, int ExcuteStatus, string s, int agentID, int PaymentType, int ShippingType, string sku)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_Order> rs = new List<tbl_Order>();
                List<tbl_Order> end = new List<tbl_Order>();
                if (!string.IsNullOrEmpty(s))
                {
                    if (orderType > 0)
                    {
                        if (PaymentStatus > 0)
                        {
                            if (ExcuteStatus > 0)
                            {
                                if (PaymentType > 0)
                                {
                                    if (ShippingType > 0)
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.OrderType == orderType && r.PaymentStatus == PaymentStatus && r.ExcuteStatus == ExcuteStatus && r.PaymentType == PaymentType && r.ShippingType == ShippingType && r.ID.ToString().Contains(s)
                                    || r.OrderType == orderType && r.PaymentStatus == PaymentStatus && r.ExcuteStatus == ExcuteStatus && r.PaymentType == PaymentType && r.ShippingType == ShippingType && r.CustomerName.Contains(s)
                                    || r.OrderType == orderType && r.PaymentStatus == PaymentStatus && r.ExcuteStatus == ExcuteStatus && r.PaymentType == PaymentType && r.ShippingType == ShippingType && r.CustomerPhone.Contains(s))
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                    else
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.OrderType == orderType && r.PaymentStatus == PaymentStatus && r.ExcuteStatus == ExcuteStatus && r.PaymentType == PaymentType && r.ID.ToString().Contains(s)
                                    || r.OrderType == orderType && r.PaymentStatus == PaymentStatus && r.ExcuteStatus == ExcuteStatus && r.PaymentType == PaymentType && r.CustomerName.Contains(s)
                                    || r.OrderType == orderType && r.PaymentStatus == PaymentStatus && r.ExcuteStatus == ExcuteStatus && r.PaymentType == PaymentType && r.CustomerPhone.Contains(s))
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                }
                                else
                                {
                                    if (ShippingType > 0)
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.OrderType == orderType && r.PaymentStatus == PaymentStatus && r.ExcuteStatus == ExcuteStatus && r.ShippingType == ShippingType && r.ID.ToString().Contains(s)
                                    || r.OrderType == orderType && r.PaymentStatus == PaymentStatus && r.ExcuteStatus == ExcuteStatus && r.ShippingType == ShippingType && r.CustomerName.Contains(s)
                                    || r.OrderType == orderType && r.PaymentStatus == PaymentStatus && r.ExcuteStatus == ExcuteStatus && r.ShippingType == ShippingType && r.CustomerPhone.Contains(s))
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                    else
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.OrderType == orderType && r.PaymentStatus == PaymentStatus && r.ExcuteStatus == ExcuteStatus && r.ID.ToString().Contains(s)
                                    || r.OrderType == orderType && r.PaymentStatus == PaymentStatus && r.ExcuteStatus == ExcuteStatus && r.CustomerName.Contains(s)
                                    || r.OrderType == orderType && r.PaymentStatus == PaymentStatus && r.ExcuteStatus == ExcuteStatus && r.CustomerPhone.Contains(s))
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                }
                            }
                            else
                            {
                                if (PaymentType > 0)
                                {
                                    if (ShippingType > 0)
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.OrderType == orderType && r.PaymentStatus == PaymentStatus && r.PaymentType == PaymentType && r.ShippingType == ShippingType && r.ID.ToString().Contains(s)
                                    || r.OrderType == orderType && r.PaymentStatus == PaymentStatus && r.PaymentType == PaymentType && r.ShippingType == ShippingType && r.CustomerName.Contains(s)
                                    || r.OrderType == orderType && r.PaymentStatus == PaymentStatus && r.PaymentType == PaymentType && r.ShippingType == ShippingType && r.CustomerPhone.Contains(s))
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                    else
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.OrderType == orderType && r.PaymentStatus == PaymentStatus && r.PaymentType == PaymentType && r.ID.ToString().Contains(s)
                                    || r.OrderType == orderType && r.PaymentStatus == PaymentStatus && r.PaymentType == PaymentType && r.CustomerName.Contains(s)
                                    || r.OrderType == orderType && r.PaymentStatus == PaymentStatus && r.PaymentType == PaymentType && r.CustomerPhone.Contains(s))
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                }
                                else
                                {
                                    if (ShippingType > 0)
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.OrderType == orderType && r.PaymentStatus == PaymentStatus && r.ShippingType == ShippingType && r.ID.ToString().Contains(s)
                                    || r.OrderType == orderType && r.PaymentStatus == PaymentStatus && r.ShippingType == ShippingType && r.CustomerName.Contains(s)
                                    || r.OrderType == orderType && r.PaymentStatus == PaymentStatus && r.ShippingType == ShippingType && r.CustomerPhone.Contains(s))
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                    else
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.OrderType == orderType && r.PaymentStatus == PaymentStatus && r.ID.ToString().Contains(s)
                                    || r.OrderType == orderType && r.PaymentStatus == PaymentStatus && r.CustomerName.Contains(s)
                                    || r.OrderType == orderType && r.PaymentStatus == PaymentStatus && r.CustomerPhone.Contains(s))
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (ExcuteStatus > 0)
                            {
                                if (PaymentType > 0)
                                {
                                    if (ShippingType > 0)
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.OrderType == orderType && r.ExcuteStatus == ExcuteStatus && r.PaymentType == PaymentType && r.ShippingType == ShippingType && r.ID.ToString().Contains(s)
                                    || r.OrderType == orderType && r.ExcuteStatus == ExcuteStatus && r.PaymentType == PaymentType && r.ShippingType == ShippingType && r.CustomerName.Contains(s)
                                    || r.OrderType == orderType && r.ExcuteStatus == ExcuteStatus && r.PaymentType == PaymentType && r.ShippingType == ShippingType && r.CustomerPhone.Contains(s))
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                    else
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.OrderType == orderType && r.ExcuteStatus == ExcuteStatus && r.PaymentType == PaymentType && r.ID.ToString().Contains(s)
                                    || r.OrderType == orderType && r.ExcuteStatus == ExcuteStatus && r.PaymentType == PaymentType && r.CustomerName.Contains(s)
                                    || r.OrderType == orderType && r.ExcuteStatus == ExcuteStatus && r.PaymentType == PaymentType && r.CustomerPhone.Contains(s))
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                }
                                else
                                {
                                    if (ShippingType > 0)
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.OrderType == orderType && r.ExcuteStatus == ExcuteStatus && r.ShippingType == ShippingType && r.ID.ToString().Contains(s)
                                    || r.OrderType == orderType && r.ExcuteStatus == ExcuteStatus && r.ShippingType == ShippingType && r.CustomerName.Contains(s)
                                    || r.OrderType == orderType && r.ExcuteStatus == ExcuteStatus && r.ShippingType == ShippingType && r.CustomerPhone.Contains(s))
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                    else
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.OrderType == orderType && r.ExcuteStatus == ExcuteStatus && r.ID.ToString().Contains(s)
                                    || r.OrderType == orderType && r.ExcuteStatus == ExcuteStatus && r.CustomerName.Contains(s)
                                    || r.OrderType == orderType && r.ExcuteStatus == ExcuteStatus && r.CustomerPhone.Contains(s))
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                }
                            }
                            else
                            {
                                if (PaymentType > 0)
                                {
                                    if (ShippingType > 0)
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.OrderType == orderType && r.PaymentType == PaymentType && r.ShippingType == ShippingType && r.ID.ToString().Contains(s)
                                    || r.OrderType == orderType && r.PaymentType == PaymentType && r.ShippingType == ShippingType && r.CustomerName.Contains(s)
                                    || r.OrderType == orderType && r.PaymentType == PaymentType && r.ShippingType == ShippingType && r.CustomerPhone.Contains(s))
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                    else
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.OrderType == orderType && r.PaymentType == PaymentType && r.ID.ToString().Contains(s)
                                    || r.OrderType == orderType && r.PaymentType == PaymentType && r.CustomerName.Contains(s)
                                    || r.OrderType == orderType && r.PaymentType == PaymentType && r.CustomerPhone.Contains(s))
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                }
                                else
                                {
                                    if (ShippingType > 0)
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.OrderType == orderType && r.ShippingType == ShippingType && r.ID.ToString().Contains(s)
                                    || r.OrderType == orderType && r.ShippingType == ShippingType && r.CustomerName.Contains(s)
                                    || r.OrderType == orderType && r.ShippingType == ShippingType && r.CustomerPhone.Contains(s))
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                    else
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.OrderType == orderType && r.ID.ToString().Contains(s)
                                    || r.OrderType == orderType && r.CustomerName.Contains(s)
                                    || r.OrderType == orderType && r.CustomerPhone.Contains(s))
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (PaymentStatus > 0)
                        {
                            if (ExcuteStatus > 0)
                            {
                                if (PaymentType > 0)
                                {
                                    if (ShippingType > 0)
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.PaymentStatus == PaymentStatus && r.ExcuteStatus == ExcuteStatus && r.PaymentType == PaymentType && r.ShippingType == ShippingType && r.ID.ToString().Contains(s)
                                    || r.PaymentStatus == PaymentStatus && r.ExcuteStatus == ExcuteStatus && r.PaymentType == PaymentType && r.ShippingType == ShippingType && r.CustomerName.Contains(s)
                                    || r.PaymentStatus == PaymentStatus && r.ExcuteStatus == ExcuteStatus && r.PaymentType == PaymentType && r.ShippingType == ShippingType && r.CustomerPhone.Contains(s))
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                    else
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.PaymentStatus == PaymentStatus && r.ExcuteStatus == ExcuteStatus && r.PaymentType == PaymentType && r.ID.ToString().Contains(s)
                                    || r.PaymentStatus == PaymentStatus && r.ExcuteStatus == ExcuteStatus && r.PaymentType == PaymentType && r.CustomerName.Contains(s)
                                    || r.PaymentStatus == PaymentStatus && r.ExcuteStatus == ExcuteStatus && r.PaymentType == PaymentType && r.CustomerPhone.Contains(s))
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                }
                                else
                                {
                                    if (ShippingType > 0)
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.PaymentStatus == PaymentStatus && r.ExcuteStatus == ExcuteStatus && r.ShippingType == ShippingType && r.ID.ToString().Contains(s)
                                    || r.PaymentStatus == PaymentStatus && r.ExcuteStatus == ExcuteStatus && r.ShippingType == ShippingType && r.CustomerName.Contains(s)
                                    || r.PaymentStatus == PaymentStatus && r.ExcuteStatus == ExcuteStatus && r.ShippingType == ShippingType && r.CustomerPhone.Contains(s))
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                    else
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.PaymentStatus == PaymentStatus && r.ExcuteStatus == ExcuteStatus && r.ID.ToString().Contains(s)
                                    || r.PaymentStatus == PaymentStatus && r.ExcuteStatus == ExcuteStatus && r.CustomerName.Contains(s)
                                    || r.PaymentStatus == PaymentStatus && r.ExcuteStatus == ExcuteStatus && r.CustomerPhone.Contains(s))
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                }
                            }
                            else
                            {
                                if (PaymentType > 0)
                                {
                                    if (ShippingType > 0)
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.PaymentStatus == PaymentStatus && r.PaymentType == PaymentType && r.ShippingType == ShippingType && r.ID.ToString().Contains(s)
                                    || r.PaymentStatus == PaymentStatus && r.PaymentType == PaymentType && r.ShippingType == ShippingType && r.CustomerName.Contains(s)
                                    || r.PaymentStatus == PaymentStatus && r.PaymentType == PaymentType && r.ShippingType == ShippingType && r.CustomerPhone.Contains(s))
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                    else
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.PaymentStatus == PaymentStatus && r.PaymentType == PaymentType && r.ID.ToString().Contains(s)
                                    || r.PaymentStatus == PaymentStatus && r.PaymentType == PaymentType && r.CustomerName.Contains(s)
                                    || r.PaymentStatus == PaymentStatus && r.PaymentType == PaymentType && r.CustomerPhone.Contains(s))
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                }
                                else
                                {
                                    if (ShippingType > 0)
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.PaymentStatus == PaymentStatus && r.ShippingType == ShippingType && r.ID.ToString().Contains(s)
                                    || r.PaymentStatus == PaymentStatus && r.ShippingType == ShippingType && r.CustomerName.Contains(s)
                                    || r.PaymentStatus == PaymentStatus && r.ShippingType == ShippingType && r.CustomerPhone.Contains(s))
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                    else
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.PaymentStatus == PaymentStatus && r.ID.ToString().Contains(s)
                                    || r.PaymentStatus == PaymentStatus && r.CustomerName.Contains(s)
                                    || r.PaymentStatus == PaymentStatus && r.CustomerPhone.Contains(s))
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (ExcuteStatus > 0)
                            {
                                if (PaymentType > 0)
                                {
                                    if (ShippingType > 0)
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.ExcuteStatus == ExcuteStatus && r.PaymentType == PaymentType && r.ShippingType == ShippingType && r.ID.ToString().Contains(s)
                                    || r.ExcuteStatus == ExcuteStatus && r.PaymentType == PaymentType && r.ShippingType == ShippingType && r.CustomerName.Contains(s)
                                    || r.ExcuteStatus == ExcuteStatus && r.PaymentType == PaymentType && r.ShippingType == ShippingType && r.CustomerPhone.Contains(s))
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                    else
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.ExcuteStatus == ExcuteStatus && r.PaymentType == PaymentType && r.ID.ToString().Contains(s)
                                    || r.ExcuteStatus == ExcuteStatus && r.PaymentType == PaymentType && r.CustomerName.Contains(s)
                                    || r.ExcuteStatus == ExcuteStatus && r.PaymentType == PaymentType && r.CustomerPhone.Contains(s))
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                }
                                else
                                {
                                    if (ShippingType > 0)
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.ExcuteStatus == ExcuteStatus && r.ShippingType == ShippingType && r.ID.ToString().Contains(s)
                                    || r.ExcuteStatus == ExcuteStatus && r.ShippingType == ShippingType && r.CustomerName.Contains(s)
                                    || r.ExcuteStatus == ExcuteStatus && r.ShippingType == ShippingType && r.CustomerPhone.Contains(s))
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                    else
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.ExcuteStatus == ExcuteStatus && r.ID.ToString().Contains(s)
                                    || r.ExcuteStatus == ExcuteStatus && r.CustomerName.Contains(s)
                                    || r.ExcuteStatus == ExcuteStatus && r.CustomerPhone.Contains(s))
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                }
                            }
                            else
                            {
                                if (PaymentType > 0)
                                {
                                    if (ShippingType > 0)
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.PaymentType == PaymentType && r.ShippingType == ShippingType && r.ID.ToString().Contains(s)
                                    || r.PaymentType == PaymentType && r.ShippingType == ShippingType && r.CustomerName.Contains(s)
                                    || r.PaymentType == PaymentType && r.ShippingType == ShippingType && r.CustomerPhone.Contains(s))
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                    else
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.PaymentType == PaymentType && r.ID.ToString().Contains(s)
                                    || r.PaymentType == PaymentType && r.CustomerName.Contains(s)
                                    || r.PaymentType == PaymentType && r.CustomerPhone.Contains(s))
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                }
                                else
                                {
                                    if (ShippingType > 0)
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.ShippingType == ShippingType && r.ID.ToString().Contains(s)
                                    || r.ShippingType == ShippingType && r.CustomerName.Contains(s)
                                    || r.ShippingType == ShippingType && r.CustomerPhone.Contains(s))
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                    else
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.ID.ToString().Contains(s)
                                    || r.CustomerName.Contains(s)
                                    || r.CustomerPhone.Contains(s))
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (orderType > 0)
                    {
                        if (PaymentStatus > 0)
                        {
                            if (ExcuteStatus > 0)
                            {
                                if (PaymentType > 0)
                                {
                                    if (ShippingType > 0)
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.OrderType == orderType && r.PaymentStatus == PaymentStatus && r.ExcuteStatus == ExcuteStatus && r.PaymentType == PaymentType && r.ShippingType == ShippingType)
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                    else
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.OrderType == orderType && r.PaymentStatus == PaymentStatus && r.ExcuteStatus == ExcuteStatus && r.PaymentType == PaymentType)
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                }
                                else
                                {
                                    if (ShippingType > 0)
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.OrderType == orderType && r.PaymentStatus == PaymentStatus && r.ExcuteStatus == ExcuteStatus && r.ShippingType == ShippingType)
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                    else
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.OrderType == orderType && r.PaymentStatus == PaymentStatus && r.ExcuteStatus == ExcuteStatus)
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                }
                            }
                            else
                            {
                                if (PaymentType > 0)
                                {
                                    if (ShippingType > 0)
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.OrderType == orderType && r.PaymentStatus == PaymentStatus && r.PaymentType == PaymentType && r.ShippingType == ShippingType)
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                    else
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.OrderType == orderType && r.PaymentStatus == PaymentStatus && r.PaymentType == PaymentType)
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                }
                                else
                                {
                                    if (ShippingType > 0)
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.OrderType == orderType && r.PaymentStatus == PaymentStatus && r.ShippingType == ShippingType)
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                    else
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.OrderType == orderType && r.PaymentStatus == PaymentStatus)
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (ExcuteStatus > 0)
                            {
                                if (PaymentType > 0)
                                {
                                    if (ShippingType > 0)
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.OrderType == orderType && r.ExcuteStatus == ExcuteStatus && r.PaymentType == PaymentType && r.ShippingType == ShippingType)
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                    else
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.OrderType == orderType && r.ExcuteStatus == ExcuteStatus && r.PaymentType == PaymentType)
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                }
                                else
                                {
                                    if (ShippingType > 0)
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.OrderType == orderType && r.ExcuteStatus == ExcuteStatus && r.ShippingType == ShippingType)
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                    else
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.OrderType == orderType && r.ExcuteStatus == ExcuteStatus)
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                }
                            }
                            else
                            {
                                if (PaymentType > 0)
                                {
                                    if (ShippingType > 0)
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.OrderType == orderType && r.PaymentType == PaymentType && r.ShippingType == ShippingType)
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                    else
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.OrderType == orderType && r.PaymentType == PaymentType)
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                }
                                else
                                {
                                    if (ShippingType > 0)
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.OrderType == orderType && r.ShippingType == ShippingType)
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                    else
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.OrderType == orderType)
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (PaymentStatus > 0)
                        {
                            if (ExcuteStatus > 0)
                            {
                                if (PaymentType > 0)
                                {
                                    if (ShippingType > 0)
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.PaymentStatus == PaymentStatus && r.ExcuteStatus == ExcuteStatus && r.PaymentType == PaymentType && r.ShippingType == ShippingType)
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                    else
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.PaymentStatus == PaymentStatus && r.ExcuteStatus == ExcuteStatus && r.PaymentType == PaymentType)
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                }
                                else
                                {
                                    if (ShippingType > 0)
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.PaymentStatus == PaymentStatus && r.ExcuteStatus == ExcuteStatus && r.ShippingType == ShippingType)
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                    else
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.PaymentStatus == PaymentStatus && r.ExcuteStatus == ExcuteStatus)
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                }
                            }
                            else
                            {
                                if (PaymentType > 0)
                                {
                                    if (ShippingType > 0)
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.PaymentStatus == PaymentStatus && r.PaymentType == PaymentType && r.ShippingType == ShippingType)
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                    else
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.PaymentStatus == PaymentStatus && r.PaymentType == PaymentType)
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                }
                                else
                                {
                                    if (ShippingType > 0)
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.PaymentStatus == PaymentStatus && r.ShippingType == ShippingType)
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                    else
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.PaymentStatus == PaymentStatus)
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (ExcuteStatus > 0)
                            {
                                if (PaymentType > 0)
                                {
                                    if (ShippingType > 0)
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.ExcuteStatus == ExcuteStatus && r.PaymentType == PaymentType && r.ShippingType == ShippingType)
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                    else
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.ExcuteStatus == ExcuteStatus && r.PaymentType == PaymentType)
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                }
                                else
                                {
                                    if (ShippingType > 0)
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.ExcuteStatus == ExcuteStatus && r.ShippingType == ShippingType)
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                    else
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.ExcuteStatus == ExcuteStatus)
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                }
                            }
                            else
                            {
                                if (PaymentType > 0)
                                {
                                    if (ShippingType > 0)
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.PaymentType == PaymentType && r.ShippingType == ShippingType)
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                    else
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.PaymentType == PaymentType)
                                    .OrderByDescending(r => r.ID).ToList();
                                    }
                                }
                                else
                                {
                                    if (ShippingType > 0)
                                    {
                                        rs = dbe.tbl_Order
                                    .Where(r => r.ShippingType == ShippingType).OrderByDescending(r => r.ID).ToList();
                                    }
                                    else
                                    {
                                        rs = dbe.tbl_Order.OrderByDescending(r => r.ID).ToList();
                                    }
                                }
                            }
                        }
                    }
                }


                if (!string.IsNullOrEmpty(sku))
                {
                    var product = dbe.tbl_OrderDetail.Where(x => x.SKU == sku).ToList();
                    if (product != null)
                    {
                        for (int i = 0; i < product.Count(); i++)
                        {
                            for (int j = 0; j < rs.Count(); j++)
                            {
                                if (product[i].OrderID == rs[j].ID)
                                {
                                    end.Add(rs[j]);
                                }
                            }
                        }
                    }
                    return end.Where(r => r.AgentID == agentID).OrderByDescending(r => r.ID).ToList();
                }
                else
                {
                    return rs.Where(r => r.AgentID == agentID).OrderByDescending(r => r.ID).ToList();
                }
            }

        }

        public static List<tbl_Order> GetByCustomerID(int ID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_Order> las = new List<tbl_Order>();
                las = dbe.tbl_Order.Where(x => x.CustomerID == ID).OrderByDescending(x => x.ID).ToList();
                return las;
            }
        }

        public static List<tbl_Order> Report(string fromdate, string todate)
        {
            using (var db = new inventorymanagementEntities())
            {
                List<tbl_Order> or = new List<tbl_Order>();
                if (!string.IsNullOrEmpty(fromdate))
                {
                    if (!string.IsNullOrEmpty(todate))
                    {
                        DateTime fd = Convert.ToDateTime(fromdate);
                        DateTime td = Convert.ToDateTime(todate);
                        or = db.tbl_Order
                            .Where(r => r.DateDone >= fd && r.DateDone <= td)
                            .Where(r => r.ExcuteStatus == 2)
                            .Where(r => r.PaymentStatus == 3)
                            .ToList();
                    }
                    else
                    {
                        DateTime fd = Convert.ToDateTime(fromdate);
                        or = db.tbl_Order
                            .Where(r => r.CreatedDate >= fd)
                            .Where(r => r.ExcuteStatus == 2)
                            .Where(r => r.PaymentStatus == 3)
                            .ToList();
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(todate))
                    {
                        DateTime td = Convert.ToDateTime(todate);
                        or = db.tbl_Order
                            .Where(r => r.CreatedDate <= td)
                            .Where(r => r.ExcuteStatus == 2)
                            .Where(r => r.PaymentStatus == 3)
                            .ToList();
                    }
                    else
                    {
                        or = db.tbl_Order
                            .Where(r => r.ExcuteStatus == 2)
                            .Where(r => r.PaymentStatus == 3)
                            .ToList();
                    }
                }
                return or;
            }
        }

        public static long GetTotalPriceByAccount(string accountName, DateTime fromdate, DateTime todate)
        {
            using (var con = new inventorymanagementEntities())
            {
                return con.tbl_Order
                    .Where(x => (fromdate <= x.DateDone && x.DateDone <= todate)
                                && x.CreatedBy.Trim().ToUpper() == accountName.Trim().ToUpper()
                                && (x.ExcuteStatus == 2 && x.PaymentStatus == 3)
                           )
                     .ToList()
                     .Sum(x => Convert.ToInt64(x.TotalPrice));
            }
        }

        public static int GetTotalProductSalesByAccount(string accountName, DateTime fromdate, DateTime todate)
        {
            using (var con = new inventorymanagementEntities())
            {
                List<tbl_Order> or = new List<tbl_Order>();
                or = con.tbl_Order
                    .Where(x => (fromdate <= x.DateDone && x.DateDone <= todate)
                                && x.CreatedBy.Trim().ToUpper() == accountName.Trim().ToUpper()
                                && (x.ExcuteStatus == 2 && x.PaymentStatus == 3)
                           )
                     .ToList();
                int tongbanra = 0;
                if (or != null)
                {
                    foreach (var item in or)
                    {
                        var oddetail = OrderDetailController.GetByOrderID(item.ID);
                        if (oddetail != null)
                        {
                            foreach (var temp in oddetail)
                            {
                                tongbanra += Convert.ToInt32(temp.Quantity);
                            }
                        }
                    }
                }
                return tongbanra;
            }
        }

        #endregion

        public class OrderReport
        {
            public int ID { get; set; }
            public int Quantity { get; set; }
            public double TotalRevenue { get; set; }
            public double TotalCost { get; set; }
        }
        public class OrderList
        {
            public int ID { get; set; }
            public int OrderType { get; set; }
            public string CustomerName { get; set; }
            public string CustomerPhone { get; set; }
            public string Nick { get; set; }
            public int CustomerID { get; set; }
            public double TotalPrice { get; set; }
            public double TotalDiscount { get; set; }
            public double FeeShipping { get; set; }
            public string OtherFeeName { get; set; }
            public double OtherFeeValue { get; set; }
            public string CreatedBy { get; set; }
            public DateTime CreatedDate { get; set; }
            public Nullable<System.DateTime> DateDone { get; set; }
            public int PaymentStatus { get; set; }
            public int ExcuteStatus { get; set; }
            public int PaymentType { get; set; }
            public int ShippingType { get; set; }
            public Nullable<int> RefundsGoodsID { get; set; }
            public int Quantity { get; set; }
            public string ShippingCode { get; set; }
            public Nullable<int> TransportCompanyID { get; set; }
            public Nullable<int> TransportCompanySubID { get; set; }
            public string OrderNote { get; set; }
            public int PostalDeliveryType { get; set; }

            // Custom Transfer Bank
            public int? CusBankID { get; set; }
            public string CusBankName { get; set; }
            public int? AccBankID { get; set; }
            public string AccBankName { get; set; }
            public Decimal? MoneyReceive { get; set; }
            public int? TransferStatus { get; set; }
            public string StatusName { get; set; }
            public DateTime? DoneAt { get; set; }
            public string TransferNote { get; set; }

            // Custom Delivery
            public DateTime? DeliveryDate { get; set; }
            public int? DeliveryStatus { get; set; }
            public int? ShipperID { get; set; }
            public Decimal? CostOfDelivery { get; set; }
            public Decimal? CollectionOfOrder { get; set; }
            public string ShipNote { get; set; }
            public string ShipperName { get; set; }
            public string InvoiceImage { get; set; }
        }

        public class OrderSQL
        {
            public int ID { get; set; }
            public int OrderType { get; set; }
            public string CustomerName { get; set; }
            public string CustomerPhone { get; set; }
            public int TotalPrice { get; set; }
            public string CreatedDate { get; set; }
            public string CreatedBy { get; set; }
            public string DateDone { get; set; }

            public int PaymentStatus { get; set; }
            public int ExcuteStatus { get; set; }
            public int PaymentType { get; set; }
            public int ShippingType { get; set; }

        }

        public static ProfitReportModel GetProfitReport(DateTime fromDate, DateTime toDate)
        {
            using (var con = new inventorymanagementEntities())
            {
                int TotalNumberOfOrder = 0;
                double TotalSalePrice = 0;
                double TotalSaleCost = 0;
                double TotalSaleDiscount = 0;
                double TotalOtherFee = 0;
                double TotalShippingFee = 0;

                double TotalRefundPrice = 0;
                double TotalRefundFee = 0;
                double TotalRefundCost = 0;

                // Get all infor product
                var productTarget = con.tbl_Product
                    .GroupJoin(
                        con.tbl_ProductVariable,
                        product => new
                        {
                            ProductStyle = product.ProductStyle.Value,
                            ProductID = product.ID,
                        },
                        productVariable => new
                        {
                            ProductStyle = 2,
                            ProductID = productVariable.ProductID.Value,
                        },
                        (product, productVariable) => new
                        {
                            product,
                            productVariable
                        })
                    .SelectMany(x => x.productVariable.DefaultIfEmpty(),
                                (parent, child) => new
                                {
                                    CategoryID = parent.product.CategoryID.Value,
                                    ProductID = parent.product.ID,
                                    ProductVariableID = child != null ? child.ID : 0,
                                    ProductStyle = parent.product.ProductStyle.Value,
                                    ProductImage = child != null ? child.Image : parent.product.ProductImage,
                                    ProductTitle = parent.product.ProductTitle,
                                    VariableValue = String.Empty,
                                    SKU = child != null ? child.SKU : parent.product.ProductSKU,
                                    RegularPrice = child != null ? child.Regular_Price.Value : parent.product.Regular_Price.Value,
                                    CostOfGood = child != null ? child.CostOfGood.Value : parent.product.CostOfGood.Value,
                                    RetailPrice = child != null ? child.RetailPrice.Value : parent.product.Retail_Price.Value
                                })
                    .OrderBy(x => x.SKU);

                // Choose order in date target
                var orderTarget = con.tbl_Order
                    .Where(x => (x.DateDone >= fromDate && x.DateDone <= toDate)
                                && x.ExcuteStatus == 2
                                && x.PaymentStatus == 3)
                    .OrderBy(x => x.ID);

                var orderDetailTarget = con.tbl_OrderDetail.OrderBy(x => x.OrderID).ThenBy(x => x.ID);

                // Get info order
                var inforOrder = orderTarget
                    .Join(
                        orderDetailTarget,
                        order => order.ID,
                        detail => detail.OrderID,
                        (order, detail) => new
                        {
                            OrderID = order.ID,
                            ProductID = detail.ProductID.Value,
                            ProductVariableID = detail.ProductVariableID.Value,
                            ProductStyle = detail.ProductType.Value,
                            SKU = detail.SKU,
                            Price = detail.Price,
                            Quantity = detail.Quantity.Value,
                            TotalPrice = order.TotalPrice,
                            TotalDiscount = order.TotalDiscount,
                            TotalOtherFee = order.OtherFeeValue,
                            TotalShippingFee = order.FeeShipping
                        })
                        .OrderBy(x => x.SKU)
                    .Join(
                        productTarget,
                        order => order.SKU,
                        product => product.SKU,
                        (order, product) => new
                        {
                            OrderID = order.OrderID,
                            ProductID = order.ProductID,
                            ProductVariableID = order.ProductVariableID,
                            ProductStyle = order.ProductStyle,
                            SKU = order.SKU,
                            Price = order.Price,
                            Quantity = order.Quantity,
                            CostOfGood = product.CostOfGood,
                            TotalPrice = order.TotalPrice,
                            TotalDiscount = order.TotalDiscount,
                            TotalOtherFee = order.TotalOtherFee,
                            TotalShippingFee = order.TotalShippingFee
                        })
                    .ToList();

                // Choose refund good in date target
                var refundTarget = con.tbl_RefundGoods
                    .Where(x => x.CreatedDate >= fromDate && x.CreatedDate <= toDate)
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
                    .OrderBy(x => x.SKU)
                    .Join(
                        productTarget,
                        refund => refund.SKU,
                        product => product.SKU,
                        (refund, product) => new
                        {
                            RefundGoodsID = refund.RefundGoodsID,
                            ProductStyle = refund.ProductStyle,
                            SKU = refund.SKU,
                            Quantity = refund.Quantity,
                            SoldPrice = refund.SoldPrice,
                            CostOfGood = product.CostOfGood,
                            TotalRefundPrice = refund.TotalRefundPrice,
                            TotalRefundFee = refund.TotalRefundFee
                        })
                      .ToList();

                TotalNumberOfOrder = inforOrder.GroupBy(x => x.OrderID).Count();

                TotalSalePrice = inforOrder
                    .Select(x => new { TotalSalePrice = x.Quantity * Convert.ToDouble(x.Price) })
                    .Sum(x => x.TotalSalePrice);

                TotalSaleCost = inforOrder
                    .Select(x => new { TotalSaleCost = x.Quantity * Convert.ToDouble(x.CostOfGood) })
                    .Sum(x => x.TotalSaleCost);

                TotalSaleDiscount = inforOrder
                    .GroupBy(x => x.OrderID)
                    .Select(g => new { TotalSaleDiscount = g.Max(x => Convert.ToDouble(x.TotalDiscount)) })
                    .Sum(x => x.TotalSaleDiscount);

                TotalOtherFee = inforOrder
                    .GroupBy(x => x.OrderID)
                    .Select(g => new { TotalOtherFee = g.Max(x => Convert.ToDouble(x.TotalOtherFee)) })
                    .Sum(x => x.TotalOtherFee);

                TotalShippingFee = inforOrder
                    .GroupBy(x => x.OrderID)
                    .Select(g => new { TotalShippingFee = g.Max(x => Convert.ToDouble(x.TotalShippingFee)) })
                    .Sum(x => x.TotalShippingFee);

                TotalRefundPrice = infoRefund
                    .Select(x => new { TotalRefundPrice = x.Quantity * Convert.ToDouble(x.SoldPrice) })
                    .Sum(x => x.TotalRefundPrice);

                TotalRefundCost = infoRefund
                    .Select(x => new { TotalRefundCost = x.Quantity * Convert.ToDouble(x.CostOfGood) })
                    .Sum(x => x.TotalRefundCost);

                TotalRefundFee = infoRefund
                    .GroupBy(x => x.RefundGoodsID)
                    .Select(g => new { TotalRefundFee = g.Max(x => Convert.ToDouble(x.TotalRefundFee)) })
                    .Sum(x => x.TotalRefundFee);

                return new ProfitReportModel()
                {
                    TotalNumberOfOrder = TotalNumberOfOrder,
                    TotalSalePrice = TotalSalePrice,
                    TotalSaleCost = TotalSaleCost,
                    TotalSaleDiscount = TotalSaleDiscount,
                    TotalOtherFee = TotalOtherFee,
                    TotalShippingFee = TotalShippingFee,
                    TotalRefundPrice = TotalRefundPrice,
                    TotalRefundCost = TotalRefundCost,
                    TotalRefundFee = TotalRefundFee
                };
            }
        }

        public static ProductReportModel getProductReport(string SKU, DateTime fromDate, DateTime toDate)
        {
            var list = new List<OrderReport>();
            var sql = new StringBuilder();

            sql.AppendLine(String.Format("SELECT Ord.ID, SUM(ISNULL(OrdDetail.Quantity, 0)) AS Quantity, SUM(OrdDetail.Quantity * ISNULL(Product.CostOfGood, Variable.CostOfGood)) AS TotalCost, SUM(OrdDetail.Quantity * (OrdDetail.Price - Ord.DiscountPerProduct)) AS TotalRevenue"));
            sql.AppendLine(String.Format("FROM tbl_Order AS Ord"));
            sql.AppendLine(String.Format("INNER JOIN tbl_OrderDetail AS OrdDetail"));
            sql.AppendLine(String.Format("ON 	Ord.ID = OrdDetail.OrderID"));
            sql.AppendLine(String.Format("LEFT JOIN tbl_ProductVariable AS Variable"));
            sql.AppendLine(String.Format("ON 	OrdDetail.SKU = Variable.SKU"));
            sql.AppendLine(String.Format("LEFT JOIN tbl_Product AS Product"));
            sql.AppendLine(String.Format("ON 	OrdDetail.SKU = Product.ProductSKU"));
            sql.AppendLine(String.Format("WHERE 1 = 1"));
            sql.AppendLine(String.Format("	AND Ord.ExcuteStatus = 2"));
            sql.AppendLine(String.Format("	AND Ord.PaymentStatus = 3"));
            sql.AppendLine(String.Format("	AND OrdDetail.SKU LIKE '{0}%'", SKU));
            sql.AppendLine(String.Format("	AND	CONVERT(datetime, Ord.DateDone, 121) BETWEEN CONVERT(datetime, '{0}', 121) AND CONVERT(datetime, '{1}', 121)", fromDate.ToString(), toDate.ToString()));
            sql.AppendLine(String.Format("GROUP BY Ord.ID"));

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql.ToString());
            while (reader.Read())
            {
                var entity = new OrderReport();

                entity.ID = Convert.ToInt32(reader["ID"]);
                entity.Quantity = Convert.ToInt32(reader["Quantity"]);
                entity.TotalRevenue = Convert.ToDouble(reader["TotalRevenue"]);
                entity.TotalCost = Convert.ToDouble(reader["TotalCost"]);
                list.Add(entity);
            }
            reader.Close();

            return new ProductReportModel()
            {
                totalSold = list.Sum(x => x.Quantity),
                totalRevenue = list.Sum(x => x.TotalRevenue),
                totalCost = list.Sum(x => x.TotalCost)
            };
        }

        public class ProductReportModel
        {
            public int totalSold { get; set; }
            public double totalRevenue { get; set; }
            public double totalCost { get; set; }
        }
    }
}