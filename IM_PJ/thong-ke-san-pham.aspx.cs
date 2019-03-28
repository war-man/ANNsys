using IM_PJ.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IM_PJ
{
    public partial class thong_ke_san_pham : System.Web.UI.Page
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
            string SKU = String.Empty;
            DateTime fromdate = DateTime.Today;
            DateTime todate = fromdate.AddDays(1).AddMinutes(-1);

            int totalRemainQuantity = 0;
            int totalSoldQuantity = 0;
            int totalRefundQuantity = 0;
            double totalCost = 0;
            double totalProfit = 0;
            double totalRefundProfit = 0;
            double totalRevenue = 0;

            int totalDays = 0;

            if (!String.IsNullOrEmpty(Request.QueryString["SKU"]))
            {
                SKU = Request.QueryString["SKU"];

                var product = ProductController.GetBySKU(SKU);
                if (product != null)
                {
                    fromdate = Convert.ToDateTime(product.CreatedDate);
                }
            }

            if (!String.IsNullOrEmpty(Request.QueryString["fromdate"]))
            {
                fromdate = Convert.ToDateTime(Request.QueryString["fromdate"]);
            }

            if (!String.IsNullOrEmpty(Request.QueryString["todate"]))
            {
                todate = Convert.ToDateTime(Request.QueryString["todate"]).AddDays(1).AddMinutes(-1);
            }

            txtTextSearch.Text = SKU;
            rFromDate.SelectedDate = fromdate;
            rToDate.SelectedDate = todate;
            totalDays = Convert.ToInt32((todate - fromdate).TotalDays);

            if(totalDays <= 0)
            {
                totalDays = 1;
            }

            if (String.IsNullOrEmpty(SKU))
            {
                this.ltrTotalRemain.Text = String.Empty;
                this.ltrTotalRemainPerDay.Text = String.Empty;
                this.ltrTotalSold.Text = String.Empty;
                this.ltrTotalRefund.Text = String.Empty;
                this.ltrTotalProfit.Text = String.Empty;
                this.ltrTotalRevenue.Text = String.Empty;
                this.ltrTotalStock.Text = String.Empty;
                this.ltrTotalStockValue.Text = String.Empty;

                return;
            }

            var productReport = OrderController.getProductReport(SKU, fromdate, todate);
            totalSoldQuantity = productReport.totalSold;
            totalRevenue = productReport.totalRevenue;
            totalCost = productReport.totalCost;
            totalProfit = totalRevenue - totalCost;

            var productRefundReport = RefundGoodController.getRefundProductReport(SKU, fromdate, todate);
            totalRefundQuantity = productRefundReport.totalRefund;

            totalRefundProfit = productRefundReport.totalRevenue - productRefundReport.totalCost;

            totalRemainQuantity = totalSoldQuantity - totalRefundQuantity;

            var productStockReport = ProductController.getProductStockReport(SKU);

            ltrTotalSold.Text = totalSoldQuantity.ToString();
            ltrTotalRefund.Text = totalRefundQuantity.ToString();
            ltrTotalRemain.Text = totalRemainQuantity.ToString();
            ltrTotalRemainPerDay.Text = (totalRemainQuantity / totalDays).ToString() + " cái/ngày";
            ltrTotalRevenue.Text = string.Format("{0:N0}", totalRevenue);
            ltrTotalProfit.Text = string.Format("{0:N0}", (totalProfit - totalRefundProfit + productRefundReport.totalRefundFee));
            ltrTotalStock.Text = productStockReport.totalStock.ToString();
            ltrTotalStockValue.Text = string.Format("{0:N0}", productStockReport.totalStockValue);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string SKU = txtTextSearch.Text;

            string fromdate = rFromDate.SelectedDate.ToString();

            var product = ProductController.GetBySKU(SKU);
            if(product != null)
            {
                fromdate = product.CreatedDate.ToString();
            }
            
            string todate = rToDate.SelectedDate.ToString();

            Response.Redirect(String.Format("/thong-ke-san-pham?SKU={0}&fromdate={1}&todate={2}", SKU, fromdate, todate));
        }
    }
}