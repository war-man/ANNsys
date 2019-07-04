<%@ Page Title="Danh sách chuyển khoản" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="danh-sach-chuyen-khoan.aspx.cs" Inherits="IM_PJ.danh_sach_chuyen_khoan" EnableSessionState="ReadOnly" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/moment.min.js"></script>
    <script src="/Scripts/moment-with-locales.min.js"></script>
    <script src="/Scripts/bootstrap-datetimepicker.min.js"></script>
    <style>
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
                    <h3 class="page-title left">Chuyển khoản <span>(<asp:Literal ID="ltrNumberOfOrder" runat="server" EnableViewState="false"></asp:Literal>)</span>
                    </h3>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="filter-above-wrap clear">
                        <div class="filter-control">
                            <div class="row">
                                <div class="col-md-5 col-xs-6">
                                    <asp:TextBox ID="txtSearchOrder" runat="server" CssClass="form-control" placeholder="Tìm đơn hàng" autocomplete="off"></asp:TextBox>
                                </div>
                                <div class="col-md-2 col-xs-6">
                                    <asp:DropDownList ID="ddlExcuteStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="" Text="Xử lý đơn"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Đang xử lý"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Đã hoàn tất"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Đã hủy"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-xs-6">
                                    <asp:DropDownList ID="ddlCreatedBy" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-xs-6">
                                    <asp:DropDownList ID="ddlCreatedDate" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="" Text="Thời gian đơn hàng"></asp:ListItem>
                                        <asp:ListItem Value="today" Text="Hôm nay"></asp:ListItem>
                                        <asp:ListItem Value="yesterday" Text="Hôm qua"></asp:ListItem>
                                        <asp:ListItem Value="beforeyesterday" Text="Hôm kia"></asp:ListItem>
                                        <asp:ListItem Value="week" Text="Tuần này"></asp:ListItem>
                                        <asp:ListItem Value="7days" Text="7 ngày"></asp:ListItem>
                                        <asp:ListItem Value="thismonth" Text="Tháng này"></asp:ListItem>
                                        <asp:ListItem Value="lastmonth" Text="Tháng trước"></asp:ListItem>
                                        <asp:ListItem Value="beforelastmonth" Text="Tháng trước nữa"></asp:ListItem>
                                        <asp:ListItem Value="30days" Text="30 ngày"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-1 col-xs-6 search-button">
                                    <a href="javascript:;" onclick="searchOrder()" class="btn primary-btn h45-btn"><i class="fa fa-search"></i></a>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn primary-btn h45-btn" OnClick="btnSearch_Click" Style="display: none" />
                                    <a href="/danh-sach-chuyen-khoan" class="btn primary-btn h45-btn"><i class="fa fa-times" aria-hidden="true"></i></a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="filter-above-wrap clear">
                        <div class="filter-control">
                            <div class="row">
                                <div class="col-md-1"></div>
                                <div class="col-md-2 col-xs-6">
                                    <asp:DropDownList ID="ddlTransferStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="" Text="Trạng thái tiền"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Đã nhận tiền"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Chưa nhận tiền"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-xs-6">
                                    <asp:DropDownList ID="ddlBankReceive" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-xs-6">
                                    <asp:DropDownList ID="ddlTransferDoneAt" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="" Text="Thời gian nhận tiền"></asp:ListItem>
                                        <asp:ListItem Value="today" Text="Hôm nay"></asp:ListItem>
                                        <asp:ListItem Value="yesterday" Text="Hôm qua"></asp:ListItem>
                                        <asp:ListItem Value="beforeyesterday" Text="Hôm kia"></asp:ListItem>
                                        <asp:ListItem Value="week" Text="Tuần này"></asp:ListItem>
                                        <asp:ListItem Value="7days" Text="7 ngày"></asp:ListItem>
                                        <asp:ListItem Value="thismonth" Text="Tháng này"></asp:ListItem>
                                        <asp:ListItem Value="lastmonth" Text="Tháng trước"></asp:ListItem>
                                        <asp:ListItem Value="beforelastmonth" Text="Tháng trước nữa"></asp:ListItem>
                                        <asp:ListItem Value="30days" Text="30 ngày"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-xs-6">
                                    <asp:DropDownList ID="ddlQuantityFilter" runat="server" CssClass="form-control" onchange="changeQuantityFilter($(this))">
                                        <asp:ListItem Value="" Text="Số lượng"></asp:ListItem>
                                        <asp:ListItem Value="greaterthan" Text="Lớn hơn"></asp:ListItem>
                                        <asp:ListItem Value="lessthan" Text="Nhỏ hơn"></asp:ListItem>
                                        <asp:ListItem Value="between" Text="Trong khoảng"></asp:ListItem>
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
                    <div class="panel-table clear">
                        <div class="panel-footer clear">
                            <div class="pagination">
                                <%this.DisplayHtmlStringPaging1();%>
                            </div>
                        </div>
                        <div class="responsive-table">
                            <table class="table table-checkable table-product table-new-product shop_table_responsive">
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

        <!-- Modal phí khác -->
        <div class="modal fade" id="feeInfoModal" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Các loại phí khác</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <table class="table table-striped">
                                <thead>
                                    <tr>
                                    <th>Tên loại phí</th>
                                    <th>Số tiền</th>
                                    </tr>
                                </thead>
                                <tbody id="feeInfo">
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Đóng</button>
                    </div>
                </div>
            </div>
        </div>

        <!-- Modal cập nhật chuyển khoản -->
        <div class="modal fade" id="TransferBankModal" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Cập nhật chuyển khoản</h4>
                    </div>
                    <div class="modal-body">
                        <asp:HiddenField ID="hdOrderID" runat="server" />
                        <div class="row form-group hide">
                            <div class="col-md-3 col-xs-4">
                                <p>Chuyển từ</p>
                            </div>
                            <div class="col-md-9 col-xs-8">
                                <asp:DropDownList ID="ddlCustomerBank" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-md-3 col-xs-4">
                                <p>Ngân hàng</p>
                            </div>
                            <div class="col-md-9 col-xs-8">
                                <asp:DropDownList ID="ddlAccoutBank" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-md-3 col-xs-4">
                                <p>Trạng thái</p>
                            </div>
                            <div class="col-md-9 col-xs-8">
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-md-3 col-xs-4">
                                <p>Tổng đơn</p>
                            </div>
                            <div class="col-md-9 col-xs-8 text-right">
                                <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control text-align-right" ReadOnly="True"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-md-3 col-xs-4">
                                <p>Đã nhận</p>
                            </div>
                            <div class="col-md-9 col-xs-8">
                                <asp:TextBox ID="txtMoneyReceived" runat="server" CssClass="form-control text-right" placeholder="Số tiền đã nhận" data-type="currency" onkeypress='return event.charCode >= 48 && event.charCode <= 57'></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-md-3 col-xs-4">
                                <p>Thời gian</p>
                            </div>
                            <div class="col-md-9 col-xs-8">
                                <div class="input-group date" id="dtDoneAt">
                                    <input type="text" class="form-control" />
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 col-xs-4">
                                <p>Ghi chú</p>
                            </div>
                            <div class="col-md-9 col-xs-8">
                                <asp:TextBox ID="txtNote" runat="server" CssClass="form-control text-left" placeholder="Ghi chú"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button id="closeTransfer" type="button" class="btn btn-default" data-dismiss="modal">Đóng</button>
                        <button id="updateTransfer" type="button" class="btn btn-primary">Lưu</button>
                    </div>
                </div>
            </div>
        </div>

        <script type="text/javascript">
            $(document).ready(() => {
                $("button[data-toggle='modal']").click(e => {
                    let row = e.currentTarget.parentNode.parentNode;
                    let modal = $("#TransferBankModal");
                    let orderIDDOM = modal.find("#<%=hdOrderID.ClientID%>");
                    let cusBankDOM = modal.find("#<%=ddlCustomerBank.ClientID%>");
                    let accBankDOM = modal.find("#<%=ddlAccoutBank.ClientID%>");
                    let priceDOM = modal.find("#<%=txtPrice.ClientID%>");
                    let moneyReceivedDOM = modal.find("#<%=txtMoneyReceived.ClientID%>");
                    let statusDOM = modal.find("#<%=ddlStatus.ClientID%>");
                    let pickerDOM = modal.find('#dtDoneAt');
                    let noteDOM = modal.find("#<%=txtNote.ClientID%>");

                    orderIDDOM.val(row.dataset["orderid"]);
                    pickerDOM.datetimepicker({
                        format: 'DD/MM/YYYY HH:mm',
                        date: new Date()
                    });

                    let status = row.dataset["statusid"];

                    // Đã nhận tiền
                    if (status && status == 1) {
                        moneyReceivedDOM.removeAttr("disabled");
                        cusBankDOM.val(row.dataset["cusbankid"]);
                        accBankDOM.val(row.dataset["accbankid"]);
                        // Money receive
                        if (row.dataset["moneyreceived"]) {
                            moneyReceivedDOM.val(formatThousands(row.dataset["moneyreceived"]));
                        }
                        pickerDOM.val(row.dataset["doneat"]);
                    }
                    else {
                        moneyReceivedDOM.val("");
                        moneyReceivedDOM.attr("disabled", true);
                    }

                    
                    priceDOM.val(formatThousands(row.dataset["price"]))

                    if (row.dataset["doneat"]){
                        pickerDOM.data("DateTimePicker").date(row.dataset["doneat"]);
                        statusDOM.val(status);
                    }
                    else {
                        statusDOM.val(1);
                        moneyReceivedDOM.val(priceDOM.val());
                        moneyReceivedDOM.attr("disabled", false);
                        pickerDOM.data("DateTimePicker").date(moment(new Date).format('DD/MM/YYYY HH:mm'));
                    }

                    // Note
                    noteDOM.val(row.dataset["transfernote"]);

                    if (!cusBankDOM.val() || !accBankDOM.val() || !noteDOM.val())
                    {
                        let data = {
                            'orderID': row.dataset["orderid"],
                            'cusID': row.dataset["cusid"]
                        }
                        $.ajax({
                            type: "POST",
                            url: "/danh-sach-chuyen-khoan.aspx/getTransferLast",
                            data: JSON.stringify(data),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (msg) {
                                var data = JSON.parse(msg.d);
                                if (data)
                                {
                                    // Customer Bank
                                    cusBankDOM.val(data.CusBankID);
                                    // Account Bank
                                    accBankDOM.val(data.AccBankID);
                                    // Note
                                    noteDOM.val(data.Note);
                                }
                            },
                            error: function (xmlhttprequest, textstatus, errorthrow) {
                                swal("Thông báo", "Đã có vấn đề lấy thông tin chuyển khoản", "error");
                            }
                        });
                    }
                })

                // Change status tien reset = 0
                $("#<%=ddlStatus.ClientID%>").change(e => {
                    let status = e.currentTarget.value;
                    let moneyReceivedDOM = $("#<%=txtMoneyReceived.ClientID%>");

                    if (status != 1)
                    {
                        moneyReceivedDOM.val("");
                        moneyReceivedDOM.attr("disabled", true);
                    }
                    else
                    {
                        let orderID = $("#<%=hdOrderID.ClientID%>").val();
                        let row = $("tr[data-orderid='" + orderID + "'")
                        if (row.data("moneyreceived")) {
                            moneyReceivedDOM.val(formatThousands(row.data("moneyreceived")));
                        }
                        moneyReceivedDOM.removeAttr("disabled");
                    }
                });

                $("#updateTransfer").click(e => {
                    let orderID = $("#<%=hdOrderID.ClientID%>").val();
                    let accBankID = $("#<%=ddlAccoutBank.ClientID%>").val();
                    let cusBankID = accBankID;
                    let status = $("#<%=ddlStatus.ClientID%>").val();
                    let money = $("#<%=txtMoneyReceived.ClientID%>").val().replace(/\,/g, '');
                    if (money == "") {
                        money = $("#<%=txtPrice.ClientID%>").val().replace(/\,/g, '');
                    }
                    let doneAt = $("#dtDoneAt").data('date');
                    let note = $("#<%=txtNote.ClientID%>").val();

                    let data = {
                        'OrderID': orderID,
                        'CusBankID': cusBankID,
                        'AccBankID': accBankID,
                        'Money': money ? money : 0,
                        'DoneAt': formatDateToInsert(doneAt),
                        'Status': status,
                        'Note': note
                    };

                    if (status == 1 && (cusBankID == 0 || accBankID == 0)) {
                        swal("Thông báo", "Chưa chọn ngân hàng", "error");
                    }
                    else {
                        $.ajax({
                            type: "POST",
                            url: "/danh-sach-chuyen-khoan.aspx/updateTransfer",
                            data: JSON.stringify({'transfer': data}),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            beforeSend: function () {
                                HoldOn.open();
                            },
                            success: function (msg) {
                                let row = $("tr[data-orderid='" + orderID + "']");
                                let statusNameDOM = row.find('#statusName');
                                let cusBankName = $("#<%=ddlCustomerBank.ClientID%> :selected").text();
                                let accBankName = $("#<%=ddlAccoutBank.ClientID%> :selected").text();
                                let statusName = $("#<%=ddlStatus.ClientID%> :selected").text();

                                // Update screen
                                row.attr("data-cusbankid", cusBankID);
                                row.attr("data-cusbankname", cusBankName);
                                row.attr("data-accbankid", accBankID);
                                row.attr("data-accbankname", accBankName);
                                row.attr("data-statusid", status);
                                row.attr("data-statusname", statusName);
                                row.attr("data-moneyreceived", money);
                                row.attr("data-doneat", doneAt);
                                row.attr("data-transfernote", note);
                                if ($("#<%=ddlCustomerBank.ClientID%>").val() != 0) {
                                    row.find('#cusBankName').html(cusBankName);
                                }
                                if ($("#<%=ddlAccoutBank.ClientID%>").val() != 0) {
                                    row.find('#accBankName').html(accBankName);
                                }
                            
                                statusNameDOM.children("span").html(statusName);

                                // Đã nhận tiền
                                if (status == 1)
                                {
                                    statusNameDOM.children("span").removeClass();
                                    statusNameDOM.children("span").addClass("bg-green");
                                    if (money)
                                    {
                                        row.find('#moneyReceive').html('<strong>' + formatThousands(money) + '</strong>');
                                    }
                                    else
                                    {
                                        row.find('#moneyReceive').html("");
                                    }
                                    row.find('#doneAt').html(formatDate(doneAt));
                                }
                                else
                                {
                                    statusNameDOM.children("span").removeClass();
                                    statusNameDOM.children("span").addClass("bg-red");
                                    row.find('#moneyReceive').html("");
                                    row.find('#doneAt').html("");
                                }

                                $("#closeTransfer").click();
                                HoldOn.close();
                            },
                            error: function (xmlhttprequest, textstatus, errorthrow) {
                                swal("Thông báo", "Đã có vấn đề trong việc cập nhật thông tin chuyển khoản", "error");
                            }
                        });
                    }
                    
                });
            });

            function formatDateToInsert(dateString) {
                var date = dateString.split(' ');
                var datetmp = date[0].split('/');
                var hourtmp = date[1].split(':');

                return datetmp[2] + '-' + datetmp[1] + '-' + datetmp[0] + ' ' + hourtmp[0] + ':' + hourtmp[1] + ':00';
            }

            function formatDate(dateString) {
                var date = dateString.split(' ');
                var datetmp = date[0].split('/');
                var hourtmp = date[1].split(':');

                return datetmp[0] + '/' + datetmp[1] + ' ' + hourtmp[0] + ':' + hourtmp[1];
            }

            // Parse URL Queries
            function url_query(query) {
                query = query.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
                var expr = "[\\?&]" + query + "=([^&#]*)";
                var regex = new RegExp(expr);
                var results = regex.exec(window.location.href);
                if (results !== null) {
                    return results[1];
                } else {
                    return false;
                }
            }

            var url_param = url_query('quantityfilter');
            if (url_param) {
                if (url_param == "greaterthan" || url_param == "lessthan") {
                    $(".greaterthan").removeClass("hide");
                    $(".between").addClass("hide");
                }
                else if (url_param == "between") {
                    $(".between").removeClass("hide");
                    $(".greaterthan").addClass("hide");
                }
            }

            function searchOrder() {
                $("#<%= btnSearch.ClientID%>").click();
            }

            var formatThousands = function (n, dp) {
                var s = '' + (Math.floor(n)), d = n % 1, i = s.length, r = '';
                while ((i -= 3) > 0) { r = ',' + s.substr(i, 3) + r; }
                return s.substr(0, i + 3) + r +
                    (d ? '.' + Math.round(d * Math.pow(10, dp || 2)) : '');
            };

            function isNumber(evt) {
                evt = (evt) ? evt : window.event;
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                return true;
            }

            // Jquery Dependency
            $("input[data-type='currency']").on({
                keyup: function () {
                    formatCurrency($(this));
                },
                blur: function () {
                    formatCurrency($(this), "blur");
                }
            });


            function formatNumber(n) {
                // format number 1000000 to 1,234,567
                return n.replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",")
            }


            function formatCurrency(input, blur) {
                // appends $ to value, validates decimal side
                // and puts cursor back in right position.

                // get input value
                var input_val = input.val();

                // don't validate empty input
                if (input_val === "") { return; }

                // original length
                var original_len = input_val.length;

                // initial caret position 
                var caret_pos = input.prop("selectionStart");

                // check for decimal
                if (input_val.indexOf(".") >= 0) {

                    // get position of first decimal
                    // this prevents multiple decimals from
                    // being entered
                    var decimal_pos = input_val.indexOf(".");

                    // split number by decimal point
                    var left_side = input_val.substring(0, decimal_pos);
                    var right_side = input_val.substring(decimal_pos);

                    // add commas to left side of number
                    left_side = formatNumber(left_side);

                    // validate right side
                    right_side = formatNumber(right_side);

                    // On blur make sure 2 numbers after decimal
                    if (blur === "blur") {
                        right_side += "00";
                    }

                    // Limit decimal to only 2 digits
                    right_side = right_side.substring(0, 2);

                    // join number by .
                    input_val = left_side;

                } else {
                    // no decimal entered
                    // add commas to number
                    // remove all non-digits
                    input_val = formatNumber(input_val);
                    input_val = input_val;
                }

                // send updated string to input
                input.val(input_val);

                // put caret back in the right position
                var updated_len = input_val.length;
                caret_pos = updated_len - original_len + caret_pos;
                input[0].setSelectionRange(caret_pos, caret_pos);
            }

            function createFeeInfoHTML(fee, is_total) {
                if (!is_total) {
                    is_total = false;
                }
                let addHTML = "";

                if (is_total) {
                    addHTML += "<tr class='info'>";
                    addHTML += "    <td style='text-align: right'>" + fee.FeeTypeName + "</td>";
                    addHTML += "    <td>" + formatThousands(fee.FeePrice) + "</td>";
                    addHTML += "</tr>";
                }
                else {
                    addHTML += "<tr>";
                    addHTML += "    <td>" + fee.FeeTypeName + "</td>";
                    addHTML += "    <td>" + formatThousands(fee.FeePrice) + "</td>";
                    addHTML += "</tr>";
                }

                return addHTML;
            }

            function openFeeInfoModal(orderID) {
                let tbodyDOM = $("tbody[id='feeInfo']");
                // Clear body
                tbodyDOM.html("");
                $.ajax({
                    type: "POST",
                    url: "/danh-sach-don-hang.aspx/getFeeInfo",
                    data: JSON.stringify({ 'orderID': orderID }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: (response) => {
                        if (response.d) {
                            let data = JSON.parse(response.d);
                            let feeTotal = 0;
                            data.forEach((item) => {
                                feeTotal += item.FeePrice;
                                tbodyDOM.append(createFeeInfoHTML(item));
                            });
                            tbodyDOM.append(createFeeInfoHTML({ "FeeTypeName": "Tổng", "FeePrice": feeTotal }, true));
                        }
                    },
                    error: (xmlhttprequest, textstatus, errorthrow) => {
                        swal("Thông báo", "Có lỗi trong quá trình lấy thông tin phí", "error");
                    }
                })
            }
        </script>
    </main>
</asp:Content>
