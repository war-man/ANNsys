﻿<%@ Page Title="Thêm sản phẩm" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="tao-san-pham.aspx.cs" Inherits="IM_PJ.tao_san_pham" EnableSessionState="ReadOnly" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" rel="stylesheet" href="Content/bootstrap-tagsinput.css" />
    <link type="text/css" rel="stylesheet" href="Content/bootstrap-tagsinput-typeahead.css" />
    <link type="text/css" rel="stylesheet" href="Content/typeahead.css" />
    <style>
        .select2-container {
            width: 100%!important;
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
            margin-bottom: 15px;
            border: solid 1px #4a4a4a;
            margin-right: 15px;
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
        .variable-name-select {
            float: left; 
            width: 30%; 
            padding-right: 15px;
        }
        .variable-value-select {
            float: left; 
            width: 30%; 
            padding-right: 15px;
        }
        .variable-button-select {
            float: left; 
            width: 30%;
            padding-right: 15px;
        }
        .generat-variable-content {
            float: left;
            width: 100%;
            margin: 20px 0;
            display: none;
        }
        .generat-variable-content .item-var-gen {
            float: left;
            width: 100%;
            margin: 15px 0;
            border: dotted 1px #ccc;
            padding: 15px 0;
        }

        .bootstrap-tagsinput {
            width: 100%;
        }

        .bootstrap-tagsinput .label {
            font-size: 100%;
        }

        .bootstrap-tagsinput .twitter-typeahead input {
            margin-top: 5px;
        }

        .bootstrap-tagsinput input {
            width: 100%
        }

        @media (max-width: 769px) {
            .RadUpload .ruInputs li {
                width: 100%;
            }
            .variable-select {
                width: 100%;
            }
            #selectvariabletitle {
                width: 100%;
            }
            .variable-name-select, .variable-value-select, .variable-button-select {
                width: 100%;
                padding-top: 15px;
                padding-right: 0;
            }
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
                            <h3 class="page-title left not-margin-bot">Thêm sản phẩm</h3>
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
                                    <asp:TextBox ID="txtProductTitle" runat="server" CssClass="form-control" placeholder="Tên sản phẩm" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Danh mục
                                </div>
                                <div class="row-right parent">
                                    <select id="ddlCategory" date-name="parentID" runat="server" class="form-control slparent" data-level="1" onchange="selectCategory($(this))"></select>
                                </div>
                            </div>

                            <div class="form-row">
                                <div class="row-left">
                                    Mã sản phẩm
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtProductSKU" ForeColor="Red" ErrorMessage="(*)" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtProductSKU" runat="server" CssClass="form-control sku-input" onblur="CheckSKU()" placeholder="Mã sản phẩm"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Chất liệu
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMaterials" ForeColor="Red" SetFocusOnError="true" ErrorMessage="(*)" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtMaterials" runat="server" CssClass="form-control" placeholder="Chất liệu"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Loại sản phẩm
                                </div>
                                <div class="row-right">
                                    <select id="ddlProductStyle" class="form-control" onchange="selectProductType()">
                                        <option value="1">Đơn giản</option>
                                        <option value="2">Có biến thể</option>
                                    </select>
                                </div>
                            </div>
                            <div class="form-row main-color">
                                <div class="row-left">
                                    Màu chủ đạo
                                </div>
                                <div class="row-right">
                                    <asp:DropDownList ID="ddlColor" runat="server" CssClass="form-control select2" Width="100%">
                                        <asp:ListItem Value="" Text="Chọn màu chủ đạo"></asp:ListItem>
                                        <asp:ListItem Value="cam" Text="Cam"></asp:ListItem>
                                        <asp:ListItem Value="cam tươi" Text="Cam tươi"></asp:ListItem>
                                        <asp:ListItem Value="cam đất" Text="Cam đất"></asp:ListItem>
                                        <asp:ListItem Value="cam sữa" Text="Cam sữa"></asp:ListItem>
                                        <asp:ListItem Value="caro" Text="Caro"></asp:ListItem>
                                        <asp:ListItem Value="da bò" Text="Da bò"></asp:ListItem>
                                        <asp:ListItem Value="đen" Text="Đen"></asp:ListItem>
                                        <asp:ListItem Value="đỏ" Text="Đỏ"></asp:ListItem>
                                        <asp:ListItem Value="đỏ đô" Text="Đỏ đô"></asp:ListItem>
                                        <asp:ListItem Value="đỏ tươi" Text="Đỏ tươi"></asp:ListItem>
                                        <asp:ListItem Value="dưa cải" Text="Dưa cải"></asp:ListItem>
                                        <asp:ListItem Value="gạch tôm" Text="Gạch tôm"></asp:ListItem>
                                        <asp:ListItem Value="hồng" Text="Hồng"></asp:ListItem>
                                        <asp:ListItem Value="hồng cam" Text="Hồng cam"></asp:ListItem>
                                        <asp:ListItem Value="hồng da" Text="Hồng da"></asp:ListItem>
                                        <asp:ListItem Value="hồng dâu" Text="Hồng dâu"></asp:ListItem>
                                        <asp:ListItem Value="hồng phấn" Text="Hồng phấn"></asp:ListItem>
                                        <asp:ListItem Value="hồng ruốc" Text="Hồng ruốc"></asp:ListItem>
                                        <asp:ListItem Value="hồng sen" Text="Hồng sen"></asp:ListItem>
                                        <asp:ListItem Value="kem" Text="Kem"></asp:ListItem>
                                        <asp:ListItem Value="kem tươi" Text="Kem tươi"></asp:ListItem>
                                        <asp:ListItem Value="kem đậm" Text="Kem đậm"></asp:ListItem>
                                        <asp:ListItem Value="kem nhạt" Text="Kem nhạt"></asp:ListItem>
                                        <asp:ListItem Value="nâu" Text="Nâu"></asp:ListItem>
                                        <asp:ListItem Value="nho" Text="Nho"></asp:ListItem>
                                        <asp:ListItem Value="rạch tôm" Text="Rạch tôm"></asp:ListItem>
                                        <asp:ListItem Value="sọc" Text="Sọc"></asp:ListItem>
                                        <asp:ListItem Value="tím" Text="Tím"></asp:ListItem>
                                        <asp:ListItem Value="tím cà" Text="Tím cà"></asp:ListItem>
                                        <asp:ListItem Value="tím đậm" Text="Tím đậm"></asp:ListItem>
                                        <asp:ListItem Value="tím xiêm" Text="Tím xiêm"></asp:ListItem>
                                        <asp:ListItem Value="trắng" Text="Trắng"></asp:ListItem>
                                        <asp:ListItem Value="trắng-đen" Text="Trắng-đen"></asp:ListItem>
                                        <asp:ListItem Value="trắng-đỏ" Text="Trắng-đỏ"></asp:ListItem>
                                        <asp:ListItem Value="trắng-xanh" Text="Trắng-xanh"></asp:ListItem>
                                        <asp:ListItem Value="vàng" Text="Vàng"></asp:ListItem>
                                        <asp:ListItem Value="vàng tươi" Text="Vàng tươi"></asp:ListItem>
                                        <asp:ListItem Value="vàng bò" Text="Vàng bò"></asp:ListItem>
                                        <asp:ListItem Value="vàng nghệ" Text="Vàng nghệ"></asp:ListItem>
                                        <asp:ListItem Value="vàng nhạt" Text="Vàng nhạt"></asp:ListItem>
                                        <asp:ListItem Value="xanh vỏ đậu" Text="Xanh vỏ đậu"></asp:ListItem>
                                        <asp:ListItem Value="xám" Text="Xám"></asp:ListItem>
                                        <asp:ListItem Value="xám chì" Text="Xám chì"></asp:ListItem>
                                        <asp:ListItem Value="xám chuột" Text="Xám chuột"></asp:ListItem>
                                        <asp:ListItem Value="xám nhạt" Text="Xám nhạt"></asp:ListItem>
                                        <asp:ListItem Value="xám tiêu" Text="Xám tiêu"></asp:ListItem>
                                        <asp:ListItem Value="xám xanh" Text="Xám xanh"></asp:ListItem>
                                        <asp:ListItem Value="xanh biển" Text="Xanh biển"></asp:ListItem>
                                        <asp:ListItem Value="xanh biển đậm" Text="Xanh biển đậm"></asp:ListItem>
                                        <asp:ListItem Value="xanh lá chuối" Text="Xanh lá chuối"></asp:ListItem>
                                        <asp:ListItem Value="xanh cổ vịt" Text="Xanh cổ vịt"></asp:ListItem>
                                        <asp:ListItem Value="xanh coban" Text="Xanh coban"></asp:ListItem>
                                        <asp:ListItem Value="xanh da" Text="Xanh da"></asp:ListItem>
                                        <asp:ListItem Value="xanh dạ quang" Text="Xanh dạ quang"></asp:ListItem>
                                        <asp:ListItem Value="xanh đen" Text="Xanh đen"></asp:ListItem>
                                        <asp:ListItem Value="xanh jean" Text="Xanh jean"></asp:ListItem>
                                        <asp:ListItem Value="xanh lá" Text="Xanh lá"></asp:ListItem>
                                        <asp:ListItem Value="xanh lá mạ" Text="Xanh lá mạ"></asp:ListItem>
                                        <asp:ListItem Value="xanh lính" Text="Xanh lính"></asp:ListItem>
                                        <asp:ListItem Value="xanh lông công" Text="Xanh lông công"></asp:ListItem>
                                        <asp:ListItem Value="xanh môn" Text="Xanh môn"></asp:ListItem>
                                        <asp:ListItem Value="xanh ngọc" Text="Xanh ngọc"></asp:ListItem>
                                        <asp:ListItem Value="xanh rêu" Text="Xanh rêu"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Loại hàng
                                </div>
                                <div class="row-right">
                                    <asp:DropDownList ID="ddlPreOrder" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Hàng có sẵn" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Hàng order" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-row" id="Minimum">
                                <div class="row-left">
                                    Tồn kho ít nhất
                                </div>
                                <div class="row-right">
                                    <asp:TextBox type="number" min="0" value="3" ID="pMinimumInventoryLevel" runat="server" CssClass="form-control" placeholder="Số lượng tồn kho ít nhất"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row" id="Maximum">
                                <div class="row-left">
                                    Tồn kho nhiều nhất
                                </div>
                                <div class="row-right">
                                    <asp:TextBox type="number" min="0" value="10" ID="pMaximumInventoryLevel" runat="server" CssClass="form-control" placeholder="Số lượng tồn kho nhiều nhất"></asp:TextBox>
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
                                    Giá cũ chưa sale
                                </div>
                                <div class="row-right">
                                    <asp:TextBox type="number" min="0" autocomplete="off" ID="pOld_Price" runat="server" CssClass="form-control" placeholder="Giá sỉ cũ chưa sale"></asp:TextBox>
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
                                        MultipleFileSelection="Disabled" OnClientFileSelected="OnClientFileSelected1" 
                                        MaxFileInputsCount="1">
                                    </telerik:RadAsyncUpload>
                                    <asp:Image runat="server" ID="ProductThumbnail" Width="200" />
                                    <asp:HiddenField runat="server" ID="ListProductThumbnail" ClientIDMode="Static" />
                                    <div class="hidProductThumbnail"></div>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Hiện trang chủ
                                </div>
                                <div class="row-right">
                                    <asp:DropDownList ID="ddlShowHomePage" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Không" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Có" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Tags
                                </div>
                                <div class="row-right">
                                    <input type="text" id="txtTag" class="typeahead" data-role="tagsinput" />
                                    <div id="tagList"></div>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Nội dung
                                </div>
                                <div class="row-right">
                                    <telerik:RadEditor runat="server" ID="pContent" Width="100%" Height="500px" ToolsFile="~/FilesResources/ToolContent.xml" Skin="Metro" DialogHandlerUrl="~/Telerik.Web.UI.DialogHandler.axd" AutoResizeHeight="False" EnableResize="False">
                                        <ImageManager ViewPaths="~/uploads/images" UploadPaths="~/uploads/images" DeletePaths="~/uploads/images" />
                                    </telerik:RadEditor>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Thư viện ảnh
                                </div>
                                <div class="row-right">
                                    <telerik:RadAsyncUpload Skin="Metro" runat="server" ID="hinhDaiDien" ChunkSize="0"
                                        Localization-Select="Chọn ảnh" AllowedFileExtensions=".jpeg,.jpg,.png"
                                        MultipleFileSelection="Automatic" OnClientFileSelected="OnClientFileSelected1">
                                    </telerik:RadAsyncUpload>
                                    <asp:Image runat="server" ID="imgDaiDien" Width="200" />
                                    <asp:HiddenField runat="server" ID="listImg" ClientIDMode="Static" />
                                    <div class="hidImage"></div>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Ảnh đại diện sạch
                                </div>
                                <div class="row-right">
                                    <telerik:RadAsyncUpload Skin="Metro" runat="server" ID="ProductThumbnailImageClean" ChunkSize="5242880"
                                        Localization-Select="Chọn ảnh" AllowedFileExtensions=".jpeg,.jpg,.png"
                                        MultipleFileSelection="Disabled" OnClientFileSelected="OnClientFileSelected1" MaxFileInputsCount="1">
                                    </telerik:RadAsyncUpload>
                                    <asp:Image runat="server" ID="ProductThumbnailClean" Width="200" />
                                    <asp:HiddenField runat="server" ID="ListProductThumbnailClean" ClientIDMode="Static" />
                                    <div class="hidProductThumbnailClean"></div>
                                </div>
                            </div>
                            <div class="form-row variable" style="display: none">
                                <div class="row-left">
                                    Thuộc tính
                                </div>
                                <div class="row-right">
                                    <asp:UpdatePanel ID="up" runat="server">
                                        <ContentTemplate>
                                            <div class="variable-name-select">
                                                <asp:DropDownList runat="server" ID="ddlVariablename" CssClass="form-control" DataTextField="VariableName" DataValueField="ID" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlVariablename_SelectedIndexChanged" />
                                            </div>
                                            <div class="variable-value-select">
                                                <asp:DropDownList runat="server" ID="ddlVariableValue" CssClass="form-control select2" Width="100%" DataTextField="VariableValue" DataValueField="ID" AppendDataBoundItems="True" AutoPostBack="True" />
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div class="variable-button-select">
                                        <a href="javascript:;" onclick="chooseVariable()" class="btn primary-btn fw-btn not-fullwidth">Chọn</a>
                                    </div>

                                    <div class="variableselect">
                                        <span id="selectvariabletitle">Các thuộc tính đã chọn: 
                                            <a href="javascript:;" onclick="generateVariable()" id="generateVariable" class="btn primary-btn fw-btn not-fullwidth"><i class="fa fa-file-o" aria-hidden="true"></i> Thiết lập biến thể</a></span>
                                    </div>
                                    <div class="generat-variable-content">
                                        <div class="row">
                                            <div class="col-md-9">
                                                <h3>Danh sách biến thể</h3>
                                            </div>
                                            <div class="col-md-3 delete" style="display: none;">
                                                <a href="javascript:;" onclick="deleteAllVariable()" id="delete" class="btn primary-btn fw-btn not-fullwidth"><i class="fa fa-times" aria-hidden="true"></i> Xóa tất cả</a>
                                            </div>
                                        </div>
                                        <div class="row list-item-genred">
                                            <div class="col-md-12">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-row">
                                <a href="javascript:;" class="btn primary-btn fw-btn not-fullwidth" onclick="addNewProduct()"><i class="fa fa-pencil-square-o" aria-hidden="true"></i> Xuất bản</a>
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn primary-btn fw-btn not-fullwidth" Text="Xuất bản" OnClick="btnSubmit_Click" Style="display: none" />
                                <a href="/tat-ca-san-pham" class="btn primary-btn fw-btn not-fullwidth"><i class="fa fa-arrow-left" aria-hidden="true"></i> Trở về</a>
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
                                <a href="javascript:;" class="btn primary-btn fw-btn not-fullwidth" onclick="addNewProduct()"><i class="fa fa-pencil-square-o" aria-hidden="true"></i> Xuất bản</a>
                                <a href="/tat-ca-san-pham" class="btn primary-btn fw-btn not-fullwidth"><i class="fa fa-arrow-left" aria-hidden="true"></i> Trở về</a>
                             </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <asp:HiddenField ID="hdfTempVariable" runat="server" />
        <asp:HiddenField ID="hdfVariableFull" runat="server" />
        <asp:HiddenField ID="hdfVariableListInsert" runat="server" />
        <asp:HiddenField ID="hdfGiasi" runat="server" />
        <asp:HiddenField ID="hdfsetStyle" runat="server" />
        <asp:HiddenField ID="hdfMaximum" runat="server" />
        <asp:HiddenField ID="hdfMinimum" runat="server" />
        <asp:HiddenField ID="hdfParentID" runat="server" />
        <asp:HiddenField ID="hdfUserRole" runat="server" />
        <asp:HiddenField ID="hdfTags" runat="server" />
    </main>

    <telerik:RadCodeBlock runat="server">
        <script type="text/javascript" src="Scripts/bootstrap-tagsinput.min.js"></script>
        <script type="text/javascript" src="Scripts/typeahead.bundle.min.js"></script>
        <script type="text/javascript" src="Scripts/typeahead.jquery.js"></script>
        <script type="text/javascript">
            // init Input Tag
            let tags = new Bloodhound({
                datumTokenizer: (tag) => {
                    return Bloodhound.tokenizers.whitespace(tag.name);
                },
                queryTokenizer: Bloodhound.tokenizers.whitespace,
                remote: {
                    url: '/tao-san-pham.aspx/GetTags?tagName="%QUERY"',
                    filter: (response) => {
                        if (!response.d)
                            return {};

                        return $.map(response.d, function (item) {
                            return {
                                id: item.id,
                                name: item.name,
                                slug: item.slug,
                            };
                        });
                    },
                    ajax: {
                        type: "GET",
                        contentType: "application/json; charset=utf-8"
                    }
                }
            });
            tags.initialize();

            let txtTagDOM = $('#txtTag');
            txtTagDOM.tagsinput({
                itemValue: 'slug',
                itemText: 'name',
                trimValue: true,
                typeaheadjs: {
                    name: 'tags',
                    displayKey: 'name',
                    source: tags.ttAdapter()
                }
            });

            function initdropdown() {
                $("#<%=ddlVariableValue.ClientID%>").select2();

                if($("#<%=ddlVariablename.ClientID%>").val() != "0") {
                    if ($("#<%=ddlVariableValue.ClientID%>").val() != "0") {
                        chooseVariable();
                    }
                    $("#<%=ddlVariableValue.ClientID%>").select2("open");
                }
                else {
                    $("#<%=ddlVariableValue.ClientID%>").val("0");
                }
            }

            function pageLoad(sender, args) {
                initdropdown();
            }

            $(document).ready(function () {
                var userRole = $("#<%=hdfUserRole.ClientID%>").val();
                if (userRole != "0") {
                    $(".cost-of-goods").addClass("hide");
                }

                $("#<%=pRegular_Price.ClientID%>").blur(function () {
                    var categoryID = $("#<%=ddlCategory.ClientID%>").val();
                    var regularPrice = parseInt($("#<%=pRegular_Price.ClientID%>").val());
                    var cost = 0;
                    if (categoryID == 17 || categoryID == 18 || categoryID == 19 || categoryID == 47) {
                        cost = regularPrice - 25000;
                    }
                    else if (categoryID == 12) {
                        cost = regularPrice - 40000;
                    }
                    else 
                    {
                        cost = regularPrice - 25000;
                    }

                    $("input.cost-price").val(cost);
                });

                $("#<%=pRegular_Price.ClientID%>").blur(function () {
                    var regularPrice = parseInt($("#<%=pRegular_Price.ClientID%>").val());
                    var retailPrice = 0;
                    if (regularPrice < 60000)
                    {
                        retailPrice = regularPrice + 40000;
                    }
                    else if (regularPrice >= 60000 && regularPrice < 100000)
                    {
                        retailPrice = regularPrice + 50000;
                    }
                    else if (regularPrice >= 100000 && regularPrice < 140000)
                    {
                        retailPrice = regularPrice + 60000;
                    }
                    else if (regularPrice >= 140000 && regularPrice < 190000) {
                        retailPrice = regularPrice + 70000;
                    }
                    else if (regularPrice >= 190000) {
                        retailPrice = regularPrice + 80000;
                    }

                    $("#<%=pRetailPrice.ClientID%>").val(retailPrice);
                });

                $('input.sku-input').val(function () {
                    return this.value.toUpperCase();
                });

                // Handle unique name
                txtTagDOM.on('beforeItemAdd', function (event) {
                    // event.item: contains the item
                    // event.cancel: set to true to prevent the item getting added
                    let items = txtTagDOM.tagsinput('items') || [];
                    let exist = false;

                    items.forEach((tag) => {
                        if (tag.name === event.item.name) {
                            exist = true;
                            return false;
                        }
                    })

                    if (exist)
                        event.cancel = true;
                });

                // Handle short code taginput
                $(".bootstrap-tagsinput").find(".tt-input").keypress((event) => {
                    if (event.which === 13 || event.which === 44) {
                        if (event.which === 13)
                            event.preventDefault();

                        let target = event.target;
                        let tagName = target.value || "";

                        if (!tagName)
                            return;

                        let tagNameList = tagName.split(',');
                        let url = "";

                        if (tagNameList.length > 1) {
                            url += "/tao-san-pham.aspx/GetTagListByNameList?";
                            url += `&tagNameList=${JSON.stringify(tagNameList)}`;
                        }
                        else {
                            url += `/tao-san-pham.aspx/GetTags?tagName=${JSON.stringify(tagNameList[0])}`;
                        }

                        $.ajax({
                            headers: {
                                Accept: "application/json, text/javascript, */*; q=0.01",
                                "Content-Type": "application/json; charset=utf-8"
                            },
                            type: "GET",
                            url: url,
                            success: function (response) {
                                let data = response.d || [];

                                data.forEach((item) => {
                                    txtTagDOM.tagsinput('add', { id: item.id, name: item.name, slug: item.slug })
                                })

                                target.value = "";
                            }
                        })
                    }
                });

                
            });

            function redirectTo(ID) {
                window.location.href = "/xem-san-pham?id=" +ID;
            }

            function selectVariableValue() {
                setTimeout(function () {
                    $("#<%=ddlVariableValue.ClientID%>").select2("open");
                }, 200);
            };

            function chooseVariable() {
                var vName = $("#<%=ddlVariablename.ClientID%> option:selected").val();
                var vName_text = $("#<%=ddlVariablename.ClientID%> option:selected").text();
                var vValue = $("#<%=ddlVariableValue.ClientID%> option:selected").val();
                var vValue_text = $("#<%=ddlVariableValue.ClientID%> option:selected").text();
                if (vName != 0) {
                    if (vValue != 0) {
                        if ($(".variable-select").length > 0) {
                            var isExistParent = false;
                            $(".variable-select").each(function () {
                                var currentVname1 = $(this).attr("data-id");
                                if (currentVname1 == vName) {
                                    isExistParent = true;
                                }
                            });
                            if (isExistParent == true) {
                                $(".variable-select").each(function () {
                                    var currentVname = $(this).attr("data-id");
                                    if (currentVname == vName) {
                                        var vValueContentChild = $(this).find(".variablevalue");
                                        var vValueChild = $(this).find(".variablevalue").find(".variablevalue-item");
                                        if (vValueChild.length > 0) {
                                            var checkIsExist = false;
                                            vValueChild.each(function () {
                                                if ($(this).attr("data-valueid") == vValue) {
                                                    checkIsExist = true;
                                                }
                                            });
                                            if (checkIsExist == false) {
                                                var html = "";
                                                html += "<div class='variablevalue-item' data-valueid='" + vValue + "' data-valuename='" + vValue_text + "'>";
                                                html += "<span class='v-value'>" + vValue_text + "</span>";
                                                html += "<a href='javascript:;' class='btn primary-btn fw-btn not-fullwidth v-delete' onclick='deleteValueInGroup($(this))'>Xóa</a>";
                                                html += "</div>";
                                                vValueContentChild.append(html);
                                            }
                                            else {
                                                swal("Thông báo", "Bạn đã thêm giá trị này trước đó.", "error");
                                                $("#<%=ddlVariableValue.ClientID%>").focus();
                                            }
                                        }
                                    }
                                });
                            }
                            else {
                                var html = "";
                                html += "<div class='variable-select' data-name='" + vName_text + "' data-id='" + vName + "'>";
                                html += "   <div class='variablename' data-name='" + vName_text + "' data-id='" + vName + "'><strong>" + vName_text + "</strong><a href='javascript:;' style='float:right;margin-right:13px;' class='btn primary-btn fw-btn not-fullwidth v-delete' onclick='deleteGroup($(this))'>Xóa</a></div>";
                                html += "   <div class='variablevalue'>";
                                html += "       <div class='variablevalue-item' data-valueid='" + vValue + "' data-valuename='" + vValue_text + "'>";
                                html += "           <span class='v-value'>" + vValue_text + "</span>";
                                html += "           <a href='javascript:;' class='btn primary-btn fw-btn not-fullwidth v-delete' onclick='deleteValueInGroup($(this))'>Xóa</a>";
                                html += "       </div>";
                                html += "   </div>";
                                html += "</div>";
                                $(".variableselect").append(html);
                            }
                        }
                        else {
                            var html = "";
                            html += "<div class='variable-select' data-name='" + vName_text + "' data-id='" + vName + "'>";
                            html += "   <div class='variablename' data-name='" + vName_text + "' data-id='" + vName + "'><strong>" + vName_text + "</strong><a href='javascript:;' style='float:right;margin-right:13px;' class='btn primary-btn fw-btn not-fullwidth v-delete' onclick='deleteGroup($(this))'>Xóa</a></div>";
                            html += "   <div class='variablevalue'>";
                            html += "       <div class='variablevalue-item' data-valueid='" + vValue + "' data-valuename='" + vValue_text + "'>";
                            html += "           <span class='v-value'>" + vValue_text + "</span>";
                            html += "           <a href='javascript:;' class='btn primary-btn fw-btn not-fullwidth v-delete' onclick='deleteValueInGroup($(this))'>Xóa</a>";
                            html += "       </div>";
                            html += "   </div>";
                            html += "</div>";
                            $(".variableselect").append(html);
                        }
                        $("#selectvariabletitle").show();
                    }
                    else {
                        swal("Thông báo", "Hãy chọn một giá trị của thuộc tính.", "error");
                        $("#<%=ddlVariableValue.ClientID%>").focus();
                    }
                    
                }
                else {
                    swal("Thông báo", "Hãy chọn một thuộc tính sản phẩm.", "error");
                    $("#<%=ddlVariablename.ClientID%>").focus();
                }
            }

            function deleteValueInGroup(obj) {
                var c = confirm('Bạn muốn xóa giá trị này?');
                if (c == true) {
                    var root = obj.parent().parent().parent();
                    var parent_content = obj.parent().parent();
                    var valueContent = obj.parent();
                    valueContent.remove();
                    if (parent_content.find(".variablevalue-item").length == 0) {
                        root.remove();
                    }
                    if ($(".variable-select").length == 0) {
                        $("#selectvariabletitle").hide();
                    }
                }
            }

            function deleteGroup(obj) {
                var c = confirm('Bạn muốn xóa thuộc tính này?');
                if (c == true) {
                    obj.parent().parent().remove();
                    if ($(".variable-select").length == 0) {
                        $("#selectvariabletitle").hide();
                    }
                }
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
                    url: "/tao-san-pham.aspx/getParent",
                    data: "{parent:'" + parentID + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var data = JSON.parse(msg.d);
                        var html = "";
                        //var sl = "";
                        if (data.length > 0) {
                            html += "<select class='form-control slparent' style='margin-top:15px;' data-level='" + level + "' onchange='selectCategory($(this))'>";
                            html += "<option  value='0'>Chọn danh mục</option>";
                            for (var i = 0; i < data.length; i++) {
                                html += "<option value='" + data[i].ID + "'>" + data[i].CategoryName + "</option>";
                            }
                            html += "</select>";
                        }
                        $(".parent").append(html);
                    }
                });

                getTagList(parentID);
            }

            function getTagList(categoryID) {
                // clear old value
                $("#tagList").html("");

                var search = "";
                if (categoryID == 18) {
                    search = "đồ bộ";
                }
                else if (categoryID == 17) {
                    search = "đầm";
                }
                else if (categoryID == 3 || categoryID == 4 || categoryID == 5 || categoryID == 6) {
                    search = "áo thun nam";
                }
                else if (categoryID == 19) {
                    search = "áo thun nữ";
                }

                if (search != "") {
                    var url = `/tao-san-pham.aspx/GetTagList?tagName=${JSON.stringify(search)}`;

                    $.ajax({
                        headers: {
                            Accept: "application/json, text/javascript, */*; q=0.01",
                            "Content-Type": "application/json; charset=utf-8"
                        },
                        type: "GET",
                        url: url,
                        success: function (response) {
                            let data = response.d || [];

                            data.forEach((item) => {
                                $("#tagList").append("<span onclick='clickTagList(`" + item.name + "`)' class='tag-blue-click'>" + item.name + "</span>");
                            })
                        }
                    });
                }
            }

            function clickTagList(tagName) {

                let url = `/tao-san-pham.aspx/GetTags?tagName=${JSON.stringify(tagName)}`;

                $.ajax({
                    headers: {
                        Accept: "application/json, text/javascript, */*; q=0.01",
                        "Content-Type": "application/json; charset=utf-8"
                    },
                    type: "GET",
                    url: url,
                    success: function (response) {
                        let data = response.d || [];

                        data.forEach((item) => {
                            txtTagDOM.tagsinput('add', { id: item.id, name: item.name, slug: item.slug })
                        })
                    }
                })
            }

            function selectProductType() {
                var vl = $("#ddlProductStyle").val();
                $("#<%=hdfsetStyle.ClientID%>").val(vl);
                if (vl == 2) {
                    $(".variable").show();
                    $(".main-color").hide();
                }
                else {
                    $(".variable").hide();
                    $(".main-color").show();
                }
            }

            function generateVariable() {
                var giasi = $("#<%=pRegular_Price.ClientID%>").val();
                var giavon = $("#<%=pCostOfGood.ClientID%>").val();
                var giale = $("#<%=pRetailPrice.ClientID%>").val();
                var SKU = $("#<%=txtProductSKU.ClientID%>").val();

                var checkError = false;
                if (giasi == 0 || giavon == 0 || giale == 0 || isBlank(SKU)) {
                    checkError = true;
                }

                if (SKU == "") {
                    $("#<%=txtProductSKU.ClientID%>").focus();
                    swal("Thông báo", "Chưa nhập mã sản phẩm", "error");
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
                    swal("Thông báo", "Giá lẻ không được thấp hơn giá sỉ ", "error");
                }
                else {

                    if (checkError == true) {
                        swal("Thông báo", "Hãy nhập đầy đủ thông tin sản phẩm.", "error");
                    }
                    else {
                        HoldOn.open();
                        if ($(".variablevalue-item").length > 0) {
                            var parentnameid = "";
                            var listname = "";
                            var listname_id = "";
                            var listvalue = "";
                            var listvalue_id = "";

                            if ($(".variable-select").length > 0) {
                                $(".variable-select").each(function () {
                                    parentnameid += $(this).attr("data-name") + ":";
                                    $(this).find(".variablevalue").find(".variablevalue-item").each(function () {
                                        parentnameid += $(this).attr("data-valueid") + "-" + $(this).attr("data-valuename") + ";"
                                    });
                                    parentnameid += "|";
                                });
                            }
                            var parent = parentnameid.split('|');
                            parent.sort();
                            var parentlist = "";
                            for (var j = 1; j < parent.length; j++) {
                                parentlist += parent[j] + "|";
                            }
                        }


                        $.ajax({
                            type: "POST",
                            url: "/tao-san-pham.aspx/getVariable",
                            data: "{list:'" + parentlist + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (msg) {
                                var data = JSON.parse(msg.d);
                                if (data.length > 0) {
                                    var html = "";

                                    var numberCreated = 0;
                                    for (var i = 0; i < data.length; i++) {
                                        var item = data[i];
                                        var temp1 = item.ProductVariableName.split('|');
                                        var a = $("#<%=hdfsetStyle.ClientID%>").val();

                                        var checkExisted = false;
                                        $(".item-var-gen").each(function () {
                                            var existedID = $(this).attr("data-name-value");
                                            if (item.ProductVariable == existedID) {
                                                checkExisted = true;
                                            }
                                        });

                                        if (checkExisted == false) {

                                            html += "<div class='item-var-gen' data-name-id='" + item.VariableListValue + "|' data-value-id='" + item.VariableValue + "|' data-name-text='" + item.VariableNameList + "|' data-value-text='" + item.VariableValueName + "' data-name-value='" + item.ProductVariable + "'>";
                                            html += "	<div class='col-md-12 variable-label margin-bottom-15' onclick='showVariableContent($(this))'>";
                                            html += "<strong>#" + (i + 1) + "</strong>";
                                            for (var j = 0; j < temp1.length - 1; j++) {
                                                html += " - " + temp1[j] + "";
                                            }
                                            html += " - <span class='sku-input'>" + SKU + item.VariableSKUText + "</span>";
                                            html += "    </div>";
                                            html += "    <div class='col-md-12 variable-content show'>";
                                            html += "    	<div class='row'>";
                                            html += "		    <div class='col-md-2'>";
                                            html += "		    	<input type='file' class='productVariableImage upload-btn' onchange='imagepreview(this,$(this))' name='" + SKU + item.VariableSKUText + "'/>";
                                            html += "				<img class='imgpreview' onclick='openUploadImage($(this))' src='/App_Themes/Ann/image/placeholder.png' />";
                                            html += "				<a href='javascript:;' onclick='deleteImageVariable($(this))' class='btn-delete hide'><i class='fa fa-times' aria-hidden='true'></i> Xóa hình</a>";
                                            html += "			</div>";
                                            html += "		    <div class='col-md-5'>";
                                            html += "			    <div class='row margin-bottom-15'>";
                                            html += "			    	    <div class='col-md-5'>Mã sản phẩm</div>";
                                            html += "			    	    <div class='col-md-7'><input type='text' disabled class='form-control productvariablesku sku-input' value='" + SKU + item.VariableSKUText + "'></div>";
                                            html += "			    	</div>";
                                            html += "			    </div>";
                                            html += "		    <div class='col-md-5'>";
                                            html += "		    	<div class='row margin-bottom-15'>";
                                            html += "		    	    <div class='col-md-5'>Giá sỉ</div>";
                                            html += "		    	    <div class='col-md-7'><input class='form-control regularprice' type='text' value='" + giasi + "'> </div>";
                                            html += "		    	</div>";
                                            html += "		    	<div class='row margin-bottom-15 cost-of-goods'>";
                                            html += "		    	    <div class='col-md-5'>Giá vốn</div>";
                                            html += "		    	    <div class='col-md-7'><input class='form-control costofgood cost-price' type='text' value='" + giavon + "'></div>";
                                            html += "		    	</div>";
                                            html += "		    	<div class='row margin-bottom-15'>";
                                            html += "		    	    <div class='col-md-5'>Giá bán lẻ</div>";
                                            html += "		    	    <div class='col-md-7'><input class='form-control retailprice' type='text' value='" + giale + "'></div>";
                                            html += "		    	</div>";
                                            html += "		    	<div class='row margin-bottom-15'>";
                                            html += "		    	    <div class='col-md-5'></div>";
                                            html += "		    	    <div class='col-md-7'><a href='javascript:;' onclick='deleteVariableItem($(this))' class='btn primary-btn fw-btn not-fullwidth'>Xóa</a></div>";
                                            html += "		    	</div>";
                                            html += "			</div>";
                                            html += "		</div>";
                                            html += "	</div>";
                                            html += "</div>";
                                            numberCreated++;
                                        }
                                    }

                                    $(".list-item-genred > .col-md-12").append(html);
                                    $(".delete").show();

                                    if (data.length == numberCreated) {
                                        swal("Thông báo", "Đã tạo thành công " + numberCreated + " biến thể sản phẩm", "success");
                                    }
                                    else {
                                        if (numberCreated > 0) {
                                            swal("Thông báo", "Đã thêm thành công " + numberCreated + " biến thể còn thiếu. Những biến thể mới sẽ thêm vào dưới cùng.", "success");
                                        }
                                        else {
                                            swal("Thông báo", "Đã tạo đầy đủ " + data.length + " biến thể sản phẩm trước đó", "info");
                                        }
                                    }
                                    
                                    HoldOn.close();

                                    var userRole = $("#<%=hdfUserRole.ClientID%>").val();
                                    if (userRole != "0") {
                                        $(".cost-of-goods").addClass("hide");
                                    }
                                    $('input.sku-input').val(function () {
                                        return this.value.toUpperCase();
                                    })
                                }
                                $(".generat-variable-content").show();
                            },
                            error: function (xmlhttprequest, textstatus, errorthrow) {
                                alert('lỗi');
                            }
                        });
                    }
                }
            }

            function openUploadImage(obj) {
                obj.parent().find(".productVariableImage").click();
            }

            function deleteImageVariable(obj) {
                obj.parent().find(".imgpreview").attr("src", "/App_Themes/Ann/image/placeholder.png").attr("data-file-name", "/App_Themes/Ann/image/placeholder.png");
                obj.addClass("hide");
            }

            function showVariableContent(obj) {
                var content = obj.parent().find(".variable-content");
                if (content.is(":hidden")) {
                    content.addClass("show");
                    obj.addClass("margin-bottom-15");
                }
                else {
                    content.removeClass("show");
                    obj.removeClass("margin-bottom-15");
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

            function deleteVariableItem(obj) {
                var c = confirm("Bạn muốn xóa biến thể này?");
                if (c == true) {
                    obj.closest(".item-var-gen").remove();
                }
            }

            function deleteAllVariable() {
                var c = confirm("Bạn muốn xóa tất cả biến thể?");
                if (c == true) {
                    $(".list-item-genred").remove();
                    var html = "<div class=\"list-item-genred\"></div>";
                    $(".generat-variable-content").append(html);
                }
            }

            function addNewProduct() {
                
                var listv = "";
                var a = $("#<%= hdfsetStyle.ClientID%>").val();
                var parent = $("#<%=hdfParentID.ClientID%>").val();
                if (a == 2) {
                    var title = $("#<%=txtProductTitle.ClientID%>").val();
                    var SKU = $("#<%=txtProductSKU.ClientID%>").val();
                    var materials = $("#<%=txtMaterials.ClientID%>").val();
                    var maximum = $("#<%=pMaximumInventoryLevel.ClientID%>").val();
                    var minimum = $("#<%=pMinimumInventoryLevel.ClientID%>").val();
                    var giacu = $("#<%=pOld_Price.ClientID%>").val() || 0;
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
                        swal("Thông báo", "Giá sỉ không được thấp hơn giá vốn", "error");
                    }
                    else if (parseFloat(giacu) > 0 && parseFloat(giacu) < parseFloat(giasi)) {
                        $("#<%=pOld_Price.ClientID%>").focus();
                        swal("Thông báo", "Giá cũ chưa sale không được thấp hơn giá sỉ", "error");
                    }
                    else if (parseFloat(giasi) > parseFloat(giale)) {
                        $("#<%=pRetailPrice.ClientID%>").focus();
                        swal("Thông báo", "Giá lẻ không được thấp hơn giá sỉ", "error");
                    }
                    else {
                        HoldOn.open();
                        if ($(".item-var-gen").length > 0) {
                            var checkError = false;
                            var indexError = 0;
                            var inputError = "";
                            $(".item-var-gen").each(function (index) {
                                var productvariablesku = $(this).find(".productvariablesku").val();
                                var regularprice = $(this).find(".regularprice").val();
                                var costofgood = $(this).find(".costofgood").val();
                                var retailprice = $(this).find(".retailprice").val();

                                if (isBlank(productvariablesku) || isBlank(regularprice) || isBlank(costofgood) || isBlank(retailprice)) {
                                    checkError = true;
                                    indexError = index;
                                    if (isBlank(regularprice)) {
                                        inputError = ".regularprice";
                                    }
                                    else if (isBlank(costofgood)) {
                                        inputError = ".costofgood";
                                    }
                                    else if (isBlank(retailprice)) {
                                        inputError = ".retailprice";
                                    }
                                }
                                else {
                                    var datanameid = $(this).attr("data-name-id");
                                    var datavalueid = $(this).attr("data-value-id");
                                    var datanametext = $(this).attr("data-name-text");
                                    var datavaluetext = $(this).attr("data-value-text");
                                    var datanamevalue = $(this).attr("data-name-value");
                                    var image = $(this).find(".productVariableImage").attr("name");
                                    var StockStatus = 3;
                                    var checked = true;

                                    // nối chuỗi dữ liệu biến thể sản phẩm
                                    listv += datanameid + ";" + datavalueid + ";" + datanametext + ";" + datavaluetext + ";" + productvariablesku + ";" + regularprice.replace(",", "") + ";" + costofgood.replace(",", "") + ";" + retailprice.replace(",", "") + ";" + datanamevalue + ";" + maximum + ";" + minimum + ";" + StockStatus + ";" + checked + ",";
                                }
                            });

                            if (checkError == true) {
                                // focus đến input chưa nhập dữ liệu
                                $(".item-var-gen").eq(indexError).css("background-color", "#fff0c5");
                                $('html, body').animate({
                                    scrollTop: $(".item-var-gen").eq(indexError).offset().top - 150
                                }, 500);
                                $(".item-var-gen").eq(indexError).find(inputError).focus();
                                HoldOn.close();
                                swal("Thông báo", "Hãy nhập đầy đủ thông tin biến thể.", "error");
                            }
                            else {
                                // Insert tagID list into hdfTags
                                $("#<%=hdfTags.ClientID%>").val(JSON.stringify(txtTagDOM.tagsinput('items')));
                                $("#<%=hdfVariableListInsert.ClientID%>").val(listv);

                                $("#<%=btnSubmit.ClientID%>").click();
                            }
                        }
                        else {
                            HoldOn.close();
                            swal("Lỗi", "Chưa thiếp lập biến thể sản phẩm", "error");
                        }
                    }
                }
                else {
                    var title = $("#<%=txtProductTitle.ClientID%>").val();
                    var SKU = $("#<%=txtProductSKU.ClientID%>").val();
                    var materials = $("#<%=txtMaterials.ClientID%>").val();
                    var giacu = $("#<%=pOld_Price.ClientID%>").val() || 0;
                    var giasi = $("#<%=pRegular_Price.ClientID%>").val();
                    var giavon = $("#<%=pCostOfGood.ClientID%>").val();
                    var giale = $("#<%=pRetailPrice.ClientID%>").val();
                    var maincolor = $("#<%=ddlColor.ClientID%>").val();

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
                    else if (maincolor == "") {
                        $("#<%=ddlColor.ClientID%>").focus();
                        swal("Thông báo", "Chưa chọn màu chủ đạo", "error");
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
                    else if (parseFloat(giacu) > 0 && parseFloat(giacu) < parseFloat(giasi)) {
                        $("#<%=pOld_Price.ClientID%>").focus();
                        swal("Thông báo", "Giá cũ chưa sale không được thấp hơn giá sỉ", "error");
                    }
                    else if (parseFloat(giasi) > parseFloat(giale)) {
                        $("#<%=pRetailPrice.ClientID%>").focus();
                        swal("Thông báo", "Giá lẻ không được thấp hơn giá sỉ", "error");
                    }
                    else {
                        HoldOn.open();
                        if (!isBlank(title) && !isBlank(SKU) && !isBlank(materials) && !isBlank(giasi) && !isBlank(giavon) && !isBlank(giale)) {
                            // Insert tagID list into hdfTags
                            $("#<%=hdfTags.ClientID%>").val(JSON.stringify(txtTagDOM.tagsinput('items')));
                            $("#<%=hdfVariableListInsert.ClientID%>").val("");
                            $("#<%=btnSubmit.ClientID%>").click();
                        }
                        else {
                            HoldOn.close();
                            swal("Thông báo", "Hãy nhập đầy đủ thông tin sản phẩm.", "error");
                        }
                    }
                }
            }

            function isBlank(str) {
                return (!str || /^\s*$/.test(str));
            }

            function CheckSKU() {
                var sku = $("#<%=txtProductSKU.ClientID%>").val();
                $.ajax({
                    type: "POST",
                    url: "/tao-san-pham.aspx/CheckSKU",
                    data: "{SKU:'" + sku + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d != "ok") {
                            swal("Thông báo", "Mã sản phẩm đã tồn tại. Hãy kiểm tra lại!", "error");
                            $("#<%=txtProductSKU.ClientID%>").select().focus();
                            $("body").removeClass("stop-scrolling");
                        }
                    }
                });
            }

            function OnClientFileSelected1(sender, args) {
                if ($telerik.isIE) return;
                else {
                    truncateName(args);
                    var file = args.get_fileInputField().files.item(args.get_rowIndex());
                    //var file = args.get_fileInputField().files.item(0);
                    showThumbnail(file, args);
                }
            }

            function DelRow(that, link) {

                $(that).parent().parent().remove();
                var myHidden = $("#<%= listImg.ClientID %>");
                var tempF = myHidden.value;
                myHidden.value = tempF.replace(link, '');
            }
            (function (global, undefined) {
                var textBox = null;

                function textBoxLoad(sender) {
                    textBox = sender;
                }

                function OpenFileExplorerDialog() {
                    global.radopen("/Dialogs/Dialog.aspx", "ExplorerWindow");
                }

                //This function is called from a code declared on the Explorer.aspx page
                function OnFileSelected(fileSelected) {
                    if (textBox) {
                        {
                            var myHidden = document.getElementById('<%= listImg.ClientID %>');
                            var tempF = myHidden.value;

                            tempF = tempF + '#' + fileSelected;
                            myHidden.value = tempF;

                            $('.hidImage').append('<tr><td><img height="100px" src="' + fileSelected + '"/></td><td style="text-align:center"><a class="btn btn-success" onclick="DelRow(this,\'' + fileSelected + '\')">Xóa</a></td></li>');
                            //alert(fileSelected);
                            textBox.set_value(fileSelected);
                        }
                    }
                }

                global.OpenFileExplorerDialog = OpenFileExplorerDialog;
                global.OnFileSelected = OnFileSelected;
                global.textBoxLoad = textBoxLoad;
            })(window);

        </script>
    </telerik:RadCodeBlock>
</asp:Content>
