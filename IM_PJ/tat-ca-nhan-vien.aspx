<%@ Page Title="Danh sách nhân viên" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="tat-ca-nhan-vien.aspx.cs" Inherits="IM_PJ.tat_ca_nhan_vien" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <h3 class="page-title left">Danh sách nhân viên
                        <asp:Label ID="lblAgentName" runat="server"></asp:Label>
                    </h3>
                    <div class="right above-list-btn">
                        <asp:Literal ID="ltrback" runat="server"></asp:Literal>
                        <asp:Literal ID="ltrAddnew" runat="server"></asp:Literal>
                        <a href="/them-moi-nhan-vien" class="h45-btn primary-btn btn">Thêm mới</a>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="filter-above-wrap clear">
                        <div class="filter-control">
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtAgentName" runat="server" CssClass="form-control" placeholder="Tìm nhân viên"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlAgent" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0" Text="Nhóm nhân viên"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Nhân viên kho"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Nhân viên bán hàng"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-1">
                                    <a href="javascript:;" onclick="searchAgent()" class="btn primary-btn h45-btn"><i class="fa fa-search"></i></a>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn primary-btn h45-btn" OnClick="btnSearch_Click" Style="display: none" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel-table clear">
                        <div class="responsive-table">
                            <table class="table table-checkable table-product">
                                <tbody>
                                    <tr>
                                        <th>Tài khoản</th>
                                        <th>Họ tên</th>
                                        <th>Email</th>
                                        <th>Nhóm</th>
                                        <th>Chi nhánh</th>
                                        <th>Trạng thái</th>
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
        <script type="text/javascript">
            function searchAgent() {
                $("#<%= btnSearch.ClientID%>").click();
            }
        </script>
    </main>
</asp:Content>
