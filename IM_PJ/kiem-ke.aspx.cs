using IM_PJ.Controllers;
using IM_PJ.Models;
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
using static IM_PJ.Controllers.ProductController;

namespace IM_PJ
{
    public partial class kiem_ke : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 600;

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
                            hdfRoleID.Value = acc.RoleID.ToString();
                            hdfUsernameCurrent.Value = acc.Username;
                        }
                        else if (acc.RoleID == 2)
                        {
                            hdfRoleID.Value = acc.RoleID.ToString();
                            hdfUsername.Value = acc.Username;
                            hdfUsernameCurrent.Value = acc.Username;
                        }
                        else
                        {
                            Response.Redirect("/trang-chu");
                        }
                        var agent = acc.AgentID;
                    }
                }
                else
                {
                    Response.Redirect("/dang-nhap");
                }
                LoadData();
            }
        }

        // Load data drop down list floor
        public void loadFloor(int value)
        {
            var floors = CategoryShelfController.getFloors();
            ddlFloor.Items.Clear();
            ddlFloor.Items.Insert(0, new ListItem("Chọn Lầu", "0"));
            foreach (var item in floors)
            {
                var listItem = new ListItem() { Value = item.ID.ToString(), Text = item.Name };
                if (item.ID == value)
                {
                    listItem.Selected = true;
                }
                ddlFloor.Items.Add(listItem);
            }
            // Cài đặt giá trị
            ddlFloor.DataBind();
        }

        // Load data drop down list row
        public void loadRow(int parentID, int value)
        {
            ddlRow.Items.Clear();
            ddlRow.Items.Insert(0, new ListItem("Chọn dãy", "0"));

            if (parentID <= 0)
            {
                ddlRow.Attributes.Add("disabled", "true");
                ddlRow.Style.Add("background-color", "gray");
                return;
            }

            var rows = CategoryShelfController.getRows(parentID);
            foreach (var item in rows)
            {
                var listItem = new ListItem() { Value = item.ID.ToString(), Text = item.Name };
                if (item.ID == value)
                {
                    listItem.Selected = true;
                }
                ddlRow.Items.Add(listItem);
            }
            // Cài đặt giá trị
            ddlRow.Attributes.Remove("disabled");
            ddlRow.Style.Remove("background-color");
            ddlRow.DataBind();
        }

        // Load data drop down list shelf
        public void loadShelf(int parentID, int value)
        {
            ddlShelf.Items.Clear();
            ddlShelf.Items.Insert(0, new ListItem("Chọn kệ", "0"));

            if (parentID <= 0)
            {
                ddlShelf.Attributes.Add("disabled", "true");
                ddlShelf.Style.Add("background-color", "gray");
                return;
            }

            var shelfs = CategoryShelfController.getShelfs(parentID);
            foreach (var item in shelfs)
            {
                var listItem = new ListItem() { Value = item.ID.ToString(), Text = item.Name };
                if (item.ID == value)
                {
                    listItem.Selected = true;
                }
                ddlShelf.Items.Add(listItem);
            }
            // Cài đặt giá trị
            ddlShelf.Attributes.Remove("disabled");
            ddlShelf.Style.Remove("background-color");
            ddlShelf.DataBind();
        }

        // Load data drop down list shelf
        public void loadFloorShelf(int parentID, int value)
        {
            ddlFloorShelf.Items.Clear();
            ddlFloorShelf.Items.Insert(0, new ListItem("Chọn tầng", "0"));

            if (parentID <= 0)
            {
                ddlFloorShelf.Attributes.Add("disabled", "true");
                ddlFloorShelf.Style.Add("background-color", "gray");
                return;
            }

            var floorShelfs = CategoryShelfController.getFloorShelfs(parentID);
            foreach (var item in floorShelfs)
            {
                var listItem = new ListItem() { Value = item.ID.ToString(), Text = item.Name };
                if (item.ID == value)
                {
                    listItem.Selected = true;
                }
                ddlFloorShelf.Items.Add(listItem);
            }
            // Cài đặt giá trị
            ddlFloorShelf.Attributes.Remove("disabled");
            ddlFloorShelf.Style.Remove("background-color");
            ddlFloorShelf.DataBind();
        }

        public void LoadData()
        {
            // Drop down list floor
            loadFloor(0);
            // Drop down list row
            loadRow(0, 0);
            // Drop down list shelf
            loadShelf(0, 0);
            // Drop down list floorShelf
            loadFloorShelf(0, 0);
        }

        // API get drop downlist row
        [WebMethod]
        public static List<ListItem> getRows(int parentID)
        {
            if (parentID > 0)
            {
                return CategoryShelfController.getRows(parentID)
                    .Select(x => new ListItem()
                    {
                        Value = x.ID.ToString(),
                        Text = x.Name
                    })
                    .ToList();
            }
            else
            {
                return null;
            }
        }

        // API get drop downlist shelf
        [WebMethod]
        public static List<ListItem> getShelfs(int parentID)
        {
            if (parentID > 0)
            {
                return CategoryShelfController.getShelfs(parentID)
                    .Select(x => new ListItem()
                    {
                        Value = x.ID.ToString(),
                        Text = x.Name
                    })
                    .ToList();
            }
            else
            {
                return new List<ListItem>();
            }
        }

        // API get drop downlist floor shelf
        [WebMethod]
        public static List<ListItem> getFloorShelfs(int parentID)
        {
            if (parentID > 0)
            {
                return CategoryShelfController.getFloorShelfs(parentID)
                    .Select(x => new ListItem()
                    {
                        Value = x.ID.ToString(),
                        Text = x.Name
                    })
                    .ToList();
            }
            else
            {
                return new List<ListItem>();
            }
        }

        [WebMethod]
        public static List<Product> getProduct(string sku, int floor, int row, int shelf, int floorShelf)
        {
            var filter = new ProductFilterModel()
            {
                search = sku,
                floor = floor,
                row = row,
                shelf = shelf,
                floorShelf = floorShelf
            };

            return ProductController.GetProductShelf(filter);
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            string username = Request.Cookies["usernameLoginSystem"].Value;
            var acc = AccountController.GetByUsername(username);
            var productShelf = JsonConvert.DeserializeObject<List<Product>>(hdfProductShelf.Value);


            ProductController.updateProductQuantityInShelf(productShelf, acc.ID);
            PJUtils.ShowMessageBoxSwAlert("Kiểm kê thành công!", "s", true, Page);
        }
    }
}