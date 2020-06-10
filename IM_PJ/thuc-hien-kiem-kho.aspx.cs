using System;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web;
using IM_PJ.Controllers;
using Newtonsoft.Json;
using IM_PJ.Models.Pages.thuc_hien_kiem_kho;
using IM_PJ.Models;
using System.Collections.Generic;
using System.Text;

namespace IM_PJ
{
    public partial class thuc_hien_kiem_kho : System.Web.UI.Page
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
                        if (acc.RoleID != 0 && acc.RoleID != 1 && acc.RoleID != 3)
                        {
                            Response.Redirect("/trang-chu");
                        }
                        LoadData();
                    }
                }
                else
                {
                    Response.Redirect("/dang-nhap");
                }

            }
        }

        public void LoadData()
        {
            var checkWarehouse = CheckWarehouseController.getAll();
            dllCheckWarehouse.Items.Clear();
            dllCheckWarehouse.Items.Insert(0, new ListItem("Chọn phiên kiểm kho", ""));

            foreach (var cw in checkWarehouse)
            {
                ListItem listitem = new ListItem(cw.Name, cw.ID.ToString());
                dllCheckWarehouse.Items.Add(listitem);
            }

            if (checkWarehouse.Count > 0)
                dllCheckWarehouse.DataBind();
        }
    }
}