<%@ Page Title="Danh sách thuộc tính sản phẩm" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="quan-ly-danh-muc-thuoc-tinh.aspx.cs" Inherits="IM_PJ.quan_ly_danh_muc_thuoc_tinh" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <h3 class="page-title left">Danh sách thuộc tính sản phẩm</h3>
                    <div class="right above-list-btn">
                        <a href="/them-moi-danh-muc-thuoc-tinh" class="h45-btn primary-btn btn">Thêm mới</a>

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
                                        <th>Thuộc tính</th>
                                        <th>Ẩn</th>
                                        <th>Ngày tạo</th>
                                        <th></th>
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
