using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IM_PJ.Controllers;

namespace IM_PJ
{
    public partial class thong_ke_loi_nhuan : System.Web.UI.Page
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
                        if (acc.RoleID != 0)
                        {
                            Response.Redirect("/dang-nhap");
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
            DateTime fromdate = DateTime.Today;
            DateTime todate = fromdate.AddDays(1).AddMinutes(-1);

            if (!String.IsNullOrEmpty(Request.QueryString["fromdate"]))
            {
                fromdate = Convert.ToDateTime(Request.QueryString["fromdate"]);
            }

            if (!String.IsNullOrEmpty(Request.QueryString["todate"]))
            {
                todate = Convert.ToDateTime(Request.QueryString["todate"]).AddDays(1).AddMinutes(-1);
            }

            rFromDate.SelectedDate = fromdate;
            rToDate.SelectedDate = todate;

            int day = Convert.ToInt32((todate - fromdate).TotalDays);

            var reportModel = OrderController.GetProfitReport(fromdate, todate);
            double TotalSalePrice = reportModel.Sum(x => x.TotalSalePrice);
            double TotalRefundPrice = reportModel.Sum(x => x.TotalRefundPrice);
            double TotalRevenue = TotalSalePrice - TotalRefundPrice;

            double TotalSaleCost = reportModel.Sum(x => x.TotalSaleCost);
            double TotalRefundCost = reportModel.Sum(x => x.TotalRefundCost);
            double TotalCost = TotalSaleCost - TotalRefundCost;

            double TotalSaleDiscount = reportModel.Sum(x => x.TotalSaleDiscount);
            double TotalRefundFee = reportModel.Sum(x => x.TotalRefundFee);
            double TotalProfit = TotalRevenue - TotalCost - TotalSaleDiscount + TotalRefundFee;
            double TotalProfitPerDay = TotalProfit / day;
            double TotalProfitPerOrder = 0;

            int TotalSoldQuantity = reportModel.Sum(x => x.TotalSoldQuantity);
            int TotalRefundQuantity = reportModel.Sum(x => x.TotalRefundQuantity);
            int TotalRemainQuantity = TotalSoldQuantity - TotalRefundQuantity;

            int TotalNumberOfOrder = reportModel.Sum(x => x.TotalNumberOfOrder);
            if (TotalNumberOfOrder > 0)
            {
                TotalProfitPerOrder = Math.Ceiling(TotalProfit / TotalNumberOfOrder);
            }

            
            double TotalActualRevenue = TotalSalePrice - TotalRefundPrice - TotalSaleDiscount;
            double TotalOtherFee = reportModel.Sum(x => x.TotalOtherFee);
            double TotalShippingFee = reportModel.Sum(x => x.TotalShippingFee);

            ltrTotalProfit.Text = string.Format("{0:N0}", TotalProfit);
            ltrProfitPerDay.Text = string.Format("{0:N0}", TotalProfitPerDay);
            ltrProfitPerOrder.Text = string.Format("{0:N0}", TotalProfitPerOrder);

            ltrTotalRemainQuantity.Text = (TotalRemainQuantity).ToString() + " cái";
            ltrAverageRemainQuantity.Text = (TotalRemainQuantity / day).ToString() + " cái/ngày";
            ltrTotalSoldQuantity.Text = (TotalSoldQuantity).ToString() + " cái";

            ltrTotalSalePrice.Text = string.Format("{0:N0}", TotalSalePrice);
            ltrTotalSaleCost.Text = string.Format("{0:N0}", TotalSaleCost);
            ltrTotalDisount.Text = string.Format("{0:N0}", TotalSaleDiscount);

            ltrTotalRefundPrice.Text = string.Format("{0:N0}", TotalRefundPrice);
            ltrTotalRefundCost.Text = string.Format("{0:N0}", TotalRefundCost);
            ltrTotalRefundFee.Text = string.Format("{0:N0}", TotalRefundFee);

            ltrTotalActualRevenue.Text = string.Format("{0:N0}", TotalActualRevenue);
            ltrTotalOtherFee.Text = string.Format("{0:N0}", TotalOtherFee);
            ltrTotalShippingFee.Text = string.Format("{0:N0}", TotalShippingFee);

            if (day > 1)
            {
                string chartLabelDays = "";
                string chartTotalProfit = "";
                string chartTotalRemainQuantity = "";

                List<string> dataDays = new List<string>();
                List<string> dataTotalProfit = new List<string>();
                List<string> dataTotalRemainQuantity = new List<string>();

                foreach (var item in reportModel)
                {
                    dataDays.Add(String.Format("'{0:d/M}'", item.DateDone));

                    double chartTotalRevenue = item.TotalSalePrice - item.TotalRefundPrice;
                    double chartTotalCost = item.TotalSaleCost - item.TotalRefundCost;
                    dataTotalProfit.Add((chartTotalRevenue - chartTotalCost - item.TotalSaleDiscount + item.TotalRefundFee).ToString());

                    dataTotalRemainQuantity.Add((item.TotalSoldQuantity - item.TotalRefundQuantity).ToString());
                }

                chartLabelDays = String.Join(", ", dataDays);
                chartTotalProfit = String.Join(", ", dataTotalProfit);
                chartTotalRemainQuantity = String.Join(", ", dataTotalRemainQuantity);

                StringBuilder html = new StringBuilder();
                html.Append("<script>");
                html.Append("var lineChartData = {");
                html.Append("	labels: [" + chartLabelDays + "],");
                html.Append("	datasets: [{");
                html.Append("		label: 'Lợi nhuận',");
                html.Append("		borderColor: 'rgb(255, 99, 132)',");
                html.Append("		backgroundColor: 'rgb(255, 99, 132)',");
                html.Append("		fill: false,");
                html.Append("		data: [" + chartTotalProfit + "],");
                html.Append("		yAxisID: 'y-axis-1',");
                html.Append("	}, {");
                html.Append("		label: 'Sản lượng',");
                html.Append("		borderColor: 'rgb(54, 162, 235)',");
                html.Append("		backgroundColor: 'rgb(54, 162, 235)',");
                html.Append("		fill: false,");
                html.Append("		data: [" + chartTotalRemainQuantity + "],");
                html.Append("		yAxisID: 'y-axis-2'");
                html.Append("	}]");
                html.Append("};");
                html.Append("</script>");

                ltrChartData.Text = html.ToString();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string fromdate = rFromDate.SelectedDate.ToString();
            string todate = rToDate.SelectedDate.ToString();

            Response.Redirect("/thong-ke-loi-nhuan?fromdate=" + fromdate + "&todate=" + todate + "");
        }
    }
}