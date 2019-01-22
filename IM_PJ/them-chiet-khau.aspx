<%@ Page Title="Thêm chiết khấu" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="them-chiet-khau.aspx.cs" Inherits="IM_PJ.them_chiet_khau" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panelborderheading">
                        <div class="panel-heading clear">
                            <h3 class="page-title left not-margin-bot">Thêm mới chiết khấu</h3>
                        </div>
                        <div class="panel-body">
                            <div class="form-row">
                                <asp:Label ID="lblError" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Số lượng
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" placeholder="Số lượng sản phẩm"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-row">
                                <div class="row-left">
                                    Số tiền chiết khấu mỗi cái
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtDiscountPerProduct" runat="server" CssClass="form-control" placeholder="Số tiền chiến khấu mỗi cái"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-right">
                                    <div class="clear mar-top-3">
                                        <asp:Button ID="btnLogin" runat="server" CssClass="btn primary-btn fw-btn not-fullwidth" Text="Tạo mới" OnClick="btnLogin_Click" />
                                        <a href="/danh-sach-chiet-khau" class="btn primary-btn fw-btn not-fullwidth">Quay về danh sách</a>
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
