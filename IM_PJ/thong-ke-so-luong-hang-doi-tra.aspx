<%@ Page Title="Đổi trả hàng" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="thong-ke-so-luong-hang-doi-tra.aspx.cs" Inherits="IM_PJ.thong_ke_so_luong_hang_doi_tra" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <h3 class="page-title left">Thống kê số lượng hàng đổi trả</h3>
                    <div class="right above-list-btn">
                        <a href="/bao-cao" class="h45-btn btn" style="background-color: #ff3f4c">Trở về</a>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="filter-above-wrap clear">
                        <div class="filter-wrapper">
                            <div class="filter-above-wrap clear">
                               <%-- <div>
                                    <label>Loại đổi trả</label>
                                    <span class="inline">
                                        <asp:DropDownList ID="ddlRestType" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="0" Text=""></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Có tính phí"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Không tính phí"></asp:ListItem>

                                        </asp:DropDownList>
                                    </span>
                                </div>--%>
                                <div class="col-md-6">
                                    <label>Từ ngày</label>
                                    <telerik:RadDatePicker RenderMode="Lightweight" ID="rFromDate" ShowPopupOnFocus="true" Width="100%" runat="server"
                                        DateInput-CssClass="radPreventDecorate" MinDate="01/01/1900">
                                        <DateInput DisplayDateFormat="dd/MM/yyyy" runat="server">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </div>

                                <div class="col-md-6">
                                    <label>Đến ngày</label>
                                    <telerik:RadDatePicker RenderMode="Lightweight" ID="rToDate" ShowPopupOnFocus="true" Width="100%" runat="server"
                                        DateInput-CssClass="radPreventDecorate" MinDate="01/01/1900">
                                        <DateInput DisplayDateFormat="dd/MM/yyyy" runat="server">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </div>
                            </div>
                            <div class="action-btns text-center">
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn green" Text="Tìm kiếm" OnClick="btnSearch_Click" />
                                <asp:Button ID="btnReset" runat="server" CssClass="btn green" Text="Làm lại" OnClick="btnReset_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="panel-table clear">
                        <div class="responsive-table">
                            <table class="table table-checkable table-product">
                                <tbody>
                                    <tr>

                                        <th>Tổng số lượng hàng đổi trả</th>
                                        <th>Tổng số lượng có tính phí</th>
                                        <th>Tổng số lượng không tính phí</th>
                                        
                                    </tr>
                                    <asp:Literal ID="ltrList" runat="server" EnableViewState="false"></asp:Literal>
                                </tbody>
                            </table>
                        </div>
                        <div class="panel-footer clear">
                            <div class="pagination">
                                <%--<%this.DisplayHtmlStringPaging1();%>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </main>
    <style>
        td {
            text-align:center;
        }
    </style>

</asp:Content>
