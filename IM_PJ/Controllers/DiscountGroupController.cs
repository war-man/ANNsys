using IM_PJ.Models;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Controllers
{
    public class DiscountGroupController
    {
        #region CRUD
        public static string Insert(tbl_DiscountGroup data)
        {
            using (var con = new inventorymanagementEntities())
            {
                con.tbl_DiscountGroup.Add(data);
                con.SaveChanges();

                return data.ID.ToString();
            }
        }
        public static string Update(tbl_DiscountGroup data)
        {
            using (var con = new inventorymanagementEntities())
            {
                var discount = con.tbl_DiscountGroup
                    .Where(a => a.ID == data.ID)
                    .FirstOrDefault();

                if (discount != null)
                {
                    discount.DiscountName = data.DiscountName;
                    discount.DiscountAmount = data.DiscountAmount;
                    discount.QuantityProduct = data.QuantityProduct;
                    discount.DiscountAmountPercent = data.DiscountAmountPercent;
                    discount.FeeRefund = data.FeeRefund;
                    discount.NumOfDateToChangeProduct = data.NumOfDateToChangeProduct;
                    discount.NumOfProductCanChange = data.NumOfProductCanChange;
                    discount.RefundQuantityNoFee = data.RefundQuantityNoFee;
                    discount.DiscountNote = data.DiscountNote;
                    discount.IsHidden = data.IsHidden;
                    discount.ModifiedBy = data.ModifiedBy;
                    discount.ModifiedDate = data.ModifiedDate;
                    int kq = con.SaveChanges();

                    return kq.ToString();
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static tbl_DiscountGroup GetByID(int ID)
        {
            using (var con = new inventorymanagementEntities())
            {
                var discountGroup = con.tbl_DiscountGroup
                    .Where(a => a.ID == ID)
                    .FirstOrDefault();

                return discountGroup;
            }
        }
        public static List<tbl_DiscountGroup> GetAll(string s)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                var ags = dbe.tbl_DiscountGroup
                    .Where(c => c.DiscountName.Contains(s) || c.DiscountAmountPercent.ToString().Contains(s))
                    .OrderByDescending(x => x.DiscountAmount)
                    .ThenByDescending(x => x.QuantityProduct)
                    .ThenByDescending(x => x.FeeRefund)
                    .ToList();

                return ags;
            }
        }

     
        #endregion
    }
}