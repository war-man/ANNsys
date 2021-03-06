﻿<%@ Page Title="Giá trị thuộc tính sản phẩm" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="gia-tri-thuoc-tinh-san-pham.aspx.cs" Inherits="IM_PJ.gia_tri_thuoc_tinh_san_pham" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <h3 class="page-title left">
                        <asp:Label ID="lblCategoryName" runat="server"></asp:Label></h3>
                    <div class="right above-list-btn">
                        <asp:Literal ID="ltrBack" runat="server"></asp:Literal>
                        <asp:Literal ID="ltrAddnew" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="panel-table clear">
                        <div class="responsive-table">
                            <table class="table table-checkable table-product">
                                <tbody>
                                    <tr>
                                        <th>Mã sản phẩm</th>
                                        <th>Thuộc tính</th>
                                        <th>Giá trị</th>
                                        <th>Ngày tạo</th>
                                        <th>Ẩn</th>
                                        <th>Thao tác</th>
                                    </tr>
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
    </main>
</asp:Content>
