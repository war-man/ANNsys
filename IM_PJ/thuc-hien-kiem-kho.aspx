<%@ Page Title="Thực hiện kiểm kho" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="thuc-hien-kiem-kho.aspx.cs" Inherits="IM_PJ.thuc_hien_kiem_kho" EnableSessionState="ReadOnly" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- DataTable -->
    <!-- https://datatables.net/manual/installation -->
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.20/css/dataTables.bootstrap4.min.css" type="text/css">
    <link rel="stylesheet" href="https://cdn.datatables.net/fixedheader/3.1.6/css/fixedHeader.bootstrap4.min.css" type="text/css">

    <style>
        .swal-modal .swal-text {
            text-align: center;
        }

        table {
            border-collapse: collapse;
            width: 100%;
        }

        .table > thead > tr > .history-index {
            width: 5%;
        }

        .table > thead > tr > .history-product-name {
            width: 45%;
        }

        .table > thead > tr > .history-sku {
            width: 25%;
        }

        .table > thead > tr > .history-quantity {
            width: 10%;
        }

        .table > thead > tr > .history-date {
            width: 15%;
        }

        .pagination li a {
            padding: 6px 12px;
            color: #428bca;
        }
        .table > thead > tr > th {
            height: inherit;
            padding: 15px 5px;
        }

        @media only screen and (max-width: 800px) {
            .none-padding-right {
                padding-right: 0px;
            }

            .none-padding-horizontal {
                padding-left: 0px;
                padding-right: 0px;
            }

            .table > thead > tr > .history-index {
                width: 10%;
            }

            .table > thead > tr > .history-product-name {
                display: none;
            }

            .table > thead > tr > .history-sku {
                width: 40%;
            }

            .table > thead > tr > .history-quantity {
                width: 20%;
            }

            .table > thead > tr > .history-date {
                width: 30%;
            }

            .table > tbody > tr > td:nth-child(2) {
                display: none;
            }
            .table > tbody > tr > td {
                padding: 0px 5px;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="row margin-bottom-15">
                <div class="col-sm-4 col-xs-8 none-padding-right">
                    <asp:DropDownList ID="dllCheckWarehouse" runat="server" CssClass="form-control select2" Width="100%"></asp:DropDownList>
                </div>
                <div class="col-sm-1 col-xs-4">
                    <a id="btnExecute" class="btn primary-btn fw-btn" href="javascript:;" onclick="execute()">
                        <i class="fa fa-paper-plane" aria-hidden="true"></i> Kiểm
                    </a>
                </div>
            </div>
            <div id="body" hidden>
                <div class="row margin-bottom-15">
                        <div class="col-sm-4 col-xs-8 none-padding-right">
                            <input type="text" id="txtSKU" class="form-control" placeholder="Nhập mã sản phẩm">
                        </div>
                        <div class="col-sm-1 col-xs-4">
                            <a href="javascript:;" id="btnSearch" onclick="searchProduct()" class="btn primary-btn fw-btn">
                                <i class="fa fa-search"></i> Tìm
                            </a>
                        </div>
                    </div>
                    <div class="row margin-bottom-15 body_update" hidden>
                        <div class="col-sm-2 col-xs-4 none-padding-horizontal">
                            <div class="col-sm-6 col-xs-12" style="height: 40px; padding-top: 10px">
                                <label>Số lượng cũ</label>
                            </div>
                            <div class="col-sm-6 col-xs-12">
                                <input type="number" id="txtQuantityOld" class="form-control" disabled="disabled" readonly>
                            </div>
                        </div>
                        <div class="col-sm-2 col-xs-4 none-padding-horizontal">
                            <div class="col-sm-6 col-xs-12" style="height: 40px; padding-top: 10px">
                                <label>Số lượng mới</label>
                            </div>
                            <div class="col-sm-6 col-xs-12">
                                <input type="number" id="txtQuantityNew" class="form-control">
                            </div>
                        </div>
                        <div class="col-sm-1 col-xs-4 body_update" hidden>
                        <a href="javascript:;" onclick="updateQuantity()" class="btn primary-btn btn-red fw-btn" tabindex="6">
                            <i class="fa fa-refresh"></i> Lưu
                        </a>
                    </div>
                    </div>
                    
                </div>
            </div>
            <div class="container staff-history hidden">
                <div class="row">
                    <div class="col-xs-12">
                        <table id="tbStaffHistories" class="table table-striped table-bordered">
                            <thead>
                                <tr>
                                    <th class="text-center history-index">#</th>
                                    <th class="text-center history-product-name">Sản phẩm</th>
                                    <th class="text-center history-sku">Mã</th>
                                    <th class="text-center history-quantity">SL mới</th>
                                    <th class="text-center history-date">Ngày</th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </main>

    <!-- DataTable -->
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.20/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.20/js/dataTables.bootstrap4.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/fixedheader/3.1.6/js/dataTables.fixedHeader.min.js"></script>

    <script>
        $(document).ready(function () {
            if ($(window).innerWidth() < 800) {
                $('.nav-toggle').removeClass('open');
                $('body').removeClass('menuin');
            }

            initSearch();
            initQuantity();
        });

        function getAccount() {
            let staffName = $.cookie("usernameLoginSystem");

            return staffName;
        }

        function initSearch() {
            $('#txtSKU').keypress((event) => {
                if (event.charCode === 13) {
                    searchProduct();
                }
            });
        }

        function initQuantity() {
            $('#txtQuantityNew').keypress((event) => {
                if (event.charCode === 13) {
                    updateQuantity();
                }
            });
        }

        function execute() {
            let $dllCheckWarehouse = $('#<%= dllCheckWarehouse.ClientID %>');

            if ($dllCheckWarehouse.val() == "")
                return swal({
                    title: 'Thông báo',
                    text: 'Vui lòng chọn phiên kiểm kho',
                    type: 'warning'
                }, function () {
                    $dllCheckWarehouse.select2('open');
                });;

            // Drop downlist Check Warehouse
            $dllCheckWarehouse.attr('disabled', true);
            $dllCheckWarehouse.attr('readonly', true);

            // Button Execute
            let $btnExecute = $('#btnExecute');

            $btnExecute.attr('disabled', true);
            $btnExecute.attr('readonly', true);

            $('#body').removeAttr('hidden');
            $('#txtSKU').focus();
            initStaffHistories();
            $(".staff-history").removeClass('hidden');
        }

        function initStaffHistories() {
            let account = getAccount();
            let checkID = +$('#<%= dllCheckWarehouse.ClientID %>').val() || 0;
            let url = 'api/v1/check-warehouse/' + checkID + '/staff-histories/data-table';

            tbStaffHistories = $('#tbStaffHistories').DataTable({
                autoWidth: false,
                lengthChange: false,
                searching: false,
                ordering: false,
                fixedHeader: true,
                pagingType: "first_last_numbers",
                pageLength: 20,
                serverSide: true,
                ajax: {
                    method: 'GET',
                    url: url,
                    headers: {
                        'staff': account
                    },
                    data: (params) => {
                        var query = {
                            draw: params.draw,
                            page: Math.trunc(params.start / params.length) + 1,
                            pageSize: params.length
                        }

                        return query;
                    },
                    dataSrc: "data"
                },
                columns: [
                  { data: 'index' },
                  { data: 'productName' },
                  { data: 'sku' },
                  { data: 'quantity' },
                  { data: 'checkedDate' }
                ],
            });
        }

        function searchProduct() {
            let account = getAccount();
            let checkID = +$('#<%= dllCheckWarehouse.ClientID %>').val() || 0;
            let sku = $('#txtSKU').val()

            if (!account || !checkID || !sku)
                return

            let titleAlert = 'Tìm kiếm sản phẩm';
            let url = 'api/v1/check-warehouse/' + checkID + '/check-product/' + sku;

            $.ajax({
                method: 'GET',
                url: url,
                headers: {
                    'staff': account
                },
                success: (data, textStatus, xhr) => {
                    if (xhr.status === 200) {
                        // input text search
                        $('#txtSKU').attr('disabled', true);
                        $('#btnSearch').attr('disabled', true);

                        // show element update the quantity of the product
                        $(".body_update").each((index, elem) => {
                            elem.removeAttribute('hidden');
                        })

                        $("#txtQuantityOld").val(data.quantityOld);
                        $("#txtQuantityNew").val(data.quantityNew);
                        $('#txtQuantityNew').focus();
                    }
                    else {
                        alter400(titleAlert);
                    }
                },
                error: (xhr, textStatus, error) => {
                    if (xhr.status === 400) {
                        alter400(titleAlert, xhr.responseJSON)
                    }
                    else if (xhr.status === 403) {
                        alert403(titleAlert);
                    }
                    else {
                        alter400(titleAlert);
                    }
                }
            })
        }

        function updateQuantity() {
            let account = getAccount();
            let checkID = +$('#<%= dllCheckWarehouse.ClientID %>').val() || 0;
            let sku = $('#txtSKU').val()
            let quantity = $('#txtQuantityNew').val() || "";

            if (!account || !checkID || !sku)
                return;

            if (isBlank(quantity) || +quantity < 0) {
                return swal({
                    title: "Thông báo",
                    text: "Vui lòng nhập số lượng sản phẩm",
                    icon: "warning",
                }, function () { $('#txtQuantityNew').focus(); });
            }
            else {
                quantity = +quantity || 0;
            }

            let titleAlert = 'Cập số lượng sản phẩm';
            let url = 'api/v1/check-warehouse/' + checkID + '/update-quantity';

            $.ajax({
                method: 'POST',
                url: url,
                headers: {
                    'staff': account
                },
                contentType: 'application/json',
                dataType: "json",
                data: JSON.stringify({ sku: sku, quantity: quantity }),
                success: (data, textStatus, xhr) => {
                    if (xhr.status === 200) {
                        // refresh Histories Table 
                        tbStaffHistories.ajax.reload();

                        // hidden element update the quantity of the product
                        $(".body_update").each((index, elem) => {
                            elem.hidden = true;
                        })

                        $("#productName").html('');

                        // search
                        $('#txtSKU').attr('disabled', false);
                        $('#txtSKU').val('');
                        $('#txtSKU').html('');
                        $('#txtSKU').focus();
                        $('#btnSearch').attr('disabled', false);
                    }
                    else {
                        alter400(title = titleAlert);
                    }
                },
                error: (xhr, textStatus, error) => {
                    if (xhr.status === 400) {
                        alter400(titleAlert, xhr.responseJSON)
                    }
                    else if (xhr.status === 403) {
                        alert403(titleAlert);
                    }
                    else {
                        alter400(titleAlert);
                    }
                }
            })
        }

        function alter400(title, responseJSON) {
            let message = '';
            title = (typeof title !== 'undefined') ? title : 'Thông báo lỗi';

            if (responseJSON === undefined || responseJSON === null) {
                message = 'Đẫ có lỗi xãy ra.';
            }
            else {
                if (responseJSON.message)
                    message += responseJSON.message;
                if (responseJSON.error) {
                    if (responseJSON.error.checkID)
                        message += (message ? '\r\n' : '') + responseJSON.error.checkID;
                    if (responseJSON.error.sku)
                        message += (message ? '\r\n' : '') + responseJSON.error.sku;
                    if (responseJSON.error.quantity)
                        message += (message ? '\r\n' : '') + responseJSON.error.quantity;
                }
            }

            swal({
                title: title,
                text: message,
                icon: "error",
            });
        }

        function alert403(title) {
            title = (typeof title !== 'undefined') ? title : 'Thông báo lỗi';

            swal({
                title: title,
                text: "Thời gian đăng nhập đã hết.\r\nVui lòng đăng nhập lại",
                icon: "warning",
            }, function () { document.location = 'http://hethongann.com/dang-nhap'; });
        }
    </script>
</asp:Content>
