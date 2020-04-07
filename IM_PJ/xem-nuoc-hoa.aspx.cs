using IM_PJ.Controllers;
using IM_PJ.Models;
using IM_PJ.Utils;
using MB.Extensions;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IM_PJ
{
    public partial class xem_nuoc_hoa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        public void LoadData()
        {
            int id = Request.QueryString["id"].ToInt(0);
            if (id > 0)
            {
                var p = ProductController.GetByID(id);
                if (p == null)
                {
                    PJUtils.ShowMessageBoxSwAlertError("Không tìm thấy sản phẩm", "e", true, "/", Page);
                }
                else
                {
                    if (p.CategoryID != 44)
                    {
                        PJUtils.ShowMessageBoxSwAlertError("Không tìm thấy sản phẩm", "e", true, "/", Page);
                    }

                    this.Title = String.Format("{0} - {1}", p.ProductTitle.ToTitleCase(), p.ProductSKU);

                    ViewState["ID"] = id;
                    ViewState["cateID"] = p.CategoryID;
                    ViewState["SKU"] = p.ProductSKU;

                    ltrProductName.Text = p.ProductSKU + " - " + p.ProductTitle;
                    ltrRegularPrice.Text = "<p><strong>📌 Giá sỉ</strong>: " + (p.Regular_Price/1000).ToString() + "k</p>";
                    ltrRetailPrice.Text = "<p><strong>📌 Giá lẻ</strong>: " + (p.Retail_Price/1000).ToString() + "k</p>";

                    if (!string.IsNullOrEmpty(p.Materials))
                    {
                        ltrContent.Text += "<p>" + p.Materials + "</p>";
                    }

                    if (!string.IsNullOrEmpty(p.ProductContent))
                    {
                        string content = Regex.Replace(p.ProductContent, @"<img\s[^>]*>(?:\s*?</img>)?", "").ToString();
                        ltrContent.Text += "<p>" + content + "</p>";
                    }

                    if (p.ProductImage != null)
                    {
                        ProductThumbnail.Text = "<p><img src='" + Thumbnail.getURL(p.ProductImage, Thumbnail.Size.Source) + "'><a href='" + Thumbnail.getURL(p.ProductImage, Thumbnail.Size.Source) + "' download class='btn download-btn download-image h45-btn'><i class='fa fa-cloud-download'></i> Tải hình này</a></p>";
                    }

                    var image = ProductImageController.GetByProductID(id);

                    if (image != null)
                    {
                        foreach(var img in image)
                        {
                            if(img.ProductImage != p.ProductImage)
                            {
                                imageGallery.Text += "<p><img src='" + Thumbnail.getURL(img.ProductImage, Thumbnail.Size.Source) + "'><a href='" + Thumbnail.getURL(img.ProductImage, Thumbnail.Size.Source) + "' download class='btn download-btn download-image h45-btn'><i class='fa fa-cloud-download'></i> Tải hình này</a></p>";
                            }
                        }
                    }

                    var v = ProductVariableController.SearchProductID(id, "");
                    string css = "col-xs-6";
                    if (v.Count > 0)
                    {
                        css = "col-xs-4";
                    }

                    StringBuilder htmlButton = new StringBuilder();
                    htmlButton.Append("<div class='" + css +"'>");
                    htmlButton.Append("    <div class='row'>");
                    htmlButton.Append("        <p><a href='javascript:;' class='btn primary-btn copy-btn h45-btn' onclick='copyProduct(`" + p.ID + "`);'><i class='fa fa-files-o' aria-hidden='true'></i> Copy mô tả</a></p>");
                    htmlButton.Append("    </div>");
                    htmlButton.Append("</div>");

                    if (v.Count > 0)
                    {
                        htmlButton.Append("<div class='" + css + "'>");
                        htmlButton.Append("    <div class='row'>");
                        htmlButton.Append("        <p><a href='#variableTable' class='btn download-btn h45-btn'><i class='fa fa-list-ul' aria-hidden='true'></i> Biến thể</a></p>");
                        htmlButton.Append("    </div>");
                        htmlButton.Append("</div>");

                        ltrVariableList.Text = "<h3><i class='fa fa-list-ul' aria-hidden='true'></i> Danh sách biến thể</h3>";
                        pagingall(v);
                    }

                    htmlButton.Append("<div class='" + css + "'>");
                    htmlButton.Append("    <div class='row'>");
                    htmlButton.Append("        <a href='javascript:;' class='btn primary-btn h45-btn' onclick='getAllProductImage(`" + p.ProductSKU + "`);'><i class='fa fa-cloud-download' aria-hidden='true'></i> Tải hình</a>");
                    htmlButton.Append("    </div>");
                    htmlButton.Append("</div>");

                    ltrButton.Text = htmlButton.ToString();
                }
            }
        }
        public class ProductVariable
        {
            public string VariableName { get; set; }
            public string VariableValue { get; set; }
        }
        #region Paging
        public void pagingall(List<tbl_ProductVariable> acs)
        {
            int PageSize = 30;
            StringBuilder html = new StringBuilder();
            html.Append("<div class='row'>");
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
                    html.Append("<div class='col-md-3 product-item'>");

                    html.Append("   <div><img src='" + Thumbnail.getURL(item.Image, Thumbnail.Size.Large) + "'></div>");
                    if (!string.IsNullOrEmpty(item.Image))
                    {
                        html.Append("   <div><a href='" + Thumbnail.getURL(item.Image, Thumbnail.Size.Source) + "' download class='btn download-btn h45-btn'><i class='fa fa-cloud-download'></i> Tải hình này</a></div>");
                    }

                    var value = ProductVariableValueController.GetByProductVariableID(item.ID);
                    if (value != null)
                    {
                        foreach (var temp in value)
                        {
                            html.Append("<h3>" + temp.VariableName + ": " + temp.VariableValue + "</h3>");
                        }
                    }

                    html.Append("   <p>Mã: " + item.SKU + "</p>");
                    html.Append("   <p>Giá sỉ: " + string.Format("{0:N0}", item.Regular_Price) + "</p>");
                    html.Append("</div>");
                }
            }
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
    }
}