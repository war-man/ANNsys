<%@ Page Title="Thông kê giao hàng bưu điện" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="thong-ke-buu-dien.aspx.cs" Inherits="IM_PJ.thong_ke_buu_dien" EnableSessionState="ReadOnly" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/moment.min.js"></script>
    <script src="/Scripts/moment-with-locales.min.js"></script>
    <script src="/Scripts/bootstrap-datetimepicker.min.js"></script>
    <style>
        #invoice-image li {
            list-style: none;
        }

        #invoice-image img {
            width: 60%;
        }

        .select2-container .select2-selection--single {
            height: 45px;
        }

        .select2-container--default .select2-selection--single .select2-selection__rendered {
            line-height: 45px;
        }

        .select2-container--default .select2-selection--single .select2-selection__arrow {
            height: 43px;
        }

        @media (max-width: 768px) {
            table.shop_table_responsive thead {
                display: none;
            }

            table.shop_table_responsive > tbody > tr > td:nth-of-type(1):before {
                content: none;
            }

            table.shop_table_responsive > tbody > tr > td:nth-of-type(1) {
                text-align: left;
            }

            table.shop_table_responsive > tbody > tr > td:nth-of-type(2):before {
                content: "#";
                font-size: 20px;
                margin-right: 2px;
            }

            table.shop_table_responsive > tbody > tr > td:nth-of-type(2) {
                text-align: left;
                font-size: 20px;
                font-weight: bold;
                height: 50px;
            }

            table.shop_table_responsive > tbody > tr:nth-of-type(2n) td {
                border-top: none;
                border-bottom: none !important;
            }

            table.shop_table_responsive > tbody > tr > td:first-child {
                border-left: none;
                padding-left: 20px;
            }

            table.shop_table_responsive > tbody > tr > td:last-child {
                border-right: none;
                padding-left: 20px;
            }

            table.shop_table_responsive > tbody > tr > td {
                height: 40px;
            }

                table.shop_table_responsive > tbody > tr > td.customer-td {
                    height: 60px;
                }

                table.shop_table_responsive > tbody > tr > td.update-button {
                    height: 85px;
                }

            table.shop_table_responsive .bg-bronze,
            table.shop_table_responsive .bg-red,
            table.shop_table_responsive .bg-blue,
            table.shop_table_responsive .bg-yellow,
            table.shop_table_responsive .bg-black,
            table.shop_table_responsive .bg-green {
                display: initial;
                float: right;
            }

            table.shop_table_responsive tbody td {
                background-color: #f8f8f8;
                display: block;
                text-align: right;
                border: none;
                padding: 20px;
            }

            table.shop_table_responsive > tbody > tr.tr-more-info > td {
                height: initial;
            }

                table.shop_table_responsive > tbody > tr.tr-more-info > td span {
                    display: block;
                    text-align: left;
                    margin-bottom: 10px;
                    margin-right: 0;
                }

                table.shop_table_responsive > tbody > tr.tr-more-info > td:nth-child(2):before {
                    content: none;
                }

            table.shop_table_responsive tbody td:before {
                content: attr(data-title) ": ";
                font-weight: 700;
                float: left;
                text-transform: uppercase;
                font-size: 14px;
            }

            table.shop_table_responsive tbody td:empty {
                display: none;
            }

            #invoice-image img {
                width: 40%;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <h3 class="page-title left">Giao hàng  <span>(<asp:Literal ID="ltrNumberOfOrder" runat="server" EnableViewState="false"></asp:Literal>)
                    </span>
                    </h3>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="filter-above-wrap clear">
                        <div class="filter-control">
                            <div class="row">
                                <div class="col-md-3 col-xs-6">
                                    <asp:FileUpload ID="FileUpload" accept=".xlsx" runat="server" />
                                </div>
                                <div class="col-md-2 col-xs-4">
                                    <a href="javascript:;" class="btn primary-btn fw-btn width-100" onclick="getElementById('ContentPlaceHolder1_btUpload').click()">
                                        <i class="fa fa-cloud-upload" aria-hidden="true"></i>Upload
                                    </a>
                                    <asp:Button ID="btUpload" OnClick="UploadButton_Click" runat="server" Style="display: none"></asp:Button>
                                </div>
                                <div class="col-md-6 col-xs-12">
                                    <asp:Label id="UploadStatusLabel" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="filter-above-wrap clear">
                        <div class="filter-control">
                            <div class="row">
                                <div class="col-md-3 col-xs-6">
                                    <asp:TextBox ID="txtSearchOrder" runat="server" CssClass="form-control" placeholder="Tìm đơn hàng" autocomplete="off"></asp:TextBox>
                                </div>
                                <div class="col-md-2 col-xs-6">
                                    <asp:DropDownList ID="ddlReview" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0" Text="Trạng thái đơn bưu điện"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Đang chờ duyệt"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Đã duyệt"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Đơn hủy"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="Không tìm tháy order"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-xs-6">
                                    <asp:DropDownList ID="ddlFeeStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0" Text="Tình trạng phí vận chuyển"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Có lời"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Lỗ vốn"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-xs-6">
                                    <label>Từ ngày</label>
                                    <telerik:RadDatePicker RenderMode="Lightweight" ID="rOrderFromDate" ShowPopupOnFocus="true" Width="100%" runat="server" DateInput-CssClass="radPreventDecorate">
                                        <DateInput DisplayDateFormat="dd/MM/yyyy" runat="server">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </div>
                                <div class="col-md-2 col-xs-6">
                                    <label>Đến ngày</label>
                                    <telerik:RadDatePicker RenderMode="Lightweight" ID="rOrderToDate" ShowPopupOnFocus="true" Width="100%" runat="server" DateInput-CssClass="radPreventDecorate">
                                        <DateInput DisplayDateFormat="dd/MM/yyyy" runat="server">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </div>
                                <div class="col-md-1 col-xs-6 search-button">
                                    <a href="javascript:;" onclick="searchOrder()" class="btn primary-btn h45-btn"><i class="fa fa-search"></i></a>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn primary-btn h45-btn" OnClick="btnSearch_Click" Style="display: none" />
                                    <a href="/danh-sach-van-chuyen" class="btn primary-btn h45-btn"><i class="fa fa-times" aria-hidden="true"></i></a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel-table clear">
                        <div class="panel-footer clear">
                            <div class="pagination">
                                <%this.DisplayHtmlStringPaging1();%>
                            </div>
                        </div>
                        <div class="responsive-table">
                            <table class="table shop_table_responsive table-checkable table-product table-new-product">
                                <asp:Literal ID="ltrList" runat="server" EnableViewState="false"></asp:Literal>
                            </table>
                        </div>
                        <div class="panel-footer clear">
                            <div class="pagination">
                                <%this.DisplayHtmlStringPaging1();%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <script type="text/javascript">
            $("#<%=txtSearchOrder.ClientID%>").keyup(function (e) {
                if (e.keyCode == 13) {
                    $("#<%=btnSearch.ClientID%>").click();
                }
            });

            function searchOrder() {
                $("#<%=btnSearch.ClientID%>").click();
            }
        </script>
    </main>
</asp:Content>
