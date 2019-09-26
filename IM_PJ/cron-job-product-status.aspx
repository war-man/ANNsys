<%@ Page Title="Cron Job Trạng Thái Sản Phẩm" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cron-job-product-status.aspx.cs" Inherits="IM_PJ.cron_job_product_status" EnableSessionState="ReadOnly" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .select2-container .select2-selection--single {
            height: 45px;
        }

        .select2-container--default .select2-selection--single .select2-selection__rendered {
            line-height: 45px;
        }

        .select2-container--default .select2-selection--single .select2-selection__arrow {
            height: 43px;
        }

        .btn.download-btn {
            background-color: #000;
            color: #fff;
            border-radius: 0;
            text-transform: uppercase;
            width: 100%;
            height: 35px;
            line-height: 8px;
        }

            .btn.download-btn:hover {
                color: #ff8400;
            }

        table.shop_table_responsive > tbody > tr:nth-of-type(2n+1) td {
            border-bottom: solid 1px #e1e1e1 !important;
        }

        @media (max-width: 768px) {
            table.shop_table_responsive thead {
                display: none;
            }

            table.shop_table_responsive > tbody > tr > td:nth-of-type(1):before {
                content: "";
            }

            table.shop_table_responsive > tbody > tr > td:nth-of-type(1) {
                height: auto;
                padding-bottom: 0;
            }

            table.shop_table_responsive > tbody > tr > td:nth-of-type(2):before {
                content: "";
            }

            table.shop_table_responsive > tbody > tr > td:nth-of-type(2) {
                content: "";
                text-align: left;
            }

            table.shop_table_responsive > tbody > tr:nth-of-type(2n) td {
                border-top: none;
                border-bottom: none !important;
            }

            table.shop_table_responsive > tbody > tr:nth-of-type(2n+1) td {
                border-bottom: none !important;
                background-color: #fff;
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

                table.shop_table_responsive > tbody > tr > td.payment-type, table.shop_table_responsive > tbody > tr > td.shipping-type {
                    height: 70px;
                }

                table.shop_table_responsive > tbody > tr > td .new-status-btn {
                    display: block;
                    margin-top: 10px;
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
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <h3 class="page-title left">Lịch Trình <span>(<asp:Literal ID="ltrNumberOfSchedule" runat="server" EnableViewState="false"></asp:Literal>)</span></h3>
                    <div class="right above-list-btn cron-job">
                        <button type='button' class='h45-btn btn primary-btn' onclick='onClick_btnCronJob()'>
                            <i class="glyphicon glyphicon-cog"></i> Setting
                        </button>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="filter-above-wrap clear">
                        <div class="filter-control">
                            <div class="row">
                                <div class="col-md-3 col-xs-6">
                                    <asp:TextBox ID="txtSearchProduct" runat="server" CssClass="form-control" placeholder="Tìm sản phẩm" autocomplete="off" onkeyup="onKeyUp_txtSearchProduct(event)"></asp:TextBox>
                                </div>
                                <div class="col-md-2 col-xs-6">
                                    <asp:DropDownList ID="ddlWebAdvertisement" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="" Text="Web quảng cáo"></asp:ListItem>
                                        <asp:ListItem Value="ann.com.vn" Text="ann.com.vn"></asp:ListItem>
                                        <asp:ListItem Value="khohangsiann.com" Text="khohangsiann.com"></asp:ListItem>
                                        <asp:ListItem Value="bosiquanao.net" Text="bosiquanao.net"></asp:ListItem>
                                        <asp:ListItem Value="quanaogiaxuong.com" Text="quanaogiaxuong.com"></asp:ListItem>
                                        <asp:ListItem Value="bansithoitrang.net" Text="bansithoitrang.net"></asp:ListItem>
                                        <asp:ListItem Value="quanaoxuongmay.com" Text="quanaoxuongmay.com"></asp:ListItem>
                                        <asp:ListItem Value="annshop.vn" Text="annshop.vn"></asp:ListItem>
                                        <asp:ListItem Value="panpan.vn" Text="panpan.vn"></asp:ListItem>
                                        <asp:ListItem Value="chuyensidobo.com" Text="chuyensidobo.com"></asp:ListItem>
                                        <asp:ListItem Value="damgiasi.vn" Text="damgiasi.vn"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-xs-6">
                                    <asp:DropDownList ID="ddlScheduleStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0" Text="Trang thái cron job"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Đang chờ chạy"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Đang chạy"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Tạm dừng"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="Hoàn thành"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="Thất bại"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-xs-6">
                                    <label>Từ ngày</label>
                                    <telerik:RadDatePicker RenderMode="Lightweight" ID="rFromDate" ShowPopupOnFocus="true" Width="100%" runat="server" DateInput-CssClass="radPreventDecorate">
                                        <DateInput DisplayDateFormat="dd/MM/yyyy" runat="server">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </div>
                                <div class="col-md-2 col-xs-6">
                                    <label>Đến ngày</label>
                                    <telerik:RadDatePicker RenderMode="Lightweight" ID="rToDate" ShowPopupOnFocus="true" Width="100%" runat="server" DateInput-CssClass="radPreventDecorate">
                                        <DateInput DisplayDateFormat="dd/MM/yyyy" runat="server">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </div>
                                <div class="col-md-1 col-xs-6 search-button">
                                    <a href="javascript:;" onclick="onClick_btnSearchProduct()" class="btn primary-btn h45-btn"><i class="fa fa-search"></i></a>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn primary-btn h45-btn" OnClick="btnSearch_Click" Style="display: none" />
                                    <a href="/cron-job-product-status" class="btn primary-btn h45-btn"><i class="fa fa-times" aria-hidden="true"></i></a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="panel-table clear">
                        <div class="panel-footer clear">
                            <div class="pagination">
                                <%this.DisplayHtmlStringPaging1();%>
                            </div>
                        </div>
                        <div class="responsive-table">
                            <table class="table table-checkable table-product all-product-table shop_table_responsive">
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

        <!-- Modal Cấu hình Cron Job của trạng thái sản phẩm -->
        <div class="modal fade" id="CronJobSettingModal" role="dialog">
            <div class="modal-dialog modal-sm">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Cấu hình Cron Job</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row form-group">
                            <div class="col-md-5 col-xs-6">
                                <p>Thời gian</p>
                            </div>
                            <div class="col-md-7 col-xs-6">
                                <asp:TextBox ID="txtCronExpression" runat="server" CssClass="form-control text-left" placeholder="Thời gian chạy Cron Job"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-md-5 col-xs-6">
                                <p>Trạng thái</p>
                            </div>
                            <div class="col-md-7 col-xs-6">
                                <asp:DropDownList ID="ddlCronJobStatus" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="1" Text="Cho chạy"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="Tạm dừng"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button id="closeCronJobSetting" type="button" class="btn btn-default" data-dismiss="modal">Đóng</button>
                        <button id="updateCronJobSetting" type="button" class="btn btn-primary" onclick="onclick_UpdateCronJobSetting()">Lưu</button>
                    </div>
                </div>
            </div>
        </div>

        <script type="text/javascript" src="App_Themes/Ann/js/services/cron-job-product-status/cron-job-product-status-service.js?v=20190926120003"></script>
        <script type="text/javascript" src="App_Themes/Ann/js/controllers/cron-job-product-status/cron-job-product-status-controller.js?v=20190926120003"></script>
        <script type="text/javascript" src="App_Themes/Ann/js/pages/cron-job-product-status/cron-job-product-status.js?v=20190926120003"></script>
    </main>
</asp:Content>
