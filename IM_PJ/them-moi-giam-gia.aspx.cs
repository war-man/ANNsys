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
                if (Request.Cookies["userLoginSystem"] != null)
                {
                    string username = Request.Cookies["userLoginSystem"].Value;
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
            string username = Request.Cookies["userLoginSystem"].Value;
            var acc = AccountController.GetByUsername(username);
            if (acc != null)
            {
                if (acc.RoleID == 0)
                {
                    DiscountGroupController.Insert(txtDiscountName.Text, Convert.ToDouble(pDiscountAmount.Value),
                        Convert.ToDouble(pDiscountAmountPercent.Value), pDiscountNote.Content, chkIsHidden.Checked, DateTime.Now, username, 
                        Convert.ToDouble(rRefundGoods.Value), Convert.ToDouble(pNumOfDateToChangeProduct.Value), Convert.ToDouble(pNumOfProductCanChange.Value));
                    Response.Redirect("/danh-sach-nhom-khach-hang.aspx");
                    //PJUtils.ShowMessageBoxSwAlert("Tạo mới thành công", "s", true, Page);
                }
            }
        }
    }
}