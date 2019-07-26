using IM_PJ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace IM_PJ.Controllers
{
    public class SessionController
    {
        #region Xử lý cho màn danh sách vận chuyển
        public static List<DeliverySession> getDeliverySession(tbl_Account acc, string page = "danh-sach-van-chuyen")
        {
            using (var con = new inventorymanagementEntities())
            {
                var session = con.Sessions
                    .Where(x => x.AccountID == acc.ID)
                    .Where(x => x.Page.ToLower() == page.ToLower())
                    .FirstOrDefault();

                if (session != null)
                    return JsonConvert.DeserializeObject<List<DeliverySession>>(session.Value);
                else
                    return new List<DeliverySession>();
            }
        }

        public static string addDeliverySession(tbl_Account acc, List<DeliverySession> deliverySession, string page = "danh-sach-van-chuyen")
        {
            using (var con = new inventorymanagementEntities())
            {
                var session = con.Sessions
                    .Where(x => x.AccountID == acc.ID)
                    .Where(x => x.Page.ToLower() == page.ToLower())
                    .FirstOrDefault();

                if (session != null)
                {
                    var oldDdeliveries = JsonConvert.DeserializeObject<List<DeliverySession>>(session.Value);
                    // Loc ra dua lieu cu
                    var dataExists = oldDdeliveries
                        .Join(
                            deliverySession,
                            olds => olds.OrderID,
                            news => news.OrderID,
                            (olds, news) => olds
                        )
                        .ToList();
                    // Remove du lieu cu
                    foreach (var item in dataExists)
                    {
                        oldDdeliveries.Remove(item);
                    }

                    if (deliverySession.Count > 0)
                    {
                        oldDdeliveries.AddRange(deliverySession);
                        oldDdeliveries = oldDdeliveries.OrderByDescending(o => o.OrderID).ToList();
                        session.Value = JsonConvert.SerializeObject(oldDdeliveries);
                        session.ModifiedDate = DateTime.Now;

                        con.SaveChanges();
                    }

                    return session.Value;
                }
                else
                {
                    var now = DateTime.Now;
                    var data = new Session()
                    {
                        AccountID = acc.ID,
                        Page = page.ToLower(),
                        Value = JsonConvert.SerializeObject(deliverySession),
                        CreatedBy = acc.ID,
                        CreatedDate = now,
                        ModifiedBy = acc.ID,
                        ModifiedDate = now
                    };

                    con.Sessions.Add(data);
                    con.SaveChanges();

                    return data.Value;
                }
            }
        }

        public static void updateDeliverySession(
            tbl_Account acc, 
            List<DeliverySession> deliverySession, 
            string page = "danh-sach-van-chuyen"
        )
        {
            using (var con = new inventorymanagementEntities())
            {
                var session = con.Sessions
                    .Where(x => x.AccountID == acc.ID)
                    .Where(x => x.Page.ToLower() == page.ToLower())
                    .FirstOrDefault();

                if (session != null)
                {
                    var oldDdeliveries = JsonConvert.DeserializeObject<List<DeliverySession>>(session.Value);
                    // update dữ liệu
                    oldDdeliveries = oldDdeliveries
                        .GroupJoin(
                            deliverySession,
                            olds => olds.OrderID,
                            news => news.OrderID,
                            (olds, news) => new { olds, news}
                        )
                        .SelectMany(
                            x => x.news.DefaultIfEmpty(),
                            (parent, child) =>
                            {
                                if (child != null)
                                {
                                    return child;
                                }
                                else
                                {
                                    return parent.olds;
                                }
                            }
                        )
                        .ToList();

                    session.Value = JsonConvert.SerializeObject(oldDdeliveries);
                    session.ModifiedDate = DateTime.Now;

                    con.SaveChanges();
                }
            }
        }

        public static string deleteDeliverySession(tbl_Account acc, List<DeliverySession> deliverySession, string page = "danh-sach-van-chuyen")
        {
            using (var con = new inventorymanagementEntities())
            {
                var session = con.Sessions
                    .Where(x => x.AccountID == acc.ID)
                    .Where(x => x.Page.ToLower() == page.ToLower())
                    .FirstOrDefault();

                if (session != null)
                {
                    var oldDdeliveries = JsonConvert.DeserializeObject<List<DeliverySession>>(session.Value);

                    oldDdeliveries = oldDdeliveries
                        .Except(
                            oldDdeliveries.Join(
                                    deliverySession,
                                    olds => olds.OrderID,
                                    news => news.OrderID,
                                    (olds, news) => olds
                                )
                        )
                        .OrderByDescending(o => o.OrderID)
                        .ToList();
                    session.Value = JsonConvert.SerializeObject(oldDdeliveries);
                    session.ModifiedDate = DateTime.Now;

                    con.SaveChanges();

                    return session.Value;
                }

                return JsonConvert.SerializeObject(new List<DeliverySession>());
            }
        }

        public static void deleteDeliverySession(tbl_Account acc, string page = "danh-sach-van-chuyen")
        {
            using (var con = new inventorymanagementEntities())
            {
                var session = con.Sessions
                    .Where(x => x.AccountID == acc.ID)
                    .Where(x => x.Page.ToLower() == page.ToLower());

                if (session.Count() > 0)
                {
                    con.Sessions.RemoveRange(session);
                    con.SaveChanges();
                }
            }
        }
        #endregion

        #region Xử lý cho màn hình danh sách đăng ký nhập hàng
        public static List<RegisterProductSession> getRegisterProductSession(tbl_Account acc,
                                                                      string page = "danh-sach-nhap-hang")
        {
            using (var con = new inventorymanagementEntities())
            {
                var session = con.Sessions
                    .Where(x => x.AccountID == acc.ID)
                    .Where(x => x.Page.ToLower() == page.ToLower())
                    .FirstOrDefault();

                if (session != null)
                    return JsonConvert.DeserializeObject<List<RegisterProductSession>>(session.Value);
                else
                    return new List<RegisterProductSession>();
            }
        }

        public static string addRegisterProductSession(tbl_Account acc, 
                                                       List<RegisterProductSession> registerProductSession,
                                                       string page = "danh-sach-nhap-hang")
        {
            using (var con = new inventorymanagementEntities())
            {
                var session = con.Sessions
                    .Where(x => x.AccountID == acc.ID)
                    .Where(x => x.Page.ToLower() == page.ToLower())
                    .FirstOrDefault();

                if (session != null)
                {
                    var oldDdeliveries = JsonConvert.DeserializeObject<List<RegisterProductSession>>(session.Value);
                    // Loc ra dua lieu cu
                    var dataExists = oldDdeliveries
                        .Join(
                            registerProductSession,
                            olds => olds.id,
                            news => news.id,
                            (olds, news) => olds
                        )
                        .ToList();

                    // Remove du lieu cu
                    foreach (var item in dataExists)
                    {
                        oldDdeliveries.Remove(item);
                    }

                    if (registerProductSession.Count > 0)
                    {
                        oldDdeliveries.AddRange(registerProductSession);
                        oldDdeliveries = oldDdeliveries.OrderByDescending(o => o.id).ToList();
                        session.Value = JsonConvert.SerializeObject(oldDdeliveries);
                        session.ModifiedDate = DateTime.Now;

                        con.SaveChanges();
                    }

                    return session.Value;
                }
                else
                {
                    var now = DateTime.Now;
                    var data = new Session()
                    {
                        AccountID = acc.ID,
                        Page = page.ToLower(),
                        Value = JsonConvert.SerializeObject(registerProductSession),
                        CreatedBy = acc.ID,
                        CreatedDate = now,
                        ModifiedBy = acc.ID,
                        ModifiedDate = now
                    };

                    con.Sessions.Add(data);
                    con.SaveChanges();

                    return data.Value;
                }
            }
        }

        public static void updateRegisterProductSession(tbl_Account acc,
                                                        List<RegisterProductSession> registerProductSession,
                                                        string page = "danh-sach-nhap-hang"
        )
        {
            using (var con = new inventorymanagementEntities())
            {
                var session = con.Sessions
                    .Where(x => x.AccountID == acc.ID)
                    .Where(x => x.Page.ToLower() == page.ToLower())
                    .FirstOrDefault();

                if (session != null)
                {
                    var oldDdeliveries = JsonConvert.DeserializeObject<List<RegisterProductSession>>(session.Value);
                    // update dữ liệu
                    oldDdeliveries = oldDdeliveries
                        .GroupJoin(
                            registerProductSession,
                            olds => olds.id,
                            news => news.id,
                            (olds, news) => new { olds, news }
                        )
                        .SelectMany(
                            x => x.news.DefaultIfEmpty(),
                            (parent, child) =>
                            {
                                if (child != null)
                                {
                                    return child;
                                }
                                else
                                {
                                    return parent.olds;
                                }
                            }
                        )
                        .ToList();

                    session.Value = JsonConvert.SerializeObject(oldDdeliveries);
                    session.ModifiedDate = DateTime.Now;

                    con.SaveChanges();
                }
            }
        }

        public static string deleteRegisterProductSession(tbl_Account acc,
                                                          List<RegisterProductSession> registerProductSession,
                                                          string page = "danh-sach-nhap-hang")
        {
            using (var con = new inventorymanagementEntities())
            {
                var session = con.Sessions
                    .Where(x => x.AccountID == acc.ID)
                    .Where(x => x.Page.ToLower() == page.ToLower())
                    .FirstOrDefault();

                if (session != null)
                {
                    var oldDdeliveries = JsonConvert.DeserializeObject<List<RegisterProductSession>>(session.Value);

                    oldDdeliveries = oldDdeliveries
                        .Except(
                            oldDdeliveries.Join(
                                    registerProductSession,
                                    olds => olds.id,
                                    news => news.id,
                                    (olds, news) => olds
                                )
                        )
                        .OrderByDescending(o => o.id)
                        .ToList();
                    session.Value = JsonConvert.SerializeObject(oldDdeliveries);
                    session.ModifiedDate = DateTime.Now;

                    con.SaveChanges();

                    return session.Value;
                }

                return JsonConvert.SerializeObject(new List<RegisterProductSession>());
            }
        }

        public static void deleteRegisterProductSession(tbl_Account acc, string page = "danh-sach-nhap-hang")
        {
            using (var con = new inventorymanagementEntities())
            {
                var session = con.Sessions
                    .Where(x => x.AccountID == acc.ID)
                    .Where(x => x.Page.ToLower() == page.ToLower());

                if (session.Count() > 0)
                {
                    con.Sessions.RemoveRange(session);
                    con.SaveChanges();
                }
            }
        }
        #endregion
    }

    public class DeliverySession
    {
        public int OrderID { get; set; }
        public int ShipperID { get; set; }
        public int ShippingType { get; set; }
        public DateTime CreatedDate { get; set; }
        public int DeliveryTimes { get; set; }
        public int DeliveryStatus { get; set; }
        public decimal COD { get; set; }
        public decimal ShippingFee { get; set; }
    }

    public class RegisterProductSession
    {
        public int id { get; set; }
        public string customer { get; set; }
        public int status { get; set; }
        public int quantity { get; set; }
        public DateTime? expectedDate { get; set; }
        public string note { get; set; }
    }
}