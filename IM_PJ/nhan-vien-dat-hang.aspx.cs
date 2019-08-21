using IM_PJ.Controllers;
using IM_PJ.Models;
using IM_PJ.Utils;
using MB.Extensions;
using Newtonsoft.Json;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;
using static IM_PJ.Controllers.ProductController;

namespace IM_PJ
{
    public partial class nhan_vien_dat_hang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["loginHiddenPage"] != null)
                {
                    LoadData();
                    LoadCategory();
                }
                else
                {
                    Response.Redirect("/login-hidden-page");
                }
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
            string username = Request.Cookies["loginHiddenPage"].Value;
            var acc = AccountController.GetByUsername(username);

            DateTime DateConfig = new DateTime(2019, 2, 15);

            var config = ConfigController.GetByTop1();
            if (config.ViewAllOrders == 1)
            {
                DateConfig = new DateTime(2018, 6, 22);
            }

            DateTime fromDate = DateConfig;
            DateTime toDate = DateTime.Now;

            if (!String.IsNullOrEmpty(Request.QueryString["orderfromdate"]))
            {
                fromDate = Convert.ToDateTime(Request.QueryString["orderfromdate"]);
            }

            if (!String.IsNullOrEmpty(Request.QueryString["ordertodate"]))
            {
                toDate = Convert.ToDateTime(Request.QueryString["ordertodate"]).AddDays(1).AddMinutes(-1);
            }

            rFromDate.SelectedDate = fromDate;
            rFromDate.MinDate = DateConfig;
            rFromDate.MaxDate = DateTime.Now;

            rToDate.SelectedDate = toDate;
            rToDate.MinDate = DateConfig;
            rToDate.MaxDate = DateTime.Now;

            string textSearch = "";
            int categoryID = 0;
            int registerStatus = 0;
            int Page = 1;

            if (Request.QueryString["textsearch"] != null)
                textSearch = Request.QueryString["textsearch"].Trim();
            if (Request.QueryString["categoryid"] != null)
                categoryID = Request.QueryString["categoryid"].ToInt();
            if (Request.QueryString["registerstatus"] != null)
                registerStatus = Request.QueryString["registerstatus"].ToInt(0);
            if (Request.QueryString["Page"] != null)
                Page = Request.QueryString["Page"].ToInt();

            txtSearchProduct.Text = textSearch;
            ddlCategory.SelectedValue = categoryID.ToString();
            ddlRegisterStatus.SelectedValue = registerStatus.ToString();

            // Create order fileter
            var filter = new RegisterProductFilterModel()
            {
                search = textSearch,
                category = categoryID,
                status = registerStatus,
                createdBy = acc.Username,
                fromDate = fromDate,
                toDate = toDate
            };
            // Create pagination
            var page = new PaginationMetadataModel()
            {
                currentPage = Page,
                pageSize = 30
            };
            List<RegisterProductList> rs = new List<RegisterProductList>();
            rs = RegisterProductController.Filter(filter, ref page);

            pagingall(rs, page);
            ltrHeading.Text = "Danh sách đặt hàng (" + page.totalCount.ToString() + ")";
            ltrAccount.Text = "Tài khoản: <strong>" + acc.Username + "</strong>";

        }
        [WebMethod]
        #region Paging
        public void pagingall(List<RegisterProductList> acs, PaginationMetadataModel page)
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class='row'>");

            if (acs.Count > 0)
            {
                PageCount = page.totalPages;
                Int32 Page = page.currentPage;
                var index = 0;

                foreach (var item in acs)
                {
                    html.AppendLine("<div id='" + item.id+ "' class='col-xs-12 item-" + index + " product-item'");
                    html.AppendLine("    data-id='" + item.id + "'");
                    html.AppendLine("    data-customer='" + item.customer + "'");
                    html.AppendLine("    data-status='" + item.status + "'");
                    html.AppendLine("    data-color='" + item.color + "'");
                    html.AppendLine("    data-size='" + item.size + "'");
                    html.AppendLine("    data-quantity='" + item.quantity + "'");
                    html.AppendLine("    data-note1='" + item.note1 + "'");
                    html.AppendLine(">");
                    html.AppendLine("    <div class='row'>");
                    html.AppendLine("        <div class='col-xs-12'>");
                    html.AppendLine("            <p class='product-name'><a href='/xem-sp?id=" + item.productID + "'>" + item.sku + " - " + PJUtils.Truncate(item.title, 29) + "</a></p>");
                    html.AppendLine("        </div>");
                    html.AppendLine("    </div>");
                    html.AppendLine("    <div class='row'>");
                    html.AppendLine("        <div class='col-xs-5 col-md-3'>");
                    html.AppendLine("            <p><a href='/xem-sp?id=" + item.productID + "'><img src='" + Thumbnail.getURL(item.image, Thumbnail.Size.Large) + "'></a></p>");
                    html.AppendLine("        </div>");
                    html.AppendLine("        <div class='col-xs-7 col-md-9'>");
                    html.AppendLine("                <p class='customer'>" + item.customer.ToTitleCase() + "</p>");
                    
                    if (!string.IsNullOrEmpty(item.color))
                    {
                        html.AppendLine("                <p>Màu: " + item.color + "</p>");
                    }
                    
                    if (!string.IsNullOrEmpty(item.size))
                    {
                        html.AppendLine("                <p>Size: " + item.size + "</p>");
                    }
                    
                    html.AppendLine("                <p class='quantity'>Số lượng: " + String.Format("{0:#,###}", item.quantity) + "</p>");

                    if (!string.IsNullOrEmpty(item.note1))
                    {
                        html.AppendLine("                <p class='note1'><strong>Nhân viên:</strong> " + item.note1 + "</p>");
                    }
                    if (!string.IsNullOrEmpty(item.note2))
                    {
                        html.AppendLine("                <p><strong>Quản lý:</strong> " + item.note2 + "</p>");
                    }

                    html.AppendLine("                <p class='status'>");
                    switch (item.status)
                    {
                        case (int)RegisterProductStatus.Approve:
                            html.AppendLine("                    <span class='bg-green'>" + item.statusName + "</span>");
                            break;
                        case (int)RegisterProductStatus.Ordering:
                            html.AppendLine("                    <span class='bg-yellow'>" + item.statusName + "</span>");
                            break;
                        case (int)RegisterProductStatus.Done:
                            html.AppendLine("                    <span class='bg-blue'>" + item.statusName + "</span>");
                            break;
                        default:
                            html.AppendLine("                    <span class='bg-red'>" + item.statusName + "</span>");
                            break;
                    }
                    html.AppendLine("                </p>");
                    html.AppendLine("                <p class='status'>Ngày đặt: " + String.Format("{0:HH:mm dd/MM}", item.createdDate) + "</p>");
                    if (item.expectedDate != null)
                    {
                        html.AppendLine("                <p class='status'>Ngày về dự kiến: " + String.Format("{0:dd/MM}", item.expectedDate) + "</p>");
                    }
                    html.AppendLine("        </div>");
                    html.AppendLine("    </div>");
                    
                    if (item.status == (int)RegisterProductStatus.NoApprove)
                    {
                        html.AppendLine("    <div class='row btn-handle'>");
                        html.AppendLine("          <div class='col-xs-12'>");
                        html.AppendLine("              <a href='javascript:;' class='bg-red remove-btn' onclick='removeRegister(" + item.id + ")'>");
                        html.AppendLine("                  <i class='glyphicon glyphicon-trash' aria-hidden='true'></i> Hủy</a>");
                        html.AppendLine("              </a>");
                        html.AppendLine("              <a href='javascript:;' class='bg-green' onclick='openRegister(" + item.id + ")'>");
                        html.AppendLine("                  <i class='glyphicon glyphicon-edit' aria-hidden='true'></i> Chỉnh sửa</a>");
                        html.AppendLine("              </a>");
                        html.AppendLine("          </div>");
                        html.AppendLine("    </div>");
                    }
                    
                    html.AppendLine("</div>");

                    if ((index + 1) % 4 == 0)
                    {
                        html.AppendLine("<div class='clear'></div>");
                    }
                    index++;
                }
            }
            else
            {
                html.AppendLine("<div class='col-md-12'>Không tìm thấy sản phẩm...</div>");
            }
            html.AppendLine("</div>");

            ltrList.Text = html.ToString();

        }
        public static Int32 GetIntFromQueryString(String key)
        {
            Int32 returnValue = -1;
            String queryStringValue = HttpContext.Current.Request.QueryString[key];
            try
            {
                if (queryStringValue == null)
                    return returnValue;
                if (queryStringValue.IndexOf("#") > 0)
                    queryStringValue = queryStringValue.Substring(0, queryStringValue.IndexOf("#"));
                returnValue = Convert.ToInt32(queryStringValue);
            }
            catch
            { }
            return returnValue;
        }
        private int PageCount;
        protected void DisplayHtmlStringPaging1()
        {

            Int32 CurrentPage = Convert.ToInt32(Request.QueryString["Page"]);
            if (CurrentPage == -1) CurrentPage = 1;
            string[] strText = new string[4] { "Trang đầu", "Trang cuối", "Trang sau", "Trang trước" };
            if (PageCount > 1)
                Response.Write(GetHtmlPagingAdvanced(4, CurrentPage, PageCount, Context.Request.RawUrl, strText));

        }
        private static string GetPageUrl(int currentPage, string pageUrl)
        {
            pageUrl = Regex.Replace(pageUrl, "(\\?|\\&)*" + "Page=" + currentPage, "");
            if (pageUrl.IndexOf("?") > 0)
            {
                pageUrl += "&Page={0}";
            }
            else
            {
                pageUrl += "?Page={0}";
            }
            return pageUrl;
        }
        public static string GetHtmlPagingAdvanced(int pagesToOutput, int currentPage, int pageCount, string currentPageUrl, string[] strText)
        {
            //Nếu Số trang hiển thị là số lẻ thì tăng thêm 1 thành chẵn
            if (pagesToOutput % 2 != 0)
            {
                pagesToOutput++;
            }

            //Một nửa số trang để đầu ra, đây là số lượng hai bên.
            int pagesToOutputHalfed = pagesToOutput / 2;

            //Url của trang
            string pageUrl = GetPageUrl(currentPage, currentPageUrl);


            //Trang đầu tiên
            int startPageNumbersFrom = currentPage - pagesToOutputHalfed; ;

            //Trang cuối cùng
            int stopPageNumbersAt = currentPage + pagesToOutputHalfed; ;

            StringBuilder output = new StringBuilder();

            //Nối chuỗi phân trang
            //output.Append("<div class=\"paging\">");
            output.Append("<ul>");

            //Link First(Trang đầu) và Previous(Trang trước)
            if (currentPage > 1)
            {
                output.Append("<li><a title=\"" + strText[0] + "\" href=\"" + string.Format(pageUrl, 1) + "\"><i class=\"fa fa-angle-left\"></i><i class=\"fa fa-angle-left\"></i></a></li>");
            }

            /******************Xác định startPageNumbersFrom & stopPageNumbersAt**********************/
            if (startPageNumbersFrom < 1)
            {
                startPageNumbersFrom = 1;

                //As page numbers are starting at one, output an even number of pages.  
                stopPageNumbersAt = pagesToOutput;
            }

            if (stopPageNumbersAt > pageCount)
            {
                stopPageNumbersAt = pageCount;
            }

            if ((stopPageNumbersAt - startPageNumbersFrom) < pagesToOutput)
            {
                startPageNumbersFrom = stopPageNumbersAt - pagesToOutput;
                if (startPageNumbersFrom < 1)
                {
                    startPageNumbersFrom = 1;
                }
            }
            /******************End: Xác định startPageNumbersFrom & stopPageNumbersAt**********************/

            //Duyệt vòng for hiển thị các trang
            for (int i = startPageNumbersFrom; i <= stopPageNumbersAt; i++)
            {
                if (currentPage == i)
                {
                    output.Append("<li class=\"current\" ><a >" + i.ToString() + "</a> </li>");
                }
                else
                {
                    output.Append("<li><a href=\"" + string.Format(pageUrl, i) + "\">" + i.ToString() + "</a> </li>");
                }
            }

            //Link Next(Trang tiếp) và Last(Trang cuối)
            if (currentPage != pageCount)
            {
                output.Append("<li><a title=\"" + strText[3] + "\" href=\"" + string.Format(pageUrl, pageCount) + "\"><i class=\"fa fa-angle-right\"></i><i class=\"fa fa-angle-right\"></i></a></li>");
            }

            output.Append("</ul>");
            //output.Append("</div>");
            return output.ToString();
        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string search = txtSearchProduct.Text;
            string request = "/nhan-vien-dat-hang?";

            if (!String.IsNullOrEmpty(search))
            {
                request += "&textsearch=" + search;
            }

            if (ddlCategory.SelectedValue != "0")
            {
                request += "&categoryid=" + ddlCategory.SelectedValue;
            }

            // Register Status
            if (ddlRegisterStatus.SelectedValue != "0")
                request += "&registerstatus=" + ddlRegisterStatus.SelectedValue;

            // Created Date
            if (rFromDate.SelectedDate.HasValue)
            {
                request += "&orderfromdate=" + rFromDate.SelectedDate.ToString();
            }
            if (rToDate.SelectedDate.HasValue)
            {
                request += "&ordertodate=" + rToDate.SelectedDate.ToString();
            }

            Response.Redirect(request);
        }

        [WebMethod]
        public static void updateRegister(Models.RegisterProduct item)
        {
            string username = HttpContext.Current.Request.Cookies["loginHiddenPage"].Value;
            var acc = AccountController.GetByUsername(username);
            var now = DateTime.Now;

            item.ModifiedBy = acc.ID;
            item.ModifiedDate = now;

            RegisterProductController.Update(item);
        }

        [WebMethod]
        public static void removeRegister(int registerID)
        {
            RegisterProductController.Delete(registerID);
        }
    }
}