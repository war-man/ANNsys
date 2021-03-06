﻿using IM_PJ.Controllers;
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

namespace IM_PJ
{
    public partial class san_pham_theo_ke : System.Web.UI.Page
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
            string TextSearch = "";
            string CreatedDate = "";
            int CategoryID = 0;
            int StockStatus = 0;
            string strColor = String.Empty;
            string strSize = String.Empty;
            int Page = 1;
            // Filter: floor
            var floor = 0;
            // Filter: row
            var row = 0;
            // Filter: shelf
            var shelf = 0;
            // Filter: floorShelf
            var floorShelf = 0;

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
            // Filter: floor
            if (Request.QueryString["floor"] != null)
                floor = Request.QueryString["floor"].ToInt();
            // Filter: row
            if (Request.QueryString["row"] != null)
                row = Request.QueryString["row"].ToInt();
            // Filter: shelf
            if (Request.QueryString["shelf"] != null)
                shelf = Request.QueryString["shelf"].ToInt();
            // Filter: floorShelf
            if (Request.QueryString["floorShelf"] != null)
                floorShelf = Request.QueryString["floorShelf"].ToInt();

            txtSearchProduct.Text = TextSearch;
            ddlColor.SelectedValue = strColor;
            ddlSize.SelectedValue = strSize;
            ddlCategory.SelectedValue = CategoryID.ToString();
            ddlCreatedDate.SelectedValue = CreatedDate.ToString();
            ddlStockStatus.SelectedValue = StockStatus.ToString();
            // Drop down list floor
            loadFloor(floor);
            // Drop down list row
            loadRow(floor, row);
            // Drop down list shelf
            loadShelf(floor, shelf);
            // Drop down list floorShelf
            loadFloorShelf(floor, floorShelf);

            // Create order fileter
            var filter = new ProductFilterModel()
            {
                category = CategoryID,
                search = UnSign.convert(TextSearch),
                color = strColor,
                size = strSize,
                stockStatus = StockStatus,
                productDate = CreatedDate,
                floor = floor,
                row = row,
                shelf = shelf,
                floorShelf = floorShelf
            };
            // Create pagination
            var page = new PaginationMetadataModel()
            {
                currentPage = Page,
                pageSize = 20
            };
            List<ProductShelf> a = new List<ProductShelf>();
            a = ProductController.GetProductShelf(filter, ref page);

            pagingall(a, page);

        }
        [WebMethod]
        #region Paging
        public void pagingall(List<ProductShelf> acs, PaginationMetadataModel page)
        {
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
                    html.Append("    <div class='row'>");
                    html.Append("         <div class='col-xs-12'>");
                    html.Append("       <p><a href='/xem-sp?id=" + item.ProductID + "'><img src='" + Thumbnail.getURL(item.Image, Thumbnail.Size.Large) + "'></a></p>");
                    html.Append("       <h3 class='product-name'><a href='/xem-sp?id=" + item.ProductID + "'>" + item.SKU + " - " + item.Title + "</a></h3>");
                    html.Append("       <h3 class='product-price'>📌 " + string.Format("{0:N0}", item.RegularPrice) + "</h3>");

                    if (!string.IsNullOrEmpty(item.Materials))
                    {
                        html.Append("       <p>🔖 Chất liệu: " + item.Materials + "</p>");
                    }

                    if (!string.IsNullOrEmpty(item.Content))
                    {
                        string content = Regex.Replace(item.Content, @"<img\s[^>]*>(?:\s*?</img>)?", "").ToString();
                        if (!String.IsNullOrEmpty(content))
                            html.Append("       <p>🔖 " + content.Substring(0, content.Length > 100 ? 100 : content.Length) + "</p>");
                    }

                    if (item.Quantity > 0)
                    {
                        html.Append("       <p>🔖 <span class='bg-green'>Còn hàng</span> (" + string.Format("{0:N0}", item.Quantity) + " cái)</p>");
                    }
                    else
                    {
                        html.Append("       <p>🔖 <span class='bg-red'>Hết hàng</span></p>");
                    }
                    html.Append("       <p>🔖 " + string.Format("{0:dd/MM/yyyy}", item.CreatedDate) + "</p>");
                    if (item.Floor > 0)
                        html.Append("       <p>⭐ Lầu: " + item.FloorName  + "</p>");
                    if (item.Row > 0)
                        html.Append("       <p>⭐ Dãy: " + item.RowName + "</p>");
                    if (item.Shelf > 0)
                        html.Append("       <p>⭐ Kệ: " + item.ShelfName + "</p>");
                    if (item.FloorShelf > 0)
                        html.Append("       <p>⭐ Lầu: " + item.FloorShelfName + "</p>");
                    html.Append("         </div>");
                    html.Append("    </div>");
                    html.Append("</div>");

                    if ((index + 1) % 4 == 0)
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
            string request = "/san-pham-theo-ke?";

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
            // Filter: floor
            if (!String.IsNullOrEmpty(ddlFloor.SelectedValue) && ddlFloor.SelectedValue != "0")
            {
                request += "&floor=" + ddlFloor.SelectedValue;
            }
            // Filter: row
            if (!String.IsNullOrEmpty(ddlRow.SelectedValue) && ddlRow.SelectedValue != "0")
            {
                request += "&row=" + ddlRow.SelectedValue;
            }
            // Filter: shelf
            if (!String.IsNullOrEmpty(ddlShelf.SelectedValue) && ddlShelf.SelectedValue != "0")
            {
                request += "&shelf=" + ddlShelf.SelectedValue;
            }
            // Filter: floorShelf
            if (!String.IsNullOrEmpty(ddlFloorShelf.SelectedValue) && ddlFloorShelf.SelectedValue != "0")
            {
                request += "&floorShelf=" + ddlFloorShelf.SelectedValue;
            }

            Response.Redirect(request);
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
    }
}