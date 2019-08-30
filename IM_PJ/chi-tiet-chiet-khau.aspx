<%@ Page Title="Chi tiết chiết khấu" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="chi-tiet-chiet-khau.aspx.cs" Inherits="IM_PJ.chi_tiet_chiet_khau" %>

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
                            <h3 class="page-title left not-margin-bot">Chi tiết chiết khấu</h3>
                        </div>
                        <div class="panel-body">
                            <div class="form-row">
                                <asp:Label ID="lblError" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Số lượng sản phẩm
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" placeholder="Số lượng sản phẩm"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Số tiền chiết khấu mỗi sản phẩm
                                    
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtDiscountPerProduct" runat="server" CssClass="form-control" placeholder="Số tiền chiết khấu mỗi sản phẩm"></asp:TextBox>
                                </div>
                            </div>


                            <div class="form-row">
                                <asp:Button ID="btnLogin" runat="server" CssClass="btn primary-btn fw-btn not-fullwidth" Text="Cập nhật" OnClick="btnLogin_Click" />
                                <a href="/danh-sach-chiet-khau" class="btn primary-btn fw-btn not-fullwidth">Trở về</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
