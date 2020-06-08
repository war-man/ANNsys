<%@ Page Title="Thêm mới nơi nhận nhà xe" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="them-moi-noi-den-nha-xe.aspx.cs" Inherits="IM_PJ.them_moi_noi_den_nha_xe" %>
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
                            <h3 class="page-title left not-margin-bot">Thêm mới nơi nhận</h3>
                        </div>
                        <div class="panel-body">
                            <div class="form-row">
                                <asp:Label ID="lblError" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                            </div>
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
                                    Điện thoại nơi gửi
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtCompanyPhone" runat="server" CssClass="form-control" Enabled="false" TabIndex="2"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Địa chỉ nơi gửi
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtCompanyAddress" runat="server" CssClass="form-control capitalize" Enabled="false" TabIndex="3"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Nơi nhận
                                    <asp:RequiredFieldValidator ID="rqfShipTo" runat="server" ControlToValidate="txtShipTo" ForeColor="Red"
                                        SetFocusOnError="true" ErrorMessage="(*)" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtShipTo" runat="server" CssClass="form-control capitalize" placeholder="Gửi đến" TabIndex="4"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Địa chỉ nơi nhận
                                    <asp:RequiredFieldValidator ID="rqfAddress" runat="server" ControlToValidate="txtAddress" ForeColor="Red"
                                        SetFocusOnError="true" ErrorMessage="(*)" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control capitalize" placeholder="Địa chỉ chành xe nhận" TabIndex="5"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Trả cước
                                </div>
                                <div class="row-right">
                                    <asp:RadioButtonList ID="rdbPrepay" runat="server" RepeatDirection="Horizontal" TabIndex="6">
                                        <asp:ListItem Value="true">Trả trước</asp:ListItem>
                                        <asp:ListItem Value="false">Trả sau</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Thu hộ
                                </div>
                                <div class="row-right">
                                    <asp:RadioButtonList ID="rdbCOD" runat="server" RepeatDirection="Horizontal" TabIndex="7">
                                        <asp:ListItem Value="true">Có</asp:ListItem>
                                        <asp:ListItem Value="false" Selected="True">Không</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Ghi chú
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="pNote" runat="server" CssClass="form-control" TabIndex="8"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <asp:Button ID="btnAdd" runat="server" CssClass="btn primary-btn fw-btn not-fullwidth" Text="Tạo mới" OnClick="btnAdd_Click" TabIndex="9" />
                                <a href="/chi-tiet-nha-xe?id=<%=hdfID.Value%>" class="btn primary-btn fw-btn not-fullwidth" tabindex="10">Trở về</a>
                            </div>
                            <asp:HiddenField ID="hdfID" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
