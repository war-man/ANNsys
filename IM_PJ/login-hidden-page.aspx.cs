using IM_PJ.Controllers;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IM_PJ
{
    public partial class login_hidden_page : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["loginHiddenPage"] != null)
                {
                    Response.Redirect("/sp");
                }
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = PJUtils.Encrypt("scode", txtPassword.Text);

            var p = ConfigController.GetByTop1();
            var acc = AccountController.GetByUsername(username);
            if (password == p.SecurityCode && acc != null)
            {
                Response.Cookies["loginHiddenPage"].Value = username;
                Response.Cookies["loginHiddenPage"].Expires = DateTime.Now.AddDays(90);
                Response.Redirect("/sp");
            }
            else
            {
                lblError.Text = "Sai thông tin đăng nhập!";
                lblError.Visible = true;
            }
        }
    }
}