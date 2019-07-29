using IM_PJ.Controllers;
using IM_PJ.Models;
using MB.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;
using static IM_PJ.Controllers.ProductController;
using IM_PJ.Utils;
using IM_PJ.Models.Pages.thong_ke_nhap_kho;

namespace IM_PJ
{
    public partial class thong_ke_nhap_kho : System.Web.UI.Page
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
                        LoadData();
                        LoadCategory();
                    }
                }
                else
                {
                    Response.Redirect("/dang-nhap");
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
            string username = Request.Cookies["userLoginSystem"].Value;
            var acc = AccountController.GetByUsername(username);
            if (acc != null)
            {
                string TextSearch = "";
                string CreatedDate = "today";
                int CategoryID = 0;
                string strColor = String.Empty;
                string strSize = String.Empty;
                int Page = 1;

                if (Request.QueryString["textsearch"] != null)
                    TextSearch = Request.QueryString["textsearch"].Trim();
                if (Request.QueryString["categoryid"] != null)
                    CategoryID = Request.QueryString["categoryid"].ToInt();
                if (Request.QueryString["createddate"] != null)
                    CreatedDate = Request.QueryString["createddate"];
                // Add filter valiable value
                if (Request.QueryString["color"] != null)
                    strColor = Request.QueryString["color"].Trim();
                if (Request.QueryString["size"] != null)
                    strSize = Request.QueryString["size"].Trim();
                if (Request.QueryString["Page"] != null)
                {
                    Page = Request.QueryString["Page"].ToInt();
                }

                txtSearchProduct.Text = TextSearch;
                ddlCategory.SelectedValue = CategoryID.ToString();
                ddlCreatedDate.SelectedValue = CreatedDate.ToString();

                // Add filter valiable value
                ddlColor.SelectedValue = strColor;
                ddlSize.SelectedValue = strSize;

                // Create order fileter
                var filter = new ProductFilterModel()
                {
                    search = TextSearch,
                    category = CategoryID,
                    goodsReceiptDate = CreatedDate,
                    color = strColor,
                    size = strSize
                };

                // Create pagination
                var page = new PaginationMetadataModel()
                {
                    currentPage = Page
                };
                List<GoodsReceiptReport> a = new List<GoodsReceiptReport>();
                a = StockManagerController.getGoodsReceiptReport(filter, ref page);

                pagingall(filter, a, page);

                ltrNumberOfProduct.Text = page.totalCount.ToString();
            }
        }
        #region Paging
        public void pagingall(ProductFilterModel filter, List<GoodsReceiptReport> acs, PaginationMetadataModel page)
        {
            string username = Request.Cookies["userLoginSystem"].Value;
            var acc = AccountController.GetByUsername(username);

            StringBuilder html = new StringBuilder();
            html.AppendLine("<thead>");
            html.AppendLine("<tr>");
            html.AppendLine("    <th class='image-column'>Ảnh</th>");
            html.AppendLine("    <th class='name-column'>Sản phẩm</th>");
            html.AppendLine("    <th class='sku-column'>Mã</th>");
            html.AppendLine("    <th class='quantity-input-column'>Thêm mới</th>");
            html.AppendLine("    <th class='quantity-stock-column'>Kho</th>");
            html.AppendLine("    <th class='category-column'>Danh mục</th>");
            html.AppendLine("    <th class='date-column'>Ngày nhập kho</th>");
            html.AppendLine("    <th class='action-column'></th>");
            html.AppendLine("</tr>");
            html.AppendLine("</thead>");

            html.AppendLine("<tbody>");
            if (acs.Count > 0)
            {
                PageCount = page.totalPages;
                Int32 Page = page.currentPage;

                foreach (var item in acs)
                {
                    html.AppendLine("<tr>");
                    html.AppendLine("   <td>");
                    html.AppendLine("      <a href='/xem-san-pham?sku=" + item.sku + "'>");
                    html.AppendLine("          <img src='" + Thumbnail.getURL(item.image, Thumbnail.Size.Small) + "'/>");
                    html.AppendLine("      </a>");
                    html.AppendLine("   </td>");
                    html.AppendLine("   <td class='customer-name-link'>");
                    html.AppendLine("       <a href='/xem-san-pham?sku=" + item.sku + "'>" + item.title + "</a></td>");
                    html.AppendLine("   </td>");
                    html.AppendLine("   <td data-title='Mã' class='customer-name-link'>" + item.sku + "</td>");
                    html.AppendLine("   <td data-title='Thêm mới'>" + String.Format("{0:N0}", item.quantityInput) + "</td>");
                    html.AppendLine("   <td data-title='Kho'>" + String.Format("{0:N0}", item.quantityStock) + "</td>");
                    html.AppendLine("   <td data-title='Danh mục'>" + item.categoryName + "</td>");
                    html.AppendLine("   <td data-title='Ngày tạo'>" + String.Format("{0:dd/MM/yyyy}", item.goodsReceiptDate) + "</td>");
                    html.AppendLine("   <td data-title='Thao tác' class='update-button'>");
                    if (item.isVariable)
                    {
                        html.AppendLine("      <a href='javascript:;' title='Xem thông tin sản phẩm con' class='btn primary-btn h45-btn' onclick='showSubGoodsReceipt(`" + item.sku + "`)'>");
                        html.AppendLine("           <i class='fa fa fa-plus' aria-hidden='true'></i>");
                        html.AppendLine("      </a>");
                    }
                    html.AppendLine("  </td>");
                    html.AppendLine("</tr>");

                    var productVariables = StockManagerController.getSubGoodsReceipt(filter, item);
                    foreach (var subItem in productVariables)
                    {
                        html.AppendLine("<tr class='" + item.sku + "' style='display: none; '>");
                        html.AppendLine("   <td></td>");
                        html.AppendLine("   <td>");
                        html.AppendLine("      <a href='/xem-san-pham?sku=" + subItem.sku + "'>");
                        html.AppendLine("          <img src='" + Thumbnail.getURL(subItem.image, Thumbnail.Size.Small) + "'/>");
                        html.AppendLine("      </a>");
                        html.AppendLine("   </td>");
                        html.AppendLine("   <td data-title='Mã' class='customer-name-link'>" + subItem.sku + "</td>");
                        html.AppendLine("   <td data-title='Thêm mới'>" + String.Format("{0:N0}", subItem.quantityInput) + "</td>");
                        html.AppendLine("   <td data-title='Kho'>" + String.Format("{0:N0}", subItem.quantityStock) + "</td>");
                        html.AppendLine("   <td data-title='Danh mục'>" + subItem.categoryName + "</td>");
                        html.AppendLine("   <td data-title='Ngày tạo'>" + String.Format("{0:dd/MM/yyyy}", subItem.goodsReceiptDate) + "</td>");
                        html.AppendLine("   <td data-title='Thao tác' class='update-button'></td>");
                        html.AppendLine("</tr>");
                    }
                }

            }
            else
            {
                html.Append("<tr><td colspan='11'>Không tìm thấy sản phẩm...</td></tr>");
            }
            html.Append("</tbody>");

            ltrList.Text = html.ToString();
        }

        private int PageCount;
        protected void DisplayHtmlStringPaging1()
        {

            Int32 CurrentPage = Convert.ToInt32(Request.QueryString["Page"]);
            if (CurrentPage == -1) CurrentPage = 1;
            string[] strText = new string[4] { "Trang đầu", "Trang cuối", "Trang sau", "Trang trước" };
            if (PageCount > 1)
                Response.Write(GetHtmlPagingAdvanced(6, CurrentPage, PageCount, Context.Request.RawUrl, strText));

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
                output.Append("<li><a title=\"" + strText[0] + "\" href=\"" + string.Format(pageUrl, 1) + "\">Trang đầu</a></li>");
                output.Append("<li><a title=\"" + strText[1] + "\" href=\"" + string.Format(pageUrl, currentPage - 1) + "\">Trang trước</a></li>");
                //output.Append("<li class=\"UnselectedPrev\" ><a title=\"" + strText[1] + "\" href=\"" + string.Format(pageUrl, currentPage - 1) + "\"><i class=\"fa fa-angle-left\"></i></a></li>");

                //output.Append("<span class=\"Unselect_prev\"><a href=\"" + string.Format(pageUrl, currentPage - 1) + "\"></a></span>");
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

            //Các dấu ... chỉ những trang phía trước  
            if (startPageNumbersFrom > 1)
            {
                output.Append("<li><a href=\"" + string.Format(GetPageUrl(currentPage - 1, pageUrl), startPageNumbersFrom - 1) + "\">&hellip;</a></li>");
            }

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

            //Các dấu ... chỉ những trang tiếp theo  
            if (stopPageNumbersAt < pageCount)
            {
                output.Append("<li><a href=\"" + string.Format(pageUrl, stopPageNumbersAt + 1) + "\">&hellip;</a></li>");
            }

            //Link Next(Trang tiếp) và Last(Trang cuối)
            if (currentPage != pageCount)
            {
                //output.Append("<span class=\"Unselect_next\"><a href=\"" + string.Format(pageUrl, currentPage + 1) + "\"></a></span>");
                //output.Append("<li class=\"UnselectedNext\" ><a title=\"" + strText[2] + "\" href=\"" + string.Format(pageUrl, currentPage + 1) + "\"><i class=\"fa fa-angle-right\"></i></a></li>");
                output.Append("<li><a title=\"" + strText[2] + "\" href=\"" + string.Format(pageUrl, currentPage + 1) + "\">Trang sau</a></li>");
                output.Append("<li><a title=\"" + strText[3] + "\" href=\"" + string.Format(pageUrl, pageCount) + "\">Trang cuối</a></li>");
            }
            output.Append("</ul>");
            //output.Append("</div>");
            return output.ToString();
        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string search = txtSearchProduct.Text;
            string request = "/thong-ke-nhap-kho?";

            if (search != "")
                request += "&textsearch=" + search;

            if (ddlCategory.SelectedValue != "0")
                request += "&categoryid=" + ddlCategory.SelectedValue;

            if (ddlCreatedDate.SelectedValue != "")
                request += "&createddate=" + ddlCreatedDate.SelectedValue;

            // Add filter valiable value
            if (!String.IsNullOrEmpty(ddlColor.SelectedValue))
            {
                request += "&color=" + ddlColor.SelectedValue;
            }
            if (!String.IsNullOrEmpty(ddlSize.SelectedValue))
            {
                request += "&size=" + ddlSize.SelectedValue;
            }

            Response.Redirect(request);
        }
    }
}