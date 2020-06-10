<%@ Page Title="Sửa thông tin nhà xe" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="sua-thong-tin-nha-xe.aspx.cs" Inherits="IM_PJ.sua_thong_tin_nha_xe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panelborderheading">
                        <div class="panel-heading clear">
                            <h3 class="page-title left not-margin-bot">Chỉnh sửa thông tin nhà xe</h3>
                        </div>
                        <div class="panel-body">
                            <div class="form-row">
                                <div class="row-left">
                                    Tên nhà xe
                                    <asp:RequiredFieldValidator ID="rqfCompanyName" runat="server" ControlToValidate="txtCompanyName" ForeColor="Red"
                                        SetFocusOnError="true" ErrorMessage="(*)" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control capitalize" TabIndex="1"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Điện thoại nơi gửi
                                    <asp:RequiredFieldValidator ID="rqfCompanyPhone" runat="server" ControlToValidate="txtCompanyPhone" ForeColor="Red"
                                        SetFocusOnError="true" ErrorMessage="(*)" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtCompanyPhone" runat="server" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Địa chỉ nơi gửi
                                    <asp:RequiredFieldValidator ID="rqfCompanyAddress" runat="server" ControlToValidate="txtCompanyAddress" ForeColor="Red"
                                        SetFocusOnError="true" ErrorMessage="(*)" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtCompanyAddress" runat="server" CssClass="form-control capitalize" TabIndex="3"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Trả cước
                                </div>
                                <div class="row-right">
                                    <asp:RadioButtonList ID="rdbPrepay" runat="server" RepeatDirection="Horizontal" TabIndex="4">
                                        <asp:ListItem Value="true">Trả trước</asp:ListItem>
                                        <asp:ListItem Value="false" Selected="True">Trả sau</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Thu hộ
                                </div>
                                <div class="row-right">
                                    <asp:RadioButtonList ID="rdbCOD" runat="server" RepeatDirection="Horizontal" TabIndex="5">
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
                                    <asp:TextBox ID="pNote" runat="server" CssClass="form-control" TabIndex="6"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <asp:Button ID="btnUpdate" runat="server" CssClass="btn primary-btn fw-btn not-fullwidth" Text="Cập nhật" OnClick="btnUpdate_Click" TabIndex="7" />
                                <a href="/danh-sach-nha-xe" class="btn primary-btn fw-btn not-fullwidth" tabindex="8">Trở về</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hdfID" runat="server" />
        </div>
    </main>

</asp:Content>
