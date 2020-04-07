<%@ Page Language="C#" Title="Nhân viên" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="thong-ke-nhan-vien.aspx.cs" Inherits="IM_PJ.thong_ke_nhan_vien" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <h3 class="page-title left">Thống kê nhân viên</h3>
                    <div class="right above-list-btn">
                        <a href="/bao-cao" class="h45-btn btn" style="background-color: #ff3f4c">Trở về</a>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="filter-above-wrap clear">
                        <div class="filter-control">
                            <div class="row">
                                <div class="col-md-5">
                                    <asp:DropDownList runat="server" ID="ddlAccountInfo" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <label>Từ ngày</label>
                                    <telerik:RadDatePicker RenderMode="Lightweight" ID="rFromDate" ShowPopupOnFocus="true" Width="100%" runat="server" DateInput-CssClass="radPreventDecorate" MinDate="01/01/2018">
                                        <DateInput DisplayDateFormat="dd/MM/yyyy" runat="server">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </div>
                                <div class="col-md-3">
                                    <label>Đến ngày</label>
                                    <telerik:RadDatePicker RenderMode="Lightweight" ID="rToDate" ShowPopupOnFocus="true" Width="100%" runat="server" DateInput-CssClass="radPreventDecorate" MinDate="01/01/2018">
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
                                        Tổng sản lượng còn lại:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrTotalRemainQuantity" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="report-column">
                                    <div class="report-label">
                                        SL còn lại mỗi ngày:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrAverageRemainQuantity" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="report-column">
                                    <div class="report-label">
                                        Tổng sản lượng bán ra:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrTotalSoldQuantity" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="report-column">
                                    <div class="report-label">
                                        SL bán ra mỗi ngày:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrAverageSoldQuantity" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="report-column">
                                    <div class="report-label">
                                        Tổng lượng hàng đổi trả:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrTotalRefundQuantity" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="report-column">
                                    <div class="report-label">
                                        SL đổi trả mỗi ngày:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrAverageRefundQuantity" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row margin-bottom-15">
                            <div class="col-md-2">
                                <div class="report-column">
                                    <div class="report-label">
                                        Tổng doanh số:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrTotalRevenue" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="report-column">
                                    <div class="report-label">
                                        Doanh số mỗi ngày:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrAverageRevenue" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="report-column">
                                    <div class="report-label">
                                        Tổng lợi nhuận:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrTotalProfit" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="report-column">
                                    <div class="report-label">
                                        Phần trăm sản lượng:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrQuantityPercentOfSystem" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="report-column">
                                    <div class="report-label">
                                        Phần trăm lợi nhuận:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrProfitPercentOfSystem" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="report-column">
                                    <div class="report-label">
                                        Tổng số khách mới:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrTotalNewCustomer" runat="server" EnableViewState="false"></asp:Literal>
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
