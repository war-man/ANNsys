﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dang-ky-nhap-hang.aspx.cs" Inherits="IM_PJ.dang_ky_nhap_hang" EnableSessionState="ReadOnly" %>

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
    <link href="/App_Themes/NewUI/js/select2/select2.css" rel="stylesheet" />
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
        .btn.copy-btn {
            background-color: #E91E63;
            color: #fff;
        }
        .product-item {
            margin-bottom: 40px;
            background-color: #fff;
            padding: 15px;
        }
        h3 {
            margin-top: 10px;
        }
        .product-name a {
            font-size: 18px;
            line-height: 1.5;
        }
        .product-sku {
            font-size: 18px;
            color: #0289bc;
        }
        .product-price {
            font-size: 20px;
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
                                                    <asp:DropDownList ID="ddlStockStatus" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="" Text="Kho"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="Còn hàng"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Hết hàng"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-3 col-xs-6 margin-bottom-15">
                                                    <asp:DropDownList ID="ddlCreatedDate" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="" Text="Thời gian"></asp:ListItem>
                                                        <asp:ListItem Value="today" Text="Hôm nay"></asp:ListItem>
                                                        <asp:ListItem Value="yesterday" Text="Hôm qua"></asp:ListItem>
                                                        <asp:ListItem Value="beforeyesterday" Text="Hôm kia"></asp:ListItem>
                                                        <asp:ListItem Value="week" Text="Tuần này"></asp:ListItem>
                                                        <asp:ListItem Value="thismonth" Text="Tháng này"></asp:ListItem>
                                                        <asp:ListItem Value="7days" Text="7 ngày"></asp:ListItem>
                                                        <asp:ListItem Value="30days" Text="30 ngày"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                </div>
                                                <div class="col-md-3 col-xs-6 margin-bottom-15">
                                                    <asp:DropDownList ID="ddlColor" runat="server" CssClass="form-control select2" Width="100%">
                                                        <asp:ListItem Value="" Text="Chọn màu"></asp:ListItem>
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
                                                <div class="col-md-3 col-xs-6 margin-bottom-15">
                                                    <asp:DropDownList ID="ddlSize" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="" Text="Chọn size"></asp:ListItem>
                                                        <asp:ListItem Value="s" Text="Size S"></asp:ListItem>
                                                        <asp:ListItem Value="m" Text="Size M"></asp:ListItem>
                                                        <asp:ListItem Value="l" Text="Size L"></asp:ListItem>
                                                        <asp:ListItem Value="xl" Text="Size XL"></asp:ListItem>
                                                        <asp:ListItem Value="xxl" Text="Size XXL"></asp:ListItem>
                                                        <asp:ListItem Value="xxxl" Text="Size XXXL"></asp:ListItem>
                                                        <asp:ListItem Value="28" Text="Size 28"></asp:ListItem>
                                                        <asp:ListItem Value="29" Text="Size 29"></asp:ListItem>
                                                        <asp:ListItem Value="30" Text="Size 30"></asp:ListItem>
                                                        <asp:ListItem Value="31" Text="Size 31"></asp:ListItem>
                                                        <asp:ListItem Value="32" Text="Size 32"></asp:ListItem>
                                                        <asp:ListItem Value="33" Text="Size 33"></asp:ListItem>
                                                        <asp:ListItem Value="34" Text="Size 34"></asp:ListItem>
                                                        <asp:ListItem Value="36" Text="Size 36"></asp:ListItem>
                                                        <asp:ListItem Value="38" Text="Size 38"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3 col-xs-12">
                                            <div class="row">
                                                <div class="col-xs-6">
                                                    <a href="javascript:;" onclick="searchProduct()" class="btn primary-btn h45-btn"><i class="fa fa-search"></i> Tìm kiếm</a>
                                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn primary-btn h45-btn" OnClick="btnSearch_Click" Style="display: none" />
                                                </div>
                                                <div class="col-xs-6">
                                                    <a href="/san-pham-theo-ke" class="btn primary-btn h45-btn download-btn"><i class="fa fa-times" aria-hidden="true"></i> Làm lại</a>
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
                                    <p>Thuộc tính</p>
                                </div>
                                <div class="col-md-9 col-xs-8">
                                    <input type="text" id="txtVariableValue" class="form-control text-right" disabled />
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
                            <button id="register" type="button" class="btn btn-primary" onclick="registerProduct()">Đăng ký</button>
                        </div>
                    </div>
                </div>
            </div>

            <a href="javascript:;" class="scroll-top-link" id="scroll-top"><i class="fa fa-angle-up"></i></a>
            <a href="javascript:;" class="scroll-bottom-link" id="scroll-bottom"><i class="fa fa-angle-down"></i></a>
            
            <script src="/App_Themes/Ann/js/bootstrap.min.js"></script>
            <script src="/App_Themes/Ann/js/bootstrap-table/bootstrap-table.js"></script>
            <script src="/App_Themes/NewUI/js/select2/select2.min.js"></script>
            <script src="/App_Themes/Ann/js/master.js?v=2011"></script>
            <script src="/App_Themes/Ann/js/copy-product-info.js?v=2011"></script>
            <script src="/App_Themes/Ann/js/sync-product-small.js?v=30052019"></script>
            <script src="/App_Themes/Ann/js/download-product-image.js?v=17072019"></script>

            <script type="text/javascript">
                class RegisterProduct {
                    constructor(
                        customer
                        , productID
                        , variableID
                        , sku
                        , status
                        , title
                        , image
                        , color
                        , size
                        , quantity
                    ) {
                        this.customer = customer;
                        this.productID = productID;
                        this.variableID = variableID;
                        this.sku = sku;
                        this.status = status;
                        this.title = title;
                        this.image = image;
                        this.color = color;
                        this.size = size;
                        this.quantity = quantity;
                    }
                }

                var productRegister = null;

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

                $(document).ready(function () {
                    LoadSelect();
                });

                function LoadSelect() {
                    $(".select2").select2({
                        templateResult: formatState,
                        templateSelection: formatState
                    });
                    function formatState(opt) {
                        if (!opt.id) {
                            return opt.text;
                        }
                        var optimage = $(opt.element).data('image');
                        if (!optimage) {
                            return opt.text;
                        } else {
                            var $opt = $(
                                '<span>' + opt.text + '</span>'
                            );
                            return $opt;
                        }
                    };
                }


                $('.form-filter input').keyup(function (e) {
                    var $input = $(this),
                        inputContent = $input.val().toLowerCase(),
                        column = $('.form-filter input').index($input),
                        $table = $('#table-student'),
                        $rows = $table.find('tbody tr');

                    var $filteredRows = $rows.filter(function () {
                        var value = $(this).find('td').eq(column).text().toLowerCase();

                        if (value.indexOf(inputContent) > -1) {
                            $(this).show();
                        } else {
                            $(this).hide();
                        }


                    });


                    /* Clean no-result if exist */
                    /* Prepend no-result */
                    if ($table.find('tbody tr:visible').length === 0) {
                        $table.find('tbody').prepend($('<tr class="no-result text-center"><td colspan="3">No result found</td></tr>'));
                    } else {
                        $table.find('tbody .no-result').remove();
                    }
                });

                function OnClientFileSelected(sender, args) {
                    if ($telerik.isIE) return;
                    else {
                        truncateName(args);
                        //var file = args.get_fileInputField().files.item(args.get_rowIndex());
                        var file = args.get_fileInputField().files.item(0);
                        showThumbnail(file, args);
                    }
                }

                function truncateName(args) {
                    var $span = $(".ruUploadProgress", args.get_row());
                    var text = $span.text();
                    if (text.length > 23) {
                        var newString = text.substring(0, 23) + '...';
                        $span.text(newString);
                    }
                }

                function showThumbnail(file, args) {

                    var image = document.createElement("img");

                    image.file = file;
                    image.className = "ab img-responsive";

                    var $row = $(args.get_row());
                    $row.parent().className = "row ruInputs list-unstyled";
                    $row.append(image);


                    var reader = new FileReader();
                    reader.onload = (function (aImg) {
                        return function (e) {
                            aImg.src = e.target.result;
                        };
                    }(image));
                    var ret = reader.readAsDataURL(file);
                    var canvas = document.createElement("canvas");

                    ctx = canvas.getContext("2d");
                    image.onload = function () {
                        ctx.drawImage(image, 100, 100);
                    };

                }

                function isBlank(str) {
                    return (!str || /^\s*$/.test(str));
                }

                function openRegister(item) {
                    productRegister = new RegisterProduct('', item.productID, item.variableID, item.sku, 1, item.title, item.image, item.color, item.size, 0);
                    $('#txtCustomerName').focus();
                    let variableValue = "Đủ màu - Đủ size";
                    if (item.variableID != 0) {
                        variableValue = "";
                        if (item.color) {
                            variableValue = "Màu: " + item.color;
                        }
                        if (item.size) {
                            variableValue += " - Size: " + item.size;
                        }
                    }
                    $('#txtVariableValue').val(variableValue);
                    $('#registerModal').modal({ show: 'true', backdrop: 'static' });
                }

                function registerProduct() {
                    let customer = $("#txtCustomerName").val() || "";
                    let quantity = +$("#txtQuantity").val() || 0;
                    let note = $("#areaNote").val() || "";

                    if (!quantity)
                    {
                        $("#txtQuantity").focus();
                        $("#txtQuantity").select();
                        return alert("Số lượng bạn nhập không đúng");
                    }

                    // Lấy dữ liệu nhập từ modal
                    productRegister.customer = customer;
                    productRegister.quantity = quantity;
                    productRegister.note = note;
                    if(productRegister.productID && productRegister.variableID == "")
                    {
                        productRegister.color = "Đủ màu";
                        productRegister.size = "Đủ size";
                    }

                    // Truyền dữ liệu xuống server
                    $.ajax({
                        type: "POST",
                        url: "/dang-ky-nhap-hang.aspx/registerProduct",
                        data: JSON.stringify({'item': productRegister }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: (response) => {
                            $("#registerModal").find("#close").click();
                        },
                        error: (xmlhttprequest, textstatus, errorthrow) => {
                            alert("Có lỗi trong quá trình đang ký nhập hàng");
                        }
                    })
                }
            </script>
        </div>
    </form>
</body>
</html>
