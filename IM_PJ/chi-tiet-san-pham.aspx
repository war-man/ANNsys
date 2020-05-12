<%@ Page Language="C#" Title="Chi tiết sản phẩm" AutoEventWireup="true" MasterPageFile="~/ProductPage.Master" CodeBehind="chi-tiet-san-pham.aspx.cs" Inherits="IM_PJ.chi_tiet_san_pham" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .main-block {
            margin: auto;
            float: inherit;
        }
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

        .bootstrap-tagsinput {
            width: 100%;
        }
        
        .bootstrap-tagsinput .label {
            font-size: 100%;
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
    <main class="col-md-8 col-xs-12 main-block">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panelborderheading">
                        <div class="panel-heading clear">
                            <h3 class="page-title left not-margin-bot"><asp:Literal ID="ltrProductName" runat="server"></asp:Literal></h3>
                        </div>
                        <div class="panel-body">
                            <div class="form-row">
                                <asp:Literal ID="ltrEdit1" runat="server"></asp:Literal>
                            </div>
                            <div class="form-row">
                                🔰 Danh mục: <asp:Literal ID="ltrCategory" runat="server"></asp:Literal>
                            </div>
                            <div class="form-row">
                                🔰 Mã sản phẩm: <asp:Literal ID="ltrSKU" runat="server"></asp:Literal>
                            </div>
                            <div class="form-row">
                                🔰 Chất liệu: <asp:Literal ID="ltrMaterials" runat="server"></asp:Literal>
                            </div>
                            <div class="form-row">
                                🔰 Tổng số lượng: <asp:Literal ID="ltrStock" runat="server"></asp:Literal>
                            </div>
                            <div class="form-row">
                                🔰 Trạng thái: <asp:Literal ID="ltrStockStatus" runat="server"></asp:Literal>
                            </div>
                            <div class="form-row">
                                🔰 Giá sỉ: <asp:Literal ID="ltrRegularPrice" runat="server"></asp:Literal>
                            </div>
                            <div class="form-row">
                                🔰 Giá lẻ: <asp:Literal ID="ltrRetailPrice" runat="server"></asp:Literal>
                            </div>
                            <div class="form-row">
                                <asp:Literal ID="pContent" runat="server"></asp:Literal>
                            </div>
                            <div class="form-row">
                                <asp:Literal ID="imageGallery" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row tableview">
                <div class="col-md-12">
                    <div class="panel-table clear">
                        <div class="responsive-table">
                            <table class="table table-checkable table-product all-variable-table">
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
        <asp:HiddenField ID="hdfTags" runat="server" />
    </main>

    <script src="/App_Themes/Ann/js/copy-product-info.js?v=09052020"></script>
    <script src="/App_Themes/Ann/js/download-product-image.js?v=09052020"></script>
</asp:Content>
