<%@ Page Title="Kiểm kho" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="kiem-kho.aspx.cs" Inherits="IM_PJ.kiem_kho" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            line-height: 40px;
            height: 40px;
        }

        .select2-container .select2-selection--single {
            appearance: none;
            -webkit-appearance: none;
            -moz-appearance: none;
            -ms-appearance: none;
            -o-appearance: none;
            height: 40px;
            float: left;
            border: solid 1px #eee;
            width: 100%;
            background: url(/App_Themes/Ann/image/icon-select.png) no-repeat right 15px center;
        }

        .select2-container--default .select2-selection--single .select2-selection__arrow b {
            border-style: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panelborderheading">
                        <div class="panel-heading clear">
                            <h3 class="page-title left not-margin-bot">Kiểm kho</h3>
                        </div>
                        <div class="panel-body">
                            <div class="form-row">
                                <input type="text" id="txtSearch" class="form-control" placeholder="Nhập tên sản phẩm hoặc mã SKU"
                                    style="width: 40%; float: left; margin-right: 10px" />
                                <select id="typeinout" class="form-control" style="width: 20%; float: left; margin-right: 10px">
                                    <option value="2">Mã SKU</option>
                                    <option value="1">Tên sản phẩm</option>
                                </select>
                                <asp:Literal ID="ltrSupplier" runat="server"></asp:Literal>
                                <a href="javascript:;" class="btn primary-btn fw-btn not-fullwidth" style="margin-right: 10px"
                                    onclick="searchProduct()">Tìm sản phẩm</a>
                            </div>
                            <div class="form-row">
                            </div>
                            <div class="form-row">
                                <h3 class="no-margin float-left">Kết quả tìm kiếm: <span class="result-numsearch"></span></h3>
                                <div class="float-right excute-in">
                                    <a href="javascript:;" class="btn primary-btn fw-btn not-fullwidth"
                                        onclick="show_messageorder('')">Cập nhật kho</a>
                                </div>
                            </div>
                            <div class="form-row" style="border: solid 1px #ccc; padding: 10px;">
                                <table class="table table-checkable table-product">
                                    <thead>
                                        <tr>
                                            <th>Ảnh</th>
                                            <th>Tên sản phẩm</th>
                                            <th>SKU</th>
                                            <th>Nhà cung cấp</th>
                                            <th>Thuộc tính</th>
                                            <th>Số lượng hệ thống</th>
                                            <th>Số lượng trong kho</th>
                                            <th>Ghi chú</th>
                                            <th>Thao tác</th>
                                        </tr>
                                    </thead>
                                    <tbody class="content-product">
                                        <%--<tr class="product-result" data-sku="CS001" data-type="1" data-productid="2">
                                            <td>
                                                <img src="/App_Themes/Ann/image/sp_demo.png" alt="" style="width: 50px" /></td>
                                            <td>Áo thun nam cá sấu Adidas</td>
                                            <td>CS001</td>
                                            <td>Color:Xanh rêu|Size:XXXL</td>
                                            <td>
                                                <input type="number" min="0" class="in-quanlity" value="0" /></td>
                                        </tr>
                                        <tr class="product-result" data-sku="CS001" data-type="1" data-productid="2">
                                            <td>
                                                <img src="/App_Themes/Ann/image/sp_demo.png" alt="" style="width: 50px" /></td>
                                            <td>Áo thun nam cá sấu Adidas</td>
                                            <td>CS001</td>
                                            <td>Color:Xanh rêu|Size:XXXL</td>
                                            <td>
                                                <input type="number" min="0" class="in-quanlity" value="0" /></td>
                                        </tr>
                                        <tr class="product-result" data-sku="CS001" data-type="1" data-productid="2">
                                            <td>
                                                <img src="/App_Themes/Ann/image/sp_demo.png" alt="" style="width: 50px" /></td>
                                            <td>Áo thun nam cá sấu Adidas</td>
                                            <td>CS001</td>
                                            <td>Color:Xanh rêu|Size:XXXL</td>
                                            <td>
                                                <input type="number" min="0" class="in-quanlity" value="0" /></td>
                                        </tr>--%>
                                    </tbody>
                                </table>
                            </div>
                            <div class="form-row excute-in">
                                <div class="float-right"><a href="javascript:;" class="btn primary-btn fw-btn not-fullwidth" onclick="show_messageorder('')">Cập nhật kho</a></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hdfvalue" runat="server" />
        <asp:HiddenField ID="hdfNote" runat="server" />
        <asp:Button ID="btnImport" runat="server" OnClick="btnImport_Click" Style="display: none" />
    </main>
    <script type="text/javascript">
        $('#txtSearch').keydown(function (event) {
            if (event.which === 13) {
                searchProduct();
                event.preventDefault();
                return false;
            }
        });

        function searchProduct() {
            var textsearch = $("#txtSearch").val();
            var typeinout = $("#typeinout").val();
            var supplier = $("#supplierList").val();
            if (!isBlank(textsearch)) {
                $.ajax({
                    type: "POST",
                    url: "/kiem-kho.aspx/getProduct",
                    data: "{textsearch:'" + textsearch + "',typeinout:'" + typeinout + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var count = 0;
                        var data = JSON.parse(msg.d);
                        if (data.length > 0) {
                            var html = "";
                            for (var i = 0; i < data.length; i++) {
                                var item = data[i];
                                var sku = item.SKU
                                var supplierID = item.SupplierID;;
                                var check = false;
                                $(".product-result").each(function () {
                                    var existedSKU = $(this).attr("data-sku");
                                    if (sku == existedSKU) {
                                        check = true;
                                    }
                                });
                                if (supplier != 0) {
                                    if (supplierID == supplier) {
                                        if (check == false) {
                                            html += "<tr class=\"product-result\" data-quantityinstock=\"" + item.QuantityInstock + "\" data-productimageorigin=\"" + item.ProductImageOrigin + "\" data-productvariable=\"" + item.ProductVariable + "\" data-productname=\"" + item.ProductName + "\" data-sku=\"" + item.SKU + "\" data-producttype=\"" + item.ProductType + "\" data-id=\"" + item.ID + "\" data-productnariablename=\"" + item.ProductVariableName + "\" data-productvariablevalue =\"" + item.ProductVariableValue + "\">";
                                            html += "   <td>" + item.ProductImage + "";
                                            html += "   <td>" + item.ProductName + "</td>";
                                            html += "   <td>" + item.SKU + "</td>";
                                            html += "   <td>" + item.SupplierName + "</td>";
                                            html += "   <td>" + item.ProductVariable + "</td>";
                                            html += "   <td>" + item.QuantityInstockString + "</td>";
                                            //html += "   <td style=\"width:30%;\"><input type=\"number\" min=\"0\" max=\"" + item.QuantityInstock + "\" class=\"form-control in-quanlity\" style=\"width: 40%;margin: 0 auto;\" value=\"0\" onkeyup=\"checkQuantiy($(this))\" /></td>";
                                            html += "   <td style=\"width:20%;\"><input type=\"text\" min=\"0\" class=\"form-control in-quanlity\" style=\"width: 40%;margin: 0 auto;\" value=\"0\"  onkeypress='return event.charCode >= 48 && event.charCode <= 57'/></td>";
                                            html += "   <td style=\"width:20%;\"><input type=\"text\" class=\"form-control note-checkkho\" style=\"width: 40%;margin: 0 auto;\"/></td>";
                                            html += "   <td><a href=\"javascript:;\" onclick=\"deleteRow($(this))\">Xóa</a></td>";
                                            html += "</tr>";
                                        }
                                        //else {
                                        //    $(".product-result").each(function () {
                                        //        var skuFind = $(this).attr("data-sku");
                                        //        if (skuFind == sku) {
                                        //            var quantityOld = parseFloat($(this).find(".in-quanlity").val());
                                        //            var quantityNew = quantityOld + 1;
                                        //            $(this).find(".in-quanlity").val(quantityNew);
                                        //        }
                                        //    });
                                        //}
                                        count++;
                                    }
                                    else {
                                        alert('Không tìm thấy sản phẩm');
                                    }
                                }
                                else {
                                    if (check == false) {
                                        html += "<tr class=\"product-result\" data-quantityinstock=\"" + item.QuantityInstock + "\" data-productimageorigin=\"" + item.ProductImageOrigin + "\" data-productvariable=\"" + item.ProductVariable + "\" data-productname=\"" + item.ProductName + "\" data-sku=\"" + item.SKU + "\" data-producttype=\"" + item.ProductType + "\" data-id=\"" + item.ID + "\" data-productnariablename=\"" + item.ProductVariableName + "\" data-productvariablevalue =\"" + item.ProductVariableValue + "\">";
                                        html += "   <td>" + item.ProductImage + "";
                                        html += "   <td>" + item.ProductName + "</td>";
                                        html += "   <td>" + item.SKU + "</td>";
                                        html += "   <td>" + item.SupplierName + "</td>";
                                        html += "   <td>" + item.ProductVariable + "</td>";
                                        html += "   <td>" + item.QuantityInstockString + "</td>";
                                        //html += "   <td style=\"width:30%;\"><input type=\"number\" min=\"0\" max=\"" + item.QuantityInstock + "\" class=\"form-control in-quanlity\" style=\"width: 40%;margin: 0 auto;\" value=\"0\" onkeyup=\"checkQuantiy($(this))\" /></td>";
                                        html += "   <td style=\"width:20%;\"><input type=\"text\" min=\"0\" class=\"form-control in-quanlity\" style=\"width: 40%;margin: 0 auto;\" value=\"" + item.QuantityInstock + "\"  onkeypress='return event.charCode >= 48 && event.charCode <= 57'/></td>";
                                        html += "   <td style=\"width:20%;\"><input type=\"text\" class=\"form-control note-checkkho\" style=\"width: 40%;margin: 0 auto;\"/></td>";
                                        html += "   <td><a href=\"javascript:;\" onclick=\"deleteRow($(this))\">Xóa</a></td>";
                                        html += "</tr>";
                                    }
                                    //else {
                                    //    $(".product-result").each(function () {
                                    //        var skuFind = $(this).attr("data-sku");
                                    //        if (skuFind == sku) {
                                    //            var quantityOld = parseFloat($(this).find(".in-quanlity").val());
                                    //            var quantityNew = quantityOld + 1;
                                    //            $(this).find(".in-quanlity").val(quantityNew);
                                    //        }
                                    //    });
                                    //}
                                    count++;
                                }

                            }
                            $(".content-product").append(html);
                            $("#txtSearch").val("");
                            if (count > 0) {
                                $(".excute-in").show();
                            }
                        }
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                        alert('lỗi');
                    }
                });
            }
            else {
                alert('Vui lòng nhập nội dung tìm kiếm');
            }

        }
        function deleteRow(obj) {
            var c = confirm('Bạn muốn xóa sản phẩm này?');
            if (c) {
                obj.parent().parent().remove();
                if ($(".product-result").length == 0) {
                    $(".excute-in").hide();
                }
            }
        }
        function inProduct() {
            if ($(".product-result").length > 0) {
                var note = $("#txtnote").val();
                var list = "";
                var count = 0;
                $(".product-result").each(function () {
                    var id = $(this).attr("data-id");
                    var sku = $(this).attr("data-sku");
                    var producttype = $(this).attr("data-producttype");
                    var productnariablename = $(this).attr("data-productnariablename");
                    var productvariablevalue = $(this).attr("data-productvariablevalue");
                    var quantity = $(this).find(".in-quanlity").val();
                    var productname = $(this).attr("data-productname");
                    var productimageorigin = $(this).attr("data-productimageorigin");
                    var productvariable = $(this).attr("data-productvariable");
                    var noteeach = $(this).find('.note-checkkho').val();
                    var quantityInstock = $(this).attr("data-QuantityInstock");
                    if (quantity > 0) {
                        list += id + "," + sku + "," + producttype + "," + productnariablename + "," + productvariablevalue + "," + quantity + ","
                            + productname + "," + productimageorigin + "," + productvariable + "," + noteeach + "," + quantityInstock + ";";
                        count++;
                    }
                });
                if (count > 0) {
                    $("#<%=hdfNote.ClientID%>").val(note);
                    $("#<%=hdfvalue.ClientID%>").val(list);
                    $("#<%=btnImport.ClientID%>").click();
                }
                else {
                    alert('Vui lòng nhập số lượng để xuất kho');
                }
            }
            else {
                alert("Vui lòng nhập sản phẩm");
            }
        }
        function show_messageorder(content) {
            var obj = $('body');
            $(obj).css('overflow', 'hidden');
            $(obj).attr('onkeydown', 'keyclose_ms(event)');
            var bg = "<div id='bg_popup'></div>";
            var fr = "<div id='pupip' class=\"columns-container1\"><div class=\"container\" id=\"columns\"><div class='row'>" +
                     "  <div class=\"center_column col-xs-12 col-sm-5\" id=\"popup_content\"><a style='cursor:pointer;right:5px;' onclick='close_popup_ms()' class='close_message'></a>";
            fr += "     <div class=\"changeavatar\">";
            fr += content;
            fr += "         <label class=\"lbl-popup\">Nội dung kiểm kho</label>";
            fr += "         <textarea id=\"txtnote\" class=\"form-control\"/>";
            fr += "         <div class=\"clearfix\"></div>";
            fr += "         <div class=\"clearfix\"></div>";
            fr += "         <div class=\"btn-content\">";
            fr += "             <a class=\"btn primary-btn fw-btn not-fullwidth\" style=\"padding:10px 30px;float:right;margin:10px 0\" href=\"javascript:;\" onclick=\"inProduct()\" >Xác nhận</a>";
            fr += "         </div>";
            fr += "     </div>";
            fr += "   </div>";
            fr += "</div></div></div>";
            $(bg).appendTo($(obj)).show().animate({ "opacity": 0.7 }, 800);
            $(fr).appendTo($(obj));
            setTimeout(function () {
                $('#pupip').show().animate({ "opacity": 1, "top": 20 + "%" }, 200);
                $("#bg_popup").attr("onclick", "close_popup_ms()");
            }, 1000);
        }
        function keyclose_ms(e) {
            if (e.keyCode == 27) {
                close_popup_ms();
            }
        }
        function close_popup_ms() {
            $("#pupip").animate({ "opacity": 0 }, 400);
            $("#bg_popup").animate({ "opacity": 0 }, 400);
            setTimeout(function () {
                $("#pupip").remove();
                $(".zoomContainer").remove();
                $("#bg_popup").remove();
                $('body').css('overflow', 'auto').attr('onkeydown', '');
            }, 500);
        }
        function checkQuantiy(obj) {
            var instock = parseFloat(obj.parent().parent().attr("data-quantityinstock"));
            var currentVal = parseFloat(obj.val());
            var check = true;
            if (currentVal > instock) {
                obj.val("");
                obj.val(instock);
            }
        }
        function keypress(e) {
            var keypressed = null;
            if (window.event) {
                keypressed = window.event.keyCode; //IE
            }
            else {
                keypressed = e.which; //NON-IE, Standard
            }
            if (keypressed < 48 || keypressed > 57) {
                if (keypressed == 8 || keypressed == 127) {
                    return;
                }
                return false;
            }
        }
    </script>
</asp:Content>
