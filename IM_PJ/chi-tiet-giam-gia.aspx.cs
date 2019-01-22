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
                LoadData();
            }
        }

        public void LoadData()
        {
            int id = Request.QueryString["id"].ToInt(0);
            if (id > 0)
            {
                var d = DiscountGroupController.GetByID(id);
                if (d != null)
                {
                    ViewState["ID"] = id;
                    txtDiscountName.Text = d.DiscountName;
                    pDiscountAmount.Value = d.DiscountAmount;
                    pDiscountAmountPercent.Value = d.DiscountAmountPercent;
                    pDiscountNote.Content = d.DiscountNote;
                    chkIsHidden.Checked = Convert.ToBoolean(d.IsHidden);
                    rRefundGoods.Value = Convert.ToDouble(d.FeeRefund);
                    pNumOfDateToChangeProduct.Value = Convert.ToDouble(d.NumOfDateToChangeProduct);
                    pNumOfProductCanChange.Value = Convert.ToDouble(d.NumOfProductCanChange);
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
                    int id = ViewState["ID"].ToString().ToInt(0);
                    if (id > 0)
                    {
                        var d = DiscountGroupController.GetByID(id);
                        if (d != null)
                        {
                            DiscountGroupController.Update(id, txtDiscountName.Text, Convert.ToDouble(pDiscountAmount.Value), Convert.ToDouble(pDiscountAmountPercent.Value), pDiscountNote.Content, chkIsHidden.Checked, DateTime.Now, username, Convert.ToDouble(rRefundGoods.Value), Convert.ToDouble(pNumOfDateToChangeProduct.Value), Convert.ToDouble(pNumOfProductCanChange.Value));
                            PJUtils.ShowMessageBoxSwAlert("Cập nhật nhóm khách hàng thành công", "s", true, Page);
                        }
                    }
                }
            }
        }
    }
}