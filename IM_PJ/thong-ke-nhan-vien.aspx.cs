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

            long totalSales = 0;
            double averageSales = 0D;
            int totalOutput = 0;
            int averageOutput = 0;
            int totalRefund = 0;
            int averageRefund = 0;
            double totalDays = 0D;

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
            totalDays = (todate - fromdate).TotalDays;

            // Load trang lan dau hoac la chua chon nhan vien
            if (String.IsNullOrEmpty(accountName))
            {
                this.ltrTotalSales.Text = String.Empty;
                this.ltrAverageSales.Text = String.Empty;
                this.ltrTotalOutput.Text = String.Empty;
                this.ltrAverageOutput.Text = String.Empty;
                this.ltrTotalRefund.Text = String.Empty;
                this.ltrAverageRefund.Text = String.Empty;

                return;
            }


            totalSales = OrderController.GetTotalPriceByAccount(accountName, fromdate, todate);
            averageSales = Math.Ceiling(totalSales / totalDays);


            totalOutput = OrderController.GetTotalProductSalesByAccount(accountName, fromdate, todate);
            averageOutput = totalOutput / Convert.ToInt32(totalDays);

            totalRefund = RefundGoodController.GetTotalRefundByAccount(accountName, fromdate, todate);
            averageRefund = totalRefund / Convert.ToInt32(totalDays);


            ltrTotalSales.Text = String.Format("{0:N0}  đ", totalSales);
            ltrAverageSales.Text = String.Format("{0:N0}   đ/ngày", averageSales);
            ltrTotalOutput.Text = totalOutput.ToString() + " cái";
            ltrAverageOutput.Text = averageOutput.ToString() + " cái/ngày";
            ltrTotalRefund.Text = totalRefund.ToString() + " cái";
            ltrAverageRefund.Text = averageRefund.ToString() + " cái/ngày";
            ltrTotalRemain.Text = (totalOutput - totalRefund).ToString() + " cái";
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