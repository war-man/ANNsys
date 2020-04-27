<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dang-ky-ghtk.aspx.cs" Inherits="IM_PJ.dang_ky_ghtk" EnableSessionState="ReadOnly" %>

<!DOCTYPE html>
<html>
<head runat="server">
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

    <title>Đăng ký Giao Hàng Tiết Kiệm</title>
    <style>
        table {
            border-collapse: collapse;
            width: 100%;
        }

        .table > tbody > .odd {
            background-color: rgba(0,0,0,.05);
        }

        .table > tbody > tr > .index {
            width: 10%;
            text-align: center;
            vertical-align: middle;
        }

        .table > tbody > tr > .product-name {
            width: 90%;
        }

        .table > tbody > tr > .label-weight {
            width: 10%;
            text-align: center;
            vertical-align: middle;
        }

        .table > tbody > tr > .input-weight {
            width: 45%;
        }

        .table > tbody > tr > .label-weight-unit {
            width: 10%;
            text-align: center;
            vertical-align: middle;
        }

        .table > tbody > tr > .label-number {
            width: 10%;
            text-align: center;
            vertical-align: middle;
        }

        .table > tbody > tr > .input-number {
            width: 25%;
            vertical-align: middle;
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
                        <div class="col-xl-12">
                            <h2>Nhập thông tin đơn hàng</h2>
                        </div>
                    </div>
                    <div class="row mb-5">
                        <div class="col-12 col-xl-6">
                            <div class="form-group">
                                <h4>Thông tin người nhận hàng</h4>
                            </div>
                            <div class="form-group">
                                <div class="input-group mb-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text" id="inputGroup-tel"><i class="fa fa-phone"></i></span>
                                    </div>
                                    <input type="text" id="tel" class="form-control" aria-label="Sizing example input"
                                        aria-describedby="inputGroup-tel" placeholder="Nhập SĐT">
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="input-group mb-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text" id="inputGroup-name"><i class="fa fa-user"></i></span>
                                    </div>
                                    <input type="text" id="name" class="form-control" aria-label="Sizing example input"
                                        aria-describedby="inputGroup-name" placeholder="Tên khách hàng">
                                </div>
                            </div>
                            <div class="form-group">
                                <select id="ddlProvince" class="form-control">
                                </select>
                            </div>
                            <div class="form-group">
                                <select id="ddlDistrict" class="form-control">
                                </select>
                            </div>
                            <div class="form-group">
                                <select id="ddlWard" class="form-control">
                                </select>
                            </div>
                            <div class="form-group">
                                <div class="input-group mb-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text" id="inputGroup-address"><i class="fa fa-home"></i></span>
                                    </div>
                                    <input type="text" id="address" class="form-control" aria-label="Sizing example input"
                                        aria-describedby="inputGroup-address" placeholder="Địa chỉ chi tiết (nhà/ngõ/đường...)">
                                </div>
                            </div>
                            <br />
                            <div class="form-group">
                                <h4>Thông tin lấy hàng</h4>
                            </div>
                            <div class="form-group row">
                                <div class="col-xl-4">
                                    <label>Hình thức gửi hàng</label>
                                </div>
                                <div class="col-xl-8">
                                    <div class="custom-control custom-radio custom-control-inline">
                                        <input type="radio" id="shipment_shop" name="shipment_type" class="custom-control-input" value="shop" checked="checked">
                                        <label class="custom-control-label" for="shipment_shop">Lấy hàng tận nơi</label>
                                    </div>
                                    <div class="custom-control custom-radio custom-control-inline">
                                        <input type="radio" id="shipment_post_office" name="shipment_type" class="custom-control-input" value="post_office">
                                        <label class="custom-control-label" for="shipment_post_office">Gửi hàng bưu cục</label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-xl-4">
                                    <label>Địa chỉ lấy hàng</label>
                                </div>
                                <div class="col-xl-8">
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text" id="inputGroup-pick-address"><i class="fa fa-home"></i></span>
                                        </div>
                                        <input type="text" id="pick_address" class="form-control" aria-label="Sizing example input"
                                            aria-describedby="inputGroup-pick-address" disabled="disabled" readonly>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pick_work_shift" class="col-5 col-xl-4 col-form-label">Dự kiến lấy hàng</label>
                                <div class="col-7 col-xl-8">
                                    <select id="pick_work_shift" class="form-control"></select>
                                </div>
                            </div>
                            <%--<div class="form-group row">
                                <label for="deliver_work_shift" class="col-5 col-xl-4 col-form-label">Dự kiến giao hàng</label>
                                <div class="col-7 col-xl-8">
                                    <select id="deliver_work_shift" class="form-control"></select>
                                </div>
                            </div>--%>
                        </div>
                        <div class="col-12 col-xl-6">
                            <div class="form-group">
                                <h4>Thông tin hàng hoá</h4>
                            </div>
                            <div class="form-group">
                                <table class="table table-bordered">
                                    <tbody>
                                        <tr class="odd">
                                            <td class="index">1</td>
                                            <td class="product-name" colspan="4">
                                                <select id="ddlProduct" class="form-control"></select>
                                            </td>
                                        </tr>
                                        <tr class="odd">
                                            <td class="label-weight">KL</td>
                                            <td class="input-weight">
                                                <input type="number" id="weight" class="form-control text-right">
                                            </td>
                                            <td class="label-weight-unit">KG</td>
                                            <td class="label-number">SL</td>
                                            <td class="input-number text-right">1</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <div class="form-group row">
                                <div class="col-xl-12">
                                    <label>Hình thức vận chuyển (Đường bộ)</label>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-3 col-xl-3">
                                    <label>Phí ship</label>
                                </div>
                                <div class="col-9 col-xl-9">
                                    <div class="row">
                                        <div class="col-5 col-xl-3">
                                            <label id="feeship">0 VND</label>
                                        </div>
                                        <div class="col-7 col-xl-9">
                                            <div class="custom-control custom-radio custom-control-inline">
                                                <input type="radio" id="feeship_shop" name="feeship" class="custom-control-input" value="1" checked="checked">
                                                <label class="custom-control-label" for="feeship_shop">Shop trả</label>
                                            </div>
                                            <div class="custom-control custom-radio custom-control-inline">
                                                <input type="radio" id="feeship_receiver" name="feeship" class="custom-control-input" value="0">
                                                <label class="custom-control-label" for="feeship_receiver">Khách trả</label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="divFeeShop" class="form-group row">
                                <div class="col-3 col-xl-3">
                                    <label>Phí đã nhập</label>
                                </div>
                                <div class="col-9 col-xl-9">
                                    <div class="row">
                                        <div class="col-5 col-xl-3">
                                            <label id="labelFeeShop">0 VND</label>
                                        </div>
                                        <div class="col-7 col-xl-9">
                                            <div class="custom-control custom-radio custom-control-inline">
                                                <input type="radio" id="feeShop" name="feeship" class="custom-control-input" value="2">
                                                <label class="custom-control-label" for="feeShop">Đã nhập</label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="pick_money" class="col-4 col-xl-3 col-form-label">Tiền thu hộ</label>
                                <div class="input-group col-8 col-xl-9">
                                    <input type="text" id="pick_money" class="form-control text-right" value="0" disabled="disabled" readonly>
                                    <div class="input-group-append">
                                        <span class="input-group-text">VND</span>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="value" class="col-4 col-xl-3 col-form-label">Giá trị hàng</label>
                                <div class="input-group col-8 col-xl-9">
                                    <input type="text" id="value" class="form-control text-right" value="0" disabled="disabled" readonly>
                                    <div class="input-group-append">
                                        <span class="input-group-text">VND</span>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label for="total_money" class="col-4 col-xl-3 col-form-label">Tổng tiền thu</label>
                                <div class="input-group col-8 col-xl-9">
                                    <input type="text" id="total_money" class="form-control text-right" value="0" disabled="disabled" readonly>
                                    <div class="input-group-append">
                                        <span class="input-group-text">VND</span>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="input-group col-xl-12">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">Mã đơn KH</span>
                                    </div>
                                    <input type="text" id="client_id" class="form-control" disabled="disabled" readonly>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="input-group col-xl-12">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text"><i class="fa fa-file-o"></i></span>
                                    </div>
                                    <input type="text" id="note" class="form-control" maxlength="120" placeholder="Hãy thêm thông tin để GHTK phục vụ tốt hơn.">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="fixed-bottom">
                        <div class="form-group row">
                            <div class="col-xl-12">
                                <button class="form-control btn-primary" onclick="_checkSubmit()">Đăng ký đơn hàng (F3)</button>
                            </div>
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

            <script>
                let _feeShipment, // Dùng để lấy trạng thái trước của radio Shipment
                  _fee,
                  _feeShop,
                  _order,
                  _weight_min,
                  _product,
                  _personal,
                  _postOffice;

                $(document).ready(function () {
                    _initParameterLocal();
                    _initReceiveInfo();
                    _initReceiverAddress();
                    _onChangeReceiverAddress();
                    _initShipment();
                    _initTableProduct();
                    _initFee();
                    _initNote();
                    _initPage();
                });

                $(document).bind('keydown', function (e) {
                    if (e.which === 114)
                        return _checkSubmit();
                })

                function _initParameterLocal() {
                    // Fee Ship
                    _feeShipment = 1;
                    _fee = 0;
                    _feeShop = 0;

                    // Order
                    _order = {
                        id: null,
                        pick_name: null,
                        pick_money: 0,
                        pick_address_id: null,
                        pick_address: null,
                        pick_province: null,
                        pick_district: null,
                        pick_ward: null,
                        pick_street: null,
                        pick_tel: null,
                        pick_email: null,
                        name: null,
                        address: null,
                        province_id: null,
                        province: null,
                        district_id: null,
                        district: null,
                        ward_id: null,
                        ward: null,
                        street: null,
                        tel: null,
                        note: null,
                        email: null,
                        use_return_address: 0,
                        is_freeship: _feeShipment == 0 ? 0 : 1,
                        weight_option: 'kilogram',
                        pick_work_shift: null,
                        //deliver_work_shift: null,
                        pick_date: null,
                        //deliver_date: null,
                        value: 0,
                        opm: null,
                        pick_option: null,
                        actual_transfer_method: 'road',
                        transport: 'road'
                    }

                    // Product Table
                    _weight_min = 0.1;
                    _product = {
                        name: null,
                        weight: _weight_min,
                    }
                }

                function _initReceiveInfo() {
                    $("#tel").change(function () {
                        _order.tel = $(this).val();
                    })

                    $("#name").change(function () {
                        _order.name = $(this).val();
                    })

                    $("#address").change(function () {
                        _order.address = $(this).val();
                    })
                }

                function _initReceiverAddress() {
                    // Danh sách tỉnh / thành phố
                    $('#ddlProvince').select2({
                        placeholder: '(Bấm để chọn tỉnh / thành phố)',
                        ajax: {
                            delay: 500,
                            method: 'GET',
                            url: '/api/v1/delivery-save/provinces/select2',
                            data: (params) => {
                                var query = {
                                    search: params.term,
                                    page: params.page || 1
                                }

                                return query;
                            }
                        }
                    });

                    // Danh sách quận / huyện
                    $('#ddlDistrict').attr('disabled', true);
                    $('#ddlDistrict').attr('readonly', 'readonly');
                    $('#ddlDistrict').select2({ placeholder: '(Bấm để chọn quận / huyện)' });

                    // Danh sách phường / xã
                    $('#ddlWard').attr('disabled', true);
                    $('#ddlWard').attr('readonly', 'readonly');
                    $('#ddlWard').select2({ placeholder: '(Bấm để chọn phường / xã)' });
                }

                function _onChangeReceiverAddress() {
                    // // Danh sách tỉnh / thành phố
                    $('#ddlProvince').on('select2:select', (e) => {
                        let data = e.params.data;

                        // Danh sách quận / huyện
                        $('#ddlDistrict').removeAttr('disabled');
                        $('#ddlDistrict').removeAttr('readonly');
                        $('#ddlDistrict').val(null).trigger('change');
                        $('#ddlDistrict').select2({
                            placeholder: '(Bấm để chọn tỉnh / thành phố)',
                            ajax: {
                                delay: 500,
                                method: 'GET',
                                url: '/api/v1/delivery-save/province/' + data.id + '/districts/select2',
                                data: (params) => {
                                    var query = {
                                        search: params.term,
                                        page: params.page || 1
                                    }

                                    return query;
                                }
                            }
                        });
                        $('#ddlDistrict').select2('open');

                        // Danh sách phường / xã
                        $('#ddlWard').attr('disabled', true);
                        $('#ddlWard').attr('readonly', 'readonly');
                        $('#ddlWard').val(null).trigger('change');
                        $('#ddlWard').select2({ placeholder: '(Bấm để chọn phường / xã)' });

                        // Cập nhật order
                        _order.province_id = data.id;
                        _order.province = data.text;
                    });

                    // Danh sách quận / huyện
                    $('#ddlDistrict').on('select2:select', (e) => {
                        let data = e.params.data;

                        // Danh sách quận / huyện
                        $('#ddlWard').removeAttr('disabled');
                        $('#ddlWard').removeAttr('readonly');
                        $('#ddlWard').val(null).trigger('change');
                        $('#ddlWard').select2({
                            placeholder: '(Bấm để chọn phường / xã)',
                            ajax: {
                                delay: 500,
                                method: 'GET',
                                url: '/api/v1/delivery-save/district/' + data.id + '/wards/select2',
                                data: (params) => {
                                    var query = {
                                        search: params.term,
                                        page: params.page || 1
                                    }

                                    return query;
                                }
                            }
                        });
                        $('#ddlWard').select2('open');

                        // Cập nhật order
                        _order.district_id = data.id;
                        _order.district = data.text;
                        // Tính tiền phí Ship
                        _calculateFee();
                    })

                    // Danh sách quận / huyện
                    $('#ddlWard').on('select2:select', (e) => {
                        let data = e.params.data;

                        // Cập nhật order
                        _order.ward_id = data.id;
                        _order.ward = data.text;
                        // Tính tiền phí Ship
                        _calculateFee();
                    });
                }

                function _initShipment() {
                    $('input:radio[name="shipment_type"]').change(function () {

                        if ($(this).val() == 'shop') {
                            if (_personal) {
                                return _onChangeShipment('shop')
                            }

                            let titleAlert = 'Lấy thông tin cá nhân của shop';

                            $.ajax({
                                method: 'GET',
                                url: '/api/v1/delivery-save/personal',
                                success: (data, textStatus, xhr) => {
                                    if (xhr.status == 200 && data) {
                                        _personal = data;
                                        return _onChangeShipment('shop');
                                    } else {
                                        _alterError(titleAlert);
                                    }
                                },
                                error: (xhr, textStatus, error) => {
                                    _alterError(titleAlert);
                                }
                            });
                        } else {
                            if (_postOffice) {
                                return _onChangeShipment('post_office');
                            }

                            let titleAlert = 'Lấy thông tin của buc cục Giao Hàng Tiết Kiệm';

                            $.ajax({
                                method: 'GET',
                                url: '/api/v1/delivery-save/post-office',
                                success: (data, textStatus, xhr) => {
                                    if (xhr.status == 200 && data) {
                                        _postOffice = data;
                                        _onChangeShipment('post_office');
                                    } else {
                                        _alterError(titleAlert);
                                    }
                                },
                                error: (xhr, textStatus, error) => {
                                    _alterError(titleAlert);
                                }
                            });
                        }
                    });

                    // Cài đặt drop dơwn list pick_work_shift
                    let now = new Date();
                    let strNow = "";
                    let hours = now.getHours();
                    let pick_work_shift = [];

                    strNow = now.toISOString().substring(0, 10).replace(/-/g, '/');

                    if (hours <= 11) {
                        pick_work_shift.push({ id: 2, text: "Chiều nay" });
                        pick_work_shift.push({ id: 3, text: "Tối nay" });
                        pick_work_shift.push({ id: 1, text: "Sáng mai" });

                        _order.pick_date = strNow;
                        _order.pick_work_shift = 2;
                    }
                    else if (hours <= 15) {
                        pick_work_shift.push({ id: 3, text: "Tối nay" });
                        pick_work_shift.push({ id: 1, text: "Sáng mai" });
                        pick_work_shift.push({ id: 2, text: "Chiều mai" });

                        _order.pick_date = strNow;
                        _order.pick_work_shift = 3;
                    }
                    else {
                        pick_work_shift.push({ id: 1, text: "Sáng mai" });
                        pick_work_shift.push({ id: 2, text: "Chiều mai" });
                        pick_work_shift.push({ id: 3, text: "Tối mai" });

                        let tomorrow = new Date();
                        tomorrow.setDate(tomorrow.getDate() + 1);
                        strTomorrow = tomorrow.toISOString().substring(0, 10).replace(/-/g, '/');
                        _order.pick_date = strTomorrow;
                        _order.pick_work_shift = 1;
                    }

                    $("#pick_work_shift").select2({
                        minimumResultsForSearch: Infinity,
                        data: pick_work_shift
                    });
                    //$("#deliver_work_shift").select2({
                    //    allowClear: true,
                    //});
                    _onChangePickWorkShift(pick_work_shift[0].text);


                    $('#pick_work_shift').on('select2:select', function (e) {
                        let data = e.params.data;

                        _onChangePickWorkShift(data.text);
                    });

                    //$('#deliver_work_shift').on('select2:select', function (e) {
                    //    let data = e.params.data;
                    //    let now = new Date();
                    //    let tomorrow = new Date();
                    //    let dateAfterTomorrow = new Date();
                    //    let strNow = "";
                    //    let strTomorrow = "";
                    //    let strDateAfterTomorrow = "";

                    //    tomorrow.setDate(tomorrow.getDate() + 1);
                    //    dateAfterTomorrow.setDate(tomorrow.getDate() + 2);
                    //    strNow = now.toISOString().substring(0, 10).replace(/-/g, '/');
                    //    strTomorrow = tomorrow.toISOString().substring(0, 10).replace(/-/g, '/');
                    //    strDateAfterTomorrow = dateAfterTomorrow.toISOString().substring(0, 10).replace(/-/g, '/');

                    //    switch (data.text) {
                    //        case "Tối nay":
                    //            _order.deliver_date = _order.pick_date
                    //            _order.deliver_work_shift = 3;
                    //            break;
                    //        case "Sáng mai":
                    //            _order.deliver_date = strTomorrow;
                    //            _order.deliver_work_shift = 1;
                    //            break;
                    //        case "Chiều mai":
                    //            _order.deliver_date = strTomorrow;
                    //            _order.deliver_work_shift = 2;
                    //            break;
                    //        case "Tối mai":
                    //            _order.deliver_date = strTomorrow;
                    //            _order.deliver_work_shift = 3;
                    //            break;
                    //        case "Sáng ngày kia":
                    //            _order.deliver_date = strDateAfterTomorrow;
                    //            _order.deliver_work_shift = 1;
                    //            break;
                    //        case "Chiều ngày kia":
                    //            _order.deliver_date = strDateAfterTomorrow;
                    //            _order.deliver_work_shift = 2;
                    //            break;
                    //        case "Tối ngày kia":
                    //            _order.deliver_date = strDateAfterTomorrow;
                    //            _order.deliver_work_shift = 3;
                    //            break;
                    //        default:
                    //            break;
                    //    }
                    //});
                }

                function _initTableProduct() {
                    // https://select2.org/searching#single-select
                    $("#ddlProduct").select2({
                        placeholder: 'Tên sản phẩm',
                        minimumResultsForSearch: Infinity,
                        ajax: {
                            method: 'GET',
                            url: '/api/v1/delivery-save/products/select2'
                        },
                        width: '100%'
                    });

                    $('#ddlProduct').on('select2:select', (e) => {
                        let data = e.params.data;
                        _product.name = data.text;
                    });

                    $("#weight").blur(function () {
                        let weight = $(this).val();

                        if (weight === undefined || weight === null || weight === '')
                            return _alterError(
                              "Lỗi nhập trọng lượng sản phẩm",
                              { message: "Vui lòng nhập giá trị trọng lượng sản phẩm" }
                            ).then(() => $("#weight").focus());


                        // Chuyển kiểu string thành float 
                        weight = parseFloat(weight);

                        if (weight < _weight_min)
                            return _alterError(
                              "Lỗi nhập trọng lượng sản phẩm",
                              { message: "Giá trị thấp nhất của trọng lượng sản phẩm là " + _weight_min + "kg" }
                            ).then(() => $("#weight").focus());

                        let $shipmentShop = $("#shipment_shop");
                        let $shipmentPostOffice = $("#shipment_post_office");

                        if (weight < 10) {
                            $shipmentPostOffice.prop('checked', false);
                            $shipmentPostOffice.attr('disabled', true);

                            $shipmentShop.removeAttr('disabled');
                            $shipmentShop.prop('checked', true).trigger('change');
                        } else {
                            $shipmentShop.prop('checked', false);
                            $shipmentShop.attr('disabled', true);

                            $shipmentPostOffice.removeAttr('disabled');
                            $shipmentPostOffice.prop('checked', true).trigger('change');
                        }

                        _product.weight = weight;
                        _calculateFee();
                    });
                }

                function _initFee() {
                    $('input:radio[name="feeship"]').change(function () {
                        _order.is_freeship = +$(this).val() == 0 ? 0 : 1;
                        _calculateMoney();
                    });
                }

                function _initNote() {
                    $("#note").change(function () {
                        _order.note = $(this).val();
                    });
                }

                function _initPage() {
                    // Cài đặt trọng lượng mặc định cho sản phẩm
                    $("#weight").val(_weight_min).trigger('blur');

                    // Lấy thông tin query parameter
                    let urlParams = new URLSearchParams(window.location.search);
                    let orderID = +urlParams.get('orderID') || 0;

                    if (orderID == 0)
                        return swal({
                            title: "Lỗi",
                            text: "Giá trị query param orderID không đúng",
                            icon: "error",
                        })
                          .then(() => {
                              window.location.href = "/danh-sach-don-hang";
                          });

                    let titleAlert = "Lấy thông tin đơn giao hàng"

                    $.ajax({
                        method: 'GET',
                        url: "/api/v1/order/" + orderID + "/delivery-save",
                        success: (response, textStatus, xhr) => {
                            let data = response.data;

                            // tel
                            $("#tel").val(data.customerPhone).trigger('change');
                            // customer_id
                            // name
                            $("#name").val(data.customerName).trigger('change');
                            // address
                            $("#address").val(data.customerAddress).trigger('change');
                            // pick_money
                            _order.pick_money = data.money;
                            $("#pick_money").val(_formatThousand(data.money));
                            // value
                            _order.value = data.money;
                            $("#value").val(_formatThousand(data.money));
                            $("#total_money").val(_formatThousand(data.money));
                            // id
                            _order.id = data.orderID;
                            $("#client_id").val(data.orderID);
                            // province
                            if (data.customerProvinceID) {
                                _order.province_id = data.customerProvinceID;
                                _order.province = data.customerProvinceName;
                                let newOption = new Option(data.customerProvinceName, data.customerProvinceID, false, false);
                                $('#ddlProvince').append(newOption).trigger('change');
                            }
                            if (data.customerProvinceID && data.customerDistrictID) {
                                _order.district_id = data.customerDistrictID;
                                _order.district = data.customerDistrictName;

                                // Danh sách quận / huyện
                                let newOption = new Option(data.customerDistrictName, data.customerDistrictID, false, false);
                                $('#ddlDistrict').removeAttr('disabled');
                                $('#ddlDistrict').removeAttr('readonly');
                                $('#ddlDistrict').append(newOption).trigger('change');
                            }
                            if (data.customerProvinceID && data.customerDistrictID && data.customerWardID) {
                                _order.ward_id = data.customerWardID;
                                _order.ward = data.customerWardName;

                                // Danh sách quận / huyện
                                let newOption = new Option(data.customerWardName, data.customerWardID, false, false);
                                $('#ddlWard').removeAttr('disabled');
                                $('#ddlWard').removeAttr('readonly');
                                $('#ddlWard').append(newOption).trigger('change');
                            }
                            if (data.weight) {
                                _weight_min = parseFloat(data.weight);
                                $("#weight").val(_weight_min).trigger('blur');
                            }
                            if (data.feeShop) {
                                _feeShop = data.feeShop;
                                $("#feeship_shop").attr("disabled", true);
                                $("#feeship_receiver").attr("disabled", true);
                                $("#divFeeShop").show();
                                $("#labelFeeShop").html(_formatThousand(data.feeShop) + " VND");
                                $("#feeShop").prop('checked', true).trigger('change');
                            }
                            else {
                                $("#feeship_shop").removeAttr("disabled");
                                $("#feeship_receiver").removeAttr("disabled");
                                $("#divFeeShop").hide();
                            }
                            if (data.note)
                                $("#note").val(data.note).trigger('change');
                        },
                        error: (xhr, textStatus, error) => {
                            return _alterError(titleAlert, xhr.responseJSON)
                              .then(() => {
                                  window.location.href = "/danh-sach-don-hang";
                              });
                        }
                    });
                }

                function _onChangeShipment(shipment) {
                    if (shipment == 'shop') {
                        // Cập nhật vô order
                        _order.pick_name = _personal.contactName;
                        _order.pick_address_id = _personal.warehouseCode;
                        _order.pick_address = _personal.address;
                        _order.pick_province = _personal.province;
                        _order.pick_district = _personal.district;
                        _order.pick_ward = _personal.ward;
                        _order.pick_street = _personal.street;
                        _order.pick_tel = _personal.tel;
                        _order.pick_email = _personal.email;
                        _order.pick_option = "cod";
                        // Cập nhật hiện thị nơi nhận hàng
                        $("#pick_address").val(_personal.address);
                    }
                    else {
                        // Cập nhật vô order
                        _order.pick_option = "post";
                        // Cập nhật hiện thị nơi nhận hàng
                        $("#pick_address").val(_postOffice.displayName);
                    }
                }

                function _onChangePickWorkShift(pick_work_shift) {
                    //let deliver_work_shift = [];
                    let now = new Date();
                    let tomorrow = new Date();
                    //let dateAfterTomorrow = new Date();
                    let strNow = "";
                    let strTomorrow = "";
                    //let strDateAfterTomorrow = "";

                    tomorrow.setDate(tomorrow.getDate() + 1);
                    //dateAfterTomorrow.setDate(tomorrow.getDate() + 2);
                    strNow = now.toISOString().substring(0, 10).replace(/-/g, '/');
                    strTomorrow = tomorrow.toISOString().substring(0, 10).replace(/-/g, '/');
                    //strDateAfterTomorrow = dateAfterTomorrow.toISOString().substring(0, 10).replace(/-/g, '/');

                    if (pick_work_shift == "Chiều nay") {
                        //deliver_work_shift.push({ id: 3, text: "Tối nay" });
                        //deliver_work_shift.push({ id: 1, text: "Sáng mai" });
                        //deliver_work_shift.push({ id: 2, text: "Chiều mai" });

                        _order.pick_date = strNow;
                        _order.pick_work_shift = 2;
                        //_order.deliver_date = strNow;
                        //_order.deliver_work_shift = 3;
                    }
                    else if (pick_work_shift == "Tối nay") {
                        //deliver_work_shift.push({ id: 1, text: "Sáng mai" });
                        //deliver_work_shift.push({ id: 2, text: "Chiều mai" });
                        //deliver_work_shift.push({ id: 3, text: "Tối mai" });

                        _order.pick_date = strNow;
                        _order.pick_work_shift = 3;
                        //_order.deliver_date = strTomorrow;
                        //_order.deliver_work_shift = 1;
                    }
                    else if (pick_work_shift == "Sáng mai") {
                        //deliver_work_shift.push({ id: 2, text: "Chiều mai" });
                        //deliver_work_shift.push({ id: 3, text: "Tối mai" });
                        //deliver_work_shift.push({ id: 1, text: "Sáng ngày kia" });

                        _order.pick_date = strTomorrow;
                        _order.pick_work_shift = 1;
                        //_order.deliver_date = strTomorrow;
                        //_order.deliver_work_shift = 2;
                    }
                    else if (pick_work_shift == "Chiều mai") {
                        //deliver_work_shift.push({ id: 3, text: "Tối mai" });
                        //deliver_work_shift.push({ id: 1, text: "Sáng ngày kia" });
                        //deliver_work_shift.push({ id: 2, text: "Chiều ngày kia" });

                        _order.pick_date = strTomorrow;
                        _order.pick_work_shift = 2;
                        //_order.deliver_date = strTomorrow;
                        //_order.deliver_work_shift = 3;
                    }
                    else if (pick_work_shift == "Tối mai") {
                        //deliver_work_shift.push({ id: 1, text: "Sáng ngày kia" });
                        //deliver_work_shift.push({ id: 2, text: "Chiều ngày kia" });
                        //deliver_work_shift.push({ id: 3, text: "Tối ngày kia" });

                        _order.pick_date = strTomorrow;
                        _order.pick_work_shift = 3;
                        //_order.deliver_date = strDateAfterTomorrow;
                        //_order.deliver_work_shift = 1;
                    }

                    //let $deliver_work_shift = $('#deliver_work_shift');

                    //// https://github.com/select2/select2/issues/2830
                    ////clear chosen items
                    //$deliver_work_shift.val(null).trigger('change');

                    ////destroy select2
                    //$deliver_work_shift.select2("destroy");

                    ////remove options physically from the HTML
                    //$deliver_work_shift.find("option").remove();

                    //$deliver_work_shift.select2({
                    //    minimumResultsForSearch: Infinity,
                    //    data: deliver_work_shift
                    //});
                }

                function _checkSubmit() {
                    let titleAlert = "Thông báo lỗi";

                    if (!_order.tel)
                        return swal({
                            title: titleAlert,
                            text: "Số điện thoại khách hàng chưa được nhập",
                            icon: "error",
                        })
                          .then(() => { $('#tel').focus(); });

                    if (!_order.province)
                        return swal({
                            title: titleAlert,
                            text: "Địa chỉ tỉnh / thành khách hàng chưa được chọn",
                            icon: "error",
                        })
                          .then(() => { $("#ddlProvince").select2('open'); });
                    if (_order.province && !_order.district)
                        return swal({
                            title: titleAlert,
                            text: "Địa chỉ quận / huyện khách hàng chưa được chọn",
                            icon: "error",
                        })
                          .then(() => { $("#ddlDistrict").select2('open'); });
                    if (_order.province && _order.district && !_order.ward)
                        return swal({
                            title: titleAlert,
                            text: "Địa chỉ phường / xã khách hàng chưa được chọn",
                            icon: "error",
                        })
                          .then(() => { $("#ddlDistrict").select2('open'); });
                    if (!_order.address)
                        return swal({
                            title: titleAlert,
                            text: "Địa chỉ khách hàng chưa được nhập",
                            icon: "error",
                        })
                          .then(() => { $('#address').focus(); });
                    if (!_product.name)
                        return swal({
                            title: titleAlert,
                            text: "Bạn chưa chọn loại sản phẩm để giao hàng",
                            icon: "error",
                        })
                          .then(() => { $("#ddlProduct").select2('open'); });

                    _submit();
                }

                function _calculateFee() {
                    if (!_order.pick_province || !_order.pick_district || !_order.province || !_order.district) {
                        _fee = 0;
                        $("#feeship").html("0 VND");
                    }
                    else {
                        let url = "/api/v1/delivery-save/fee",
                          query = "?";

                        if (_order.pick_address_id)
                            query += "&pick_address_id=" + _order.pick_address_id;
                        if (_order.pick_address)
                            query += "&pick_address=" + _order.pick_address;
                        if (_order.pick_province)
                            query += "&pick_province=" + _order.pick_province;
                        if (_order.pick_district)
                            query += "&pick_district=" + _order.pick_district;
                        if (_order.pick_ward)
                            query += "&pick_ward=" + _order.pick_ward;
                        if (_order.pick_street)
                            query += "&pick_street=" + _order.pick_street;
                        if (_order.address)
                            query += "&address=" + _order.address;
                        if (_order.province)
                            query += "&province=" + _order.province;
                        if (_order.district)
                            query += "&district=" + _order.district;
                        if (_order.ward)
                            query += "&ward=" + _order.ward;
                        if (_order.street)
                            query += "&street=" + _order.street;
                        query += "&weight=" + ((+_product.weight || 0) * 1000);
                        query += "&transport=road";

                        let titleAlert = "Tính phí giao hàng";

                        $.ajax({
                            method: 'GET',
                            url: url + query,
                            success: (data, textStatus, xhr) => {
                                if (xhr.status == 200 && data) {
                                    if (data.success) {
                                        _fee = data.fee.fee;
                                        $("#feeship").html(_formatThousand(_fee) + " VND");
                                        _calculateMoney();
                                    }
                                    else {
                                        _alterError(titleAlert, { message: data.message });
                                    }
                                } else {
                                    _alterError(titleAlert);
                                }
                            },
                            error: (xhr, textStatus, error) => {
                                _alterError(titleAlert);
                            }
                        });
                    }
                }

                function _calculateMoney() {
                    let $pick_money = $("#pick_money");
                    let $value = $("#value");
                    let $total_money = $("#total_money");
                    let feeShipment = +$("input:radio[name='feeship']:checked").val() || 0;
                    let total_money = 0;

                    if (_feeShipment == 2) {
                        _order.pick_money = _order.pick_money - _feeShop;
                        _order.value = _order.pick_money;
                    }

                    if (feeShipment == 1) {
                        total_money = _order.pick_money;
                    }
                    else if (feeShipment == 2) {
                        _order.pick_money = _order.pick_money + _feeShop;
                        _order.value = _order.pick_money;
                        total_money = _order.pick_money;
                    }
                    else {
                        total_money = _order.pick_money + _fee;
                    }

                    _feeShipment = feeShipment;
                    $pick_money.val(_formatThousand(_order.pick_money));
                    $value.val(_formatThousand(_order.value));
                    $total_money.val(_formatThousand(total_money));
                }

                function _submit() {
                    let titleAlert = "Thực hiện đăng ký Giao Hàng Tiết Kiệm";

                    $.ajax({
                        method: 'POST',
                        contentType: 'application/json',
                        dataType: "json",
                        data: JSON.stringify({ products: [_product], order: _order }),
                        url: "/api/v1/delivery-save/register-order",
                        success: (data, textStatus, xhr) => {
                            if (xhr.status == 200 && data) {
                                if (data.success)
                                    swal({
                                        title: titleAlert,
                                        text: "Đăng ký thành công",
                                        icon: "success",
                                    })
                                    .then(() => {
                                        window.location.href = "/danh-sach-don-hang";
                                    });
                                else
                                    _alterError(titleAlert, { message: data.message });
                            } else {
                                _alterError(titleAlert);
                            }
                        },
                        error: (xhr, textStatus, error) => {
                            _alterError(titleAlert);
                        }
                    });
                }

                function _alterError(title, responseJSON) {
                    let message = '';
                    title = (typeof title !== 'undefined') ? title : 'Thông báo lỗi';

                    if (responseJSON === null) {
                        message = 'Đẫ có lỗi xãy ra.';
                    }
                    else {
                        if (responseJSON.message)
                            message += responseJSON.message;
                    }

                    return swal({
                        title: title,
                        text: message,
                        icon: "error",
                    });
                }

                function _formatThousand(value) {
                    nfObject = new Intl.NumberFormat('en-US');

                    return nfObject.format(value);
                }
            </script>
        </div>
    </form>
</body>
</html>
