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
    public partial class them_moi_giam_gia : System.Web.UI.Page
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
                    var now = DateTime.Now;
                    var data = new tbl_DiscountGroup()
                    {
                        DiscountName = txtDiscountName.Text,
                        DiscountAmount = pDiscountAmount.Value.HasValue ? pDiscountAmount.Value.Value : 0,
                        DiscountAmountPercent = 0,
                        QuantityProduct = pQuantityProduct.Value.HasValue ? Convert.ToInt32(pQuantityProduct.Value.Value) : 0,
                        FeeRefund = rRefundGoods.Value.HasValue ? rRefundGoods.Value.Value : 0,
                        NumOfDateToChangeProduct = pNumOfDateToChangeProduct.Value.HasValue ? pNumOfDateToChangeProduct.Value.Value : 0,
                        NumOfProductCanChange = pNumOfProductCanChange.Value.HasValue ? pNumOfProductCanChange.Value.Value : 0,
                        RefundQuantityNoFee = pRefundQuantityNoFee.Value.HasValue ? Convert.ToInt32(pRefundQuantityNoFee.Value.Value) : 0,
                        DiscountNote = pDiscountNote.Content,
                        IsHidden = chkIsHidden.Checked,
                        CreatedBy = username,
                        CreatedDate = now,
                        ModifiedBy = username,
                        ModifiedDate = now
                    };

                    DiscountGroupController.Insert(data);

                    Response.Redirect("/danh-sach-nhom-khach-hang.aspx");
                    PJUtils.ShowMessageBoxSwAlert("Tạo mới thành công", "s", true, Page);
                }
            }
        }
    }
}