using IM_PJ.Controllers;
using IM_PJ.Models;
using MB.Extensions;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
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
        public string createBarcode(string barcodeValue)
        {
            // Tạo barcode cho bưu điện
            var temps = new List<String>();
            var imageName = String.Format("{0}{1}.png", DateTime.UtcNow.ToString("yyyyMMddHHmmss"), Guid.NewGuid());
            string barcodeImage = "/uploads/shipping-barcodes/" + imageName;
            System.Drawing.Image barCode = PJUtils.MakeShippingBarcode(barcodeValue, 2, false);
            barCode.Save(HttpContext.Current.Server.MapPath("" + barcodeImage + ""), ImageFormat.Png);
            temps.Add(imageName);
            string result = "data:image/png;base64, " + Convert.ToBase64String(File.ReadAllBytes(Server.MapPath("" + barcodeImage + "")));

            // Xóa barcode sau khi tạo
            string[] filePaths = Directory.GetFiles(Server.MapPath("/uploads/shipping-barcodes/"));
            foreach (string filePath in filePaths)
            {
                foreach (var item in temps)
                {
                    if (filePath.EndsWith(item))
                    {
                        File.Delete(filePath);
                    }
                }
            }

            return result;
        }
        public void LoadData()
        {
            string username = Request.Cookies["usernameLoginSystem"].Value;
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

                if (order.ShippingType == 1 && acc.RoleID != 0)
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

                rowHtml += Environment.NewLine + String.Format("<div class='table'>");
                rowHtml += Environment.NewLine + String.Format("    <div class='top-left'>");
                rowHtml += Environment.NewLine + String.Format("        <p><span>Người gửi: <span class='name'>{0}</span></span></p>", leader);
                rowHtml += Environment.NewLine + String.Format("        <p><span>{0}</span></p>", phone);
                rowHtml += Environment.NewLine + String.Format("        <p><span>{0}</span></p>", address);
                rowHtml += Environment.NewLine + String.Format("        <p><span class='web'>ANN.COM.VN</span></p>");
                rowHtml += Environment.NewLine + String.Format("    </div>");
                rowHtml += Environment.NewLine + String.Format("    <div class='bottom-left'>");
                if (order.PaymentType == 3)
                {
                    rowHtml += Environment.NewLine + String.Format("        <p><span class='cod'>Thu hộ: {0}</span></p>", string.Format("{0:N0}", TotalOrder));
                }
                else
                {
                    rowHtml += Environment.NewLine + String.Format("        <p><span class='cod'>Thu hộ: KHÔNG</span></p>");
                }
                rowHtml += Environment.NewLine + String.Format("        <p><span>Nhân viên: {0}</span></p>", order.CreatedBy);
                rowHtml += Environment.NewLine + String.Format("        <img src='" + createBarcode(order.ID.ToString()) + "'>");
                rowHtml += Environment.NewLine + String.Format("        <p><span>Mã đơn hàng: {0}</span></p>", order.ID);
                rowHtml += Environment.NewLine + String.Format("    </div>");
                rowHtml += Environment.NewLine + String.Format("    <div class='top-right'>");
                if (order.ShippingType != 2 && order.ShippingType != 3)
                {
                    rowHtml += Environment.NewLine + String.Format("        <img class='img' src='https://ann.com.vn/wp-content/uploads/ANN-logo-3.png'>");
                }
                var company = TransportCompanyController.GetTransportCompanyForOrderList(Convert.ToInt32(order.TransportCompanyID));
                string transportCompany = "";
                string transportCompanyPhone = "";
                string transportCompanyAddress = "";
                string transportCompanyNote = "";

                string CustomerAddress = order.CustomerAddress.ToTitleCase();

                // NHÀ XE
                if (company != null)
                {
                    transportCompany = "<strong>" + company.CompanyName.ToTitleCase() + "</strong>";
                    if (company.CompanyPhone != "")
                    {
                        transportCompanyPhone = "<span class='transport-info'>(" + company.CompanyPhone + ")</span>";
                    }

                    transportCompanyAddress = "<p class='transport-info'>" + company.CompanyAddress.ToTitleCase() + "</p>";

                    if (company.Note != "")
                    {
                        transportCompanyNote = "<span class='transport-info capitalize'> - " + company.Note.ToTitleCase() + "</span>";
                    }

                    var subID = Convert.ToInt32(order.TransportCompanySubID);
                    var shipto = TransportCompanyController.GetReceivePlaceForOrderList(company.ID, subID);
                    if (shipto != null && subID > 0)
                    {
                        CustomerAddress = "<span class='phone'>" + shipto.ShipTo.ToTitleCase() + "</span>";
                    }
                    else
                    {
                        error += "<p>- Đơn hàng này gửi xe " + transportCompany + " nhưng <strong>chưa chọn Nơi nhận</strong>!</p>";
                    }
                }
                // BƯU ĐIỆN
                if (order.ShippingType == 2)
                {
                    if (!string.IsNullOrEmpty(order.ShippingCode))
                    {
                        string PostalDeliveryType = "Thường";
                        if (order.PostalDeliveryType == 2)
                        {
                            PostalDeliveryType = "Nhanh";
                        }
                        rowHtml += Environment.NewLine + String.Format("        <p class='delivery'><strong>Bưu điện - {0}:</strong> {1}</p>", PostalDeliveryType, order.ShippingCode);
                        rowHtml += Environment.NewLine + String.Format("        <img src='" + createBarcode(order.ShippingCode) + "'>");
                    }
                    else
                    {
                        error += "<p>- Đơn hàng này <strong>gửi Bưu điện</strong> nhưng <strong>chưa nhập</strong> MÃ VẬN ĐƠN!</p>";
                    }

                    if (order.PaymentType != 3 && acc.RoleID != 0)
                    {
                        error += "<p>- Đơn hàng này <strong>gửi Bưu điện</strong> nhưng <strong>Không gửi thu hộ</strong>. Nếu có lý do thì báo chị Ngọc xử lý nhé!</p>";
                    }
                }
                // PROSHIP
                else if (order.ShippingType == 3)
                {
                    if (!string.IsNullOrEmpty(order.ShippingCode))
                    {
                        rowHtml += Environment.NewLine + String.Format("        <p class='delivery'><strong>Proship:</strong> <span>{0}</span></p>", order.ShippingCode);
                        rowHtml += Environment.NewLine + String.Format("        <img src='" + createBarcode(order.ShippingCode) + "'>");
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
                // GỬI XE
                else if (order.ShippingType == 4)
                {
                    if (!string.IsNullOrEmpty(transportCompany))
                    {
                        rowHtml += Environment.NewLine + String.Format("        <p class='delivery'><span>Xe: {0} {1} {2}</p>", transportCompany, transportCompanyPhone, transportCompanyNote);

                        // Lấy thông tin xe ra gửi
                        rowHtml += Environment.NewLine + String.Format("        {0}", transportCompanyAddress);
                    }
                    else
                    {
                        error += "<p>- Đơn hàng này <strong>gửi xe</strong> nhưng <strong>chưa chọn Chành xe</strong> nào!</p>";
                    }
                }
                // NHÂN VIÊN GIAO
                else if (order.ShippingType == 5)
                {
                    rowHtml += Environment.NewLine + String.Format("        <p class='delivery'><span>Nhân viên giao</span></p>");
                }
                // GHTK
                else if (order.ShippingType == 6)
                {
                    if (!string.IsNullOrEmpty(order.ShippingCode))
                    {
                        rowHtml += Environment.NewLine + String.Format("        <p class='delivery'><strong>GHTK</strong>: <span>{0}</span></p>", order.ShippingCode);
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
                // VIETTEL
                else if (order.ShippingType == 7)
                {
                    rowHtml += Environment.NewLine + String.Format("        <p class='delivery'><strong>Viettel</strong></p>");
                }

                // THU HỘ
                if (order.PaymentType == 3)
                {
                    rowHtml += Environment.NewLine + String.Format("        <p><span class='cod'>Thu hộ: {0}</span></p>", string.Format("{0:N0}", TotalOrder));
                }
                else
                {
                    rowHtml += Environment.NewLine + String.Format("        <p><span class='cod'>Thu hộ: KHÔNG</span></p>");
                }

                // NHÂN VIÊN GIAO
                if (order.ShippingType == 5)
                {
                    if (Convert.ToDouble(order.FeeShipping) > 0)
                    {
                        rowHtml += Environment.NewLine + String.Format("        <p class='shipping-fee'><span>Phí ship (đã cộng vào thu hộ): {0}</span></p>", string.Format("{0:N0}", Convert.ToDouble(order.FeeShipping)));
                    }
                    else
                    {
                        rowHtml += Environment.NewLine + String.Format("        <p class='shipping-fee'><span>Phí ship: không</span></p>");
                    }
                }

                rowHtml += Environment.NewLine + String.Format("    </div>");
                rowHtml += Environment.NewLine + String.Format("    <div class='bottom-right'>");
                rowHtml += Environment.NewLine + String.Format("        <p><span>Người nhận: <span class='name'>{0}</span></span></p>", order.CustomerName.ToTitleCase());

                string CustomerPhone2 = "";

                var customer = CustomerController.GetByID(Convert.ToInt32(order.CustomerID));
                if (!string.IsNullOrEmpty(customer.CustomerPhone2))
                {
                    CustomerPhone2 = " - " + customer.CustomerPhone2;
                }

                rowHtml += Environment.NewLine + String.Format("        <p><span>Điện thoại: <span class='phone'>{0}{1}</span></span></p>", order.CustomerPhone, CustomerPhone2);
                rowHtml += Environment.NewLine + String.Format("        <p><span>Địa chỉ: <span class='address'>{0}</span></span></p>", CustomerAddress);
                rowHtml += Environment.NewLine + String.Format("    </div>");
                rowHtml += Environment.NewLine + String.Format("    <div class='rotated'>");
                rowHtml += Environment.NewLine + String.Format("        KHO HÀNG SỈ ANN");
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
                ltrPrintButton.Text = "<div class='print-it'>";
                ltrPrintButton.Text += "<a class='btn' href='javascript:;' onclick='printIt()'>In phiếu gửi hàng</a>";
                if (order.ShippingType == 4)
                {
                    ltrPrintButton.Text += "<a class='btn show-transport-info' href='javascript:;' onclick='showTransportInfo()'>Hiện thông tin nhà xe</a>";
                }
                if (order.ShippingType == 3 && order.PaymentType == 3)
                {
                    ltrPrintButton.Text += "<a class='btn show-transport-info' href='https://proship.vn/quan-ly-van-don/?isInvoiceFilter=1&generalInfo=" + order.ShippingCode + "' target='_blank'>Kiểm tra thu hộ trên Proship</a>";
                }
                ltrPrintButton.Text += "</div>";
            }
        }
    }
}