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
using static IM_PJ.Controllers.DeliveryController;

namespace IM_PJ
{
    public partial class print_delivery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["userLoginSystem"] != null)
                {
                    string username = Request.Cookies["userLoginSystem"].Value;
                    var acc = AccountController.GetByUsername(username);
                    if (acc != null)
                    {
                        if (acc.RoleID == 0)
                        {

                        }
                        else if (acc.RoleID == 2)
                        {

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

        public void LoadData()
        {
            string username = Request.Cookies["userLoginSystem"].Value;
            var acc = AccountController.GetByUsername(username);
            var session = SessionController.getDeliverySession(acc);
            var shippers = session
                .Select(x => x.ShipperID)
                .Distinct()
                .OrderBy(o => o)
                .ToList();
            decimal totalMoneyCollection = 0;

            ltrPrintDelivery.Text = String.Empty;
            foreach (var shipperID in shippers)
            {
                for (var timers = 1; timers <= 2; ++timers)
                {
                    // Báo cáo chuyển hàng tới nhà xe
                    var transforOrders = session
                            .Where(x => x.ShipperID == shipperID)
                            .Where(x => x.ShippingType == (int)DeliveryType.TransferStation)
                            .Where(x => x.DeliveryTimes == timers)
                            .Select(x => x.OrderID)
                            .OrderBy(o => o)
                            .ToList();
                    var transforData = DeliveryController.getTransportReport(transforOrders);

                    if (transforData.Transports.Count > 0)
                    {
                        ltrPrintDelivery.Text += getTransportReportHTML(transforData, shipperID, timers, ref totalMoneyCollection);
                        // Update Delivery
                        DeliveryController.udpateAfterPrint(shipperID, transforOrders, acc.ID, timers);
                    }
                    
                    // Báo cáo chuyển hàng của shipper
                    var orders = session
                            .Where(x => x.ShipperID == shipperID)
                            .Where(x => x.ShippingType == (int)DeliveryType.Shipper)
                            .Where(x => x.DeliveryTimes == timers)
                            .Select(x => x.OrderID)
                            .OrderBy(o => o)
                            .ToList();
                    var shipperData = DeliveryController.getShipperReport(orders);

                    if (shipperData.Count > 0)
                    {
                        ltrPrintDelivery.Text += getShipperReportHTML(shipperData, shipperID, ref totalMoneyCollection);
                        // Update Delivery
                        DeliveryController.udpateAfterPrint(shipperID, orders, acc.ID, timers);
                    }
                }
            }

            if (totalMoneyCollection > 0)
            {
                ltrPrintDelivery.Text += getMoneyCollectionHTML(totalMoneyCollection);
            }
            
            if (!String.IsNullOrEmpty(ltrPrintDelivery.Text))
            {
                ltrPrintEnable.Text = "<div class='print-enable true'></div>";
            }
            else
            {
                ltrPrintDelivery.Text = "Không tìm thấy dữ liệu để xuất hóa đơn giao hàng";
            }
        }

        public string getTransportReportHTML(TransportReport data, int shipperID, int deliveryTimes, ref decimal moneyCollection)
        {
            var html = new StringBuilder();
            int index = 0;
            double totalQuantity = 0;
            double totalCollection = 0;
            string shipperName = ShipperController.getShipperNameByID(shipperID);

            html.AppendLine("<h1>PHIẾU GỬI XE</h1>");
            
            html.AppendLine("<div class='delivery'>");
            html.AppendLine("    <div class='all'>");
            html.AppendLine("        <div class='body'>");
            html.AppendLine("            <div class='table-2'>");
            html.AppendLine("               <div class='info'>");
            html.AppendLine(String.Format(" <p>Ngày giao: {0}</p>", string.Format("{0:dd/MM HH:mm}", DateTime.Now)));
            html.AppendLine(String.Format(" <p>Người giao: {0}</p>", shipperName));
            html.AppendLine(String.Format(" <p>Đợt giao: Đợt {0}</p>", deliveryTimes));
            html.AppendLine("               </div>");
            html.AppendLine("                <table>");
            html.AppendLine("                    <colgroup>");
            html.AppendLine("                        <col />");
            html.AppendLine("                        <col />");
            html.AppendLine("                        <col />");
            html.AppendLine("                    </colgroup>");
            html.AppendLine("                    <thead>");
            html.AppendLine("                        <th>Nhà xe</th>");
            html.AppendLine("                        <th>SL</th>");
            html.AppendLine("                        <th>Thu hộ</th>");
            html.AppendLine("                    </thead>");
            html.AppendLine("                    <tbody>");
            foreach(var item in data.Transports)
            {
                index += 1;
                totalQuantity += item.Quantity;
                totalCollection += item.Collection;

                html.AppendLine("                        <tr>");
                html.AppendLine(String.Format("                            <td><strong>{0}</strong></td>", item.TransportName.ToTitleCase()));
                html.AppendLine(String.Format("                            <td>{0:#,###}</td>", item.Quantity));
                if (item.Collection > 0)
                {
                    html.AppendLine(String.Format("                            <td>{0:#,###}</td>", item.Collection));
                }
                else
                {
                    html.AppendLine("                            <td>Không</td>");
                }
                html.AppendLine("                        </tr>");
            }
            html.AppendLine("                        <tr>");
            html.AppendLine("                            <td colspan='1' style='text-align: right'>Tổng số đơn</td>");
            html.AppendLine(String.Format("                            <td colspan='2'>{0:#,###}</td>", totalQuantity));
            html.AppendLine("                        </tr>");
            if(totalCollection > 0)
            {
                html.AppendLine("                        <tr>");
                html.AppendLine("                            <td colspan='1'  style='text-align: right'>Thu hộ</td>");
                html.AppendLine(String.Format("                            <td colspan='2'>{0:#,###}</td>", totalCollection));
                html.AppendLine("                        </tr>");
            }
            html.AppendLine("                    </tbody>");
            html.AppendLine("                </table>");
            html.AppendLine("            </div>");
            html.AppendLine("        </div>");
            html.AppendLine("    </div>");
            html.AppendLine("</div>");

            // Thông tin bổ xung cho giao hàng thu hộ
            if (data.Collections.Count > 0)
            {
                html.AppendLine("<h1>Danh sách đơn thu hộ</h1>");
                html.AppendLine("<div class='delivery'>");
                html.AppendLine("    <div class='all'>");
                html.AppendLine("        <div class='body'>");
                html.AppendLine("            <div class='table-2'>");
                html.AppendLine("                <table>");
                html.AppendLine("                    <colgroup>");
                html.AppendLine("                        <col />");
                html.AppendLine("                        <col />");
                html.AppendLine("                        <col />");
                html.AppendLine("                        <col />");
                html.AppendLine("                    </colgroup>");
                html.AppendLine("                    <thead>");
                html.AppendLine("                        <th>Mã</th>");
                html.AppendLine("                        <th>Nhà xe</th>");
                html.AppendLine("                        <th>Khách hàng</th>");
                html.AppendLine("                        <th>Thu hộ</th>");
                html.AppendLine("                    </thead>");
                html.AppendLine("                    <tbody>");
                foreach (var item in data.Collections)
                {
                    html.AppendLine("                        <tr>");
                    html.AppendLine(String.Format("                            <td><strong>{0}</strong></td>", item.OrderID));
                    html.AppendLine(String.Format("                            <td>{0}</td>", item.TransportName.ToTitleCase()));
                    html.AppendLine(String.Format("                            <td>{0}</td>", item.CustomerName.ToTitleCase()));
                    html.AppendLine(String.Format("                            <td style='text-align: right'>{0:#,###}</td>", item.Collection));
                    html.AppendLine("                        </tr>");
                    moneyCollection += item.Collection;
                }
                html.AppendLine("                        <tr>");
                html.AppendLine("                            <td colspan='3' style='text-align: right'>Tổng số đơn</td>");
                html.AppendLine(String.Format("                            <td colspan='4'>{0:#,###}</td>", data.Collections.Count));
                html.AppendLine("                        </tr>");
                if (totalCollection > 0)
                {
                    html.AppendLine("                        <tr>");
                    html.AppendLine("                            <td colspan='3' style='text-align: right'>Tổng thu hộ</td>");
                    html.AppendLine(String.Format("                            <td colspan='4'>{0:#,###}</td>", data.Collections.Sum(x => x.Collection)));
                    html.AppendLine("                        </tr>");
                }
                html.AppendLine("                    </tbody>");
                html.AppendLine("                </table>");
                html.AppendLine("            </div>");
                html.AppendLine("        </div>");
                html.AppendLine("    </div>");
                html.AppendLine("</div>");
            }

            return html.ToString();
        }

        public string getShipperReportHTML(List<ShipperReport> data, int shipperID, ref decimal moneyCollection)
        {
            var html = new StringBuilder();
            var index = 0;
            decimal totalPayment = 0;
            decimal totalMoneyCollection = 0;
            decimal totalPrice = 0;
            string shipperName = ShipperController.getShipperNameByID(shipperID);

            html.AppendLine("<h1>PHIẾU NHÂN VIÊN GIAO</h1>");

            html.AppendLine("<div class='delivery'>");
            html.AppendLine("    <div class='all'>");
            html.AppendLine("        <div class='body'>");
            html.AppendLine("            <div class='table-2'>");
            html.AppendLine("               <div class='info'>");
            html.AppendLine(String.Format(" <p>Ngày giao: {0}</p>", string.Format("{0:dd/MM HH:mm}", DateTime.Now)));
            html.AppendLine(String.Format(" <p>Người giao: {0}</p>", shipperName));
            html.AppendLine("               </div>");
            html.AppendLine("                <table>");
            html.AppendLine("                    <colgroup>");
            html.AppendLine("                        <col />");
            html.AppendLine("                        <col />");
            html.AppendLine("                        <col />");
            html.AppendLine("                        <col />");
            html.AppendLine("                    </colgroup>");
            html.AppendLine("                    <thead>");
            html.AppendLine("                        <tr>");
            html.AppendLine("                            <th rowspan='2' style='text-align: center;'>#</th>");
            html.AppendLine("                            <th colspan='3'>Tên khách hàng - Mã</th>");
            html.AppendLine("                        </tr>");
            html.AppendLine("                        <tr>");
            html.AppendLine("                            <th>Số tiền</th>");
            html.AppendLine("                            <th>Thu hộ</th>");
            html.AppendLine("                            <th>Phí</th>");
            html.AppendLine("                        </tr>");
            html.AppendLine("                    </thead>");
            html.AppendLine("                    <tbody>");
            foreach (var item in data)
            {
                index +=  1;
                totalPayment += item.Payment;
                totalMoneyCollection += item.MoneyCollection;
                totalPrice += item.Price;

                html.AppendLine("                        <tr>");
                html.AppendLine(String.Format("                            <td rowspan='2' style='text-align: center;'>{0:#,###}</td>", index));
                html.AppendLine(String.Format("                            <td colspan='3' style='border-bottom: 0;'><strong>{0}</strong> - {1}</td>", item.CustomerName.ToTitleCase(), item.OrderID));
                html.AppendLine("                        </tr>");

                html.AppendLine("                        <tr>");
                html.AppendLine(String.Format("                            <td style='border-top: 0;'>{0:#,###}</td>", item.Payment));
                html.AppendLine(String.Format("                            <td style='border-top: 0;'>{0:#,###}</td>", item.MoneyCollection));
                html.AppendLine(String.Format("                            <td style='border-top: 0;'>{0:#,###}</td>", item.Price));
                html.AppendLine("                        </tr>");
            }
            html.AppendLine("                        <tr>");
            html.AppendLine("                            <td colspan='3'></td>");
            html.AppendLine("                        </tr>");
            html.AppendLine("                        <tr>");
            html.AppendLine("                            <td colspan='3' style='text-align: right'>Tổng số đơn</td>");
            html.AppendLine(String.Format("                            <td style='text-align: right'>{0:#,###}</td>", data.Count));
            html.AppendLine("                        </tr>");
            html.AppendLine("                        <tr>");
            html.AppendLine("                            <td colspan='3' style='text-align: right'>Tổng tiền</td>");
            html.AppendLine(String.Format("                            <td style='text-align: right'>{0:#,###}</td>", totalPayment));
            html.AppendLine("                        </tr>");
            if (totalMoneyCollection > 0)
            {
                html.AppendLine("                        <tr>");
                html.AppendLine("                            <td colspan='3' style='text-align: right'>Tổng thu hộ</td>");
                html.AppendLine(String.Format("                            <td style='text-align: right'>{0:#,###}</td>", totalMoneyCollection));
                html.AppendLine("                        </tr>");
                moneyCollection += totalMoneyCollection;
            }
            if (totalPrice > 0)
            {
                html.AppendLine("                        <tr>");
                html.AppendLine("                            <td colspan='3'  style='text-align: right'>Tổng phí</td>");
                html.AppendLine(String.Format("                            <td style='text-align: right'>{0:#,###}</td>", totalPrice));
                html.AppendLine("                        </tr>");
            }
            html.AppendLine("                    </tbody>");
            html.AppendLine("                </table>");
            html.AppendLine("            </div>");
            html.AppendLine("        </div>");
            html.AppendLine("    </div>");
            html.AppendLine("</div>");

            return html.ToString();
        }

        public string getMoneyCollectionHTML(decimal totalMoneyCollection)
        {
            var html = new StringBuilder();

            html.AppendLine("<h1>Tổng kết số tiền thu hộ</h1>");
            html.AppendLine("<div class='delivery'>");
            html.AppendLine("    <div class='all'>");
            html.AppendLine("        <div class='body'>");
            html.AppendLine("            <div class='table-2'>");
            html.AppendLine("                <table>");
            html.AppendLine("                    <colgroup>");
            html.AppendLine("                        <col />");
            html.AppendLine("                        <col />");
            html.AppendLine("                        <col />");
            html.AppendLine("                        <col />");
            html.AppendLine("                    </colgroup>");
            html.AppendLine("                    <thead>");
            html.AppendLine("                        <th></th>");
            html.AppendLine("                        <th></th>");
            html.AppendLine("                        <th></th>");
            html.AppendLine("                        <th></th>");
            html.AppendLine("                    </thead>");
            html.AppendLine("                    <tbody>");
            html.AppendLine("                        <tr>");
            html.AppendLine("                            <td colspan='3' style='text-align: right'>Tổng thu hộ</td>");
            html.AppendLine(String.Format("                            <td style='text-align: right'>{0:#,###}</td>", totalMoneyCollection));
            html.AppendLine("                        </tr>");
            html.AppendLine("                    </tbody>");
            html.AppendLine("                </table>");
            html.AppendLine("            </div>");
            html.AppendLine("        </div>");
            html.AppendLine("    </div>");
            html.AppendLine("</div>");

            return html.ToString();
        }
    }
}