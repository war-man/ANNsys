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

namespace IM_PJ
{
    public partial class print_invoice_return : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["usernameLoginSystem"] != null)
                {
                    string username = Request.Cookies["usernameLoginSystem"].Value;
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
        public void printItemList(ref int ID, ref double TotalQuantity, ref double TotalOrder, ref string Print)
        {
            var orderdetails = RefundGoodDetailController.GetByRefundGoodsID(ID);
            if (orderdetails.Count > 0)
            {

                foreach (var item in orderdetails)
                {
                    TotalQuantity += Convert.ToDouble(item.Quantity);

                    int ProductType = Convert.ToInt32(item.ProductType);
                    double ItemPrice = Convert.ToDouble(item.SoldPricePerProduct);
                    string SKU = item.SKU;
                    string ProductName = "";
                    int SubTotal = (Convert.ToInt32(ItemPrice) - Convert.ToInt32(item.RefundFeePerProduct)) * Convert.ToInt32(item.Quantity);

                    Print += "<tr>";

                    if (ProductType == 1)
                    {
                        var product = ProductController.GetBySKU(SKU);
                        if (product != null)
                        {
                            ProductName = product.ProductTitle;
                            Print += "<td><strong>" + SKU + "</strong> - " + PJUtils.Truncate(ProductName, 20) + "</td>";
                        }
                    }
                    else
                    {
                        var productvariable = ProductVariableController.GetBySKU(SKU);
                        if (productvariable != null)
                        {
                            var parent_product = ProductController.GetByID(Convert.ToInt32(productvariable.ProductID));
                            if (parent_product != null)
                            {
                                ProductName = parent_product.ProductTitle;
                            }
                            Print += "<td><strong>" + SKU + "</strong> - " + PJUtils.Truncate(ProductName, 20) + "</td>";
                        }
                    }

                    Print += "<td>" + item.Quantity + "</td>";
                    Print += "<td>" + string.Format("{0:N0}", ItemPrice) + "</td>";
                    Print += "<td>" + string.Format("{0:N0}", Convert.ToDouble(item.RefundFeePerProduct)) + "</td>";
                    Print += "<td>" + string.Format("{0:N0}", SubTotal) + "</td>";
                    Print += "</tr>";

                    TotalOrder += SubTotal;
                }
            }
        }
        public void LoadData()
        {
            int ID = Request.QueryString["id"].ToInt(0);
            
            if (ID > 0)
            {
                var order = RefundGoodController.GetByID(ID);
                
                if (order != null)
                {
                    string error = "";
                    string Print = "";

                    double TotalQuantity = 0;
                    double TotalOrder = 0;

                    var orderdetails = RefundGoodDetailController.GetByRefundGoodsID(ID);

                    if (orderdetails.Count > 0)
                    {
                        printItemList(ref ID, ref TotalQuantity, ref TotalOrder, ref Print);

                        string productPrint = "";
                        string shtml = "";

                        productPrint += "<div class=\"body\">";
                        productPrint += "<div class=\"table-1\">";
                        productPrint += "<h1 class=\"invoice-return\">HÓA ĐƠN ĐỔI TRẢ HÀNG #" + order.ID + "</h1>";
                        
                        productPrint += "<table>";
                        productPrint += "<colgroup >";
                        productPrint += "<col class=\"col-left\"/>";
                        productPrint += "<col class=\"col-right\"/>";
                        productPrint += "</colgroup>";
                        productPrint += "<tbody>";
                        productPrint += "<tr>";
                        productPrint += "<td>Khách hàng</td>";
                        productPrint += "<td>" + order.CustomerName + "</td>";
                        productPrint += "</tr>";
                        productPrint += "<tr>";
                        productPrint += "<td>Điện thoại</td>";
                        productPrint += "<td>" + order.CustomerPhone + "</td>";
                        productPrint += "</tr>";
                        productPrint += "<tr>";
                        productPrint += "</tr>";
                        productPrint += "<tr>";
                        productPrint += "<td>Ngày tạo</td>";
                        string date = string.Format("{0:dd/MM/yyyy HH:mm}", order.CreatedDate);
                        productPrint += "<td>" + date + "</td>";
                        productPrint += "</tr>";
                        productPrint += "<tr>";
                        productPrint += "<td>Nhân viên</td>";
                        productPrint += "<td>" + order.CreatedBy + "</td>";
                        productPrint += "</tr>";

                        productPrint += "<tr>";
                        productPrint += "<td>Trạng thái</td>";
                        if (order.Status == 0)
                        {
                            productPrint += "<td>Chưa trừ tiền</td>";
                        }
                        else
                        {
                            productPrint += "<td>Đã trừ tiền</td>";
                        }
                        productPrint += "</tr>";

                        if (!string.IsNullOrEmpty(order.RefundNote)) {
                            productPrint += "<tr>";
                            productPrint += "<td>Ghi chú</td>";
                            productPrint += "<td>" + order.RefundNote + "</td>";
                            productPrint += "</tr>";
                        }

                        productPrint += "</tbody>";
                        productPrint += "</table>";
                        productPrint += "</div>";

                        productPrint += "<div class=\"table-2 print-invoice-return\">";
                        productPrint += "<table>";
                        productPrint += "<colgroup>";
                        productPrint += "<col class=\"sanpham\" />";
                        productPrint += "<col class=\"soluong\" />";
                        productPrint += "<col class=\"gia\" />";
                        productPrint += "<col class=\"gia\" />";
                        productPrint += "<col class=\"tong\"/>";
                        productPrint += "</colgroup>";
                        productPrint += "<thead>";
                        productPrint += "<th>Sản phẩm</th>";
                        productPrint += "<th>SL</th>";
                        productPrint += "<th>Giá</th>";
                        productPrint += "<th>Phí</th>";
                        productPrint += "<th>Tổng</th>";
                        productPrint += "</thead>";
                        productPrint += "<tbody>";
                        productPrint += Print;
                        productPrint += "<tr>";
                        productPrint += "<td colspan=\"4\">Số lượng</td>";
                        productPrint += "<td>" + TotalQuantity + "</td>";
                        productPrint += "</tr>";

                        if (TotalOrder != Convert.ToDouble(order.TotalPrice))
                        {
                            error += "Đơn hàng tính sai tổng tiền";
                        }

                        productPrint += "<tr>";
                        productPrint += "<td class=\"strong\" colspan=\"4\">Tổng tiền</td>";
                        productPrint += "<td class=\"strong\">" + string.Format("{0:N0}", TotalOrder) + "</td>";
                        productPrint += "</tr>";

                        productPrint += "<tr>";
                        productPrint += "<td colspan=\"4\">Phí đổi hàng</td>";
                        productPrint += "<td>" + string.Format("{0:N0}", Convert.ToDouble(order.TotalRefundFee)) + "</td>";
                        productPrint += "</tr>";

                        productPrint += "</tbody>";
                        productPrint += "</table>";
                        productPrint += "</div>";
                        productPrint += "</div>";

                        shtml += "<div class=\"hoadon\">";
                        shtml += "<div class=\"all\">";

                        shtml += productPrint;

                        shtml += "<div class=\"footer\"><h3>CẢM ƠN QUÝ KHÁCH !!!</h3>";
                        shtml += "<p>Lưu ý:</p>";
                        shtml += "<p>- Chúng tôi chỉ trả lại tiền mặt khi tổng tiền dưới 50.000đ.</p>";
                        shtml += "<p>- Đơn hàng đổi trả trên 50.000đ dùng để trừ tiền khi mua sản phẩm khác.</p>";
                        shtml += "<p>- Giá trên hóa đơn là giá bán ra đã trừ chiết khấu (nếu có).</p>";
                        shtml += "<p>- Phí đổi hàng áp dụng khi đổi hàng tồn hoặc đổi sang màu/mẫu khác.</p>";
                        shtml += "<p>- Miễn phí đổi size hoặc hàng lỗi cùng màu/mẫu như lúc đầu.</p>";
                        shtml += "</div>";
                        shtml += "</div>";
                        shtml += "</div>";


                        if (error != "")
                        {
                            ltrPrintInvoice.Text = "Xảy ra lỗi: " + error;
                            ltrPrintEnable.Text = "";
                        }
                        else
                        {
                            ltrPrintInvoice.Text = shtml;
                            ltrPrintEnable.Text = "<div class=\"print-enable true\"></div>";
                        }
                    }
                }
                else
                {
                    ltrPrintInvoice.Text = "Không tìm thấy đơn hàng " + ID;
                }
            }
            else
            {
                ltrPrintInvoice.Text = "Xảy ra lỗi!!!";
            }
        }
    }
}