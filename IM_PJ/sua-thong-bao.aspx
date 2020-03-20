<%@ Page Title="Sửa thông báo" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="sua-thong-bao.aspx.cs" Inherits="IM_PJ.sua_thong_bao" %>

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
                            <h3 class="page-title left not-margin-bot">Sửa thông báo</h3>
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
                                    Kiểu bài viết
                                </div>
                                <div class="row-right">
                                    <asp:DropDownList ID="ddlAction" AppendDataBoundItems="true" runat="server" class="form-control" onchange="changeAction()">
                                        <asp:ListItem Text="Chọn kiểu" Value="" />
                                        <asp:ListItem Text="Bài nội bộ" Value="view_more" />
                                        <asp:ListItem Text="Link ngoài" Value="show_web" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="input-link form-row hide">
                                <div class="row-left">
                                    Link ngoài
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtLink" runat="server" CssClass="form-control" placeholder="Nhập link" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="input-slug form-row hide">
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
                                    Trang chủ
                                </div>
                                <div class="row-right">
                                    <asp:DropDownList ID="ddlAtHome" AppendDataBoundItems="true" runat="server" class="form-control">
                                        <asp:ListItem Text="Ẩn" Value="False" />
                                        <asp:ListItem Text="Hiện" Value="True" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Ảnh đại diện
                                </div>
                                <div class="row-right">
                                    <telerik:RadAsyncUpload Skin="Metro" runat="server" ID="PostPublicThumbnailImage" ChunkSize="0" Localization-Select="Chọn ảnh" AllowedFileExtensions=".jpeg,.jpg,.png" MultipleFileSelection="Disabled" OnClientFileSelected="OnClientFileSelected1" MaxFileInputsCount="1"  OnClientFileUploadRemoved="OnClientFileUploadRemoved">
                                    </telerik:RadAsyncUpload>
                                    <asp:Image runat="server" ID="PostPublicThumbnail" Width="200" />
                                    <asp:HiddenField runat="server" ID="ListPostPublicThumbnail" ClientIDMode="Static" />
                                    <div class="hidPostPublicThumbnail"></div>
                                </div>
                            </div>
                            <div class="input-summary form-row">
                                <div class="row-left">
                                    Mô tả ngắn
                                </div>
                                <div class="row-right">
                                    <telerik:RadEditor runat="server" ID="pSummary" Width="100%" Height="150px" ToolsFile="~/FilesResources/ToolContent.xml" Skin="Metro" DialogHandlerUrl="~/Telerik.Web.UI.DialogHandler.axd" AutoResizeHeight="False" ContentFilters="MakeUrlsAbsolute">
                                        <ImageManager ViewPaths="~/uploads/images/posts" UploadPaths="~/uploads/images/posts" DeletePaths="~/uploads/images/posts" />
                                    </telerik:RadEditor>
                                </div>
                            </div>
                            <div class="input-content form-row">
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
        <asp:HiddenField ID="hdfAction" runat="server" />
        <asp:HiddenField ID="hdfParentID" runat="server" />
    </main>

    <telerik:RadCodeBlock runat="server">
        <script>
            $(document).ready(function () {
                var action = $("#<%=hdfAction.ClientID%>").val();
                if (action == "show_web") {
                    $(".input-link").removeClass("hide");
                    $(".input-slug").addClass("hide");
                }
                else if (action == "view_more") {
                    $(".input-slug").removeClass("hide");
                    $(".input-link").addClass("hide");
                }
            });

            function redirectTo(ID) {
                window.location.href = "/xem-thong-bao?id=" + ID;
            }

            var storedFiles = [];

            function isBlank(str) {
                return (!str || /^\s*$/.test(str));
            }

            function changeAction() {
                var action = $("#<%=ddlAction.ClientID%>").val();
                if (action == "show_web") {
                    $(".input-link").removeClass("hide");
                    $(".input-slug").addClass("hide");
                    $(".input-summary").removeClass("hide");
                    $(".input-content").addClass("hide");
                }
                else if (action == "view_more") {
                    $(".input-slug").removeClass("hide");
                    $(".input-link").addClass("hide");
                    $(".input-summary").removeClass("hide");
                    $(".input-content").removeClass("hide");

                    var slug = $("#<%=txtSlug.ClientID%>").val();
                    if (slug == "") {
                        ChangeToSlug();
                    }
                }
                else {
                    $(".input-slug").addClass("hide");
                    $(".input-link").addClass("hide");
                    $(".input-summary").addClass("hide");
                    $(".input-content").addClass("hide");
                }
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
                    url: "/tao-thong-bao.aspx/getParent",
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

            function updatePost() {
                var action = $("#<%=ddlAction.ClientID%>").val();
                var category = $("#<%=hdfParentID.ClientID%>").val();
                var title = $("#<%=txtTitle.ClientID%>").val();
                var slug = $("#<%=txtSlug.ClientID%>").val();
                var link = $("#<%=txtLink.ClientID%>").val();

                if (title == "") {
                    $("#<%=txtTitle.ClientID%>").focus();
                    swal("Thông báo", "Chưa nhập tiêu đề bài viết", "error");
                }
                else if (action == "") {
                    $("#<%=ddlAction.ClientID%>").focus();
                    swal("Thông báo", "Chưa chọn kiểu bài viết", "error");
                }
                else if (action == "view_more" && slug == "") {
                    $("#<%=txtSlug.ClientID%>").focus();
                    swal("Thông báo", "Chưa nhập slug", "error");
                }
                else if (action == "show_web" && link == "") {
                    $("#<%=txtLink.ClientID%>").focus();
                    swal("Thông báo", "Chưa nhập link", "error");
                }
                else if (category == "") {
                    $("#<%=ddlCategory.ClientID%>").focus();
                    swal("Thông báo", "Chưa chọn danh mục bài viết", "error");
                }
                else {
                    HoldOn.open();
                    $("#<%=btnSubmit.ClientID%>").click();
                }
            }

            function imagepreview(input, obj) {
                if (input.files && input.files[0]) {
                    var reader = new FileReader();

                    reader.onload = function (e) {
                        obj.parent().find(".imgpreview").attr("src", e.target.result);
                        obj.parent().find(".imgpreview").attr("data-file-name", obj.parent().find("input:file").val());
                        obj.parent().find(".btn-delete").removeClass("hide");
                    }

                    reader.readAsDataURL(input.files[0]);
                }
            }

            function OnClientFileSelected1(sender, args) {
                if ($telerik.isIE) return;
                else {
                    truncateName(args);
                    var file = args.get_fileInputField().files.item(args.get_rowIndex());
                    showThumbnail(file, args);
                    $("#<%= PostPublicThumbnail.ClientID %>").hide();
                }
            }

            function OnClientFileUploadRemoved(sender, args) {
                    $("#<%= PostPublicThumbnail.ClientID %>").show();
            }

        </script>
    </telerik:RadCodeBlock>
</asp:Content>
