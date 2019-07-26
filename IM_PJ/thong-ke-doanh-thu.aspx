<%@ Page Language="C#" Title="Doanh thu" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="thong-ke-doanh-thu.aspx.cs" Inherits="IM_PJ.thong_ke_doanh_thu" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="/App_Themes/Ann/js/Chart.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <h3 class="page-title left">Thống kê doanh thu</h3>
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
                        <div class="row">
                            <div class="col-md-2">
                                <div class="report-column">
                                    <div class="report-label">
                                        Tổng số đơn hàng:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrTotalNumberOfOrder" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="report-column">
                                    <div class="report-label">
                                        Số đơn hàng mỗi ngày:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrNumberOfOrderPerDay" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="report-column">
                                    <div class="report-label">
                                        Tổng doanh thu:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrTotalRevenue" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="report-column">
                                    <div class="report-label">
                                        Doanh thu mỗi ngày:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrAverageRevenue" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="report-column">
                                    <div class="report-label">
                                        Doanh thu mỗi đơn hàng:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrRevenuePerOrder" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row margin-bottom-15">
                            <div class="col-md-12">
                                <canvas id="canvas"></canvas>
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
        <asp:Literal ID="ltrChartData" runat="server" EnableViewState="false"></asp:Literal>
        <script>
            window.onload = function () {
                if (typeof lineChartData !== 'undefined')
                {
                    var ctx = document.getElementById('canvas').getContext('2d');
                    window.myLine = Chart.Line(ctx, {
                        data: lineChartData,
                        options: {
                            responsive: true,
                            aspectRatio: 3.5,
                            hoverMode: 'index',
                            stacked: false,
                            title: {
                                display: true,
                                text: 'Biểu đồ doanh thu'
                            },
                            scales: {
                                yAxes: [{
                                    type: 'linear', // only linear but allow scale type registration. This allows extensions to exist solely for log scale for instance
                                    display: true,
                                    position: 'left',
                                    id: 'y-axis-1',
                                    ticks: {
                                        beginAtZero: true
                                    },
                                }, {
                                    type: 'linear', // only linear but allow scale type registration. This allows extensions to exist solely for log scale for instance
                                    display: true,
                                    position: 'right',
                                    id: 'y-axis-2',
                                    ticks: {
                                        beginAtZero: true
                                    },
                                    // grid line settings
                                    gridLines: {
                                        drawOnChartArea: false, // only want the grid lines for one axis to show up
                                    },
                                }],
                            }
                        }
                    });
                }
		    };
	    </script>
    </main>
</asp:Content>
