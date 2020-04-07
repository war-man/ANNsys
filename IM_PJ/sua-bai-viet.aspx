<%@ Page Title="Sửa bài viết nội bộ" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="sua-bai-viet.aspx.cs" Inherits="IM_PJ.sua_bai_viet" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .width {
            width: calc(100% - 100px);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="row">
                <div class="col-md-9">
                    <div class="panel panelborderheading">
                        <div class="panel-heading clear">
                            <h3 class="page-title left not-margin-bot">Sửa bài viết nội bộ</h3>
                        </div>
                        <div class="panel-body">
                            <div class="form-row">
                                <asp:Label ID="lblError" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Tiêu đề
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" placeholder="Tiêu đề bài viết" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Slug
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtSlug" runat="server" CssClass="form-control" placeholder="Slug" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Danh mục
                                </div>
                                <div class="row-right parent">
                                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control slparent" date-name="parentID" data-level="1" onchange="selectCategory($(this))">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Nổi bật
                                </div>
                                <div class="row-right">
                                    <asp:DropDownList ID="ddlFeatured" AppendDataBoundItems="true" runat="server" class="form-control">
                                        <asp:ListItem Text="Không" Value="0" />
                                        <asp:ListItem Text="Có" Value="1" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Ảnh đại diện
                                </div>
                                <div class="row-right">
                                    <telerik:RadAsyncUpload Skin="Metro" runat="server" ID="PostThumbnailImage" ChunkSize="0" Localization-Select="Chọn ảnh" AllowedFileExtensions=".jpeg,.jpg,.png" MultipleFileSelection="Disabled" OnClientFileSelected="OnClientFileSelected1" MaxFileInputsCount="1"  OnClientFileUploadRemoved="OnClientFileUploadRemoved">
                                    </telerik:RadAsyncUpload>
                                    <asp:Image runat="server" ID="PostThumbnail" Width="200" />
                                    <asp:HiddenField runat="server" ID="ListPostThumbnail" ClientIDMode="Static" />
                                    <div class="hidPostThumbnail"></div>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Nội dung
                                </div>
                                <div class="row-right">
                                    <telerik:RadEditor runat="server" ID="pContent" Width="100%" Height="700px" ToolsFile="~/FilesResources/ToolContent.xml" Skin="Metro" DialogHandlerUrl="~/Telerik.Web.UI.DialogHandler.axd" AutoResizeHeight="False" ContentFilters="MakeUrlsAbsolute">
                                        <ImageManager ViewPaths="~/uploads/images/posts" UploadPaths="~/uploads/images/posts" DeletePaths="~/uploads/images/posts" />
                                    </telerik:RadEditor>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Thư viện ảnh
                                </div>
                                <div class="row-right">
                                    <asp:FileUpload runat="server" ID="UploadImages" name="uploadImageGallery" onchange='showImageGallery(this,$(this));' AllowMultiple="true" />  
                                    <asp:Literal ID="imageGallery" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div class="form-row">
                                <a href="javascript:;" class="btn primary-btn fw-btn not-fullwidth" onclick="updatePost()"><i class="fa fa-pencil-square-o" aria-hidden="true"></i> Cập nhật</a>
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn primary-btn fw-btn not-fullwidth" Text="Cập nhật" OnClick="btnSubmit_Click" Style="display: none" />
                                <asp:Literal ID="ltrBack" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="panel panelborderheading">
                        <div class="panel-heading clear">
                            <h3 class="page-title left not-margin-bot">Thông tin</h3>
                        </div>
                        <div class="panel-body">
                            <div class="form-row">
                                <asp:Literal ID="ltrPostInfo" runat="server"></asp:Literal>
                                <a href="javascript:;" class="btn primary-btn fw-btn not-fullwidth" onclick="updatePost()"><i class="fa fa-pencil-square-o" aria-hidden="true"></i> Cập nhật</a>
                                <asp:Literal ID="ltrBack2" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hdfUploadGallery" runat="server" />
        <asp:HiddenField ID="hdfDeleteImageGallery" runat="server" />
        <asp:HiddenField ID="hdfsetStyle" runat="server" />
        <asp:HiddenField ID="hdfParentID" runat="server" />
    </main>

    <telerik:RadCodeBlock runat="server">
        <script>

            function redirectTo(ID) {
                window.location.href = "/xem-bai-viet?id=" + ID;
            }

            var storedFiles = [];

            function isBlank(str) {
                return (!str || /^\s*$/.test(str));
            }

            function ChangeToSlug() {
                var title, slug;

                //Lấy text từ thẻ input title 
                title = $("#<%=txtTitle.ClientID%>").val();

                //Đổi chữ hoa thành chữ thường
                slug = title.toLowerCase();

                //Đổi ký tự có dấu thành không dấu
                slug = slug.replace(/á|à|ả|ạ|ã|ă|ắ|ằ|ẳ|ẵ|ặ|â|ấ|ầ|ẩ|ẫ|ậ/gi, 'a');
                slug = slug.replace(/é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ/gi, 'e');
                slug = slug.replace(/i|í|ì|ỉ|ĩ|ị/gi, 'i');
                slug = slug.replace(/ó|ò|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ/gi, 'o');
                slug = slug.replace(/ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự/gi, 'u');
                slug = slug.replace(/ý|ỳ|ỷ|ỹ|ỵ/gi, 'y');
                slug = slug.replace(/đ/gi, 'd');
                //Xóa các ký tự đặt biệt
                slug = slug.replace(/\`|\~|\!|\@|\#|\||\$|\%|\^|\&|\*|\(|\)|\+|\=|\,|\.|\/|\?|\>|\<|\'|\"|\:|\;|_/gi, '');
                //Đổi khoảng trắng thành ký tự gạch ngang
                slug = slug.replace(/ /gi, "-");
                //Đổi nhiều ký tự gạch ngang liên tiếp thành 1 ký tự gạch ngang
                //Phòng trường hợp người nhập vào quá nhiều ký tự trắng
                slug = slug.replace(/\-\-\-\-\-/gi, '-');
                slug = slug.replace(/\-\-\-\-/gi, '-');
                slug = slug.replace(/\-\-\-/gi, '-');
                slug = slug.replace(/\-\-/gi, '-');
                //Xóa các ký tự gạch ngang ở đầu và cuối
                slug = '@' + slug + '@';
                slug = slug.replace(/\@\-|\-\@|\@/gi, '');
                //In slug ra textbox có id “slug”
                $("#<%=txtSlug.ClientID%>").val(slug);
            }

            function selectCategory(obj) {
                var parentID = obj.val();
                $("#<%=hdfParentID.ClientID%>").val(parentID);
                var lv = parseFloat(obj.attr('data-level'));
                var level = lv + 1;
                $(".slparent").each(function () {
                    var lev = $(this).attr('data-level');
                    if (lv < lev) {
                        $(this).remove();
                    }
                });

                $.ajax({
                    type: "POST",
                    url: "/tao-bai-viet.aspx/getParent",
                    data: "{parent:'" + parentID + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var data = JSON.parse(msg.d);
                        var html = "";
                        //var sl = "";
                        if (data.length > 0) {
                            html += "<select class='form-control slparent' style='margin-top:15px;' data-level='" + level + "' onchange='chooseParent($(this))'>";
                            html += "<option  value='0'>Chọn danh mục</option>";
                            for (var i = 0; i < data.length; i++) {
                                html += "<option value='" + data[i].ID + "'>" + data[i].CategoryName + "</option>";
                            }
                            html += "</select>";
                        }
                        $(".parent").append(html);
                    }
                });
            }

            function showImageGallery(input, obj) {
                if (input.files) {
                    base64 = "";
                    fileSize = 0;
                    var allSizes = "";
                    for (i = 0; i < input.files.length ; i++) {
                        
                        if (!input.files[i].type.match("image.*")) {
                            return;
                        }

                        storedFiles.push(input.files[i]);
                        fileSize += input.files[i].size; // total files size  
                        allSizes = allSizes + input.files[i].size + ",";

                        var reader = new FileReader();
                        reader.onload = function (e) {
                            $(".image-gallery").append("<li><img src='" + e.target.result + "' /><a href='javascript:;' data-image-id='' onclick='deleteImageGallery($(this))' class='btn-delete'><i class='fa fa-times' aria-hidden='true'></i> Xóa hình</a></li>")
                            base64 += e.target.result + "|";
                        }
                        reader.readAsDataURL(input.files[i]);
                    }
                    allSizes = allSizes.substring(0, allSizes.length - 1);

                    $("#<%=hdfUploadGallery.ClientID%>").val(allSizes);
                }
            }

            function updatePost() {
                var category = $("#<%=ddlCategory.ClientID%>").val();
                var title = $("#<%=txtTitle.ClientID%>").val();
                var slug = $("#<%=txtSlug.ClientID%>").val();

                // tạo slug cho trường hợp chưa nhập
                if (slug == "") {
                    ChangeToSlug();
                }

                if (category == "0") {
                    $("#<%=ddlCategory.ClientID%>").focus();
                    swal("Thông báo", "Chưa chọn danh mục bài viết", "error");
                }
                else if (title == "") {
                    $("#<%=txtTitle.ClientID%>").focus();
                    swal("Thông báo", "Chưa nhập tiêu đề bài viết", "error");
                }
                else {
                    $("#<%=btnSubmit.ClientID%>").click();
                    HoldOn.open();
                }
            }

            function deleteImageGallery(obj) {
                swal({
                    title: "Xác nhận",
                    text: "Cưng có chắc xóa hình này?",
                    type: "warning",
                    showCancelButton: true,
                    closeOnConfirm: true,
                    cancelButtonText: "Đợi em xem tí!",
                    confirmButtonText: "Chắc chắn sếp ơi..",
                }, function (isConfirm) {
                    if (isConfirm) {
                        if (obj.attr("data-image-id") != "") {
                            var deletelist = $("#<%=hdfDeleteImageGallery.ClientID%>").val();
                            $("#<%=hdfDeleteImageGallery.ClientID%>").val(deletelist + obj.attr("data-image-id") + ",");
                        }
                        obj.parent().addClass("hide");
                    }
                });
            }

            function OnClientFileSelected1(sender, args) {
                if ($telerik.isIE) return;
                else {
                    truncateName(args);
                    var file = args.get_fileInputField().files.item(args.get_rowIndex());
                    showThumbnail(file, args);
                    $("#<%= PostThumbnail.ClientID %>").hide();
                }
            }

            function OnClientFileUploadRemoved(sender, args) {
                    $("#<%= PostThumbnail.ClientID %>").show();
            }

        </script>
    </telerik:RadCodeBlock>
</asp:Content>
