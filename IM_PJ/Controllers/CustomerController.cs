using IM_PJ.Models;
using MB.Extensions;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebUI.Business;
using System.Text.RegularExpressions;
using System.Data.Entity.Validation;

namespace IM_PJ.Controllers
{
    public class CustomerController
    {
        #region CRUD
        public static string Insert(string CustomerName, string CustomerPhone, string CustomerAddress, string CustomerEmail, int CustomerLevelID, int Status,
            DateTime CreatedDate, string CreatedBy, bool IsHidden, string Zalo, string Facebook, string Note, string Province, string Nick, string Avatar = "", int ShippingType = 0, int PaymentType = 0, int TransportCompanyID = 0, int TransportCompanySubID = 0, string CustomerPhone2 = "")
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Customer ui = new tbl_Customer();
                ui.CustomerName = CustomerName;
                ui.CustomerPhone = CustomerPhone;
                ui.CustomerAddress = CustomerAddress;
                if (!string.IsNullOrEmpty(CustomerEmail))
                    ui.CustomerEmail = CustomerEmail;
                ui.CustomerLevelID = CustomerLevelID;
                ui.Status = Status;
                ui.CreatedDate = CreatedDate;
                ui.CreatedBy = CreatedBy;
                ui.IsHidden = IsHidden;

                if (!string.IsNullOrEmpty(Zalo))
                    ui.Zalo = Zalo;
                if (!string.IsNullOrEmpty(Facebook))
                    ui.Facebook = Facebook;
                if (!string.IsNullOrEmpty(Note))
                    ui.Note = Note;
                if (!string.IsNullOrEmpty(Province))
                    ui.ProvinceID = Province.ToInt();

                ui.Nick = Nick;

                if (!string.IsNullOrEmpty(Avatar))
                    ui.Avatar = Avatar;

                ui.ShippingType = ShippingType;
                ui.PaymentType = PaymentType;
                ui.TransportCompanyID = TransportCompanyID;
                ui.TransportCompanySubID = TransportCompanySubID;
                ui.CustomerPhone2 = CustomerPhone2;
                try
                {
                    dbe.tbl_Customer.Add(ui);
                    dbe.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }
                int kq = ui.ID;
                return kq.ToString();
            }
        }
        public static string Update(int ID, string CustomerName, string CustomerPhone, string CustomerAddress, string CustomerEmail, int CustomerLevelID, int Status,
           string CreatedBy, DateTime ModifiedDate, string ModifiedBy, bool IsHidden, string Zalo, string Facebook, string Note, string Province, string Nick, string Avatar, int ShippingType, int PaymentType, int TransportCompanyID, int TransportCompanySubID, string CustomerPhone2)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Customer ui = dbe.tbl_Customer.Where(a => a.ID == ID).SingleOrDefault();
                if (ui != null)
                {
                    ui.CustomerName = CustomerName;
                    ui.CustomerPhone = CustomerPhone;
                    ui.CustomerAddress = CustomerAddress;
                    ui.CustomerEmail = CustomerEmail;
                    ui.CustomerLevelID = CustomerLevelID;
                    ui.Status = Status;
                    ui.CreatedBy = CreatedBy;
                    ui.ModifiedBy = ModifiedBy;
                    ui.ModifiedDate = ModifiedDate;
                    ui.IsHidden = IsHidden;
                    ui.Zalo = Zalo;
                    ui.Facebook = Facebook;
                    ui.Note = Note;
                    ui.Nick = Nick;
                    ui.Avatar = Avatar;
                    ui.ShippingType = ShippingType;
                    ui.PaymentType = PaymentType;
                    ui.TransportCompanyID = TransportCompanyID;
                    ui.TransportCompanySubID = TransportCompanySubID;
                    ui.CustomerPhone2 = CustomerPhone2;

                    if (!string.IsNullOrEmpty(Province))
                        ui.ProvinceID = Province.ToInt();
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static tbl_Customer GetByPhone(string CustomerPhone)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Customer ai = dbe.tbl_Customer.Where(a => a.CustomerPhone == CustomerPhone || a.CustomerPhone2 == CustomerPhone || a.CustomerPhoneBackup == CustomerPhone).FirstOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }
        public static string convertToUnSign(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(System.Text.NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
        public static List<tbl_Customer> Find2(string text)
        {
            string text2 = convertToUnSign(text);
            using (var dbe = new inventorymanagementEntities())
            {
                var a = dbe.tbl_Customer.Where(delegate (tbl_Customer x)
                {
                    if (convertToUnSign(x.CustomerName).IndexOf(text2, StringComparison.CurrentCultureIgnoreCase) >= 0)
                        return true;
                    else
                        return false;
                }).ToList();
                var i = dbe.tbl_Customer.Where(x => x.CustomerName.Contains(text2) || x.CustomerName.Contains(text) || x.Nick.Contains(text) || x.Nick.Contains(text2) || x.CustomerPhone.Contains(text) || x.Zalo.Contains(text)).ToList();
                var ai = a.Concat(i).ToList();
                if (ai.Count() > 0)
                    return ai;
                return null;
            }
        }
        public static List<CustomerOut> Find(string text, string createdby = "")
        {
            string textsearch = '"' + text + '"';
            var list = new List<CustomerOut>();
            var sql = @"select c.ID, c.CustomerName, c.Nick, c.CustomerPhone, c.CustomerPhone2, c.Zalo, c.Facebook, c.CustomerAddress, c.CreatedBy, c.ProvinceID as Province
                        from tbl_Customer c
                         WHERE (CONTAINS(c.CustomerName,'" + textsearch + "')  OR CONTAINS(c.Nick,'" + textsearch + "') OR c.CustomerPhone like '%" + text + "%' OR c.CustomerPhone2 like '%" + text + "%' OR c.CustomerPhoneBackup like '%" + text + "%' OR c.Facebook like '%" + text + "%' OR c.Zalo like '%" + text + "%')";
            if (createdby != "")
            {
                sql += " And c.CreatedBy = N'" + createdby + "'";
            }
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                var entity = new CustomerOut();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["CustomerName"] != DBNull.Value)
                    entity.CustomerName = reader["CustomerName"].ToString();
                if (reader["CustomerPhone"] != DBNull.Value)
                    entity.CustomerPhone = reader["CustomerPhone"].ToString();
                if (reader["CustomerPhone2"] != DBNull.Value)
                    entity.CustomerPhone2 = reader["CustomerPhone2"].ToString();
                if (reader["CreatedBy"] != DBNull.Value)
                    entity.CreatedBy = reader["CreatedBy"].ToString();
                if (reader["CustomerAddress"] != DBNull.Value)
                    entity.CustomerAddress = reader["CustomerAddress"].ToString();
                if (reader["Zalo"] != DBNull.Value)
                    entity.Zalo = reader["Zalo"].ToString();
                if (reader["Facebook"] != DBNull.Value)
                    entity.Facebook = reader["Facebook"].ToString();
                if (reader["Nick"] != DBNull.Value)
                    entity.Nick = reader["Nick"].ToString();
                if (reader["Province"] != DBNull.Value)
                {
                    var provinceID = reader["Province"].ToString().ToInt();
                    var pro = ProvinceController.GetByID(provinceID);
                    if (pro != null)
                    {
                        entity.Province = pro.ProvinceName;
                    }
                    else
                    {
                        entity.Province = "";
                    }
                    
                }

                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public static tbl_Customer GetByID(int ID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Customer ai = dbe.tbl_Customer.Where(a => a.ID == ID).FirstOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }
        public static List<tbl_Customer> GetAll(string s)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_Customer> ags = new List<tbl_Customer>();
                ags = dbe.tbl_Customer.Where(c => c.CustomerName.Contains(s) || c.CustomerPhone.Contains(s) || c.Zalo.Contains(s) || c.Facebook.Contains(s)).ToList();
                return ags;
            }
        }

        public static List<CustomerOut> Get(string text, int Discount, string by, int Provice)
        {
            string textsearch = '"' + text + '"';
            var list = new List<CustomerOut>();
            var sql = @"select c.ID, c.CustomerName, c.Nick, c.CustomerPhone, c.Zalo, c.Facebook, c.CreatedBy, c.CreatedDate, dg.DiscountName, dg.ID as DiscountID, c.ProvinceID as Province
                        from tbl_DiscountCustomer dc, tbl_DiscountGroup dg, tbl_Customer c
                        where dc.DiscountGroupID = dg.ID and c.ID = dc.UID";
            if (!string.IsNullOrEmpty(textsearch))
            {
                sql += " And (CONTAINS(c.CustomerName,'" + textsearch + "')  OR CONTAINS(c.Nick,'" + textsearch + "') OR c.CustomerPhone like '%" + text + "%' OR c.Facebook like '%" + text + "%' OR c.Zalo like '%" + text + "%')";
            }
            if (Discount > 0)
            {
                sql += " AND dg.ID  = " + Discount;
            }
            if (Provice > 0)
            {
                sql += " AND c.ProvinceID  = " + Provice;
            }
            if (!string.IsNullOrEmpty(by))
            {
                sql += " And c.CreatedBy = N'" + by + "'";
            }
            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                var entity = new CustomerOut();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["CustomerName"] != DBNull.Value)
                    entity.CustomerName = reader["CustomerName"].ToString();
                if (reader["Nick"] != DBNull.Value)
                    entity.Nick = reader["Nick"].ToString();
                if (reader["CustomerPhone"] != DBNull.Value)
                    entity.CustomerPhone = reader["CustomerPhone"].ToString();
                if (reader["Zalo"] != DBNull.Value)
                    entity.Zalo = reader["Zalo"].ToString();
                if (reader["Facebook"] != DBNull.Value)
                    entity.Facebook = reader["Facebook"].ToString();
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                if (reader["CreatedBy"] != DBNull.Value)
                    entity.CreatedBy = reader["CreatedBy"].ToString();
                if (reader["DiscountName"] != DBNull.Value)
                    entity.DiscountName = reader["DiscountName"].ToString();
                if (reader["Province"] != DBNull.Value)
                    entity.Province = reader["Province"].ToString();
                list.Add(entity);
            }
            reader.Close();
            return list;
        }

        public static List<CustomerOut> Filter(string text, string by, int Provice, string CreatedDate)
        {
            string textsearch = '"' + text + '"';
            var list = new List<CustomerOut>();
            var sql = @"select c.ID, c.CustomerName, c.Nick, c.CustomerPhone, c.CustomerPhone2, c.Zalo, c.Facebook, c.CreatedBy, c.CreatedDate, c.Avatar, c.ShippingType, c.PaymentType, c.TransportCompanyID, c.TransportCompanySubID, c.ProvinceID as Province
                        from tbl_Customer c
                         WHERE 1 = 1";

            if (!string.IsNullOrEmpty(textsearch))
            {
                sql += " AND (CONTAINS(c.CustomerName,'" + textsearch + "') OR CONTAINS(c.Nick,'" + textsearch + "') OR c.CustomerPhone like '%" + text + "%' OR c.CustomerPhone2 like '%" + text + "%' OR c.CustomerPhoneBackup like '%" + text + "%' OR c.Facebook like '%" + text + "%' OR c.Zalo like '%" + text + "%')";
            }
           
            if (Provice > 0)
            {
                sql += " AND c.ProvinceID  = " + Provice;
            }

            if (!string.IsNullOrEmpty(by))
            {
                sql += " AND c.CreatedBy = N'" + by + "'";
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
                    case "month":
                        fromdate = new DateTime(fromdate.Year, fromdate.Month, 1);
                        todate = DateTime.Now;
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

                sql += "	AND	CONVERT(datetime, c.CreatedDate, 121) BETWEEN CONVERT(datetime, '" + fromdate.ToString() + "', 121) AND CONVERT(datetime, '" + todate.ToString() + "', 121)";
            }

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                var entity = new CustomerOut();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["CustomerName"] != DBNull.Value)
                    entity.CustomerName = reader["CustomerName"].ToString();
                if (reader["CustomerPhone"] != DBNull.Value)
                    entity.CustomerPhone = reader["CustomerPhone"].ToString();
                if (reader["CustomerPhone2"] != DBNull.Value)
                    entity.CustomerPhone2 = reader["CustomerPhone2"].ToString();
                if (reader["Zalo"] != DBNull.Value)
                    entity.Zalo = reader["Zalo"].ToString();
                if (reader["Facebook"] != DBNull.Value)
                    entity.Facebook = reader["Facebook"].ToString();
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                if (reader["CreatedBy"] != DBNull.Value)
                    entity.CreatedBy = reader["CreatedBy"].ToString();
                if (reader["Nick"] != DBNull.Value)
                    entity.Nick = reader["Nick"].ToString();
                if (reader["Province"] != DBNull.Value)
                    entity.Province = reader["Province"].ToString();
                if (!string.IsNullOrEmpty(reader["Avatar"].ToString()))
                {
                    entity.Avatar = reader["Avatar"].ToString();
                }
                else
                {
                    entity.Avatar = "/uploads/avatars/no-avatar.png";
                }
                if (reader["ShippingType"] != DBNull.Value)
                    entity.ShippingType = reader["ShippingType"].ToString().ToInt(0);
                if (reader["PaymentType"] != DBNull.Value)
                    entity.PaymentType = reader["PaymentType"].ToString().ToInt(0);
                if (reader["TransportCompanyID"] != DBNull.Value)
                    entity.TransportCompanyID = reader["TransportCompanyID"].ToString().ToInt(0);
                if (reader["TransportCompanySubID"] != DBNull.Value)
                    entity.TransportCompanySubID = reader["TransportCompanySubID"].ToString().ToInt(0);
                list.Add(entity);
            }

            reader.Close();
            return list;
        }
        public static List<CustomerGet> GetNotInGroupByGroupID(int GroupID)
        {
            var list = new List<CustomerGet>();
            var sql = @"SELECT  l.ID, l.CustomerName, l.CustomerPhone FROM tbl_Customer l";
            sql += " LEFT JOIN (Select UID, CustomerName from tbl_DiscountCustomer where DiscountGroupID = " + GroupID + " ) as r ON  r.UID = l.ID";
            sql += " WHERE r.UID IS NULL";

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                var entity = new CustomerGet();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["CustomerName"] != DBNull.Value)
                    entity.CustomerName = reader["CustomerName"].ToString();
                if (reader["CustomerPhone"] != DBNull.Value)
                    entity.CustomerPhone = reader["CustomerPhone"].ToString();
                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public static List<tbl_Customer> GetBylevelID(int LevelID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_Customer> ags = new List<tbl_Customer>();
                ags = dbe.tbl_Customer.Where(a => a.CustomerLevelID == LevelID).ToList();
                return ags;
            }
        }

        public static List<tbl_Customer> TotalCustomer(string fromdate, string todate)
        {
            using (var db = new inventorymanagementEntities())
            {
                List<tbl_Customer> or = new List<tbl_Customer>();
                if (!string.IsNullOrEmpty(fromdate))
                {
                    if (!string.IsNullOrEmpty(todate))
                    {
                        DateTime fd = Convert.ToDateTime(fromdate);
                        DateTime td = Convert.ToDateTime(todate);
                        or = db.tbl_Customer
                            .Where(r => r.CreatedDate >= fd && r.CreatedDate <= td)
                            .OrderByDescending(r => r.ID).ToList();
                    }
                    else
                    {
                        DateTime fd = Convert.ToDateTime(fromdate);
                        or = db.tbl_Customer
                            .Where(r => r.CreatedDate >= fd)
                            .OrderByDescending(r => r.ID).ToList();
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(todate))
                    {
                        DateTime td = Convert.ToDateTime(todate);
                        or = db.tbl_Customer
                            .Where(r => r.CreatedDate <= td)
                            .OrderByDescending(r => r.ID).ToList();
                    }
                    else
                    {
                        or = db.tbl_Customer.ToList();
                    }
                }
                return or;
            }
        }
        #endregion
        #region Class
        public class CustomerGet
        {
            public int ID { get; set; }
            public string CustomerName { get; set; }
            public string CustomerPhone { get; set; }
        }

        public class CustomerOut
        {
            public int ID { get; set; }
            public string CustomerName { get; set; }
            public string CustomerPhone { get; set; }
            public string CustomerAddress { get; set; }
            public string Zalo { get; set; }
            public string Facebook { get; set; }
            public string CreatedBy { get; set; }
            public string DiscountName { get; set; }
            public DateTime CreatedDate { get; set; }
            public string IsHidden { get; set; }
            public string Province { get; set; }

            public string DisID { get; set; }
            public string Nick { get; set; }
            public string Avatar { get; set; }
            public int ShippingType { get; set; }
            public int PaymentType { get; set; }
            public int TransportCompanyID { get; set; }
            public int TransportCompanySubID { get; set; }
            public string CustomerPhone2 { get; set; }
        }
        #endregion
    }
}