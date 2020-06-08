<%@ Page Title="Chuyển sản phẩm san kệ khác" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="chuyen-ke.aspx.cs" Inherits="IM_PJ.chuyen_ke" EnableSessionState="ReadOnly" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="parent" runat="server">
        <main id="main-wrap">
            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panelborderheading">
                            <div class="panel-heading clear">
                                <h3 class="page-title left not-margin-bot">Chuyển sản phẩm qua kệ khác</h3>
                            </div>
                            <div class="panel-body">
                                <div class="filter-above-wrap clear">
                                    <div class="filter-control">
                                        <div class="row">
                                            <h5 style="margin-left: 1%">Chọn kệ cần chuyển tới</h5>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-9 col-xs-12">
                                                <div class="row">
                                                    <%-- Filter: Floor --%>
                                                    <div class="col-md-3 col-xs-6 margin-bottom-15">
                                                        <asp:DropDownList ID="ddlFloor" runat="server" CssClass="form-control select2" Width="100%"></asp:DropDownList>
                                                    </div>
                                                    <%-- Filter: Row --%>
                                                    <div class="col-md-3 col-xs-6 margin-bottom-15">
                                                        <asp:DropDownList ID="ddlRow" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                    <%-- Filter: Shelf --%>
                                                    <div class="col-md-3 col-xs-6 margin-bottom-15">
                                                        <asp:DropDownList ID="ddlShelf" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                    <%-- Filter: Floor Shelf --%>
                                                    <div class="col-md-3 col-xs-6 margin-bottom-15">
                                                        <asp:DropDownList ID="ddlFloorShelf" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
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
                        <div class="panel-post">
                            <div class="post-above clear">
                                <div class="search-box left">
                                    <input type="text" id="txtSearch" class="form-control sku-input" placeholder="NHẬP MÃ SẢN PHẨM (F3)" autocomplete="off" style="background-color: gray;" disabled>
                                </div>
                            </div>
                            <div class="post-body search-product-content clear">
                                <div class="search-product-content">
                                    <table class="table table-checkable table-product table-sale-order">
                                        <thead>
                                            <tr>
                                                <th class="image-item">Ảnh</th>
                                                <th class="name-item">Sản phẩm</th>
                                                <th class="sku-item">Mã</th>
                                                <th class="variable-item">Thuộc tính</th>
                                                <th class="floor">Lầu</th>
                                                <th class="row">Dãy</th>
                                                <th class="shelf">Kệ</th>
                                                <th class="floor-shelf">Tầng của kệ</th>
                                                <th class="quantity-item">Số lượng</th>
                                                <th class="trash-item">Xóa</th>
                                            </tr>
                                        </thead>
                                        <tbody class="content-product">
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <div class="post-row clear">
                                <div class="left">Số lượng</div>
                                <div class="right totalproductQuantity"></div>
                            </div>
                            <div class="post-table-links clear">
                                <a href="javascript:;" class="btn link-btn" id="payall" style="background-color: #f87703; float: right" title="Hoàn tất kiểm kê" onclick="confirm()"><i class="fa fa-floppy-o"></i>Xác nhận</a>
                                <asp:Button ID="btnConfirm" OnClick="btnConfirm_Click" runat="server" Style="display: none" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="buttonbar">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel-buttonbar">
                            <div class="panel-post">
                                <div class="post-table-links clear row">
                                    <a href="javascript:;" class="btn link-btn" style="background-color: #f87703; float: right" title="Hoàn tất kiểm kê" onclick="confirm()"><i class="fa fa-floppy-o"></i>Xác nhận</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="notAcceptChangeUser" Value="1" runat="server" />
            <asp:HiddenField ID="hdfRoleID" runat="server" />
            <asp:HiddenField ID="hdfUsername" runat="server" />
            <asp:HiddenField ID="hdfUsernameCurrent" runat="server" />
            <asp:HiddenField ID="hdfProductShelf" runat="server" />
            <asp:HiddenField ID="hdfFloor" runat="server" />
            <asp:HiddenField ID="hdfRow" runat="server" />
            <asp:HiddenField ID="hdfShelf" runat="server" />
            <asp:HiddenField ID="hdfFloorShelf" runat="server" />
        </main>
    </asp:Panel>
    <style>
        .search-product-content {
            height: initial !important;
            min-height: 200px;
            background: #fff;
        }

        #txtSearch {
            width: 100%;
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
            padding: 20px 20px;
            right: 0%;
            margin: 0 auto;
        }

        .select2-container.select2-container--default.select2-container--open {
            z-index: 99991;
        }
    </style>
    <telerik:RadAjaxManager ID="rAjax" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnConfirm">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="parent" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadScriptBlock ID="sc" runat="server">
        <script type="text/javascript">
            "use strict";
            class Product {
                constructor(
                    uuid,
                    productID,
                    variableID,
                    sku,
                    title,
                    image,
                    color,
                    size,
                    quantity,
                    floor,
                    floorName,
                    row,
                    rowName,
                    shelf,
                    shelfName,
                    floorShelf,
                    floorShelfName
                    )
                {
                    this.uuid = uuid;
                    this.productID = productID;
                    this.variableID = variableID;
                    this.sku = sku;
                    this.title = title;
                    this.image = image;
                    this.color = color;
                    this.size = size;
                    this.quantity = quantity;
                    this.floor = floor;
                    this.floorName = floorName;
                    this.row = row;
                    this.rowName = rowName;
                    this.shelf = shelf;
                    this.shelfName = shelfName;
                    this.floorShelf = floorShelf;
                    this.floorShelfName = floorShelfName;
                }
            }

            var products = [];

            $(document).keydown(function (e) {
                if (e.which == 114) { //F3 Search Product
                    let txtSearch = $("#txtSearch");

                    if (!txtSearch.prop('disabled'))
                        $("#txtSearch").focus();
                    return false;
                }
            });

            $(document).ready(() => {
                onChangeFloor();
                onChangeRow();
                onChangeShelf();
                onChangeFloorShelf();

                keyDownSearch();
            });
            // Loading drop downlist row with parentID
            function loadRow(parentID) {
                let ddlRow = $("#<%=ddlRow.ClientID%>");
                let option = "";

                ddlRow.html("");
                ddlRow.attr('disabled', 'true');
                ddlRow.css('background', 'gray');
                option = "<option selected='selected' value='0'>Chọn dãy</option>";

                if (parentID) {
                    $.ajax({
                        type: "POST",
                        url: "/kiem-ke.aspx/getRows",
                        async: false,
                        data: JSON.stringify({ 'parentID': parentID }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: respon => {
                            let data = respon.d;
                            if (data) {
                                data.forEach(item => {
                                    option += "<option value='" + item.Value + "'>" + item.Text + "</option>";
                                });
                                ddlRow.removeAttr('disabled');
                                ddlRow.css('background', '');
                            }
                        }
                    });
                }

                ddlRow.html(option);
            }
            // Loading drop downlist shelf with parentID
            function loadShelf(parentID) {
                let ddlShelf = $("#<%=ddlShelf.ClientID%>");
                let option = "";

                ddlShelf.html("");
                ddlShelf.attr('disabled', 'true');
                ddlShelf.css('background', 'gray');
                option = "<option selected='selected' value='0'>Chọn kệ</option>";

                if (parentID) {
                    $.ajax({
                        type: "POST",
                        url: "/kiem-ke.aspx/getShelfs",
                        async: false,
                        data: JSON.stringify({ 'parentID': parentID }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: respon => {
                            let data = respon.d;
                            if (data) {
                                data.forEach(item => {
                                    option += "<option value='" + item.Value + "'>" + item.Text + "</option>";
                                });
                                ddlShelf.removeAttr('disabled');
                                ddlShelf.css('background', '');
                            }

                        },
                    });
                }

                ddlShelf.html(option);
            }
            // Loading drop downlist floor shelf with parentID
            function loadFloorShelf(parentID) {
                let ddlFloorShelf = $("#<%=ddlFloorShelf.ClientID%>");
                let option = "";

                ddlFloorShelf.html("");
                ddlFloorShelf.attr('disabled', 'true');
                ddlFloorShelf.css('background', 'gray');
                option = "<option selected='selected' value='0'>Chọn tầng</option>";

                if (parentID) {
                    $.ajax({
                        type: "POST",
                        url: "/kiem-ke.aspx/getFloorShelfs",
                        async: false,
                        data: JSON.stringify({ 'parentID': parentID }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: respon => {
                            let data = respon.d;
                            if (data) {
                                data.forEach(item => {
                                    option += "<option value='" + item.Value + "'>" + item.Text + "</option>";
                                });
                                ddlFloorShelf.removeAttr('disabled');
                                ddlFloorShelf.css('background', '');
                            }
                        },
                    });
                }

                ddlFloorShelf.html(option);
            }
            // Handle on change drop down list floor
            function onChangeFloor() {
                $("#<%=ddlFloor.ClientID%>").change(e => {
                    let parentID = parseInt(e.currentTarget.value);
                    let txtSearch = $("#txtSearch");

                    if (parentID) {
                        loadRow(parentID);
                    }
                    else {
                        loadRow(0);
                    }

                    loadShelf(0);
                    loadFloorShelf(0);
                    txtSearch.attr('disabled', 'true');
                    txtSearch.css('background-color', 'gray');
                });

            }
            // Handle on change drop down list row
            function onChangeRow() {
                $("#<%=ddlRow.ClientID%>").change(e => {
                    let parentID = parseInt(e.currentTarget.value);
                    let txtSearch = $("#txtSearch");
                    if (parentID) {
                        loadShelf(parentID);
                    }
                    else {
                        loadShelf(0);
                    }

                    loadFloorShelf(0);
                    txtSearch.attr('disabled', 'true');
                    txtSearch.css('background-color', 'gray');
                });

            }
            // Handle on change drop down list shelf
            function onChangeShelf() {
                $("#<%=ddlShelf.ClientID%>").change(e => {
                    let parentID = parseInt(e.currentTarget.value);
                    let txtSearch = $("#txtSearch");

                    if (parentID) {
                        loadFloorShelf(parentID);
                    }
                    else {
                        loadFloorShelf(0);
                    }

                    txtSearch.attr('disabled', 'true');
                    txtSearch.css('background-color', 'gray');
                });
            }
            // Handle on change drop down list floor shelf
            function onChangeFloorShelf() {
                $("#<%=ddlFloorShelf.ClientID%>").change(e => {
                    let parentID = parseInt(e.currentTarget.value);
                    let txtSearch = $("#txtSearch");
                    if (parentID) {
                        txtSearch.removeAttr('disabled');
                        txtSearch.css('background-color', '');
                        $("#txtSearch").focus();
                    }
                    else {
                        txtSearch.attr('disabled', 'true');
                        txtSearch.css('background-color', 'gray');
                    }
                });
            }

            // Thư thi lấy thông tin sản phẩm
            // Bắt sự kiện nhấp enter search
            function keyDownSearch() {
                let txtSearch = $("#txtSearch");

                txtSearch.keydown(function (event) {
                    if (event.which === 13) {
                        let sku = txtSearch.val().trim().toUpperCase();
                        let floor = $("#<%=ddlFloor.ClientID%>").val();
                        let row = $("#<%=ddlRow.ClientID%>").val();
                        let shelf = $("#<%=ddlShelf.ClientID%>").val();
                        let floorShelf = $("#<%=ddlFloorShelf.ClientID%>").val();

                        txtSearch.val("");
                        event.preventDefault();

                        //Get search product
                        searchProduct(sku, floor, row, shelf, floorShelf);

                        return false;
                    }
                });
            }
            // Gọi API get thông tin sản phẩm
            function searchProduct(sku, floor, row, shelf, floorShelf) {
                if (!isBlank(sku)) {
                    $.ajax({
                        type: "POST",
                        url: "/chuyen-ke.aspx/getProduct",
                        async: false,
                        data: JSON.stringify({ 'sku': sku, 'floor': floor, 'row': row, 'shelf': shelf, 'floorShelf': floorShelf }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: respon => {
                            var data = respon.d;
                            if (data) {
                                data.forEach(item => {
                                    let isProductNew = true;

                                    products.forEach(product => {
                                        if (product.productID == item.productID &&
                                            product.variableID == item.variableID &&
                                            product.floor == item.floor &&
                                            product.row == item.row &&
                                            product.shelf == item.shelf &&
                                            product.floorShelf == item.floorShelf
                                            ) {
                                            product.quantity += 1;
                                            $("tr[data-uuid='" + product.uuid + "'] > td > .in-quantity").val(product.quantity);
                                            isProductNew = false;
                                            return false;
                                        }
                                    });

                                    if (isProductNew) {
                                        let productNew = new Product(
                                            uuid.v4(),
                                            item.productID,
                                            item.variableID,
                                            item.sku,
                                            item.title,
                                            item.image,
                                            item.color,
                                            item.size,
                                            item.quantity,
                                            item.floor,
                                            item.floorName,
                                            item.row,
                                            item.rowName,
                                            item.shelf,
                                            item.shelfName,
                                            item.floorShelf,
                                            item.floorShelfName
                                            );
                                        products.push(productNew);
                                        createHTML(productNew);
                                    }
                                });

                                calQuantityProduct();
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
                else {
                    alert('Vui lòng nhập nội dung tìm kiếm');
                }
            }
            // Khởi tạo HTML cho sản phẩm
            function createHTML(item) {
                let html = ""
                if (item) {
                    html += "<tr data-uuid='" + item.uuid + "'>";
                    html += "    <td class='image-item'>";
                    html += "        <img onclick='openImage($(this))' src='/uploads/images/85x113/" + item.image + "'>";
                    html += "    </td>";
                    html += "    <td class='name-item'>";
                    html += "        <a href='/xem-san-pham?sku=" + item.sku + "' target='_blank'>" + item.title + "</a>";
                    html += "    </td>";
                    html += "    <td class='sku-item'>" + item.sku + "</td>";
                    html += "    <td class='variable-item'>";
                    if (item.color)
                        html += "        " + item.color;
                    if (item.size)
                        html += "        <br/>" + item.size;
                    html += "    </td>";
                    html += "    <td class='floor'>" + item.floorName + "</td>";
                    html += "    <td class='row'>" + item.rowName + "</td>";
                    html += "    <td class='shelf'>" + item.shelfName + "</td>";
                    html += "    <td class='floor-shelf'>" + item.floorShelfName + "</td>";
                    html += "    <td class='quantity-item'>";
                    html += "        <input type='text' class='form-control in-quantity'";
                    html += "            pattern='[0-9]{1,3}'";
                    html += "            onblur='checkQuantiy($(this))'";
                    html += "            onkeyup='pressKeyQuantity($(this))'";
                    html += "            onkeypress='return event.charCode >= 48 && event.charCode <= 57'";
                    html += "            value='1' disabled/>";
                    html += "    </td>";
                    html += "    <td class='trash-item'>";
                    html += "        <a href='javascript:;' class='link-btn' onclick='deleteRow($(this))'>";
                    html += "            <i class='fa fa-trash'></i>";
                    html += "        </a>";
                    html += "    </td>";
                    html += "</tr>";

                    $(".content-product").prepend(html);
                }
            }
            // Sự kiện thay đổi số lượng sản phẩm
            function checkQuantiy(obj) {
                var current = obj.val();
                if (current == 0 || current == "" || current == null)
                    obj.val("1");
                calQuantityProduct();
            }
            // Sự kiện press key số lượng sản phẩm
            function pressKeyQuantity(obj) {
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
            }

            // remove product form list
            function deleteRow(obj) {
                swal({
                    title: "Xác nhận",
                    text: "Bạn muốn xóa sản phẩm này?",
                    type: "warning",
                    showCancelButton: true,
                    closeOnConfirm: true,
                    cancelButtonText: "Để em xem lại...",
                    confirmButtonText: "Đúng rồi sếp!",
                }, function (confirm) {
                    if (confirm) {
                        let row = obj.parent().parent();
                        let uuid = row.data('uuid');
                        products = products.filter(item => { return item.uuid != uuid });
                        row.remove();
                        calQuantityProduct();
                    }
                });
            }

            function calQuantityProduct() {
                let totalProduct = 0;

                products.forEach(item => totalProduct += item.quantity);

                if (totalProduct)
                    $(".totalproductQuantity").html(formatThousands(totalProduct));
                else
                    $(".totalproductQuantity").html("");
            }

            // format price
            function formatThousands(n, dp) {
                var s = '' + (Math.floor(n)),
                    d = n % 1,
                    i = s.length,
                    r = '';
                while ((i -= 3) > 0) {
                    r = ',' + s.substr(i, 3) + r;
                }
                return s.substr(0, i + 3) + r +
                    (d ? '.' + Math.round(d * Math.pow(10, dp || 2)) : '');
            };

            // Submit dữ liệu xuống server
            function confirm() {
                let floor = $("#<%=ddlFloor.ClientID%>").val();
                let row = $("#<%=ddlRow.ClientID%>").val();
                let shelf = $("#<%=ddlShelf.ClientID%>").val();
                let floorShelf = $("#<%=ddlFloorShelf.ClientID%>").val();

                if (floor == "0" || row == "0" || shelf == "0" || floorShelf == "0")
                    return swal("Thông báo", "Vui lòng chọn vị trí kệ cần chuyển trước khi xác nhận", "error");

                $("#<%=hdfFloor.ClientID%>").val(floor);
                $("#<%=hdfRow.ClientID%>").val(row);
                $("#<%=hdfShelf.ClientID%>").val(shelf);
                $("#<%=hdfFloorShelf.ClientID%>").val(floorShelf);

                if (products.length > 0)
                {
                    $("#<%=hdfProductShelf.ClientID%>").val(JSON.stringify(products));
                    $("#<%=btnConfirm.ClientID%>").click();
                }
                else {
                    swal("Thông báo", "Vui lòng nhập sản phẩm trước khi xác nhận", "error");
                }
            }
        </script>
    </telerik:RadScriptBlock>

</asp:Content>
