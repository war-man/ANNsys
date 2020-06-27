<%@ Page Title="Thông kê chuyển kho" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="thong-ke-chuyen-kho.aspx.cs" Inherits="IM_PJ.thong_ke_chuyen_kho" EnableSessionState="ReadOnly" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .all-product-table tr.parent-row td {
            background-color: #fff!important;
        }
        .all-product-table tr.child-row td {
            background-color: #f8f8f8!important;
        }
        .all-product-table tr.parent-row td img {
            width: 159px;
        }
        .all-product-table tr.child-row td img {
            width: 100px;
        }
        .all-product-table .image-column {
            width: 10%;
        }
        .all-product-table .name-column {
            width: 30%;
        }
        .all-product-table .sku-column {
            width: 14%;
        }
        .all-product-table .stock-column {
            width: 8%;
        }
        .all-product-table .category-column {
            width: 12%;
        }
        .all-product-table .date-column {
            width: 12%;
        }
        .all-product-table .action-column {
            width: 5%;
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
            border-bottom: solid 1px #e1e1e1!important;
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
                border-bottom: none!important;
            }
            table.shop_table_responsive > tbody > tr:nth-of-type(2n+1) td {
                border-bottom: none!important;
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
                    <h3 class="page-title left">Thống kê nhập kho <span>(<asp:Literal ID="ltrNumberOfProduct" runat="server" EnableViewState="false"></asp:Literal>)</span></h3>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="filter-above-wrap clear">
                        <div class="filter-control">
                            <div class="row">
                                <div class="col-md-3 col-xs-6">
                                    <asp:TextBox ID="txtSearchProduct" runat="server" CssClass="form-control" placeholder="Tìm sản phẩm" autocomplete="off"></asp:TextBox>
                                </div>
                                <div class="col-md-2 col-xs-6">
                                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-xs-6">
                                    <asp:DropDownList ID="ddlStock" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0" Text="Chọn kho"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Kho 1"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="kho 2"></asp:ListItem>
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
                                    <a href="javascript:;" onclick="searchProduct()" class="btn primary-btn h45-btn"><i class="fa fa-search"></i></a>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn primary-btn h45-btn" OnClick="btnSearch_Click" Style="display: none" />
                                    <a href="/tat-ca-san-pham" class="btn primary-btn h45-btn"><i class="fa fa-times" aria-hidden="true"></i></a>
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

        <script type="text/javascript">
            $("#<%=txtSearchProduct.ClientID%>").keyup(function (e) {
                if (e.keyCode == 13)
                {
                    $("#<%= btnSearch.ClientID%>").click();
                }
            });

            function searchProduct()
            {
                $("#<%= btnSearch.ClientID%>").click();
            }

            function showSubGoodsReceipt(obj, sku)
            {
                let variableDOM = $("." + sku);
                variableDOM.each((index, element) => {
                    //let display = element.style.display;
                    //element.style.display = display == "none" ? "" : "none";
                    if ($(element).hasClass('hide'))
                    {
                        $(element).removeClass('hide');
                        $(obj).html("<i class='fa fa-chevron-up' aria-hidden='true'></i>");
                    }
                    else {
                        $(element).addClass('hide');
                        $(obj).html("<i class='fa fa-chevron-down' aria-hidden='true'></i>");
                    }
                })
            }
        </script>
    </main>
</asp:Content>
