using IM_PJ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Controllers
{
    public class DeliveryPostOfficeController
    {
        public static void reviewOrder(List<DeliveryPostOffice> postOffices)
        {
            using (var con = new inventorymanagementEntities())
            {
                var toDate = postOffices.Max(x => x.StartDate);
                var fromDate = postOffices.Min(x => x.StartDate).AddDays(-30); // Trừ cho 7 ngày đề phòng đơn tạo lâu chưa giao đi

                foreach (var item in postOffices)
                {
                    var order = con.tbl_Order
                        .Where(x => x.CreatedDate >= new DateTime(2019, 2, 15))
                        .Where(x => x.ExcuteStatus == (int)ExcuteStatus.Done || x.ExcuteStatus == (int)ExcuteStatus.Return)
                        .Where(x => x.ID == item.OrderID || x.ShippingCode == item.NumberID)
                        .FirstOrDefault();

                    if (order == null)
                    {
                        item.OrderStatus = (int)OrderStatus.NoExist;
                        continue;
                    }

                    // Các đơn có trạng thái hủy buộc phải kiểm tra bằng tay
                    if (item.DeliveryStatus == "Hủy")
                    {
                        item.OrderStatus = (int)OrderStatus.Spam;
                        continue;
                    }

                    if (item.DeliveryStatus == "Trả hàng thành công")
                    {
                        item.Review = (int)DeliveryPostOfficeReview.Approve;
                        order.PaymentStatus = (int)PaymentStatus.Approve;
                        continue;
                    }


                    // Lấy thông tin của order
                    if (item.OrderID == 0)
                        item.OrderID = order.ID;
                    var refound = con.tbl_RefundGoods
                        .Where(x => x.ID == order.RefundsGoodsID)
                        .FirstOrDefault();

                    // Trường hợp là thu hộ thì sẽ cập nhập số tiền thu hộ của đơn hàng
                    if (order.PaymentType == (int)PaymentType.CashCollection)
                    {
                        // Số tiền thu hộ bằng tổng tiền đơn hàng trừ đi hàng đổi trả
                        item.OrderCOD = Convert.ToDecimal(order.TotalPrice);
                        if (refound != null)
                            item.OrderCOD = item.OrderCOD - Convert.ToDecimal(refound.TotalPrice);
                        if (item.OrderCOD < 0)
                            item.OrderCOD = 0;
                    }
                    else
                    {
                        item.OrderCOD = 0;
                    }
                    item.OrderFee = Convert.ToDecimal(order.FeeShipping);
                    item.Staff = order.CreatedBy;

                    if (order.PaymentType != (int)PaymentType.CashCollection)
                    {
                        item.Review = (int)DeliveryPostOfficeReview.Approve;
                        order.PaymentStatus = (int)PaymentStatus.Approve;
                    }
                    else if (item.COD == item.OrderCOD)
                    {
                        item.Review = (int)DeliveryPostOfficeReview.Approve;
                        order.PaymentStatus = (int)PaymentStatus.Approve;
                    }
                }

                // Kiểm tra xem dữ liệu có tồn tại trong databse không nêu không thì insert
                foreach (var item in postOffices)
                {
                    var exist = con.DeliveryPostOffices
                        .Where(x => x.OrderID == item.OrderID && x.NumberID == item.NumberID);

                    if (exist.Count() == 0)
                        con.DeliveryPostOffices.Add(item);
                }

                // Thực thi commit
                con.SaveChanges();
            }
        }

        public static List<DeliveryPostOffice> Filter(DeliveryPostOfficeFilterModel filter,
                                                      ref PaginationMetadataModel page,
                                                      ref decimal lossMoney)
        {
            using (var con = new inventorymanagementEntities())
            {
                var postOffices = con.DeliveryPostOffices.Where(x => 1 == 1);

                #region Thực hiện filter dữ liệu
                #region Filter theo từ khóa
                if (!String.IsNullOrEmpty(filter.search))
                {
                    postOffices = postOffices.Where(x =>
                        x.OrderID.ToString() == filter.search ||
                        x.NumberID == filter.search
                    );
                }
                #endregion
                #region Filter theo trạng thái đơn hàng
                if (filter.orderStatus > 0)
                {
                    postOffices = postOffices
                        .Where(x => x.OrderStatus == filter.orderStatus);
                }
                #endregion
                #region Filter theo trạng thái của bưu điện
                if (!String.IsNullOrEmpty(filter.deliveryStatus))
                {
                    if (filter.deliveryStatus == "Không lấy đơn hủy")
                        postOffices = postOffices
                        .Where(x => x.DeliveryStatus != "Hủy");
                    else
                        postOffices = postOffices
                            .Where(x => x.DeliveryStatus == filter.deliveryStatus);
                }
                #endregion
                #region Filter theo trạng thái xử lý
                if (filter.review > 0)
                {
                    postOffices = postOffices
                        .Where(x => x.Review == filter.review);
                }
                #endregion
                #region Filter theo nhân viên tạo đơn hàng
                if (filter.feeStatus > 0)
                {
                    if (filter.feeStatus == (int)PostOfficeFeeStatus.Profitable)
                        postOffices = postOffices
                            .Where(x => x.DeliveryStatus != "Mới tạo")
                            .Where(x => x.OrderFee >= x.Fee);
                    if (filter.feeStatus == (int)PostOfficeFeeStatus.Losses)
                        postOffices = postOffices
                            .Where(x => x.DeliveryStatus != "Mới tạo")
                            .Where(x => x.OrderFee < x.Fee);
                }
                #endregion
                #region Filter theo ngày tạo đơn
                // Filter Order Created Date
                if (filter.orderFromDate.HasValue && filter.orderToDate.HasValue)
                {
                    postOffices = postOffices
                        .Where(x => x.StartDate >= filter.orderFromDate)
                        .Where(x => x.StartDate <= filter.orderToDate);
                }
                #endregion
                #endregion

                #region Tính toán phân trang
                // Tính toán báo cáo
                if(postOffices.Count() > 0)
                    lossMoney = postOffices
                        .Where(x => x.DeliveryStatus != "Mới tạo")
                        .Sum(x => x.OrderFee - x.Fee);

                // Calculate pagination
                page.totalCount = postOffices.Count();
                page.totalPages = (int)Math.Ceiling(page.totalCount / (double)page.pageSize);

                postOffices = postOffices
                    .OrderByDescending(x => x.StartDate)
                    .Skip((page.currentPage - 1) * page.pageSize)
                    .Take(page.pageSize);
                #endregion

                return postOffices.ToList();
            }
        }

        public static string approve(int postOfficeID, int orderID)
        {
            using (var con = new inventorymanagementEntities())
            {
                var postOffice = con.DeliveryPostOffices.Where(x => x.ID == postOfficeID).FirstOrDefault();
                var order = con.tbl_Order.Where(x => x.ID == orderID).FirstOrDefault();

                if (postOffice != null && order != null)
                {
                    postOffice.Review = (int)DeliveryPostOfficeReview.Approve;
                    postOffice.OrderStatus = (int)OrderStatus.Exist;
                    order.PaymentStatus = (int)PaymentStatus.Approve;

                    // Trường hơp không có order và mã vận đơn của hệ thống khác với bưu điện
                    if (postOffice.OrderID == 0)
                    {
                        postOffice.OrderID = orderID;
                        postOffice.OrderCOD = Convert.ToDecimal(order.TotalPrice);
                        if (order.RefundsGoodsID != 0)
                        {
                            var refund = con.tbl_RefundGoods.Where(x => x.ID == order.RefundsGoodsID).FirstOrDefault();
                            if (refund != null)
                            {
                                postOffice.OrderCOD = postOffice.OrderCOD - Convert.ToDecimal(refund.TotalPrice);
                            }
                        }
                        postOffice.Fee = Convert.ToDecimal(order.FeeShipping);
                        order.ShippingCode = postOffice.NumberID;
                    }
                    con.SaveChanges();

                    return "success";
                }
                else if(order == null)
                {
                    return "notfoundOrder";
                }
            }

            return "false";
        }

        public static void cancel(int postOfficeID)
        {
            using (var con = new inventorymanagementEntities())
            {
                var postOffice = con.DeliveryPostOffices.Where(x => x.ID == postOfficeID).FirstOrDefault();

                if (postOffice != null)
                {
                    postOffice.OrderStatus = (int)OrderStatus.Spam;
                    con.SaveChanges();
                }
            }
        }
    }
}