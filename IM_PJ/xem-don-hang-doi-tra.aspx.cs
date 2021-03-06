﻿using IM_PJ.Controllers;
using IM_PJ.Models;
using IM_PJ.Utils;
using MB.Extensions;
using Newtonsoft.Json;
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
using static IM_PJ.Controllers.DiscountCustomerController;

namespace IM_PJ
{
    public partial class xem_don_hang_doi_tra : System.Web.UI.Page
    {
        private static RefundGoodModel _refundGood;

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
                        if (_refundGood == null)
                        {
                            _refundGood = new RefundGoodModel();
                            _refundGood.CreateBy = acc.Username;
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
            int n;
            if (String.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out n))
            {
                PJUtils.ShowMessageBoxSwAlertError("Không tìm thấy đơn hàng", "e", true, "/danh-sach-don-tra-hang", Page);
            }

            string username = Request.Cookies["usernameLoginSystem"].Value;
            var acc = AccountController.GetByUsername(username);
            if (acc != null)
            {
                int AgentID = Convert.ToInt32(acc.AgentID);
                int ID = Request.QueryString["id"].ToInt(0);
                if (ID > 0)
                {
                    ViewState["ID"] = ID;
                    var r = RefundGoodController.GetByIDAndAgentID(ID, AgentID);
                    if (r == null)
                    {
                        PJUtils.ShowMessageBoxSwAlertError("Không tìm thấy đơn hàng " + ID, "e", true, "/danh-sach-don-tra-hang", Page);
                    }
                    else
                    {
                        
                        if (acc.RoleID != 0)
                        {
                            // Kiểm tra nếu đơn hàng này không "chính chủ"
                            if (r.CreatedBy != acc.Username)
                            {
                                // Kiểm tra đơn hàng này có đang được tạo giúp bởi nhân viên khác không?
                                var usernameRequest = HttpContext.Current.Request["username"];
                                if (!String.IsNullOrEmpty(usernameRequest))
                                {
                                    var userRequest = AccountController.GetByUsername(usernameRequest);
                                    if (userRequest == null)
                                    {
                                        PJUtils.ShowMessageBoxSwAlertError("Không tìm thấy nhân viên " + usernameRequest, "e", true, "/danh-sach-don-tra-hang", Page);
                                    }
                                    else
                                    {
                                        if (usernameRequest != r.CreatedBy)
                                        {
                                            PJUtils.ShowMessageBoxSwAlertError("Đơn hàng này không phải của " + usernameRequest, "e", true, "/danh-sach-don-tra-hang", Page);
                                        }
                                        else
                                        {
                                            if (r.UserHelp != acc.Username)
                                            {
                                                PJUtils.ShowMessageBoxSwAlertError("Đơn hàng này không phải do bạn tạo giúp", "e", true, "/danh-sach-don-tra-hang", Page);
                                            }
                                            else
                                            {
                                                PJUtils.ShowMessageBoxSwAlertError("Đơn hàng này do bạn tạo giúp. Nhấn OK để tiếp tục xử lý!", "i", false, "", Page);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    PJUtils.ShowMessageBoxSwAlertError("Đơn hàng này không phải của bạn", "e", true, "/danh-sach-don-tra-hang", Page);
                                }
                            }
                        }
                        
                        ltrCreateBy.Text = r.CreatedBy;
                        ltrCreateDate.Text = r.CreatedDate.ToString();
                        ltrOrderStatus.Text = PJUtils.RefundStatus(Convert.ToInt32(r.Status));
                        if(r.OrderSaleID > 0)
                        {
                            ltrOrderSaleID.Text = "<td><a class='customer-name-link' target='_blank' title='Bấm vào xem đơn hàng trừ tiền' href='/thong-tin-don-hang?id=" + r.OrderSaleID + "'>" + r.OrderSaleID + " (Xem đơn)</a>";
                        }
                        else
                        {
                            ltrOrderSaleID.Text = "";
                        }
                        ltrOrderQuantity.Text = r.TotalQuantity.ToString();
                        ltrOrderTotalPrice.Text = string.Format("{0:N0}", (Convert.ToDouble(r.TotalPrice)));
                        ltrTotalRefundFee.Text = string.Format("{0:N0}", (Convert.ToDouble(r.TotalRefundFee)));
                        ltrRefundNote.Text = r.RefundNote;

                        int cusID = 0;
                        string zalo = "";
                        string nick = "";
                        string address = "";
                        string facebook = "";
                        var cus = CustomerController.GetByID(r.CustomerID.Value);
                        if (cus != null)
                        {
                            cusID = cus.ID;
                            zalo = cus.Zalo;
                            nick = cus.Nick;
                            address = cus.CustomerAddress;
                            facebook = cus.Facebook;
                            hdfCustomerPhone.Value = cus.CustomerPhone;
                        }

                        // Title
                        this.Title = String.Format("{0} - Đổi trả", string.IsNullOrEmpty(nick) ? nick.ToTitleCase() : r.CustomerName.ToTitleCase());
                        ltrHeading.Text = "Đơn đổi trả " + ID.ToString() + " - " + (string.IsNullOrEmpty(nick) ? nick.ToTitleCase() : r.CustomerName.ToTitleCase()) + (!string.IsNullOrEmpty(r.UserHelp) ? " (được tạo giúp bởi " + r.UserHelp + ")" : "");

                        ltrInfo.Text += "<div class='row'>";
                        ltrInfo.Text += "    <div class='col-md-3'>";
                        ltrInfo.Text += "        <div class='form-group'>";
                        ltrInfo.Text += "            <label>Họ tên</label>";
                        ltrInfo.Text += "            <span class='form-control input-disabled'>" + r.CustomerName + "</span>";
                        ltrInfo.Text += "        </div>";
                        ltrInfo.Text += "    </div>";
                        ltrInfo.Text += "    <div class='col-md-3'>";
                        ltrInfo.Text += "        <div class='form-group'>";
                        ltrInfo.Text += "            <label>Điện thoại</label>";
                        ltrInfo.Text += "            <span class='form-control input-disabled'>" + r.CustomerPhone + "</span>";
                        ltrInfo.Text += "        </div>";
                        ltrInfo.Text += "    </div>";
                        ltrInfo.Text += "    <div class='col-md-3'>";
                        ltrInfo.Text += "        <div class='form-group'>";
                        ltrInfo.Text += "            <label>Nick đặt hàng</label>";
                        ltrInfo.Text += "            <span class='form-control input-disabled'>" + nick + "</span>";
                        ltrInfo.Text += "        </div>";
                        ltrInfo.Text += "    </div>";
                        ltrInfo.Text += "    <div class='col-md-3'>";
                        ltrInfo.Text += "        <div class='form-group'>";
                        ltrInfo.Text += "            <label>Facebook</label>";
                        ltrInfo.Text += "            <span class='form-control input-disabled'>" + facebook + "</span>";
                        ltrInfo.Text += "        </div>";
                        ltrInfo.Text += "    </div>";
                        ltrInfo.Text += "</div> ";
                        ltrInfo.Text += "<div class='row'>";
                        ltrInfo.Text += "    <div class='col-md-3'>";
                        ltrInfo.Text += "        <div class='form-group'>";
                        ltrInfo.Text += "            <label>Tỉnh thành</label>";
                        ltrInfo.Text += "            <select id='_ddlProvince' class='form-control' disable='true' readonly='readonly'></select>";
                        ltrInfo.Text += "        </div>";
                        ltrInfo.Text += "    </div>";
                        ltrInfo.Text += "    <div class='col-md-3'>";
                        ltrInfo.Text += "        <div class='form-group'>";
                        ltrInfo.Text += "            <label>Quận huyện</label>";
                        ltrInfo.Text += "            <select id='_ddlDistrict' class='form-control' disable='true' readonly='readonly'></select>";
                        ltrInfo.Text += "        </div>";
                        ltrInfo.Text += "    </div>";
                        ltrInfo.Text += "    <div class='col-md-3'>";
                        ltrInfo.Text += "        <div class='form-group'>";
                        ltrInfo.Text += "            <label>Phường xã</label>";
                        ltrInfo.Text += "            <select id='_ddlWard' class='form-control' disable='true' readonly='readonly'></select>";
                        ltrInfo.Text += "        </div>";
                        ltrInfo.Text += "    </div>";
                        ltrInfo.Text += "    <div class='col-md-3'>";
                        ltrInfo.Text += "        <div class='form-group'>";
                        ltrInfo.Text += "            <label>Địa chỉ</label>";
                        ltrInfo.Text += "            <span class='form-control input-disabled'>" + address + "</span>";
                        ltrInfo.Text += "        </div>";
                        ltrInfo.Text += "    </div>";
                        ltrInfo.Text += "</div>";

                        ltrInfo.Text += "<div class='form-row view-detail'>";
                        ltrInfo.Text += "    <a href='javascript:;' class='btn primary-btn fw-btn not-fullwidth' onclick='viewCustomerDetail(`" + cusID + "`)'><i class='fa fa-address-card-o' aria-hidden='true'></i> Xem</a>";
                        ltrInfo.Text += "</div>";

                        #region Thông tin phí đổi tra
                        double feeRefundDefault = 0;

                        var discount = DiscountCustomerController.getbyCustID(cusID).FirstOrDefault();
                        if (discount != null)
                        {
                            feeRefundDefault = discount.FeeRefund;

                            ltrInfo.Text += "<div class='form-row discount-info'>";
                            ltrInfo.Text += String.Format("    <strong>* Chiết khấu của khách: {0:0,0}/cái. (đơn từ {1:N0} cái)</strong>", discount.DiscountAmount, discount.QuantityProduct);
                            ltrInfo.Text += "</div>";
                        }
                        else
                        {
                            var config = ConfigController.GetByTop1();
                            feeRefundDefault = config.FeeChangeProduct.Value;
                        }

                        var refundPromotion = RefundGoodController.getPromotion(cusID);
                        if (refundPromotion.IsPromotion)
                            feeRefundDefault = (feeRefundDefault - refundPromotion.DecreasePrice) < 0 ? 0 : feeRefundDefault - refundPromotion.DecreasePrice;

                        ltrInfo.Text += "<div class='form-row refund-info'>";
                        if (feeRefundDefault == 0)
                        {
                            ltrInfo.Text += "    <strong>* Miễn phí đổi hàng</strong>";
                        }
                        else
                        {
                            ltrInfo.Text += String.Format("    <strong>* Phí đổi trả hàng: {0:0,0}/cái.</strong>", feeRefundDefault);
                        }
                        ltrInfo.Text += "</div>";

                        if (UserController.checkExists(cusID))
                        {
                            ltrInfo.Text += "<div class='form-row refund-info'>";
                            ltrInfo.Text += "    <strong class='font-green'>Đã đăng ký App</strong>";
                            ltrInfo.Text += "</div>";
                        }
                        else
                        {
                            ltrInfo.Text += "<div class='form-row refund-info'>";
                            ltrInfo.Text += "    <strong class='font-red'>Chưa đăng ký App</strong>";
                            ltrInfo.Text += "</div>";
                        }
                        #endregion


                        ltrTotal.Text = string.Format("{0:N0}", Convert.ToDouble(r.TotalPrice));
                        ltrQuantity.Text = string.Format("{0:N0}", Convert.ToDouble(r.TotalQuantity));
                        ltrRefund.Text = string.Format("{0:N0}", Convert.ToDouble(r.TotalRefundFee));

                        // get info tranfor page tao-don-hang-doi-tra.aspx
                        _refundGood.RefundGoodsID = ID;
                        _refundGood.CustomerID = r.CustomerID.Value;
                        _refundGood.CustomerName = r.CustomerName;
                        _refundGood.CustomerPhone = r.CustomerPhone;
                        _refundGood.CustomerNick = cus != null ? cus.Nick : String.Empty;
                        _refundGood.CustomerAddress = cus != null ? cus.CustomerAddress : String.Empty;
                        _refundGood.CustomerZalo = cus != null ? cus.Zalo : String.Empty;
                        _refundGood.CustomerFacebook = cus != null ? cus.Facebook : String.Empty;
                        _refundGood.RefundDetails = RefundGoodDetailController.GetInfoShowRefundDetail(ID, feeRefundDefault);
                        _refundGood.TotalPrice = Convert.ToDouble(r.TotalPrice);
                        _refundGood.TotalQuantity = Convert.ToDouble(r.TotalQuantity);
                        _refundGood.TotalFreeRefund = Convert.ToDouble(r.TotalRefundFee);
                        _refundGood.Status = r.Status.Value;
                        _refundGood.Note = txtRefundsNote.Text;
                        _refundGood.CreateBy = r.CreatedBy;

                        var rds = RefundGoodDetailController.GetByRefundGoodsID(ID);

                        var product = _refundGood.RefundDetails
                            .Join(
                                rds,
                                p1 => new {
                                    RefundGoodsID = p1.RefundGoodsID,
                                    RefundDetailID = p1.RefundDetailID
                                },
                                p2 => new {
                                    RefundGoodsID = p2.RefundGoodsID.Value,
                                    RefundDetailID = p2.ID
                                },
                                (p1, p2) => new { p1, p2 })
                            .Select(x => new {
                                SKU = x.p2.SKU,
                                OrderID = x.p2.OrderID,
                                ProductName = x.p2.ProductName,
                                ProductType = x.p2.ProductType,
                                GiavonPerProduct = x.p2.GiavonPerProduct,
                                SoldPricePerProduct = x.p2.SoldPricePerProduct,
                                DiscountPricePerProduct = x.p2.DiscountPricePerProduct,
                                Quantity = x.p2.Quantity,
                                QuantityMax = x.p2.QuantityMax,
                                RefundType = x.p2.RefundType,
                                RefundFeePerProduct = x.p2.RefundFeePerProduct,
                                TotalPriceRow = x.p2.TotalPriceRow,
                                ProductImage = x.p1.ProductImage
                            })
                            .ToList();

                        if (product.Count > 0)
                        {
                            string html = "";
                            int t = 0;
                            foreach (var item in product)
                            {

                                var variables = ProductVariableValueController.GetByProductVariableSKU(item.SKU);
                                string variable = "";
                                if (variables.Count > 0)
                                {
                                    variable += "<br><br>";
                                    foreach (var v in variables)
                                    {
                                        variable += v.VariableName.Trim() + ": " + v.VariableValue.Trim() + "<br>";
                                    }
                                }
                                t++;
                                html += "<tr ondblclick='clickrow($(this))' class='product-result' data-sku='" + item.SKU 
                                                    + "' data-orderID='" + item.OrderID
                                                    + "' data-ProductName='" + item.ProductName
                                                    + "' data-ProductType='" + item.ProductType 
                                                    + "' data-Giagoc='" + item.GiavonPerProduct
                                                    + "' data-Giadaban='" + item.SoldPricePerProduct
                                                    + "' data-TienGiam='" + item.DiscountPricePerProduct
                                                    + "' data-Soluongtoida='" + item.QuantityMax 
                                                    + "' data-RefundFee='" + item.RefundFeePerProduct 
                                                    + "'>";
                                html += "   <td>" + t + "</td>";
                                html += "   <td class='image-item'><img src='" + Thumbnail.getURL(item.ProductImage, Thumbnail.Size.Small) + "'></td>";
                                html += "   <td class='name-item'><a href='/xem-san-pham?sku=" + item.SKU + "' target='_blank'>" + item.ProductName + "</a>" + variable + "</td>";
                                html += "   <td class='sku-item'>" + item.SKU + "</td>";
                                html += "   <td class='price-item giagoc' data-giagoc='" + item.GiavonPerProduct + "'>" + string.Format("{0:N0}", Convert.ToDouble(item.GiavonPerProduct)) + "</td>";
                                html += "   <td class='giadaban' data-giadaban='" + item.SoldPricePerProduct + "'><strong>" + string.Format("{0:N0}", Convert.ToDouble(item.SoldPricePerProduct)) + "</strong><br>(CK: " + string.Format("{0:N0}", Convert.ToDouble(item.DiscountPricePerProduct)) + ")</td>";
                                html += "   <td class='slcandoi'>" + item.Quantity + "</td>";
                                html += "   <td>";
                                int refundType = Convert.ToInt32(item.RefundType);
                                string refuntTypeName = "";
                                if (item.RefundType == 1)
                                    refuntTypeName = "Đổi size";
                                else if (item.RefundType == 2)
                                    refuntTypeName = "Đổi sản phẩm khác";
                                else if (item.RefundType == 4)
                                    refuntTypeName = "Đổi sản phẩm khác (miễn phí)";
                                else
                                    refuntTypeName = "Đổi hàng lỗi";
                                html += refuntTypeName;
                                html += "    </td>";
                                html += "   <td class='phidoihang'>" + string.Format("{0:N0}", Convert.ToDouble(item.RefundFeePerProduct)) + "</td>";
                                html += "   <td class='thanhtien'>" + string.Format("{0:N0}", Convert.ToDouble(item.TotalPriceRow)) + "</td>";
                                html += "</tr>";

                            }

                            ddlRefundStatus.SelectedValue = r.Status.ToString();
                            txtRefundsNote.Text = r.RefundNote;
                            ltrList.Text = html;
                        }

                        ltrPrint.Text = "<a href='/print-invoice-return?id=" + ID + "' target='_blank' class='btn primary-btn fw-btn not-fullwidth'><i class='fa fa-print' aria-hidden='true'></i> In hóa đơn</a>";
                        ltrPrint.Text += "<a href='/print-return-order-image?id=" + ID + "' target='_blank' class='btn primary-btn btn-blue fw-btn not-fullwidth print-invoice-merged'><i class='fa fa-picture-o' aria-hidden='true'></i> Lấy ảnh đơn hàng</a>";

                    }
                }
            }

        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            string username = Request.Cookies["usernameLoginSystem"].Value;
            var a = AccountController.GetByUsername(username);
            if (a != null)
            {
                var re = RefundGoodController.updatestatus((ViewState["ID"].ToString()).ToInt(), Convert.ToInt32(ddlRefundStatus.SelectedValue), DateTime.Now, a.Username,txtRefundsNote.Text);
                if(re != null)
                {
                    PJUtils.ShowMessageBoxSwAlert("Cập nhật đơn hàng đổi trả thành công", "s", true, Page);
                }
                else
                {
                    PJUtils.ShowMessageBoxSwAlert("Thất bại", "e", false, Page);
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var product in _refundGood.RefundDetails)
                {
                    if (!string.IsNullOrEmpty(product.ChildSKU))
                    {
                        var ProductVariable = ProductVariableValueController.GetByProductVariableSKU(product.ChildSKU);
                        string VariableValue = "";
                        if (ProductVariable.Count > 0)
                        {
                            foreach (var v in ProductVariable)
                            {
                                VariableValue += v.VariableName.Trim() + ": " + v.VariableValue.Trim() + "|";
                            }
                        }
                        product.VariableValue = VariableValue;
                    }
                    
                    if (product.ChangeType != 3) // Change product error
                    {
                        StockManagerController.Insert(new tbl_StockManager()
                        {
                            AgentID = 1,
                            ProductID = product.ProductStyle == 1 ? product.ProductID : 0,
                            ProductVariableID = product.ProductVariableID,
                            Quantity = product.QuantityRefund,
                            QuantityCurrent = 0,
                            Type = 1,
                            NoteID = "Xuất kho do làm lại đơn hàng đổi trả",
                            OrderID = product.OrderID,
                            Status = 11,
                            SKU = product.ProductStyle == 1 ? product.ParentSKU : product.ChildSKU,
                            CreatedDate = DateTime.Now,
                            CreatedBy = _refundGood.CreateBy,
                            MoveProID = 0,
                            ParentID = product.ProductID,
                        });
                    }
                }

                HttpContext.Current.Items.Add("xem-don-hang-doi-tra", JsonConvert.SerializeObject(_refundGood));
                Server.Transfer("tao-don-hang-doi-tra.aspx");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}