<%@ Page Title="Chuyển kho" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="chuyen-kho.aspx.cs" Inherits="IM_PJ.chuyen_kho" EnableSessionState="ReadOnly" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panelborderheading">
                        <div class="panel-heading clear">
                            <h3 class="page-title left not-margin-bot">Chuyển kho</h3>
                        </div>
                        <div class="panel-body">
                            <div class="form-row">
                                <div class="col-xs-3">
                                    <asp:DropDownList ID="ddlWarehouseTransfer" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0" Text="Hướng chuyển kho"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Kho 1 => Kho 2"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Kho 2 => Kho 1"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <input type="text" id="txtSearch" class="form-control sku-input" placeholder="NHẬP MÃ SẢN PHẨM (F3)" style="width: 40%; float: left; margin-right: 10px" autocomplete="off" disabled="disabled" readonly />
                                <a id="btnSearch" href="javascript:;" class="btn primary-btn fw-btn not-fullwidth" style="margin-right: 10px" onclick="searchProduct()" disabled="disabled">
                                    <i class="fa fa-search" aria-hidden="true"></i> Tìm
                                </a>
                            </div>
                            <div class="form-row">
                            </div>
                            <div class="form-row">
                                <h3 class="no-margin float-left">Kết quả tìm kiếm: <span class="result-numsearch"></span></h3>
                                <div class="excute-in">
                                    <a href="javascript:;" style="background-color: #f87703; float: right;" class="btn primary-btn link-btn" onclick="inProduct()">Chuyển kho</a>
                                    <%--<a href="javascript:;" style="background-color: #ffad00; float: right;" class="btn primary-btn link-btn" onclick="quickInput()">Nhập nhanh số lượng (F2)</a>--%>
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
                                            <th class="stock-column">Kho 1</th>
                                            <th class="stock-column">Kho 2</th>
                                            <th class="quantity-column">Số lượng chuyển</th>
                                            <th class="trash-column"></th>
                                        </tr>
                                    </thead>
                                    <tbody class="content-product">
                                    </tbody>
                                </table>
                            </div>
                            <div class="post-table-links excute-in clear">
                                <a href="javascript:;" style="background-color: #f87703; float: right;" class="btn primary-btn link-btn" onclick="inProduct()">Chuyển kho</a>
                                <%--<a href="javascript:;" style="background-color: #ffad00; float: right;" class="btn primary-btn link-btn" onclick="quickInput()">Nhập nhanh số lượng (F2)</a>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hdfWarehouseTransfer" runat="server" />
        <asp:HiddenField ID="hdfvalue" runat="server" />
        <asp:Button ID="btnImport" runat="server" OnClick="btnImport_Click" Style="display: none" />
    </main>
    <script type="text/javascript">
        // focus to searchProduct input when page on ready
        $(document).ready(function () {
            // Event change Warehouse Transfer
            onChangeWareHouseTransfer()

            // Transfer
            let urlParams = new URLSearchParams(window.location.search);
            if (urlParams.has('transfer')) {
                $("#<%= ddlWarehouseTransfer.ClientID%>").val(urlParams.get('transfer'));
                $("#<%= ddlWarehouseTransfer.ClientID%>").trigger('change');
            }

            // Input search
            $("#txtSearch").focus();
            $('#txtSearch').keydown(function (event) {
                if (event.which === 13) {
                    searchProduct();
                    event.preventDefault();
                    return false;
                }
            });
        });

        $(document).keydown(function (e) {
            if (e.which == 113) { //F2 Quick Input
                quickInput();
                return false;
            }
            if (e.which == 114) { //F3 Search Product
                $("#txtSearch").focus();
                return false;
            }
        });

        function onChangeWareHouseTransfer() {
            $("#<%= ddlWarehouseTransfer.ClientID%>").change(function () {
                let $txtSearch = $("#txtSearch");
                let $btnSearch = $("#btnSearch");

                if ($(this).val() !== "0") {
                    $txtSearch.removeAttr("disabled");
                    $txtSearch.removeAttr("readonly");
                    $btnSearch.removeAttr("disabled");

                    $(this).attr("disabled", true);
                    $(this).attr("readonly", true);

                    $txtSearch.focus();
                }
            });
        }

        function clickrow(obj) {
            if (!obj.find("td").eq(1).hasClass("checked")) {
                obj.find("td").addClass("checked");
            }
            else {
                obj.find("td").removeClass("checked");
            }
        }

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

        function searchProduct() {
            var transfer = +$("#<%= ddlWarehouseTransfer.ClientID %>").val() || 0;
            var textsearch = $("#txtSearch").val();

            if (transfer == 0)
                return swal({
                    type: "error",
                    title: "Thông báo",
                    text: "Vui lòng chọn kho muốn chuyển hàng"
                });

            if (isBlank(textsearch))
                return swal({
                    type: "error",
                    title: "Thông báo",
                    text: "Vui lòng nhập nội dung tìm kiếm"
                });

            $.ajax({
                type: "POST",
                url: "/chuyen-kho.aspx/getProduct",
                data: JSON.stringify({ transfer: transfer, textsearch: textsearch }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var count = 0;
                    var data = JSON.parse(msg.d);
                    if (data.length > 0) {
                        var html = "";
                        for (var i = 0; i < data.length; i++) {
                            var item = data[i];
                            var sku = item.SKU;
                            var supplierID = item.SupplierID;
                            var check = false;
                            $(".product-result").each(function () {
                                var existedSKU = $(this).attr("data-sku");
                                if (sku == existedSKU) {
                                    check = true;
                                }
                            });

                            if (check == false) {
                                html += "<tr ondblclick=\"clickrow($(this))\" class=\"product-result\"";
                                html += "       data-productimageorigin=\"" + item.ProductImageOrigin + "\"";
                                html += "       data-productvariable=\"" + item.ProductVariable + "\"";
                                html += "       data-productname=\"" + item.ProductName + "\"";
                                html += "       data-sku=\"" + item.SKU + "\"";
                                html += "       data-productstyle=\"" + item.ProductStyle + "\"";
                                html += "       data-id=\"" + item.ID + "\"";
                                html += "       data-productnariablename=\"" + item.ProductVariableName + "\"";
                                html += "       data-productvariablevalue =\"" + item.ProductVariableValue + "\"";
                                html += "       data-stock1=\"" + (+item.WarehouseQuantity || 0) + "\"";
                                html += "       data-stock2=\"" + (+item.WarehouseQuantity2 || 0) + "\"";
                                html += "       data-parentid=\"" + item.ParentID + "\"";
                                html += "       data-parentsku=\"" + item.ParentSKU + "\"";
                                html += ">";
                                html += "   <td class='image-item'><img onclick='openImage($(this))' src='" + item.ProductImage + "'></td>";
                                html += "   <td class='name-item'><a href='/xem-san-pham?sku=" + item.SKU + "' target='_blank'>" + item.ProductName + "</a></td>";
                                html += "   <td class='sku-item'>" + item.SKU + "</td>";
                                html += "   <td class='price-item'>" + item.ProductVariable.replace(/\|/g, "<br>") + "</td>";
                                html += "   <td>" + item.WarehouseQuantity + "</td>";
                                html += "   <td>" + item.WarehouseQuantity2 + "</td>";
                                html += "   <td><input type=\"text\" class=\"form-control in-quantity\" pattern=\"[0-9]{1,3}\" onkeyup=\"pressKeyQuantity($(this))\" onkeypress=\"return event.charCode >= 48 && event.charCode <= 57\" value=\"1\" onblur='blurQuantity($(this), " + (+item.WarehouseQuantity || 0) + ", " + (+item.WarehouseQuantity2 || 0) + ")'/></td>";
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
                        $(".content-product").prepend(html);
                        $("#txtSearch").val("");
                        if (count > 0) {
                            $(".excute-in").show();
                        }
                    }
                    else {
                        swal('Thông báo', 'Sản phẩm hiện tại không có trong kho', 'warning');
                    }
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    alert('lỗi');
                }
            });
        }

        // change quantity of product
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
                else if (e.which == 40)
                {
                    // press down 
                    $(this).closest('tr').next().find('td:eq(' + $(this).closest('td').index() + ')').find(".in-quantity").focus().select();
                }
                else if (e.which == 38) 
                {
                    // press up
                    $(this).closest('tr').prev().find('td:eq(' + $(this).closest('td').index() + ')').find(".in-quantity").focus().select();
                }
            });
            checkQuantiy(e);
        }

        function blurQuantity($input, stock1, stock2) {
            let transfer = +$("#<%= ddlWarehouseTransfer.ClientID %>").val() || 0;

            if (transfer == 0)
                return;

            let quantityTransfer = +$input.val() || 0;
            let quantityCurrent = transfer == 1 ? stock1 : stock2;

            if (quantityTransfer > quantityCurrent) {
                return swal({
                    type: "error",
                    title: "Thông báo",
                    text: "Số lượng chuyển kho (" + quantityTransfer + " cái) đã quá số lượng kho " + transfer + " (" + quantityCurrent + " cái)"
                }, function () {
                    $input.focus();
                    $input.select();
                })
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
            let transfer = +$("#<%= ddlWarehouseTransfer.ClientID %>").val() || 0;

            if (transfer == 0)
                return swal({
                    type: "error",
                    title: "Thông báo",
                    text: "Vui lòng chọn kho muốn chuyển hàng"
                });
            else
                $("#<%=hdfWarehouseTransfer.ClientID%>").val(transfer);

            if ($(".product-result").length > 0) {
                HoldOn.open();
                var note = $("#txtnote").val();
                var list = [];
                $(".product-result").each(function () {
                    var productstyle = +$(this).attr("data-productstyle") || 1;
                    var parentID = +$(this).attr("data-parentid") || 0;
                    var id = +$(this).attr("data-id") || 0;
                    var sku = $(this).attr("data-sku");
                    var parentSKU = $(this).attr("data-parentsku");
                    var quantity = +$(this).find(".in-quantity").val() || 0;
                    var stock1 = +$(this).attr("data-stock1") || 0;
                    var stock2 = +$(this).attr("data-stock2") || 0;

                    if (quantity > 0) {
                        let item = {
                            "productStyle": productstyle,
                            "productID": parentID,
                            "productVariableID": productstyle == 1 ? 0 : id,
                            "sku": sku,
                            "parentSKU": parentSKU,
                            "quantity": quantity,
                            "stock1": stock1,
                            "stock2": stock2,
                        };

                        list.push(item);
                    }
                });
                $("#<%=hdfvalue.ClientID%>").val(JSON.stringify(list));
                HoldOn.close();
                $("#<%=btnImport.ClientID%>").click();
            }
            else {
                alert("Vui lòng nhập sản phẩm");
            }
        }
    </script>
</asp:Content>
