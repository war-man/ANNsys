using IM_PJ.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IM_PJ
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("/dang-nhap");
            if (!IsPostBack)
            {
                //UpdateVariableValue();
                //UpdateProductID();
                //CreateProductImage();
            }
        }
        public void CreateProductImage()
        {
            DateTime currentDate = DateTime.Now;
            var products = ProductController.GetAll("");
            if (products.Count > 0)
            {
                foreach (var p in products)
                {
                    if (!string.IsNullOrEmpty(p.ProductImage))
                    {
                        string images = p.ProductImage;
                        if (images.Contains("|"))
                        {
                            string[] img = images.Split('|');
                            if (img.Length > 0)
                            {
                                for (int i = 0; i < img.Length; i++)
                                {
                                    string itemimg = img[i];
                                    ProductImageController.Insert(p.ID, itemimg, false, currentDate, "admin");
                                }
                            }
                        }
                        else
                        {
                            ProductImageController.Insert(p.ID, images, false, currentDate, "admin");
                        }
                    }
                }
            }
        }
        public void UpdateVariableValue()
        {
            var v = VariableValueController.GetAll("");
            foreach (var item in v)
            {
                string value = LeoUtils.ConvertToUnSign(item.VariableValue.Trim().ToLower());
                VariableValueController.UpdateVariableValueText(item.ID, value);
            }
        }
        public void UpdateProductID()
        {
            DateTime currentDate = DateTime.Now;
            var product = ProductVariableController.GetAll("");
            foreach (var p in product)
            {
                var pr = ProductController.GetBySKU(p.ParentSKU);
                if (pr != null)
                {
                    ProductVariableController.UpdateProductID(p.ID, pr.ID);
                }
                string color = "";
                string size = "";
                if (!string.IsNullOrEmpty(p.color))
                    color = p.color;
                if (!string.IsNullOrEmpty(p.size))
                    size = p.size;

                if (!string.IsNullOrEmpty(color))
                {
                    var vcolor = VariableValueController.GetByVariableID(1);
                    foreach (var c in vcolor)
                    {
                        if (color == c.VariableValueText)
                        {
                            ProductVariableValueController.Insert(p.ID, p.SKU, c.ID, c.VariableName, c.VariableValue, false, currentDate, "admin");
                        }
                    }
                }
                if (!string.IsNullOrEmpty(size))
                {
                    var vsize = VariableValueController.GetByVariableID(2);
                    foreach (var s in vsize)
                    {
                        if (size == s.VariableValueText)
                        {
                            ProductVariableValueController.Insert(p.ID, p.SKU, s.ID, s.VariableName, s.VariableValue, false, currentDate, "admin");
                        }
                    }
                }
            }
        }
    }
}