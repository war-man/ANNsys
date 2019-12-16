using IM_PJ.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace IM_PJ.Controllers
{
    public class CouponController
    {
        public static string getCoupon(int customerID, string code, int productNumber, decimal price)
        {
            using (var con = new inventorymanagementEntities())
            {
                var now = DateTime.Now;
                var coupon = con.Coupons
                    .Where(x => x.Code.Trim().ToUpper() == code.Trim().ToUpper())
                    .OrderByDescending(x => x.Value)
                    .FirstOrDefault();

                if (coupon != null)
                {
                    if (!(now.Date >= coupon.CreatedDate.Date && now.Date <= coupon.EndDate.Date))
                        return JsonConvert.SerializeObject(new
                        {
                            status = false,
                            message = String.Format("Mã {0} hết hiệu lực ({1:dd/MM/yyyy} - {2:dd/MM/yyyy})", code, coupon.CreatedDate, coupon.EndDate),
                            couponID = 0,
                            value = 0,
                            productNumber = 0,
                            priceMin = 0
                        });

                    if (!coupon.Active)
                        return JsonConvert.SerializeObject(new {
                            status = false,
                            message = String.Format("Mã {0} hết hiệu lực", code),
                            couponID = 0,
                            value = 0,
                            productNumber = 0,
                            priceMin = 0
                        });

                    if (!(productNumber >= coupon.ProductNumber && price >= coupon.PriceMin))
                        return JsonConvert.SerializeObject(new
                        {
                            status = false,
                            message = String.Format("Mã {0} chỉ áp dụng với đơn hàng số lượng > {1:N0} và trị giá > {2:N0}", code, coupon.ProductNumber, coupon.PriceMin),
                            couponID = 0,
                            value = 0,
                            productNumber = 0,
                            priceMin = 0
                        });

                    var customerCouponList = con.CustomerCoupons
                        .Where(x => x.CustomerID == customerID)
                        .Where(x => x.CouponID == coupon.ID)
                        .ToList();

                    if (customerCouponList.Count == 0)
                    {
                        var textInfo = new CultureInfo("vi-VN", false).TextInfo;
                        var user = CustomerController.GetByID(customerID);

                        return JsonConvert.SerializeObject(new
                        {
                            status = false,
                            message = String.Format("Mã {0} không áp dụng cho khách hàng {1}", code, textInfo.ToTitleCase(user.CustomerName)),
                            couponID = 0,
                            value = 0,
                            productNumber = 0,
                            priceMin = 0
                        });
                    }

                    var customerCouponActives = customerCouponList.Where(x => x.Active == true).ToList();

                    if (customerCouponActives.Count == 0)
                    {
                        var textInfo = new CultureInfo("vi-VN", false).TextInfo;
                        var user = CustomerController.GetByID(customerID);

                        if (customerCouponList.Count > 1)
                            return JsonConvert.SerializeObject(new
                            {
                                status = false,
                                message = String.Format("Mã {0} đã được khách hàng {1} sử dụng {2} lần", code, textInfo.ToTitleCase(user.CustomerName), customerCouponList.Count),
                                couponID = 0,
                                value = 0,
                                productNumber = 0,
                                priceMin = 0
                            });
                        else
                            return JsonConvert.SerializeObject(new
                            {
                                status = false,
                                message = String.Format("Khách hàng {0} đã sử dụng mã {1}", textInfo.ToTitleCase(user.CustomerName), code),
                                couponID = 0,
                                value = 0,
                                productNumber = 0,
                                priceMin = 0
                            });
                    }
                    else
                    {
                        var customerCoupon = customerCouponActives.OrderByDescending(o => o.ID).FirstOrDefault();

                        if (!(now.Date >= customerCoupon.StartDate.Date && now.Date <= customerCoupon.EndDate.Date))
                        {
                            var textInfo = new CultureInfo("vi-VN", false).TextInfo;
                            var user = CustomerController.GetByID(customerID);

                            return JsonConvert.SerializeObject(new
                            {
                                status = false,
                                message = String.Format("Khách hàng {0} sử dụng mã {1} không đúng thời gian ({2:dd/MM/yyyy} - {3:dd/MM/yyyy})", textInfo.ToTitleCase(user.CustomerName), code, customerCoupon.StartDate, customerCoupon.EndDate),
                                couponID = 0,
                                value = 0,
                                productNumber = 0,
                                priceMin = 0
                            });
                        }
                    }

                    return JsonConvert.SerializeObject(new
                    {
                        status = true,
                        message = String.Empty,
                        couponID = coupon.ID,
                        value = coupon.Value,
                        productNumber = coupon.ProductNumber,
                        priceMin = coupon.PriceMin
                    });
                }

                return JsonConvert.SerializeObject(new {
                    status = false,
                    message = String.Format("Mã {0} không tồn tại", code),
                    couponID = 0,
                    value = 0,
                    productNumber = 0,
                    priceMin = 0
                });
            }
        }

        public static Coupon getCoupon(int id)
        {
            using (var con = new inventorymanagementEntities())
            {
                return con.Coupons.Where(x => x.ID == id).FirstOrDefault();
            }
        }

        public static CustomerCoupon updateStatusCouponCustomer(int customerID, int couponID, bool active)
        {
            using (var con = new inventorymanagementEntities())
            {
                var customerCoupon = con.CustomerCoupons
                    .Where(x => x.CustomerID == customerID)
                    .Where(x => x.CouponID == couponID)
                    .FirstOrDefault();

                if (customerCoupon == null)
                    return null;

                customerCoupon.Active = active;
                con.SaveChanges();

                return customerCoupon;
            }
        }
    }
}