<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="nhan-vien-dat-hang.aspx.cs" Inherits="IM_PJ.nhan_vien_dat_hang" EnableSessionState="ReadOnly" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Đăng ký nhập hàng</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1, user-scalable=yes">
    <meta name="format-detection" content="telephone=no">
    <meta name="robots" content="noindex, nofollow">
    <link rel="stylesheet" href="/App_Themes/Ann/css/style.css?v=0110" media="all">
    <link rel="stylesheet" href="/App_Themes/Ann/css/style-P.css?v=0110" media="all">
    <link href="/App_Themes/NewUI/js/sweet/sweet-alert.css" rel="stylesheet" />
    <script type="text/javascript" src="/App_Themes/Ann/js/jquery-2.1.3.min.js"></script>
    <style>
        .select2-container {
            box-sizing: border-box;
            display: inline-block;
            margin: 0;
            position: relative;
            vertical-align: middle;
            max-width: 100%;
        }

            .select2-container .select2-selection--single {
                height: 45px;
                box-sizing: border-box;
                cursor: pointer;
                display: block;
                user-select: none;
                -webkit-user-select: none;
            }

        .select2-container--default .select2-selection--single {
            background-color: #fff;
            border: solid 1px #e1e1e1;
        }

            .select2-container--default .select2-selection--single .select2-selection__rendered {
                line-height: 45px;
                padding-left: 15px;
                color: #444;
            }

        .select2-container .select2-selection--single .select2-selection__rendered {
            display: block;
            padding-right: 20px;
            overflow: hidden;
            text-overflow: ellipsis;
            white-space: nowrap;
        }

        .select2-container--default .select2-selection--single .select2-selection__arrow {
            height: 43px;
            position: absolute;
            top: 1px;
            right: 10px;
            width: 20px;
        }

            .select2-container--default .select2-selection--single .select2-selection__arrow b {
                border-color: #000 transparent transparent transparent;
                border-style: solid;
                border-width: 5px 4px 0 4px;
                height: 0;
                left: 50%;
                margin-left: -4px;
                margin-top: -2px;
                position: absolute;
                top: 50%;
                width: 0;
            }

        .panel-table .panel-footer {
            padding: 5px 0;
        }

        .pagination li {
            display: inline-block;
            vertical-align: middle;
            line-height: 20px;
            font-weight: 600;
            background-color: #ff8400;
            color: #fff;
            font-size: 15px;
            margin-right: 10px;
        }

            .pagination li a {
                color: #fff;
                padding: 8px 11px;
            }

            .pagination li.current > a, .pagination li:hover > a {
                color: #000;
            }

        .btn.btn-product {
            background-color: #F44336;
        }

        .btn.btn-post {
            background-color: #009688;
        }

        .btn.download-btn {
            background-color: #000;
            color: #fff;
            border-radius: 0;
            font-size: 16px;
            text-transform: uppercase;
        }

        .btn.primary-btn {
            border-radius: 0;
            font-size: 16px;
            text-transform: uppercase;
        }

        .btn.remove-btn {
            background-color: #FF675B;
            color: #fff;
        }

        .product-item {
            margin-bottom: 40px;
            background-color: #fff;
            padding: 15px;
        }

        h3 {
            margin-top: 0;
        }

        .product-name a {
            font-size: 14px;
            line-height: 1.5;
        }

        .product-sku {
            font-size: 18px;
            color: #0289bc;
        }

        .product-price {
            font-size: 14px;
            color: #ff0023;
        }

        .bg-green, .bg-red, .bg-yellow {
            display: initial;
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
                        <div class="col-md-12">
                            <div class="filter-above-wrap clear">
                                <div class="filter-control">
                                    <div class="row">
                                        <div class="col-md-9 col-xs-12">
                                            <div class="row">
                                                <div class="col-md-3 col-xs-6 margin-bottom-15">
                                                    <asp:TextBox ID="txtSearchProduct" runat="server" CssClass="form-control sku-input" placeholder="Tìm sản phẩm" autocomplete="off"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3 col-xs-6 margin-bottom-15">
                                                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                                <div class="col-md-3 col-xs-6 margin-bottom-15">
                                                    <asp:DropDownList ID="ddlRegisterStatus" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="0" Text="Trạng thái"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="Chưa duyệt"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Đã duyệt"></asp:ListItem>
                                                        <asp:ListItem Value="3" Text="Đã đặt hàng"></asp:ListItem>
                                                        <asp:ListItem Value="4" Text="Hàng về"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
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
                                        <div class="col-md-3 col-xs-12">
                                            <div class="row">
                                                <div class="col-xs-6">
                                                    <a href="javascript:;" onclick="searchProduct()" class="btn primary-btn h45-btn"><i class="fa fa-search"></i>Tìm kiếm</a>
                                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn primary-btn h45-btn" OnClick="btnSearch_Click" Style="display: none" />
                                                </div>
                                                <div class="col-xs-6">
                                                    <a href="/nhan-vien-dat-hang" class="btn primary-btn h45-btn download-btn"><i class="fa fa-times" aria-hidden="true"></i>Làm lại</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel-table clear">
                                <div class="clear">
                                    <div class="pagination">
                                        <%this.DisplayHtmlStringPaging1();%>
                                    </div>
                                </div>
                                <div class="responsive-table">
                                    <asp:Literal ID="ltrList" runat="server" EnableViewState="false"></asp:Literal>
                                </div>
                                <div class="clear">
                                    <div class="pagination">
                                        <%this.DisplayHtmlStringPaging1();%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </main>

            <!-- Đăng ký só lượng đặt hàng Modal -->
            <div class="modal fade" id="registerModal" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Đăng ký nhập hàng</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row form-group">
                                <div class="col-md-3 col-xs-4">
                                    <p>Khách hàng</p>
                                </div>
                                <div class="col-md-9 col-xs-8">
                                    <input type="text" id="txtCustomerName" class="form-control" placeholder="Nhập tên khách hàng" />
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-3 col-xs-4">
                                    <p>Màu</p>
                                </div>
                                <div class="col-md-9 col-xs-8">
                                    <input type="text" id="txtColor" class="form-control text-right" disabled />
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-3 col-xs-4">
                                    <p>Size</p>
                                </div>
                                <div class="col-md-9 col-xs-8">
                                    <input type="text" id="txtSize" class="form-control text-right" disabled />
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-3 col-xs-4">
                                    <p>Số lượng</p>
                                </div>
                                <div class="col-md-9 col-xs-8">
                                    <input type="text" id="txtQuantity" class="form-control text-right" placeholder="Số lượng đăng ký" onkeypress="return event.charCode >= 48 && event.charCode <= 57" />
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-3 col-xs-4">
                                    <p>Ghi chú</p>
                                </div>
                                <div class="col-md-9 col-xs-8">
                                    <textarea id="areaNote" class="form-control" placeholder="Nhập thông tin chú thích" rows="3"></textarea>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button id="close" type="button" class="btn btn-default" data-dismiss="modal">Đóng</button>
                            <button id="register" type="button" class="btn btn-primary" onclick="updateRegister()">Đăng ký</button>
                        </div>
                    </div>
                </div>
            </div>

            <a href="javascript:;" class="scroll-top-link" id="scroll-top"><i class="fa fa-angle-up"></i></a>
            <a href="javascript:;" class="scroll-bottom-link" id="scroll-bottom"><i class="fa fa-angle-down"></i></a>

            <script src="/App_Themes/Ann/js/bootstrap.min.js"></script>
            <script src="/App_Themes/Ann/js/bootstrap-table/bootstrap-table.js"></script>
            <script src="/App_Themes/Ann/js/master.js?v=2011"></script>
            <script src="/App_Themes/NewUI/js/sweet/sweet-alert.js?v=3006" type="text/javascript"></script>

            <script type="text/javascript">
                class RegisterProduct {
                    constructor(
                        id
                        , customer
                        , status
                        , color
                        , size
                        , quantity
                        , note
                    ) {
                        this.id = id;
                        this.customer = customer;
                        this.status = status;
                        this.color = color;
                        this.size = size;
                        this.quantity = quantity;
                        this.note = note;
                    }
                }

                var register = null;

                $("#<%=txtSearchProduct.ClientID%>").keyup(function (e) {
                    if (e.keyCode == 13) {
                        $("#<%= btnSearch.ClientID%>").click();
                    }
                });

                function searchProduct() {
                    $("#<%= btnSearch.ClientID%>").click();
                }

                function openRegister(registerID) {
                    let registerDOM = $("#" + registerID);

                    register = new RegisterProduct(
                        registerID,
                        registerDOM.data('customer'),
                        registerDOM.data('status'),
                        registerDOM.data('color'),
                        registerDOM.data('size'),
                        registerDOM.data('quantity'),
                        registerDOM.data('note')
                    );

                    $('#txtCustomerName').val(register.customer);
                    $('#txtCustomerName').focus();
                    $('#txtColor').val(register.color);
                    $('#txtSize').val(register.size);
                    $('#txtQuantity').val(register.quantity);
                    $('#areaNote').val(register.note);
                    $('#registerModal').modal({ show: 'true', backdrop: 'static' });
                }

                function updateRegister() {
                    let customer = $("#txtCustomerName").val() || "";
                    let quantity = +$("#txtQuantity").val() || 0;
                    let note = $("#areaNote").val() || "";

                    if (!quantity) {
                        $("#txtQuantity").focus();
                        $("#txtQuantity").select();
                        return alert("Số lượng bạn nhập không đúng");
                    }

                    // Lấy dữ liệu nhập từ modal
                    register.customer = customer;
                    register.quantity = quantity;
                    register.note = note;

                    // Truyền dữ liệu xuống server
                    $.ajax({
                        type: "POST",
                        url: "/nhan-vien-dat-hang.aspx/updateRegister",
                        data: JSON.stringify({ 'item': register }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: (response) => {
                            let registerDOM = $("#" + register.id);
                            registerDOM.attr('data-customer', register.customer),
                            registerDOM.attr('data-quantity', register.quantity),
                            registerDOM.attr('data-note', register.note)

                            registerDOM.find(".customer").html(register.customer);
                            registerDOM.find(".quantity").html("Số lượng: " + formatThousands(register.quantity));

                            $("#registerModal").find("#close").click();
                        },
                        error: (xmlhttprequest, textstatus, errorthrow) => {
                            alert("Có lỗi trong quá trình đang ký nhập hàng");
                        }
                    })
                }

                function removeRegister(registerID) {
                    swal({
                        title: "Hủy",
                        text: "Bạn muốn hủy yêu cầu nhập hàng?",
                        showCancelButton: true,
                        closeOnConfirm: true,
                        cancelButtonText: "Không",
                        confirmButtonText: "Đúng",
                    }, function (confirm) {
                        if (confirm) {
                            // Truyền dữ liệu xuống server
                            $.ajax({
                                type: "POST",
                                url: "/nhan-vien-dat-hang.aspx/removeRegister",
                                data: JSON.stringify({ 'registerID': registerID }),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: (response) => {
                                    let strHTML = "<div class='col-xs-12'><span class='bg-red'>Đã hủy đơn đăng ký nhập hàng</span><div>"
                                    $("#" + registerID).find(".btn-handle").html(strHTML);
                                },
                                error: (xmlhttprequest, textstatus, errorthrow) => {
                                    alert("Có lỗi trong quá trình đang ký nhập hàng");
                                }
                            })
                        }
                    });
                    
                }

                // Format số lượng
                var formatThousands = function (n, dp) {
                    var s = '' + (Math.floor(n)), d = n % 1, i = s.length, r = '';
                    while ((i -= 3) > 0) { r = ',' + s.substr(i, 3) + r; }
                    return s.substr(0, i + 3) + r +
                        (d ? '.' + Math.round(d * Math.pow(10, dp || 2)) : '');
                };
            </script>
        </div>
    </form>
</body>
</html>
