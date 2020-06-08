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

namespace IM_PJ
{
    public partial class xem_bv : System.Web.UI.Page
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
            int id = Request.QueryString["id"].ToInt(0);
            if (id > 0)
            {
                var p = PostController.GetByID(id);
                if (p == null)
                {
                    PJUtils.ShowMessageBoxSwAlertError("Không tìm thấy sản phẩm " + id, "e", true, "/sp", Page);
                }
                else
                {
                    ViewState["ID"] = id;
                    ViewState["cateID"] = p.CategoryID;

                    ltrProductName.Text = p.Title;
                    ltrContent.Text = p.Content;

                    if (p.Image != null)
                    {
                        PostThumbnail.Text = "<p><img src=\"" + p.Image + "\" /><a href='" + p.Image + "' download class='btn download-btn download-image h45-btn'><i class='fa fa-cloud-download'></i> Tải hình này</a></p>";
                    }

                    var image = PostImageController.GetByPostID(id);

                    if (image != null)
                    {
                        foreach(var img in image)
                        {
                            if(img.Image != p.Image)
                            {
                                imageGallery.Text += "<p><img src=\"" + img.Image + "\" /><a href='" + img.Image + "' download class='btn download-btn download-image h45-btn'><i class='fa fa-cloud-download'></i> Tải hình này</a></p>";
                            }
                        }
                    }

                    ltrCopyProductInfoButton.Text = "<p><a href=\"javascript:;\" class=\"btn primary-btn copy-btn h45-btn\" onclick=\"copyPost(" + p.ID + ")\"><i class=\"fa fa-files-o\" aria-hidden=\"true\"></i> Copy</a></p>";
                    ltrDownloadProductImageButton.Text = "<a href =\"javascript:;\" class=\"btn primary-btn h45-btn\" onclick=\"getAllPostImage(" + p.ID + ");\"><i class=\"fa fa-cloud-download\" aria-hidden=\"true\"></i> Tải hình</a>";
                }
            }
        }
    }
}