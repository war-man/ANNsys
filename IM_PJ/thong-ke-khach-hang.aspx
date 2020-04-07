<%@ Page Language="C#" Title="Thống kê khách hàng" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="thong-ke-khach-hang.aspx.cs" Inherits="IM_PJ.thong_ke_khach_hang" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="/App_Themes/Ann/js/Chart.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <h3 class="page-title left">Thống kê khách hàng</h3>
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
                                    <asp:TextBox ID="txtTextSearch" runat="server" CssClass="form-control" placeholder="Tìm khách hàng"></asp:TextBox>
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
                            <div class="col-md-12">
                                <div class="panel panelborderheading">
                                    <div class="panel-heading clear">
                                        <h3 class="page-title left not-margin-bot">Thông tin khách hàng</h3>
                                    </div>
                                    <div class="panel-body">
                                        <asp:Literal ID="ltrInfo" runat="server"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row margin-bottom-15">
                            <div class="col-md-2">
                                <div class="report-column">
                                    <div class="report-label">
                                        Số đơn mua:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrTotalQuantityOrder" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="report-column">
                                    <div class="report-label">
                                        Số lượng mua:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrTotalQuantityProduct" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="report-column">
                                    <div class="report-label">
                                        Số đơn đổi trả:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrTotalQuantityRefund" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="report-column">
                                    <div class="report-label">
                                        Số lượng đổi trả:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrTotalQuantityProductRefund" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="report-column">
                                    <div class="report-label">
                                        Số lượng còn lại:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrTotalProductLeft" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="report-column">
                                    <div class="report-label">
                                        Slượng còn lại trung bình:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrAverageProduct" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row margin-bottom-15">
                            <div class="col-md-2">
                                <div class="report-column">
                                    <div class="report-label">
                                        Doanh số:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrTotalPrice" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="report-column">
                                    <div class="report-label">
                                        Chiết khấu:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrTotalDiscount" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="report-column">
                                    <div class="report-label">
                                        Phí giao hàng:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrTotalFeeShipping" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="report-column">
                                    <div class="report-label">
                                        Đổi trả:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrTotalRefundMoney" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="report-column">
                                    <div class="report-label">
                                        Phí đổi trả:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrTotalRefundFee" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="report-column">
                                    <div class="report-label">
                                        Lợi nhuận:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrTotalProfit" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row margin-bottom-15">
                            <div class="col-md-12">
                                <canvas id="saleChart"></canvas>
                            </div>
                        </div>
                        <div class="row margin-bottom-15">
                            <div class="col-md-12">
                                <canvas id="productChart"></canvas>
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
        <asp:Literal ID="ltrSaleData" runat="server" EnableViewState="false"></asp:Literal>
        <asp:Literal ID="ltrProductData" runat="server" EnableViewState="false"></asp:Literal>
        <script>
            window.onload = function () {

                // Sale chart 
                if (typeof lineSaleData !== 'undefined') {
                    var ctx = document.getElementById('saleChart').getContext('2d');
                    window.myLine = Chart.Line(ctx, {
                        data: lineSaleData,
                        options: {
                            responsive: true,
                            aspectRatio: 3.5,
                            hoverMode: 'index',
                            stacked: false,
                            title: {
                                display: true,
                                text: 'Biểu đồ doanh thu theo khách hàng'
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
                                    }
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

                // Product chart
                if (typeof lineProductData !== 'undefined') {
                    var ctx = document.getElementById('productChart').getContext('2d');
                    window.myLine = Chart.Line(ctx, {
                        data: lineProductData,
                        options: {
                            responsive: true,
                            aspectRatio: 3.5,
                            hoverMode: 'index',
                            stacked: false,
                            title: {
                                display: true,
                                text: 'Biểu đồ số lượng khách hàng mua - trả hàng'
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
                                    }
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
