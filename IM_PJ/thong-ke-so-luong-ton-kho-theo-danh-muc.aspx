<%@ Page Title="Thống kê số lượng tồn kho theo danh mục" Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.Master" CodeBehind="thong-ke-so-luong-ton-kho-theo-danh-muc.aspx.cs" Inherits="IM_PJ.thong_ke_so_luong_ton_kho_theo_danh_muc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <h3 class="page-title left">Thống kê tồn kho</h3>

                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="panel-table clear">
                        <div class="responsive-table">
                            <table class="table table-checkable table-product">
                                <tbody>
                                    <tr>
                                        <th>Tổng tồn kho theo danh mục</th>
                                        <th>Tổng giá vốn tồn kho theo danh mục</th>
                                    </tr>
                                    <asp:Literal ID="ltrList" runat="server" EnableViewState="false"></asp:Literal>
                                    <asp:Literal ID="ltrTotalCost" runat="server" EnableViewState="false"></asp:Literal>
                                    <asp:Literal ID="ltrTotalProduct" runat="server" EnableViewState="false"></asp:Literal>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
