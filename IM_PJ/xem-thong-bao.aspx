<%@ Page Language="C#" Title="Xem thông báo" AutoEventWireup="true" MasterPageFile="~/MasterPage.Master" CodeBehind="xem-thong-bao.aspx.cs" Inherits="IM_PJ.xem_thong_bao" %>

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
        img {
            width: inherit;
            margin: 10px auto;
            max-width: 100%;
            height: auto;
        }
        .post-content {
            font-size: 18px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="row">
                <div class="col-md-3">
                    <div class="panel panelborderheading">
                        <div class="panel-heading clear">
                            <h3 class="page-title left not-margin-bot">Tóm tắt</h3>
                        </div>
                        <div class="panel-body">
                            <div class="form-row">
                                <asp:Literal ID="ltrThumbnail" runat="server"></asp:Literal>
                                <asp:Literal ID="ltrSummary" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="panel panelborderheading">
                        <div class="panel-heading clear">
                            <h3 class="page-title left not-margin-bot"><asp:Literal ID="ltrTitle" runat="server"></asp:Literal></h3>
                        </div>
                        <div class="panel-body">
                            <div class="form-row post-content">
                                <asp:Literal ID="ltrLink" runat="server"></asp:Literal>
                                <asp:Literal ID="ltrContent" runat="server"></asp:Literal>
                            </div>
                            <div class="form-row">
                                <asp:Literal ID="ltrEditBottom" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="panel panelborderheading">
                        <div class="panel-heading clear">
                            <h3 class="page-title left not-margin-bot">Thao tác</h3>
                        </div>
                        <div class="panel-body">
                            <div class="form-row">
                                <asp:Literal ID="ltrPostInfo" runat="server"></asp:Literal>
                                <asp:Literal ID="ltrEditTop" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
