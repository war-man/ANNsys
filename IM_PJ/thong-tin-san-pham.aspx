<%@ Page Title="Thông tin sản phẩm" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="thong-tin-san-pham.aspx.cs" Inherits="IM_PJ.thong_tin_san_pham" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .generat-variable-content {
            float: left;
            width: 100%;
            margin: 20px 0;
        }

        .generat-variable-content .item-var-gen {
                float: left;
                width: 100%;
                margin: 15px 0;
                border: dotted 1px #ccc;
                padding: 15px 0;
        }
        .variableselect {
            float: left;
            width: 100%;
            clear: both;
            margin: 10px 0;
        }

        .variable-select {
            float: left;
            width: 30%;
            margin-bottom: 10px;
            border: solid 1px #4a4a4a;
            margin-left: 10px;
        }

            .variable-select .variablename {
                float: left;
                width: 100%;
                margin-right: 10px;
                background: blue;
                color: #fff;
                text-align: center;
                padding: 10px 0;
                line-height: 40px;
            }

            .variable-select .variablevalue {
                float: left;
                width: 100%;
                padding: 10px;
            }

                .variable-select .variablevalue .variablevalue-item {
                    float: left;
                    width: 100%;
                    clear: both;
                    margin-bottom: 10px;
                    border-bottom: solid 1px #ccc;
                    padding-bottom: 5px;
                }

                    .variable-select .variablevalue .variablevalue-item:last-child {
                        border: none;
                    }

                    .variable-select .variablevalue .variablevalue-item .v-value {
                        float: left;
                        width: 78%;
                        line-height: 40px;
                    }

                    .variable-select .variablevalue .variablevalue-item .v-delete {
                        float: left;
                        width: 20%;
                    }

        #selectvariabletitle {
            float: left;
            width: 70%;
            clear: both;
            font-weight: bold;
            margin: 20px 0;
            display: none;
        }

        #generateVariable {
            float: right;
            display: block;
        }

        .width {
            width: calc(100% - 100px);
        }
        .img-product {
            width: 200px;
        }
        @media (max-width: 769px) {
            .RadUpload .ruInputs li {
                width: 100%;
            }
            .img-product {
                width: 100%;
            }
            ul.image-gallery li {
                width: 100%;
            }
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
                            <h3 class="page-title left not-margin-bot">Chỉnh sửa sản phẩm</h3>
                        </div>
                        <div class="panel-body">
                            <div class="form-row">
                                <asp:Label ID="lblError" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                            </div>
                            
                            <div class="form-row">
                                <div class="row-left">
                                    Tên sản phẩm
                                    <asp:RequiredFieldValidator ID="rq" runat="server" ControlToValidate="txtProductTitle" ForeColor="Red" SetFocusOnError="true" ErrorMessage="(*)" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtProductTitle" runat="server" CssClass="form-control" placeholder="Tên sản phẩm"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Danh mục
                                </div>
                                <div class="row-right parent">
                                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control slparent" date-name="parentID" data-level="1" onchange="chooseParent($(this))">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Mã sản phẩm
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtProductSKU" Enabled="false" runat="server" CssClass="form-control sku-input" placeholder="Mã sản phẩm"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Chất liệu
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtMaterials" runat="server" CssClass="form-control" placeholder="Chất liệu"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Tồn kho ít nhất
                                </div>
                                <div class="row-right">
                                    <asp:TextBox type="number" min="0" ID="pMinimumInventoryLevel" runat="server" CssClass="form-control" placeholder="Số lượng tồn kho ít nhất"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Tồn kho nhiều nhất
                                </div>
                                <div class="row-right">
                                    <asp:TextBox type="number" min="0" ID="pMaximumInventoryLevel" runat="server" CssClass="form-control" placeholder="Số lượng tồn kho nhiều nhất"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Nhà cung cấp
                                </div>
                                <div class="row-right">
                                    <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Giá sỉ
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="pRegular_Price" ForeColor="Red" ErrorMessage="(*)" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>
                                <div class="row-right">
                                    <asp:TextBox type="number" min="0" autocomplete="off" ID="pRegular_Price" runat="server" CssClass="form-control" placeholder="Giá sỉ"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row cost-of-goods">
                                <div class="row-left">
                                    Giá vốn
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="pCostOfGood" ForeColor="Red" ErrorMessage="(*)" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>
                                <div class="row-right">
                                    <asp:TextBox type="number" min="0" autocomplete="off" ID="pCostOfGood" runat="server" CssClass="form-control cost-price" placeholder="Giá vốn"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Giá lẻ
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="pRetailPrice" ForeColor="Red" ErrorMessage="(*)" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>
                                <div class="row-right">
                                    <asp:TextBox type="number" min="0" autocomplete="off" ID="pRetailPrice" runat="server" CssClass="form-control" placeholder="Giá lẻ"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Ảnh đại diện
                                </div>
                                <div class="row-right">
                                    <telerik:RadAsyncUpload Skin="Metro" runat="server" ID="ProductThumbnailImage" ChunkSize="0"
                                        Localization-Select="Chọn ảnh" AllowedFileExtensions=".jpeg,.jpg,.png"
                                        MultipleFileSelection="Disabled" OnClientFileSelected="OnClientFileSelected1" MaxFileInputsCount="1"  OnClientFileUploadRemoved="OnClientFileUploadRemoved1">
                                    </telerik:RadAsyncUpload>
                                    <asp:Image runat="server" ID="ProductThumbnail" class="img-product" />
                                    <asp:HiddenField runat="server" ID="ListProductThumbnail" ClientIDMode="Static" />
                                    <div class="hidProductThumbnail"></div>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Ảnh đại diện sạch
                                </div>
                                <div class="row-right">
                                    <telerik:RadAsyncUpload Skin="Metro" runat="server" ID="ProductThumbnailImageClean" ChunkSize="0"
                                        Localization-Select="Chọn ảnh" AllowedFileExtensions=".jpeg,.jpg,.png"
                                        MultipleFileSelection="Disabled" OnClientFileSelected="OnClientFileSelected2" MaxFileInputsCount="1"  OnClientFileUploadRemoved="OnClientFileUploadRemoved2">
                                    </telerik:RadAsyncUpload>
                                    <asp:Image runat="server" ID="ProductThumbnailClean" Width="200" />
                                    <asp:HiddenField runat="server" ID="ListProductThumbnailClean" ClientIDMode="Static" />
                                    <div class="hidProductThumbnailClean"></div>
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
                                <div class="row-left">
                                    Ẩn                   
                                </div>
                                <div class="row-right">
                                    <asp:CheckBox ID="chkIsHidden" runat="server" />
                                </div>
                            </div>
                            <div class="form-row variable">
                                <div class="row-left">
                                    Biến thể
                                </div>
                                <div class="row-right">
                                    <div class="generat-variable-content">
                                        <div class="row">
                                            <div class="col-md-10">
                                                <h3>Danh sách biến thể</h3>
                                            </div>
                                            <div class="col-md-2">
                                                <a href="javascript:;" onclick="addVariant()" id="delete" class="btn primary-btn fw-btn not-fullwidth">Thêm biến thể</a>
                                            </div>
                                        </div>
                                        <div class="row list-item-genred">
                                            <div class="col-md-12">
                                                <asp:Literal ID="ltrVariables" runat="server"></asp:Literal>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-row">
                                <a href="javascript:;" class="btn primary-btn fw-btn not-fullwidth" onclick="updateProduct()">Cập nhật</a>
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn primary-btn fw-btn not-fullwidth" Text="Cập nhật" OnClick="btnSubmit_Click" Style="display: none" />
                                <asp:Literal ID="ltrBack" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hdfTempVariable" runat="server" />
        <asp:HiddenField ID="hdfUploadGallery" runat="server" />
        <asp:HiddenField ID="hdfDeleteImageGallery" runat="server" />
        <asp:HiddenField ID="hdfsetStyle" runat="server" />
        <asp:HiddenField ID="hdfVariableFull" runat="server" />
        <asp:HiddenField ID="hdfVariableListInsert" runat="server" />
        <asp:HiddenField ID="hdfParentID" runat="server" />
        <asp:HiddenField ID="hdfUserRole" runat="server" />
    </main>

    <telerik:RadCodeBlock runat="server">
        <script>

            var storedFiles = [];

            $(document).ready(function () {
                var userRole = $("#<%=hdfUserRole.ClientID%>").val();
                if (userRole != "0") {
                    $(".cost-of-goods").addClass("hide");
                }
            });

            function showVariableContent(obj) {
                var content = obj.parent().find(".variable-content");
                if (content.is(":hidden")) {
                    content.show();
                    obj.addClass("margin-bottom-15");
                }
                else {
                    content.hide();
                    obj.removeClass("margin-bottom-15");
                }
            }

            function isBlank(str) {
                return (!str || /^\s*$/.test(str));
            }

            function chooseParent(obj) {
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
                    url: "/tao-san-pham.aspx/getParent",
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

            function changeVariable(obj) {
                var parentDiv = obj.closest(".item-var-gen");
                var datanameid = "";
                var datavalueid = "";
                var datanametext = "";
                var datavaluetext = "";
                var datanamevalue = "";
                var variableSKU = $("#<%=txtProductSKU.ClientID%>").val();
                parentDiv.find("select option:selected").each(function () {
                    datanameid += $(this).parent().attr("data-name-id") + "|";
                    datavalueid += $(this).val() + "|";
                    datanametext += $(this).parent().attr("data-name-text") + "|";
                    datavaluetext += $(this).text() + "|";
                    datanamevalue += $(this).parent().attr("data-name-id") + ":" + $(this).val() + "|";
                    variableSKU += $(this).attr("data-sku-text");
                });

                parentDiv.attr("data-name-id", datanameid);
                parentDiv.attr("data-value-id", datavalueid);
                parentDiv.attr("data-name-text", datanametext);
                parentDiv.attr("data-value-text", datavaluetext);
                parentDiv.attr("data-name-value", datanamevalue);
                parentDiv.find(".productVariableImage").attr("name", datanamevalue);
                if (!parentDiv.find(".productvariablesku").prop('disabled')) {
                    parentDiv.find(".productvariablesku").val(variableSKU);
                }
                
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
                            $(".image-gallery").append("<li><img src='" + e.target.result + "' /><a href='javascript:;' data-image-id='' onclick='deleteImageGallery($(this))' class='btn-delete'><i class=\"fa fa-times\" aria-hidden=\"true\"></i> Xóa hình</a></li>")
                            base64 += e.target.result + "|";
                        }
                        reader.readAsDataURL(input.files[i]);
                    }
                    allSizes = allSizes.substring(0, allSizes.length - 1);

                    $("#<%=hdfUploadGallery.ClientID%>").val(allSizes);
                }
            }

            function updateProduct() {
                var listv = "";
                var a = $("#<%=hdfsetStyle.ClientID%>").val();
                var parent = $("#<%=hdfParentID.ClientID%>").val();
                if (a == 2) {
                    var title = $("#<%=txtProductTitle.ClientID%>").val();
                    var SKU = $("#<%=txtProductSKU.ClientID%>").val();
                    var materials = $("#<%=txtMaterials.ClientID%>").val();
                    var maximum = $("#<%=pMaximumInventoryLevel.ClientID%>").val();
                    var minimum = $("#<%=pMinimumInventoryLevel.ClientID%>").val();
                    var giasi = $("#<%=pRegular_Price.ClientID%>").val();
                    var giavon = $("#<%=pCostOfGood.ClientID%>").val();
                    var giale = $("#<%=pRetailPrice.ClientID%>").val();

                    if (parent == "") {
                        $("#<%=ddlCategory.ClientID%>").focus();
                        swal("Thông báo", "Chưa chọn danh mục sản phẩm", "error");
                    }
                    else if (title == "") {
                        $("#<%=txtProductTitle.ClientID%>").focus();
                        swal("Thông báo", "Chưa nhập tên sản phẩm", "error");
                    }
                    else if (SKU == "") {
                        $("#<%=txtProductSKU.ClientID%>").focus();
                        swal("Thông báo", "Chưa nhập mã sản phẩm", "error");
                    }
                    else if (materials == "") {
                        $("#<%=txtMaterials.ClientID%>").focus();
                        swal("Thông báo", "Chưa nhập chất liệu sản phẩm", "error");
                    }
                    else if (giasi == "") {
                        $("#<%=pRegular_Price.ClientID%>").focus();
                        swal("Thông báo", "Chưa nhập giá sỉ", "error");
                    }
                    else if (giavon == "") {
                        $("#<%=pCostOfGood.ClientID%>").focus();
                        swal("Thông báo", "Chưa nhập giá vốn", "error");
                    }
                    else if (giale == "") {
                        $("#<%=pRetailPrice.ClientID%>").focus();
                        swal("Thông báo", "Chưa nhập giá lẻ", "error");
                    }
                    else if (parseFloat(giasi) < parseFloat(giavon)) {
                        $("#<%=pRegular_Price.ClientID%>").focus();
                        swal("Thông báo", "Gía sỉ không được thấp hơn giá vốn", "error");
                    }
                    else if (parseFloat(giasi) > parseFloat(giale)) {
                        $("#<%=pRetailPrice.ClientID%>").focus();
                        swal("Thông báo", "Giá lẻ không được thấp hơn giá sỉ", "error");
                    }
                    else {
                        if ($(".item-var-gen").length > 0) {
                            var checkError = false;
                            var errorIn = [];
                            var items = $(".item-var-gen");
                            var arraySKU = [];
                            var arrayVariableValue = [];

                            $.each(items , function (index, value) {
                                var productvariablesku = $(this).find(".productvariablesku").val();
                                var regularprice = $(this).find(".regularprice").val();
                                var costofgood = $(this).find(".costofgood").val();
                                var retailprice = $(this).find(".retailprice").val();
                                var image = $(this).find(".productVariableImage").attr("name");
                                var variable = true;

                                $(this).find("select").each(function () {
                                    if ($(this).val() == "" || $(this).val() == null) {
                                        variable = false;
                                    }
                                });

                                arraySKU.push(productvariablesku);
                                arrayVariableValue.push($(this).attr("data-name-value"));

                                // check null value
                                if (isBlank(productvariablesku) || isBlank(regularprice) || isBlank(costofgood) || isBlank(retailprice) || isBlank(variable) || variable == false) {
                                    checkError = true;
                                    errorIn.push(index);
                                }

                                $(this).css("background-color", "#fff");
                            });

                            // Check duplicate SKU
                            var sorted_arr = arraySKU.slice().sort();
                            var resultDuplicateSKU = [];
                            for (var i = 0; i < sorted_arr.length - 1; i++) {
                                if (sorted_arr[i + 1] == sorted_arr[i]) {
                                    resultDuplicateSKU.push(sorted_arr[i]);
                                }
                            }

                            // Check duplicate variable value
                            var sorted_arr = arrayVariableValue.slice().sort();
                            var resultDuplicateVariable = [];
                            for (var i = 0; i < sorted_arr.length - 1; i++) {
                                if (sorted_arr[i + 1] == sorted_arr[i]) {
                                    resultDuplicateVariable.push(sorted_arr[i]);
                                }
                            }

                            if (checkError == true) {
                                $.each(errorIn, function (index, value) {
                                    $(".item-var-gen").eq(value).css("background-color", "#fff0c5");
                                });

                                swal({
                                    title: "Thông báo",
                                    text: "Hãy nhập đầy đủ thông tin các biến thể!",
                                    type: "warning",
                                    showCancelButton: false,
                                    closeOnConfirm: true,
                                    confirmButtonText: "Để em xem lại...",
                                }, function () {
                                    $('html, body').animate({
                                        scrollTop: $(".item-var-gen").eq(errorIn[0]).offset().top - 150
                                    }, 500);
                                });
                            }
                            else if (resultDuplicateSKU.length > 0) {
                                swal({
                                    title: "Thông báo",
                                    text: "Có biến thể trùng mã sản phẩm!",
                                    type: "warning",
                                    showCancelButton: false,
                                    closeOnConfirm: true,
                                    confirmButtonText: "Để em xem lại...",
                                }, function () {
                                    $('html, body').animate({
                                        scrollTop: $(".item-var-gen").eq(0).offset().top - 150
                                    }, 500);
                                });
                            }
                            else if (resultDuplicateVariable.length > 0) {
                                swal({
                                    title: "Thông báo",
                                    text: "Có biến thể trùng thuộc tính!",
                                    type: "warning",
                                    showCancelButton: false,
                                    closeOnConfirm: true,
                                    confirmButtonText: "Để em xem lại...",
                                }, function () {
                                    $('html, body').animate({
                                        scrollTop: $(".item-var-gen").eq(0).offset().top - 150
                                    }, 500);
                                });
                            }
                            else {

                                $(".item-var-gen").each(function () {
                                    var datanameid = $(this).attr("data-name-id");
                                    var datavalueid = $(this).attr("data-value-id");
                                    var datanametext = $(this).attr("data-name-text");
                                    var datavaluetext = $(this).attr("data-value-text");
                                    var productvariablesku = $(this).find(".productvariablesku").val();
                                    var regularprice = $(this).find(".regularprice").val();
                                    var costofgood = $(this).find(".costofgood").val();
                                    var retailprice = $(this).find(".retailprice").val();
                                    var datanamevalue = $(this).attr("data-name-value");
                                    var max = $(this).find(".maximum").val();
                                    var min = $(this).find(".minimum").val();
                                    var StockStatus = 3;
                                    var checked = true;
                                    var image = $(this).find(".imgpreview").attr("data-file-name");
                                    

                                    if (!isBlank(productvariablesku) && !isBlank(regularprice) && !isBlank(costofgood) && !isBlank(retailprice) && !isBlank(StockStatus)) {

                                        listv += datanameid + ";" + datavalueid + ";" + datanametext + ";" + datavaluetext + ";" + productvariablesku + ";" + regularprice.replace(",", "") + ";" + costofgood.replace(",", "") + ";" + retailprice.replace(",", "") + ";" + datanamevalue + ";" + max + ";" + min + ";" + StockStatus + ";" + checked + ";" + image + ",";

                                        $("#<%=hdfVariableListInsert.ClientID%>").val(listv);

                                        HoldOn.open();
                                        $("#<%=btnSubmit.ClientID%>").click();
                                    }
                                    else {
                                        swal("Lỗi", "Hãy kiểm tra thông tin các biến thể", "error");
                                    }
                                });
                            }
                        }
                        else {
                            swal("Lỗi", "Chưa thiếp lập biến thể sản phẩm", "error");
                        }
                    }
                }
                else {
                    var title = $("#<%=txtProductTitle.ClientID%>").val();
                    var materials = $("#<%=txtMaterials.ClientID%>").val();
                    var maximum = $("#<%=pMaximumInventoryLevel.ClientID%>").val();
                    var minimum = $("#<%=pMinimumInventoryLevel.ClientID%>").val();
                    var giasi = $("#<%=pRegular_Price.ClientID%>").val();
                    var giavon = $("#<%=pCostOfGood.ClientID%>").val();
                    var giale = $("#<%=pRetailPrice.ClientID%>").val();

                    if (title == "") {
                        $("#<%=txtProductTitle.ClientID%>").focus();
                        swal("Thông báo", "Chưa nhập tên sản phẩm", "error");
                    }
                    else if (materials == "") {
                        $("#<%=txtMaterials.ClientID%>").focus();
                        swal("Thông báo", "Chưa nhập chất liệu sản phẩm", "error");
                    }
                    else if (giasi == "") {
                        $("#<%=pRegular_Price.ClientID%>").focus();
                        swal("Thông báo", "Chưa nhập giá sỉ", "error");
                    }
                    else if (giavon == "") {
                        $("#<%=pCostOfGood.ClientID%>").focus();
                        swal("Thông báo", "Chưa nhập giá vốn", "error");
                    }
                    else if (giale == "") {
                        $("#<%=pRetailPrice.ClientID%>").focus();
                        swal("Thông báo", "Chưa nhập giá lẻ", "error");
                    }
                    else if (parseFloat(giasi) < parseFloat(giavon)) {
                        $("#<%=pRegular_Price.ClientID%>").focus();
                        swal("Thông báo", "Giá sỉ không được thấp hơn giá vốn", "error");
                    }
                    else if (parseFloat(giasi) > parseFloat(giale)) {
                        $("#<%=pRetailPrice.ClientID%>").focus();
                        swal("Thông báo", "Giá lẻ không được thấp hơn giá sỉ", "error");
                    }
                    else {
                        $("#<%=btnSubmit.ClientID%>").click();
                        HoldOn.open();
                    }
                }
            }

            function addVariant() {
                $(".item-var-gen:eq('0')").clone().prependTo(".list-item-genred > .col-md-12");
                var parentSKU = $("#<%=txtProductSKU.ClientID%>").val();
                $(".item-var-gen:eq('0')").find(".productvariablesku").val(parentSKU).prop("disabled", false);
                $(".item-var-gen:eq('0')").find("select[name='ddlVariableValue']").prop("selectedIndex", 0);
                $(".item-var-gen:eq('0')").find(".variable-label").html("");
                $(".item-var-gen:eq('0')").find(".imgpreview").attr("src", "/App_Themes/Ann/image/placeholder.png");
                $(".item-var-gen:eq('0')").find(".variable-content").show();
                $(".item-var-gen:eq('0')").css("background-color", "#fff");
                $(".item-var-gen:eq('0')").find(".btn-delete").addClass("hide");
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

            function deleteImageVariable(obj) {
                obj.parent().find(".imgpreview").attr("src", "/App_Themes/Ann/image/placeholder.png").attr("data-file-name", "/App_Themes/Ann/image/placeholder.png");
                obj.addClass("hide");
            }

            function AddNewProduct() {
                if ($(".row-variable-selected").length > 0) {
                    var check = true;
                    var listva = "";
                    $(".row-variable-selected").each(function () {
                        var sku = $(this).find(".sku-variable").val();
                        if (!isBlank(sku)) {

                        }
                    });
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

            function addVariable() {
                var variable_selected = "";
                var valu_list = "";
                var select_count = 0;
                var current_selected = $("#<%= hdfTempVariable.ClientID%>").val();
                $(".variable-row").each(function () {
                    var v_name = $(this).find(".variable-name").attr("data-name");
                    var vl_id = $(this).find(".variable-value").val();
                    var vl_name = $(this).find(".variable-value :selected").text();

                    if (vl_id > 0) {
                        variable_selected += v_name + ":" + vl_name + ":" + vl_id + "|";
                        select_count++;
                    }
                });
                if (select_count > 0) {
                    var check = false;
                    var itemcur = current_selected.split(',');
                    if (itemcur.length - 1 > 0) {
                        for (var j = 0; j < itemcur.length - 1; j++) {
                            if (itemcur[j] == variable_selected)
                                check = true;
                        }
                    }
                    if (check == false) {
                        itemcur += variable_selected + ",";
                        $("#<%= hdfTempVariable.ClientID%>").val(itemcur);
                        var html = "";
                        var vs = variable_selected.split('|');
                        if (vs.length - 1 > 0) {
                            html += "<div class=\"row-variable-selected\">";
                            html += "   <div class=\"v-element\"><input type=\"text\" class=\"form-control sku-variable\" placeholder=\"SKU\"></div>";
                            html += "   <div class=\"v-element\"><input type=\"number\" min=\"0\" class=\"form-control stock-variable\" placeholder=\"Stock\"></div>";
                            html += "   <div class=\"v-element\"><select class=\"form-control stock-status\"><option value=\"1\">Instock</option><option value=\"2\">Out of stock</option></select></div>";
                            html += "   <div class=\"v-element\"><input type=\"number\" min=\"0\" class=\"form-control regularprice-variable\" placeholder=\"Regular Price\"></div>";
                            html += "   <div class=\"v-element\"><input type=\"number\" min=\"0\" class=\"form-control costofgood-variable\" placeholder=\"Cost Of Good\"></div>";
                            html += "   <div class=\"v-element\"><input type=\"checkbox\" class=\"form-control managestock-variable\"></div>";
                            html += "   <div class=\"v-element\"><input type=\"checkbox\" class=\"form-control managestock-variable\"></div>";
                            for (var i = 0; i < vs.length - 1; i++) {
                                var item = vs[i];
                                var item_element = item.split(':');
                                var v_name1 = item_element[0];
                                var vl_name1 = item_element[1];
                                var vl_id1 = item_element[2];
                                html += "<div class=\"v-element content-vari-value\" data-vname=\"" + v_name1 + "\" data-vl_name=\"" + vl_name1 + "\" data-vl_id=\"" + vl_id1 + "\">" + v_name1 + ": " + vl_name1 + "</div>";

                            }
                            html += "   <div class=\"v-element delete-value\"><a href=\"javascript:;\" onclick=\"deleterowva($(this))\" class=\"btn primary-btn fw-btn not-fullwidth\">Xóa</a></div>";
                            html += "</div>";
                        }
                        $(".variable-selected").append(html);
                    }
                }
                else {

                }
            }

            function deleterowva(obj) {
                var c = confirm("Bạn muốn xóa thuộc tính này?");
                if (c == true) {
                    obj.parent().parent().remove();
                    var newc = "";
                    $(".row-variable-selected").each(function () {
                        $(this).find(".content-vari-value").each(function () {
                            var vname = $(this).attr("data-vname");
                            var vl_name = $(this).attr("data-vl_name");
                            var vl_id = $(this).attr("data-vl_id");
                            newc += vname + ":" + vl_name + ":" + vl_id + "|";
                        });
                        newc += ",";
                    });
                    $("#<%=hdfTempVariable.ClientID%>").val(newc);
                }
            }

            function OnClientFileSelected1(sender, args) {
                if ($telerik.isIE) return;
                else {
                    truncateName(args);
                    var file = args.get_fileInputField().files.item(args.get_rowIndex());
                    showThumbnail(file, args);
                    $("#<%= ProductThumbnail.ClientID %>").hide();
                }
            }

            function OnClientFileSelected2(sender, args) {
                if ($telerik.isIE) return;
                else {
                    truncateName(args);
                    var file = args.get_fileInputField().files.item(args.get_rowIndex());
                    showThumbnail(file, args);
                    $("#<%= ProductThumbnailClean.ClientID %>").hide();
                }
            }

            function OnClientFileUploadRemoved1(sender, args) {
                    $("#<%= ProductThumbnail.ClientID %>").show();
            }

            function OnClientFileUploadRemoved2(sender, args) {
                    $("#<%= ProductThumbnailClean.ClientID %>").show();
            }

        </script>
    </telerik:RadCodeBlock>
</asp:Content>
