<%@ Page Title="Thêm mới nhân viên" MasterPageFile="~/MasterPage.Master" Language="C#" AutoEventWireup="true" CodeBehind="them-moi-nhan-vien.aspx.cs" Inherits="IM_PJ.them_moi_nhan_vien" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panelborderheading">
                        <div class="panel-heading clear">
                            <h3 class="page-title left not-margin-bot">Thêm mới nhân viên</h3>
                        </div>
                        <div class="panel-body">
                            <div class="form-row">
                                <asp:Label ID="lblError" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Tài khoản
                                    <asp:RequiredFieldValidator ID="rq" runat="server" ControlToValidate="txtUsername" ForeColor="Red" SetFocusOnError="true" ErrorMessage="(*)" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Tên đăng nhập"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Mật khẩu
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPassword" ForeColor="Red" SetFocusOnError="true" ErrorMessage="(*)" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Mật khẩu" TextMode="Password"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Xác nhận mật khẩu
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtConfirmPassword" ForeColor="Red" SetFocusOnError="true" ErrorMessage="(*)" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="cp" runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" ForeColor="Red" ErrorMessage="(không trùng với mật khẩu)" Display="Dynamic" SetFocusOnError="true"></asp:CompareValidator>
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Xác nhận mật khẩu"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-row">
                                <div class="row-left">
                                    Email
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtEmail" ForeColor="Red" ErrorMessage="(*)" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" ControlToValidate="txtEmail" ForeColor="Red" ErrorMessage="(Sai định dạng Email)" ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" Display="Dynamic" SetFocusOnError="true" />
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Họ tên
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFullname" ForeColor="Red" ErrorMessage="(*)" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtFullname" runat="server" CssClass="form-control" placeholder="Họ tên"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Điện thoại
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" placeholder="Số điện thoại"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Zalo
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtZalo" runat="server" CssClass="form-control" placeholder="Số điện thoại Zalo"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Facebook
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtFacebook" runat="server" CssClass="form-control" placeholder="Link Facebook"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Địa chỉ
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="Địa chỉ"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Giới tính
                                </div>
                                <div class="row-right">
                                    <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="1" Text="Nam"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Nữ"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Ngày sinh 
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rBirthday" ErrorMessage="(*)" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>
                                <div class="row-right">
                                    <telerik:RadDateTimePicker RenderMode="Lightweight" ID="rBirthday" ShowPopupOnFocus="true" Width="100%" runat="server" DateInput-CssClass="radPreventDecorate" placeholder="Ngày sinh" CssClass="date" DateInput-EmptyMessage="Ngày sinh">
                                        <DateInput DisplayDateFormat="dd/MM/yyyy" runat="server">
                                        </DateInput>
                                    </telerik:RadDateTimePicker>
                                </div>
                            </div>
                            <asp:Panel ID="pnAdmin" runat="server" Visible="false">
                                <div class="form-row">
                                    <div class="row-left">
                                        Nhóm nhân viên                                    
                                    </div>
                                    <div class="row-right">
                                        <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="1" Text="Nhân viên kho"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Nhân viên bán hàng"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="form-row">
                                <div class="row-left">
                                    Chi nhánh
                                </div>
                                <div class="row-right">
                                    <asp:DropDownList ID="ddlAgent" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Trạng thái
                                </div>
                                <div class="row-right">
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="1" Text="Hoạt động"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Khóa"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                </div>
                                <div class="row-right">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn primary-btn fw-btn not-fullwidth" Text="Tạo mới" OnClick="btnSubmit_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
    <style>
        #ContentPlaceHolder1_pnAdmin {
            margin-bottom: 15px;
            float: left;
            width: 100%;
        }
    </style>
</asp:Content>
