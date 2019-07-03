using IM_PJ.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace IM_PJ
{
    public partial class thong_ke_phi_van_chuyen : System.Web.UI.Page
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
            double day = (todate - fromdate).TotalDays;

            double tongdonhang = 0;
            double totalshipping = 0;
            var od = OrderController.Report(fromdate.ToString(), todate.ToString());
            if (od.Count() > 0)
            {
                //var t = od.LastOrDefault();
                var o = od.FirstOrDefault();
                
                foreach (var item in od)
                {
                    totalshipping += Convert.ToInt32(item.FeeShipping);
                }
                tongdonhang = totalshipping / day;
            }

            ltrList.Text += "<tr>";
            ltrList.Text += "<td>" + string.Format("{0:N0}", totalshipping) + "đ"+ "</td>";
            ltrList.Text += "<td>" + string.Format("{0:N0}", tongdonhang) + "đ" + "</td>";
            ltrList.Text += "</tr>";

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string fromdate = rFromDate.SelectedDate.ToString();
            string todate = rToDate.SelectedDate.ToString();

            Response.Redirect("/thong-ke-phi-van-chuyen?fromdate=" + fromdate + "&todate=" + todate + "");
        }
    }
}