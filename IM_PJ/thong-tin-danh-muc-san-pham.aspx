<%@ Page Title="Thông tin danh mục sản phẩm" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="thong-tin-danh-muc-san-pham.aspx.cs" Inherits="IM_PJ.thong_tin_danh_muc_san_pham" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panelborderheading">
                        <div class="panel-heading clear">
                            <h3 class="page-title left not-margin-bot">Thông tin danh mục sản phẩm</h3>
                        </div>
                        <div class="panel-body">
                            <div class="form-row">
                                <asp:Label ID="lblError" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Danh mục cha
                                </div>
                                <div class="row-right">
                                   <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Tên danh mục
                                    <asp:RequiredFieldValidator ID="rq" runat="server" ControlToValidate="txtCategoryName" ForeColor="Red" SetFocusOnError="true" ErrorMessage="(*)" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtCategoryName" runat="server" CssClass="form-control" placeholder="Tên danh mục"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Mô tả                            
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtCategoryDescription" runat="server" CssClass="form-control" placeholder="Mô tả" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Ẩn
                                </div>
                                <div class="row-right">
                                    <asp:CheckBox ID="chkIsHidden" runat="server" />
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                </div>
                                <div class="row-right">
                                    <div class="clear">
                                        <asp:Button ID="Button1" runat="server" CssClass="btn primary-btn fw-btn not-fullwidth" Text="Cập nhật" OnClick="btnLogin_Click" />
                                        <a href="/quan-ly-danh-muc-san-pham" class="btn primary-btn fw-btn not-fullwidth">Quay về danh sách</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
