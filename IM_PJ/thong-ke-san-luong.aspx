<%@ Page Title="Thống kê sản lượng" Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.Master" CodeBehind="thong-ke-san-luong.aspx.cs" Inherits="IM_PJ.thong_ke_san_luong" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="/App_Themes/Ann/js/Chart.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <h3 class="page-title left">Thống kê sản lượng</h3>
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
                                text: 'Biểu đồ sản lượng'
                            },
                            scales: {
                                yAxes: [{
                                    type: 'linear', // only linear but allow scale type registration. This allows extensions to exist solely for log scale for instance
                                    display: true,
                                    position: 'left',
                                    id: 'y-axis-1',
                                    ticks: {
                                        beginAtZero: true,
                                        callback: function (label, index, labels) {
                                            return label.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                                        }
                                    },
                                }],
                            },
                            tooltips: {
                                callbacks: {
                                    label: function (tooltipItems, data) {
                                        let label = data.datasets[tooltipItems.datasetIndex].label;
                                        let value = tooltipItems.yLabel.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");

                                        return label + ' : ' + value;
                                    }
                                }
                            }
                        }
                    });
                }
		    };
	    </script>
    </main>
</asp:Content>
