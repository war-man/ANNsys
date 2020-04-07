<%@ Page Title="Danh sách nhóm khách hàng" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="danh-sach-nhom-khach-hang.aspx.cs" Inherits="IM_PJ.danh_sach_nhom_khach_hang" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <h3 class="page-title left">Danh sách nhóm khách hàng</h3>
                    <div id="btnAddDiscountGroup" class="right above-list-btn">
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
                                        <th>Khách hàng</th>
                                        <th>Chiết khấu</th>
                                        <th>SL tối thiểu</th>
                                        <th>Phí đổi trả</th>
                                        <th>Số ngày tối đa</th>
                                        <th>Số lượng đổi/số ngày</th>
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

        <asp:HiddenField ID="hdfPermittedEdit" runat="server" />
        <script type="text/javascript" src="/App_Themes/Ann/js/controllers/danh-sach-nhom-khach-hang/danh-sach-nhom-khach-hang-controller.js"></script>
        <script type="text/javascript" src="/App_Themes/Ann/js/pages/danh-sach-nhom-khach-hang/danh-sach-nhom-khach-hang.js"></script>
    </main>
</asp:Content>
