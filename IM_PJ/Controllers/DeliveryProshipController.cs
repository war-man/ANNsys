using IM_PJ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Controllers
{
    public class DeliveryProshipController
    {
        public static void reviewOrder(List<DeliveryProship> proships)
        {
            using (var con = new inventorymanagementEntities())
            {
                var toDate = proships.Max(x => x.StartDate);
                var fromDate = proships.Min(x => x.StartDate).AddDays(-30); // Trừ cho 7 ngày đề phòng đơn tạo lâu chưa giao đi

                foreach (var item in proships)
                {
                    var order = con.tbl_Order
                        .Where(x => x.CreatedDate >= new DateTime(2019, 12, 15))
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
                        item.Review = (int)DeliveryProshipReview.Approve;
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
                        item.Review = (int)DeliveryProshipReview.Approve;
                        order.PaymentStatus = (int)PaymentStatus.Approve;
                    }
                    else if (item.COD == item.OrderCOD)
                    {
                        item.Review = (int)DeliveryProshipReview.Approve;
                        order.PaymentStatus = (int)PaymentStatus.Approve;
                    }
                }

                // Kiểm tra xem dữ liệu có tồn tại trong databse không nêu không thì insert
                foreach (var item in proships)
                {
                    var exist = con.DeliveryProships
                        .Where(x => x.OrderID == item.OrderID && x.NumberID == item.NumberID);

                    if (exist.Count() == 0)
                        con.DeliveryProships.Add(item);
                }

                // Thực thi commit
                con.SaveChanges();
            }
        }

        public static List<DeliveryProship> Filter(DeliveryProshipFilterModel filter,
                                                      ref PaginationMetadataModel page,
                                                      ref decimal lossMoney)
        {
            using (var con = new inventorymanagementEntities())
            {
                var proships = con.DeliveryProships.Where(x => 1 == 1);

                #region Thực hiện filter dữ liệu
                #region Filter theo từ khóa
                if (!String.IsNullOrEmpty(filter.search))
                {
                    proships = proships.Where(x =>
                        x.OrderID.ToString() == filter.search ||
                        x.NumberID == filter.search
                    );
                }
                #endregion
                #region Filter theo trạng thái đơn hàng
                if (filter.orderStatus > 0)
                {
                    proships = proships
                        .Where(x => x.OrderStatus == filter.orderStatus);
                }
                #endregion
                #region Filter theo trạng thái của bưu điện
                if (!String.IsNullOrEmpty(filter.deliveryStatus))
                {
                    if (filter.deliveryStatus == "Không lấy đơn hủy")
                        proships = proships
                        .Where(x => x.DeliveryStatus != "Hủy");
                    else
                        proships = proships
                            .Where(x => x.DeliveryStatus == filter.deliveryStatus);
                }
                #endregion
                #region Filter theo trạng thái xử lý
                if (filter.review > 0)
                {
                    proships = proships
                        .Where(x => x.Review == filter.review);
                }
                #endregion
                #region Filter theo nhân viên tạo đơn hàng
                if (filter.feeStatus > 0)
                {
                    if (filter.feeStatus == (int)ProshipFeeStatus.Profitable)
                        proships = proships
                            .Where(x => x.DeliveryStatus != "Mới tạo")
                            .Where(x => x.OrderFee >= x.Fee);
                    if (filter.feeStatus == (int)ProshipFeeStatus.Losses)
                        proships = proships
                            .Where(x => x.DeliveryStatus != "Mới tạo")
                            .Where(x => x.OrderFee < x.Fee);
                }
                #endregion
                #region Filter theo ngày tạo đơn
                // Filter Order Created Date
                if (filter.orderFromDate.HasValue && filter.orderToDate.HasValue)
                {
                    proships = proships
                        .Where(x => x.StartDate >= filter.orderFromDate)
                        .Where(x => x.StartDate <= filter.orderToDate);
                }
                #endregion
                #endregion

                #region Tính toán phân trang
                // Tính toán báo cáo
                if(proships.Count() > 0)
                    lossMoney = proships
                        .Where(x => x.DeliveryStatus != "Mới tạo")
                        .Sum(x => x.OrderFee - x.Fee);

                // Calculate pagination
                page.totalCount = proships.Count();
                page.totalPages = (int)Math.Ceiling(page.totalCount / (double)page.pageSize);

                proships = proships
                    .OrderByDescending(x => x.StartDate)
                    .Skip((page.currentPage - 1) * page.pageSize)
                    .Take(page.pageSize);
                #endregion

                return proships.ToList();
            }
        }

        public static void approve(int postOfficeID, int orderID)
        {
            using (var con = new inventorymanagementEntities())
            {
                var postOffice = con.DeliveryProships.Where(x => x.ID == postOfficeID).FirstOrDefault();
                var order = con.tbl_Order.Where(x => x.ID == orderID).FirstOrDefault();

                if (postOffice != null && order != null)
                {
                    postOffice.Review = (int)DeliveryProshipReview.Approve;
                    order.PaymentStatus = (int)PaymentStatus.Approve;

                    // Trường hơp không có order và mã vận đơn của hệ thống khác với bưu điện
                    if (postOffice.OrderID == 0)
                    {
                        postOffice.OrderID = orderID;
                        order.ShippingCode = postOffice.NumberID;
                    }

                    con.SaveChanges();
                }
            }
        }

        public static void cancel(int postOfficeID)
        {
            using (var con = new inventorymanagementEntities())
            {
                var postOffice = con.DeliveryProships.Where(x => x.ID == postOfficeID).FirstOrDefault();

                if (postOffice != null)
                {
                    postOffice.OrderStatus = (int)OrderStatus.Spam;
                    con.SaveChanges();
                }
            }
        }
    }
}