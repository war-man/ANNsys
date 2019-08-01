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
                var fromDate = postOffices.Min(x => x.StartDate).AddDays(-7); // Trừ cho 7 ngày đề phòng đơn tạo lâu chưa giao đi

                foreach (var item in postOffices)
                {
                    var order = con.tbl_Order
                        .Where(x => x.ExcuteStatus == (int)ExcuteStatus.Done)
                        .Where(x => x.ID == item.OrderID || x.ShippingCode == item.NumberID)
                        .FirstOrDefault();

                    if (order == null)
                    {
                        item.Review = (int)DeliveryPostOfficeReview.NoInfor;
                        continue;
                    }

                    // Các đơn có trạng thái hủy buộc phải kiểm tra bằng tay
                    if (item.DeliveryStatus == "Hủy")
                    {
                        item.Review = (int)DeliveryPostOfficeReview.Cancel;
                        continue;
                    }

                    item.Review = (int)DeliveryPostOfficeReview.NoApprove;
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
                    }
                    else
                    {
                        item.OrderCOD = 0;
                    }
                    item.OrderFee = Convert.ToDecimal(order.FeeShipping);
                    item.Staff = order.CreatedBy;

                    if (order.PaymentType != (int)PaymentType.CashCollection)
                    {
                        item.Review = 2;
                        order.PaymentStatus = 4;
                    }
                    else if (item.COD == item.OrderCOD)
                    {
                        item.Review = 2;
                        order.PaymentStatus = 4;
                    }
                }

                // Kiểm tra xem dữ liệu có tồn tại trong databse không nêu không thì insert
                foreach (var item in postOffices)
                {
                    var exist = con.DeliveryPostOffices
                        .Where(x => x.OrderID == item.OrderID || x.NumberID == item.NumberID);

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
                var postOffices = con.DeliveryPostOffices
                    .Where(x => x.Review != (int)DeliveryPostOfficeReview.NoHandle);

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
                #region Filter theo trạng thái review thông tin bưu điện và thông order
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
    }


}