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
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace IM_PJ
{
    public partial class chi_tiet_giam_gia : System.Web.UI.Page
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
                        if (acc.RoleID != 0)
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
            int id = Request.QueryString["id"].ToInt(0);
            if (id > 0)
            {
                var discountGroup = DiscountGroupController.GetByID(id);
                if (discountGroup != null)
                {
                    ViewState["ID"] = id;
                    txtDiscountName.Text = discountGroup.DiscountName;
                    pDiscountAmount.Value = discountGroup.DiscountAmount;
                    pQuantityProduct.Value = discountGroup.QuantityProduct;
                    pDiscountAmountPercent.Value = discountGroup.DiscountAmountPercent;
                    rRefundGoods.Value = discountGroup.FeeRefund;
                    pNumOfDateToChangeProduct.Value = discountGroup.NumOfDateToChangeProduct;
                    pNumOfProductCanChange.Value = discountGroup.NumOfProductCanChange;
                    pRefundQuantityNoFee.Value = discountGroup.RefundQuantityNoFee;
                    pDiscountNote.Content = discountGroup.DiscountNote;
                    chkIsHidden.Checked = discountGroup.IsHidden.HasValue ? discountGroup.IsHidden.Value : false;
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = Request.Cookies["usernameLoginSystem"].Value;
            var acc = AccountController.GetByUsername(username);
            if (acc != null)
            {
                if (acc.RoleID == 0)
                {
                    int id = ViewState["ID"].ToString().ToInt(0);
                    if (id > 0)
                    {
                        var now = DateTime.Now;

                        var data = new tbl_DiscountGroup()
                        {
                            ID = id,
                            DiscountName = txtDiscountName.Text,
                            DiscountAmount = pDiscountAmount.Value.HasValue ? pDiscountAmount.Value.Value : 0,
                            DiscountAmountPercent = pDiscountAmountPercent.Value.HasValue ? pDiscountAmountPercent.Value.Value : 0,
                            QuantityProduct = pQuantityProduct.Value.HasValue ? Convert.ToInt32(pQuantityProduct.Value.Value) : 0,
                            FeeRefund = rRefundGoods.Value.HasValue ? rRefundGoods.Value.Value : 0,
                            NumOfDateToChangeProduct = pNumOfDateToChangeProduct.Value.HasValue ? pNumOfDateToChangeProduct.Value.Value : 0,
                            NumOfProductCanChange = pNumOfProductCanChange.Value.HasValue ? pNumOfProductCanChange.Value.Value : 0,
                            RefundQuantityNoFee = pRefundQuantityNoFee.Value.HasValue ? Convert.ToInt32(pRefundQuantityNoFee.Value.Value) : 0,
                            DiscountNote = pDiscountNote.Content,
                            IsHidden = chkIsHidden.Checked,
                            ModifiedBy = username,
                            ModifiedDate = now
                        };


                        var result = DiscountGroupController.Update(data);

                        if (!String.IsNullOrEmpty(result))
                            PJUtils.ShowMessageBoxSwAlert("Cập nhật nhóm khách hàng thành công", "s", true, Page);
                    }
                }
            }
        }
    }
}