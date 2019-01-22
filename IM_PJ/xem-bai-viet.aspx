<%@ Page Language="C#" Title="Xem bài viết" AutoEventWireup="true" MasterPageFile="~/MasterPage.Master" CodeBehind="xem-bai-viet.aspx.cs" Inherits="IM_PJ.xem_bai_viet" %>

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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panelborderheading">
                        <div class="panel-heading clear">
                            <h3 class="page-title left not-margin-bot"><asp:Literal ID="ltrTitle" runat="server"></asp:Literal></h3>
                        </div>
                        <div class="panel-body">
                            <div class="form-row">
                                <asp:Literal ID="ltrContent" runat="server"></asp:Literal>
                            </div>
                            <div class="form-row">
                                <asp:Literal ID="imageGallery" runat="server"></asp:Literal>
                            </div>
                            <div class="form-row">
                                <asp:Literal ID="ltrEdit" runat="server"></asp:Literal>
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

    <script src="/App_Themes/Ann/js/copy-post-info.js?v=2011"></script>
    <script src="/App_Themes/Ann/js/download-post-image.js?v=2011"></script>
</asp:Content>
