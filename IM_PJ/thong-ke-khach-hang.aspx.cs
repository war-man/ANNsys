using IM_PJ.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IM_PJ
{
    public partial class thong_ke_khach_hang : System.Web.UI.Page
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
            string fromdate = "";
            string todate = "";

            if (Request.QueryString["fromdate"] != null)
            {
                fromdate = Request.QueryString["fromdate"];
            }
            if (Request.QueryString["todate"] != null)
            {
                todate = Request.QueryString["todate"];
            }

            DateTime now = DateTime.Now;
            var start = new DateTime(now.Year, now.Month, 1, 0, 0, 0);
            if (!string.IsNullOrEmpty(fromdate))
            {
                rFromDate.SelectedDate = Convert.ToDateTime(fromdate);
            }
            else
            {
                fromdate = start.ToString();
            }
            if (!string.IsNullOrEmpty(todate))
            {
                rToDate.SelectedDate = Convert.ToDateTime(todate);
            }

            var od = CustomerController.TotalCustomer(fromdate, todate);
            if (od.Count() > 0)
            {
              
                foreach (var item in od)
                {
                    ltrList.Text += "<tr>";
                    ltrList.Text += "<td>" + item.CustomerName + "</td>";
                    ltrList.Text += "<td>" + item.CustomerPhone + "</td>";
                    ltrList.Text += "<td>" + item.CustomerEmail + "</td>";
                    ltrList.Text += "<td>" + item.CustomerAddress + "</td>";
                    string date = string.Format("{0:dd/MM/yyyy}", item.CreatedDate);
                    ltrList.Text += "<td>" + date + "</td>";
                   
                    ltrList.Text += "</tr>";
                }


            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string fromdate = rFromDate.SelectedDate.ToString();
            string todate = rToDate.SelectedDate.ToString();

            Response.Redirect("/thong-ke-khach-hang?fromdate=" + fromdate + "&todate=" + todate + "");
        }

       
    }
}