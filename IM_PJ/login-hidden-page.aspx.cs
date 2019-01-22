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
            string password = PJUtils.Encrypt("scode", txtPassword.Text);

            var p = ConfigController.GetByTop1();

            if (password == p.SecurityCode)
            {
                Response.Cookies["loginHiddenPage"].Value = p.SecurityCode;
                Response.Cookies["loginHiddenPage"].Expires = DateTime.Now.AddDays(60);
                Response.Redirect("/sp");
            }
            else
            {
                lblError.Text = "Sai mật khẩu!";
                lblError.Visible = true;
            }
        }
    }
}