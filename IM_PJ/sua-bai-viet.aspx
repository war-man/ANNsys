<%@ Page Title="Chỉnh sửa bài viết" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="sua-bai-viet.aspx.cs" Inherits="IM_PJ.sua_bai_viet" %>

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
                <div class="col-md-12">
                    <div class="panel panelborderheading">
                        <div class="panel-heading clear">
                            <h3 class="page-title left not-margin-bot">Chỉnh sửa bài viết</h3>
                        </div>
                        <div class="panel-body">
                            <div class="form-row">
                                <asp:Label ID="lblError" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Tiêu đề
                                    <asp:RequiredFieldValidator ID="rq" runat="server" ControlToValidate="txtPostTitle" ForeColor="Red" SetFocusOnError="true" ErrorMessage="(*)" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtPostTitle" runat="server" CssClass="form-control" placeholder="Tiêu đề bài viết"></asp:TextBox>
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
                                    <telerik:RadEditor runat="server" ID="pContent" Width="100%" Height="600px" ToolsFile="~/FilesResources/ToolContent.xml" Skin="Metro" DialogHandlerUrl="~/Telerik.Web.UI.DialogHandler.axd" AutoResizeHeight="False">
                                        <ImageManager ViewPaths="~/uploads/images" UploadPaths="~/uploads/images" DeletePaths="~/uploads/images" />
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
                                <a href="javascript:;" class="btn primary-btn fw-btn not-fullwidth" onclick="updatePost()">Cập nhật</a>
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn primary-btn fw-btn not-fullwidth" Text="Cập nhật" OnClick="btnSubmit_Click" Style="display: none" />
                                <asp:Literal ID="ltrBack" runat="server"></asp:Literal>
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
        <asp:HiddenField ID="hdfUserRole" runat="server" />
    </main>

    <telerik:RadCodeBlock runat="server">
        <script>

            var storedFiles = [];

            function isBlank(str) {
                return (!str || /^\s*$/.test(str));
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
                })
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
                            html += "<select class=\"form-control slparent\" style=\"margin-top:15px;\" data-level=" + level + " onchange=\"chooseParent($(this))\">";
                            html += "<option  value=\"0\">Chọn danh mục</option>";
                            for (var i = 0; i < data.length; i++) {
                                html += "<option value=\"" + data[i].ID + "\">" + data[i].CategoryName + "</option>";
                            }
                            html += "</select>";
                        }
                        $(".parent").append(html);
                    }
                })
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
                            $(".image-gallery").append("<li><img src='" + e.target.result + "' /><a href='javascript:;' data-image-id='' onclick='deleteImageGallery($(this))' class='btn-delete'><i class=\"fa fa-times\" aria-hidden=\"true\"></i> Xóa hình</a></li>'")
                            base64 += e.target.result + "|";
                        }
                        reader.readAsDataURL(input.files[i]);
                    }
                    allSizes = allSizes.substring(0, allSizes.length - 1);

                    $("#<%=hdfUploadGallery.ClientID%>").val(allSizes);
                }
            }

            function updatePost() {

                var category = $("#<%=hdfParentID.ClientID%>").val();
                var title = $("#<%=txtPostTitle.ClientID%>").val();

                if (category == "") {
                    $("#<%=ddlCategory.ClientID%>").focus();
                    swal("Thông báo", "Chưa chọn danh mục bài viết", "error");
                }
                if (title == "") {
                    $("#<%=txtPostTitle.ClientID%>").focus();
                    swal("Thông báo", "Chưa nhập tiêu đề bài viết", "error");
                }
                else {
                    $("#<%=btnSubmit.ClientID%>").click();
                    HoldOn.open();
                }
            }

            function openUploadImage(obj) {
                obj.parent().find(".productVariableImage").click();
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
                    $("#<%= PostThumbnail.ClientID %>").hide();
                }
            }

            function OnClientFileUploadRemoved(sender, args) {
                    $("#<%= PostThumbnail.ClientID %>").show();
            }

        </script>
    </telerik:RadCodeBlock>
</asp:Content>
