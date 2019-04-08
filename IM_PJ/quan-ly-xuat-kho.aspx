<%@ Page Title="Xuất kho sản phẩm" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="quan-ly-xuat-kho.aspx.cs" Inherits="IM_PJ.quan_ly_xuat_kho" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panelborderheading">
                        <div class="panel-heading clear">
                            <h3 class="page-title left not-margin-bot">Xuất kho sản phẩm</h3>
                        </div>
                        <div class="panel-body">
                            <div class="form-row">
                                <input type="text" id="txtSearch" class="form-control sku-input" placeholder="NHẬP MÃ SẢN PHẨM (F3)" style="width: 40%; float: left; margin-right: 10px" autocomplete="off" />
                                <asp:Literal ID="ltrSupplier" runat="server"></asp:Literal>
                                <a href="javascript:;" class="btn primary-btn fw-btn not-fullwidth" style="margin-right: 10px" onclick="searchProduct()"><i class="fa fa-search" aria-hidden="true"></i> Tìm</a>
                            </div>
                            <div class="form-row">
                            </div>
                            <div class="form-row">
                                <h3 class="no-margin float-left">Kết quả tìm kiếm: <span class="result-numsearch"></span></h3>
                                <div class="float-right excute-in">
                                    <a href="javascript:;" class="btn primary-btn fw-btn not-fullwidth" onclick="noteExportStock()">Xuất kho</a>
                                </div>
                            </div>
                            <div class="form-row" style="border: solid 1px #ccc; padding: 10px;">
                                <table class="table table-checkable table-product import-stock">
                                    <thead>
                                        <tr>
                                            <th class="image-column">Ảnh</th>
                                            <th class="name-column">Sản phẩm</th>
                                            <th class="sku-column">Mã</th>
                                            <th class="variable-column">Thuộc tính</th>
                                            <th class="supplier-column">Nhà cung cấp</th>
                                            <th class="stock-column">Kho</th>
                                            <th class="quantity-column">Số lượng</th>
                                            <th class="trash-column"></th>
                                        </tr>
                                    </thead>
                                    <tbody class="content-product">
                                    </tbody>
                                </table>
                            </div>
                            <div class="form-row excute-in">
                                <div class="float-right"><a href="javascript:;" class="btn primary-btn fw-btn not-fullwidth" onclick="noteExportStock()">Xuất kho</a></div>
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
        $(document).keydown(function (e) {
            if (e.which == 114) { //F3 Search Product
                $("#txtSearch").focus();
                return false;
            }
        });

        // focus to searchProduct input when page on ready
        $(document).ready(function () {
            $("#txtSearch").focus();
        });

        $('#txtSearch').keydown(function (event) {
            if (event.which === 13) {
                searchProduct();
                event.preventDefault();
                return false;
            }
        });

        function searchProduct() {
            var textsearch = $("#txtSearch").val();
            var supplier = $("#supplierList").val();
            if (!isBlank(textsearch)) {
                $.ajax({
                    type: "POST",
                    url: "/quan-ly-xuat-kho.aspx/getProduct",
                    data: "{textsearch:'" + textsearch + "'}",
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
                                if (supplier != 0)
                                {
                                    if (supplierID == supplier) {
                                        if (check == false) {
                                            html += "<tr class=\"product-result\" data-quantityinstock=\"" + item.QuantityInstock + "\" data-productimageorigin=\"" + item.ProductImageOrigin + "\" data-productvariable=\"" + item.ProductVariable + "\" data-productname=\"" + item.ProductName + "\" data-sku=\"" + item.SKU + "\" data-producttype=\"" + item.ProductType + "\" data-id=\"" + item.ID + "\" data-productnariablename=\"" + item.ProductVariableName + "\" data-productvariablevalue =\"" + item.ProductVariableValue + "\">";
                                            html += "   <td>" + item.ProductImage + "";
                                            html += "   <td>" + item.ProductName + "</td>";
                                            html += "   <td>" + item.SKU + "</td>";
                                            html += "   <td>" + item.SupplierName + "</td>";
                                            html += "   <td>" + item.ProductVariable + "</td>";
                                            html += "   <td>" + item.QuantityInstockString + "</td>";
                                            html += "   <td><input type=\"text\" max=\"" + item.QuantityInstock + "\" pattern=\"[0-9]{1,3}\" class=\"form-control in-quantity\" value=\"1\" onkeyup=\"pressKeyQuantity($(this))\" onkeypress='return event.charCode >= 48 && event.charCode <= 57'/></td>";
                                            html += "   <td class=\"trash-column\"><a href=\"javascript:;\" onclick=\"deleteRow($(this))\"><i class=\"fa fa-trash\"></i></a></td>";
                                            html += "</tr>";
                                        }
                                        else {
                                            $(".product-result").each(function () {
                                                var skuFind = $(this).attr("data-sku");
                                                if (skuFind == sku) {
                                                    var quantityOld = parseFloat($(this).find(".in-quantity").val());
                                                    var quantityNew = quantityOld + 1;
                                                    $(this).find(".in-quantity").val(quantityNew);
                                                }
                                            });
                                        }
                                    }
                                    else {
                                        alert('Không tìm thấy sản phẩm');
                                    }
                                }
                                else
                                {
                                    if (check == false) {
                                        html += "<tr class=\"product-result\" data-quantityinstock=\"" + item.QuantityInstock + "\" data-productimageorigin=\"" + item.ProductImageOrigin + "\" data-productvariable=\"" + item.ProductVariable + "\" data-productname=\"" + item.ProductName + "\" data-sku=\"" + item.SKU + "\" data-producttype=\"" + item.ProductType + "\" data-id=\"" + item.ID + "\" data-productnariablename=\"" + item.ProductVariableName + "\" data-productvariablevalue =\"" + item.ProductVariableValue + "\">";
                                        html += "   <td>" + item.ProductImage + "";
                                        html += "   <td>" + item.ProductName + "</td>";
                                        html += "   <td>" + item.SKU + "</td>";
                                        html += "   <td>" + item.SupplierName + "</td>";
                                        html += "   <td>" + item.ProductVariable + "</td>";
                                        html += "   <td>" + item.QuantityInstockString + "</td>";
                                        html += "   <td><input type=\"text\" max=\"" + item.QuantityInstock + "\" pattern=\"[0-9]{1,3}\" class=\"form-control in-quantity\" value=\"1\" onkeyup=\"pressKeyQuantity($(this))\" onkeypress='return event.charCode >= 48 && event.charCode <= 57'/></td>";
                                        html += "   <td class=\"trash-column\"><a href=\"javascript:;\" onclick=\"deleteRow($(this))\"><i class=\"fa fa-trash\"></i></a></td>";
                                        html += "</tr>";
                                    }
                                    else {
                                        $(".product-result").each(function () {
                                            var skuFind = $(this).attr("data-sku");
                                            if (skuFind == sku) {
                                                var quantityOld = parseFloat($(this).find(".in-quantity").val());
                                                var quantityNew = quantityOld + 1;
                                                $(this).find(".in-quantity").val(quantityNew);
                                            }
                                        });
                                    }
                                    count++;
                                }
                            }
                            $(".content-product").append(html);
                            $("#txtSearch").val("");
                            if (count > 0) {
                                $(".excute-in").show();
                            }
                        }
                        else
                        {
                            alert('Sản phẩm hiện tại không có trong kho');
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
                    var quantity = $(this).find(".in-quantity").val();
                    var productname = $(this).attr("data-productname");
                    var productimageorigin = $(this).attr("data-productimageorigin");
                    var productvariable = $(this).attr("data-productvariable");
                    if (quantity > 0) {
                        list += id + "," + sku + "," + producttype + "," + productnariablename + "," + productvariablevalue + "," + quantity + "," + productname + "," + productimageorigin + "," + productvariable + ";";
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

        function noteExportStock() {
            fr = "<div class=\"form-row\">";
            fr += "    <label class=\"lbl-popup\">Nội dung xuất kho</label>";
            fr += "    <textarea id=\"txtnote\" class=\"form-control\" placeholder=\"Có thể để trống\"/>";
            fr += "</div>";
            fr += "<div class=\"btn-content\">";
            fr += "    <a class=\"btn primary-btn fw-btn float-right-btn not-fullwidth\" href=\"javascript:;\" onclick=\"inProduct()\" >Xác nhận</a>";
            fr += "</div>";
            showPopup(fr);
        }

        function checkQuantiy(obj) {
            var current = obj.val();
            if (current == 0 || current == "" || current == null) {
                obj.val("1");
            }

            var instock = parseFloat(obj.parent().parent().attr("data-quantityinstock"));
            var current = parseFloat(obj.val());
            var check = true;
            if (current > instock) {
                obj.val(instock);
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

        
    </script>
</asp:Content>
