using IM_PJ.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IM_PJ
{
    public partial class thong_ke_doanh_thu : System.Web.UI.Page
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

            var od = OrderController.GetProfitReport(fromdate, todate);

            double TotalSale = od.TotalSalePrice - od.TotalRefundPrice - od.TotalSaleDiscount;

            ltrTotalNumberOfOrder.Text =  od.TotalNumberOfOrder.ToString() + " đơn";
            ltrNumberOfOrderPerDay.Text = (od.TotalNumberOfOrder / day).ToString() + " đơn / ngày";
            ltrTotalRevenue.Text = string.Format("{0:N0}", TotalSale) + "đ";
            ltrAverageRevenue.Text = string.Format("{0:N0}", TotalSale / day) + "đ / ngày";

            if (od.TotalNumberOfOrder == 0)
            {
                ltrRevenuePerOrder.Text = "0";
            }
            else
            {
                ltrRevenuePerOrder.Text = string.Format("{0:N0}", TotalSale / od.TotalNumberOfOrder) + "đ / đơn";
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string fromdate = rFromDate.SelectedDate.ToString();
            string todate = rToDate.SelectedDate.ToString();

            Response.Redirect("/thong-ke-doanh-thu?fromdate=" + fromdate + "&todate=" + todate + "");
        }
    }
}