using IM_PJ.Controllers;
using IM_PJ.Models;
using MB.Extensions;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using static IM_PJ.Controllers.DeliveryController;

namespace IM_PJ
{
    public partial class print_delivery : System.Web.UI.Page
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
                        if (acc.RoleID == 0)
                        {

                        }
                        else if (acc.RoleID == 2)
                        {

                        }
                        else
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
            int shepperID = Request.QueryString["shipperid"].ToInt(0);
            string key = Request.QueryString["key"].ToString();
            var orderCookie = HttpContext.Current.Request.Cookies[key];

            if (shepperID == 0 || orderCookie == null)
            {
                if (shepperID == 0)
                    ltrPrintDelivery.Text = String.Format("ShipperID #{0} truyền vào có vấn đề", shepperID);
                if (orderCookie == null)
                    ltrPrintDelivery.Text = String.Format("Key #{0} không tồn tại trong cookies của trang", key);
            }
            else
            {
                var serializer = new JavaScriptSerializer();
                var orderIDList = serializer.Deserialize<List<int>>(Server.UrlDecode(orderCookie.Value));

                if (orderIDList != null && orderIDList.Count > 0)
                {
                    var data = DeliveryController.getDeliveryReport(orderIDList);
                    if (data.Count == 0)
                    {
                        ltrPrintDelivery.Text = "Không tìm thấy dư liệu để xuất hóa đơn giao hàng";
                    }
                    else
                    {
                        ltrPrintDelivery.Text = getReportHTML(data);
                        ltrPrintEnable.Text = "<div class='print-enable true'></div>";

                        // Update Delivery
                        string username = Request.Cookies["userLoginSystem"].Value;
                        var acc = AccountController.GetByUsername(username);
                        DeliveryController.udpateAfterPrint(shepperID, orderIDList, acc.ID);
                    }
                }
                else
                {
                    ltrPrintDelivery.Text = "Đã xay ra lỗi trong quá trình lấy thông tin order";
                }

                // Remove cookie
                orderCookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(orderCookie);
            }
        }

        public string getReportHTML(List<DeliveryReport> data)
        {
            var html = new StringBuilder();
            int index = 0;
            double totalQuantity = 0;
            double totalCollection = 0;

            html.AppendLine("<div class='hoadon'>");
            html.AppendLine("    <div class='all'>");
            html.AppendLine("        <div class='body'>");
            html.AppendLine("            <div class='table-2'>");
            html.AppendLine("                <table>");
            html.AppendLine("                    <colgroup>");
            html.AppendLine("                        <col />");
            html.AppendLine("                        <col />");
            html.AppendLine("                        <col />");
            html.AppendLine("                        <col />");
            html.AppendLine("                    </colgroup>");
            html.AppendLine("                    <thead>");
            html.AppendLine("                        <th>#</th>");
            html.AppendLine("                        <th>Nhà xe</th>");
            html.AppendLine("                        <th>Số lượng</th>");
            html.AppendLine("                        <th>Thu hộ</th>");
            html.AppendLine("                    </thead>");
            html.AppendLine("                    <tbody>");
            foreach(var item in data)
            {
                index += 1;
                totalQuantity += item.Quantity;
                totalCollection += item.Collection;

                html.AppendLine("                        <tr>");
                html.AppendLine(String.Format("                            <td>{0:#,###}</td>", index));
                html.AppendLine(String.Format("                            <td><strong>{0}</strong></td>", item.TransportName));
                html.AppendLine(String.Format("                            <td>{0:#,###}</td>", item.Quantity));
                html.AppendLine(String.Format("                            <td>{0:#,###}</td>", item.Collection));
                html.AppendLine("                        </tr>");
            }
            html.AppendLine("                        <tr>");
            html.AppendLine("                            <td colspan='2' style='text-align: right'>Tổng</td>");
            html.AppendLine(String.Format("                            <td colspan='2'>{0:#,###}</td>", totalQuantity));
            html.AppendLine("                        </tr>");
            html.AppendLine("                        <tr>");
            html.AppendLine("                            <td colspan='2'  style='text-align: right'>Thu hộ</td>");
            html.AppendLine(String.Format("                            <td colspan='2'>{0:#,###}</td>", totalCollection));
            html.AppendLine("                        </tr>");
            html.AppendLine("                    </tbody>");
            html.AppendLine("                </table>");
            html.AppendLine("            </div>");
            html.AppendLine("        </div>");
            html.AppendLine("    </div>");
            html.AppendLine("</div>");

            return html.ToString();
        }

    }
}