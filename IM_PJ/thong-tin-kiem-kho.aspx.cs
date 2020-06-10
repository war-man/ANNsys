using IM_PJ.Controllers;
using IM_PJ.Models;
using IM_PJ.Utils;
using MB.Extensions;
using Newtonsoft.Json;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IM_PJ
{
    public partial class thong_tin_kiem_kho : System.Web.UI.Page
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
            StringBuilder html = new StringBuilder();

            html.Append("<select id='ddlCheckWarehouse' class='form-control'>");
            html.Append("<option value=''>Chọn phiên kiểm kho</option>");

            foreach (var item in checkWarehouse)
            {
                html.Append("<option value='" + item.ID + "'>" + item.Name + "</option>");
            }

            html.Append("</select>");
            ltrCheckWarehouse.Text = html.ToString();
        }

        [WebMethod]
        public static string getCheckWarehouse(int id)
        {
            using (var con = new inventorymanagementEntities())
            {
                var productCheck = con.CheckWarehouseDetails.Where(x => x.CheckWarehouseID == id);

                if (productCheck.Count() == 0)
                    return null;

                var data = productCheck
                    .Join(
                        con.tbl_Product,
                        c => c.ProductID,
                        p => p.ID,
                        (c, p) => new
                        {
                            ProductName = p.ProductTitle,
                            SKU = c.ProductSKU,
                            QuantityOld = c.QuantityOld,
                            QuantityNew = c.QuantityNew,
                            CreatedDate = c.CreatedDate,
                            CreatedBy = c.CreatedBy,
                            ModifiedDate = c.ModifiedDate,
                            ModifiedBy = c.ModifiedBy
                        }
                    ).ToList();

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                var result = data.Select(x => new CheckInfo()
                {
                    ProductName = x.ProductName,
                    SKU = x.SKU,
                    QuantityOld = String.Format("{0:N0}", x.QuantityOld),
                    QuantityNew = x.QuantityNew.HasValue ? String.Format("{0:N0}", x.QuantityNew.Value) : String.Empty,
                    ModifiedDate = x.CreatedDate == x.ModifiedDate ? String.Empty : x.ModifiedDate.ToString(),
                    //ModifiedDate = x.CreatedDate == x.ModifiedDate ? String.Empty : x.ModifiedDate.ToString("yyyy-MM-dd hh:mm:ss"),
                    ModifiedBy = x.CreatedBy == x.ModifiedBy ? String.Empty : x.ModifiedBy
                }).ToList();

                return serializer.Serialize(result);
            }
        }



        public class CheckInfo
        {
            public string ProductName { get; set; }
            public string SKU { get; set; }
            public string QuantityOld { get; set; }
            public string QuantityNew { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
        }

        protected void btnCloseCheckWareHouse_Click(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            string username = Request.Cookies["usernameLoginSystem"].Value;
            var acc = AccountController.GetByUsername(username);
            if (acc == null)
                return;

            var result = CheckWarehouseController.closeCheckHouse(hdfCheckHouseID.Value.ToInt(0), DateTime.Now, acc.Username);

            if (result)
                PJUtils.ShowMessageBoxSwAlert("Thành công trong việc đóng lại hoạt động kiểm tra kho!", "s", true, Page);
            else
                PJUtils.ShowMessageBoxSwAlert("Thất bại trong việc đóng lại hoạt động kiểm tra kho!", "e", true, Page);
        }
    }
}