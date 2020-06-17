<%@ Page Title="Tạo phiên kiểm kho" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="tao-phien-kiem-kho.aspx.cs" Inherits="IM_PJ.tao_phien_kiem_kho" EnableSessionState="ReadOnly" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .none-padding-left {
            padding-left: 0px
        }

        .margin-left-10 {
            margin-left: 10px;
        }

        .margin-left-15 {
            margin-left: 15px;
        }

        tr:last-child {
            border-bottom: solid 1px #ccc;
        }

        td[class='select-column'] {
            width: 3%;
            text-align: center !important;
        }

        td[class='name-column'] {
            width: 22%;
        }

        td[class='sku-column'] {
            width: 11%;
        }

        td[class='stock-column'] {
            width: 7%;
        }

        td[class='trash-column'] {
            width: 6%;
        }

        @media only screen and (max-width: 800px) {
            .col-sm-2, .col-sm-3, .col-sm-5, .col-xs-4, .col-xs-8, .col-xs-12 {
                padding-left: 0px;
                padding-right: 0px;
            }

            .btnSearch {
                margin-left: 15px;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="form-row">
                <div class="col-xs-12 none-padding-left">
                    <h3 class="page-title left not-margin-bot">Tạo phiên kiểm kho</h3>
                </div>
            </div>
            <div class="form-row">
                <div class="col-sm-5 col-xs-8 none-padding-left">
                    <asp:TextBox runat="server" ID="txtTestName" CssClass="form-control" placeholder="Tên phiên kiểm kho" autocomplete="off" />
                </div>
                <div class="col-sm-2 col-xs-4">
                    <asp:DropDownList ID="ddlStock" runat="server" CssClass="form-control" Style="background-color: #fff;">
                        <asp:ListItem Value="" Text="Chọn kho"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Kho 1"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Kho 2"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <hr style="border-top: 5px solid #fff;" />
            <div class="form-row">
                <div class="col-sm-5 col-xs-12 margin-bottom-15 none-padding-left">
                    <input type="text" id="txtSearch" class="form-control sku-input" placeholder="Nhập mã sản phẩm cần kiểm kho" autocomplete="off" disabled="disabled" readonly />
                </div>
                <div class="col-sm-3 col-xs-12 margin-bottom-15">
                    <asp:Literal ID="ltrCategory" runat="server"></asp:Literal>
                </div>
                <div class="col-sm-2 col-xs-9 margin-bottom-15">
                    <asp:DropDownList ID="ddlStockStatus" runat="server" CssClass="form-control" disabled="disabled" readonly>
                        <asp:ListItem Value="" Text="Trạng thái kho"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Còn hàng"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Hết hàng"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-sm-2 col-xs-3 margin-bottom-15">
                    <a id="btnSearch" href="javascript:;" class="btn primary-btn fw-btn not-fullwidth btnSearch" onclick="searchProduct()" disabled="disabled" readonly>
                        <i class="fa fa-search" aria-hidden="true"></i> Tìm
                    </a>
                </div>
            </div>
            <div class="form-row hidden">
                <div class="excute-in">
                    <a href="javascript:;" style="background-color: #f87703; float: right;" class="btn primary-btn link-btn" onclick="registerCheckWarehouse()">Tạo phiên kiểm kho</a>
                </div>
                <div class="pagination"></div>
            </div>
            <div class="form-row">
                <table id="product-table" class="col-xs-12 table table-checkable table-product import-stock">
                </table>
            </div>
            <div class="form-row hidden">
                <div class="pagination"></div>
                <div class="post-table-links excute-in clear">
                    <a href="javascript:;" style="background-color: #f87703; float: right;" class="btn primary-btn link-btn" onclick="registerCheckWarehouse()">Tạo phiên kiểm kho</a>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hdfTestName" runat="server" />
        <asp:HiddenField ID="hdfStock" runat="server" />
        <asp:HiddenField ID="hdfvalue" runat="server" />
        <asp:Button ID="btnImport" runat="server" OnClick="btnImport_Click" Style="display: none" />
    </main>

    <script type="text/javascript">
        let _tableType = 'SingleRow';
        let _table = {
            page: 1,
            pageSize: 30,
            data: []
        };

        $(window).resize(function () {
            if ($(window).innerWidth() < 800) {
                if (_tableType != 'MultiRow') {
                    _tableType = 'MultiRow';

                    initTable();
                    drawBodyTable();
                    drawPagination();
                }
            }
            else {
                if (_tableType != 'SingleRow') {
                    _tableType = 'SingleRow';

                    initTable();
                    drawBodyTable();
                    drawPagination();
                }
            }
        });

        $(document).keydown(function (e) {
            if (e.which == 114) { //F3 Search Product
                $("#txtSearch").focus();
                return false;
            }
        });

        $(document).ready(function () {
            if ($(window).innerWidth() < 800) {
                $('.nav-toggle').removeClass('open');
                $('body').removeClass('menuin');
                _tableType = 'MultiRow';
            }
            else {
                _tableType = 'SingleRow';
            }

            initTxtTestName();
            initStock();
            initTxtSearch();
            initTable();
        });

        function initSearchArea() {
            let $txtTestName = $("#<%=txtTestName.ClientID%>");
            let $ddlStock = $("#<%=ddlStock.ClientID%>");
            let $txtSearch = $('#txtSearch');
            let $ddlCategory = $('#ddlCategory');
            let $ddlStockStatus = $("#<%=ddlStockStatus.ClientID%>");
            let $btnSearch = $('#btnSearch');

            let name = $txtTestName.val() || "";
            let stock = $ddlStock.val() || "";

            if (name === "" || $ddlStock.val() == "") {
                // Search
                $txtSearch.attr('disabled', true);
                $txtSearch.attr('readonly', true);
                // Category
                $ddlCategory.css('background-color', 'initial')
                $ddlCategory.attr('disabled', true);
                $ddlCategory.attr('readonly', true);
                // Stock Status
                $ddlStockStatus.css('background-color', 'initial')
                $ddlStockStatus.attr('disabled', true);
                $ddlStockStatus.attr('readonly', true);
                // Search
                $btnSearch.attr('disabled', true);
                $btnSearch.attr('readonly', true);
            }
            else {
                // Test Name
                $("#<%=hdfTestName.ClientID%>").val($txtTestName.val());
                $txtTestName.attr('disabled', true);
                $txtTestName.attr('readonly', true);
                // Stock
                $("#<%=hdfStock.ClientID%>").val($ddlStock.val());
                $ddlStock.attr('disabled', true);
                $ddlStock.attr('readonly', true);
                $ddlStock.removeAttr('style');
                // Search
                $txtSearch.removeAttr('disabled');
                $txtSearch.removeAttr('readonly');
                // Category
                $ddlCategory.css('background-color', '#fff')
                $ddlCategory.removeAttr('disabled');
                $ddlCategory.removeAttr('readonly');
                // Stock Status
                $ddlStockStatus.css('background-color', '#fff')
                $ddlStockStatus.removeAttr('disabled');
                $ddlStockStatus.removeAttr('readonly');
                // Search
                $btnSearch.removeAttr('disabled');
                $btnSearch.removeAttr('readonly');

                $txtSearch.focus();
            }
        }

        function initTxtTestName() {
            let $txtTestName = $("#<%=txtTestName.ClientID%>");

            // Focus ngay khi load trang hoàn tất
            $txtTestName.focus();

            // Check kiểm tra xem đã ghi tên cho đợt kiểm kho chưa
            $txtTestName.blur(function () { initSearchArea(); });

            $txtTestName.keydown(function (event) {
                if (event.which === 13) {
                    event.preventDefault();
                    $txtTestName.trigger('blur');

                    return false;
                }
            });
        }

        function initStock() {
            let $ddlStock = $("#<%=ddlStock.ClientID%>");

            $ddlStock.change(function () { initSearchArea(); });
        }

        function initTxtSearch() {
            let $txtSearch = $('#txtSearch');

            $txtSearch.keydown(function (event) {
                if (event.which === 13) {
                    searchProduct();
                    event.preventDefault();
                    return false;
                }
            });
        }

        function initTable() {
            let html = '';

            if (_tableType == 'SingleRow') {
                html += "<thead>";
                html += "    <tr>";
                html += "        <th style='width: 3%; text-align: center!important'>";
                html += "            <input type='checkbox' id='check-all' class='check-popup' />";
                html += "        </th>";
                html += "        <th style='width: 40%; text-align: left!important'>Sản phẩm</th>";
                html += "        <th style='width: 25%; text-align: left!important'>Mã</th>";
                html += "        <th style='width: 16%; text-align: left!important'>Kho hiện tại</th>";
                html += "        <th style='width: 6%; text-align: center!important; font-size: 22px'>";
                html += "            <a href='javascript:;' onclick='deleteAllRow()'>";
                html += "                <i class='fa fa-trash'></i>";
                html += "            </a>";
                html += "        </th>";
                html += "    </tr>";
                html += "</thead>";
                html += "<tbody class='content-product'>";
                html += "</tbody>";
            }
            else {
                html += "<thead>";
                html += "    <tr>";
                html += "        <th rowspan='2' style='width: 3%; text-align: center!important; border-right: 2px solid #ddd'>";
                html += "            <input type='checkbox' id='check-all' class='check-popup' />";
                html += "        </th>";
                html += "        <th colspan='2' style='width: 91%; text-align: left!important'>Sản phẩm</th>";
                html += "        <th rowspan='2' style='width: 6%; text-align: center!important; font-size: 22px; border-left: 2px solid #ddd'>";
                html += "            <a href='javascript:;' onclick='deleteAllRow()'>";
                html += "                <i class='fa fa-trash'></i>";
                html += "            </a>";
                html += "        </th>";
                html += "    </tr>";
                html += "    <tr>";
                html += "        <th style='width: 50%; text-align: left!important'>Mã</th>";
                html += "        <th style='width: 41%; text-align: left!important; border-left: 2px solid #ddd'>Kho</th>";
                html += "    </tr>";
                html += "</thead>";
                html += "<tbody class='content-product'>";
                html += "</tbody>";
            }

            $('#product-table').html(html);

            $("#check-all").change(function () {
                $(".check-popup").prop('checked', $(this).prop("checked"));
            });
        }

        function drawBodyTable() {
            let html = '';
            let index = (_table.page - 1) * _table.pageSize;
            let data = _table.data.slice(index, index + _table.pageSize);
            let evenRow = false;

            data.forEach(item => {
                if (_tableType == 'SingleRow') {
                    html += "<tr ondblclick='clickrow($(this))' ";
                    html += "    class='product-result' ";
                    html += "    data-productname='" + item.ProductName + "' ";
                    html += "    data-sku='" + item.SKU + "' ";
                    html += "    data-productStyle='" + item.ProductStyle + "' ";
                    html += "    data-id='" + item.ID + "' + ";
                    html += "    data-warehouse-quantity='" + item.WarehouseQuantity + "'";
                    html += ">";
                    html += "    <td style='width: 3%; text-align: center!important; padding: 5px 15px'>";
                    html += "        <input type='checkbox' class='check-popup' onchange='check()' />";
                    html += "    </td>";
                    html += "    <td style='width: 40%; text-align: left!important'>";
                    html += "        <a href='/xem-san-pham?sku=" + item.SKU + "' target='_blank'>" + item.ProductName + "</a>";
                    html += "    </td>";
                    html += "    <td style='width: 25%; text-align: left!important'>" + item.SKU + "</td>";
                    html += "    <td style='width: 16%; text-align: left!important'>" + item.WarehouseQuantity + "</td>";
                    html += "    <td style='width: 6%; text-align: center!important; font-size: 22px'>";
                    html += "        <a href='javascript:;' onclick='deleteRow($(this))'>";
                    html += "            <i class='fa fa-trash'></i>";
                    html += "        </a>";
                    html += "    </td>";
                    html += "</tr>";
                }
                else {
                    html += "<tr ondblclick='clickrow($(this))' ";
                    html += "    class='product-result' ";
                    html += "    data-productname='" + item.ProductName + "' ";
                    html += "    data-sku='" + item.SKU + "' ";
                    html += "    data-productStyle='" + item.ProductStyle + "' ";
                    html += "    data-id='" + item.ID + "' + ";
                    html += "    data-warehouse-quantity='" + item.WarehouseQuantity + "'";
                    html += ">";
                    html += "    <td rowspan='2' style='width: 3%; text-align: center!important; padding: 5px 15px; border-right: 1px solid #ddd; background-color: " + (!evenRow ? "#f8f8f8" : "#fff") + "'>";
                    html += "        <input type='checkbox' class='check-popup' onchange='check()' />";
                    html += "    </td>";
                    html += "    <td colspan='2' style='width: 91%; text-align: left!important; background-color: " + (!evenRow ? "#f8f8f8" : "#fff") + "'>";
                    html += "        <a href='/xem-san-pham?sku=" + item.SKU + "' target='_blank'>" + item.ProductName + "</a>";
                    html += "    </td>";
                    html += "    <td rowspan='2' style='width: 6%; text-align: center!important; font-size: 22px; border-left: 1px solid #ddd; background-color: " + (!evenRow ? "#f8f8f8" : "#fff") + "'>";
                    html += "        <a href='javascript:;' onclick='deleteRow($(this))'>";
                    html += "            <i class='fa fa-trash'></i>";
                    html += "        </a>";
                    html += "    </td>";
                    html += "</tr>";
                    html += "<tr ondblclick='clickrow($(this))' ";
                    html += "    class='product-result' ";
                    html += "    data-productname='" + item.ProductName + "' ";
                    html += "    data-sku='" + item.SKU + "' ";
                    html += "    data-productStyle='" + item.ProductStyle + "' ";
                    html += "    data-id='" + item.ID + "' + ";
                    html += "    data-warehouse-quantity='" + item.WarehouseQuantity + "'";
                    html += ">";
                    html += "    <td style='width: 50%; text-align: left!important; background-color: " + (!evenRow ? "#f8f8f8" : "#fff") + "'>" + item.SKU + "</td>";
                    html += "    <td style='width: 41%; text-align: left!important; border-left: 1px solid #ddd; background-color: " + (!evenRow ? "#f8f8f8" : "#fff") + "'>" + item.WarehouseQuantity + "</td>";
                    html += "</tr>";

                    evenRow = !evenRow;
                }
            });

            $(".content-product").html(html);

            let $btnExcute = $(".excute-in");

            if ($(".product-result").length > 0) {
                $btnExcute.parent().removeClass('hidden');
                $btnExcute.show();
            }
            else {
                $btnExcute.parent().addClass('hidden');
                $btnExcute.hide();
            }

            if ($(".check-popup").is(':checked'))
                $(".check-popup").prop('checked', false);
        }

        function drawPagination() {
            let $pagination = $(".pagination");
            let totalPage = Math.ceil(_table.data.length / _table.pageSize);

            if (totalPage == 1) {
                $pagination.hide();
                return;
            }

            let html = '';
            let pageMax = _tableType == 'SingleRow' ? 6 : 4;
            let startPage = 1;
            let endPage = startPage + pageMax - 1;

            if (totalPage > pageMax) {
                startPage = _table.page - Math.floor(pageMax / 2);
                endPage = startPage + pageMax - 1;

                if (startPage < 1) {
                    startPage = 1;
                    endPage = startPage + pageMax - 1;
                }

                if (endPage > totalPage) {
                    endPage = totalPage;
                    startPage = endPage - (pageMax - 1);
                }
            }
            else {
                endPage = totalPage;
            }

            html += '<ul>';
            if (startPage != 1)
                html += "   <li><a href='javascript:;' onclick='goPage(1)'>Trang đầu</a></li>";

            if (_tableType == 'SingleRow' && _table.page > 1)
                html += "   <li><a href='javascript:;' onclick='goPage(" + (_table.page - 1) + ")'>Trang trước</a></li>";

            for (let pageIndex = startPage; pageIndex <= endPage; pageIndex++) {
                if (pageIndex == _table.page) {
                    html += "   <li" + (pageIndex == _table.page ? " class='current'" : "") + ">" + pageIndex + "</li>";
                }
                else {
                    html += "   <li>";
                    html += "       <a href='javascript:;' onclick='goPage(" + pageIndex + ")'>" + pageIndex + "</a>";
                    html += "   </li>"
                }
            }

            if (_tableType == 'SingleRow' && _table.page < totalPage)
                html += "   <li><a href='javascript:;' onclick='goPage(" + (_table.page + 1) + ")'>Trang sau</a></li>";

            if (endPage < totalPage)
                html += "   <li><a href='javascript:;' onclick='goPage(" + totalPage + ")'>Trang cuối</a></li>";
            html += '</ul>';

            $pagination.show();
            $pagination.html(html);
        }

        function goPage(page) {
            _table.page = page;

            drawBodyTable();
            drawPagination();
        }

        function clickrow(obj) {
            if (!obj.find("td").eq(1).hasClass("checked")) {
                obj.find("td").addClass("checked");
            }
            else {
                obj.find("td").removeClass("checked");
            }
        }

        function check() {
            let checkALL = true;

            $("tbody").find("input[type='checkbox']").each(function (index, element) {
                if (!element.checked) {
                    checkALL = false;
                    return false;
                }
            });

            $("#check-all").prop('checked', checkALL);
        }

        function searchProduct() {
            let stock = +$("#<%=ddlStock.ClientID%>").val() || 1;
            let textsearch = $("#txtSearch").val();
            let categoryID = $("#ddlCategory").val();
            let stockStatus = $("#<%=ddlStockStatus.ClientID%>").val();

            let data = {
                sku: textsearch,
                stock: stock,
                categoryID: categoryID != "" ? +categoryID : null,
                stockStatus: stockStatus != "" ? +stockStatus : null
            };

            $.ajax({
                beforeSend: function () {
                    HoldOn.open();
                },
                type: "POST",
                url: "/tao-phien-kiem-kho.aspx/getProduct",
                data: JSON.stringify(data),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    HoldOn.close();

                    let data = JSON.parse(msg.d) || [];

                    if (data.length == 0) {
                        return swal({
                            title: 'Thông báo',
                            text: 'Không tìm thấy sản phẩm',
                            type: 'error'
                        });
                    }

                    let productNew = [];

                    data.forEach(item => {
                        const olds = _table.data.filter(product => product.ID == item.ID);

                        if (olds.length == 0) {
                            productNew.push(item);
                        }
                    })

                    if (productNew.length > 0) {
                        _table.data = productNew.reverse().concat(_table.data);

                        drawBodyTable();
                        drawPagination();
                    }

                    $("#txtSearch").val("");
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    HoldOn.close();

                    return swal({
                        title: 'Thông báo',
                        text: 'Đã có lỗi trong quá trình tìm sản phẩm',
                        type: 'error'
                    });
                }
            });
        }

        function deleteRow(obj) {
            let $td = obj.parent().parent();
            let data = $td.data();
            let message = '';

            if (data.sku)
                message = 'Bạn muốn xóa sản phẩm #' + data.sku + ' này?'
            else
                message = 'Bạn muốn xóa sản phẩm này?';

            return swal({
                title: 'Thông báo',
                text: message,
                type: 'warning',
                showCancelButton: true,
                closeOnConfirm: true,
                cancelButtonText: "Để em xem lại!!",
                confirmButtonText: "Đúng vậy",
            }, function (isConfirm) {
                if (isConfirm) {
                    _table.data = _table.data.filter(item => item.ID != data.id);
                    let totalPage = Math.ceil(_table.data.length / _table.pageSize);

                    if (totalPage == 0) {
                        _table.page = 1;
                    }
                    else if (_table.page > totalPage) {
                        _table.page = totalPage;
                    }

                    drawBodyTable();
                    drawPagination();
                }
            });
        }

        function deleteAllRow() {
            let $tdChecked = $("tbody").find("input[type='checkbox']:checked").parent().parent();

            if ($tdChecked.length == 0) {
                return swal({
                    title: 'Thông báo',
                    text: 'Vui lòng check những sản phẩm bạn muốn xóa',
                    type: 'warning'
                });
            }
            else {
                return swal({
                    title: 'Thông báo',
                    text: 'Bạn muốn xóa sản phẩm đang checked?',
                    type: 'warning',
                    showCancelButton: true,
                    closeOnConfirm: true,
                    cancelButtonText: "Để em xem lại!!",
                    confirmButtonText: "Đúng vậy",
                }, function (isConfirm) {
                    if (isConfirm) {
                        $tdChecked.each((index, element) => {
                            _table.data = _table.data.filter(item => item.ID != element.dataset.id)
                        });

                        let totalPage = Math.ceil(_table.data.length / _table.pageSize);

                        if (totalPage == 0) {
                            _table.page = 1;
                        }
                        else if (_table.page > totalPage) {
                            _table.page = totalPage;
                        }

                        drawBodyTable();
                        drawPagination();

                        
                    }
                });
            }
        }

        function registerCheckWarehouse() {
            if ($(".product-result").length > 0) {
                HoldOn.open();

                $("#<%=hdfvalue.ClientID%>").val(JSON.stringify(_table.data));
                $("#<%=btnImport.ClientID%>").click();
            }
            else {
                return swal({
                    title: 'Thông báo',
                    text: 'Hiện tại không thấy sản phẩm nào đăng ký',
                    type: 'warning'
                });
            }
        }
    </script>
</asp:Content>
