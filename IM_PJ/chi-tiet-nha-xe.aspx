<%@ Page Title="Thông tin nhà xe" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="chi-tiet-nha-xe.aspx.cs" Inherits="IM_PJ.chi_tiet_nha_xe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <h3 class="page-title left">Danh sách nơi nhận</h3>
                    <div class="right above-list-btn">
                        <a href="/them-moi-noi-den-nha-xe/?id=<%=hdfID.Value%>" class="h45-btn primary-btn btn">Thêm nơi nhận</a>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="panel panelborderheading">
                        <div class="panel-heading clear">
                            <h3 class="page-title left not-margin-bot">Thông tin nhà xe</h3>
                        </div>
                        <div class="panel-body">
                            <div class="form-row">
                                <div class="row-left">
                                    Tên nhà xe
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control capitalize" Enabled="false" TabIndex="1"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Điện thoại
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtCompanyPhone" runat="server" CssClass="form-control" Enabled="false" TabIndex="2"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Địa chỉ chành xe gửi ở TPHCM
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtCompanyAddress" runat="server" CssClass="form-control capitalize" Enabled="false" TabIndex="3"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Trả cước
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtPrepay" runat="server" CssClass="form-control" Enabled="false" TabIndex="3"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Thu hộ
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtCOD" runat="server" CssClass="form-control" Enabled="false" TabIndex="3"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Ghi chú
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtNote" runat="server" CssClass="form-control" Enabled="false" TabIndex="4"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Literal ID="ltrEditButton" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="panel-table clear">
                        <div class="panel-footer clear">
                            <div class="pagination">
                                <%this.DisplayHtmlStringPaging1();%>
                            </div>
                        </div>
                        <div class="responsive-table">
                            <table class="table table-checkable table-product">
                                <tbody>
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
            <br />
            <div class="row">
                <div class="col-md-12">
                    <div class="form-row">
                        <a href="/danh-sach-nha-xe" class="btn primary-btn fw-btn not-fullwidth" tabindex="10">Trở về</a>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hdfID" runat="server" />
        </div>
    </main>

</asp:Content>
