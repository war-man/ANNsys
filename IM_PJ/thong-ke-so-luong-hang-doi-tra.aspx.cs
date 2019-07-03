using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IM_PJ.Controllers;

namespace IM_PJ
{
    public partial class thong_ke_so_luong_hang_doi_tra : System.Web.UI.Page
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

            var refund = RefundGoodController.TotalRefund(fromdate, todate);
            if(refund != null)
            {
                int total = 0;
                int kt = 0;
                int ct = 0;
                foreach (var item in refund)
                {
                    var refundg = RefundGoodDetailController.GetByRefundGoodsID(item.ID);
                    if(refundg != null)
                    {
                        foreach (var temp in refundg)
                        {
                            total +=Convert.ToInt32(temp.Quantity);
                            if(temp.RefundType == 2)
                            {
                                ct += Convert.ToInt32(temp.Quantity);
                            }
                            else
                            {
                                kt += Convert.ToInt32(temp.Quantity);
                            }
                        }
                    }
                }
                ltrList.Text += "<tr>";
                ltrList.Text += "<td style=\"text-align:center;\">" + total + "</td>";
                ltrList.Text += "<td>" + ct + "</td>";
                ltrList.Text += "<td>" + kt + "</td>";
             

                ltrList.Text += "</tr>";
            }
        }



        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string fromdate = rFromDate.SelectedDate.ToString();
            string todate = rToDate.SelectedDate.ToString();

            Response.Redirect("/thong-ke-so-luong-hang-doi-tra?fromdate=" + fromdate + "&todate=" + todate + "");
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("/thong-ke-so-luong-hang-doi-tra.aspx");
        }
    }
}