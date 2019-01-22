using IM_PJ.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IM_PJ
{
    public partial class thong_ke_chiet_khau : System.Web.UI.Page
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
            DateTime fromdate = DateTime.Today;
            DateTime todate = fromdate.AddDays(1).AddMinutes(-1);

            if (!String.IsNullOrEmpty(Request.QueryString["fromdate"]))
            {
                fromdate = Convert.ToDateTime(Request.QueryString["fromdate"]);
            }

            if (!String.IsNullOrEmpty(Request.QueryString["todate"]))
            {
                todate = Convert.ToDateTime(Request.QueryString["todate"]);

                if (fromdate == todate)
                {
                    todate = fromdate.AddDays(1).AddMinutes(-1);
                }
            }

            rFromDate.SelectedDate = fromdate;
            rToDate.SelectedDate = todate;
            double day = (todate - fromdate).TotalDays;

            var od = OrderController.Report(fromdate.ToString(), todate.ToString());
            if (od.Count() > 0)
            {
                var o = od.FirstOrDefault();

                double totaldiscount = 0;
                foreach (var item in od)
                {
                    totaldiscount += Convert.ToInt32(item.TotalDiscount);
                }

                double discountperday = totaldiscount / day;
                ltrList.Text += "<tr>";
                ltrList.Text += "   <td>" + string.Format("{0:N0}", totaldiscount) + "đ" + "</td>";
                ltrList.Text += "   <td>" + string.Format("{0:N0}", discountperday) + "đ" + "</td>";
                ltrList.Text += "</tr>";
            }

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string fromdate = rFromDate.SelectedDate.ToString();
            string todate = rToDate.SelectedDate.ToString();

            Response.Redirect("/thong-ke-chiet-khau?fromdate=" + fromdate + "&todate=" + todate + "");
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("/thong-ke-chiet-khau.aspx");
        }
    }
}