<%@ Page Title="Thống kê số lượng sản phẩm bán ra" Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.Master" CodeBehind="thong-ke-so-luong-san-pham-ban-ra.aspx.cs" Inherits="IM_PJ.thong_ke_so_luong_san_pham_ban_ra" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <h3 class="page-title left">Thống kê số lượng sản phẩm bán ra</h3>
                     <div class="right above-list-btn">
                        <a href="/bao-cao" class="h45-btn btn" style="background-color: #ff3f4c">Trở về</a>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="filter-above-wrap clear">
                        <div style="float: right!important">
                            <div class="filter-control right">
                                <div class="col-md-5">
                                </div>
                                <div class="col-md-3">
                                    <label>Từ ngày</label>
                                    <telerik:RadDatePicker RenderMode="Lightweight" ID="rFromDate" ShowPopupOnFocus="true" Width="100%" runat="server"
                                        DateInput-CssClass="radPreventDecorate" MinDate="01/01/1900">
                                        <DateInput DisplayDateFormat="dd/MM/yyyy" runat="server">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </div>
                                <div class="col-md-3">
                                    <label>Đến ngày</label>
                                    <telerik:RadDatePicker RenderMode="Lightweight" ID="rToDate" ShowPopupOnFocus="true" Width="100%" runat="server"
                                        DateInput-CssClass="radPreventDecorate" MinDate="01/01/1900">
                                        <DateInput DisplayDateFormat="dd/MM/yyyy" runat="server">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </div>
                                <div class="col-md-1">
                                    <a href="javascript:;" onclick="searchAgent()" class="btn primary-btn h45-btn"><i class="fa fa-search"></i></a>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn primary-btn h45-btn" OnClick="btnSearch_Click" Style="display: none" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="panel-table clear">
                        <div class="row margin-bottom-15">
                            <div class="col-md-2">
                                <div class="report-column">
                                    <div class="report-label">
                                        SL còn lại:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrTotalRemain" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="report-column">
                                    <div class="report-label">
                                        SL còn lại mỗi ngày:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrAverageTotalRemain" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="report-column">
                                    <div class="report-label">
                                        SL bán ra:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrTotalSales" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="report-column">
                                    <div class="report-label">
                                        SL bán ra mỗi ngày:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrAverageTotalSales" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="report-column">
                                    <div class="report-label">
                                        SL đổi trả:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrTotalRefund" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="report-column">
                                    <div class="report-label">
                                        SL đổi trả mỗi ngày:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrAverageTotalRefund" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row margin-bottom-15">
                            <div class="col-md-2">
                                <div class="report-column">
                                    <div class="report-label">
                                        TB lợi nhuận mỗi cái:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrAverageProfitPerProduct" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
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
