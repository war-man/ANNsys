<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="trang-chu.aspx.cs" Inherits="IM_PJ.trang_chu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <h3 class="page-title">Xin chào!</h3>
            <div class="row">
                <div class="col-md-12">
                    <div class="panel-table clear">
                        <div class="responsive-table">
                            <asp:Literal ID="ltrList" runat="server" EnableViewState="false"></asp:Literal>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
    <style>
        .fixed-table-body {
            overflow-x: hidden;
            overflow-y: hidden;
            height: 100%;
        }
    </style>
</asp:Content>
