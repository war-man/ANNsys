<%@ Page Title="Danh sách nhóm khách hàng" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="danh-sach-nhom-khach-hang.aspx.cs" Inherits="IM_PJ.danh_sach_nhom_khach_hang" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <h3 class="page-title left">Danh sách nhóm khách hàng</h3>
                    <div class="right above-list-btn">
                        <a href="/them-moi-giam-gia" class="h45-btn primary-btn btn">Thêm mới</a>
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
                                        <th>Tên nhóm</th>
                                        <th>Chiết khấu</th>
                                        <th>Phí đổi hàng</th>
                                        <th>Số ngày đổi hàng tối đa</th>
                                        <th>Số lượng đổi tối đa/số ngày</th>
                                        <th>Ngày tạo</th>
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
        <script type="text/javascript">
            <%--function searchGroup() {
                $("#<%= btnSearch.ClientID%>").click();
            }--%>
        </script>
    </main>
</asp:Content>
