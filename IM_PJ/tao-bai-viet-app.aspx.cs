using IM_PJ.Controllers;
using IM_PJ.Models;
using MB.Extensions;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace IM_PJ
{
    public partial class tao_bai_viet_app : System.Web.UI.Page
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
                        if (acc.RoleID == 0 || acc.Username == "nhom_zalo502")
                        {
                            LoadCategory();
                        }
                        else
                        {
                            Response.Redirect("/trang-chu");
                        }
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
            var category = PostPublicCategoryController.GetAll();
            ddlCategory.Items.Clear();
            ddlCategory.Items.Insert(0, new ListItem("Chọn danh mục", "0"));
            if (category.Count > 0)
            {
                addItemCategory(0, "");
                ddlCategory.DataBind();
            }
        }

        public void addItemCategory(int id, string h = "")
        {
            var categories = PostPublicCategoryController.GetByParentID("", id);
            
            if (categories.Count > 0)
            {
                foreach (var c in categories)
                {
                    ListItem listitem = new ListItem(h + c.Name, c.ID.ToString());
                    ddlCategory.Items.Add(listitem);

                    addItemCategory(c.ID, h + "---");
                }
            }
        }
        
        [WebMethod]
        public static string getParent(int parent)
        {
            List<GetOutCategory> gc = new List<GetOutCategory>();
            if (parent != 0)
            {
                var parentlist = PostPublicCategoryController.API_GetByParentID(parent);
                if (parentlist != null)
                {

                    for (int i = 0; i < parentlist.Count; i++)
                    {
                        GetOutCategory go = new GetOutCategory();
                        go.ID = parentlist[i].ID;
                        go.CategoryName = parentlist[i].Name;
                        gc.Add(go);
                    }
                }
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(gc);
        }

        public class GetOutCategory
        {
            public int ID { get; set; }
            public string CategoryName { get; set; }
            public string CategoryLevel { get; set; }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string username = Request.Cookies["usernameLoginSystem"].Value;
            var acc = AccountController.GetByUsername(username);
            DateTime currentDate = DateTime.Now;
            int cateID = hdfParentID.Value.ToInt();
            if (cateID > 0)
            {
                int CategoryID = ddlCategory.Value.ToInt();
                var category = PostPublicCategoryController.GetByID(CategoryID);
                string CategorySlug = category.Slug;
                string Title = txtTitle.Text.Trim();
                string Slugs = Slug.ConvertToSlug(txtSlug.Text.Trim());
                string Link = txtLink.Text.Trim();
                string Content = pContent.Content.ToString();
                string Summary = HttpUtility.HtmlDecode(pSummary.Content.ToString());
                string Action = ddlAction.SelectedValue.ToString();
                string ActionValue = "";
                if (Action == "view_more")
                {
                    ActionValue = Slugs;
                }
                else if (Action == "show_web")
                {
                    ActionValue = Link;
                }
                bool AtHome = ddlAtHome.SelectedValue.ToBool();
                bool IsPolicy = ddlIsPolicy.SelectedValue.ToBool();

                var newPostPublic = new PostPublic()
                {
                    CategoryID = CategoryID,
                    CategorySlug = CategorySlug,
                    Title = Title,
                    Thumbnail = "",
                    Summary = Summary,
                    Content = Content,
                    Action = Action,
                    ActionValue = ActionValue,
                    AtHome = AtHome,
                    IsPolicy = IsPolicy,
                    CreatedDate = currentDate,
                    CreatedBy = acc.Username,
                    ModifiedDate = currentDate,
                    ModifiedBy = acc.Username
                };

                var post = PostPublicController.Insert(newPostPublic);

                //Phần thêm ảnh đại diện
                string path = "/uploads/images/posts/";
                string Image = "";
                if (PostPublicThumbnailImage.UploadedFiles.Count > 0)
                {
                    foreach (UploadedFile f in PostPublicThumbnailImage.UploadedFiles)
                    {
                        var o = path + "post-app-" + post.ID.ToString() + "-" + Slug.ConvertToSlug(Path.GetFileName(f.FileName), isFile: true);
                        try
                        {
                            f.SaveAs(Server.MapPath(o));
                            Image = o;
                        }
                        catch { }
                    }
                }
                string updateImage = PostPublicController.UpdateImage(post.ID, Image);

                //Phần thêm thư viện ảnh
                string IMG = "";
                if (ImageGallery.UploadedFiles.Count > 0)
                {
                    foreach (UploadedFile f in ImageGallery.UploadedFiles)
                    {
                        var o = path + "post-app-" + post.ID.ToString() + "-" + Slug.ConvertToSlug(Path.GetFileName(f.FileName), isFile: true);
                        try
                        {
                            f.SaveAs(Server.MapPath(o));
                            IMG = o;
                            PostPublicImageController.Insert(post.ID, IMG, username, currentDate);
                        }
                        catch { }
                    }
                }

                // tạo phiên bản cho wordpress
                if (post != null)
                {
                    var webWordpress = WebWordpressController.GetAll();
                    foreach (var item in webWordpress)
                    {
                        var newPostWordpress = new PostWordpress()
                        {
                            PostPublicID = post.ID,
                            WebWordpress = item.Web,
                            PostWordpressID = 0,
                            CategoryID = post.CategoryID,
                            Title = post.Title,
                            Summary = post.Summary,
                            Content = post.Content,
                            Thumbnail = post.Thumbnail,
                            CreatedDate = post.CreatedDate,
                            CreatedBy = acc.Username,
                            ModifiedDate = post.ModifiedDate,
                            ModifiedBy = acc.Username
                        };

                        var postWordpress = PostWordpressController.Insert(newPostWordpress);
                    }
                }

                if (post != null)
                {
                    PJUtils.ShowMessageBoxSwAlertCallFunction("Tạo bài viết thành công", "s", true, "redirectTo(" + post.ID.ToString() + ")", Page);
                }
            }
        }
    }
}