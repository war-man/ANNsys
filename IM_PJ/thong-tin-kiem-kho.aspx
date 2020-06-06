<%@ Page Title="Xem thông tin kiểm kho" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="thong-tin-kiem-kho.aspx.cs" Inherits="IM_PJ.thong_tin_kiem_kho" EnableSessionState="ReadOnly" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        tr:last-child {
            border-bottom: solid 1px #ccc;
        }

        @media only screen and (max-width: 800px) {
            .col-md-12, .col-xs-12 {
                padding: 0px;
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
                            <h3 class="page-title left not-margin-bot">Nhập kho sản phẩm</h3>
                        </div>
                        <div class="panel-body">
                            <div class="form-row">
                                <div class="col-sm-4 col-xs-9">
                                    <asp:Literal ID="ltrCheckWarehouse" runat="server"></asp:Literal>
                                </div>
                                <div class="col-sm-3 col-xs-3">
                                    <a href="javascript:;" class="btn primary-btn fw-btn not-fullwidth" onclick="getCheckWarehouse()">
                                        <i class="fa fa-search" aria-hidden="true"></i> Tìm
                                    </a>
                                </div>
                            </div>
                            <div class="form-row">
                                <h3 class="no-margin float-left">Kết quả tìm kiếm: <span class="result-numsearch"></span></h3>
                                <div class="excute-in">
                                    <a href="javascript:;" style="background-color: #f87703; float: right;" class="btn primary-btn link-btn" onclick="closeCheckWarehouse()">Kết thúc kiểm tra kho</a>
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
                                                <th class="stock-column">Số lượng củ</th>
                                                <th class="stock-column">Số lượng mới</th>
                                                <th class="trash-column">Ngày kiểm</th>
                                                <th class="trash-column">Người kiểm</th>
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
                                <a href="javascript:;" style="background-color: #f87703; float: right;" class="btn primary-btn link-btn" onclick="closeCheckWarehouse()">Kết thúc kiểm tra kho</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hdfCheckHouseID" runat="server" />
        <asp:Button ID="btnCloseCheckWareHouse" runat="server" OnClick="btnCloseCheckWareHouse_Click" Style="display: none" />
    </main>

    <script type="text/javascript">
        let _isMobile = false;
        let _table = {
            page: 1,
            pageSize: 10,
            data: []
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
        });

        function drawBodyTable() {
            let html = '';
            let index = (_table.page - 1) * _table.pageSize;
            let data = _table.data.slice(index, index + _table.pageSize);

            data.forEach(item => {
                html += "<tr ondblclick=\"clickrow($(this))\" class=\"product-result\" >";
                html += "   <td class='name-column' data-title='Sản phẩm'>";
                html += "       <a href='/xem-san-pham?sku=" + item.SKU + "' target='_blank'>" + item.ProductName + "</a>";
                html += "   </td>";
                html += "   <td data-title='Mã'>" + item.SKU + "</td>";
                html += "   <td data-title='Số lượng củ'>" + item.QuantityOld + "</td>";
                html += "   <td data-title='Số lượng mới'>" + item.QuantityNew + "</td>";
                html += "   <td data-title='Ngày kiểm'>" + item.ModifiedDate + "</td>";
                html += "   <td data-title='Người kiểm'>" + item.ModifiedBy + "</td>";
                html += "</tr>";
            });

            $(".content-product").html(html);

            let $btnExcute = $(".excute-in");

            if ($(".product-result").length > 0) {
                $btnExcute.show();
            }
            else {
                $btnExcute.hide();
            }
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
            let checkWarehouseID = +$("#ddlCheckWarehouse").val() || 0;

            if (!checkWarehouseID)
                return;

            _table.page = 1;
            _table.data = [];

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

        function closeCheckWarehouse() {
            let checkWarehouseID = +$("#ddlCheckWarehouse").val() || 0;

            $("#<%=hdfCheckHouseID.ClientID%>").val(checkWarehouseID);
            $("#<%=btnCloseCheckWareHouse.ClientID%>").click();
        }
    </script>
</asp:Content>
