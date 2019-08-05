using IM_PJ.Controllers;
using IM_PJ.Models;
using IM_PJ.Utils;
using MB.Extensions;
using Newtonsoft.Json;
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
    public partial class dang_ky_nhap_hang : System.Web.UI.Page
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
            string TextSearch = "";
            string CreatedDate = "";
            int CategoryID = 0;
            int StockStatus = 0;
            string strColor = String.Empty;
            string strSize = String.Empty;
            int Page = 1;

            if (Request.QueryString["textsearch"] != null)
                TextSearch = Request.QueryString["textsearch"].Trim();
            if (Request.QueryString["stockstatus"] != null)
                StockStatus = Request.QueryString["stockstatus"].ToInt();
            if (Request.QueryString["categoryid"] != null)
                CategoryID = Request.QueryString["categoryid"].ToInt();
            if (Request.QueryString["createddate"] != null)
                CreatedDate = Request.QueryString["createddate"];
            if (Request.QueryString["color"] != null)
                strColor = Request.QueryString["color"].Trim();
            if (Request.QueryString["size"] != null)
                strSize = Request.QueryString["size"].Trim();
            if (Request.QueryString["Page"] != null)
                Page = Request.QueryString["Page"].ToInt();

            txtSearchProduct.Text = TextSearch;
            ddlColor.SelectedValue = strColor;
            ddlSize.SelectedValue = strSize;
            ddlCategory.SelectedValue = CategoryID.ToString();
            ddlCreatedDate.SelectedValue = CreatedDate.ToString();
            ddlStockStatus.SelectedValue = StockStatus.ToString();

            // Create order fileter
            var filter = new ProductFilterModel()
            {
                category = CategoryID,
                search = UnSign.convert(TextSearch),
                color = strColor,
                size = strSize,
                stockStatus = StockStatus,
                productDate = CreatedDate
            };
            // Create pagination
            var page = new PaginationMetadataModel()
            {
                currentPage = Page,
                pageSize = 28
            };
            List<Product> a = new List<Product>();
            a = ProductController.GetAllProduct(filter, ref page);

            pagingall(a, page);

        }
        [WebMethod]
        #region Paging
        public void pagingall(List<Product> acs, PaginationMetadataModel page)
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
                    item.content = String.Empty;
                    html.AppendLine("<div class='col-md-3 item-" + index + " product-item'>");
                    html.AppendLine("    <div class='row'>");
                    html.AppendLine("        <div class='col-xs-12'>");
                    html.AppendLine("            <p><a href='/xem-sp?id=" + item.productID + "'><img src='" + Thumbnail.getURL(item.image, Thumbnail.Size.Large) + "'></a></p>");
                    html.AppendLine("            <h3 class='product-name'><a href='/xem-sp?id=" + item.productID + "'>" + item.sku + " - " + item.title + "</a></h3>");
                    html.AppendLine("            <h3 class='product-price'>📌 " + string.Format("{0:N0}", item.regularPrice) + "</h3>");

                    if (!string.IsNullOrEmpty(item.color))
                    {
                        html.AppendLine("            <p>🔖 Màu: " + item.color + "</p>");
                    }

                    if (!string.IsNullOrEmpty(item.size))
                    {
                        html.AppendLine("            <p>🔖 Size: " + item.size + "</p>");
                    }

                    if (item.quantity > 0)
                    {
                        html.AppendLine("            <p>🔖 <span class='bg-green'>Còn hàng</span> (" + string.Format("{0:N0}", item.quantity) + " cái)</p>");
                    }
                    else
                    {
                        html.AppendLine("            <p>🔖 <span class='bg-red'>Hết hàng</span></p>");
                    }

                    html.AppendLine("            <p>🔖 " + string.Format("{0:dd/MM/yyyy}", item.createdDate) + "</p>");
                    html.AppendLine("        </div>");
                    html.AppendLine("    </div>");
                    html.AppendLine("    <div class='row'>");
                    html.AppendLine("         <div class='col-xs-12'>");
                    html.AppendLine("              <div class='col-xs-6'>");
                    html.AppendLine("                   <div class='row'>");
                    html.AppendLine("                      <a href='javascript:;' class='btn primary-btn copy-btn h45-btn' onclick='openRegister(" + JsonConvert.SerializeObject(item) + ")'><i class='fa fa-cart-plus' aria-hidden='true'></i> Đăng ký</a>");
                    html.AppendLine("                   </div>");
                    html.AppendLine("              </div>");
                    html.AppendLine("         </div>");
                    html.AppendLine("    </div>");
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
            string request = "/dang-ky-nhap-hang?";

            if (search != "")
            {
                request += "&textsearch=" + search;
            }

            if (!String.IsNullOrEmpty(ddlColor.SelectedValue))
            {
                request += "&color=" + ddlColor.SelectedValue;
            }
            if (!String.IsNullOrEmpty(ddlSize.SelectedValue))
            {
                request += "&size=" + ddlSize.SelectedValue;
            }

            if (ddlStockStatus.SelectedValue != "")
            {
                request += "&stockstatus=" + ddlStockStatus.SelectedValue;
            }

            if (ddlCategory.SelectedValue != "0")
            {
                request += "&categoryid=" + ddlCategory.SelectedValue;
            }

            if (ddlCreatedDate.SelectedValue != "")
            {
                request += "&createddate=" + ddlCreatedDate.SelectedValue;
            }

            Response.Redirect(request);
        }

        [WebMethod]
        public static string registerProduct(Models.RegisterProduct item)
        {
            string username = HttpContext.Current.Request.Cookies["loginHiddenPage"].Value;
            var acc = AccountController.GetByUsername(username);
            var now = DateTime.Now;

            item.CreatedBy = acc.ID;
            item.CreatedDate = now;
            item.ModifiedBy = acc.ID;
            item.ModifiedDate = now;

            return RegisterProductController.Inster(item);
        }
    }
}