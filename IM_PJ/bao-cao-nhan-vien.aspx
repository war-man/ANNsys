<%@ Page Language="C#" Title="Báo cáo nhân viên" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="bao-cao-nhan-vien.aspx.cs" Inherits="IM_PJ.bao_cao_nhan_vien" %>

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
                    <h3 class="page-title left">Báo cáo nhân viên</h3>
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
                                <div class="col-md-3">
                                    <label>Từ ngày: <a href="javascript:;" class="fromdate-link fromdate-createddate hide">ngày tạo sản phẩm</a>, <a href="javascript:;" class="fromdate-link fromdate-today hide">hôm nay</a></label>
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
                            <div class="col-md-4">
                                <div class="report-column">
                                    <div class="report-label">
                                        Tổng sản lượng (đã trừ hàng trả):
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrTotalRemainQuantity" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="report-column">
                                    <div class="report-label">
                                        Sản lượng mỗi ngày (đã trừ hàng trả):
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrAverageRemainQuantity" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row margin-bottom-15">
                            <div class="col-md-4">
                                <div class="report-column">
                                    <div class="report-label">
                                        Tổng số đơn hàng hoàn tất:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrTotalSaleOrder" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="report-column">
                                    <div class="report-label">
                                        Số đơn hoàn tất mỗi ngày:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrAverageSaleOrder" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row margin-bottom-15">
                            <div class="col-md-4">
                                <div class="report-column">
                                    <div class="report-label">
                                        Tổng số bán ra:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrTotalSoldQuantity" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="report-column">
                                    <div class="report-label">
                                        Bán ra mỗi ngày:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrAverageSoldQuantity" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row margin-bottom-15">
                            <div class="col-md-4">
                                <div class="report-column">
                                    <div class="report-label">
                                        Tổng số đổi trả:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrTotalRefundQuantity" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="report-column">
                                    <div class="report-label">
                                        Đổi trả mỗi ngày:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrAverageRefundQuantity" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row margin-bottom-15">
                            <div class="col-md-4">
                                <div class="report-column">
                                    <div class="report-label">
                                        Phần trăm sản lượng / tổng hệ thống:
                                    </div>
                                    <div class="report-value">
                                        <asp:Literal ID="ltrPercentOfSystem" runat="server" EnableViewState="false"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
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

            $(document).ready(() => {
                if ($("#<%= txtTextSearch.ClientID%>").val() != "")
                {
                    $(".fromdate-createddate").removeClass("hide");
                    $(".fromdate-today").removeClass("hide");
                }
            });

            $(".fromdate-createddate").click(e => {
                var sku = $("#<%= txtTextSearch.ClientID%>").val();
                var categoryid = $("#<%= ddlCategory.ClientID%>").val();
                window.location.href = "/bao-cao-nhan-vien?sku=" + sku + "&categoryid=" + categoryid;
            });

            $(".fromdate-today").click(e => {
                var today = new Date();
                var date = (today.getMonth() + 1) + '/' + today.getDate() + '/' + today.getFullYear();
                var dateTime = date + ' 12:00:00 AM';

                var sku = $("#<%= txtTextSearch.ClientID%>").val();
                var categoryid = $("#<%= ddlCategory.ClientID%>").val();
                window.location.href = "/bao-cao-nhan-vien?sku=" + sku + "&categoryid=" + categoryid + "&fromdate=" + dateTime + "&todate=" + dateTime;
            });

            function searchAgent() {
                $("#<%= btnSearch.ClientID%>").click();
            }
        </script>
    </main>
</asp:Content>
