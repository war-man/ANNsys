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
    public partial class print_order_image : System.Web.UI.Page
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
        public void printItemList(ref int ID, ref double TotalQuantity, ref double TotalOrder, ref string Print)
        {
            var orderdetails = OrderDetailController.GetByOrderID(ID);
            if (orderdetails.Count > 0)
            {
                int t = 0;
                int print = 1;
                foreach (var item in orderdetails)
                {
                    TotalQuantity += Convert.ToDouble(item.Quantity);

                    int ProductType = Convert.ToInt32(item.ProductType);
                    double ItemPrice = Convert.ToDouble(item.Price);
                    string SKU = item.SKU;
                    string ProductName = "";
                    string ProductImage = "/App_Themes/Ann/image/placeholder.png";
                    int SubTotal = Convert.ToInt32(ItemPrice) * Convert.ToInt32(item.Quantity);

                    t++;
                    Print += "<tr>";
                    Print += "<td>" + t + "</td>";

                    if (ProductType == 1)
                    {
                        var product = ProductController.GetBySKU(SKU);
                        if (product != null)
                        {
                            ProductName = product.ProductTitle;
                            if (!string.IsNullOrEmpty(product.ProductImage))
                            {
                                ProductImage = product.ProductImage;
                            }
                            Print += "<td><image src=\"" + ProductImage + "\" /></td> ";
                            Print += "<td><strong>" + SKU + "</strong> - " + PJUtils.Truncate(ProductName, 30) + "</td> ";
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

                                if (string.IsNullOrEmpty(productvariable.Image))
                                {
                                    ProductImage = parent_product.ProductImage;
                                }
                                else
                                {
                                    ProductImage = productvariable.Image;
                                }
                            }
                            
                            Print += "<td><image src=\"" + ProductImage + "\" /></td>";
                            Print += "<td><p><strong>" + SKU + "</strong> - " + PJUtils.Truncate(ProductName, 30) + "</p><p class=\"variable\">" + item.ProductVariableDescrition.Replace("|", ". ") + "</p></td> ";
                        }
                    }

                    Print += "<td>" + item.Quantity + "</td>";
                    Print += "<td>" + string.Format("{0:N0}", ItemPrice) + "</td>";
                    Print += "<td>" + string.Format("{0:N0}", SubTotal) + "</td>";
                    Print += "</tr>";

                    TotalOrder += SubTotal;
                    
                    if(t % 10 == 0)
                    {
                        if(t == print * 10)
                        {
                            continue;
                        }
                        Print += "</tbody>";
                        Print += "</table>";
                        Print += "</div>";
                        Print += "</div>";
                        Print += "</div>";
                        Print += "</div>";

                        Print += "<div class=\"print-order-image\">";
                        Print += "<div class=\"all print print-" + print + "\">";
                        Print += "<div class=\"body\">";
                        Print += "<div class=\"table-2\">";
                        Print += "<table>";
                        Print += "<colgroup>";
                        Print += "<col class=\"order-item\" />";
                        Print += "<col class=\"image\" />";
                        Print += "<col class=\"name\" />";
                        Print += "<col class=\"quantity\" />";
                        Print += "<col class=\"price\" />";
                        Print += "<col class=\"subtotal\"/>";
                        Print += "</colgroup>";
                        Print += "<thead>";
                        Print += "<th>#</th>";
                        Print += "<th>Hình ảnh</th>";
                        Print += "<th>Sản phẩm</th>";
                        Print += "<th>SL</th>";
                        Print += "<th>Giá</th>";
                        Print += "<th>Tổng</th>";
                        Print += "</thead>";
                        Print += "<tbody>";
                        print++;
                    }
                }
            }
        }
        public void LoadData()
        {
            int ID = Request.QueryString["id"].ToInt(0);
            
            if (ID > 0)
            {
                var order = OrderController.GetByID(ID);
                
                if (order != null)
                {
                    string error = "";
                    string Print = "";

                    double TotalQuantity = 0;
                    double TotalOrder = 0;

                    var orderdetails = OrderDetailController.GetByIDSortBySKU(ID);

                    var numberOfOrders = OrderController.GetByCustomerID(Convert.ToInt32(order.CustomerID));

                    if (orderdetails.Count > 0)
                    {
                        printItemList(ref ID, ref TotalQuantity, ref TotalOrder, ref Print);

                        string productPrint = "";
                        string shtml = "";

                        productPrint += "<div class=\"body\">";
                        productPrint += "<div class=\"table-1\">";
                        productPrint += "<h1>XÁC NHẬN ĐƠN HÀNG #" + order.ID + "</h1>";
                        productPrint += "<div class=\"note\">";
                        productPrint += "<p>- Lưu ý hình ảnh sản phẩm trên đơn hàng có thể hiển thị không đúng.</p>";
                        productPrint += "<p>- Quý khách vui lòng kiểm tra thuộc tính sản phẩm (Mã, Màu, Mẫu, Size).</p>";
                        productPrint += "<p>- Nếu có sai sót, quý khách có thể gọi điện thoại để thông báo cho nhân viên.</p>";
                        productPrint += "</div>";
                        productPrint += "<table>";
                        productPrint += "<colgroup >";
                        productPrint += "<col class=\"col-left\"/>";
                        productPrint += "<col class=\"col-right\"/>";
                        productPrint += "</colgroup>";
                        productPrint += "<tbody>";
                        productPrint += "<tr>";
                        productPrint += "<td>Khách hàng</td>";
                        productPrint += "<td class=\"customer-name\">" + order.CustomerName + "</td>";
                        productPrint += "</tr>";
                        productPrint += "<tr>";
                        productPrint += "<td>Điện thoại</td>";
                        productPrint += "<td>" + order.CustomerPhone + "</td>";
                        productPrint += "</tr>";
                        productPrint += "<tr>";
                        productPrint += "<td>Ngày tạo</td>";
                        string date = string.Format("{0:dd/MM/yyyy HH:mm}", order.CreatedDate);
                        productPrint += "<td>" + date + "</td>";
                        productPrint += "</tr>";
                        
                        if (!string.IsNullOrEmpty(order.DateDone.ToString()))
                        {
                            productPrint += "<tr>";
                            productPrint += "<td>Hoàn tất</td>";
                            string datedone = string.Format("{0:dd/MM/yyyy HH:mm}", order.DateDone);
                            productPrint += "<td>" + datedone + "</td>";
                        }
                            
                        productPrint += "</tr>";
                        productPrint += "<tr>";
                        productPrint += "<td>Nhân viên</td>";
                        productPrint += "<td>" + order.CreatedBy + "</td>";
                        productPrint += "</tr>";

                        if (!string.IsNullOrEmpty(order.OrderNote)) {
                            productPrint += "<tr>";
                            productPrint += "<td>Ghi chú</td>";
                            productPrint += "<td>" + order.OrderNote + "</td>";
                            productPrint += "</tr>";
                        }

                        productPrint += "</tbody>";
                        productPrint += "</table>";
                        productPrint += "</div>";
                        productPrint += "<div class=\"table-2\">";
                        productPrint += "<table>";
                        productPrint += "<colgroup>";
                        productPrint += "<col class=\"order-item\" />";
                        productPrint += "<col class=\"image\" />";
                        productPrint += "<col class=\"name\" />";
                        productPrint += "<col class=\"quantity\" />";
                        productPrint += "<col class=\"price\" />";
                        productPrint += "<col class=\"subtotal\"/>";
                        productPrint += "</colgroup>";
                        productPrint += "<thead>";
                        productPrint += "<th>#</th>";
                        productPrint += "<th>Hình ảnh</th>";
                        productPrint += "<th>Sản phẩm</th>";
                        productPrint += "<th>SL</th>";
                        productPrint += "<th>Giá</th>";
                        productPrint += "<th>Tổng</th>";
                        productPrint += "</thead>";
                        productPrint += "<tbody>";
                        productPrint += Print;
                        productPrint += "<tr>";
                        productPrint += "<td colspan=\"5\" class=\"align-right\">Số lượng</td>";
                        productPrint += "<td>" + TotalQuantity + "</td>";
                        productPrint += "</tr>";
                        productPrint += "<tr>";
                        productPrint += "<td colspan=\"5\" class=\"align-right\">Thành tiền</td>";
                        productPrint += "<td>" + string.Format("{0:N0}", TotalOrder) + "</td>";
                        productPrint += "</tr>";

                        double TotalPrice = TotalOrder;

                        if (order.DiscountPerProduct > 0)
                        {
                            var TotalDiscount = Convert.ToDouble(order.DiscountPerProduct) * Convert.ToDouble(TotalQuantity);
                            TotalOrder = TotalOrder - TotalDiscount;
                            TotalPrice = TotalPrice - TotalDiscount;
                            productPrint += "<tr>";
                            productPrint += "<td colspan=\"5\" class=\"align-right\">Chiết khấu mỗi cái </td>";
                            productPrint += "<td>" + string.Format("{0:N0}", Convert.ToDouble(order.DiscountPerProduct)) + "</td>";
                            productPrint += "</tr>";
                            productPrint += "<tr>";
                            productPrint += "<td colspan=\"5\" class=\"align-right\">Trừ chiết khấu</td>";
                            productPrint += "<td>" + string.Format("{0:N0}", TotalDiscount) + "</td>";
                            productPrint += "</tr>";
                            productPrint += "<tr>";
                            productPrint += "<td colspan=\"5\" class=\"align-right\">Sau chiết khấu</td>";
                            productPrint += "<td>" + string.Format("{0:N0}", TotalOrder) + "</td>";
                            productPrint += "</tr>";
                        }
                        
                        if (order.RefundsGoodsID != null)
                        {
                            var refund = RefundGoodController.GetByID(Convert.ToInt32(order.RefundsGoodsID));
                            if (refund != null)
                            {
                                TotalOrder = TotalOrder - Convert.ToDouble(refund.TotalPrice);

                                productPrint += "<tr>";
                                productPrint += "<td colspan=\"5\" class=\"align-right\">Trừ tiền hàng trả (đơn " + order.RefundsGoodsID + ")</td>";
                                productPrint += "<td>" + string.Format("{0:N0}", Convert.ToDouble(refund.TotalPrice)) + "</td>";
                                productPrint += "</tr>";

                                productPrint += "<tr>";
                                productPrint += "<td colspan=\"5\" class=\"align-right\">Tổng tiền còn lại</td>";
                                productPrint += "<td>" + string.Format("{0:N0}", TotalOrder) + "</td>";
                                productPrint += "</tr>";
                            }
                            else
                            {
                                error += "Không tìm thấy đơn hàng đổi trả " + order.RefundsGoodsID.ToString();
                            }
                        }

                        if (Convert.ToDouble(order.FeeShipping) > 0)
                        {
                            TotalOrder = TotalOrder + Convert.ToDouble(order.FeeShipping);
                            TotalPrice = TotalPrice + Convert.ToDouble(order.FeeShipping);
                            productPrint += "<tr>";
                            productPrint += "<td colspan=\"5\" class=\"align-right\">Phí vận chuyển</td>";
                            productPrint += "<td>" + string.Format("{0:N0}", Convert.ToDouble(order.FeeShipping)) + "</td>";
                            productPrint += "</tr>";
                        }

                        if (Convert.ToDouble(order.OtherFeeValue) != 0)
                        {
                            TotalOrder = TotalOrder + Convert.ToDouble(order.OtherFeeValue);
                            TotalPrice = TotalPrice + Convert.ToDouble(order.OtherFeeValue);
                            productPrint += "<tr>";
                            productPrint += "<td colspan=\"5\" class=\"align-right\">" + order.OtherFeeName + "</td>";
                            productPrint += "<td>" + string.Format("{0:N0}", Convert.ToDouble(order.OtherFeeValue)) + "</td>";
                            productPrint += "</tr>";
                        }

                        if (TotalPrice != Convert.ToDouble(order.TotalPrice))
                        {
                            error += "Đơn hàng tính sai tổng tiền";
                        }

                        productPrint += "<tr>";
                        productPrint += "<td class=\"strong align-right\" colspan=\"5\">TỔNG CỘNG</td>";
                        productPrint += "<td class=\"strong\">" + string.Format("{0:N0}", TotalOrder) + "</td>";
                        productPrint += "</tr>";
                        
                        
                        productPrint += "</tbody>";
                        productPrint += "</table>";
                        productPrint += "</div>";
                        productPrint += "</div>";

                        string dateOrder = string.Format("{0:dd/MM/yyyy HH:mm}", order.DateDone);

                        shtml += "<div class=\"print-order-image\">";
                        shtml += "<div class=\"all print print-0\">";
                        

                        if (numberOfOrders.Count < 4)
                        {
                            shtml += "<div class=\"head\">";
                            string address = "";
                            string phone = "";
                            var agent = AgentController.GetByID(Convert.ToInt32(order.AgentID));
                            if (agent != null)
                            {
                                address = agent.AgentAddress;
                                phone = agent.AgentPhone;
                            }
                            shtml += "<div class=\"logo\"><img src=\"App_Themes/Ann/image/logo.png\" /></div>";
                            shtml += "<div class=\"info\">";

                            shtml += "<div class=\"ct\">";
                            shtml += "<div class=\"ct-title\"></div>";
                            shtml += "<div class=\"ct-detail\"> " + address + "</div>";
                            shtml += "</div>";

                            shtml += "<div class=\"ct\">";
                            shtml += "<div class=\"ct-title\"> </div>";
                            shtml += "<div class=\"ct-detail\"> " + phone + "</div>";
                            shtml += "</div>";

                            shtml += "</div>";
                            shtml += "</div>";
                        }

                        

                        shtml += productPrint;

                        shtml += "</div>";
                        shtml += "</div>";

                        if (error != "")
                        {
                            ltrPrintInvoice.Text = "Xảy ra lỗi: " + error;
                        }
                        else
                        {
                            ltrPrintInvoice.Text = shtml;
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