using IM_PJ.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IM_PJ
{
    public partial class thong_ke_so_luong_san_pham_ban_ra : System.Web.UI.Page
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
                if (Request.Cookies["userLoginSystem"] != null)
                {
                    string username = Request.Cookies["userLoginSystem"].Value;
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

            double TotalRevenue = reportModel.TotalSalePrice - reportModel.TotalRefundPrice;
            double TotalCost = reportModel.TotalSaleCost - reportModel.TotalRefundCost;
            double TotalProfit = TotalRevenue - TotalCost - reportModel.TotalSaleDiscount + reportModel.TotalRefundFee;
            double AverageProfitPerProduct = 0;
            int TotalRemainQuantity = reportModel.TotalSoldQuantity - reportModel.TotalRefundQuantity;
            if (reportModel.TotalNumberOfOrder > 0)
            {
                AverageProfitPerProduct = Math.Ceiling(TotalProfit / TotalRemainQuantity);
            }

            ltrTotalRemain.Text = (TotalRemainQuantity).ToString() + " cái";
            ltrAverageTotalRemain.Text = (TotalRemainQuantity / day).ToString() + " cái/ngày";
            ltrTotalSales.Text = (reportModel.TotalSoldQuantity).ToString() + " cái";
            ltrAverageTotalSales.Text = (reportModel.TotalSoldQuantity / day).ToString() + " cái / ngày";
            ltrTotalRefund.Text = (reportModel.TotalRefundQuantity).ToString() + " cái";
            ltrAverageTotalRefund.Text = (reportModel.TotalRefundQuantity / day).ToString() + " cái / ngày";
            ltrAverageProfitPerProduct.Text = string.Format("{0:N0}", AverageProfitPerProduct) + " đ / cái";

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string fromdate = rFromDate.SelectedDate.ToString();
            string todate = rToDate.SelectedDate.ToString();

            Response.Redirect("/thong-ke-so-luong-san-pham-ban-ra?fromdate=" + fromdate + "&todate=" + todate + "");
        }
    }
}