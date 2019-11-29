using IM_PJ.Controllers;
using IM_PJ.Models;
using IM_PJ.Utils;
using MB.Extensions;
using Newtonsoft.Json;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace IM_PJ
{
    public partial class thong_tin_san_pham : System.Web.UI.Page
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
                        else
                        {
                            Response.Redirect("/trang-chu");
                        }

                        hdfUserRole.Value = acc.RoleID.ToString();
                    }
                }
                else
                {
                    Response.Redirect("/dang-nhap");
                }
                LoadSupplier();
                LoadCategory();
                LoadData();
            }
        }

        public void LoadCategory()
        {
            var category = CategoryController.GetAllWithIsHidden(false);
            ddlCategory.Items.Clear();
            ddlCategory.Items.Insert(0, new ListItem("Chọn danh mục sản phẩm", "0"));
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
        public void LoadSupplier()
        {
            var supplier = SupplierController.GetAllWithIsHidden(false);
            ddlSupplier.Items.Clear();
            ddlSupplier.Items.Insert(0, new ListItem("Chọn nhà cung cấp", "0"));
            if (supplier.Count > 0)
            {
                foreach (var p in supplier)
                {
                    ListItem listitem = new ListItem(p.SupplierName, p.ID.ToString());
                    ddlSupplier.Items.Add(listitem);
                }
                ddlSupplier.DataBind();
            }
        }
        public static string getSelectVariable(string Name, string Value)
        {
            var variablename = VariableController.GetByName(Name);
            string html = "";
            if (variablename != null)
            {
                html += "<select name='ddlVariableValue' id='ddlVariableValue' data-name-id='" + variablename.ID + "' data-name-text='" + variablename.VariableName + "' class='form-control' onchange='changeVariable($(this))'>";
                html += "<option data-sku-text='' value=''>Chọn giá trị</option>";
                var variablevalue = VariableValueController.GetByVariableID(variablename.ID);
                foreach (var p in variablevalue)
                {
                    if(p.VariableValue == Value)
                    {
                        html += "<option data-sku-text='" + p.SKUText + "' selected='selected' value='" + p.ID + "'>" + p.VariableValue + "</option>";
                    }
                    else
                    {
                        html += "<option data-sku-text='" + p.SKUText + "' value='" + p.ID + "'>" + p.VariableValue + "</option>";
                    }
                }
                html += "</select>";
            }
            return html;
        }
        public void LoadData()
        {
            int id = Request.QueryString["id"].ToInt(0);
            if (id > 0)
            {
                var p = ProductController.GetByID(id);
                if (p == null)
                {
                    PJUtils.ShowMessageBoxSwAlertError("Không tìm thấy sản phẩm " + id, "e", true, "/tat-ca-san-pham", Page);
                }
                else
                {
                    this.Title = String.Format("{0} - Sửa sản phẩm", p.ProductSKU.ToTitleCase());

                    ViewState["ID"] = id;
                    ViewState["cateID"] = p.CategoryID;
                    ViewState["SKU"] = p.ProductSKU;
                    hdfParentID.Value = p.CategoryID.ToString();
                    hdfsetStyle.Value = p.ProductStyle.ToString();
                    ltrBack.Text = "<a href='/xem-san-pham?id=" + p.ID + "' class='btn primary-btn fw-btn not-fullwidth'>Trở về</a>";
                    txtProductTitle.Text = p.ProductTitle;
                    pContent.Content = p.ProductContent;
                    txtProductSKU.Text = p.ProductSKU;

                    pOld_Price.Text = p.Old_Price.ToString();
                    pRegular_Price.Text = p.Regular_Price.ToString();
                    pCostOfGood.Text = p.CostOfGood.ToString();
                    pRetailPrice.Text = p.Retail_Price.ToString();
                    ddlSupplier.SelectedValue = p.SupplierID.ToString();
                    ddlCategory.SelectedValue = p.CategoryID.ToString();

                    if (!string.IsNullOrEmpty(p.Color))
                    {
                        ddlColor.SelectedValue =  p.Color.Trim();
                    }

                    txtMaterials.Text = p.Materials;
                    pMinimumInventoryLevel.Text = p.MinimumInventoryLevel.ToString();
                    pMaximumInventoryLevel.Text = p.MaximumInventoryLevel.ToString();

                    if(p.ProductImage != null)
                    {
                        ListProductThumbnail.Value = p.ProductImage;
                        ProductThumbnail.ImageUrl = Thumbnail.getURL(p.ProductImage, Thumbnail.Size.Source);
                    }

                    if (p.ProductImageClean != null)
                    {
                        ListProductThumbnailClean.Value = p.ProductImageClean;
                        ProductThumbnailClean.ImageUrl = Thumbnail.getURL(p.ProductImageClean, Thumbnail.Size.Source);
                    }

                    var image = ProductImageController.GetByProductID(id);
                    imageGallery.Text = "<ul class='image-gallery'>";
                    if (image != null)
                    {
                        foreach (var img in image)
                        {
                            imageGallery.Text += "<li><img src='" + Thumbnail.getURL(img.ProductImage, Thumbnail.Size.Source) + "'><a href='javascript:;' data-image-id='" + img.ID + "' onclick='deleteImageGallery($(this))' class='btn-delete'><i class='fa fa-times' aria-hidden='true'></i> Xóa hình</a></li>";
                        }
                    }
                    imageGallery.Text += "</ul>";

                    // Hàng cần Order
                    ddlPreOrder.SelectedValue = p.PreOrder ? "1" : "0";

                    // Init Tags
                    var tags = ProductTagController.get(p.ID, 0);

                    if (tags.Count > 0)
                        hdfTags.Value = JsonConvert.SerializeObject(tags);
                    else
                        hdfTags.Value = String.Empty;

                    // Lấy tất cả biến thể ra
                    List <tbl_ProductVariable> a = new List<tbl_ProductVariable>();
                    a = ProductVariableController.GetProductID(p.ID);

                    if(a.Count > 0)
                    {
                        StringBuilder html = new StringBuilder();
                        int t = 1;
                        foreach (var item in a)
                        {
                            string VariableSKU = item.SKU;
                            double RegularPrice = Convert.ToDouble(item.Regular_Price);
                            double RetailPrice = Convert.ToDouble(item.RetailPrice);
                            double CostOfGood = Convert.ToDouble(item.CostOfGood);
                            int MinimumInventoryLevel = Convert.ToInt32(item.MinimumInventoryLevel);
                            int MaximumInventoryLevel = Convert.ToInt32(item.MaximumInventoryLevel);
                            string VariableImage = Thumbnail.getURL(item.Image, Thumbnail.Size.Source);
                            string deleteVariableImage = "<a href='javascript:;' onclick='deleteImageVariable($(this))' class='btn-delete hide'><i class='fa fa-times' aria-hidden='true'></i> Xóa hình</a>";
                            if (!string.IsNullOrEmpty(item.Image))
                            {
                                deleteVariableImage = "<a href='javascript:;' onclick='deleteImageVariable($(this))' class='btn-delete'><i class='fa fa-times' aria-hidden='true'></i> Xóa hình</a>";
                            }

                            var value = ProductVariableValueController.GetByProductVariableID(item.ID);
                            string selectVariable = "";
                            string variableID = "";
                            string variableName = "";
                            string variableValueID = "";
                            string variableValueName = "";
                            string dataVariableValue = "";
                            string labelVariableValue = "";
                            if (value != null)
                            {
                                foreach (var temp in value)
                                {
                                    selectVariable += "<div class='row margin-bottom-15'><div class='col-md-5'>" + temp.VariableName + "</div><div class='col-md-7'>" + getSelectVariable(temp.VariableName, temp.VariableValue) + "</div></div>";
                                    variableID += VariableController.GetByName(temp.VariableName.ToString()).ID.ToString() + "|";
                                    variableName += temp.VariableName.ToString() + "|";
                                    variableValueID += temp.VariableValueID.ToString() + "|";
                                    variableValueName += temp.VariableValue.ToString() + "|";
                                    dataVariableValue += VariableController.GetByName(temp.VariableName.ToString()).ID.ToString() + ":" + temp.VariableValueID.ToString() + "|";
                                    labelVariableValue += temp.VariableName.ToString() + ": " + temp.VariableValue.ToString() + " - ";
                                }
                            }

                            html.AppendLine(String.Format("<div class='item-var-gen' data-name-id='{0}' data-value-id='{1}' data-name-text='{2}' data-value-text='{3}' data-name-value='{4}'>", variableID, variableValueID, variableName, variableValueName, dataVariableValue));
                            html.AppendLine(String.Format("    <div class='col-md-12 variable-label' onclick='showVariableContent($(this))'>"));
                            html.AppendLine(String.Format("    	<strong>#{0}</strong> - {1} {2}", t, labelVariableValue, VariableSKU));
                            html.AppendLine(String.Format("    </div>"));
                            html.AppendLine(String.Format("    <div class='col-md-12 variable-content'>"));
                            html.AppendLine(String.Format("    	<div class='row'>"));
                            html.AppendLine(String.Format("		    <div class='col-md-2'>"));
                            html.AppendLine(String.Format("		    	<input type='file' class='productVariableImage upload-btn' onchange='imagepreview(this,$(this));' name='{0}'><img class='imgpreview' onclick='openUploadImage($(this))' data-file-name='{1}' src='{1}'>{2}", VariableSKU, VariableImage, deleteVariableImage));
                            html.AppendLine(String.Format("		    	</div>"));
                            html.AppendLine(String.Format("		    <div class='col-md-5'>"));
                            html.AppendLine(String.Format("		    	{0}", selectVariable));
                            html.AppendLine(String.Format("		    	<div class='row margin-bottom-15'>"));
                            html.AppendLine(String.Format("		    	    <div class='col-md-5'>Mã sản phẩm</div>"));
                            html.AppendLine(String.Format("		    	    <div class='col-md-7'><input type='text' disabled class='form-control productvariablesku sku-input' value='{0}'></div>", VariableSKU));
                            html.AppendLine(String.Format("		    	</div>"));
                            html.AppendLine(String.Format("		    </div>"));
                            html.AppendLine(String.Format("		    <div class='col-md-5'>"));
                            html.AppendLine(String.Format("		    	<div class='row margin-bottom-15'>"));
                            html.AppendLine(String.Format("		    	    <div class='col-md-5'>Giá sỉ</div>"));
                            html.AppendLine(String.Format("		    	    <div class='col-md-7'><input class='form-control regularprice' type='text' value='{0}'> </div>", RegularPrice));
                            html.AppendLine(String.Format("		    	</div>"));
                            html.AppendLine(String.Format("		    	<div class='row margin-bottom-15 cost-of-goods'>"));
                            html.AppendLine(String.Format("		    	    <div class='col-md-5'>Giá vốn</div>"));
                            html.AppendLine(String.Format("		    	    <div class='col-md-7'><input class='form-control costofgood cost-price' type='text' value='{0}'></div>", CostOfGood));
                            html.AppendLine(String.Format("		    	</div>"));
                            html.AppendLine(String.Format("		    	<div class='row margin-bottom-15'>"));
                            html.AppendLine(String.Format("		    	    <div class='col-md-5'>Giá bán lẻ</div>"));
                            html.AppendLine(String.Format("		    	    <div class='col-md-7'><input class='form-control retailprice' type='text' value='{0}'></div>", RetailPrice));
                            html.AppendLine(String.Format("		    	</div>"));
                            html.AppendLine(String.Format("			</div>"));
                            html.AppendLine(String.Format("		</div>"));
                            html.AppendLine(String.Format("	</div>"));
                            html.AppendLine(String.Format("</div>"));

                            t++;
                        }

                        ltrVariables.Text = html.ToString();
                    }

                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string username = Request.Cookies["usernameLoginSystem"].Value;
            var acc = AccountController.GetByUsername(username);
            int cateID = ViewState["cateID"].ToString().ToInt(0);
            int ProductID = ViewState["ID"].ToString().ToInt(0);
            if (cateID > 0)
            {
                string ProductTitle = Regex.Replace(txtProductTitle.Text, @"\s*\,\s*|\s*\;\s*", " - ");
                string ProductContent = pContent.Content;
                string ProductSKU = ViewState["SKU"].ToString();
                double ProductStock = 0;
                int StockStatus = 0;
                bool ManageStock = true;
                double Old_Price = String.IsNullOrEmpty(pOld_Price.Text) ? 0 : Convert.ToDouble(pOld_Price.Text);
                double Regular_Price = Convert.ToDouble(pRegular_Price.Text);
                double CostOfGood = Convert.ToDouble(pCostOfGood.Text);
                double Retail_Price = Convert.ToDouble(pRetailPrice.Text);
                int CategoryID = hdfParentID.Value.ToInt();
                string mainColor = ddlColor.SelectedValue.ToString();
                double MinimumInventoryLevel = 0;
                if (pMinimumInventoryLevel.Text != "")
                {
                    MinimumInventoryLevel = Convert.ToDouble(pMinimumInventoryLevel.Text);
                }

                double MaximumInventoryLevel = 0;
                if (pMaximumInventoryLevel.Text != "")
                {
                    MaximumInventoryLevel = Convert.ToDouble(pMaximumInventoryLevel.Text);
                }

                //Phần cap nhat ảnh đại diện sản phẩm
                string path = "/uploads/images/";
                string ProductImage = ListProductThumbnail.Value;
                if (ProductThumbnailImage.UploadedFiles.Count > 0)
                {
                    foreach (UploadedFile f in ProductThumbnailImage.UploadedFiles)
                    {
                        string o = path + ProductID + '-' + Slug.ConvertToSlug(Path.GetFileName(f.FileName));
                        if (File.Exists(Server.MapPath(o)))
                        {
                            o = path + ProductID + '-' + DateTime.UtcNow.ToString("HHmmssffff") + '-' + Slug.ConvertToSlug(Path.GetFileName(f.FileName));
                        }
                        f.SaveAs(Server.MapPath(o));
                        ProductImage = Path.GetFileName(o);

                        // Thumbnail
                        Thumbnail.create(Server.MapPath(o), 85, 113);
                        Thumbnail.create(Server.MapPath(o), 159, 212);
                        Thumbnail.create(Server.MapPath(o), 240, 320);
                        Thumbnail.create(Server.MapPath(o), 350, 467);
                    }
                }

                //Phần thêm ảnh đại diện sản phẩm sạch không đóng dấu
                string ProductImageClean = ListProductThumbnailClean.Value;
                if (ProductThumbnailImageClean.UploadedFiles.Count > 0)
                {
                    foreach (UploadedFile f in ProductThumbnailImageClean.UploadedFiles)
                    {
                        var o = path + ProductID + "-clean-" + Slug.ConvertToSlug(Path.GetFileName(f.FileName));
                        if (File.Exists(Server.MapPath(o)))
                        {
                            o = path + ProductID + "-clean-" + DateTime.UtcNow.ToString("HHmmssffff") + '-' + Slug.ConvertToSlug(Path.GetFileName(f.FileName));
                        }
                        f.SaveAs(Server.MapPath(o));
                        ProductImageClean = Path.GetFileName(o);

                        // Thumbnail
                        Thumbnail.create(Server.MapPath(o), 85, 113);
                        Thumbnail.create(Server.MapPath(o), 159, 212);
                        Thumbnail.create(Server.MapPath(o), 240, 320);
                        Thumbnail.create(Server.MapPath(o), 350, 467);
                    }
                }

                //Delete Image Gallery
                string deleteImageGallery = hdfDeleteImageGallery.Value;
                if (deleteImageGallery != "")
                {
                    string[] deletelist = deleteImageGallery.Split(',');

                    for (int i = 0; i < deletelist.Length - 1; i++)
                    {
                        var img = ProductImageController.GetByID(Convert.ToInt32(deletelist[i]));
                        if (img != null)
                        {
                            string delete = ProductImageController.Delete(img.ID);
                        }
                    }
                }

                // Hàng Order
                var preOrder = ddlPreOrder.SelectedValue == "1" ? true : false;

                // Update product
                string kq = ProductController.Update(new tbl_Product() {
                    ID = ProductID,
                    CategoryID = CategoryID,
                    ProductOldID = 0,
                    ProductTitle = ProductTitle,
                    ProductContent = ProductContent,
                    ProductSKU = ProductSKU,
                    ProductStock = ProductStock,
                    StockStatus = StockStatus,
                    ManageStock = ManageStock,
                    Regular_Price = Regular_Price,
                    CostOfGood = CostOfGood,
                    Retail_Price = Retail_Price,
                    ProductImage = ProductImage,
                    ProductType = 0,
                    IsHidden = false,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = username,
                    SupplierID = ddlSupplier.SelectedValue.ToInt(0),
                    SupplierName = ddlSupplier.SelectedItem.ToString(),
                    Materials = txtMaterials.Text,
                    MinimumInventoryLevel = MinimumInventoryLevel,
                    MaximumInventoryLevel = MaximumInventoryLevel,
                    ProductImageClean = ProductImageClean,
                    Color = mainColor,
                    PreOrder = preOrder,
                    Old_Price = Old_Price
                });

                // Upload tags
                if (!String.IsNullOrEmpty(hdfTags.Value))
                {
                    var tagList = JsonConvert.DeserializeObject<List<TagModel>>(hdfTags.Value);

                    if (tagList.Count > 0)
                    {
                        // Get tag new
                        var tagNew = TagController.insert(tagList, acc);

                        var productTag = tagList
                            .GroupJoin(
                                tagNew,
                                t => t.name.ToLower(),
                                n => n.Name.ToLower(),
                                (t, n) => new { t, n }
                            )
                            .SelectMany(
                                x => x.n.DefaultIfEmpty(),
                                (parent, child) => new ProductTag
                                {
                                    TagID = child != null ? child.ID : parent.t.id,
                                    ProductID = ProductID,
                                    ProductVariableID = 0,
                                    SKU = ProductSKU,
                                    CreatedBy = acc.ID,
                                    CreatedDate = DateTime.Now
                                }
                            )
                            .ToList();

                        ProductTagController.update(ProductID, productTag);
                    }
                    else
                    {
                        ProductTagController.delete(ProductID);
                    }
                }

                // Upload image gallery
                string itemGallery = "";
                if (UploadImages.HasFiles)
                {
                    foreach (HttpPostedFile uploadedFile in UploadImages.PostedFiles)
                    {
                        var o = path + ProductID + '-' + Slug.ConvertToSlug(Path.GetFileName(uploadedFile.FileName));
                        if (File.Exists(Server.MapPath(o)))
                        {
                            o = path + ProductID + '-' + DateTime.UtcNow.ToString("HHmmssffff") + '-' + Slug.ConvertToSlug(Path.GetFileName(uploadedFile.FileName));
                        }
                        uploadedFile.SaveAs(Server.MapPath(o));
                        itemGallery = Path.GetFileName(o);

                        // Thumbnail
                        Thumbnail.create(Server.MapPath(o), 85, 113);
                        Thumbnail.create(Server.MapPath(o), 159, 212);
                        Thumbnail.create(Server.MapPath(o), 240, 320);
                        Thumbnail.create(Server.MapPath(o), 350, 467);
                        
                        ProductImageController.Insert(ProductID, itemGallery, false, DateTime.Now, username);
                    }
                }


                if (kq.ToInt(0) > 0)
                {
                    // Update Variable
                    if (hdfsetStyle.Value == "2")
                    {
                        string variable = hdfVariableListInsert.Value;
                        if (!string.IsNullOrEmpty(variable))
                        {
                            string[] items = variable.Split(',');
                            for (int i = 0; i < items.Length - 1; i++)
                            {
                                string item = items[i];
                                string[] itemElement = item.Split(';');

                                string datanameid = itemElement[0];
                                string[] datavalueid = itemElement[1].Split('|');
                                string datanametext = itemElement[2];
                                string datavaluetext = itemElement[3];
                                string productvariablesku = itemElement[4].Trim().ToUpper();
                                string regularprice = itemElement[5];
                                string costofgood = itemElement[6];
                                string retailprice = itemElement[7];
                                string[] datanamevalue = itemElement[8].Split('|');
                                string imageUpload = itemElement[4];
                                int _MaximumInventoryLevel = itemElement[9].ToInt(0);
                                int _MinimumInventoryLevel = itemElement[10].ToInt(0);
                                int stockstatus = itemElement[11].ToInt();
                                string imageSrc = itemElement[13];
                                string kq1 = "";

                                // Check variable
                                var Variable = ProductVariableController.GetBySKU(productvariablesku);
                                if (Variable != null)
                                {
                                    // Update image
                                    string image = Variable.Image;
                                    if (imageSrc == "/App_Themes/Ann/image/placeholder.png")
                                    {
                                        image = "";
                                    }
                                    else
                                    {
                                        if (imageSrc != path + Variable.Image)
                                        {
                                            HttpPostedFile postedFile = Request.Files[imageUpload];
                                            if (postedFile != null && postedFile.ContentLength > 0)
                                            {
                                                // Upload image
                                                var o = path + ProductID + '-' + Slug.ConvertToSlug(Path.GetFileName(postedFile.FileName));

                                                if (File.Exists(Server.MapPath(o)))
                                                {
                                                    o = path + ProductID + '-' + DateTime.UtcNow.ToString("HHmmssffff") + '-' + Slug.ConvertToSlug(Path.GetFileName(postedFile.FileName));
                                                }
                                                postedFile.SaveAs(Server.MapPath(o));
                                                image = Path.GetFileName(o);

                                                // Thumbnail
                                                Thumbnail.create(Server.MapPath(o), 85, 113);
                                                Thumbnail.create(Server.MapPath(o), 159, 212);
                                                Thumbnail.create(Server.MapPath(o), 240, 320);
                                                Thumbnail.create(Server.MapPath(o), 350, 467);
                                            }
                                            else
                                            {
                                                image = "";
                                            }
                                        }
                                    }

                                    // Update variable

                                    kq1 = ProductVariableController.Update(Variable.ID, ProductID, Variable.ParentSKU, productvariablesku, Convert.ToDouble(Variable.Stock), Convert.ToInt32(Variable.StockStatus), Convert.ToDouble(regularprice), Convert.ToDouble(costofgood), Convert.ToDouble(retailprice), image, true, false, DateTime.Now, username, Convert.ToInt32(Variable.SupplierID), Variable.SupplierName, _MinimumInventoryLevel, _MaximumInventoryLevel);

                                    // Delete all productVariableValue

                                    bool deleteVariableValue = ProductVariableValueController.DeleteByProductVariableID(Variable.ID);
                                }
                                else
                                {
                                    string image = "";

                                    HttpPostedFile postedFile = Request.Files[imageUpload];
                                    if (postedFile != null && postedFile.ContentLength > 0)
                                    {
                                        // Upload image
                                        var o = path + ProductID + '-' + Slug.ConvertToSlug(Path.GetFileName(postedFile.FileName));
                                        if (File.Exists(Server.MapPath(o)))
                                        {
                                            o = path + ProductID + '-' + DateTime.UtcNow.ToString("HHmmssffff") + '-' + Slug.ConvertToSlug(Path.GetFileName(postedFile.FileName));
                                        }
                                        postedFile.SaveAs(Server.MapPath(o));
                                        image = Path.GetFileName(o);

                                        // Thumbnail
                                        Thumbnail.create(Server.MapPath(o), 85, 113);
                                        Thumbnail.create(Server.MapPath(o), 159, 212);
                                        Thumbnail.create(Server.MapPath(o), 240, 320);
                                        Thumbnail.create(Server.MapPath(o), 350, 467);
                                    }

                                    // Insert new variable

                                    kq1 = ProductVariableController.Insert(ProductID, ProductSKU, productvariablesku, 0, stockstatus, Convert.ToDouble(regularprice),
                                            Convert.ToDouble(costofgood), Convert.ToDouble(retailprice), image, true, false, DateTime.Now, username,
                                            ddlSupplier.SelectedValue.ToInt(0), ddlSupplier.SelectedItem.ToString(), _MinimumInventoryLevel, _MaximumInventoryLevel);
                                }

                                // Update ProductVariableValue

                                if (kq1.ToInt(0) > 0)
                                {
                                    string[] Data = datanametext.Split('|');
                                    string[] DataValue = datavaluetext.Split('|');
                                    for (int k = 0; k < Data.Length - 1; k++)
                                    {
                                        int variablevalueID = datavalueid[k].ToInt();
                                        string variableName = Data[k];
                                        string variableValueName = DataValue[k];
                                        ProductVariableValueController.Insert(kq1.ToInt(), productvariablesku, variablevalueID, variableName, variableValueName, false, DateTime.Now, username);
                                    }
                                }

                            }
                        }
                    }

                    PJUtils.ShowMessageBoxSwAlert("Cập nhật sản phẩm thành công", "s", true, Page);
                }
            }
        }
    }
}