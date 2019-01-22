<%@ Page Title="Tạo mã vạch" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="tao-ma-vach.aspx.cs" Inherits="IM_PJ.tao_ma_vach" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="parent" runat="server">
        <main id="main-wrap">
            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <h3 class="page-title left">In mã vạch</h3>
                        <div class="right above-list-btn">
                            <asp:Literal ID="ltrBack" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel-post">
                            <div class="post-table-links clear">
                                <a href="javascript:;" style="background-color: #f87703; float: right;" class="btn primary-btn link-btn" onclick="payall()">In mã vạch (F1)</a>
                                <a href="javascript:;" style="background-color: #ffad00; float: right;" class="btn primary-btn link-btn" onclick="quickInput()">Nhập nhanh số lượng (F2)</a>
                            </div>
                            <div class="post-above clear">
                                <div class="search-box left" style="width: 96%;">
                                    <input type="text" id="txtSearch" class="form-control sku-input" placeholder="SKU (F3)" autocomplete="off">
                                </div>
                                <div class="right">
                                    <a href="javascript:;" class="link-btn" onclick="Show_popup_search()"><i class="fa fa-search"></i></a>
                                </div>
                            </div>
                            <div class="post-body search-product-content clear">
                                <table class="table table-checkable table-product import-stock">
                                    <thead>
                                        <tr>
                                            <th class="image-column">Ảnh</th>
                                            <th class="name-column">Sản phẩm</th>
                                            <th class="sku-column">Mã</th>
                                            <th class="variable-column">Thuộc tính</th>
                                            <th class="price-column">Giá sỉ</th>
                                            <th class="stock-column">Kho</th>
                                            <th class="quantity-column">Số lượng in</th>
                                            <th class="trash-column"></th>
                                        </tr>
                                    </thead>
                                    <tbody class="content-product">
                                    </tbody>
                                </table>
                            </div>
                            <div class="post-table-links clear">
                                <a href="javascript:;" style="background-color: #f87703; float: right;" class="btn primary-btn link-btn" onclick="payall()">In mã vạch (F1)</a>
                                <a href="javascript:;" style="background-color: #ffad00; float: right;" class="btn primary-btn link-btn" onclick="quickInput()">Nhập nhanh số lượng (F2)</a>
                                <asp:Button ID="btnOrder" runat="server" OnClick="btnOrder_Click" Style="display: none" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <asp:HiddenField ID="hdfListProduct" runat="server" />
            <asp:HiddenField ID="hdfCSSPrintBarcode" runat="server" />
            <asp:HiddenField ID="hdfListSearch" runat="server" />
            <asp:HiddenField ID="hdfcheck" runat="server" />
            <div id="printcontent" style="display: none">
                <asp:Literal ID="ltrprint" runat="server"></asp:Literal>
            </div>
        </main>
    </asp:Panel>
    <style>
        .search-product-content {
            min-height: 450px;
            background: #fff;
        }

        #popup_content2 {
            min-height: 10px;
            position: fixed;
            background-color: #fff;
            top: 15%;
            z-index: 9999;
            left: 0;
            -moz-border-radius: 10px;
            -webkit-border-radius: 10px;
            width: 45%;
            padding: 20px 40px;
            right: 0%;
            margin: 0 auto;
        }
    </style>
  
        <script type="text/javascript">

            // key press F1 - F3
            $(document).keydown(function(e) {
                if (e.which == 112) { //F1 Print Barcode
                    payall();
                    return false;
                }
                if (e.which == 113) { //F2 Quick Input
                    quickInput();
                    return false;
                }
                if (e.which == 114) { //F3 Search Product
                    $("#txtSearch").focus();
                    return false;
                }
            });

            // focus to searchProduct input when page on ready
            $(document).ready(function () {
                $("#txtSearch").focus();
            });

            function printBarcode() {
                swal({
                    title: "Thông báo", text: "Tạo mã vạch thành công! Bấm OK để in mã vạch...",
                    type: "success",
                    showCancelButton: false,
                    confirmButtonText: "OK in đê!!",
                    closeOnConfirm: true,
                    html: false
                }, function () {
                    window.location.replace(window.location.href);
                    printDiv('printcontent');
                });
            }

            function printDiv(divid) {
                var divToPrint = document.getElementById('' + divid + '');
                var css = $("#<%=hdfCSSPrintBarcode.ClientID%>").val();
                var newWin = window.open('', 'Print-Window');
                newWin.document.open();
                newWin.document.write('<html><head><style>' + css + '</style></head><body><script>window.onload = setTimeout(function () {window.print();setTimeout(function () { window.close(); }, 1);}, 1500);<\/script>' + divToPrint.innerHTML + '</body></html>');
                newWin.document.close();
            }

            $('#txtSearch').keydown(function (event) {
                if (event.which === 13) {
                    //searchProduct();
                    Show_popup_search();
                    event.preventDefault();
                    return false;
                }
            });

            // quick input quantity
            function quickInput() {
                if ($(".product-result").length == 0) {
                    alert("Hãy nhập sản phẩm!");
                    $("#txtSearch").focus();
                } else {
                    var html = "";
                    html += "<div class=\"form-row\">";
                    html += "<label>Nhập nhanh số lượng cho mỗi sản phẩm: </label>";
                    html += "<input ID=\"txtQuickInput\" class=\"form-control fjx\"></input>";
                    html += "<a href=\"javascript:;\" class=\"btn primary-btn float-right-btn link-btn\" onclick=\"submitQuickInput()\"><i class=\"fa fa-search\" aria-hidden=\"true\"></i> OK</a>";
                    html += "</div>";
                    showPopup(html, 3);
                    $("#txtQuickInput").focus();
                    $('#txtQuickInput').keydown(function (event) {
                        if (event.which === 13) {
                            submitQuickInput();
                            event.preventDefault();
                            return false;
                        }
                    });
                }
            }

            function submitQuickInput() {
                var quantity = $("#txtQuickInput").val();
                $(".product-result").each(function () {
                    $(this).find(".in-quantity").val(quantity);
                });
                closePopup();
            }

            //N.A
            function GetProduct(list, list2) {
                var textsearch = $("#<%=hdfListSearch.ClientID%>").val();
                var customerType = $(".customer-type").val();
                $.ajax({
                    type: "POST",
                    url: "/tao-ma-vach.aspx/getProduct",
                    data: "{textsearch:'" + textsearch + "', gettotal: 1 }",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var data = JSON.parse(msg.d);
                        if (data.length > 0) {
                            var html = "";
                            for (var j = 0; j < list.length - 1; j++) {
                                var key = list[j];
                                for (var i = 0; i < data.length; i++) {
                                    var item = data[i];
                                    var sku = item.SKU;
                                    var check = false;
                                    if (key == sku) {
                                        $(".product-result").each(function () {
                                            var existedSKU = $(this).attr("data-sku");
                                            if (sku == existedSKU) {
                                                check = true;
                                            }
                                        });
                                        if (check == false) {
                                            html += "<tr class=\"product-result\" data-giabansi=\"" + item.Giabansi + "\" data-giabanle=\"" + item.Giabanle + "\" " +
                                                "data-quantityinstock=\"" + item.QuantityInstock + "\" data-productimageorigin=\"" + item.ProductImageOrigin + "\" " +
                                                "data-productvariable=\"" + item.ProductVariable + "\" data-productname=\"" + item.ProductName + "\" " +
                                                "data-sku=\"" + item.SKU + "\" data-producttype=\"" + item.ProductType + "\" data-id=\"" + item.ID + "\" " +
                                                "data-productnariablename=\"" + item.ProductVariableName + "\" " +
                                                "data-productvariablevalue =\"" + item.ProductVariableValue + "\" " +
                                                "data-productvariablesave =\"" + item.ProductVariableSave + "\">";
                                            html += "   <td>" + item.ProductImage + "";
                                            html += "   <td>" + item.ProductName + "</td>";
                                            html += "   <td>" + item.SKU + "</td>";
                                            html += "   <td>" + item.ProductVariable + "</td>";
                                            html += "   <td class=\"gia-san-pham\" data-price=\"" + item.Giabansi + "\">" + item.stringGiabansi + "</td>";
                                            html += "   <td>" + item.QuantityInstockString + "</td>";

                                            html += "   <td><input type=\"text\" class=\"form-control in-quantity\" onkeyup=\"pressKeyQuantity($(this))\" pattern=\"[0-9]{1,3}\" value=\"" + list2[j] + "\" onkeypress='return event.charCode >= 48 && event.charCode <= 57'/></td>";

                                            html += "   <td><a href=\"javascript:;\" class=\"link-btn\" onclick=\"deleteRow($(this))\"><i class=\"fa fa-trash\"></i></a></td>";
                                            html += "</tr>";
                                        }
                                        else if (check == true) {
                                            $(".product-result").each(function () {
                                                var existedSKU = $(this).attr("data-sku");
                                                if (sku == existedSKU) {
                                                    var quantityinstock = parseFloat($(this).attr("data-quantityinstock"));
                                                    var quantityCurrent = parseFloat($(this).find(".in-quantity").val());
                                                    var newquantity = quantityCurrent + parseInt(list2[j]);
                                                    if (newquantity <= quantityinstock) {
                                                        $(this).find(".in-quantity").val(newquantity);

                                                    }
                                                }
                                            });
                                        }
                                    }
                                }
                            }
                            $(".content-product").append(html);
                            $("#txtSearch").val("");
                        }
                        else {
                            alert('Không tìm thấy sản phẩm');
                        }
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                        alert('lỗi');
                    }
                });
            }

            // select all variable product
            function check_all() {
                if ($('#check-all').is(":checked")) {
                    $(".check-popup").prop('checked', true);
                } else {
                    $(".check-popup").prop('checked', false);
                }
            }

            // checkbox a variable product
            function check(obj) {
                var temp = 0;
                var temp2 = 0;
                $(".search-popup").each(function () {
                    if ($(this).find(".check-popup").is(':checked')) {
                        temp++;
                    } else {
                        temp2++;
                    }
                    if (temp2 > 0) {
                        obj.parent().parent().parent().find("#check-all").prop('checked', false);
                    } else {
                        obj.parent().parent().parent().find("#check-all").prop('checked', true);
                    }
                });
            }

            function Show_popup_search() {
                var textsearch = $("#txtSearch").val();
                $("#<%=hdfListSearch.ClientID%>").val(textsearch);
                var customerType = $(".customer-type").val();
                if (!isBlank(textsearch)) {
                    $.ajax({
                        type: "POST",
                        url: "/tao-ma-vach.aspx/getProduct",
                        data: "{textsearch:'" + textsearch + "', gettotal: 0 }",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {

                            var data = JSON.parse(msg.d);
                            if (data.length > 1) {
                                var html = "";
                                var listGet = "";
                                html += ("<table class=\"table table-checkable table-product import-stock\">");
                                html += ("<tr>");
                                html += ("<td class=\"select-column\">");
                                html += ("<input type=\"checkbox\" id=\"check-all\"onchange=\"check_all()\"/>");
                                html += ("</td>");
                                html += ("<td class=\"image-column\">Ảnh</td>");
                                html += ("<td class=\"name-column\">Sản phẩm</td>");
                                html += ("<td class=\"sku-column\">Mã</td>");
                                html += ("<td class=\"variable-column\">Thuộc tính</td>");
                                html += ("<td class=\"quantity-column\">Số lượng</td>");
                                html += ("</tr>");
                                for (var i = 0; i < data.length; i++) {
                                    var item = data[i];
                                    html += ("<tr class=\"search-popup\" id=\"search-key\";>");
                                    html += ("<td>");
                                    html += ("<input id=\"" + item.ID + "\" type=\"checkbox\" class=\"check-popup\" />");
                                    html += ("</td>");
                                    html += ("<td>" + item.ProductImage + "</td>");
                                    html += ("<td>" + item.ProductName + "</td>");
                                    html += ("<td class=\"key\">" + item.SKU + "</td>");
                                    html += ("<td>" + item.ProductVariable + "</td>");
                                    html += ("<td><input class=\"quantity\" pattern=\"[0-9]{1,3}\" type=\"text\" value=\"1\"></td>");
                                    html += ("</tr>");
                                }
                                html += ("</table>");
                                html += ("<div>");
                                html += ("<a href=\"javascript:;\" class=\"btn primary-btn link-btn\" onclick=\"Submitproduct()\">Chọn</a>");
                                html += ("</div >");
                                $("#txtSearch").val("");
                                showPopup(html);
                            }
                            else if (data.length == 1) {

                                var item = data[0];
                                var sku = item.SKU;
                                var check = false;
                                $(".product-result").each(function () {
                                    var existedSKU = $(this).attr("data-sku");
                                    if (sku == existedSKU) {
                                        check = true;
                                    }
                                });
                                if (check == false) {
                                    html += "<tr class=\"product-result\" data-giabansi=\"" + item.Giabansi + "\" data-giabanle=\"" + item.Giabanle + "\" " +
                                        "data-quantityinstock=\"" + item.QuantityInstock + "\" data-productimageorigin=\"" + item.ProductImageOrigin + "\" " +
                                        "data-productvariable=\"" + item.ProductVariable + "\" data-productname=\"" + item.ProductName + "\" " +
                                        "data-sku=\"" + item.SKU + "\" data-producttype=\"" + item.ProductType + "\" data-id=\"" + item.ID + "\" " +
                                        "data-productnariablename=\"" + item.ProductVariableName + "\" " +
                                        "data-productvariablevalue =\"" + item.ProductVariableValue + "\" " +
                                        "data-productvariablesave =\"" + item.ProductVariableSave + "\">";
                                    html += "   <td>" + item.ProductImage + "";
                                    html += "   <td>" + item.ProductName + "</td>";
                                    html += "   <td>" + item.SKU + "</td>";
                                    html += "   <td>" + item.ProductVariable + "</td>";
                                    html += "   <td class=\"gia-san-pham\" data-price=\"" + item.Giabansi + "\">" + item.stringGiabansi + "</td>";
                                    html += "   <td>" + item.QuantityInstockString + "</td>";
                                    html += "   <td><input type=\"text\" class=\"form-control in-quantity\" pattern=\"[0-9]{1,3}\" onkeyup=\"pressKeyQuantity($(this))\" value=\"1\" onkeypress='return event.charCode >= 48 && event.charCode <= 57'/></td>";
                                    html += "   <td><a href=\"javascript:;\" class=\"link-btn\" onclick=\"deleteRow($(this))\"><i class=\"fa fa-trash\"></i></a></td>";
                                    html += "</tr>";
                                }
                                else if (check == true) {
                                    $(".product-result").each(function () {
                                        var existedSKU = $(this).attr("data-sku");
                                        if (sku == existedSKU) {
                                            var quantityinstock = parseFloat($(this).attr("data-quantityinstock"));
                                            var quantityCurrent = parseFloat($(this).find(".in-quantity").val());
                                            var newquantity = quantityCurrent + 1;
                                            if (newquantity <= quantityinstock) {
                                                $(this).find(".in-quantity").val(newquantity);

                                            }
                                        }
                                    });
                                }

                                $(".content-product").append(html);
                                $("#txtSearch").val("");
                            }
                            else {
                                alert('Không tìm thấy sản phẩm');
                                $("#txtSearch").select();
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

            function Submitproduct() {
                var list = "";
                var value = "";
                $(".search-popup").each(function () {
                    if ($(this).find(".check-popup").is(':checked')) {
                        var sku = $(this).find("td.key").html();
                        var quantity = $(this).find("input.quantity").val();
                        value += quantity + "*";
                        list += sku + "*";
                    }
                });
                var item = list.split("*");
                var item2 = value.split("*");
                GetProduct(item, item2);
                closePopup();
                $("#txtSearch").focus();
            }
            //End

            function searchProduct() {
                var textsearch = $("#txtSearch").val();
                if (!isBlank(textsearch)) {
                    $.ajax({
                        type: "POST",
                        url: "/tao-ma-vach.aspx/getProduct",
                        data: "{textsearch:'" + textsearch + "', gettotal: 0 }",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            var data = JSON.parse(msg.d);
                            if (data.length > 0) {
                                var html = "";

                                for (var i = 0; i < data.length; i++) {
                                    var item = data[i];
                                    var sku = item.SKU;
                                    var check = false;
                                    $(".product-result").each(function () {
                                        var existedSKU = $(this).attr("data-sku");
                                        if (sku == existedSKU) {
                                            check = true;
                                        }
                                    });
                                    if (check == false) {
                                        html += "<tr class=\"product-result\" data-giabansi=\"" + item.Giabansi + "\" data-giabanle=\"" + item.Giabanle + "\" " +
                                            "data-quantityinstock=\"" + item.QuantityInstock + "\" data-productimageorigin=\"" + item.ProductImageOrigin + "\" " +
                                            "data-productvariable=\"" + item.ProductVariable + "\" data-productname=\"" + item.ProductName + "\" " +
                                            "data-sku=\"" + item.SKU + "\" data-producttype=\"" + item.ProductType + "\" data-id=\"" + item.ID + "\" " +
                                            "data-productnariablename=\"" + item.ProductVariableName + "\" " +
                                            "data-productvariablevalue =\"" + item.ProductVariableValue + "\" " +
                                            "data-productvariablesave =\"" + item.ProductVariableSave + "\">";
                                        html += "   <td>" + item.ProductImage + "";
                                        html += "   <td>" + item.ProductName + "</td>";
                                        html += "   <td>" + item.SKU + "</td>";
                                        html += "   <td>" + item.ProductVariable + "</td>";
                                        html += "   <td class=\"gia-san-pham\" data-price=\"" + item.Giabansi + "\">" + item.stringGiabansi + "</td>";
                                        html += "   <td>" + item.QuantityInstockString + "</td>";
                                        html += "   <td><input type=\"text\" class=\"form-control in-quantity\" value=\"1\" onkeypress='return event.charCode >= 48 && event.charCode <= 57'/></td>";
                                        html += "   <td><a href=\"javascript:;\" class=\"link-btn\" onclick=\"deleteRow($(this))\"><i class=\"fa fa-trash\"></i></a></td>";
                                        html += "</tr>";
                                    }
                                    else if (check == true) {
                                        $(".product-result").each(function () {
                                            var existedSKU = $(this).attr("data-sku");
                                            if (sku == existedSKU) {
                                                var quantityinstock = parseFloat($(this).attr("data-quantityinstock"));
                                                var quantityCurrent = parseFloat($(this).find(".in-quantity").val());
                                                var newquantity = quantityCurrent + 1;
                                                if (newquantity <= quantityinstock) {
                                                    $(this).find(".in-quantity").val(newquantity);
                                                }
                                            }
                                        });
                                    }
                                }
                                $(".content-product").append(html);
                                $("#txtSearch").val("");
                            }
                            else {
                                alert('Không tìm thấy sản phẩm');
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

            function payall() {

                if ($(".product-result").length > 0) {
                    var list = "";

                    $(".product-result").each(function () {
                        var id = $(this).attr("data-id");
                        var sku = $(this).attr("data-sku");
                        var quantity = $(this).find(".in-quantity").val();
                        if (quantity > 0) {
                            list += id + "," + sku + "," + quantity + ";";
                        }
                    });
                    $("#<%=hdfListProduct.ClientID%>").val(list);
                    $("#<%=btnOrder.ClientID%>").click();
                }
                else {
                    alert("Hãy nhập sản phẩm để in mã vạch");
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
                        var quantity = $(this).find(".in-quantity").val();
                        var productname = $(this).attr("data-productname");
                        var productimageorigin = $(this).attr("data-productimageorigin");
                        var productvariable = $(this).attr("data-productvariable");
                        var productvariablesave = $(this).attr("data-productvariablesave");
                        if (quantity > 0) {
                            list += id + "," + sku + "," + producttype + "," + productnariablename + "," + productvariablevalue + "," + quantity + "," + productname + "," + productimageorigin + "," + productvariablesave + "," + productvariablesave + ";";
                            count++;
                        }
                    });
                    if (count > 0) {

                    }
                    else {
                        alert('Vui lòng nhập số lượng để xuất kho');
                    }
                }
                else {
                    alert("Vui lòng nhập sản phẩm");
                }
            }

            function checkQuantiy(obj) {
                var current = obj.val();
                if (current == 0 || current == "" || current == null) {
                    obj.val("1");
                }
            }

            function pressKeyQuantity(e) {

                $(".in-quantity").keyup(function (e) {
                    if (/\D/g.test(this.value)) {
                        // Filter non-digits from input value.
                        this.value = this.value.replace(/\D/g, '');
                    }
                    else if (e.which == 40) {
                        // press down 
                        $(this).closest('tr').next().find('td:eq(' + $(this).closest('td').index() + ')').find(".in-quantity").focus().select();
                    }
                    else if (e.which == 38) {
                        // press up
                        $(this).closest('tr').prev().find('td:eq(' + $(this).closest('td').index() + ')').find(".in-quantity").focus().select();
                    }
                });
                checkQuantiy(e);
            }

            var formatThousands = function (n, dp) {
                var s = '' + (Math.floor(n)), d = n % 1, i = s.length, r = '';
                while ((i -= 3) > 0) { r = ',' + s.substr(i, 3) + r; }
                return s.substr(0, i + 3) + r +
                    (d ? '.' + Math.round(d * Math.pow(10, dp || 2)) : '');
            };
        </script>
   </asp:Content>
