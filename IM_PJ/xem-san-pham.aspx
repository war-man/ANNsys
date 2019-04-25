<%@ Page Language="C#" Title="Xem sản phẩm" AutoEventWireup="true" MasterPageFile="~/MasterPage.Master" CodeBehind="xem-san-pham.aspx.cs" Inherits="IM_PJ.xem_san_pham" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .btn.download-btn {
            background-color: #000;
            color: #fff;
            border-radius: 0;
            font-size: 16px;
            text-transform: uppercase;
            width: 100%;
        }
        .btn.down-btn {
            background-color: #E91E63;
            color: #fff;
        }
        @media (max-width: 769px) {
            ul.image-gallery li {
                width: 100%;
            }
            .btn {
                width: 100%!important;
                float: left;
                margin-bottom: 10px;
                margin-left: 0;
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
                            <h3 class="page-title left not-margin-bot">Thông tin sản phẩm</h3>
                        </div>
                        <div class="panel-body">
                            <div class="form-row">
                                <asp:Literal ID="ltrEdit1" runat="server"></asp:Literal>
                            </div>
                            <div class="form-row">
                                <asp:Label ID="lblError" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Tên sản phẩm
                                </div>
                                <div class="row-right">
                                    <asp:Label ID="lbProductTitle" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Danh mục
                                </div>
                                <div class="row-right parent">
                                    <asp:DropDownList ID="ddlCategory" Enabled="false" runat="server" CssClass="form-control slparent" date-name="parentID" data-level="1" onchange="chooseParent($(this))">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Mã sản phẩm
                                </div>
                                <div class="row-right">
                                    <asp:Label ID="lblSKU" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Chất liệu                                    
                                </div>
                                <div class="row-right">
                                    <asp:Label ID="lbMaterials" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Số lượng
                                </div>
                                <div class="row-right">
                                    <asp:Label ID="lbProductStock" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Nhà cung cấp
                                </div>
                                <div class="row-right">
                                    <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="form-control" Enabled="False">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Trạng thái                                    
                                </div>
                                <div class="row-right">
                                    <asp:DropDownList ID="ddlStockStatus" runat="server" CssClass="form-control" Enabled="False">
                                        <asp:ListItem Text="Còn hàng" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Hết hàng" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Đang nhập hàng" Value="3"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Giá sỉ
                                </div>
                                <div class="row-right">
                                    <asp:Label ID="lbRegularPrice" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <asp:Literal ID="ltrCostOfGood" runat="server"></asp:Literal>
                            <div class="form-row">
                                <div class="row-left">
                                    Giá lẻ
                                </div>
                                <div class="row-right">
                                    <asp:Label ID="lbRetailPrice" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Nội dung
                                </div>
                                <div class="row-right">
                                    <div class="content-box">
                                        <asp:Literal ID="pContent" runat="server"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Thư viện ảnh
                                </div>
                                <div class="row-right">
                                    <asp:Literal ID="imageGallery" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div class="form-row">
                                <asp:Literal ID="ltrEdit2" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row tableview">
                <div class="col-md-12">
                    <div class="panel-table clear">
                        <div class="responsive-table">
                            <table class="table table-checkable table-product all-product-table">
                                <tbody>
                                    <asp:Literal ID="ltrList" runat="server" EnableViewState="false"></asp:Literal>
                                </tbody>
                            </table>
                        </div>
                        <div class="panel-footer clear">
                            <div class="pagination">
                                <%this.DisplayHtmlStringPaging1();%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <asp:HiddenField ID="hdfTempVariable" runat="server" />
        <asp:HiddenField ID="hdfVariableFull" runat="server" />
        <asp:HiddenField ID="hdfTable" runat="server" />
    </main>

    <script src="/App_Themes/Ann/js/copy-product-info.js?v=2011"></script>
    <script src="/App_Themes/Ann/js/sync-product.js?v=2211"></script>
    <script src="/App_Themes/Ann/js/download-product-image.js?v=2011"></script>
</asp:Content>
