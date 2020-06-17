<%@ Page Title="Thông tin kiểm kho" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="thong-tin-kiem-kho.aspx.cs" Inherits="IM_PJ.thong_tin_kiem_kho" EnableSessionState="ReadOnly" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .none-padding-right {
            padding-right: 0
        }

        .btn-edit {
            float: right;
        }

        .btn-finish {
            background-color: #f87703; 
            float: right;
            margin-left: 15px;
        }

        .btn-update {
            background-color: #f87703; 
            float: right;
        }

        tr:last-child {
            border-bottom: solid 1px #ccc;
        }

        @media only screen and (max-width: 800px) {
            .btn-update {
                background-color: #f87703; 
                float: left;
            }

            .col-sm-3, .col-sm-4, .col-md-12, .col-xs-3, .col-xs-4, .col-xs-12 {
                padding-left: 0px;
                padding-right: 0px;
            }

            .padding-left-15 {
                padding-left: 15px;
            }

            .padding-top-15 {
                padding-top: 15px;
            }

            .padding-bottom-15 {
                padding-bottom: 15px;
            }

            /* Force table to not be like tables anymore */
            #no-more-tables table,
            #no-more-tables thead,
            #no-more-tables tbody,
            #no-more-tables th,
            #no-more-tables td,
            #no-more-tables tr {
                display: block;
            }

                /* Hide table headers (but not display: none;, for accessibility) */
                #no-more-tables thead tr {
                    position: absolute;
                    top: -9999px;
                    left: -9999px;
                }

            #no-more-tables tr {
                border: 1px solid #ccc;
            }

            #no-more-tables td {
                /* Behave like a "row" */
                border: none;
                border-bottom: 1px solid #eee;
                position: relative;
                padding-left: 35%;
                white-space: normal;
                text-align: left;
                width: 100%;
                padding-top: 10px;
                padding-bottom: inherit;
            }

                #no-more-tables td:before {
                    /* Now like a table header */
                    position: absolute;
                    /* Top/left values mimic padding */
                    top: 10px;
                    left: 6px;
                    width: 45%;
                    padding-right: 10px;
                    white-space: nowrap;
                    text-align: left;
                    font-weight: bold;
                }

                /*
                Label the data
                */
                #no-more-tables td:before {
                    content: attr(data-title);
                }

                #no-more-tables td[class='name-column'] {
                    height: auto;
                }
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
                            <h3 class="page-title left not-margin-bot">Thông tin kiểm kho</h3>
                        </div>
                        <div class="panel-body">
                            <div class="form-row">
                                <div class="col-sm-4 col-xs-12">
                                    <asp:DropDownList ID="ddlCheckWarehouse" runat="server" CssClass="form-control select2" Width="100%"></asp:DropDownList>
                                </div>
                                <div class="col-sm-8 col-xs-12 none-padding-right padding-top-15" style="display: none">
                                    <a href="javascript:;" class="btn primary-btn fw-btn not-fullwidth btn-edit" onclick="goEditCheckWarehouse()">
                                        <i class="fa fa-edit" aria-hidden="true"></i> Chỉnh sửa phiên kiểm kho
                                    </a>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="excute-in">
                                    <a href="javascript:;" class="btn primary-btn link-btn btn-finish" onclick="closeCheckWarehouse()">Kết thúc phiên kiểm kho</a>
                                    <a href="javascript:;" class="btn primary-btn link-btn btn-update" onclick="updateQuantity()">Cập nhật SL</a>
                                </div>
                            </div>
                            <div class="form-row" style="display: none">
                                <div class="col-sm-4 col-xs-12 padding-bottom-15">
                                    <asp:TextBox runat="server" ID="txtSearch" CssClass="form-control" placeholder="NHẬP MÃ SẢN PHẨM (F3)" autocomplete="off" />
                                </div>
                                <div class="col-sm-2 col-xs-12 padding-bottom-15">
                                    <asp:DropDownList ID="ddlProductStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0" Text="Trạng thái sản phẩm"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Đã kiểm"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Chưa kiểm"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-4 col-xs-3">
                                    <a href="javascript:;" class="btn primary-btn fw-btn not-fullwidth" onclick="searchProduct()">
                                        <i class="fa fa-search" aria-hidden="true"></i> Tìm
                                    </a>
                                </div>
                            </div>
                            <div class="form-row hidden">
                                <div class="pagination"></div>
                            </div>
                            <div class="form-row">
                                <div id="no-more-tables">
                                    <table class="col-xs-12 table table-checkable table-product import-stock">
                                        <thead class="cf">
                                            <tr>
                                                <th class="name-column">Sản phẩm</th>
                                                <th class="sku-column">Mã</th>
                                                <th class="stock-column">SL cũ</th>
                                                <th class="stock-column">SL mới</th>
                                                <th class="trash-column">Thời gian</th>
                                            </tr>
                                        </thead>
                                        <tbody class="content-product">
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <div class="form-row hidden">
                                <div class="pagination"></div>
                            </div>
                            <div class="post-table-links excute-in clear">
                                <a href="javascript:;" class="btn primary-btn link-btn btn-finish" onclick="closeCheckWarehouse()">Kết thúc phiên kiểm kho</a>
                                <a href="javascript:;" class="btn primary-btn link-btn btn-update" onclick="updateQuantity()">Cập nhật SL</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hdfRoleID" runat="server" />
        <asp:HiddenField ID="hdfCheckHouseID" runat="server" />
        <asp:Button ID="btnUpdateQuantity" runat="server" OnClick="btnUpdateQuantity_Click" Style="display: none" />
        <asp:Button ID="btnCloseCheckWareHouse" runat="server" OnClick="btnCloseCheckWareHouse_Click" Style="display: none" />
    </main>

    <script type="text/javascript">
        let _isMobile = false;
        let _table = {
            page: 1,
            pageSize: 30,
            data: [],
            dataSearch: []
        };

        $(window).resize(function () {
            if ($(window).innerWidth() < 800) {
                if (!_isMobile) {
                    _isMobile = true;
                    drawPagination();
                }
            }
            else {
                if (_isMobile) {
                    _isMobile = false;
                    drawPagination();
                }
            }
        });

        $(document).ready(function () {
            if ($(window).innerWidth() < 800) {
                _isMobile = true;
                $('.nav-toggle').removeClass('open');
                $('body').removeClass('menuin');
            }
            else {
                _isMobile = false;
            }

            initCheckWarehouseSelect2();
            initSearch();
        });

        function initCheckWarehouseSelect2() {
            let $ddlCheckWarehouse = $('#<%= ddlCheckWarehouse.ClientID %>');
            let checkID = +$ddlCheckWarehouse.val() || 0;

            // Trường hợp có query param checkID > 0
            if (checkID > 0) {
                updateURL(checkID);
                getCheckWarehouse();
            }

            // Trường hợp phiên kiểm có thay đổi
            $ddlCheckWarehouse.change(function () {
                let queryParams = new URLSearchParams(window.location.search);
                checkID = +$(this).val() || 0;

                updateURL(checkID);
                getCheckWarehouse();
            });
        }

        function initSearch() {
            let $txtSearch = $("#<%= txtSearch.ClientID %>");

            $txtSearch.keydown(function (event) {
                if (event.which === 13) {
                    event.preventDefault();
                    searchProduct();

                    return false;
                }
            });
        }

        function drawBodyTable() {
            let html = '';
            let index = (_table.page - 1) * _table.pageSize;
            let data = _table.data.slice(index, index + _table.pageSize);

            data.forEach(item => {
                html += "<tr ondblclick='clickrow($(this))' class='product-result' >";
                html += "   <td class='name-column' data-title='Sản phẩm'>";
                html += "       <a href='/xem-san-pham?sku=" + item.SKU + "' target='_blank'>" + item.ProductName + "</a>";
                html += "   </td>";
                html += "   <td data-title='Mã'>" + item.SKU + "</td>";
                html += "   <td data-title='SL cũ'>" + item.QuantityOld + "</td>";
                html += "   <td data-title='SL mới'>" + item.QuantityNew + "</td>";
                html += "   <td data-title='Thời gian'>" + item.ModifiedDate + "</td>";
                html += "</tr>";
            });

            $(".content-product").html(html);
        }

        function drawPagination() {
            let $pagination = $(".pagination");
            let totalPage = Math.ceil(_table.data.length / _table.pageSize);

            if (totalPage == 1) {
                $pagination.parent().addClass('hidden');
                return;
            }

            let html = '';
            let pageMax = !_isMobile ? 6 : 4;
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

            if (!_isMobile && _table.page > 1)
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

            if (!_isMobile && _table.page < totalPage)
                html += "   <li><a href='javascript:;' onclick='goPage(" + (_table.page + 1) + ")'>Trang sau</a></li>";

            if (endPage < totalPage)
                html += "   <li><a href='javascript:;' onclick='goPage(" + totalPage + ")'>Trang cuối</a></li>";
            html += '</ul>';

            $pagination.parent().removeClass('hidden');
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

        function getCheckWarehouse() {
            let roleID = $('#<%= hdfRoleID.ClientID %>').val() || "";
            let $ddlCheckWarehouse = $('#<%= ddlCheckWarehouse.ClientID %>');
            let checkWarehouseID = +$ddlCheckWarehouse.val() || 0;
            let $btnEdit = $(".btn-edit");
            let $btnExcute = $(".excute-in");
            let $btnUpdate = $(".btn-update");
            let $txtSearch = $("#<%= txtSearch.ClientID %>");
            let $ddlProductStatus = $("#<%= ddlProductStatus.ClientID %>");

            $btnEdit.parent().css('display', 'none');
            $txtSearch.val("");
            $txtSearch.parent().parent().hide();
            _table.page = 1;
            _table.data = [];
            _table.dataSearch = [];

            if (!checkWarehouseID) {
                $btnExcute.hide();
                drawBodyTable();
                drawPagination();

                return;
            }

            if (roleID == "0" && !$ddlCheckWarehouse.find(":selected").data('finished')) {
                $btnEdit.parent().css('display', 'block');
                $btnExcute.show();
            }
            else {
                $btnEdit.parent().css('display', 'none');
                $btnExcute.hide();
            }

            $.ajax({
                beforeSend: function () {
                    HoldOn.open();
                },
                type: "POST",
                url: "/thong-tin-kiem-kho.aspx/getCheckWarehouse",
                data: JSON.stringify({ id: checkWarehouseID }),
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

                    let productNotCheck = data.filter(product => product.QuantityNew == "");

                    if (productNotCheck.length > 0)
                        $btnUpdate.show();
                    else
                        $btnUpdate.hide();

                    $txtSearch.parent().parent().show();
                    $txtSearch.val("");
                    $ddlProductStatus.val(0);

                    _table.data = data;
                    _table.dataSearch = data;

                    drawBodyTable();
                    drawPagination();
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

        function closeCheckWarehouse() {
            HoldOn.open();
            let checkWarehouseID = +$('#<%= ddlCheckWarehouse.ClientID %>').val() || 0;

            $("#<%=hdfCheckHouseID.ClientID%>").val(checkWarehouseID);
            $("#<%=btnCloseCheckWareHouse.ClientID%>").click();
        }

        function searchProduct() {
            let $btnUpdate = $(".btn-update");
            let $txtSearch = $("#<%= txtSearch.ClientID %>");
            let $ddlProductStatus = $("#<%= ddlProductStatus.ClientID %>");
            let search = $txtSearch.val() || "";
            let productStatus = +$ddlProductStatus.val() || 0;
            let data = _table.dataSearch;

            if (search) {
                search = search.toUpperCase().trim();
                data = data.filter(product => product.SKU.startsWith(search));
            }

            if (productStatus == 1) {
                $btnUpdate.hide();
                data = data.filter(product => product.QuantityNew != "");
            }
            else if (productStatus == 2) {
                data = data.filter(product => product.QuantityNew == "");

                if (data.length > 0)
                    $btnUpdate.show();
                else
                    $btnUpdate.hide();
            }
            else {
                let productNotCheck = data.filter(product => product.QuantityNew == "");

                if (productNotCheck.length > 0)
                    $btnUpdate.show();
                else
                    $btnUpdate.hide();
            }

            $txtSearch.val("");
            $ddlProductStatus.val(0);
            _table.data = data;
            drawBodyTable();
            drawPagination();
        }

        function updateURL(checkID) {
            if (checkID == 0) {
                history.pushState(null, null, "?");
            }
            else {
                let queryParams = new URLSearchParams(window.location.search);

                queryParams.set('checkID', checkID);
                history.pushState(null, null, "?" + queryParams.toString());
            }
        }

        function updateQuantity() {
            HoldOn.open();
            let checkWarehouseID = +$('#<%= ddlCheckWarehouse.ClientID %>').val() || 0;

            $("#<%=hdfCheckHouseID.ClientID%>").val(checkWarehouseID);
            $("#<%=btnUpdateQuantity.ClientID%>").click();
        }

        function goEditCheckWarehouse() {
            let checkID = +$('#<%= ddlCheckWarehouse.ClientID %>').val() || 0;

            if (checkID)
                window.open("/sua-phien-kiem-kho?checkID=" + checkID.toString());
        }
    </script>
</asp:Content>
