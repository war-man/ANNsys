<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="thuc-hien-kiem-kho.aspx.cs" Inherits="IM_PJ.thuc_hien_kiem_kho" EnableSessionState="ReadOnly" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Sản phẩm ANN</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1, user-scalable=yes">
    <meta name="format-detection" content="telephone=no">
    <meta name="robots" content="noindex, nofollow">

    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css"
        integrity="sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh" crossorigin="anonymous">

    <!-- Add icon library -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">

    <!-- Add Select2 library -->
    <!-- https://select2.org/getting-started/installation -->
    <link href="https://cdn.jsdelivr.net/npm/select2@4.0.13/dist/css/select2.min.css" rel="stylesheet" />

    <!-- Add CSS -->
    <link rel="stylesheet" href="/App_Themes/Ann/css/style.css?v=02042020" type="text/css">
    <link rel="stylesheet" href="/App_Themes/Ann/css/style-P.css?v=02042020" type="text/css">
    <link rel="stylesheet" href="/App_Themes/Ann/css/HoldOn.css?v=02042020" type="text/css">
    <link rel="stylesheet" href="/App_Themes/Ann/css/style-sp.css?v=02042020" type="text/css">
    <link rel="stylesheet" href="/App_Themes/NewUI/js/sweet/sweet-alert.css" type="text/css">

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

        .table > thead > tr > .history-name {
            width: 90%;
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

        .pagination li a {
            padding: 6px 12px;
            color: #428bca;
        }
    </style>
</head>
<body>
    <form id="form12" runat="server" enctype="multipart/form-data">
        <asp:ScriptManager runat="server" ID="scr">
        </asp:ScriptManager>
        <div>
            <main>
                <div class="container">
                    <div class="row">
                        <div class="col-xs-4">
                            <div class="row">
                                <a href="/sp" class="btn btn-menu primary-btn h45-btn btn-product">
                                    <i class="fa fa-sign-in" aria-hidden="true"></i>Sản phẩm
                                </a>
                            </div>
                        </div>
                        <div class="col-xs-4">
                            <div class="row">
                                <a href="/bv" class="btn btn-menu primary-btn h45-btn btn-post">
                                    <i class="fa fa-sign-in" aria-hidden="true"></i>Bài viết
                                </a>
                            </div>
                        </div>
                        <div class="col-xs-4">
                            <div class="row">
                                <a href="/dang-ky-nhap-hang" class="btn btn-menu primary-btn h45-btn btn-order">
                                    <i class="fa fa-cart-plus" aria-hidden="true"></i>Đặt hàng
                                </a>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-4">
                            <div class="row">
                                <a href="/kh" class="btn primary-btn h45-btn btn-customer">
                                    <i class="fa fa-sign-in" aria-hidden="true"></i>Khách hàng
                                </a>
                            </div>
                        </div>
                        <div class="col-xs-4">
                            <div class="row">
                                <a href="/bc" class="btn primary-btn h45-btn btn-report">
                                    <i class="fa fa-sign-in" aria-hidden="true"></i>Báo cáo
                                </a>
                            </div>
                        </div>
                        <div class="col-xs-4">
                            <div class="row">
                                <a href="/nhan-vien-dat-hang" class="btn primary-btn h45-btn btn-list-order">
                                    <i class="fa fa-cart-plus" aria-hidden="true"></i>DS đặt hàng
                                </a>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xl-12">
                            <div class="filter-above-wrap clear">
                                <div class="filter-control">
                                    <div class="row">
                                        <div class="col-md-9 col-xs-12">
                                            <div class="row">
                                                <div class="col-md-6 col-xs-12 margin-bottom-15">
                                                    <input type="text" id="txtStaffName" class="form-control" disabled="disabled" readonly>
                                                </div>
                                                <div class="col-md-6 col-xs-12 margin-bottom-15">
                                                    <select id="dllCheckWarehouse" class="form-control">
                                                    </select>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3 col-xs-12">
                                            <div class="row">
                                                <div class="col-xs-6">
                                                    <a href="javascript:;" id="btnExecute" onclick="execute()" class="btn primary-btn h45-btn">
                                                        <i class="fa fa-search"></i>Kiểm kho
                                                    </a>
                                                </div>
                                                <div class="col-xs-6">
                                                    <a href="/thuc-hien-kiem-kho" class="btn primary-btn h45-btn download-btn">
                                                        <i class="fa fa-times" aria-hidden="true"></i>Làm lại
                                                    </a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="body" class="row" hidden>
                        <div class="col-md-4 col-xs-12 margin-bottom-15">
                            <div class="row">
                                <div class="col-md-8 col-xs-8">
                                    <input type="text" id="txtSKU" class="form-control">
                                </div>
                                <div class="col-md-4 col-xs-4">
                                    <a href="javascript:;" id="btnSearch" onclick="searchProduct()" class="btn primary-btn h45-btn">
                                        <i class="fa fa-search"></i>Tìm kiếm
                                    </a>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6 col-xs-12 margin-bottom-15 body_update" hidden>
                            <div class="row">
                                <div class="col-md-9 col-xs-9">
                                    <label id="productName"></label>
                                </div>
                                <div class="col-md-3 col-xs-3">
                                    <input type="number" id="txtQuantity" class="form-control" tabindex="5">
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2 col-xs-12 body_update" hidden>
                            <a href="javascript:;" onclick="updateQuantity()" class="btn primary-btn h45-btn" tabindex="6">
                                <i class="fa fa-refresh"></i>Cập nhật
                            </a>
                        </div>
                    </div>
                    <div class="row" data-show-histories="false">
                        <div class="col-xs-12 margin-bottom-15">
                            <h4>Lịch sử kiểm kho</h4>
                        </div>
                    </div>
                    <div class="row" data-show-histories="false">
                        <div class="col-xl-12">
                            <table id="tbStaffHistories" class="table table-striped table-bordered">
                                <thead>
                                    <tr>
                                        <th class="text-center history-index" rowspan="2">#</th>
                                        <th class="text-center history-name" colspan="3">Kiểm kho</th>
                                    </tr>
                                    <tr>
                                        <th class="text-center history-sku">SKU</th>
                                        <th class="text-center history-quantity">Số lượng</th>
                                        <th class="text-center history-date">Ngày</th>
                                    </tr>
                                </thead>
                            </table>
                        </div>
                    </div>
                </div>


            </main>

            <!-- Optional JavaScript -->
            <!-- jQuery first, then Popper.js, then Bootstrap JS -->
            <!-- <script src="https://code.jquery.com/jquery-3.4.1.slim.min.js"
    integrity="sha384-J6qa4849blE2+poT4WnyKhv5vZF5SrPo0iEjwBvKU7imGFAV0wwj1yYfoRSJoZ+n"
    crossorigin="anonymous"></script> -->
            <!-- Fix bug: Select2 chạy được trên jquery 3.3.1 -->
            <script src="https://code.jquery.com/jquery-3.3.1.min.js"
                integrity="sha256-FgpCb/KJQlLNfOu91ta32o/NMZxltwRo8QtmkMRdAu8="
                crossorigin="anonymous"></script>
            <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js"
                integrity="sha384-Q6E9RHvbIyZFJoft+2mJbHaEWldlvI9IOYy5n3zV9zzTtmI3UksdQRVvoxMfooAo"
                crossorigin="anonymous"></script>
            <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"
                integrity="sha384-wfSDF2E50Y2D1uUdj0O3uMBJnjuUD4Ih7YwaYd1iqfktj0Uod8GCExl3Og8ifwB6"
                crossorigin="anonymous"></script>
            <!-- Sweet Alert -->
            <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
            <!-- Select2 -->
            <script src="https://cdn.jsdelivr.net/npm/select2@4.0.13/dist/js/select2.min.js"></script>
            <!-- Cookie -->
            <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-cookie/1.4.1/jquery.cookie.min.js"></script>
            <!-- DataTable -->
            <script type="text/javascript" src="https://cdn.datatables.net/1.10.20/js/jquery.dataTables.min.js"></script>
            <script type="text/javascript" src="https://cdn.datatables.net/1.10.20/js/dataTables.bootstrap4.min.js"></script>
            <!-- <script type="text/javascript" src="https://cdn.datatables.net/fixedheader/3.1.6/js/dataTables.fixedHeader.min.js"></script> -->

            <script>
                let tbStaffHistories;

                $(document).ready(() => {
                    let account = getAccount();

                    // init page
                    initCheckWarehouseSelect2();
                    initStaffHistories();
                    initSearch();
                    initQuantity();

                    $('#txtStaffName').val(account);
                    $('#dllCheckWarehouse').select2('open');
                })

                function getAccount () {
                    let staffName = $.cookie("loginHiddenPage");

                    return staffName;
                }

                function initCheckWarehouseSelect2() {
                    $('#dllCheckWarehouse').select2({
                        placeholder: 'Danh sách kiểm kho',
                        ajax: {
                            delay: 500,
                            method: 'POST',
                            url: '/thuc-hien-kiem-kho.aspx/getSelect2',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            data: (params) => {
                                var query = {
                                    search: params.term || null
                                }

                                return JSON.stringify(query);
                            },
                            processResults: (dataJSON) => {
                                let data = JSON.parse(dataJSON.d);
                                if (!data)
                                    return {
                                        results: []
                                    };

                                return { results: data.results }
                            }
                        }
                    });
                }

                function initStaffHistories() {
                    let account = getAccount();

                    tbStaffHistories = $('#tbStaffHistories').DataTable({
                        autoWidth: false,
                        lengthChange: false,
                        searching: false,
                        ordering: false,
                        // fixedHeader: true,
                        pagingType: "first_last_numbers",
                        pageLength: 10,
                        serverSide: true,
                        ajax: {
                            method: 'POST',
                            url: '/thuc-hien-kiem-kho.aspx/getStaffHistories',
                            headers: {
                                'staff': account
                            },
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            data: (params) => {
                                var query = {
                                    draw: params.draw,
                                    page: Math.trunc(params.start / params.length) + 1,
                                    pageSize: params.length
                                }

                                return JSON.stringify(query);
                            },
                            dataSrc: (dataJSON) => {
                                let response = JSON.parse(dataJSON.d);
                                
                                return response.data;
                            }
                        },
                        columns: [
                          { data: 'index'},
                          { data: 'sku'},
                          { data: 'quantity'},
                          { data: 'checkedDate'}
                        ],
                        createdRow: (row, data, dataIndex) => {
                            let classNew = (data.index - 1) % 2 === 0 ? '#f8f8f8' : '#fff';

                            $(row).removeAttr("class");
                            $(row).css('background-color', classNew);

                            if (data.row === 1) {
                              $('td:eq(0)', row).attr('rowspan', 2).css('background-color', classNew);
                              $('td:eq(1)', row).attr('colspan', 3).css('background-color', classNew).html(data.checkedName);
                              $('td:eq(2)', row).remove();
                              $('td:eq(2)', row).remove();
                            }
                            else if (data.row === 2) {
                                $('td:eq(0)', row).remove();
                                $('td:eq(0)', row).css('background-color', classNew);
                                $('td:eq(1)', row).addClass('text-center').css('background-color', classNew);
                                $('td:eq(2)', row).addClass('text-center').css('background-color', classNew);
                            } else {
                                $(row).remove();
                            }
                        }
                    });
                }
  
                function initSearch() {
                    $('#txtSKU').keypress((event) => {
                        if (event.charCode === 13) {
                            searchProduct();
                        }
                    });
                }

                function initQuantity() {
                    $('#txtQuantity').keypress((event) => {
                        if (event.charCode === 13) {
                            updateQuantity();
                        }
                    });
                }

                function execute() {
                    $('#dllCheckWarehouse').attr('disabled', true);
                    $('#btnExecute').attr('disabled', true);
                    $('#body').removeAttr('hidden');
                    $('#txtSKU').focus();
                }

                function searchProduct() {
                    let account = getAccount();
                    let checkID = +$('#dllCheckWarehouse').val() || 0;
                    let sku = $('#txtSKU').val()

                    if (!account || !checkID || !sku)
                        return

                    let titleAlert = 'Tìm kiếm sản phẩm';
      
                    $.ajax({
                        method: 'POST',
                        url: '/thuc-hien-kiem-kho.aspx/checkProduct',
                        headers: {
                            'staff': account
                        },
                        headers: {
                            'staff': account
                        },
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify({checkID: checkID, sku: sku}),
                        success: (dataJSON, textStatus, xhr) => {
                            let response = JSON.parse(dataJSON.d);

                            if (!response)
                                alter400(titleAlert);

                            if (response.statusCode === 200) {
                                // input text search
                                $('#txtSKU').attr('disabled', true);
                                $('#btnSearch').attr('disabled', true);

                                // show element update the quantity of the product
                                $(".body_update").each((index, elem) => {
                                    elem.removeAttribute('hidden');
                                })

                                $("#productName").html(sku + ' - ' + response.data.ProductTitle);
                                $('#txtQuantity').focus();
                            }
                            else if (response.statusCode === 204) {
                                alter204(titleAlert, 'Thông báo không tìm thấy sản phẩm');
                            }
                            else if (response.statusCode === 400) {
                                alter400(titleAlert, response);
                            }
                            else if (response.statusCode === 403) {
                                alter403(titleAlert);
                            }
                            else {
                                alter400(titleAlert);
                            }
                        },
                        error: (xhr, textStatus, error) => {
                            alter400(titleAlert);
                        }
                    })
                }

                function updateQuantity() {
                    let account = getAccount();
                    let checkID = +$('#dllCheckWarehouse').val() || 0;
                    let sku = $('#txtSKU').val()
                    let quantity = +$('#txtQuantity').val() || -1;

                    if (!account || !checkID || !sku || quantity < 0)
                        return;

                    let titleAlert = 'Cập số lượng sản phẩm';
      
                    $.ajax({
                        method: 'POST',
                        url: '/thuc-hien-kiem-kho.aspx/updateQuantity',
                        headers: {
                            'staff': account
                        },
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify({ checkID: checkID, sku: sku, quantity: quantity }),
                        success: (dataJSON, textStatus, xhr) => {
                            let response = JSON.parse(dataJSON.d);

                            if (!response)
                                alter400(titleAlert);

                            if (response.statusCode === 200) {
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
                            else if (response.statusCode === 204) {
                                alter204(titleAlert, 'Thông báo không tìm thấy sản phẩm');
                            }
                            else if (response.statusCode === 400) {
                                alter400(titleAlert, response);
                            }
                            else if (response.statusCode === 403) {
                                alter403(titleAlert);
                            }
                            else {
                                alter400(title=titleAlert);
                            }
                        },
                        error: (xhr, textStatus, error) => {
                            alter400(titleAlert);
                        }
                    })
                }

                function alter204(title, message) {
                    title = (typeof title !== 'undefined') ? title : 'Thông báo lỗi';
                    message = (typeof message !== 'undefined') ? message : '';

                    swal({
                        title: title,
                        text: message,
                        icon: "error",
                    });
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
                    }).then((value) => { document.location = 'http://hethongann.com/login-hidden-page'; });
                }
            </script>
        </div>
    </form>
</body>
</html>
