<%@ Page Title="Nhập kho sản phẩm" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="tao-phien-kiem-kho.aspx.cs" Inherits="IM_PJ.tao_phien_kiem_kho" EnableSessionState="ReadOnly" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panelborderheading">
                        <div class="panel-heading clear">
                            <h3 class="page-title left not-margin-bot">Nhập kho sản phẩm</h3>
                        </div>
                        <div class="panel-body">
                            <div class="form-row">
                                <div class="col-md-2 col-xs-4">
                                    <asp:DropDownList ID="ddlSearchType" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0" Text="Theo SKU"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Theo Danh Mục"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <input type="text" id="txtSearch" class="form-control sku-input" placeholder="NHẬP MÃ SẢN PHẨM (F3)" style="width: 35%; float: left; margin-right: 10px" autocomplete="off" />
                                <asp:Literal ID="ltrCategory" runat="server"></asp:Literal>
                                <div class="col-md-2 col-xs-4">
                                    <asp:DropDownList ID="ddlStockStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="" Text="Trạng thái kho"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Còn hàng"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Hết hàng"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <a href="javascript:;" class="btn primary-btn fw-btn not-fullwidth" style="margin-right: 10px" onclick="searchProduct()"><i class="fa fa-search" aria-hidden="true"></i> Tìm</a>
                            </div>
                            <div class="form-row">
                            </div>
                            <div class="form-row">
                                <h3 class="no-margin float-left">Kết quả tìm kiếm: <span class="result-numsearch"></span></h3>
                                <div class="excute-in">
                                    <a href="javascript:;" style="background-color: #f87703; float: right;" class="btn primary-btn link-btn" onclick="registerCheckWarehouse()">Đăng ký kiểm tra kho</a>
                                </div>
                            </div>
                            <div class="form-row" style="border: solid 1px #ccc; padding: 10px;">
                                <table class="table table-checkable table-product import-stock">
                                    <thead>
                                        <tr>
                                            <th class="select-column">
                                                <input type="checkbox" id="check-all" class="check-popup"/>
                                            </th>
                                            <th class="name-column">Sản phẩm</th>
                                            <th class="sku-column">Mã</th>
                                            <th class="stock-column">Kho</th>
                                            <th class="trash-column"></th>
                                        </tr>
                                    </thead>
                                    <tbody class="content-product">
                                    </tbody>
                                </table>
                            </div>
                            <div class="post-table-links excute-in clear">
                                <a href="javascript:;" style="background-color: #f87703; float: right;" class="btn primary-btn link-btn" onclick="registerCheckWarehouse()">Đăng ký kiểm tra kho</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hdfvalue" runat="server" />
        <asp:Button ID="btnImport" runat="server" OnClick="btnImport_Click" Style="display: none" />
    </main>

    <script type="text/javascript">

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

        function clickrow(obj) {
            if (!obj.find("td").eq(1).hasClass("checked")) {
                obj.find("td").addClass("checked");
            }
            else {
                obj.find("td").removeClass("checked");
            }
        }

        $("#check-all").change(function () {
            $(".check-popup").prop('checked', $(this).prop("checked"));
        });

        function check() {
            var temp = 0;
            var temp2 = 0;
            $(".product-result").each(function () {
                if ($(this).find(".check-popup").is(':checked')) {
                    temp++;
                }
                else {
                    temp2++;
                }
                if (temp2 > 0) {
                    $("#check-all").prop('checked', false);
                }
                else {
                    $("#check-all").prop('checked', true);
                }
            });
        }

        function searchProduct() {
            let ddlSearchType = +$("#<%=ddlSearchType.ClientID%>").val() || 0;
            let textsearch = $("#txtSearch").val();
            let categoryID = $("#ddlCategory").val();
            let stockStatus = $("#<%=ddlStockStatus.ClientID%>").val();
            
            let data = {
                sku: null,
                categoryID: null,
                stockStatus: null
            };

            // Tìm kiếm theo danh mục
            if (ddlSearchType == 0) {
                if (isBlank(textsearch))
                    return alert('Vui lòng nhập nội dung tìm kiếm');
                data.sku = textsearch;
            }
            else {
                data.categoryID = categoryID != "" ? +categoryID : null;
                data.stockStatus = stockStatus != "" ? +stockStatus : null;
            }

            $.ajax({
                type: "POST",
                url: "/tao-phien-kiem-kho.aspx/getProduct",
                data: JSON.stringify(data),
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
                            var check = false;

                            $(".product-result").each(function () {
                                var existedSKU = $(this).attr("data-sku");
                                if (sku == existedSKU) {
                                    check = true;
                                }
                            });

                            if (check == false) {
                                html += "<tr ondblclick=\"clickrow($(this))\" class=\"product-result\" data-productname=\"" + item.ProductName + "\" data-sku=\"" + item.SKU + "\" data-productStyle=\"" + item.ProductStyle + "\" data-id=\"" + item.ID + "\" + data-warehouse-quantity=\"" + item.WarehouseQuantity + "\">";
                                html += " <td><input type=\"checkbox\" class=\"check-popup\" onchange=\"check()\"  /></td>";
                                html += "   <td class='name-item'><a href='/xem-san-pham?sku=" + item.SKU + "' target='_blank'>" + item.ProductName + "</a></td>";
                                html += "   <td class='sku-item'>" + item.SKU + "</td>";
                                html += "   <td>" + item.WarehouseQuantity + "</td>";
                                html += "   <td class=\"trash-column\"><a href=\"javascript:;\" onclick=\"deleteRow($(this))\"><i class=\"fa fa-trash\"></i></a></td>";
                                html += "</tr>";
                            }
                        }
                        $(".content-product").prepend(html);
                        $("#txtSearch").val("");
                        if ($(".product-result").length > 0) {
                            $(".excute-in").show();
                        }
                    }
                    else {
                        swal('Thông báo', 'Không tìm thấy sản phẩm', 'error');
                    }
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    alert('lỗi');
                }
            });
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

        function registerCheckWarehouse() {
            if ($(".product-result").length > 0) {
                HoldOn.open();

                let list = [];
                $(".product-result").each(function () {
                    var id = +$(this).attr("data-id") || 0;
                    var sku = $(this).attr("data-sku") || '';
                    var productStyle = +$(this).attr("data-productStyle") || 1;
                    var productname = $(this).attr("data-productname") || '';
                    var quantity = +$(this).attr("data-warehouse-quantity").val() || 0;
                    var checked = $(this).find(".check-popup").is(':checked') || false;
                    
                    if (checked) {
                        let data = {
                            ID: id,
                            SKU: sku,
                            ProductStyle: productStyle,
                            ProductName: productname,
                            WarehouseQuantity: quantity
                        };

                        list.push(data);
                    }
                });
 
                $("#<%=hdfvalue.ClientID%>").val(JSON.stringify(list));
                $("#<%=btnImport.ClientID%>").click();
            }
            else {
                alert("Vui lòng nhập sản phẩm");
            }
        }
    </script>
</asp:Content>
