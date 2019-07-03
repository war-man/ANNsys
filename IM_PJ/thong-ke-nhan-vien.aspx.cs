using IM_PJ.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IM_PJ
{
    public partial class thong_ke_nhan_vien : System.Web.UI.Page
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
                            Response.Redirect("/dang-nhap");
                        }
                    }
                }
                else
                {
                    Response.Redirect("/dang-nhap");
                }
                LoadAccountInfo();
                LoadData();
            }
        }

        public void LoadData()
        {
            string accountName = String.Empty;
            DateTime fromdate = DateTime.Today;
            DateTime todate = fromdate.AddDays(1).AddMinutes(-1);

            double totalRevenue = 0;
            double averageRevenue = 0;
            double totalProfit = 0;
            int totalSoldQuantity = 0;
            int averageSoldQuantity = 0;
            int totalRefundQuantity = 0;
            int averageRefundQuantity = 0;
            int totalDays = 0;

            if (!String.IsNullOrEmpty(Request.QueryString["accountName"]))
            {
                accountName = Request.QueryString["accountName"];
            }

            if (!String.IsNullOrEmpty(Request.QueryString["fromdate"]))
            {
                fromdate = Convert.ToDateTime(Request.QueryString["fromdate"]);
            }

            if (!String.IsNullOrEmpty(Request.QueryString["todate"]))
            {
                todate = Convert.ToDateTime(Request.QueryString["todate"]).AddDays(1).AddMinutes(-1);
            }

            ddlAccountInfo.SelectedValue = accountName;
            rFromDate.SelectedDate = fromdate;
            rToDate.SelectedDate = todate;
            totalDays = Convert.ToInt32((todate - fromdate).TotalDays);

            // Load trang lan dau hoac la chua chon nhan vien
            if (String.IsNullOrEmpty(accountName))
            {
                this.ltrTotalRevenue.Text = String.Empty;
                this.ltrAverageRevenue.Text = String.Empty;
                this.ltrTotalSoldQuantity.Text = String.Empty;
                this.ltrAverageSoldQuantity.Text = String.Empty;
                this.ltrTotalRefundQuantity.Text = String.Empty;
                this.ltrAverageRefundQuantity.Text = String.Empty;
                this.ltrTotalProfit.Text = String.Empty;
                return;
            }

            var userReport = OrderController.getUserReport(accountName, fromdate, todate);

            totalRevenue = userReport.totalRevenue;
            averageRevenue = totalRevenue / totalDays;
            totalSoldQuantity = userReport.totalSoldQuantity;
            averageSoldQuantity = totalSoldQuantity / totalDays;

            var userRefundReport = RefundGoodController.getUserReport(accountName, fromdate, todate);

            totalRefundQuantity = userRefundReport.totalRefundQuantity;
            averageRefundQuantity = totalRefundQuantity / totalDays;
            totalProfit = (userReport.totalRevenue - userReport.totalCost) - (userRefundReport.totalRevenue - userRefundReport.totalCost) + userRefundReport.totalRefundFee;

            ltrTotalRevenue.Text = String.Format("{0:N0}", totalRevenue);
            ltrAverageRevenue.Text = String.Format("{0:N0}/ngày", averageRevenue);
            ltrTotalSoldQuantity.Text = totalSoldQuantity.ToString() + " cái";
            ltrAverageSoldQuantity.Text = averageSoldQuantity.ToString() + " cái/ngày";
            ltrTotalRefundQuantity.Text = totalRefundQuantity.ToString() + " cái";
            ltrAverageRefundQuantity.Text = averageRefundQuantity.ToString() + " cái/ngày";
            ltrTotalRemain.Text = (totalSoldQuantity - totalRefundQuantity).ToString() + " cái";
            ltrTotalProfit.Text = String.Format("{0:N0}", totalProfit);
        }

        /// <summary>
        /// Init drop down list account
        /// </summary>
        private void LoadAccountInfo()
        {
            ddlAccountInfo.Items.Clear();
            ddlAccountInfo.Items.Insert(0, new ListItem("Chọn nhân viên", "0"));
            var accounts = AccountController.GetAllUser();
            foreach (var acc in accounts)
            {
                this.ddlAccountInfo.Items.Add(new ListItem(acc.FullName, acc.Username));
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string accountName = this.ddlAccountInfo.SelectedValue;
            string fromdate = rFromDate.SelectedDate.ToString();
            string todate = rToDate.SelectedDate.ToString();

            Response.Redirect(String.Format("/thong-ke-nhan-vien?accountName={0}&fromdate={1}&todate={2}", accountName,fromdate, todate));
        }
    }
}