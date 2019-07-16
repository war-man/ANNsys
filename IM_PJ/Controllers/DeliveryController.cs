﻿using IM_PJ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace IM_PJ.Controllers
{
    public class DeliveryController
    {
        public static bool Update(Delivery delivery)
        {
            using (var con = new inventorymanagementEntities())
            {
                var old = con.Deliveries.Where(x => x.OrderID == delivery.OrderID).SingleOrDefault();
                if (old != null)
                {
                    old.ShipperID = delivery.ShipperID;
                    old.Status = delivery.Status;
                    old.Image = delivery.Image;
                    old.COD = delivery.COD;
                    old.COO = delivery.COO;
                    old.ShipNote = delivery.ShipNote;
                    old.StartAt = delivery.StartAt;
                    old.ModifiedBy = delivery.ModifiedBy;
                    old.ModifiedDate = delivery.ModifiedDate;
                    old.Times = delivery.Times;
                    con.SaveChanges();
                }
                else
                {
                    con.Deliveries.Add(delivery);
                    con.SaveChanges();
                }
            }

            return true;
        }

        public static TransportReport getTransportReport(List<int> orders)
        {
            using (var con = new inventorymanagementEntities())
            {
                var header = con.tbl_Order
                    .Where(x => x.ExcuteStatus == 2) // Đơn đã hoàn tất
                    .Where(x => x.ShippingType == 4) // Chuyển tới nhà xe
                    .Where(x => orders.Contains(x.ID))
                    .OrderBy(o => o.ID);

                var transport = con.tbl_TransportCompany
                    .Join(
                        header,
                        tran => new { tranID = tran.ID, tranSubID = tran.SubID },
                        ord => new { tranID = ord.TransportCompanyID.Value, tranSubID = ord.TransportCompanySubID.Value },
                        (tran, ord) => new { tran, ord }
                     )
                     .Select(x => new
                     {
                         OrderID = x.ord.ID,
                         TransportID = x.tran.ID,
                         TransportName = x.tran.CompanyName
                     })
                     .OrderBy(o => o.OrderID);

                var customer = con.tbl_Customer
                    .Join(
                        header,
                        c => c.ID,
                        o => o.CustomerID,
                        (c, o) => c
                    )
                    .Distinct()
                    .OrderBy(o => o.ID);

                var data = header
                    .Join(
                        customer,
                        o => o.CustomerID,
                        c => c.ID,
                        (o, c) => new { o, c }
                    )
                    .Join(
                        transport,
                        h => h.o.ID,
                        t => t.OrderID,
                        (h, t) => new
                        {
                            OrderID = h.o.ID,
                            TransportID = t.TransportID,
                            TransportName = t.TransportName,
                            CustomerID = h.o.CustomerID.Value,
                            CustomerName = h.c.CustomerName,
                            Quantity = 1,
                            Collection = h.o.PaymentType == (int)PaymentType.CashCollection ? 1 : 0,
                            Payment = h.o.TotalPrice
                        }
                    )
                    .ToList();

                var report = new TransportReport()
                {
                    Transports = data
                        .GroupBy(x => x.TransportID)
                        .Select(g => new TransportInfo
                        {
                            TransportID = g.Key,
                            TransportName = g.Max(x => x.TransportName),
                            Quantity = g.Sum(x => x.Quantity),
                            Collection = g.Sum(x => x.Collection),
                        })
                        .OrderBy(o => o.TransportName)
                        .ToList(),
                    Collections = data.Where(x => x.Collection == 1)
                        .Select(x => new CollectionInfo()
                        {
                            OrderID = x.OrderID,
                            TransportID = x.TransportID,
                            TransportName = x.TransportName,
                            CustomerID = x.CustomerID,
                            CustomerName = x.CustomerName,
                            Collection = Convert.ToDecimal(x.Payment)
                        })
                        .OrderByDescending(o => new {
                            o.TransportName,
                            o.OrderID
                        })
                        .ToList()
                }; 

                return report;
            }
        }

        public static List<ShipperReport> getShipperReport(List<int> orders)
        {
            using (var con = new inventorymanagementEntities())
            {
                var delivery = con.Deliveries
                    .Where(x => orders.Contains(x.OrderID))
                    .OrderBy(o => o.OrderID)
                    .ToList();

                var order = con.tbl_Order
                     .Where(x => x.ExcuteStatus == 2) // Đơn đã hoàn tất
                     .Where(x => x.ShippingType == 5) // Nhân viên giao hàng
                     .Where(x => orders.Contains(x.ID))
                     .OrderBy(o => o.ID)
                     .ToList();

                var report = order
                    .GroupJoin(
                        delivery,
                        ord => ord.ID,
                        del => del.OrderID,
                        (ord, del) => new { ord, del }
                    )
                    .SelectMany(
                        x => x.del.DefaultIfEmpty(),
                        (parent, child) => {
                            var data = new ShipperReport();

                            data.OrderID = parent.ord.ID;
                            data.CustomerName = parent.ord.CustomerName;
                            data.Payment = Convert.ToDecimal(parent.ord.TotalPrice);
                            data.MoneyCollection = 0;
                            data.Price = 0;

                            if (child != null)
                            {
                                // Thu hộ
                                if (parent.ord.PaymentType == 3)
                                {
                                    data.MoneyCollection = child.COO;
                                }
                                data.Price = child.COD;
                            }
                            else
                            {
                                // Thu hộ
                                if (parent.ord.PaymentType == 3)
                                {
                                    data.MoneyCollection = data.Payment;
                                }
                                data.Price = Convert.ToDecimal(parent.ord.FeeShipping);
                            }

                            return data;
                        }
                    )
                    .ToList();

                return report;
            }
        }

        public static void udpateAfterPrint(int shiperID, List<int> orders, int user, int deliveryTimes)
        {
            using (var con = new inventorymanagementEntities())
            {
                foreach (var id in orders)
                {
                    var now = DateTime.Now;
                    var old = con.Deliveries.Where(x => x.OrderID == id).SingleOrDefault();
                    if (old != null)
                    {
                        // Tránh trường hợp record đã được cập nhật trường hợp khác
                        if (old.Status == 2 || old.Status == 3)
                        {
                            old.ShipperID = shiperID;
                            old.Status = 3;
                            old.ModifiedBy = user;
                            old.ModifiedDate = now;
                            old.Times = deliveryTimes;
                            con.SaveChanges();
                        }
                    }
                    else
                    {
                        var order = con.tbl_Order.Where(x => x.ID == id).FirstOrDefault();
                        var delivery = new Delivery()
                        {
                            UUID = Guid.NewGuid(),
                            OrderID = id,
                            Status = 3,
                            StartAt = now,
                            ShipperID = shiperID,
                            COO = 0,
                            COD = 0,
                            ShipNote = String.Empty,
                            CreatedBy = user,
                            CreatedDate = now,
                            ModifiedBy = user,
                            ModifiedDate = now,
                            Times = deliveryTimes
                        };

                        if (order != null)
                        {
                            // Thu hộ
                            if (order.PaymentType == 3)
                            {
                                delivery.COO = Convert.ToDecimal(order.TotalPrice);
                            }
                            delivery.COD = Convert.ToDecimal(order.FeeShipping);
                        }
                        con.Deliveries.Add(delivery);
                        con.SaveChanges();
                    }
                }
            }
        }

        public static string getDeliveryLast(int customerID)
        {
            using (var con = new inventorymanagementEntities())
            {
                var last = con.tbl_Customer
                    .Where(x => x.ID == customerID)
                    .Where(x => x.ShippingType == 4) // Hình thức nhà xe
                    .Join(
                        con.tbl_TransportCompany,
                        cus => new { tranID = cus.TransportCompanyID.Value, tranSubID = cus.TransportCompanySubID.Value },
                        tran => new { tranID = tran.ID, tranSubID = tran.SubID },
                        (cus, tran) => new
                        {
                            tranID = tran.ID,
                            tranName = tran.CompanyName,
                            tranSubID = tran.SubID,
                            tranSubName = tran.ShipTo
                        }
                    )
                    .SingleOrDefault();

                var serializer = new JavaScriptSerializer();

                if (last == null)
                {
                    last = con.tbl_Order
                        .Where(x => x.CustomerID == customerID)
                        .Where(x => x.ShippingType == 4) // Hình thức nhà xe
                        .OrderByDescending(o => o.DateDone)
                        .Join(
                            con.tbl_TransportCompany,
                            cus => new { tranID = cus.TransportCompanyID.Value, tranSubID = cus.TransportCompanySubID.Value },
                            tran => new { tranID = tran.ID, tranSubID = tran.SubID },
                            (cus, tran) => new
                            {
                                tranID = tran.ID,
                                tranName = tran.CompanyName,
                                tranSubID = tran.SubID,
                                tranSubName = tran.ShipTo
                            }
                        )
                        .FirstOrDefault();
                }

                return serializer.Serialize(last);
            }
        }
        
        public static void updateDelivery(tbl_Account acc, List<DeliverySession> session)
        {
            using (var con = new inventorymanagementEntities())
            {
                var now = DateTime.Now;

                foreach (var item in session)
                {
                    var data = con.Deliveries
                    .Where(x => x.OrderID == item.OrderID)
                    .FirstOrDefault();

                    if (data != null)
                    {
                        data.ShipperID = item.ShipperID;
                        data.Times = item.DeliveryTimes;
                        data.Status = item.DeliveryStatus;
                        data.ModifiedBy = acc.ID;
                        data.ModifiedDate = now;

                        con.SaveChanges();
                    }
                    else
                    {
                        var delivery = new Delivery()
                        {
                            UUID = Guid.NewGuid(),
                            OrderID = item.OrderID,
                            ShipperID = item.ShipperID,
                            Status = item.DeliveryStatus,
                            Image = String.Empty,
                            COD = 0,
                            COO = 0,
                            ShipNote = String.Empty,
                            StartAt = now,
                            Note = String.Empty,
                            CreatedBy = acc.ID,
                            CreatedDate = now,
                            ModifiedBy = acc.ID,
                            ModifiedDate = now,
                            Times = item.DeliveryTimes
                        };

                        con.Deliveries.Add(delivery);
                        con.SaveChanges();
                    }

                }
            }
        }

        public class TransportInfo
        {
            public int TransportID { get; set; }
            public string TransportName { get; set; }
            public double Quantity { get; set; }
            // Số lượng thu hộ
            public double Collection { get; set; }
        }

        public class CollectionInfo
        {
            public int OrderID { get; set; }
            public int TransportID { get; set; }
            public string TransportName { get; set; }
            public int CustomerID { get; set; }
            public string CustomerName { get; set; }
            // Số tiền thu hộ
            public decimal Collection { get; set; }
        }

        public class TransportReport
        {
            public List<TransportInfo> Transports { get; set; }
            public List<CollectionInfo> Collections { get; set; }
        }

        public class ShipperReport
        {
            public int OrderID { get; set; }
            public string CustomerName { get; set; }
            public decimal Payment { get; set; }
            public decimal MoneyCollection { get; set; }
            public decimal Price { get; set; }
        }
    }
}