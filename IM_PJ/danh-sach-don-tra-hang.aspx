<%@ Page Title="Danh sách đổi trả hàng" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="danh-sach-don-tra-hang.aspx.cs" Inherits="IM_PJ.danh_sach_don_tra_hang" EnableSessionState="ReadOnly" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        table.shop_table_responsive > tbody > tr:nth-of-type(2n+1) td {
            border-bottom: solid 1px #e1e1e1!important;
        }
        @media (max-width: 768px) {
            table.shop_table_responsive thead {
	            display: none;
            }
            table.shop_table_responsive > tbody > tr > td:nth-of-type(1):before {
                content: "#";
                font-size: 20px;
                margin-right: 2px;
            }
            table.shop_table_responsive > tbody > tr > td:nth-of-type(1) {
                text-align: left;
                font-size: 20px;
                font-weight: bold;
                height: 50px;
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
                    <h3 class="page-title left">Danh sách đổi trả hàng <span>(<asp:Literal ID="ltrNumberOfOrder" runat="server" EnableViewState="false"></asp:Literal> đơn)</span></h3>
                    <div class="right above-list-btn">
                        <a href="/tao-don-hang-doi-tra" class="h45-btn primary-btn btn">Thêm mới</a>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="filter-above-wrap clear">
                        <div class="filter-control">
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtSearchOrder" runat="server" CssClass="form-control" placeholder="Tìm đơn hàng" autocomplete="off"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0">Trạng thái</asp:ListItem>
                                        <asp:ListItem Value="1">Chưa trừ tiền</asp:ListItem>
                                        <asp:ListItem Value="2">Đã trừ tiền</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlRefundFee" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="">Phí đổi hàng</asp:ListItem>
                                        <asp:ListItem Value="yes">Có phí</asp:ListItem>
                                        <asp:ListItem Value="no">Miễn phí</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlCreatedDate" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="" Text="Tất cả thời gian"></asp:ListItem>
                                        <asp:ListItem Value="today" Text="Hôm nay"></asp:ListItem>
                                        <asp:ListItem Value="yesterday" Text="Hôm qua"></asp:ListItem>
                                        <asp:ListItem Value="beforeyesterday" Text="Hôm kia"></asp:ListItem>
                                        <asp:ListItem Value="week" Text="Tuần này"></asp:ListItem>
                                        <asp:ListItem Value="7days" Text="7 ngày"></asp:ListItem>
                                        <asp:ListItem Value="thismonth" Text="Tháng này"></asp:ListItem>
                                        <asp:ListItem Value="lastmonth" Text="Tháng trước"></asp:ListItem>
                                        <asp:ListItem Value="30days" Text="30 ngày"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlCreatedBy" runat="server" CssClass="form-control create"></asp:DropDownList>
                                </div>
                                <div class="col-md-1">
                                    <a href="javascript:;" onclick="searchAgent()" class="btn primary-btn h45-btn"><i class="fa fa-search"></i></a>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn primary-btn h45-btn" OnClick="btnSearch_Click" Style="display: none" />
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
                            <table class="table table-checkable table-product shop_table_responsive">
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
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panelborderheading">
                        <div class="panel-heading clear">
                            <h3 class="page-title left not-margin-bot">Thống kê đơn hàng đổi trả</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row pad">
                                <div class="col-md-4">
                                    <label class="left pad10">Tổng số đơn hàng: </label>
                                    <div class="ordertype">
                                        <asp:Literal ID="ltrTotalOrders" runat="server"></asp:Literal>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <label class="left pad10">Số đơn đã trừ tiền: </label>
                                    <div class="ordercreateby">
                                        <asp:Literal ID="ltrType2Orders" runat="server"></asp:Literal>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <label class="left pad10">Số đơn chưa trừ tiền: </label>
                                    <div class="ordercreatedate">
                                        <asp:Literal ID="ltrType1Orders" runat="server"></asp:Literal>
                                    </div>
                                </div>
                                
                            </div>
                            <div class="row pad">
                                <div class="col-md-4"> 
                                    <label class="left pad10">Tổng sản phẩm: </label>
                                    <div class="ordernote">
                                        <asp:Literal ID="ltrTotalProducts" runat="server"></asp:Literal>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <label class="left pad10">Tổng số tiền: </label>
                                    <div class="orderquantity">
                                        <asp:Literal ID="ltrTotalMoney" runat="server"></asp:Literal>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <label class="left pad10">Tổng phí đổi hàng: </label>
                                    <div class="ordertotalprice">
                                        <asp:Literal ID="ltrTotalRefundFee" runat="server"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="printOrder" style="display: none">
            <asp:Literal ID="ltrPrintOrder" runat="server"></asp:Literal>
        </div>
        <script type="text/javascript">

            function searchAgent() {
                $("#<%= btnSearch.ClientID%>").click();
            }

            function printDiv(divid) {
                var divToPrint = document.getElementById('' + divid + '');
                var newWin = window.open('', 'Print-Window');
                newWin.document.open();
                newWin.document.write('<html><head><link rel="stylesheet" href="/App_Themes/Ann/hoadon/hoadon.css" type="text/css"/><link rel="stylesheet" href="/App_Themes/Ann/barcode/style.css" type="text/css"/><link rel="stylesheet" href="/App_Themes/Ann/css/responsive.css" type="text/css"/></head><body onload="window.print()">' + divToPrint.innerHTML + '</body></html>');
                newWin.document.close();
                setTimeout(function () { newWin.close(); }, 10);
            }

            function printOrder(id) {
                $.ajax({
                    type: "POST",
                    url: "/danh-sach-don-tra-hang.aspx/getOrder",
                    data: "{ID:'" + id + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var data = JSON.parse(msg.d)
                        if (data != null) {
                            var productPrint = "";
                            var shtml = "";

                            productPrint += " <div class=\"body\">";
                            productPrint += " <div class=\"table-1\">";
                            productPrint += " <table>";
                            productPrint += "<colgroup >";
                            productPrint += "<col class=\"col-left\"/>";
                            productPrint += "<col class=\"col-right\"/>";
                            productPrint += "</colgroup>";
                            productPrint += " <tbody>";
                            productPrint += " <tr>";
                            productPrint += " <td>Mã đơn hàng</td>";
                            productPrint += " <td>" + data.ID + "</td>";
                            productPrint += " </tr>";
                            productPrint += "  <tr>";
                            productPrint += "  <td>Ngày tạo</td>";

                            productPrint += " <td>" + data.CreateDate + "</td>";
                            productPrint += "</tr>";

                            productPrint += "<tr>";
                            productPrint += "   <td>Trạng thái</td>";
                            if (data.Status != null)
                                productPrint += "<td>" + data.Status + "</td>";
                            else
                                productPrint += "<td></td>";

                            productPrint += "</tr>";

                            productPrint += " <tr>";
                            productPrint += " <td>Nhân viên</td>";
                            productPrint += " <td>" + data.CreatedBy + "</td>";
                            productPrint += "</tr>";
                            productPrint += "<tr>";
                            productPrint += "  <td>Khách hàng</td>";
                            productPrint += "  <td>" + data.CustomerName + "</td>";
                            productPrint += " </tr>";
                            productPrint += "<tr>";
                            productPrint += " <td>Điện thoại</td>";
                            productPrint += " <td>" + data.CustomerPhone + "</td>";
                            productPrint += "</tr>";

                            productPrint += " </tbody>";
                            productPrint += "</table>";
                            productPrint += "         </div>";

                            productPrint += "<div class=\"table-2\">";
                            productPrint += " <table>";
                            productPrint += " <colgroup>";
                            productPrint += "<col class=\"stt\" />";
                            productPrint += "<col class=\"sanpham\" />";
                            productPrint += "<col class=\"soluong\" />";
                            productPrint += "<col class=\"gia\" />";
                            productPrint += "<col class=\"phi\"/>";
                            productPrint += "<col class=\"tong\"/>";
                            productPrint += "</colgroup>";
                            productPrint += "<thead>";
                            productPrint += " <th>#</th>";
                            productPrint += "<th>Sản phẩm</th>";
                            productPrint += "<th>Số lượng</th>";
                            productPrint += "<th>Giá</th>";
                            productPrint += "<th>Phí</th>";
                            productPrint += "<th>Tổng</th>";
                            productPrint += " </thead>";
                            productPrint += "<tbody>";
                            var t = 0;

                            var listproduct = data.ListProduct.split('*');
                            for (var i = 0; i < listproduct.length - 1; i++) {
                                var value = listproduct[i].split(';');

                                //product
                                productPrint += "<tr>";
                                t += parseFloat(value[3]);
                                productPrint += "<td>" + (i + 1) + "</td>";
                                productPrint += "<td>" + value[0] + " - " + value[1] + " - " + value[2] + "</td> ";
                                productPrint += "<td>" + value[3] + "</td>";
                                productPrint += "<td>" + formatThousands(value[4], ",") + "</td>";
                                productPrint += "<td>" + formatThousands(value[5], ",") + "</td>";
                                var k = parseFloat(value[3]) * parseFloat(value[4]);
                                productPrint += "<td> " + formatThousands(k, ",") + "</td>";

                                productPrint += "</tr>";
                                //product

                            }



                            productPrint += "<td colspan =\"5\" > Số lượng </td>";
                            productPrint += "<td>" + data.TotalQuantity + "</td>";
                            productPrint += "</tr>";
                            productPrint += " <tr>";
                            productPrint += " <td colspan =\"5\"> Phí đổi hàng </td>";
                            productPrint += " <td>" + formatThousands(data.TotalRefundPrice, ",") + "</td>";
                            productPrint += " </tr>";

                            productPrint += " <tr>";
                            productPrint += "<td class=\"strong\" colspan=\"5\">Tổng tiền</td>";
                            productPrint += " <td class=\"strong\">" + formatThousands(data.TotalPrice, ",") + "</td>";
                            productPrint += " </tr>";
                            productPrint += "</tbody>";
                            productPrint += " </table>";
                            productPrint += "</div>";
                            productPrint += "         </div>";

                            shtml += "<div class=\"hoadon\">";
                            shtml += "<div class=\"all\">";
                            shtml += "<div class=\"head\">";
                            shtml += "     <div class=\"logo\"><div class=\"img\"><img src=\"App_Themes/Ann/image/logo.png\" alt=\"\" /></div></div>";
                            shtml += "<div class=\"info\">";
                            shtml += "<div class=\"ct\">";
                            var agent = data.ListAgent.split('|');
                            shtml += "<div class=\"ct-title\">Địa chỉ:</div>";
                            shtml += "<div class=\"ct-detail\">" + agent[0] + "</div>";
                            shtml += "<div class=\"ct\">";
                            shtml += "<div class=\"ct-title\">Điện thoại/ Zalo:</div>";
                            shtml += "<div class=\"ct-detail\">";

                            shtml += "<a href = \"tel:+\" >" + agent[1] + "</a></div>";
                            //html += "<a href=\"tel:+\">0913268406</a> -";
                            //html += "<a href = \"tel:+\" > 0936786404 </a>";

                            shtml += "<div class=\"ct\">";
                            shtml += "<div class=\"ct-title\">Facebook:</div>";
                            shtml += "<div class=\"ct-detail\">";
                            shtml += "<a href =\"https://facebook.com/bosiquanao.net\" target=\"_blank\" >https://facebook.com/bosiquanao.net</a>";
                            shtml += "</div>";
                            shtml += "</div>";
                            shtml += "<div class=\"ct\">";
                            shtml += "<div class=\"ct-title\">Website:</div>";
                            shtml += "<div class=\"ct-detail\">";
                            shtml += "<a href =\"\">ann.com.vn</a> ";
                            shtml += "</div> ";
                            shtml += "</div> ";
                            shtml += "</div> ";
                            shtml += "</div>";
                            shtml += "</div>";
                            shtml += "<div class=\"refund\">ĐƠN HÀNG TRẢ</div>";

                            shtml += productPrint;

                            shtml += "<div class=\"footer\"><h1> Cảm ơn quý khách </h ></div> ";
                            shtml += "</div>";

                            shtml += "</div>";

                        }
                        $("#printOrder").html(shtml);

                        printDiv('printOrder');

                    }
                });
            }

            var formatThousands = function (n, dp) {
                var s = '' + (Math.floor(n)), d = n % 1, i = s.length, r = '';
                while ((i -= 3) > 0) { r = ',' + s.substr(i, 3) + r; }
                return s.substr(0, i + 3) + r +
                    (d ? '.' + Math.round(d * Math.pow(10, dp || 2)) : '');
            };
        </script>
    </main>
</asp:Content>
