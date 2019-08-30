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
                tbl_DiscountCustomer ui = dbe.tbl_DiscountCustomer.Where(a => a.UID == ID).SingleOrDefault();
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
                tbl_DiscountCustomer ai = dbe.tbl_DiscountCustomer.Where(a => a.UID == ID).FirstOrDefault();
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
            using (var con = new inventorymanagementEntities())
            {
                #region Filter khách hàng
                var customer = con.tbl_DiscountCustomer
                    .Where(x => x.UID == CustID)
                    .Where(x => x.IsHidden.HasValue && x.IsHidden.Value == false)
                    .OrderBy(o => o.DiscountGroupID);
                #endregion

                #region Lấy thông tin nhóm triết khấu
                var discount = con.tbl_DiscountGroup
                    .Join(
                        customer.Where(x => x.DiscountGroupID.HasValue),
                        d => d.ID,
                        c => c.DiscountGroupID.Value,
                        (d, c) => d
                    )
                    .OrderByDescending(o => o.DiscountAmount);
                #endregion

                var data = customer
                    .GroupJoin(
                        discount,
                        c => c.DiscountGroupID,
                        d => d.ID,
                        (c, d) => new { customer = c, discount = d }
                    )
                    .SelectMany(
                        x => x.discount.DefaultIfEmpty(),
                        (parent, child) => new
                        {
                            DiscountGroupID = child != null ? child.ID : 0,
                            DiscountName = child != null ? child.DiscountName : "",
                            CustID = parent.customer.ID,
                            CustName = parent.customer.CustomerName,
                            DiscountAmount = child != null ? (child.DiscountAmount.HasValue ? child.DiscountAmount.Value : 0) : 0,
                            QuantityProduct = child != null ? (child.QuantityProduct.HasValue ? child.QuantityProduct.Value : 0) : 0,
                            DiscountAmountPercent = child != null ? (child.DiscountAmountPercent.HasValue ? child.DiscountAmountPercent.Value : 0) : 0,
                            FeeRefund = child != null ? (child.FeeRefund.HasValue ? child.FeeRefund.Value : 0) : 0,
                            NumOfDateToChangeProduct = child != null ? (child.NumOfDateToChangeProduct.HasValue ? child.NumOfDateToChangeProduct.Value : 0) : 0,
                            NumOfProductCanChange = child != null ? (child.NumOfProductCanChange.HasValue ? child.NumOfProductCanChange.Value : 0) : 0,
                            RefundQuantityNoFee = child != null ? (child.RefundQuantityNoFee.HasValue ? child.RefundQuantityNoFee.Value : 0) : 0,
                        }
                    )
                    .ToList();

                return data
                    .Select(x => new DiscountCustomerGet()
                    {
                        DiscountGroupID = x.DiscountGroupID,
                        DiscountName = x.DiscountName,
                        CustID = x.CustID,
                        CustName = x.CustName,
                        DiscountAmount = x.DiscountAmount,
                        QuantityProduct = x.QuantityProduct,
                        DiscountAmountPercent = x.DiscountAmountPercent,
                        FeeRefund = x.FeeRefund,
                        NumOfDateToChangeProduct = x.NumOfDateToChangeProduct,
                        NumOfProductCanChange = x.NumOfProductCanChange,
                        RefundQuantityNoFee = x.RefundQuantityNoFee,
                    })
                    .OrderByDescending(o => o.DiscountAmount)
                    .ToList();
            }
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
            public int QuantityProduct { get; set; }
            public double DiscountAmountPercent { get; set; }
            public double FeeRefund { get; set; }
            public double NumOfDateToChangeProduct { get; set; }
            public double NumOfProductCanChange { get; set; }
            public int RefundQuantityNoFee { get; set; }
        }
    }
}