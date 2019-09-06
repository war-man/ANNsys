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
    public class RefundGoodDetailController
    {
        #region CRUD
        public static int Insert(int RefundGoodsID, int AgentID, int OrderID, string ProductName, int CustomerID, string SKU, double Quantity, double QuantityMax,
            string PriceNotFeeRefund, int ProductType, bool IsCount, int RefundType, string RefundFeePerProduct, string TotalRefundFee, string GiavonPerProduct,
            string DiscountPricePerProduct, string SoldPricePerProduct, string TotalPriceRow, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_RefundGoodsDetails a = new tbl_RefundGoodsDetails();
                a.RefundGoodsID = RefundGoodsID;
                a.AgentID = AgentID;
                a.OrderID = OrderID;
                a.ProductName = ProductName;
                a.CustomerID = CustomerID;
                a.SKU = SKU;
                a.Quantity = Quantity;
                a.QuantityMax = QuantityMax;
                a.PriceNotFeeRefund = PriceNotFeeRefund;
                a.ProductType = ProductType;
                a.IsCount = true;
                a.RefundType = RefundType;
                a.RefundFeePerProduct = RefundFeePerProduct;
                a.TotalRefundFee = TotalRefundFee;
                a.GiavonPerProduct = GiavonPerProduct;
                a.DiscountPricePerProduct = DiscountPricePerProduct;
                a.SoldPricePerProduct = SoldPricePerProduct;
                a.TotalPriceRow = TotalPriceRow;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                dbe.tbl_RefundGoodsDetails.Add(a);
                dbe.SaveChanges();
                int kq = a.ID;
                return kq;
            }
        }

        public static int Insert(tbl_RefundGoodsDetails refundDetail)
        {
            using (var con = new inventorymanagementEntities())
            {
                con.tbl_RefundGoodsDetails.Add(refundDetail);
                con.SaveChanges();
                int kq = refundDetail.ID;
                return kq;
            }
        }

        public static bool DeleteByRefundGoodsID(int RefundGoodsID)
        {
            using (var con = new inventorymanagementEntities())
            {
                var listDelete = con.tbl_RefundGoodsDetails.Where(x => x.RefundGoodsID == RefundGoodsID);

                if (listDelete != null)
                {
                    con.tbl_RefundGoodsDetails.RemoveRange(listDelete);
                    con.SaveChanges();
                    return true;
                }

                return false;
            }
        }
        #endregion
        #region Select        
        public static List<tbl_RefundGoodsDetails> GetAll(string s)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_RefundGoodsDetails> las = new List<tbl_RefundGoodsDetails>();
                las = dbe.tbl_RefundGoodsDetails.OrderByDescending(r => r.CreatedDate).ToList();
                return las;
            }
        }
        public static List<tbl_RefundGoodsDetails> GetByRefundGoodsID(int RefundGoodsID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_RefundGoodsDetails> las = new List<tbl_RefundGoodsDetails>();
                las = dbe.tbl_RefundGoodsDetails.Where(r => r.RefundGoodsID == RefundGoodsID).OrderByDescending(r => r.CreatedDate).ToList();
                return las;
            }
        }

        public static List<RefundDetailModel> GetInfoShowRefundDetail(int RefundGoodsID, double FeeRefundDefault)
        {
            using (var con = new inventorymanagementEntities())
            {

                var products = con.tbl_Product
                    .GroupJoin(
                        con.tbl_ProductVariable,
                        product => product.ID,
                        variable => variable.ProductID,
                        (product, variable) => new { product, variable })
                    .SelectMany(x => x.variable.DefaultIfEmpty(),
                                (parent, child) => new
                                {
                                    ProductID = parent.product.ID,
                                    ProductVariableID = parent.product.ProductStyle == 2 ? child.ID : 0,
                                    ProductStyle = parent.product.ProductStyle,
                                    ProductImage = parent.product.ProductStyle == 2 ? child.Image : parent.product.ProductImage,
                                    ProductTitle = parent.product.ProductTitle,
                                    ParentSKU = parent.product.ProductSKU,
                                    ChildSKU = parent.product.ProductStyle == 2 ? child.SKU : String.Empty
                                });

                var refundDetail = con.tbl_RefundGoodsDetails
                            .Where(x => x.RefundGoodsID == RefundGoodsID)
                            .OrderByDescending(r => r.CreatedDate)
                            .Join(
                                products,
                                refund => refund.SKU,
                                product => product.ProductStyle == 2 ? product.ChildSKU : product.ParentSKU,
                                (refund, product) => new
                                {
                                    RefundGoodsID = refund.RefundGoodsID.Value,
                                    RefundDetailID = refund.ID,
                                    OrderID = refund.OrderID.HasValue? refund.OrderID.Value : 0,
                                    ProductID = product.ProductID,
                                    ProductVariableID = product.ProductVariableID,
                                    ProductStyle = product.ProductStyle.Value,
                                    ProductImage = product.ProductImage,
                                    ProductTitle = product.ProductTitle,
                                    ParentSKU = product.ParentSKU,
                                    ChildSKU = product.ChildSKU,
                                    Price = refund.GiavonPerProduct,
                                    ReducedPrice = refund.SoldPricePerProduct,
                                    QuantityRefund = refund.Quantity.Value,
                                    ChangeType = refund.RefundType.Value,
                                    FeeRefund = refund.RefundFeePerProduct,
                                    TotalFeeRefund = refund.TotalPriceRow
                                }
                            )
                            .ToList();

                return refundDetail.Select(x => new RefundDetailModel
                            {
                                RefundGoodsID = x.RefundGoodsID,
                                RefundDetailID = x.RefundDetailID,
                                OrderID = x.OrderID,
                                ProductID = x.ProductID,
                                ProductVariableID = x.ProductVariableID,
                                ProductStyle = x.ProductStyle,
                                ProductImage = x.ProductImage,
                                ProductTitle = x.ProductTitle,
                                ParentSKU = x.ParentSKU,
                                ChildSKU = x.ChildSKU,
                                Price = Convert.ToDouble(x.Price),
                                ReducedPrice = Convert.ToDouble(x.ReducedPrice),
                                QuantityRefund = x.QuantityRefund,
                                ChangeType = x.ChangeType,
                                FeeRefund = Convert.ToDouble(x.FeeRefund),
                                FeeRefundDefault = FeeRefundDefault,
                                TotalFeeRefund = Convert.ToDouble(x.TotalFeeRefund)
                            })
                            .ToList();
            }
        }
        public static List<tbl_RefundGoodsDetails> GetQuantityMax(int CustomerID, int OrderID, string SKU)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_RefundGoodsDetails> las = new List<tbl_RefundGoodsDetails>();
                las = dbe.tbl_RefundGoodsDetails.Where(x => x.CustomerID == CustomerID && x.OrderID == OrderID && x.SKU == SKU).ToList();
                return las;
            }
        }

      
        #endregion
    }
}