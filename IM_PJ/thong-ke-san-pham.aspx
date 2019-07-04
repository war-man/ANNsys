<%@ Page Language="C#" Title="Thống kê sản phẩm" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="thong-ke-san-pham.aspx.cs" Inherits="IM_PJ.thong_ke_san_pham" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .fromdate-link {
            color:#ff8400;
            text-decoration: underline;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <h3 class="page-title left">Thống kê sản phẩm</h3>
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
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtTextSearch" runat="server" CssClass="form-control" placeholder="Tìm sản phẩm"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlCreatedBy" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <label>Từ ngày: <a href="javascript:;" class="fromdate-link fromdate-createddate hide">ngày tạo sản phẩm</a>, <a href="javascript:;" class="fromdate-link fromdate-today hide">hôm nay</a></label>
                                    <telerik:RadDatePicker RenderMode="Lightweight" ID="rFromDate" ShowPopupOnFocus="true" Width="100%" runat="server" DateInput-CssClass="radPreventDecorate" MinDate="01/01/2018">
                                        <DateInput DisplayDateFormat="dd/MM/yyyy" runat="server">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </div>
                                <div class="col-md-2">
                                    <label>Đến ngày:</label>
                                    <telerik:RadDatePicker RenderMode="Lightweight" ID="rToDate" ShowPopupOnFocus="true" Width="100%" runat="server" DateInput-CssClass="radPreventDecorate" MinDate="01/01/2018">
                                        <DateInput DisplayDateFormat="dd/MM/yyyy" runat="server">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </div>
                                <div class="col-md-1">
                                    <a href="javascript:;" onclick="searchWithDateRange()" class="btn primary-btn h45-btn"><i class="fa fa-search"></i></a>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn primary-btn h45-btn" OnClick="btnSearch_Click" Style="display: none" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel-table clear">
                        <div class="responsive-table">
                            <div class="row margin-bottom-15">
                                <div class="col-md-3">
                                    <div class="report-column">
                                        <div class="report-label">
                                            Số lượng bán (đã trừ hàng trả):
                                        </div>
                                        <div class="report-value">
                                            <asp:Literal ID="ltrTotalRemain" runat="server" EnableViewState="false"></asp:Literal>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="report-column">
                                        <div class="report-label">
                                            Số lượng bán mỗi ngày (đã trừ hàng trả):
                                        </div>
                                        <div class="report-value">
                                            <asp:Literal ID="ltrTotalRemainPerDay" runat="server" EnableViewState="false"></asp:Literal>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="report-column">
                                        <div class="report-label">
                                            Số lượng bán ra:
                                        </div>
                                        <div class="report-value">
                                            <asp:Literal ID="ltrTotalSold" runat="server" EnableViewState="false"></asp:Literal>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="report-column">
                                        <div class="report-label">
                                            Số lượng đổi trả:
                                        </div>
                                        <div class="report-value">
                                            <asp:Literal ID="ltrTotalRefund" runat="server" EnableViewState="false"></asp:Literal>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row margin-bottom-15">
                                <div class="col-md-3">
                                    <div class="report-column">
                                        <div class="report-label">
                                            Lợi nhuận:
                                        </div>
                                        <div class="report-value">
                                            <asp:Literal ID="ltrTotalProfit" runat="server" EnableViewState="false"></asp:Literal>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="report-column">
                                        <div class="report-label">
                                            Doanh số:
                                        </div>
                                        <div class="report-value">
                                            <asp:Literal ID="ltrTotalRevenue" runat="server" EnableViewState="false"></asp:Literal>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="report-column">
                                        <div class="report-label">
                                            Tồn kho:
                                        </div>
                                        <div class="report-value">
                                            <asp:Literal ID="ltrTotalStock" runat="server" EnableViewState="false"></asp:Literal>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="report-column">
                                        <div class="report-label">
                                            Giá trị tồn kho:
                                        </div>
                                        <div class="report-value">
                                            <asp:Literal ID="ltrTotalStockValue" runat="server" EnableViewState="false"></asp:Literal>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <script type="text/javascript">
            $(document).ready(() => {
                if ($("#<%= txtTextSearch.ClientID%>").val() != "")
                {
                    $(".fromdate-createddate").removeClass("hide");
                    $(".fromdate-today").removeClass("hide");
                }
            });

            $(".fromdate-createddate").click(e => {
                var sku = $("#<%= txtTextSearch.ClientID%>").val();
                var createdby = $("#<%= ddlCreatedBy.ClientID%>").val();
                var categoryid = $("#<%= ddlCategory.ClientID%>").val();
                window.location.href = "/thong-ke-san-pham?sku=" + sku + "&categoryid=" + categoryid + "&createdby=" + createdby;
            });

            $(".fromdate-today").click(e => {
                var today = new Date();
                var date = (today.getMonth() + 1) + '/' + today.getDate() + '/' + today.getFullYear();
                var dateTime = date + ' 12:00:00 AM';

                var sku = $("#<%= txtTextSearch.ClientID%>").val();
                var createdby = $("#<%= ddlCreatedBy.ClientID%>").val();
                var categoryid = $("#<%= ddlCategory.ClientID%>").val();
                window.location.href = "/thong-ke-san-pham?sku=" + sku + "&categoryid=" + categoryid + "&createdby=" + createdby + "&fromdate=" + dateTime + "&todate=" + dateTime;
            });
            
            function searchWithDateRange() 
            {
                $("#<%= btnSearch.ClientID%>").click();
            }
        </script>
    </main>
</asp:Content>
