using IM_PJ.Controllers;
using IM_PJ.Models;
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
using System.Web.Script.Serialization;
using System.Web.Services;
using static IM_PJ.Controllers.ProductController;
using IM_PJ.Utils;
using System.Web.Hosting;
using System.IO;

namespace IM_PJ
{
    public partial class nuoc_hoa : System.Web.UI.Page
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
            string TextSearch = "";
            int Page = 1;
            double Price = 0;

            if (Request.QueryString["textsearch"] != null)
                TextSearch = Request.QueryString["textsearch"].Trim();
            if (Request.QueryString["price"] != null)
                Price = Convert.ToDouble(Request.QueryString["price"]);
            if (Request.QueryString["Page"] != null)
                Page = Request.QueryString["Page"].ToInt();

            // Create order fileter
            var filter = new ProductFilterModel()
            {
                category = 44,
                stockStatus = 1,
                quantity = "greaterthan",
                quantityFrom = 3,
                search = TextSearch,
                price = Price
            };

            // Create pagination
            var page = new PaginationMetadataModel()
            {
                currentPage = Page,
                pageSize = 20
            };
            List<ProductSQL> a = new List<ProductSQL>();
            a = ProductController.GetAllSql(filter, ref page);

            pagingall(a, page);

        }
        [WebMethod]
        #region Paging
        public void pagingall(List<ProductSQL> acs, PaginationMetadataModel page)
        {
            var config = ConfigController.GetByTop1();
            string cssClass = "col-xs-6";

            StringBuilder html = new StringBuilder();
            html.Append("<div class='row'>");

            if (acs.Count > 0)
            {
                PageCount = page.totalPages;
                Int32 Page = page.currentPage;
                var index = 0;

                foreach (var item in acs)
                {
                    html.Append("<div class='col-md-3 item-" + index + " product-item'>");
                    html.Append("<div class='row'>");
                    html.Append("     <div class='col-xs-12'>");
                    html.Append("   <p><a href='/xem-nuoc-hoa?id=" + item.ID + "'><img src='" + Thumbnail.getURL(item.ProductImage, Thumbnail.Size.Large) + "'></a></p>");
                    html.Append("   <h3 class='product-name'><a href='/xem-nuoc-hoa?id=" + item.ID + "'>" + item.ProductSKU + " - " + item.ProductTitle + "</a></h3>");
                    html.Append("   <h3><span class='product-price'>Giá sỉ: " + string.Format("{0:N0}", item.RegularPrice) + "</span> - <span class='product-price price-retail'>Giá lẻ: " + string.Format("{0:N0}", item.RetailPrice) + "</span></h3>");
                    if (!string.IsNullOrEmpty(item.Materials))
                    {
                        html.Append("   <p>" + item.Materials + "</p>");
                    }

                    html.Append("     </div>");
                    html.Append("</div>");


                    html.Append("<div class='row'>");
                    html.Append("     <div class='col-xs-12'>");
                    html.Append("          <div class='" + cssClass + "'>");
                    html.Append("               <div class='row'>");
                    html.Append("                  <a href='javascript:;' class='btn primary-btn copy-btn h45-btn' onclick='copyProductInfo(" + item.ID + ");'><i class='fa fa-files-o' aria-hidden='true'></i> Copy mô tả</a>");
                    html.Append("               </div>");
                    html.Append("          </div>");
                    html.Append("          <div class='" + cssClass + "'>");
                    html.Append("               <div class='row'>");
                    html.Append("                  <a href ='javascript:;' class='btn primary-btn h45-btn' onclick='getAllProductImage(`" + item.ProductSKU + "`);'><i class='fa fa-cloud-download' aria-hidden='true'></i> Tải hình</a>");
                    html.Append("               </div>");
                    html.Append("          </div>");
                    html.Append("     </div>");
                    html.Append("</div>");

                    html.Append("</div>");

                    if((index + 1) % 4 == 0)
                    {
                        html.Append("<div class='clear'></div>");
                    }
                    index++;
                }
            }
            else
            {
                html.Append("<div class='col-md-12'>Không tìm thấy sản phẩm...</div>");
            }
            html.Append("</div>");

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
            string[] strText = new string[2] { "Trang đầu", "Trang cuối"};
            if (PageCount > 1)
                Response.Write(GetHtmlPagingAdvanced(4, CurrentPage, PageCount, "", strText));

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
                output.Append("<li><a title=\"" + strText[0] + "\" href=\"javascript:;\" onclick=\"openURL(" + 1  + ")\"><i class=\"fa fa-angle-left\"></i><i class=\"fa fa-angle-left\"></i></a></li>");
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
                    output.Append("<li><a href=\"javascript:;\" onclick=\"openURL(" + i.ToString() + ")\">" + i.ToString() + "</a> </li>");
                }
            }

            // Last(Trang cuối)
            if (currentPage != pageCount)
            {
                output.Append("<li><a title=\"" + strText[1] + "\" href=\"javascript:;\" onclick=\"openURL(" + pageCount + ")\"><i class=\"fa fa-angle-right\"></i><i class=\"fa fa-angle-right\"></i></a></li>");
            }

            output.Append("</ul>");
            //output.Append("</div>");
            return output.ToString();
        }
        #endregion

        [WebMethod]
        public static string getAllCategoryImage(double productPrice = 0)
        {
            List<string> result = new List<string>();

            string rootPath = HostingEnvironment.ApplicationPhysicalPath;
            string uploadsImagesPath = rootPath + "/uploads/images/";

            using (var dbe = new inventorymanagementEntities())
            {
                var products = dbe.tbl_Product.Where(p => p.CategoryID == 44).OrderByDescending(o => o.ID).ToList();
                if (productPrice > 0)
                {
                    products = products.Where(p => p.Regular_Price == productPrice).ToList();
                }
                else if (productPrice == -1)
                {
                    products = products.Where(p => p.Regular_Price != 30000 && p.Regular_Price != 35000 && p.Regular_Price != 49000 && p.Regular_Price != 135000).ToList();
                }

                if (products != null)
                {
                    List<string> images = new List<string>();

                    foreach (var item in products)
                    {
                        var stock = StockManagerController.getStock(item.ID, 0);
                        double quantity = 0;
                        if (stock.Count > 0)
                        {
                            quantity = stock
                            .Select(s => s.Type == 2 ? (s.QuantityCurrent.Value - s.Quantity.Value) : (s.QuantityCurrent.Value + s.Quantity.Value))
                            .Sum(s => s);
                        }

                        // Chỉ lấy ảnh sản phẩm còn hàng
                        if (quantity >= 3)
                        {
                            // lấy ảnh đại diện
                            string imgAdd = item.ProductImage;
                            if (File.Exists(uploadsImagesPath + imgAdd))
                            {
                                images.Add(imgAdd);
                            }

                            // lấy ảnh sản phẩm từ thư viện
                            var imageProduct = ProductImageController.GetByProductID(item.ID);
                            if (imageProduct != null)
                            {
                                foreach (var img in imageProduct)
                                {
                                    imgAdd = img.ProductImage;
                                    if (File.Exists(uploadsImagesPath + imgAdd))
                                    {
                                        images.Add(imgAdd);
                                    }
                                }
                            }

                            // lấy ảnh sản phẩm từ biến thể
                            var variable = ProductVariableController.GetByParentSKU(item.ProductSKU);
                            if (variable != null)
                            {
                                foreach (var v in variable)
                                {
                                    if (!String.IsNullOrEmpty(v.Image))
                                    {
                                        imgAdd = v.Image;
                                        if (File.Exists(uploadsImagesPath + imgAdd))
                                        {
                                            images.Add(imgAdd);
                                        }
                                    }
                                }
                            }
                        }
                        
                    }

                    images = images.Distinct().ToList();

                    if (images.Count() > 0)
                    {
                        // lấy hình gốc
                        for (int i = 0; i < images.Count; i++)
                        {
                            result.Add(Thumbnail.getURL(images[i], Thumbnail.Size.Source));
                        }
                    }
                }
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(result.ToList());
        }

        [WebMethod]
        public static string copyProductInfo(int id)
        {
            var product = ProductController.GetByID(id);
            StringBuilder html = new StringBuilder();
            if (product != null)
            {
                html.Append("<p>" + (product.Retail_Price / 1000).ToString() + "k - " + product.ProductTitle + "</p>\r\n");
                html.Append("<p></p>\r\n");
                html.Append("<p>📌 " + (product.Retail_Price / 1000).ToString() + "k</p>\r\n");
                html.Append("<p></p>\r\n");
                html.Append("<p>📌 Giá sỉ inbox</p>\r\n");
                html.Append("<p></p>\r\n");

                if (!string.IsNullOrEmpty(product.Materials))
                {
                    html.Append("<p>" + product.Materials + "</p>\r\n");
                    html.Append("<p></p>\r\n");
                }

                if (!string.IsNullOrEmpty(product.ProductContent))
                {
                    string content = Regex.Replace(product.ProductContent, @"<img\s[^>]*>(?:\s*?</img>)?", "").ToString();
                    html.Append("<p>" + content + "</p>\r\n");
                    html.Append("<p></p>\r\n");
                }

                html.Append("<p></p>\r\n");
                html.Append("<p></p>\r\n");
            }

            return html.ToString();
        }
        public class ProductVariable
        {
            public string VariableName { get; set; }
            public string VariableValue { get; set; }
        }
        public class danhmuccon1
        {
            public tbl_Category cate1 { get; set; }
            public string parentName { get; set; }
        }
    }
}