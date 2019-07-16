using IM_PJ.Controllers;
using IM_PJ.Models;
using MB.Extensions;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using static IM_PJ.Controllers.OrderController;

namespace IM_PJ
{
    public partial class danh_sach_don_hang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["userLoginSystem"] != null)
                {
                    string username = Request.Cookies["userLoginSystem"].Value;
                    var acc = AccountController.GetByUsername(username);
                    int agent = acc.AgentID.ToString().ToInt();

                    if (acc != null)
                    {
                        LoadShipper();
                        LoadTransportCompany();
                        if (acc.RoleID == 0)
                        {
                            LoadCreatedBy(agent);
                        }
                        else if (acc.RoleID == 2)
                        {
                            LoadCreatedBy(agent, acc);
                        }
                        else
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
        public void LoadShipper()
        {
            var shipper = ShipperController.getDropDownList();
            shipper[0].Text = "Nhân viên giao hàng";
            ddlShipperFilter.Items.Clear();
            ddlShipperFilter.Items.AddRange(shipper.ToArray());
            ddlShipperFilter.DataBind();

        }
        public void LoadTransportCompany()
        {
            var TransportCompany = TransportCompanyController.GetTransportCompany();
            ddlTransportCompany.Items.Clear();
            ddlTransportCompany.Items.Insert(0, new ListItem("Chành xe", "0"));
            if (TransportCompany.Count > 0)
            {
                foreach (var p in TransportCompany)
                {
                    ListItem listitem = new ListItem(p.CompanyName.ToTitleCase(), p.ID.ToString());
                    ddlTransportCompany.Items.Add(listitem);
                }
                ddlTransportCompany.DataBind();
            }
        }
        public void LoadCreatedBy(int AgentID, tbl_Account acc = null)
        {
            if (acc != null)
            {
                ddlCreatedBy.Items.Clear();
                ddlCreatedBy.Items.Insert(0, new ListItem(acc.Username, acc.Username));
            }
            else
            {
                var CreateBy = AccountController.GetAllNotSearch();
                ddlCreatedBy.Items.Clear();
                ddlCreatedBy.Items.Insert(0, new ListItem("Nhân viên tạo đơn", ""));
                if (CreateBy.Count > 0)
                {
                    foreach (var p in CreateBy)
                    {
                        if(p.RoleID == 0 || p.RoleID == 2)
                        {
                            ListItem listitem = new ListItem(p.Username, p.Username);
                            ddlCreatedBy.Items.Add(listitem);
                        }
                    }
                    ddlCreatedBy.DataBind();
                }
            }
        }
        public void LoadData()
        {
            string username = Request.Cookies["userLoginSystem"].Value;
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
                DateTime OrderToDate = DateTime.Today;

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
                rOrderFromDate.MaxDate = DateTime.Today;

                rOrderToDate.SelectedDate = OrderToDate;
                rOrderToDate.MinDate = DateConfig;
                rOrderToDate.MaxDate = DateTime.Today;

                int OrderType = 0;
                int PaymentStatus = 0;
                var ExcuteStatus = new List<int>() {1, 2, 3};
                int PaymentType = 0;
                var ShippingType = new List<int>();
                string Discount = "";
                string OtherFee = "";
                string OrderNote = "";
                string TextSearch = "";
                string CreatedBy = "";
                int TransportCompany = 0;
                int ShipperID = 0;
                int Page = 1;

                // add filter quantity
                string Quantity = "";
                int QuantityFrom = 0;
                int QuantityTo = 0;
                // add filter seach type
                int SearchType = 0;

                if (Request.QueryString["textsearch"] != null)
                {
                    TextSearch = Request.QueryString["textsearch"].Trim();
                }
                if (Request.QueryString["searchtype"] != null)
                {
                    SearchType = Request.QueryString["searchtype"].ToInt(0);
                }
                if (Request.QueryString["ordertype"] != null)
                {
                    OrderType = Request.QueryString["ordertype"].ToInt(0);
                }
                if (Request.QueryString["paymentstatus"] != null)
                {
                    PaymentStatus = Request.QueryString["paymentstatus"].ToInt(0);
                }
                if (Request.QueryString["excutestatus"] != null)
                {
                    ExcuteStatus.Clear();
                    ExcuteStatus.Add(Request.QueryString["excutestatus"].ToInt(0));
                }
                if (Request.QueryString["paymenttype"] != null)
                {
                    PaymentType = Request.QueryString["paymenttype"].ToInt(0);
                }
                if (Request.QueryString["shippingtype"] != null)
                {
                    ShippingType.Add(Request.QueryString["shippingtype"].ToInt(0));
                }
                if (Request.QueryString["discount"] != null)
                {
                    Discount = Request.QueryString["discount"].ToString();
                }
                if (Request.QueryString["otherfee"] != null)
                {
                    OtherFee = Request.QueryString["otherfee"].ToString();
                }
                if (Request.QueryString["ordernote"] != null)
                {
                    OrderNote = Request.QueryString["ordernote"].ToString();
                }
                if (Request.QueryString["createdby"] != null)
                {
                    CreatedBy = Request.QueryString["createdby"];
                }
                if (Request.QueryString["transportcompany"] != null)
                {
                    TransportCompany = Request.QueryString["transportcompany"].ToInt(0);
                }
                if (Request.QueryString["shipperid"] != null)
                {
                    ShipperID = Request.QueryString["shipperid"].ToInt(0);
                }

                if (Request.QueryString["Page"] != null)
                {
                    Page = Request.QueryString["Page"].ToInt();
                }

                // add filter quantity
                if (Request.QueryString["quantityfilter"] != null)
                {
                    Quantity = Request.QueryString["quantityfilter"];

                    if (Quantity == "greaterthan")
                    {
                        QuantityFrom = Request.QueryString["quantity"].ToInt();
                    }
                    else if (Quantity == "lessthan")
                    {
                        QuantityTo = Request.QueryString["quantity"].ToInt();
                    }
                    else if (Quantity == "between")
                    {
                        QuantityFrom = Request.QueryString["quantitymin"].ToInt();
                        QuantityTo = Request.QueryString["quantitymax"].ToInt();
                    }
                }

                txtSearchOrder.Text = TextSearch;
                ddlSearchType.SelectedValue = SearchType.ToString();
                ddlOrderType.SelectedValue = OrderType.ToString();
                ddlExcuteStatus.SelectedValue = ExcuteStatus.Count() > 1 ? "0" : ExcuteStatus.FirstOrDefault().ToString();
                ddlPaymentStatus.SelectedValue = PaymentStatus.ToString();
                ddlPaymentType.SelectedValue = PaymentType.ToString();
                ddlShippingType.SelectedValue = ShippingType.Count() > 0 ? ShippingType.FirstOrDefault().ToString() : "0";
                ddlDiscount.SelectedValue = Discount;
                ddlOtherFee.SelectedValue = OtherFee;
                ddlOrderNote.SelectedValue = OrderNote;
                ddlCreatedBy.SelectedValue = CreatedBy;
                ddlTransportCompany.SelectedValue = TransportCompany.ToString();
                ddlShipperFilter.SelectedValue = ShipperID.ToString();

                // add filter quantity
                ddlQuantityFilter.SelectedValue = Quantity;
                if (Quantity == "greaterthan")
                {
                    txtQuantity.Text = QuantityFrom.ToString();
                    txtQuantityMin.Text = "0";
                    txtQuantityMax.Text = "0";
                }
                else if (Quantity == "lessthan")
                {
                    txtQuantity.Text = QuantityTo.ToString();
                    txtQuantityMin.Text = "0";
                    txtQuantityMax.Text = "0";
                }
                else if (Quantity == "between")
                {
                    txtQuantity.Text = "0";
                    txtQuantityMin.Text = QuantityFrom.ToString();
                    txtQuantityMax.Text = QuantityTo.ToString();
                }

                if (acc.RoleID != 0)
                {
                    CreatedBy = acc.Username;
                }

                // Create order fileter
                var filter = new OrderFilterModel()
                {
                    search = TextSearch,
                    searchType = SearchType,
                    orderType = OrderType,
                    excuteStatus = ExcuteStatus,
                    paymentStatus = PaymentStatus,
                    paymentType = PaymentType,
                    shippingType = ShippingType,
                    discount = Discount,
                    otherFee = OtherFee,
                    quantity = Quantity,
                    quantityFrom = QuantityFrom,
                    quantityTo = QuantityTo,
                    orderCreatedBy = CreatedBy,
                    orderFromDate = OrderFromDate,
                    orderToDate = OrderToDate,
                    transportCompany = TransportCompany,
                    shipper = ShipperID,
                    orderNote = OrderNote
                };
                // Create pagination
                var page = new PaginationMetadataModel()
                {
                    currentPage = Page
                };
                List<OrderList> rs = new List<OrderList>();
                rs = OrderController.Filter(filter, ref page);

                pagingall(rs, page);

                ltrNumberOfOrder.Text = page.totalCount.ToString();
            }
        }

        #region Paging
        public void pagingall(List<OrderList> acs, PaginationMetadataModel page)
        {
            string username = Request.Cookies["userLoginSystem"].Value;
            var acc = AccountController.GetByUsername(username);

            StringBuilder html = new StringBuilder();
            html.Append("<thead>");
            html.Append("<tr>");
            html.Append("    <th>Mã</th>");
            html.Append("    <th>Loại</th>");
            html.Append("    <th class='col-customer'>Khách hàng</th>");
            html.Append("    <th>Mua</th>");
            html.Append("    <th>Xử lý</th>");
            html.Append("    <th>Thanh toán</th>");
            html.Append("    <th>Thanh toán</th>");
            html.Append("    <th>Giao hàng</th>");
            html.Append("    <th>Tổng tiền</th>");
            if (acc.RoleID == 0)
            {
                html.Append("    <th>Nhân viên</th>");
            }
            html.Append("    <th>Ngày tạo</th>");
            html.Append("    <th>Hoàn tất</th>");
            html.Append("    <th></th>");
            html.Append("</tr>");
            html.Append("</thead>");

            html.Append("<tbody>");
            if (acs.Count > 0)
            {
                PageCount = page.totalPages;
                Int32 Page = page.currentPage;
                
                foreach(var item in acs)
                {
                    html.Append("<tr>");
                    html.Append("   <td data-title='Mã đơn'><a href=\"/thong-tin-don-hang?id=" + item.ID + "\">" + item.ID + "</a></td>");
                    html.Append("   <td data-title='Loại đơn'>" + PJUtils.OrderTypeStatus(Convert.ToInt32(item.OrderType)) + "</td>");

                    if (!string.IsNullOrEmpty(item.Nick))
                    {
                        html.Append("   <td data-title='Khách hàng' class='customer-td'><a class=\"col-customer-name-link\" href=\"/thong-tin-don-hang?id=" + item.ID + "\">" + item.Nick.ToTitleCase() + "</a><br><span class=\"name-bottom-nick\">(" + item.CustomerName.ToTitleCase() + ")</span></td>");
                    }
                    else
                    {
                        html.Append("   <td data-title='Khách hàng' class='customer-td'><a class=\"col-customer-name-link\" href=\"/thong-tin-don-hang?id=" + item.ID + "\">" + item.CustomerName.ToTitleCase() + "</a></td>");
                    }

                    html.Append("   <td data-title='Đã mua'>" + item.Quantity + "</td>");
                    html.Append("   <td data-title='Xử lý'>" + PJUtils.OrderExcuteStatus(Convert.ToInt32(item.ExcuteStatus)) + "</td>");
                    html.Append("   <td data-title='Thanh toán'>" + PJUtils.OrderPaymentStatus(Convert.ToInt32(item.PaymentStatus)) + "</td>");

                    #region Phương thức thanh toán
                    html.Append("   <td data-title='Kiểu thanh toán' class='payment-type'>");
                    html.Append(PJUtils.PaymentType(Convert.ToInt32(item.PaymentType)));
                    // Đã nhận tiền chuyển khoản
                    if(item.PaymentType == 2)
                    {
                        if (item.TransferStatus.HasValue && item.TransferStatus.Value == 1)
                        {
                            html.Append("       <br/><div class='new-status-btn'><span class='bg-green'>Đã nhận tiền</span></div>");
                        }
                        else
                        {
                            if (acc.RoleID == 0)
                                html.Append("       <br/><a class='new-status-btn' target='_blank' href='/danh-sach-chuyen-khoan?&textsearch=" + item.ID + "'><span class='bg-black'>Cập nhật</span></a>");
                        }
                    }
                    html.Append("   </td>");
                    #endregion


                    #region Giao hàng
                    html.Append("   <td data-title='Giao hàng' class='shipping-type'>");
                    html.Append(PJUtils.ShippingType(Convert.ToInt32(item.ShippingType)));
                    // Đã giao hàng
                    if (item.DeliveryStatus.HasValue && item.DeliveryStatus.Value == 1)
                        html.Append("       <br/><div class='new-status-btn'><span class='bg-green'>Đã giao</span></div>");
                    html.Append("   </td>");
                    #endregion


                    html.Append("   <td data-title='Tổng tiền'><strong>" + string.Format("{0:N0}", Convert.ToDouble(item.TotalPrice - item.TotalRefund)) + "</strong></td>");

                    if (acc.RoleID == 0)
                    {
                        html.Append("   <td data-title='Nhân viên tạo đơn'>" + item.CreatedBy + "</td>");
                    }

                    string date = string.Format("{0:dd/MM}", item.CreatedDate);
                    html.Append("   <td data-title='Ngày tạo đơn'>" + date + "</td>");

                    string datedone = "";
                    if (item.ExcuteStatus == 2)
                    {
                        datedone = string.Format("{0:dd/MM}", item.DateDone);
                    }
                    html.Append("   <td data-title='Ngày hoàn tất'>" + datedone + "</td>");

                    html.Append("   <td data-title='Thao tác' class='update-button'>");
                    html.Append("       <a href=\"/print-invoice?id=" + item.ID + "\" title=\"In hóa đơn\" target=\"_blank\" class=\"btn primary-btn h45-btn\"><i class=\"fa fa-print\" aria-hidden=\"true\"></i></a>");
                    html.Append("       <a href=\"/print-shipping-note?id=" + item.ID + "\" title=\"In phiếu gửi hàng\" target=\"_blank\" class=\"btn primary-btn btn-red h45-btn\"><i class=\"fa fa-file-text-o\" aria-hidden=\"true\"></i></a>");
                    html.Append("       <a href=\"/chi-tiet-khach-hang?id=" + item.CustomerID + "\" title=\"Thông tin khách hàng " + item.CustomerName + "\" target=\"_blank\" class=\"btn primary-btn btn-black h45-btn\"><i class=\"fa fa-user-circle\" aria-hidden=\"true\"></i></a>");
                    if (item.DeliveryStatus.HasValue && item.DeliveryStatus.Value == 1 && !string.IsNullOrEmpty(item.InvoiceImage))
                        html.Append("       <a href='javascript:;' onclick='openImageInvoice($(this))' data-link='" + item.InvoiceImage + "' title='Biên nhận gửi hàng' class='btn primary-btn btn-blue h45-btn'><i class='fa fa-file-text-o' aria-hidden='true'></i></a>");
                    html.Append("   </td>");
                    html.Append("</tr>");

                    // thông tin thêm

                    html.Append("<tr class='tr-more-info'>");
                    html.Append("<td colspan='2'></td>");
                    html.Append("<td colspan='11'>");

                    if(item.TotalRefund != 0)
                    {
                        html.Append("<span class='order-info'><strong>Trừ hàng trả:</strong> " + string.Format("{0:N0}", item.TotalRefund) + " (<a href='xem-don-hang-doi-tra?id=" + item.RefundsGoodsID + "' target='_blank'>Xem đơn " + item.RefundsGoodsID + "</a>)</span>");
                    }

                    if (item.TotalDiscount > 0)
                    {
                        html.Append("<span class='order-info'><strong>Chiết khấu:</strong> " + string.Format("{0:N0}", Convert.ToDouble(item.TotalDiscount)) + "</span>");
                    }
                    if (item.OtherFeeValue != 0)
                    {
                        html.Append("<span class='order-info'><strong>Phí khác:</strong> " + string.Format("{0:N0}", Convert.ToDouble(item.OtherFeeValue)) + " (<a href='#feeInfoModal' data-toggle='modal' data-backdrop='static' onclick='openFeeInfoModal(" + item.ID + ")'>" + item.OtherFeeName.Trim() + "</a>)</span>");
                    }
                    if (item.ShippingType == 4)
                    {
                        if (item.TransportCompanyID != 0)
                        {
                            var transport = TransportCompanyController.GetTransportCompanyByID(Convert.ToInt32(item.TransportCompanyID));
                            var transportsub = TransportCompanyController.GetReceivePlaceByID(Convert.ToInt32(item.TransportCompanyID), Convert.ToInt32(item.TransportCompanySubID));
                            html.Append("<span class='order-info'><strong>Gửi xe: </strong> " + transport.CompanyName.ToTitleCase() + " (" + transportsub.ShipTo.ToTitleCase() + ")</span>");
                        }
                    }
                    if (!string.IsNullOrEmpty(item.ShippingCode))
                    {
                        string moreInfo = "";
                        if (item.ShippingType == 3)
                        {
                            moreInfo = " (<a href='https://proship.vn/quan-ly-van-don/?isInvoiceFilter=1&amp;generalInfo=" + item.ShippingCode + "' target='_blank'>Xem</a>)";
                        }
                        if (item.ShippingType == 2)
                        {
                            moreInfo = " (Chuyển " + ((item.PostalDeliveryType == 1) ? "thường" : "nhanh") + ")";
                        }
                        html.Append("<span class='order-info'><strong>Mã vận đơn:</strong> " + item.ShippingCode + moreInfo + "</span>");
                    }
                    if (item.FeeShipping > 0)
                    {
                        html.Append("<span class='order-info'><strong>Phí vận chuyển:</strong> " + string.Format("{0:N0}", Convert.ToDouble(item.FeeShipping)) + "</span>");
                    }
                    if ((item.ShippingType == 4 || item.ShippingType == 5) && !string.IsNullOrEmpty(item.ShipperName))
                    {
                        html.Append("<span class='order-info'><strong>Nhân viên giao hàng:</strong> " + item.ShipperName + "</span>");
                    }
                    if (!string.IsNullOrEmpty(item.OrderNote))
                    {
                        html.Append("<span class='order-info'><strong>Ghi chú:</strong> " + item.OrderNote + "</span>");
                    }
                    html.Append("</td>");
                    html.Append("</tr>");
                }
                
            }
            else
            {
                if (acc.RoleID == 0)
                {
                    html.Append("<tr><td colspan='13'>Không tìm thấy đơn hàng...</td></tr>");
                }
                else
                {
                    html.Append("<tr><td colspan='12'>Không tìm thấy đơn hàng...</td></tr>");
                }
            }
            html.Append("</tbody>");

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
            //output.Append("<div class=\"paging\">");
            output.Append("<ul>");

            //Link First(Trang đầu) và Previous(Trang trước)
            if (currentPage > 1)
            {
                output.Append("<li><a title=\"" + strText[0] + "\" href=\"" + string.Format(pageUrl, 1) + "\">Trang đầu</a></li>");
                output.Append("<li><a title=\"" + strText[1] + "\" href=\"" + string.Format(pageUrl, currentPage - 1) + "\">Trang trước</a></li>");
                //output.Append("<li class=\"UnselectedPrev\" ><a title=\"" + strText[1] + "\" href=\"" + string.Format(pageUrl, currentPage - 1) + "\"><i class=\"fa fa-angle-left\"></i></a></li>");

                //output.Append("<span class=\"Unselect_prev\"><a href=\"" + string.Format(pageUrl, currentPage - 1) + "\"></a></span>");
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
                output.Append("<li><a href=\"" + string.Format(GetPageUrl(currentPage - 1, pageUrl), startPageNumbersFrom - 1) + "\">&hellip;</a></li>");
            }

            //Duyệt vòng for hiển thị các trang
            for (int i = startPageNumbersFrom; i <= stopPageNumbersAt; i++)
            {
                if (currentPage == i)
                {
                    output.Append("<li class=\"current\" ><a >" + i.ToString() + "</a> </li>");
                }
                else
                {
                    output.Append("<li><a href=\"" + string.Format(pageUrl, i) + "\">" + i.ToString() + "</a> </li>");
                }
            }

            //Các dấu ... chỉ những trang tiếp theo  
            if (stopPageNumbersAt < pageCount)
            {
                output.Append("<li><a href=\"" + string.Format(pageUrl, stopPageNumbersAt + 1) + "\">&hellip;</a></li>");
            }

            //Link Next(Trang tiếp) và Last(Trang cuối)
            if (currentPage != pageCount)
            {
                //output.Append("<span class=\"Unselect_next\"><a href=\"" + string.Format(pageUrl, currentPage + 1) + "\"></a></span>");
                //output.Append("<li class=\"UnselectedNext\" ><a title=\"" + strText[2] + "\" href=\"" + string.Format(pageUrl, currentPage + 1) + "\"><i class=\"fa fa-angle-right\"></i></a></li>");
                output.Append("<li><a title=\"" + strText[2] + "\" href=\"" + string.Format(pageUrl, currentPage + 1) + "\">Trang sau</a></li>");
                output.Append("<li><a title=\"" + strText[3] + "\" href=\"" + string.Format(pageUrl, pageCount) + "\">Trang cuối</a></li>");
            }
            output.Append("</ul>");
            //output.Append("</div>");
            return output.ToString();
        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string search = txtSearchOrder.Text.Trim();
            string request = "/danh-sach-don-hang?";

            if (ddlSearchType.SelectedValue != "")
            {
                request += "&searchtype=" + ddlSearchType.SelectedValue;
            }

            if (search != "")
            {
                request += "&textsearch=" + search;
            }

            if(ddlOrderType.SelectedValue != "")
            {
                request += "&ordertype=" + ddlOrderType.SelectedValue;
            }

            if (ddlPaymentStatus.SelectedValue != "")
            {
                request += "&paymentstatus=" + ddlPaymentStatus.SelectedValue;
            }

            if (ddlExcuteStatus.SelectedValue != "")
            {
                request += "&excutestatus=" + ddlExcuteStatus.SelectedValue;
            }

            if (ddlPaymentType.SelectedValue != "")
            {
                request += "&paymenttype=" + ddlPaymentType.SelectedValue;
            }

            if (ddlShippingType.SelectedValue != "")
            {
                request += "&shippingtype=" + ddlShippingType.SelectedValue;
            }

            if (ddlDiscount.SelectedValue != "")
            {
                request += "&discount=" + ddlDiscount.SelectedValue;
            }

            if (ddlOtherFee.SelectedValue != "")
            {
                request += "&otherfee=" + ddlOtherFee.SelectedValue;
            }

            if (ddlOrderNote.SelectedValue != "")
            {
                request += "&ordernote=" + ddlOrderNote.SelectedValue;
            }

            if (ddlCreatedBy.SelectedValue != "")
            {
                request += "&createdby=" + ddlCreatedBy.SelectedValue;
            }

            if (rOrderFromDate.SelectedDate.HasValue)
            {
                request += "&orderfromdate=" + rOrderFromDate.SelectedDate.ToString();
            }

            if (rOrderToDate.SelectedDate.HasValue)
            {
                request += "&ordertodate=" + rOrderToDate.SelectedDate.ToString();
            }

            if (ddlQuantityFilter.SelectedValue != "")
            {
                if (ddlQuantityFilter.SelectedValue == "greaterthan" || ddlQuantityFilter.SelectedValue == "lessthan")
                {
                    request += "&quantityfilter=" + ddlQuantityFilter.SelectedValue + "&quantity=" + txtQuantity.Text;
                }

                if (ddlQuantityFilter.SelectedValue == "between")
                {
                    request += "&quantityfilter=" + ddlQuantityFilter.SelectedValue + "&quantitymin=" + txtQuantityMin.Text + "&quantitymax=" + txtQuantityMax.Text;
                }
            }
            if (ddlTransportCompany.SelectedValue != "0")
            {
                request += "&transportcompany=" + ddlTransportCompany.SelectedValue;
            }
            if (ddlShipperFilter.SelectedValue != "0")
            {
                request += "&shipperid=" + ddlShipperFilter.SelectedValue;
            }

            Response.Redirect(request);
        }

        [WebMethod]
        public static string getFeeInfo(int orderID)
        {
            return FeeController.getFeesJSON(orderID);
        }
    }
}