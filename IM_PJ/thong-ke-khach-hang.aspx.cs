using IM_PJ.Controllers;
using IM_PJ.Models.Pages.thong_ke_doanh_thu_khach_hang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IM_PJ
{
    public partial class thong_ke_khach_hang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var config = ConfigController.GetByTop1();
            if (config.ViewAllReports == 0)
            {
                Response.Redirect("/trang-chu");
            }

            if (!IsPostBack)
            {
                if (Request.Cookies["usernameLoginSystem"] != null)
                {
                    string username = Request.Cookies["usernameLoginSystem"].Value;
                    var acc = AccountController.GetByUsername(username);
                    if (acc != null)
                    {
                        if (acc.RoleID != 0 || acc.RoleID == 2)
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
            
            DateTime fromDate = DateTime.Today;
            DateTime toDate = fromDate.AddDays(1).AddMinutes(-1);
            string phoneNumber = String.Empty;

            if (!String.IsNullOrEmpty(Request.QueryString["textsearch"]))
            {
                phoneNumber = Request.QueryString["textsearch"].Trim();
            }

            if (phoneNumber != String.Empty)
            {
                var customer = CustomerController.GetByPhone(phoneNumber);
                if (customer != null)
                {
                    fromDate = Convert.ToDateTime(customer.CreatedDate);
                }
            }

            if (!String.IsNullOrEmpty(Request.QueryString["fromdate"]))
            {
                fromDate = Convert.ToDateTime(Request.QueryString["fromdate"]);
            }

            if (!String.IsNullOrEmpty(Request.QueryString["todate"]))
            {
                toDate = Convert.ToDateTime(Request.QueryString["todate"]).AddDays(1).AddMinutes(-1);
            }

            rFromDate.SelectedDate = fromDate;
            rToDate.SelectedDate = toDate;

            int day = Convert.ToInt32((toDate - fromDate).TotalDays);

            var reportModel = OrderController.reportProfitByCustomer(phoneNumber, fromDate, toDate);

            if (reportModel != null)
            {
                initCustomer(reportModel.customer);
                initReport(reportModel.data, fromDate, toDate);
                initChart(reportModel.data, fromDate, toDate);
                txtTextSearch.Text = phoneNumber;
            }
        }

        private void initCustomer(CustomerModel customer)
        {
            var html = new StringBuilder();

            html.AppendLine("<div class='row'>");
            html.AppendLine("    <div class='col-md-6'>");
            html.AppendLine("        <div class='form-group'>");
            html.AppendLine("            <label>Họ tên</label>");
            html.AppendLine("            <span class='form-control input-disabled'>" + customer.name + "</span>");
            html.AppendLine("        </div>");
            html.AppendLine("    </div>");
            html.AppendLine("    <div class='col-md-6'>");
            html.AppendLine("        <div class='form-group'>");
            html.AppendLine("            <label>Điện thoại</label>");
            html.AppendLine("            <span class='form-control input-disabled'>" + customer.phone + "</span>");
            html.AppendLine("        </div>");
            html.AppendLine("    </div>");
            html.AppendLine("</div>");
            html.AppendLine("<div class='row'>");
            html.AppendLine("    <div class='col-md-6'>");
            html.AppendLine("        <div class='form-group'>");
            html.AppendLine("            <label>Nick đặt hàng</label>");
            html.AppendLine("            <span class='form-control input-disabled'>" + customer.nick + "</span>");
            html.AppendLine("        </div>");
            html.AppendLine("    </div>");
            html.AppendLine("    <div class='col-md-6'>");
            html.AppendLine("        <div class='form-group'>");
            html.AppendLine("            <label>Địa chỉ</label>");
            html.AppendLine("            <span class='form-control input-disabled'>" + customer.address + "</span>");
            html.AppendLine("        </div>");
            html.AppendLine("    </div>");
            html.AppendLine("</div>");

            ltrInfo.Text = html.ToString();
        }

        private void initReport(List<OrderInfoModel> data, DateTime fromDate, DateTime toDate)
        {
            var day = Convert.ToInt32((toDate - fromDate).TotalDays);

            var report = data
                .GroupBy(g => 1)
                .Select(x => new
                    {
                        totalQuantityOrder = x.Sum(s => s.quantityOrder),
                        totalQuantityProduct = x.Sum(s => s.quantityProduct),
                        totalCostOfGoods = x.Sum(s => s.costOfGoods),
                        totalPrice = x.Sum(s => s.price),
                        totalDiscount = x.Sum(s => s.discount),
                        totalFeeShipping = x.Sum(s => s.feeShipping),
                        totalFeeOther = x.Sum(s => s.feeOther),
                        totalQuantityRefund = x.Sum(s => s.quantityRefund),
                        totalQuantityProductRefund = x.Sum(s => s.quantityProductRefund),
                        totalRefundMoney = x.Sum(s => s.refundMoney),
                        totalRefundFee = x.Sum(s => s.refundFee),
                        totalRefundCapital = x.Sum(s => s.refundCapital)
                }
                )
                .FirstOrDefault();

            if (report != null && day > 0)
            {
                // Dòng 1
                ltrTotalQuantityOrder.Text = String.Format("{0:N0} đơn", report.totalQuantityOrder);
                ltrTotalQuantityProduct.Text = String.Format("{0:N0} cái", report.totalQuantityProduct);
                ltrTotalQuantityRefund.Text = String.Format("{0:N0} đơn", report.totalQuantityRefund);
                ltrTotalQuantityProductRefund.Text = String.Format("{0:N0} cái", report.totalQuantityProductRefund);
                ltrTotalProductLeft.Text = String.Format("{0:N0} cái ({1:N0} cái/đơn)", report.totalQuantityProduct - report.totalQuantityProductRefund, (report.totalQuantityProduct - report.totalQuantityProductRefund)/report.totalQuantityOrder);
                ltrAverageProduct.Text = String.Format("{0:#,##0.##} cái/ngày", Math.Round((report.totalQuantityProduct - report.totalQuantityProductRefund) / (day * 1.0), 2));
                // Dòng 2
                ltrTotalPrice.Text = String.Format("{0:N0} đ", report.totalPrice);
                ltrTotalDiscount.Text = String.Format("{0:N0} đ", report.totalDiscount);
                ltrTotalFeeShipping.Text = String.Format("{0:N0} đ", report.totalFeeShipping);
                ltrTotalRefundMoney.Text = String.Format("{0:N0} đ", report.totalRefundMoney);
                ltrTotalRefundFee.Text = String.Format("{0:N0} đ", report.totalRefundFee);
                ltrTotalProfit.Text = String.Format("{0:N0} đ", (report.totalPrice - report.totalCostOfGoods - report.totalDiscount) - (report.totalRefundMoney - report.totalRefundCapital));
            }
        }

        private void initChart(List<OrderInfoModel> data, DateTime fromDate, DateTime toDate)
        {
            var day = Convert.ToInt32((toDate - fromDate).TotalDays);

            if (day > 1)
            {
                string chartDays = "";
                string chartProfit = "";

                List<string> dataDays = new List<string>();
                List<string> dataProfit = new List<string>();

                foreach (var item in data)
                {
                    var date = String.Format("'{0:d/M}'", item.dateDone);
                    double profit = 0;

                    // Tính số tiền lời dựa trên các sản phẩm đã bán
                    // Tiền lời bằng tiền bán - tiền vốn - tiền triết khấu + phí shipping + phí khác
                    profit += item.price - item.costOfGoods - item.discount;
                    // Tính số tiền lời sau khi sản phẩn được đổi trả
                    // Tiền lời bằng tiền lời đã bán - số liền lời của sản phẩn đổi trả + phí đổi trả
                    profit += -(item.refundMoney - item.refundCapital);
                    dataDays.Add(date);
                    dataProfit.Add(profit.ToString());
                }

                chartDays = String.Join(", ", dataDays);
                chartProfit = String.Join(", ", dataProfit);

                StringBuilder html = new StringBuilder();
                html.Append("<script>");
                html.Append("var lineChartData = {");
                html.Append("    labels: [" + chartDays + "],");
                html.Append("    datasets: [{");
                html.Append("        label: 'Lợi nhuận',");
                html.Append("        borderColor: 'rgb(255, 99, 132)',");
                html.Append("        backgroundColor: 'rgb(255, 99, 132)',");
                html.Append("        fill: false,");
                html.Append("        data: [" + chartProfit + "],");
                html.Append("        yAxisID: 'y-axis-1',");
                html.Append("    }]");
                html.Append("};");
                html.Append("</script>");

                ltrChartData.Text = html.ToString();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string request = "/thong-ke-khach-hang?";

            if (txtTextSearch.Text != "")
            {
                request += "&textsearch=" + txtTextSearch.Text;
            }

            if (rFromDate.SelectedDate.HasValue)
            {
                request += "&fromdate=" + rFromDate.SelectedDate.ToString();
            }

            if (rToDate.SelectedDate.HasValue)
            {
                request += "&todate=" + rToDate.SelectedDate.ToString();
            }

            Response.Redirect(request);
        }
    }
}