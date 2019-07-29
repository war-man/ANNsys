using IM_PJ.Controllers;
using MB.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                if (Request.Cookies["usernameLoginSystem"] != null)
                {
                    string username = Request.Cookies["usernameLoginSystem"].Value;
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
            string username = Request.Cookies["usernameLoginSystem"].Value;
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
                int day = 0;

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
                day = Convert.ToInt32((todate - fromdate).TotalDays);

                var userReport = OrderController.getProductReport(SKU, CategoryID, user, fromdate, todate);

                totalSoldQuantity = userReport.Sum(x => x.totalSold);
                averageSoldQuantity = totalSoldQuantity / day;

                var userRefundReport = RefundGoodController.getRefundProductReport(SKU, CategoryID, user, fromdate, todate);

                totalRefundQuantity = userRefundReport.Sum(x => x.totalRefund);
                averageRefundQuantity = totalRefundQuantity / day;

                double totalRevenue = userReport.Sum(x => x.totalRevenue);
                double totalCost = userReport.Sum(x => x.totalCost);
                double totalRefundRevenue = userRefundReport.Sum(x => x.totalRevenue);
                double totalRefundCost = userRefundReport.Sum(x => x.totalCost);
                double totalRefundFee = userRefundReport.Sum(x => x.totalRefundFee);
                double totalProfit = (totalRevenue - totalCost) - (totalRefundRevenue - totalRefundCost) + totalRefundFee;
                // Tổng hệ thống

                var systemReport = OrderController.getProductReport(SKU, CategoryID, "", fromdate, todate);
                var systemRefundReport = RefundGoodController.getRefundProductReport(SKU, CategoryID, "", fromdate, todate);
                double totalRevenueSystem = systemReport.Sum(x => x.totalRevenue);
                double totalCostSystem = systemReport.Sum(x => x.totalCost);
                double totalRefundRevenueSystem = systemRefundReport.Sum(x => x.totalRevenue);
                double totalRefundCostSystem = systemRefundReport.Sum(x => x.totalCost);
                double totalRefundFeeSystem = systemRefundReport.Sum(x => x.totalRefundFee);
                double totalSystemProfit = (totalRevenueSystem - totalCostSystem) - (totalRefundRevenueSystem - totalRefundCostSystem) + totalRefundFeeSystem;

                double PercentOfSystem = 0;
                if (totalSystemProfit > 0)
                {
                    PercentOfSystem = totalProfit * 100 / totalSystemProfit;
                }
                var newCustomer = CustomerController.Report(user, fromdate, todate);
                int totalOrder = userReport.Sum(x => x.totalOrder);
                ltrTotalSaleOrder.Text = totalOrder + " đơn";
                ltrAverageSaleOrder.Text = (totalOrder / day).ToString() + " đơn/ngày";
                ltrTotalSoldQuantity.Text = totalSoldQuantity.ToString() + " cái";
                ltrAverageSoldQuantity.Text = averageSoldQuantity.ToString() + " cái/ngày";
                ltrTotalRefundQuantity.Text = totalRefundQuantity.ToString() + " cái";
                ltrAverageRefundQuantity.Text = averageRefundQuantity.ToString() + " cái/ngày";
                ltrTotalRemainQuantity.Text = (totalSoldQuantity - totalRefundQuantity).ToString() + " cái";
                ltrAverageRemainQuantity.Text = ((totalSoldQuantity - totalRefundQuantity) / day).ToString() + " cái/ngày";
                ltrPercentOfSystem.Text = Math.Round(PercentOfSystem, 1).ToString() + "%";
                ltrTotalNewCustomer.Text = newCustomer.Count() + " khách mới";

                if (day > 1)
                {
                    string chartLabelDays = "";
                    string chartTotalNewCustomer = "";
                    string chartTotalRemainQuantity = "";
                    string chartPercentOfSystem = "";

                    List<string> dataDays = new List<string>();
                    List<string> dataTotalNewCustomer = new List<string>();
                    List<string> dataTotalRemainQuantity = new List<string>();
                    List<string> dataPercentOfSystem = new List<string>();

                    while (fromdate < todate)
                    {
                        // Ngày biểu đồ
                        dataDays.Add(String.Format("'{0:d/M}'", fromdate));

                        // Biểu đồ sản lượng
                        int TotalSoldQuantity = userReport.Where(x => x.reportDate.Date == fromdate).Sum(x => x.totalSold);
                        int TotalRefundQuantity = userRefundReport.Where(x => x.reportDate == fromdate).Sum(x => x.totalRefund);
                        dataTotalRemainQuantity.Add((TotalSoldQuantity - TotalRefundQuantity).ToString());

                        // Biểu đồ khách mới
                        int TotalNewCustomer = newCustomer.Where(x => x.CreatedDate.Value.Date == fromdate).Count();
                        dataTotalNewCustomer.Add(TotalNewCustomer.ToString());

                        // Biểu đồ phần trăm lợi nhuận
                        double charttotalRevenue = userReport.Where(x => x.reportDate.Date == fromdate).Sum(x => x.totalRevenue);
                        double charttotalCost = userReport.Where(x => x.reportDate.Date == fromdate).Sum(x => x.totalCost);
                        double charttotalRefundRevenue = userRefundReport.Where(x => x.reportDate.Date == fromdate).Sum(x => x.totalRevenue);
                        double charttotalRefundCost = userRefundReport.Where(x => x.reportDate.Date == fromdate).Sum(x => x.totalCost);
                        double charttotalRefundFee = userRefundReport.Where(x => x.reportDate.Date == fromdate).Sum(x => x.totalRefundFee);
                        double charttotalProfit = (charttotalRevenue - charttotalCost) - (charttotalRefundRevenue - charttotalRefundCost) + charttotalRefundFee;

                        double charttotalRevenueSystem = systemReport.Where(x => x.reportDate.Date == fromdate).Sum(x => x.totalRevenue);
                        double charttotalCostSystem = systemReport.Where(x => x.reportDate.Date == fromdate).Sum(x => x.totalCost);
                        double charttotalRefundRevenueSystem = systemRefundReport.Where(x => x.reportDate.Date == fromdate).Sum(x => x.totalRevenue);
                        double charttotalRefundCostSystem = systemRefundReport.Where(x => x.reportDate.Date == fromdate).Sum(x => x.totalCost);
                        double charttotalRefundFeeSystem = systemRefundReport.Where(x => x.reportDate.Date == fromdate).Sum(x => x.totalRefundFee);
                        double charttotalSystemProfit = (charttotalRevenueSystem - charttotalCostSystem) - (charttotalRefundRevenueSystem - charttotalRefundCostSystem) + charttotalRefundFeeSystem;
                        double chartResultPercentOfSystem = 0;
                        if (totalSystemProfit > 0)
                        {
                            chartResultPercentOfSystem = charttotalProfit * 100 / charttotalSystemProfit;
                        }
                        dataPercentOfSystem.Add(Math.Round(chartResultPercentOfSystem, 1).ToString());


                        // Thêm 1 ngày chạy vòng lặp
                        fromdate = fromdate.AddDays(1);
                    }

                    chartLabelDays = String.Join(", ", dataDays);
                    chartTotalNewCustomer = String.Join(", ", dataTotalNewCustomer);
                    chartTotalRemainQuantity = String.Join(", ", dataTotalRemainQuantity);
                    chartPercentOfSystem = String.Join(", ", dataPercentOfSystem);

                    StringBuilder html = new StringBuilder();
                    html.Append("<script>");
                    html.Append("var lineChartData = {");
                    html.Append("	labels: [" + chartLabelDays + "],");
                    html.Append("	datasets: [{");
                    html.Append("		label: 'Sản lượng',");
                    html.Append("		borderColor: 'rgb(255, 99, 132)',");
                    html.Append("		backgroundColor: 'rgb(255, 99, 132)',");
                    html.Append("		fill: false,");
                    html.Append("		data: [" + chartTotalRemainQuantity + "],");
                    html.Append("		yAxisID: 'y-axis-1',");
                    html.Append("	}]");
                    html.Append("};");
                    html.Append("var lineChartData2 = {");
                    html.Append("	labels: [" + chartLabelDays + "],");
                    html.Append("	datasets: [{");
                    html.Append("		label: 'Khách mới',");
                    html.Append("		borderColor: 'rgb(54, 162, 235)',");
                    html.Append("		backgroundColor: 'rgb(54, 162, 235)',");
                    html.Append("		fill: false,");
                    html.Append("		data: [" + chartTotalNewCustomer + "],");
                    html.Append("		yAxisID: 'y-axis-1'");
                    html.Append("	}]");
                    html.Append("};");
                    html.Append("var lineChartData3 = {");
                    html.Append("	labels: [" + chartLabelDays + "],");
                    html.Append("	datasets: [{");
                    html.Append("		label: 'Phần trăm / hệ thống',");
                    html.Append("		borderColor: 'rgb(255, 205, 86)',");
                    html.Append("		backgroundColor: 'rgb(255, 205, 86)',");
                    html.Append("		fill: false,");
                    html.Append("		data: [" + chartPercentOfSystem + "],");
                    html.Append("		yAxisID: 'y-axis-1'");
                    html.Append("	}]");
                    html.Append("};");
                    html.Append("</script>");

                    ltrChartData.Text = html.ToString();
                }
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