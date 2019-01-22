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
    public class DiscountCustomerController
    {
        #region CRUD
        public static string Insert(int DiscountGroupID, int UID, string CustomerName, string customerPhone, bool IsHidden, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_DiscountCustomer ui = new tbl_DiscountCustomer();
                ui.DiscountGroupID = DiscountGroupID;
                ui.UID = UID;
                ui.CustomerName = CustomerName;
                ui.CustomerPhone = customerPhone;
                ui.IsHidden = IsHidden;
                ui.CreatedDate = CreatedDate;
                ui.CreatedBy = CreatedBy;
                dbe.tbl_DiscountCustomer.Add(ui);
                dbe.SaveChanges();
                int kq = ui.ID;
                return kq.ToString();
            }
        }
        public static string UpdateIsHidden(int ID, bool IsHidden, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_DiscountCustomer ui = dbe.tbl_DiscountCustomer.Where(a => a.ID == ID).SingleOrDefault();
                if (ui != null)
                {
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
        public static string Delete(int ID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_DiscountCustomer ui = dbe.tbl_DiscountCustomer.Where(a => a.ID == ID).SingleOrDefault();
                if (ui != null)
                {
                    dbe.tbl_DiscountCustomer.Remove(ui);
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select        
        public static tbl_DiscountCustomer GetByID(int ID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_DiscountCustomer ai = dbe.tbl_DiscountCustomer.Where(a => a.ID == ID).FirstOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }
        public static List<tbl_DiscountCustomer> GetAll(string s)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_DiscountCustomer> ags = new List<tbl_DiscountCustomer>();
                ags = dbe.tbl_DiscountCustomer.Where(c => c.CustomerName.Contains(s)).ToList();
                return ags;
            }
        }
        public static List<DiscountCustomerGet> getbyCustID(int CustID)
        {
            List<DiscountCustomerGet> list = new List<DiscountCustomerGet>();
            var sql = @"select r.discountgroupid as DiscountGroupID,r.uid as CustID,r.customername as CustName,d.discountName as DiscountName, d.discountamount as DiscountAmount, d.DiscountAmountPercent as DiscountAmountPercent, d.FeeRefund, d.NumOfDateToChangeProduct, d.NumOfProductCanChange from tbl_DiscountCustomer as r";
            sql += " left join tbl_DiscountGroup as d on d.ID = r.DiscountGroupID";
            sql += " where r.UID = " + CustID + " and r.IsHidden = 0";
            sql += "order by d.discountamount desc";

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            while (reader.Read())
            {
                var entity = new DiscountCustomerGet();
                if (reader["DiscountGroupID"] != DBNull.Value)
                    entity.DiscountGroupID = reader["DiscountGroupID"].ToString().ToInt(0);
                if (reader["CustID"] != DBNull.Value)
                    entity.CustID = reader["CustID"].ToString().ToInt(0);
                if (reader["DiscountName"] != DBNull.Value)
                    entity.DiscountName = reader["DiscountName"].ToString();
                if (reader["CustName"] != DBNull.Value)
                    entity.CustName = reader["CustName"].ToString();
                if (reader["DiscountAmount"] != DBNull.Value)
                    entity.DiscountAmount = Convert.ToDouble(reader["DiscountAmount"].ToString().ToFloat(0));
                if (reader["DiscountAmountPercent"] != DBNull.Value)
                    entity.DiscountAmountPercent = Convert.ToDouble(reader["DiscountAmountPercent"].ToString().ToFloat(0));

                if (reader["FeeRefund"] != DBNull.Value)
                    entity.FeeRefund = Convert.ToDouble(reader["FeeRefund"].ToString().ToFloat(0));
                if (reader["NumOfDateToChangeProduct"] != DBNull.Value)
                    entity.NumOfDateToChangeProduct = Convert.ToDouble(reader["NumOfDateToChangeProduct"].ToString().ToFloat(0));
                if (reader["NumOfProductCanChange"] != DBNull.Value)
                    entity.NumOfProductCanChange = Convert.ToDouble(reader["NumOfProductCanChange"].ToString().ToFloat(0));
                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public static List<tbl_DiscountCustomer> GetByGroupID(int DiscountGroupID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_DiscountCustomer> ags = new List<tbl_DiscountCustomer>();
                ags = dbe.tbl_DiscountCustomer.Where(c => c.DiscountGroupID == DiscountGroupID).ToList();
                return ags;
            }
        }
        #endregion
        public class DiscountCustomerGet
        {
            public int DiscountGroupID { get; set; }
            public int CustID { get; set; }
            public string DiscountName { get; set; }
            public string CustName { get; set; }
            public double DiscountAmount { get; set; }
            public double DiscountAmountPercent { get; set; }
            public double FeeRefund { get; set; }
            public double NumOfDateToChangeProduct { get; set; }
            public double NumOfProductCanChange { get; set; }
        }
    }
}