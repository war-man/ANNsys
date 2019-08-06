﻿using IM_PJ.Controllers;
using IM_PJ.Models;
using MB.Extensions;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace IM_PJ
{
    public partial class thong_ke_proship : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["usernameLoginSystem"] != null)
                {
                    string username = Request.Cookies["usernameLoginSystem"].Value;
                    var acc = AccountController.GetByUsername(username);

                    if (acc != null)
                    {
                        if (acc.RoleID != 0)
                        {
                            Response.Redirect("/trang-chu");
                        }
                    }
                }
                else
                {
                    Response.Redirect("/dang-nhap");
                }
                LoadData();
            }
        }
        
        public void LoadData()
        {
            string username = Request.Cookies["usernameLoginSystem"].Value;
            var acc = AccountController.GetByUsername(username);
            if (acc != null)
            {
                DateTime DateConfig = new DateTime(2019, 2, 15);

                var config = ConfigController.GetByTop1();
                if (config.ViewAllOrders == 1)
                {
                    DateConfig = new DateTime(2018, 6, 22);
                }

                DateTime OrderFromDate = DateConfig;
                DateTime OrderToDate = DateTime.Now;

                if (!String.IsNullOrEmpty(Request.QueryString["orderfromdate"]))
                {
                    OrderFromDate = Convert.ToDateTime(Request.QueryString["orderfromdate"]);
                }

                if (!String.IsNullOrEmpty(Request.QueryString["ordertodate"]))
                {
                    OrderToDate = Convert.ToDateTime(Request.QueryString["ordertodate"]).AddDays(1).AddMinutes(-1);
                }

                rOrderFromDate.SelectedDate = OrderFromDate;
                rOrderFromDate.MinDate = DateConfig;
                rOrderFromDate.MaxDate = DateTime.Now;

                rOrderToDate.SelectedDate = OrderToDate;
                rOrderToDate.MinDate = DateConfig;
                rOrderToDate.MaxDate = DateTime.Now;

                string TextSearch = String.Empty;
                int orderStatus = 0;
                string deliveryStatus = String.Empty;
                int review = 0;
                int feeStatus = 0;
                int Page = 1;

                if (Request.QueryString["textsearch"] != null)
                    TextSearch = Request.QueryString["textsearch"].Trim();
                if (Request.QueryString["orderstatus"] != null)
                    orderStatus = Request.QueryString["orderstatus"].ToInt(0);
                if (Request.QueryString["deliverystatus"] != null)
                    deliveryStatus = Request.QueryString["deliverystatus"];
                if (Request.QueryString["review"] != null)
                    review = Request.QueryString["review"].ToInt(0);
                if (Request.QueryString["feestatus"] != null)
                    feeStatus = Request.QueryString["feestatus"].ToInt(0);
                if (Request.QueryString["Page"] != null)
                    Page = Request.QueryString["Page"].ToInt();

                txtSearchOrder.Text = TextSearch;
                ddlOrderStatus.SelectedValue = orderStatus.ToString();
                ddlDeliveryStatus.SelectedValue = deliveryStatus.ToString();
                ddlReview.SelectedValue = review.ToString();
                ddlFeeStatus.SelectedValue = feeStatus.ToString();

                // Create delivery post office fileter
                var filter = new DeliveryProshipFilterModel()
                {
                    search = TextSearch,
                    orderStatus = orderStatus,
                    deliveryStatus = deliveryStatus,
                    review = review,
                    feeStatus = feeStatus,
                    orderFromDate = OrderFromDate,
                    orderToDate = OrderToDate
                };
                // Create pagination
                var page = new PaginationMetadataModel()
                {
                    currentPage = Page
                };
                decimal lossMoney = 0;

                List<DeliveryProship> rs = new List<DeliveryProship>();
                rs = DeliveryProshipController.Filter(filter, ref page, ref lossMoney);

                pagingall(rs, page);

                ltrNumberOfOrder.Text = String.Format("{0} số đơn Proship - Số tiền phí lệch: {1:#,###} VND",page.totalCount.ToString(), lossMoney);
            }
        }

        #region Paging
        public void pagingall(List<DeliveryProship> acs, PaginationMetadataModel page)
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine("<thead>");
            html.AppendLine("<tr>");
            html.AppendLine("    <th>Mã</th>");
            html.AppendLine("    <th class='col-customer'>Khách hàng</th>");
            html.AppendLine("    <th>Điện Thoại</th>");
            html.AppendLine("    <th>Trạng thái</th>");
            html.AppendLine("    <th>Ngày gửi</th>");
            html.AppendLine("    <th>Ngày phát</th>");
            html.AppendLine("    <th>COD <br/>(Bưu Điện)</th>");
            html.AppendLine("    <th>COD <br/>(Hệ thống)</th>");
            html.AppendLine("    <th>Phí <br/>(Bưu Điện)</th>");
            html.AppendLine("    <th>Phí <br/>(Hệ thống)</th>");
            html.AppendLine("    <th>Nhân viên</th>");
            html.AppendLine("    <th></th>");
            html.AppendLine("</tr>");
            html.AppendLine("</thead>");

            html.AppendLine("<tbody>");
            if (acs.Count > 0)
            {
                PageCount = page.totalPages;
                Int32 Page = page.currentPage;

                foreach (var item in acs)
                {
                    // Insert transfer bank info for tr tag
                    var TrTag = new StringBuilder();
                    TrTag.AppendLine("<tr ");
                    TrTag.AppendLine(String.Format("data-id='{0}' ", item.ID));
                    TrTag.AppendLine(String.Format("data-orderid='{0}' ", item.OrderID));
                    TrTag.AppendLine("/>");

                    html.AppendLine(TrTag.ToString());
                    html.AppendLine("   <td data-title='Mã hóa đơn'>");
                    html.AppendLine("       <a href=\"/thong-tin-don-hang?id=" + item.OrderID + "\">" + String.Format("{0:#}", item.OrderID) + "</a>");
                    html.AppendLine("   </td>");
                    html.AppendLine("   <td data-title='Khách hàng' class='customer-td'>");
                    html.AppendLine("       <a class=\"col-customer-name-link\" href=\"/thong-tin-don-hang?id=" + item.OrderID + "\">" + item.Customer.ToTitleCase() + "</a>");
                    html.AppendLine("   </td>");
                    html.AppendLine("   <td data-title='Điện Thoại'>" + item.Phone + "</td>");
                    html.AppendLine("   <td data-title='Trạng thái'>");
                    if (item.DeliveryStatus == "Hủy")
                        html.AppendLine("      <span class='bg-red'>Hủy</span>");
                    else
                        html.AppendLine("      " + item.DeliveryStatus);
                    html.AppendLine("   </td>");
                    html.AppendLine("   <td data-title='Ngày gửi'>" + String.Format("{0:dd/MM/yyyy}", item.StartDate) + "</td>");
                    if (item.DoneDate.HasValue)
                        html.AppendLine("   <td data-title='Ngày phát'>" + String.Format("{0:dd/MM/yyyy}", item.DoneDate) + "</td>");
                    else
                        html.AppendLine("   <td data-title='Ngày phát'></td>");
                    html.AppendLine("   <td data-title='COD (Bưu điện)'><strong>" + String.Format("{0:#,###}", item.COD) + "</strong></td>");
                    html.AppendLine("   <td data-title='COD (Hệ thống)'><strong>" + String.Format("{0:#,###}", item.OrderCOD) + "</strong></td>");
                    html.AppendLine("   <td data-title='Phí (Bưu điện)'><strong>" + String.Format("{0:#,###}", item.Fee) + "</strong></td>");
                    html.AppendLine("   <td data-title='Phí (Hệ thống)'><strong>" + String.Format("{0:#,###}", item.OrderFee) + "</strong></td>");
                    html.AppendLine("   <td data-title='Nhân viên tạo đơn'>" + item.Staff + "</td>");
                    html.AppendLine("   <td data-title='Thao tác' class='handle-button'>");
                    if (!(item.OrderStatus == (int)OrderStatus.Spam || item.Review == (int)DeliveryProshipReview.Approve))
                    {
                        html.AppendLine("       <button type='button'");
                        html.AppendLine("           class='btn primary-btn h45-btn'");
                        html.AppendLine("           title='Duyệt đơn hàng'");
                        html.AppendLine("           style='background-color: #73a724'");
                        html.AppendLine(String.Format("           onclick='approve({0}, {1})'", item.ID, item.OrderID));
                        html.AppendLine("       >");
                        html.AppendLine("           <span class='glyphicon glyphicon-check'></span>");
                        html.AppendLine("       </button>");
                        html.AppendLine("       <button type='button'");
                        html.AppendLine("           class='btn primary-btn h45-btn'");
                        html.AppendLine("           title='Bỏ qua đơn hàng'");
                        html.AppendLine("           style='background-color: #FF675B'");
                        html.AppendLine(String.Format("           onclick='cancel({0})'", item.ID));
                        html.AppendLine("       >");
                        html.AppendLine("           <span class='glyphicon glyphicon-trash'></span>");
                        html.AppendLine("       </button>");
                    }
                    html.AppendLine("   </td>");
                    html.AppendLine("</tr>");

                    // thông tin thêm
                    html.AppendLine("<tr class='tr-more-info'>");
                    html.AppendLine("   <td colspan='1' data-title='Thông tin mã vẫn đơn'>");
                    html.AppendLine("   <span class='bg-blue'><strong>" + item.NumberID + "</strong></span>");
                    html.AppendLine("   </td>");
                    html.AppendLine("   <td colspan='11' data-title='Thông tin thêm địa chỉ khách hàng'>");
                    if (!string.IsNullOrEmpty(item.Address))
                    {
                        html.AppendLine("<span class='order-info'><strong>Địa chỉ:</strong> " + item.Address + "</span>");
                    }
                    html.AppendLine("</td>");
                    html.AppendLine("</tr>");
                }

            }
            else
            {
                html.AppendLine("<tr><td colspan=\"12\">Không tìm thấy đơn hàng...</td></tr>");
            }
            html.AppendLine("</tbody>");

            ltrList.Text = html.ToString();
        }
        private int PageCount;
        protected void DisplayHtmlStringPaging1()
        {

            Int32 CurrentPage = Convert.ToInt32(Request.QueryString["Page"]);
            if (CurrentPage == -1) CurrentPage = 1;
            string[] strText = new string[4] { "Trang đầu", "Trang cuối", "Trang sau", "Trang trước" };
            if (PageCount > 1)
                Response.Write(GetHtmlPagingAdvanced(6, CurrentPage, PageCount, Context.Request.RawUrl, strText));

        }
        private static string GetPageUrl(int currentPage, string pageUrl)
        {
            pageUrl = Regex.Replace(pageUrl, "(\\?|\\&)*" + "Page=" + currentPage, "");
            if (pageUrl.IndexOf("?") > 0)
            {
                pageUrl += "&Page={0}";
            }
            else
            {
                pageUrl += "?Page={0}";
            }
            return pageUrl;
        }
        public static string GetHtmlPagingAdvanced(int pagesToOutput, int currentPage, int pageCount, string currentPageUrl, string[] strText)
        {
            //Nếu Số trang hiển thị là số lẻ thì tăng thêm 1 thành chẵn
            if (pagesToOutput % 2 != 0)
            {
                pagesToOutput++;
            }

            //Một nửa số trang để đầu ra, đây là số lượng hai bên.
            int pagesToOutputHalfed = pagesToOutput / 2;

            //Url của trang
            string pageUrl = GetPageUrl(currentPage, currentPageUrl);


            //Trang đầu tiên
            int startPageNumbersFrom = currentPage - pagesToOutputHalfed; ;

            //Trang cuối cùng
            int stopPageNumbersAt = currentPage + pagesToOutputHalfed; ;

            StringBuilder output = new StringBuilder();

            //Nối chuỗi phân trang
            //output.AppendLine("<div class=\"paging\">");
            output.AppendLine("<ul>");

            //Link First(Trang đầu) và Previous(Trang trước)
            if (currentPage > 1)
            {
                output.AppendLine("<li><a title=\"" + strText[0] + "\" href=\"" + string.Format(pageUrl, 1) + "\">Trang đầu</a></li>");
                output.AppendLine("<li><a title=\"" + strText[1] + "\" href=\"" + string.Format(pageUrl, currentPage - 1) + "\">Trang trước</a></li>");
            }

            /******************Xác định startPageNumbersFrom & stopPageNumbersAt**********************/
            if (startPageNumbersFrom < 1)
            {
                startPageNumbersFrom = 1;

                //As page numbers are starting at one, output an even number of pages.  
                stopPageNumbersAt = pagesToOutput;
            }

            if (stopPageNumbersAt > pageCount)
            {
                stopPageNumbersAt = pageCount;
            }

            if ((stopPageNumbersAt - startPageNumbersFrom) < pagesToOutput)
            {
                startPageNumbersFrom = stopPageNumbersAt - pagesToOutput;
                if (startPageNumbersFrom < 1)
                {
                    startPageNumbersFrom = 1;
                }
            }
            /******************End: Xác định startPageNumbersFrom & stopPageNumbersAt**********************/

            //Các dấu ... chỉ những trang phía trước  
            if (startPageNumbersFrom > 1)
            {
                output.AppendLine("<li><a href=\"" + string.Format(GetPageUrl(currentPage - 1, pageUrl), startPageNumbersFrom - 1) + "\">&hellip;</a></li>");
            }

            //Duyệt vòng for hiển thị các trang
            for (int i = startPageNumbersFrom; i <= stopPageNumbersAt; i++)
            {
                if (currentPage == i)
                {
                    output.AppendLine("<li class=\"current\" ><a >" + i.ToString() + "</a> </li>");
                }
                else
                {
                    output.AppendLine("<li><a href=\"" + string.Format(pageUrl, i) + "\">" + i.ToString() + "</a> </li>");
                }
            }

            //Các dấu ... chỉ những trang tiếp theo  
            if (stopPageNumbersAt < pageCount)
            {
                output.AppendLine("<li><a href=\"" + string.Format(pageUrl, stopPageNumbersAt + 1) + "\">&hellip;</a></li>");
            }

            //Link Next(Trang tiếp) và Last(Trang cuối)
            if (currentPage != pageCount)
            {
                output.AppendLine("<li><a title=\"" + strText[2] + "\" href=\"" + string.Format(pageUrl, currentPage + 1) + "\">Trang sau</a></li>");
                output.AppendLine("<li><a title=\"" + strText[3] + "\" href=\"" + string.Format(pageUrl, pageCount) + "\">Trang cuối</a></li>");
            }
            output.AppendLine("</ul>");
            return output.ToString();
        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string search = txtSearchOrder.Text.Trim();
            string request = "/thong-ke-buu-dien?";

            if (search != "")
                request += "&textsearch=" + search;

            if (ddlOrderStatus.SelectedValue != "0")
                request += "&orderstatus=" + ddlOrderStatus.SelectedValue;
            if (!String.IsNullOrEmpty(ddlDeliveryStatus.SelectedValue))
                request += "&deliverystatus=" + ddlDeliveryStatus.SelectedValue;
            if (ddlReview.SelectedValue != "0")
                request += "&review=" + ddlReview.SelectedValue;

            if (ddlFeeStatus.SelectedValue != "0")
                request += "&feestatus=" + ddlFeeStatus.SelectedValue;

            if (rOrderFromDate.SelectedDate.HasValue)
                request += "&orderfromdate=" + rOrderFromDate.SelectedDate.ToString();

            if (rOrderToDate.SelectedDate.HasValue)
                request += "&ordertodate=" + rOrderToDate.SelectedDate.ToString();

            Response.Redirect(request);
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            var pathFile = Server.MapPath("~/uploads/deliveries/post_office/" + Path.GetFileName(FileUpload.FileName));
            if (FileUpload.HasFile)
            {
                // Nếu tồn tại file củ thì xóa đi
                File.Delete(pathFile);
                FileUpload.SaveAs(pathFile);

                var check = checkExcelStucture(pathFile);
                if (check)
                {
                    importExcel(pathFile);
                    UploadStatusLabel.Text = "Đã nhập import thành công file " + FileUpload.FileName;
                }
                else
                    UploadStatusLabel.Text = String.Format("Câu trúc file {0} không đúng quá quy định ", FileUpload.FileName);
                // Xóa file sau khi hoàn thành
                File.Delete(pathFile);
            }
            else
            {
                UploadStatusLabel.Text = "Có vấn đề trong việc import file " + FileUpload.FileName;
            }

            FileUpload.Dispose();
            LoadData();
        }

        private bool checkExcelStucture(string pathFile)
        {
            // Lấy stream file
            var fs = new FileStream(pathFile, FileMode.Open);

            // Khởi tạo workbook để đọc
            var wb = new XSSFWorkbook(fs);

            // Lấy sheet đầu tiên
            var sheet = wb.GetSheetAt(0);

            #region Kiểm tra dòng 1 header
            var row = sheet.GetRow(9);
            // AD10: Mã Shop
            if (row.GetCell(29).StringCellValue != "Mã Shop")
                return false;
            // AE10: Mã Hệ Thống
            if (row.GetCell(30).StringCellValue != "Mã Hệ Thống")
                return false;
            // D10: Ngày Tạo ĐH
            if (row.GetCell(3).StringCellValue != "Ngày Tạo ĐH")
                return false;
            // E10: Ngày Hoàn Thành
            if (row.GetCell(4).StringCellValue != "Ngày Hoàn Thành")
                return false;
            // W10: Thu Hộ
            if (row.GetCell(22).StringCellValue != "Thu Hộ")
                return false;
            // U10: Tổng Cộng
            if (row.GetCell(20).StringCellValue != "Tổng Cộng")
                return false;
            // AA10: Trạng thái
            if (row.GetCell(26).StringCellValue != "Trạng thái đơn hàng")
                return false;
            #endregion

            #region Kiểm tra dòng 2 header
            row = sheet.GetRow(10);
            // F11: Họ tên
            if (row.GetCell(5).StringCellValue != "Họ Tên")
                return false;
            // G11: SĐT
            if (row.GetCell(6).StringCellValue != "SĐT")
                return false;
            // H11: Địa chỉ
            if (row.GetCell(7).StringCellValue != "Địa Chỉ")
                return false;
            #endregion

            wb.Close();
            fs.Close();

            return true;
        }

        private void importExcel(string pathFile)
        {
            // Lấy stream file
            var fs = new FileStream(pathFile, FileMode.Open);

            // Khởi tạo workbook để đọc
            var wb = new XSSFWorkbook(fs);

            // Lấy sheet đầu tiên
            var sheet = wb.GetSheetAt(0);

            var proships = new List<DeliveryProship>();

            // Lấy data từ dòng thứ 3 cho đến hết file
            for (int index = 11; index < sheet.LastRowNum; index++)
            {
                var row = sheet.GetRow(index);
                var item = new DeliveryProship();
                // OrderID
                if (!String.IsNullOrEmpty(row.GetCell(29).ToString()))
                {
                    var strOrderID = Regex.Match(row.GetCell(29).ToString(), @"[0-9]+").Value;
                    if(!String.IsNullOrEmpty(strOrderID))
                        item.OrderID = Convert.ToInt32(strOrderID);
                }
                // NumberID
                item.NumberID = row.GetCell(30).ToString();
                // Customer
                item.Customer = row.GetCell(5).ToString();
                // Phone
                item.Phone = row.GetCell(6).ToString().Replace(",", " |");
                // Address
                item.Address = row.GetCell(7).StringCellValue;
                // Delivery Status
                item.DeliveryStatus = row.GetCell(26).StringCellValue;
                // Start Date
                item.StartDate = DateTime.ParseExact(row.GetCell(3).ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                // Done Date
                if (!String.IsNullOrEmpty(row.GetCell(4).ToString()))
                    item.DoneDate = DateTime.ParseExact(row.GetCell(4).ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                // Tiền thu hộ
                item.COD = Convert.ToDecimal(row.GetCell(22).ToString());
                // Tiền phí
                item.Fee = Convert.ToDecimal(row.GetCell(20).ToString());
                // Staff
                item.Staff = String.Empty;

                // Setting trạng thái review với order
                item.Review = (int)DeliveryProshipReview.NoApprove;
                item.OrderStatus = (int)OrderStatus.Exist;
                proships.Add(item);

                // Thưc thi review các order sau khi đoc 100 dòng execl
                if (proships.Count == 100)
                {
                    DeliveryProshipController.reviewOrder(proships);
                    proships.Clear();
                }
            }

            // Thưc thi review các order sau khi đoc dòng excel còn lại
            if (proships.Count > 0)
            {
                DeliveryProshipController.reviewOrder(proships);
                proships.Clear();
            }

            wb.Close();
            fs.Close();
        }


        [WebMethod]
        public static void approve(int postOfficeID, int orderID)
        {
            DeliveryProshipController.approve(postOfficeID, orderID);
        }

        [WebMethod]
        public static void cancel(int postOfficeID)
        {
            DeliveryProshipController.cancel(postOfficeID);
        }
    }
}