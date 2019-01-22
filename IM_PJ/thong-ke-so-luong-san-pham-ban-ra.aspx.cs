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

            int TotalSales = 0;
            var order = OrderDetailController.Report(fromdate.ToString(), todate.ToString());
            if (order != null)
            {
                foreach (var item in order)
                {
                    TotalSales += Convert.ToInt32(item.Quantity);
                }
            }

            int TotalRefund = 0;
            var refund = RefundGoodController.TotalRefund(fromdate.ToString(), todate.ToString());
            if (refund.Count() > 0)
            {
                foreach (var vl in refund)
                {
                    TotalRefund += Convert.ToInt32(vl.TotalQuantity);
                }
            }

            ltrTotalRemain.Text = (TotalSales - TotalRefund).ToString() + " cái";
            ltrAverageTotalRemain.Text = ((TotalSales - TotalRefund) / day).ToString() + " cái / ngày";
            ltrTotalSales.Text = TotalSales.ToString() + " cái";
            ltrAverageTotalSales.Text = (TotalSales / day).ToString() + " cái / ngày";
            ltrTotalRefund.Text = TotalRefund.ToString() + " cái";
            ltrAverageTotalRefund.Text = (TotalRefund / day).ToString() + " cái / ngày";

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string fromdate = rFromDate.SelectedDate.ToString();
            string todate = rToDate.SelectedDate.ToString();

            Response.Redirect("/thong-ke-so-luong-san-pham-ban-ra?fromdate=" + fromdate + "&todate=" + todate + "");
        }
    }
}