<%@ Page Title="Sản phẩm" Language="C#" MasterPageFile="~/ProductPage.Master" AutoEventWireup="true" CodeBehind="san-pham.aspx.cs" Inherits="IM_PJ.san_pham" EnableSessionState="ReadOnly" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            background-color: #eee;
        }
        .page-title {
            margin-top: 15px;
        }
        .margin-right-15px {
            margin-right: 12px!important;
        }
        .main-block {
            margin: auto;
            float: inherit;
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
        input[type="checkbox"] {
            width: 20px;
            height: 20px;
        }
        .bg-green, .bg-red, .bg-yellow {
            display: inherit;
        }
        .table > thead > tr > th {
            background-color: #0090da;
        }
        .btn {
            border-radius: 5px;
        }
        .btn.primary-btn {
            background-color: #4bac4d;
        }
        .btn.primary-btn:hover {
            background-color: #3e8f3e;
        }
        img {
            border-radius: 5px;
        }
        .form-control {
            border-radius: 5px;
            margin-bottom: 15px;
        }
        .select2-container .select2-selection--single {
            border-radius: 5px;
            margin-bottom: 15px;
        }
        .filter-above-wrap {
            margin-bottom: 0;
        }
        .filter-above-wrap .action-button {
            margin-bottom: 15px;
        }
        .pagination li > a, .pagination li > a {
            height: 24px;
            border: transparent;
            line-height: 24px;
            -ms-box-orient: horizontal;
            display: -webkit-box;
            display: -moz-box;
            display: -ms-flexbox;
            display: -moz-flex;
            display: -webkit-flex;
            display: flex;
            align-items: center;
            justify-content: center;
            background-color: transparent;
            z-index: 99;
            width: auto;
            min-width: 24px;
            -webkit-border-radius: 2px;
            -moz-border-radius: 2px;
            border-radius: 2px;
            margin: 0 5px 0 0;
        }
        .pagination li.current {
            background-color: #4bac4d;
            color: #fff;
            height: 24px;
            border: transparent;
            line-height: 24px;
            -ms-box-orient: horizontal;
            align-items: center;
            justify-content: center;
            z-index: 99;
            width: auto;
            min-width: 24px;
            -webkit-border-radius: 2px;
            -moz-border-radius: 2px;
            border-radius: 2px;
            margin: 0 5px 0 0;
            text-align: center;
        }
        .pagination li:hover > a {
            background-color: #e5e5e5;
            color: #000;
        }
        
        @media (max-width: 768px) {
            .margin-right-15px {
                margin-right: 6px!important;
            }
            .filter-above-wrap .action-button {
                margin-top: 15px;
            }
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
                height: auto;
                padding-bottom: 0;
            }
            table.shop_table_responsive > tbody > tr > td:nth-of-type(3):before {
                content: "";
            }
            table.shop_table_responsive > tbody > tr > td:nth-of-type(3) {
                height: auto;
                text-align: left;
                padding-bottom: 1px;
            }
            table.shop_table_responsive > tbody > tr > td:nth-of-type(11):before {
                content: "";
            }
            table.shop_table_responsive > tbody > tr > td:nth-of-type(11) {
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
    <main>
        <div class="col-md-10 main-block">
            <div class="row">
                <div class="col-md-12">
                    <h3 class="page-title left">Sản phẩm <span>(<asp:Literal ID="ltrNumberOfProduct" runat="server" EnableViewState="false"></asp:Literal>)</span></h3>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="filter-above-wrap clear">
                        <div class="filter-control">
                            <div class="row">
                                <div class="col-md-4 col-xs-12">
                                    <asp:TextBox ID="txtSearchProduct" runat="server" CssClass="form-control" placeholder="Tìm sản phẩm" autocomplete="off"></asp:TextBox>
                                </div>
                                <div class="col-md-2 col-xs-6">
                                    <asp:DropDownList ID="ddlOrderBy" runat="server" CssClass="form-control" Width="100%">
                                        <asp:ListItem Value="" Text="Sắp xếp"></asp:ListItem>
                                        <asp:ListItem Value="latestOnApp" Text="Mới nhất trên app"></asp:ListItem>
                                        <asp:ListItem Value="latestOnSystem" Text="Mới nhất trên hệ thống"></asp:ListItem>
                                        <asp:ListItem Value="stockDesc" Text="Kho giảm dần"></asp:ListItem>
                                        <asp:ListItem Value="stockAsc" Text="Kho tăng dần"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-xs-6">
                                    <asp:DropDownList ID="ddlStockStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="" Text="Trạng thái kho"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Còn hàng"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Hết hàng"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Nhập hàng"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-xs-6">
                                    <asp:DropDownList ID="ddlQuantityFilter" runat="server" CssClass="form-control" onchange="changeQuantityFilter($(this))">
                                        <asp:ListItem Value="" Text="Số lượng kho"></asp:ListItem>
                                        <asp:ListItem Value="greaterthan" Text="Số lượng lớn hơn"></asp:ListItem>
                                        <asp:ListItem Value="lessthan" Text="Số lượng nhỏ hơn"></asp:ListItem>
                                        <asp:ListItem Value="between" Text="Số lượng trong khoảng"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-xs-6 greaterthan lessthan">
                                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" placeholder="Số lượng" autocomplete="off"></asp:TextBox>
                                </div>
                                <div class="col-md-2 col-xs-6 between hide">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtQuantityMin" runat="server" CssClass="form-control" placeholder="Min" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtQuantityMax" runat="server" CssClass="form-control" placeholder="Max" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="filter-above-wrap clear">
                        <div class="filter-control">
                            <div class="row">
                                <div class="col-md-2 col-xs-6">
                                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-xs-6">
                                    <asp:DropDownList ID="ddlTag" runat="server" CssClass="form-control select2" Width="100%"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-xs-6">
                                    <asp:DropDownList ID="ddlColor" runat="server" CssClass="form-control select2" Width="100%">
                                        <asp:ListItem Value="" Text="Màu"></asp:ListItem>
                                        <asp:ListItem Value="cam" Text="Cam"></asp:ListItem>
                                        <asp:ListItem Value="cam tươi" Text="Cam tươi"></asp:ListItem>
                                        <asp:ListItem Value="cam đất" Text="Cam đất"></asp:ListItem>
                                        <asp:ListItem Value="cam sữa" Text="Cam sữa"></asp:ListItem>
                                        <asp:ListItem Value="caro" Text="Caro"></asp:ListItem>
                                        <asp:ListItem Value="da bò" Text="Da bò"></asp:ListItem>
                                        <asp:ListItem Value="đen" Text="Đen"></asp:ListItem>
                                        <asp:ListItem Value="đỏ" Text="Đỏ"></asp:ListItem>
                                        <asp:ListItem Value="đỏ đô" Text="Đỏ đô"></asp:ListItem>
                                        <asp:ListItem Value="đỏ tươi" Text="Đỏ tươi"></asp:ListItem>
                                        <asp:ListItem Value="dưa cải" Text="Dưa cải"></asp:ListItem>
                                        <asp:ListItem Value="gạch tôm" Text="Gạch tôm"></asp:ListItem>
                                        <asp:ListItem Value="hồng" Text="Hồng"></asp:ListItem>
                                        <asp:ListItem Value="hồng cam" Text="Hồng cam"></asp:ListItem>
                                        <asp:ListItem Value="hồng da" Text="Hồng da"></asp:ListItem>
                                        <asp:ListItem Value="hồng dâu" Text="Hồng dâu"></asp:ListItem>
                                        <asp:ListItem Value="hồng phấn" Text="Hồng phấn"></asp:ListItem>
                                        <asp:ListItem Value="hồng ruốc" Text="Hồng ruốc"></asp:ListItem>
                                        <asp:ListItem Value="hồng sen" Text="Hồng sen"></asp:ListItem>
                                        <asp:ListItem Value="kem" Text="Kem"></asp:ListItem>
                                        <asp:ListItem Value="kem tươi" Text="Kem tươi"></asp:ListItem>
                                        <asp:ListItem Value="kem đậm" Text="Kem đậm"></asp:ListItem>
                                        <asp:ListItem Value="kem nhạt" Text="Kem nhạt"></asp:ListItem>
                                        <asp:ListItem Value="nâu" Text="Nâu"></asp:ListItem>
                                        <asp:ListItem Value="nho" Text="Nho"></asp:ListItem>
                                        <asp:ListItem Value="rạch tôm" Text="Rạch tôm"></asp:ListItem>
                                        <asp:ListItem Value="sọc" Text="Sọc"></asp:ListItem>
                                        <asp:ListItem Value="tím" Text="Tím"></asp:ListItem>
                                        <asp:ListItem Value="tím cà" Text="Tím cà"></asp:ListItem>
                                        <asp:ListItem Value="tím đậm" Text="Tím đậm"></asp:ListItem>
                                        <asp:ListItem Value="tím xiêm" Text="Tím xiêm"></asp:ListItem>
                                        <asp:ListItem Value="trắng" Text="Trắng"></asp:ListItem>
                                        <asp:ListItem Value="trắng-đen" Text="Trắng-đen"></asp:ListItem>
                                        <asp:ListItem Value="trắng-đỏ" Text="Trắng-đỏ"></asp:ListItem>
                                        <asp:ListItem Value="trắng-xanh" Text="Trắng-xanh"></asp:ListItem>
                                        <asp:ListItem Value="vàng" Text="Vàng"></asp:ListItem>
                                        <asp:ListItem Value="vàng tươi" Text="Vàng tươi"></asp:ListItem>
                                        <asp:ListItem Value="vàng bò" Text="Vàng bò"></asp:ListItem>
                                        <asp:ListItem Value="vàng nghệ" Text="Vàng nghệ"></asp:ListItem>
                                        <asp:ListItem Value="vàng nhạt" Text="Vàng nhạt"></asp:ListItem>
                                        <asp:ListItem Value="xanh vỏ đậu" Text="Xanh vỏ đậu"></asp:ListItem>
                                        <asp:ListItem Value="xám" Text="Xám"></asp:ListItem>
                                        <asp:ListItem Value="xám chì" Text="Xám chì"></asp:ListItem>
                                        <asp:ListItem Value="xám chuột" Text="Xám chuột"></asp:ListItem>
                                        <asp:ListItem Value="xám nhạt" Text="Xám nhạt"></asp:ListItem>
                                        <asp:ListItem Value="xám tiêu" Text="Xám tiêu"></asp:ListItem>
                                        <asp:ListItem Value="xám xanh" Text="Xám xanh"></asp:ListItem>
                                        <asp:ListItem Value="xanh biển" Text="Xanh biển"></asp:ListItem>
                                        <asp:ListItem Value="xanh biển đậm" Text="Xanh biển đậm"></asp:ListItem>
                                        <asp:ListItem Value="xanh lá chuối" Text="Xanh lá chuối"></asp:ListItem>
                                        <asp:ListItem Value="xanh cổ vịt" Text="Xanh cổ vịt"></asp:ListItem>
                                        <asp:ListItem Value="xanh coban" Text="Xanh coban"></asp:ListItem>
                                        <asp:ListItem Value="xanh da" Text="Xanh da"></asp:ListItem>
                                        <asp:ListItem Value="xanh dạ quang" Text="Xanh dạ quang"></asp:ListItem>
                                        <asp:ListItem Value="xanh đen" Text="Xanh đen"></asp:ListItem>
                                        <asp:ListItem Value="xanh jean" Text="Xanh jean"></asp:ListItem>
                                        <asp:ListItem Value="xanh lá" Text="Xanh lá"></asp:ListItem>
                                        <asp:ListItem Value="xanh lá mạ" Text="Xanh lá mạ"></asp:ListItem>
                                        <asp:ListItem Value="xanh lính" Text="Xanh lính"></asp:ListItem>
                                        <asp:ListItem Value="xanh lông công" Text="Xanh lông công"></asp:ListItem>
                                        <asp:ListItem Value="xanh môn" Text="Xanh môn"></asp:ListItem>
                                        <asp:ListItem Value="xanh ngọc" Text="Xanh ngọc"></asp:ListItem>
                                        <asp:ListItem Value="xanh rêu" Text="Xanh rêu"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-xs-6">
                                    <asp:DropDownList ID="ddlSize" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="" Text="Size"></asp:ListItem>
                                        <asp:ListItem Value="s" Text="Size S"></asp:ListItem>
                                        <asp:ListItem Value="m" Text="Size M"></asp:ListItem>
                                        <asp:ListItem Value="l" Text="Size L"></asp:ListItem>
                                        <asp:ListItem Value="xl" Text="Size XL"></asp:ListItem>
                                        <asp:ListItem Value="xxl" Text="Size XXL"></asp:ListItem>
                                        <asp:ListItem Value="xxxl" Text="Size XXXL"></asp:ListItem>
                                        <asp:ListItem Value="27" Text="Size 27"></asp:ListItem>
                                        <asp:ListItem Value="28" Text="Size 28"></asp:ListItem>
                                        <asp:ListItem Value="29" Text="Size 29"></asp:ListItem>
                                        <asp:ListItem Value="30" Text="Size 30"></asp:ListItem>
                                        <asp:ListItem Value="31" Text="Size 31"></asp:ListItem>
                                        <asp:ListItem Value="32" Text="Size 32"></asp:ListItem>
                                        <asp:ListItem Value="33" Text="Size 33"></asp:ListItem>
                                        <asp:ListItem Value="34" Text="Size 34"></asp:ListItem>
                                        <asp:ListItem Value="35" Text="Size 35"></asp:ListItem>
                                        <asp:ListItem Value="36" Text="Size 36"></asp:ListItem>
                                        <asp:ListItem Value="37" Text="Size 37"></asp:ListItem>
                                        <asp:ListItem Value="38" Text="Size 38"></asp:ListItem>
                                        <asp:ListItem Value="39" Text="Size 39"></asp:ListItem>
                                        <asp:ListItem Value="40" Text="Size 40"></asp:ListItem>
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
                            </div>
                        </div>
                    </div>
                    <div class="filter-above-wrap clear">
                        <div class="filter-control action-button">
                            <div class="row">
                                <div class="col-md-12 col-xs-12">
                                    <a href="javascript:;" onclick="searchProduct()" class="btn primary-btn margin-right-15px"><i class="fa fa-search"></i> Lọc</a>
                                    <a href="/" class="btn primary-btn margin-right-15px"><i class="fa fa-times" aria-hidden="true"></i> Bỏ lọc</a>
                                    <a id="btnPostAllProductKiotViet" href="javascript:;" onclick="postALLProductKiotViet()" class="btn primary-btn margin-right-15px" disabled="disabled" readonly>
                                        <i class="fa fa-arrow-up"></i> Đồng bộ Kiotviet
                                    </a>
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
                            <table class="table table-checkable table-product all-product-table-2 shop_table_responsive">
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

        <script src="/App_Themes/Ann/js/copy-product-info-public.js?v=17052020"></script>
        <script src="/App_Themes/Ann/js/download-product-image.js?v=17052020"></script>
        <script src="/App_Themes/Ann/js/services/common/product-service.js?v=17052020"></script>
        
        <script type="text/javascript">
            $(document).ready(function() {
                $("#<%=txtSearchProduct.ClientID%>").keyup(function (e) {
                    if (e.keyCode == 13)
                    {
                        searchProduct();
                    }
                });

                $("td>input[type='checkbox']").change(function () {
                    let btnPostAllProductKiotViet = $("#btnPostAllProductKiotViet");

                    if (this.checked == true) {
                        btnPostAllProductKiotViet.removeAttr("disabled");
                        btnPostAllProductKiotViet.removeAttr("readonly");
                    }
                    else {
                        let $tdChecked = $("td>input[type='checkbox']:checked");

                        if ($tdChecked.length == 0) {
                            btnPostAllProductKiotViet.attr("disabled", true);
                            btnPostAllProductKiotViet.attr("readonly", true);
                        }

                    }
                })
            })
            

            // Parse URL Queries
            function url_query(query)
            {
                query = query.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
                var expr = "[\\?&]" + query + "=([^&#]*)";
                var regex = new RegExp(expr);
                var results = regex.exec(window.location.href);
                if (results !== null)
                {
                    return results[1];
                }
                else
                {
                    return false;
                }
            }

            var url_param = url_query('quantityfilter');
            if (url_param) {
                if (url_param == "greaterthan" || url_param == "lessthan")
                {
                    $(".greaterthan").removeClass("hide");
                    $(".between").addClass("hide");
                }
                else if (url_param == "between")
                {
                    $(".between").removeClass("hide");
                    $(".greaterthan").addClass("hide");
                }
            }


            function changeQuantityFilter(obj)
            {
                var value = obj.val();
                if (value == "greaterthan" || value == "lessthan")
                {
                    $(".greaterthan").removeClass("hide");
                    $(".between").addClass("hide");
                    $("#<%=txtQuantity.ClientID%>").focus().select();
                }
                else if (value == "between")
                {
                    $(".between").removeClass("hide");
                    $(".greaterthan").addClass("hide");
                    $("#<%=txtQuantityMin.ClientID%>").focus().select();
                }
            }

            function searchProduct()
            {
                let request = "?";

                let search = $("#<%=txtSearchProduct.ClientID%>").val();
                let stockstatus = $("#<%=ddlStockStatus.ClientID%>").val();
                let categoryid = $("#<%=ddlCategory.ClientID%>").val();
                let fromdate = $("#<%=rFromDate.ClientID%>").val();
                let todate = $("#<%=rToDate.ClientID%>").val();
                let quantityfilter = $("#<%=ddlQuantityFilter.ClientID%>").val();
                let quantity = $("#<%=txtQuantity.ClientID%>").val();
                let quantitymin = $("#<%=txtQuantityMin.ClientID%>").val();
                let quantitymax = $("#<%=txtQuantityMin.ClientID%>").val();
                let color = $("#<%=ddlColor.ClientID%>").val();
                let size = $("#<%=ddlSize.ClientID%>").val();
                let tag = $("#<%=ddlTag.ClientID%>").val();
                let orderby = $("#<%=ddlOrderBy.ClientID%>").val();

                if (search != "")
                {
                    request += "&textsearch=" + search;
                }

                if (stockstatus != "")
                {
                    request += "&stockstatus=" + stockstatus;
                }

                if (categoryid != "0")
                {
                    request += "&categoryid=" + categoryid;
                }

                if (fromdate != "")
                {
                    request += "&fromdate=" + fromdate;
                }

                if (todate != "")
                {
                    request += "&todate=" + todate;
                }

                if (quantityfilter != "")
                {
                    if (quantityfilter == "greaterthan" || quantityfilter == "lessthan")
                    {
                        request += "&quantityfilter=" + quantityfilter + "&quantity=" + quantity;
                    }

                    if (quantityfilter == "between")
                    {
                        request += "&quantityfilter=" + quantityfilter + "&quantitymin=" + quantitymin + "&quantitymax=" + quantitymax;
                    }
                }

                // Add filter valiable value
                if (color != "")
                {
                    request += "&color=" + color;
                }
                if (size != "")
                {
                    request += "&size=" + size;
                }

                // Add filter tag
                if (tag != "0")
                {
                    request += "&tag=" + tag;
                }

                // Add filter order by
                if (orderby != "")
                {
                    request += "&orderby=" + orderby;
                }

                window.open(request, "_self");
            }

            function changeCheckAll(checked) {
                let childDOM = $("td>input[type='checkbox']").not("[disabled='disabled']");

                // Button Post ALL Product KiotViet
                let btnPostAllProductKiotViet = $("#btnPostAllProductKiotViet");
                if (checked) {
                    btnPostAllProductKiotViet.removeAttr("disabled");
                    btnPostAllProductKiotViet.removeAttr("readonly");
                }
                else {
                    btnPostAllProductKiotViet.attr("disabled", true);
                    btnPostAllProductKiotViet.attr("readonly", true);
                }

                // Checkbox children
                childDOM.each((index, element) => {
                    element.checked = checked;
                });
            }

            function checkAll() {
                let parentDOM = $("#checkAll");
                let childDOM = $("td>input[type='checkbox']").not("[disabled='disabled']");
                if (childDOM.length == 0) {
                    parentDOM.prop('checked', false);

                }
                else {
                    childDOM.each((index, element) => {
                        parentDOM.prop('checked', element.checked);
                        
                        if (!element.checked) return false;
                    });
                }
            }

            function changeCheck(self) {

                // Hổ trợ xử lý check or uncheck
                checkAll();
            }

            function postALLProductKiotViet() {
                let $checkBox = $("td>input[type='checkbox']:checked");
                let products = [];

                $checkBox.each(function (index, element) {
                    let $tr = element.parentElement.parentElement;

                    products.push($tr.dataset.productsku);
                });

                let titleAlert = "Đồng bộ KiotViet";

                if (products.length == 0)
                    return _alterError(titleAlert, { message: "Chưa chọn sản phẩm nào!" });

                let dataJSON = JSON.stringify({ "productSKU": products.join(',') });

                $.ajax({
                    beforeSend: function () {
                        HoldOn.open();
                    },
                    method: 'POST',
                    contentType: 'application/json',
                    dataType: "json",
                    data: dataJSON,
                    url: "/api/v1/kiotviet/product",
                    success: (response, textStatus, xhr) => {
                        HoldOn.close();

                        if (xhr.status == 200) {
                            _alterSuccess(titleAlert, "Thành công");
                        } else {
                            _alterError(titleAlert);
                        }
                    },
                    error: (xhr, textStatus, error) => {
                        HoldOn.close();
                        _alterError(titleAlert, xhr.responseJSON);
                    }
                });
            }

            function postProductKiotViet(productSKU) {
                let titleAlert = "Đồng bộ KiotViet";

                if (!productSKU)
                    _alterError(titleAlert, { message: "Chưa chọn sản phẩm nào!" });

                let dataJSON = JSON.stringify({ "productSKU": productSKU });
                $.ajax({
                    beforeSend: function () {
                        HoldOn.open();
                    },
                    method: 'POST',
                    contentType: 'application/json',
                    dataType: "json",
                    data: dataJSON,
                    url: "/api/v1/zaloshop/product",
                    success: (response, textStatus, xhr) => {
                        HoldOn.close();

                        if (xhr.status == 200) {
                            _alterSuccess(titleAlert, "Sản phẩm <strong>" + productSKU + "</strong> đồng bộ thành công!");
                        } else {
                            _alterError(titleAlert);
                        }
                    },
                    error: (xhr, textStatus, error) => {
                        HoldOn.close();

                        _alterError(titleAlert, xhr.responseJSON);
                    }
                });
            }

            function _alterSuccess(title, message) {
                title = (typeof title !== 'undefined') ? title : 'Thông báo thành công';

                if (message === undefined) {
                    message = null;
                }

                return swal({
                    title: title,
                    text: message,
                    type: "success",
                    html: true
                });
            }

            function _alterError(title, responseJSON) {
                let message = '';
                title = (typeof title !== 'undefined') ? title : 'Thông báo lỗi';

                if (responseJSON === undefined || responseJSON === null) {
                    message = 'Đẫ có lỗi xãy ra.';
                }
                else {
                    if (responseJSON.message)
                        message += responseJSON.message;
                }

                return swal({
                    title: title,
                    text: message,
                    type: "error",
                    html: true
                });
            }
        </script>
    </main>
</asp:Content>
