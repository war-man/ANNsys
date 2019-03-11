<%@ Page Title="Danh sách khách giảm giá" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="danh-sach-khach-giam-gia.aspx.cs" Inherits="IM_PJ.danh_sach_khach_giam_gia" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panelborderheading">
                        <div class="panel-heading clear">
                            <h3 class="page-title left not-margin-bot">Thêm khách hàng vào nhóm: <strong><asp:Literal ID="ltrGroupName" runat="server" EnableViewState="false"></asp:Literal></strong></h3>
                        </div>
                        <div class="panel-body">
                            <div class="form-row">
                                <div class="row-left">
                                    Chọn khách hàng
                                </div>
                                <div class="row-right">
                                    <asp:DropDownList ID="ddlCustomer" runat="server" CssClass="form-control customerlist select2" Width="50%" Height="40px">
                                    </asp:DropDownList>
                                    <asp:Button ID="btnLogin" runat="server" CssClass="btn primary-btn fw-btn not-fullwidth" Text="Thêm" OnClick="btnLogin_Click" />
                                    <a href="/danh-sach-nhom-khach-hang" class="btn primary-btn fw-btn not-fullwidth">Trở về</a>
                                </div>
                            </div>
                        </div>
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
                                        <th>Họ tên</th>
                                        <th>Nick</th>
                                        <th>Điện thoại</th>
                                        <th>Nhân viên</th>
                                        <th>Ngày vào nhóm</th>
                                        <th>Thao tác</th>
                                    </tr>
                                    <asp:Literal ID="ltrList" runat="server" EnableViewState="false"></asp:Literal>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hdfCustomerID" runat="server" />
        <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Style="display: none" />
    </main>
    <script type="text/javascript">
        function deletecustomer(obj) {
            var c = confirm("Bạn muốn xóa khách hàng này ra khỏi nhóm?");
            if (c == true) {
                var id = obj.parent().parent().attr("data-id");
                $("#<%=hdfCustomerID.ClientID%>").val(id);
                $("#<%=btnDelete.ClientID%>").click();
            }
        }
    </script>
</asp:Content>
