using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IM_PJ.Models;

namespace IM_PJ.Controllers
{
    public class DiscountController
    {
        public static tbl_Discount insert(int Quantity, int DiscountPerProduct, string CreatedBy)
        {
            using (var db = new inventorymanagementEntities())
            {
                tbl_Discount dc = new tbl_Discount();
                dc.Quantity = Quantity;
                dc.DiscountPerProduct = DiscountPerProduct;
                dc.CreatedBy = CreatedBy;
                dc.CreatedDate = DateTime.Now;
                dc.IsHidden = false;
                db.tbl_Discount.Add(dc);
                db.SaveChanges();
                return dc;
            }
        }

        public static int update(int ID, int Quantity, int DiscountPerProduct, string CreatedBy)
        {
            using (var db = new inventorymanagementEntities())
            {
                var dc = db.tbl_Discount.Where(x => x.ID == ID).FirstOrDefault();
                if (dc != null)
                {
                    dc.Quantity = Quantity;
                    dc.DiscountPerProduct = DiscountPerProduct;
                    dc.ModifiedBy = CreatedBy;
                    dc.ModifiedDate = DateTime.Now;
                    int i = db.SaveChanges();
                    return i;
                }
                return 0;
            }
        }
        public static List<tbl_Discount> GetAll()
        {
            using (var db = new inventorymanagementEntities())
            {
                var dc = db.tbl_Discount.Where(x => x.IsHidden == false).OrderBy(x=>x.Quantity).ToList();
                if (dc.Count() > 0)
                    return dc;
                return null;
            }
        }

        public static tbl_Discount GetByID(int ID)
        {
            using (var db = new inventorymanagementEntities())
            {
                var dc = db.tbl_Discount.Where(x => x.ID == ID).FirstOrDefault();
                if (dc != null)
                    return dc;
                return null;
            }
        }
    }
}