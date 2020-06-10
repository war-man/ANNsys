using IM_PJ.Controllers;
using IM_PJ.Models;
using IM_PJ.Utils;
using MB.Extensions;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace IM_PJ
{
    public partial class chinh_sua_kho_2 : System.Web.UI.Page
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
                        if (acc.RoleID == 0)
                        {

                        }
                        else if (acc.RoleID == 1)
                        {

                        }
                        else if (acc.RoleID == 2)
                        {

                        }
                        else
                        {
                            Response.Redirect("/trang-chu");
                        }
                        LoadData();
                    }
                }
                else
                {
                    Response.Redirect("/dang-nhap");
                }
                
            }
        }
        public void LoadData()
        {
            var supplier = SupplierController.GetAllWithIsHidden(false);
            StringBuilder html = new StringBuilder();

            html.Append("<select id='supplierList' class='form-control' style='width: 20%; float: left; margin-right: 10px'>");
            html.Append("<option value='0'>Tất cả nhà cung cấp</option>");
            if (supplier.Count > 0)
            {
                foreach (var s in supplier)
                {
                    html.Append("<option value='" + s.ID + "'>" + s.SupplierName + "</option>");
                }
            }
            html.Append("</select>");
            ltrSupplier.Text = html.ToString();
        }

        private static ProductGetOut _getProduct(tbl_Product product) {
            var quantity = StockManagerController.getQuantityBySKU(product.ProductSKU);
            var result = new ProductGetOut(){
                ID = product.ID,
                ProductName = product.ProductTitle,
                ProductVariable = String.Empty,
                ProductVariableName = String.Empty,
                ProductVariableValue = String.Empty,
                ProductStyle = 1,
                ProductImage = Thumbnail.getURL(product.ProductImage, Thumbnail.Size.Normal),
                ProductImageOrigin = Thumbnail.getURL(product.ProductImage, Thumbnail.Size.Source),
                SKU = product.ProductSKU.Trim().ToUpper(),
                SupplierID = product.SupplierID.HasValue ? product.SupplierID.Value : 0,
                SupplierName = String.IsNullOrEmpty(product.SupplierName) ? String.Empty : product.SupplierName,
                WarehouseQuantity = string.Format("{0:N0}", quantity),
                ParentID = product.ID,
                ParentSKU = product.ProductSKU
            };

            return result;
        }

        private static List<ProductGetOut> _getProductVariation(List<tbl_ProductVariable> variations, tbl_Product product = null) {
            var result = new List<ProductGetOut>();

            if (variations == null || variations.Count() == 0)
                return result;
            
            if (product == null)
                product = ProductController.GetBySKU(variations.FirstOrDefault().ParentSKU);

            foreach (var variation in variations)
            {
                var attributes = ProductVariableValueController.GetByProductVariableSKU(variation.SKU);

                if (attributes.Count == 0)
                    continue;

                var variable = String.Empty;
                var variablename = String.Empty;
                var variablevalue = String.Empty;

                foreach (var attribute in attributes)
                {
                    variable += attribute.VariableName.Trim() + ": " + attribute.VariableValue.Trim() + "|";
                    variablename += attribute.VariableName.Trim() + "|";
                    variablevalue += attribute.VariableValue.Trim() + "|";
                }

                var quantity = StockManagerController.getQuantityBySKU(product.ProductSKU); 
                var item = new ProductGetOut(){
                    ID = variation.ID,
                    ProductName = product.ProductTitle,
                    ProductVariable = variable,
                    ProductVariableName = variablename,
                    ProductVariableValue = variablevalue,
                    ProductStyle = 2,
                    ProductImage = Thumbnail.getURL(variation.Image, Thumbnail.Size.Normal),
                    ProductImageOrigin = Thumbnail.getURL(variation.Image, Thumbnail.Size.Source),
                    SKU = variation.SKU.Trim().ToUpper(),
                    SupplierID = product.SupplierID.HasValue ? product.SupplierID.Value : 0,
                    SupplierName = String.IsNullOrEmpty(product.SupplierName) ? String.Empty : product.SupplierName,
                    WarehouseQuantity = string.Format("{0:N0}", quantity),
                    ParentID = product.ID,
                    ParentSKU = product.ProductSKU
                };
                result.Add(item);
            }

            return result;
        }

        [WebMethod]
        public static string getProduct(string textsearch)
        {
            List<ProductGetOut> result = new List<ProductGetOut>();
            string username = HttpContext.Current.Request.Cookies["usernameLoginSystem"].Value;
            var acc = AccountController.GetByUsername(username);
            
            if (acc != null)
            {
                int AgentID = Convert.ToInt32(acc.AgentID);
                var product = ProductController.GetBySKU(textsearch.Trim().ToUpper());

                if (product != null)
                {
                    if (product.ProductStyle == 1) {
                        result.Add(_getProduct(product));
                    }
                    else {
                        var productvariable = ProductVariableController.GetByParentSKU(product.ProductSKU);
                        result.AddRange(_getProductVariation(productvariable, product));
                    }
                }
                else
                {
                    var productvariable = ProductVariableController.GetAllBySKU(textsearch.Trim().ToUpper());
                    result.AddRange(_getProductVariation(productvariable, product));
                }
            }
            
            return JsonConvert.SerializeObject(result);
        }

        public class ProductGetOut
        {
            public int ID { get; set; }
            public string ProductName { get; set; }
            public string ProductVariable { get; set; }
            public string ProductVariableName { get; set; }
            public string ProductVariableValue { get; set; }
            public int ProductStyle { get; set; }
            public string ProductImage { get; set; }
            public string ProductImageOrigin { get; set; }
            public string SKU { get; set; }
            public int SupplierID { get; set; }
            public string SupplierName { get; set; }
            public string WarehouseQuantity { get; set; }
            public int ParentID { get; set; }
            public string ParentSKU { get; set; }
        }

        public class WarehousePostModel {
            public int productStyle { get; set; }
            public int productID { get; set; } 
            public int? productVariableID { get; set; } 
            public string sku { get; set; }
            public string parentSKU { get; set; }
            public int type { get; set; }
            public int quantity { get; set; }
            public int quantityCurrent {get; set;}
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            string username = Request.Cookies["usernameLoginSystem"].Value;
            var acc = AccountController.GetByUsername(username);
            if (acc != null)
            {
                int AgentID = Convert.ToInt32(acc.AgentID);
                string note = String.IsNullOrEmpty(hdfNote.Value) ? 
                    "Chỉnh sửa số lượng kho 2 bằng chức năng chỉnh sửa kho 2" : hdfNote.Value;
                var data = String.IsNullOrEmpty(hdfvalue.Value) ? 
                    new List<WarehousePostModel>() : 
                    JsonConvert.DeserializeObject<List<WarehousePostModel>>(hdfvalue.Value);

                foreach (var item in data)
                {
                    StockManagerController.Insert(new tbl_StockManager
                    {
                        AgentID = AgentID,
                        ProductID = item.productID,
                        ProductVariableID = item.productVariableID,
                        Quantity = item.quantity,
                        QuantityCurrent = item.quantityCurrent,
                        Type = item.type,
                        NoteID = note,
                        OrderID = 0,
                        Status = 1,
                        SKU = item.sku,
                        CreatedDate = currentDate,
                        CreatedBy = username,
                        MoveProID = 0,
                        ParentID = item.productID
                    });
                    StockManagerController.warehousing(new StockManager2() {
                        AgentID = AgentID,
                        ProductID = item.productID,
                        ProductVariableID = item.productVariableID,
                        SKU = item.sku,
                        ParentSKU = item.parentSKU,
                        Type = item.type,
                        Quantity = item.quantity,
                        QuantityCurrent = item.quantityCurrent,
                        Status = 2,
                        Note = note,
                        CreatedDate = currentDate,
                        CreatedBy = username,
                        ModifiedDate = currentDate,
                        ModifiedBy = username,
                    });

                    if (item.productStyle == 1)
                        ProductController.UpdateStockStatus(item.sku, 1, false, currentDate, username);
                    else 
                        ProductVariableController.UpdateStockStatus(item.productVariableID.Value, 1, false, currentDate, username);
                }
                
                PJUtils.ShowMessageBoxSwAlert("Chỉnh sửa số lượng kho 2 thành công!", "s", true, Page);
            }
        }
    }
}