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
    public partial class print_shipping_note : System.Web.UI.Page
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
            string username = Request.Cookies["userLoginSystem"].Value;
            var acc = AccountController.GetByUsername(username);

            string error = "";
            
            int ID = Request.QueryString["id"].ToInt(0);

            String rowHtml = String.Empty;

            var order = OrderController.GetByID(ID);

            if (order != null)
            {
                if (order.PaymentStatus == 1)
                {
                    error += "<p>- Đơn hàng này <strong>chưa thanh toán</strong>!</p>";
                }

                if (order.ExcuteStatus != 2)
                {
                    error += "<p>- Đơn hàng này <strong>chưa hoàn tất</strong>!</p>";
                }

                if (order.ShippingType == 1)
                {
                    error += "<p>- Đơn hàng này đang có phương thức giao hàng: <strong>Lấy trực tiếp</strong>. Hãy chuyển sang phương thức khác!</p>";
                }

                string address = "";
                string phone = "0914615408 - 0918567409";
                string leader = "";
                var agent = AgentController.GetByID(Convert.ToInt32(order.AgentID));

                if (agent != null)
                {
                    address = agent.AgentAddress;
                    leader = agent.AgentLeader;
                }

                double TotalOrder = Convert.ToDouble(order.TotalPrice);

                if (order.RefundsGoodsID != null)
                {
                    var refund = RefundGoodController.GetByID(Convert.ToInt32(order.RefundsGoodsID));
                    if (refund != null)
                    {
                        TotalOrder = TotalOrder - Convert.ToDouble(refund.TotalPrice);
                    }
                    else
                    {
                        error += "<p>Không tìm thấy đơn hàng đổi trả " + order.RefundsGoodsID.ToString() + " (có thể đã bị xóa khi làm lại đơn đổi trả). Thêm lại đơn hàng đổi trả nhé!</p>";
                    }
                }

                rowHtml += Environment.NewLine + String.Format("<div class=\"table\">");
                rowHtml += Environment.NewLine + String.Format("    <div class=\"top-left\">");
                rowHtml += Environment.NewLine + String.Format("        <p><span>Người gửi: <span class=\"name\">{0}</span></span></p>", leader);
                rowHtml += Environment.NewLine + String.Format("        <p><span>{0}</span></p>", phone);
                rowHtml += Environment.NewLine + String.Format("        <p><span>{0}</span></p>", address);
                rowHtml += Environment.NewLine + String.Format("        <p><span class=\"web\">ANN.COM.VN</span></p>");
                rowHtml += Environment.NewLine + String.Format("    </div>");
                rowHtml += Environment.NewLine + String.Format("    <div class=\"bottom-left\">");
                if (order.PaymentType == 3)
                {
                    rowHtml += Environment.NewLine + String.Format("        <p><span class=\"cod\">Thu hộ: {0}</span></p>", string.Format("{0:N0}", TotalOrder));
                }
                else
                {
                    rowHtml += Environment.NewLine + String.Format("        <p><span class=\"cod\">Thu hộ: KHÔNG</span></p>");
                }

                rowHtml += Environment.NewLine + String.Format("        <p><span>Mã đơn hàng: {0}</span></p>", order.ID);
                rowHtml += Environment.NewLine + String.Format("        <p><span>Nhân viên: {0}</span></p>", order.CreatedBy);

                rowHtml += Environment.NewLine + String.Format("    </div>");
                rowHtml += Environment.NewLine + String.Format("    <div class=\"top-right\">");
                rowHtml += Environment.NewLine + String.Format("        <img class=\"img\" src=\"https://ann.com.vn/wp-content/uploads/ANN-logo-3.png\">");

                var company = TransportCompanyController.GetTransportCompanyByID(Convert.ToInt32(order.TransportCompanyID));
                string transportCompany = "";
                string transportCompanyPhone = "";
                string transportCompanyAddress = "";
                string transportCompanyNote = "";

                string CustomerAddress = order.CustomerAddress.ToTitleCase();

                if (company != null)
                {
                    transportCompany = "<strong>" + company.CompanyName.ToTitleCase() + "</strong>";
                    if(company.CompanyPhone != "")
                    {
                        transportCompanyPhone = "<span class=\"transport-info\">(" + company.CompanyPhone + ")</span>";
                    }

                    transportCompanyAddress = "<p class=\"transport-info\">" + company.CompanyAddress.ToTitleCase() + "</p>";

                    if(company.Note != "")
                    {
                        transportCompanyNote = "<span class=\"transport-info\"> - " + company.Note.ToTitleCase() + "</span>";
                    }

                    var subID = Convert.ToInt32(order.TransportCompanySubID);
                    var shipto = TransportCompanyController.GetReceivePlaceByID(company.ID, subID);
                    if (shipto != null && subID > 0)
                    {
                        CustomerAddress = "<span class=\"phone\">" + shipto.ShipTo.ToTitleCase() + "</span>";
                    }
                    else
                    {
                        error += "<p>- Đơn hàng này gửi chành xe " + transportCompany + " nhưng <strong>chưa chọn Nơi nhận</strong>!</p>";
                    }
                }

                if (order.ShippingType == 2)
                {
                    string PostalDeliveryType = "Thường";
                    if (order.PostalDeliveryType == 2)
                    {
                        PostalDeliveryType = "Nhanh";
                    }

                    if (!string.IsNullOrEmpty(order.ShippingCode))
                    {
                        rowHtml += Environment.NewLine + String.Format("        <p class=\"delivery\"><span><strong>Gửi Bưu điện - {0}:</strong> {1}</span></p>", PostalDeliveryType, order.ShippingCode);
                    }
                    else
                    {
                        error += "<p>- Đơn hàng này <strong>gửi Bưu điện</strong> nhưng <strong>chưa nhập</strong> MÃ VẬN ĐƠN!</p>";
                    }

                    if(order.PaymentType != 3 && acc.RoleID != 0)
                    {
                        error += "<p>- Đơn hàng này <strong>gửi Bưu điện</strong> nhưng <strong>Không gửi thu hộ</strong>. Nếu có lý do thì báo chị Ngọc xử lý nhé!</p>";
                    }
                }
                else if (order.ShippingType == 3)
                {
                    rowHtml += Environment.NewLine + String.Format("        <p class=\"delivery\"><span>Dịch vụ Proship</span></p>");
                    if (!string.IsNullOrEmpty(order.ShippingCode))
                    {
                        rowHtml += Environment.NewLine + String.Format("        <p class=\"delivery\"><span>{0}</span></p>", order.ShippingCode);
                    }
                    else
                    {
                        error += "<p>- Đơn hàng này <strong>gửi Dịch vụ Proship</strong> nhưng <strong>chưa nhập</strong> MÃ VẬN ĐƠN!</p>";
                    }

                    if (order.PaymentType != 3 && acc.RoleID != 0)
                    {
                        error += "<p>- Đơn hàng này <strong>gửi Proship</strong> nhưng <strong>Không gửi thu hộ</strong>. Nếu có lý do thì báo chị Ngọc xử lý nhé!</p>";
                    }
                }
                else if (order.ShippingType == 4)
                {
                    if (transportCompany == "")
                    {
                        error += "<p>- Đơn hàng này <strong>gửi xe</strong> nhưng <strong>chưa chọn Chành xe</strong> nào!</p>";
                    }
                    else
                    {
                        rowHtml += Environment.NewLine + String.Format("        <p class=\"delivery\"><span>Gửi xe: {0} {1} {2}</p>", transportCompany, transportCompanyPhone, transportCompanyNote);

                        // Lấy thông tin xe ra gửi
                        rowHtml += Environment.NewLine + String.Format("        {0}", transportCompanyAddress);
                    }
                }
                else if (order.ShippingType == 5)
                {
                    rowHtml += Environment.NewLine + String.Format("        <p class=\"delivery\"><span>Nhân viên giao hàng</span></p>");
                }
                else if (order.ShippingType == 6)
                {
                    rowHtml += Environment.NewLine + String.Format("        <p class=\"delivery\"><span>GHTK</span></p>");
                    if (!string.IsNullOrEmpty(order.ShippingCode))
                    {
                        rowHtml += Environment.NewLine + String.Format("        <p class=\"delivery\"><span>{0}</span></p>", order.ShippingCode);
                    }
                    else
                    {
                        error += "<p>- Đơn hàng này <strong>gửi GHTK</strong> nhưng <strong>chưa nhập</strong> MÃ VẬN ĐƠN!</p>";
                    }

                    if (order.PaymentType != 3 && acc.RoleID != 0)
                    {
                        error += "<p>- Đơn hàng này <strong>gửi GHTK</strong> nhưng <strong>Không gửi thu hộ</strong>. Nếu có lý do thì báo chị Ngọc xử lý nhé!</p>";
                    }
                }
                else if (order.ShippingType == 7)
                {
                    rowHtml += Environment.NewLine + String.Format("        <p class=\"delivery\"><span>Viettel</span></p>");
                }

                if (order.PaymentType == 3)
                {
                    rowHtml += Environment.NewLine + String.Format("        <p><span class=\"cod\">Thu hộ: {0}</span></p>", string.Format("{0:N0}", TotalOrder));
                }
                else
                {
                    rowHtml += Environment.NewLine + String.Format("        <p><span class=\"cod\">Thu hộ: KHÔNG</span></p>");
                }
                rowHtml += Environment.NewLine + String.Format("    </div>");
                rowHtml += Environment.NewLine + String.Format("    <div class=\"bottom-right\">");
                rowHtml += Environment.NewLine + String.Format("        <p><span>Người nhận: <span class=\"name\">{0}</span></span></p>", order.CustomerName.ToTitleCase());

                string CustomerPhone2 = "";

                var customer = CustomerController.GetByID(Convert.ToInt32(order.CustomerID));
                if (!string.IsNullOrEmpty(customer.CustomerPhone2))
                {
                    CustomerPhone2 = " - " + customer.CustomerPhone2;
                }

                rowHtml += Environment.NewLine + String.Format("        <p><span>Điện thoại: <span class=\"phone\">{0}{1}</span></span></p>", order.CustomerPhone, CustomerPhone2);
                rowHtml += Environment.NewLine + String.Format("        <p><span>Địa chỉ: <span class=\"address\">{0}</span></span></p>", CustomerAddress);
                rowHtml += Environment.NewLine + String.Format("    </div>");
                rowHtml += Environment.NewLine + String.Format("</div>");
            }
            else
            {
                error += "Không tìm thấy đơn hàng!";
            }

            if (error != "")
            {
                ltrShippingNote.Text = "<h1>Lỗi:</h1>" + error;
            }
            else
            {
                ltrShippingNote.Text = rowHtml;
                ltrPrintButton.Text = "<div class=\"print-it\">";
                ltrPrintButton.Text += "<a class=\"btn\" href=\"javascript:;\" onclick=\"printIt()\">In phiếu gửi hàng</a>";
                if (order.ShippingType == 4)
                {
                    ltrPrintButton.Text += "<a class=\"btn show-transport-info\" href=\"javascript:;\" onclick=\"showTransportInfo()\">Hiện thông tin nhà xe</a>";
                }
                if (order.ShippingType == 3 && order.PaymentType == 3)
                {
                    ltrPrintButton.Text += "<a class=\"btn show-transport-info\" href=\"https://proship.vn/quan-ly-van-don/?isInvoiceFilter=1&generalInfo=" + order.ShippingCode + "\" target=\"_blank\">Kiểm tra thu hộ trên Proship</a>";
                }
                ltrPrintButton.Text += "</div>";
            }
        }
    }
}