using IM_PJ.Controllers;
using MB.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IM_PJ
{
    public partial class bao_cao_nhan_vien : System.Web.UI.Page
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
                        if (acc.RoleID == 1)
                        {
                            Response.Redirect("/dang-nhap");
                        }
                    }
                }
                else
                {
                    Response.Redirect("/dang-nhap");
                }
                LoadCategory();
                LoadData();
            }
        }
        public void LoadCategory()
        {
            var category = CategoryController.GetAllWithIsHidden(false);
            ddlCategory.Items.Clear();
            ddlCategory.Items.Insert(0, new ListItem("Danh mục", "0"));
            if (category.Count > 0)
            {
                addItemCategory(0, "");
                ddlCategory.DataBind();
            }
        }

        public void addItemCategory(int id, string h = "")
        {
            var categories = CategoryController.GetByParentID("", id);

            if (categories.Count > 0)
            {
                foreach (var c in categories)
                {
                    ListItem listitem = new ListItem(h + c.CategoryName, c.ID.ToString());
                    ddlCategory.Items.Add(listitem);

                    addItemCategory(c.ID, h + "---");
                }
            }
        }
        public void LoadData()
        {
            string username = Request.Cookies["userLoginSystem"].Value;
            var acc = AccountController.GetByUsername(username);
            if (acc != null)
            {
                string user = acc.Username;
                string SKU = String.Empty;
                int CategoryID = 0;
                DateTime fromdate = DateTime.Today;
                DateTime todate = fromdate.AddDays(1).AddMinutes(-1);

                int totalSoldQuantity = 0;
                int averageSoldQuantity = 0;
                int totalRefundQuantity = 0;
                int averageRefundQuantity = 0;
                int totalDays = 0;

                if (!String.IsNullOrEmpty(Request.QueryString["SKU"]))
                {
                    SKU = Request.QueryString["SKU"];

                    var product = ProductController.GetBySKU(SKU);
                    if (product != null)
                    {
                        fromdate = Convert.ToDateTime(product.CreatedDate);
                    }
                }

                if (!String.IsNullOrEmpty(Request.QueryString["categoryid"]))
                {
                    CategoryID = Request.QueryString["categoryid"].ToInt(0);
                }

                if (!String.IsNullOrEmpty(Request.QueryString["fromdate"]))
                {
                    fromdate = Convert.ToDateTime(Request.QueryString["fromdate"]);
                }

                if (!String.IsNullOrEmpty(Request.QueryString["todate"]))
                {
                    todate = Convert.ToDateTime(Request.QueryString["todate"]).AddDays(1).AddMinutes(-1);
                }

                txtTextSearch.Text = SKU;
                ddlCategory.SelectedValue = CategoryID.ToString();
                rFromDate.SelectedDate = fromdate;
                rToDate.SelectedDate = todate;
                totalDays = Convert.ToInt32((todate - fromdate).TotalDays);

                var userReport = OrderController.getProductReport(SKU, CategoryID, user, fromdate, todate);

                totalSoldQuantity = userReport.totalSold;
                averageSoldQuantity = totalSoldQuantity / totalDays;

                var userRefundReport = RefundGoodController.getRefundProductReport(SKU, CategoryID, user, fromdate, todate);

                totalRefundQuantity = userRefundReport.totalRefund;
                averageRefundQuantity = totalRefundQuantity / totalDays;

                ltrTotalSaleOrder.Text = userReport.totalOrder.ToString() + " đơn";
                ltrAverageSaleOrder.Text = (userReport.totalOrder / totalDays).ToString() + " đơn/ngày";
                ltrTotalSoldQuantity.Text = totalSoldQuantity.ToString() + " cái";
                ltrAverageSoldQuantity.Text = averageSoldQuantity.ToString() + " cái/ngày";
                ltrTotalRefundQuantity.Text = totalRefundQuantity.ToString() + " cái";
                ltrAverageRefundQuantity.Text = averageRefundQuantity.ToString() + " cái/ngày";
                ltrTotalRemainQuantity.Text = (totalSoldQuantity - totalRefundQuantity).ToString() + " cái";
                ltrAverageRemainQuantity.Text = ((totalSoldQuantity - totalRefundQuantity) / totalDays).ToString() + " cái/ngày";
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string SKU = txtTextSearch.Text;
            string CategoryID = ddlCategory.SelectedValue;
            string fromdate = rFromDate.SelectedDate.ToString();
            string todate = rToDate.SelectedDate.ToString();

            Response.Redirect(String.Format("/bao-cao-nhan-vien?SKU={0}&categoryid={1}&fromdate={2}&todate={3}", SKU, CategoryID, fromdate, todate));
        }
    }
}