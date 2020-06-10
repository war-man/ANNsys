using IM_PJ.Controllers;
using IM_PJ.Models;
using IM_PJ.Utils;
using MB.Extensions;
using Newtonsoft.Json;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;
using static IM_PJ.Controllers.CustomerController;

namespace IM_PJ
{
    public partial class kh : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["loginHiddenPage"] != null)
                {
                    LoadData();
                }
                else
                {
                    Response.Redirect("/login-hidden-page");
                }
            }
        }

        public void LoadData()
        {
            string username = Request.Cookies["loginHiddenPage"].Value;
            var acc = AccountController.GetByUsername(username);
            if (acc != null)
            {
                if (Request.QueryString["textsearch"] != null)
                {
                    DateTime DateConfig = new DateTime(2018, 6, 21);
                    DateTime FromDate = DateConfig;
                    DateTime ToDate = DateTime.Now;

                    string textSearch = Request.QueryString["textsearch"].Trim();
                    txtSearch.Text = textSearch;

                    List<CustomerOut> rs = new List<CustomerOut>();
                    rs = CustomerController.Filter(textSearch, "", 0, "", FromDate, ToDate);
                    pagingall(rs);

                    ltrHeading.Text = "Khách hàng (" + rs.Count.ToString() + ")";
                    ltrAccount.Text = "Tài khoản: <strong>" + acc.Username + "</strong>";
                }
            }
        }
        [WebMethod]
        #region Paging
        public void pagingall(List<CustomerOut> acs)
        {
            string username = Request.Cookies["loginHiddenPage"].Value;
            var acc = AccountController.GetByUsername(username);

            int PageSize = 30;

            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class='row'>");

            if (acs.Count > 0)
            {
                int TotalItems = acs.Count;
                if (TotalItems % PageSize == 0)
                    PageCount = TotalItems / PageSize;
                else
                    PageCount = TotalItems / PageSize + 1;

                Int32 Page = GetIntFromQueryString("Page");

                if (Page == -1) Page = 1;
                int FromRow = (Page - 1) * PageSize;
                int ToRow = Page * PageSize - 1;
                if (ToRow >= TotalItems)
                    ToRow = TotalItems - 1;
                for (int i = FromRow; i < ToRow + 1; i++)
                {
                    var item = acs[i];

                    html.AppendLine("<div id='" + item.ID+ "' class='col-xs-12 product-item'>");
                    html.AppendLine("    <div class='row'>");
                    html.AppendLine("        <div class='col-xs-12'>");
                    html.AppendLine("            <h3>" + item.CustomerName.ToLower().ToTitleCase() + "</h3>");
                    html.AppendLine("        </div>");
                    html.AppendLine("    </div>");
                    if (!string.IsNullOrEmpty(item.Nick))
                    {
                        html.AppendLine("    <div class='row'>");
                        html.AppendLine("        <div class='col-xs-4 col-md-3'>");
                        html.AppendLine("            <p>Nick:</p>");
                        html.AppendLine("        </div>");
                        html.AppendLine("        <div class='col-xs-8 col-md-9'>");
                        html.AppendLine("           <p class='customer'>" + item.Nick + "</p>");
                        html.AppendLine("        </div>");
                        html.AppendLine("    </div>");
                    }
                    if (item.CreatedBy == acc.Username || acc.RoleID == 0)
                    {
                        html.AppendLine("    <div class='row'>");
                        html.AppendLine("        <div class='col-xs-4 col-md-3'>");
                        html.AppendLine("            <p>Điện thoại:</p>");
                        html.AppendLine("        </div>");
                        html.AppendLine("        <div class='col-xs-8 col-md-9'>");
                        html.AppendLine("           <p class='customer'>" + item.CustomerPhone + "</p>");
                        html.AppendLine("        </div>");
                        html.AppendLine("    </div>");

                        if (!string.IsNullOrEmpty(item.CustomerAddress))
                        {
                            html.AppendLine("    <div class='row'>");
                            html.AppendLine("        <div class='col-xs-4 col-md-3'>");
                            html.AppendLine("            <p>Địa chỉ:</p>");
                            html.AppendLine("        </div>");
                            html.AppendLine("        <div class='col-xs-8 col-md-9'>");
                            html.AppendLine("           <p class='customer'>" + item.CustomerAddress.ToTitleCase() + "</p>");
                            html.AppendLine("        </div>");
                            html.AppendLine("    </div>");
                        }

                        if (!string.IsNullOrEmpty(item.Zalo))
                        {
                            html.AppendLine("    <div class='row'>");
                            html.AppendLine("        <div class='col-xs-4 col-md-3'>");
                            html.AppendLine("            <p>Zalo:</p>");
                            html.AppendLine("        </div>");
                            html.AppendLine("        <div class='col-xs-8 col-md-9'>");
                            html.AppendLine("           <p class='customer'>" + item.Zalo + "</p>");
                            html.AppendLine("        </div>");
                            html.AppendLine("    </div>");
                        }

                        if (!string.IsNullOrEmpty(item.Facebook) && item.Facebook != "zalo ann")
                        {
                            html.AppendLine("    <div class='row'>");
                            html.AppendLine("        <div class='col-xs-4 col-md-3'>");
                            html.AppendLine("            <p>Facebook:</p>");
                            html.AppendLine("        </div>");
                            html.AppendLine("        <div class='col-xs-8 col-md-9'>");
                            html.AppendLine("           <p class='customer'><a class='link' href='" + item.Facebook + "' target='_blank'>Xem</a></p>");
                            html.AppendLine("        </div>");
                            html.AppendLine("    </div>");
                        }

                        html.AppendLine("    <div class='row'>");
                        html.AppendLine("        <div class='col-xs-4 col-md-3'>");
                        html.AppendLine("            <p>Số đơn:</p>");
                        html.AppendLine("        </div>");
                        html.AppendLine("        <div class='col-xs-8 col-md-9'>");
                        html.AppendLine("           <p class='customer'>" + item.TotalOrder + " đơn</p>");
                        html.AppendLine("        </div>");
                        html.AppendLine("    </div>");
                        html.AppendLine("    <div class='row'>");
                        html.AppendLine("        <div class='col-xs-4 col-md-3'>");
                        html.AppendLine("            <p>Số lượng:</p>");
                        html.AppendLine("        </div>");
                        html.AppendLine("        <div class='col-xs-8 col-md-9'>");
                        html.AppendLine("           <p class='customer'>" + item.TotalQuantity + " cái</p>");
                        html.AppendLine("        </div>");
                        html.AppendLine("    </div>");
                    }
                    html.AppendLine("    <div class='row'>");
                    html.AppendLine("        <div class='col-xs-4 col-md-3'>");
                    html.AppendLine("            <p>Nhân viên:</p>");
                    html.AppendLine("        </div>");
                    html.AppendLine("        <div class='col-xs-8 col-md-9'>");
                    html.AppendLine("           <p class='customer'>" + item.CreatedBy + "</p>");
                    html.AppendLine("        </div>");
                    html.AppendLine("    </div>");
                    html.AppendLine("    <div class='row'>");
                    html.AppendLine("        <div class='col-xs-4 col-md-3'>");
                    html.AppendLine("            <p>Ngày tạo:</p>");
                    html.AppendLine("        </div>");
                    html.AppendLine("        <div class='col-xs-8 col-md-9'>");
                    html.AppendLine("           <p class='customer'>" + string.Format("{0:dd/MM/yyyy}", item.CreatedDate) + "</p>");
                    html.AppendLine("        </div>");
                    html.AppendLine("    </div>");

                    html.AppendLine("</div>");
                    if ((i + 1) % 4 == 0)
                    {
                        html.AppendLine("<div class='clear'></div>");
                    }
                }
            }
            else
            {
                html.AppendLine("<div class='col-md-12'>Không tìm thấy khách hàng...</div>");
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
            string search = txtSearch.Text;
            string request = "/kh?";

            if (!String.IsNullOrEmpty(search))
            {
                request += "&textsearch=" + search;
            }

            Response.Redirect(request);
        }
    }
}