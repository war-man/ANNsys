<%@ Page Title="Danh sách nhập hàng" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="danh-sach-nhap-hang.aspx.cs" Inherits="IM_PJ.danh_sach_nhap_hang" EnableSessionState="ReadOnly" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/moment.min.js"></script>
    <script src="/Scripts/moment-with-locales.min.js"></script>
    <script src="/Scripts/bootstrap-datetimepicker.min.js"></script>
    <style>
        .table-new-product .img-product {
            width: 7%;
        }
        .table-new-product .customer-td span.name {
            font-weight: bold;
            font-size: 16px;
        }
        #invoice-image li {
            list-style: none;
        }
        #invoice-image img {
            width: 60%;
        }
        .select2-container .select2-selection--single {
            height: 45px;
        }
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            line-height: 45px;
        }
        .select2-container--default .select2-selection--single .select2-selection__arrow {
            height: 43px;
        }
        .btn.remove-btn {
            background-color: #FF675B;
            color: #fff;
        }
        table.shop_table_responsive > tbody > tr:nth-of-type(2n) td {
            border-top: solid 1px #e1e1e1!important;
        }
        @media (max-width: 768px) {
            table.shop_table_responsive thead {
                display: none;
            }
            table.shop_table_responsive > tbody > tr > td:nth-of-type(1):before {
                content: none;
            }
            table.shop_table_responsive > tbody > tr > td:nth-of-type(1) {
                text-align: left;
            }
            table.shop_table_responsive > tbody > tr > td:nth-of-type(2):before {
                content: "#";
                font-size: 20px;
                margin-right: 2px;
            }
            table.shop_table_responsive > tbody > tr > td:nth-of-type(2) {
                text-align: left;
                font-size: 20px;
                font-weight: bold;
                height: 50px;
            }
            table.shop_table_responsive > tbody > tr:nth-of-type(2n) td {
                border-top: none;
                border-bottom: none!important;
            }
            table.shop_table_responsive > tbody > tr > td:first-child {
                border-left: none;
                padding-left: 20px;
            }
            table.shop_table_responsive > tbody > tr > td:last-child {
                border-right: none;
                padding-left: 20px;
            }
            table.shop_table_responsive > tbody > tr > td {
                height: 40px;
            }
            table.shop_table_responsive > tbody > tr > td.customer-td {
                height: 60px;
            }
            table.shop_table_responsive > tbody > tr > td.update-button {
                height: 85px;
            }
            table.shop_table_responsive .bg-bronze,
            table.shop_table_responsive .bg-red,
            table.shop_table_responsive .bg-blue,
            table.shop_table_responsive .bg-yellow,
            table.shop_table_responsive .bg-black,
            table.shop_table_responsive .bg-green {
                display: initial;
                float: right;
            }
            table.shop_table_responsive tbody td {
                background-color: #f8f8f8;
                display: block;
                text-align: right;
                border: none;
                padding: 20px;
            }
            table.shop_table_responsive > tbody > tr.tr-more-info > td {
                height: initial;
            }
            table.shop_table_responsive > tbody > tr.tr-more-info > td span {
                display: block;
                text-align: left;
                margin-bottom: 10px;
                margin-right: 0;
            }
            table.shop_table_responsive > tbody > tr.tr-more-info > td:nth-child(2):before {
                content: none;
            }
            table.shop_table_responsive tbody td:before {
                content: attr(data-title) ": ";
                font-weight: 700;
                float: left;
                text-transform: uppercase;
                font-size: 14px;
            }
            table.shop_table_responsive tbody td:empty {
                display: none;
            }
            #invoice-image img {
                width: 40%;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <h3 class="page-title left">Nhân viên đặt hàng  <span>(<asp:Literal ID="ltrNumberOfOrder" runat="server" EnableViewState="false"></asp:Literal>)
                        </span>
                    </h3>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="filter-above-wrap clear">
                        <div class="filter-control">
                            <div class="row">
                                <div class="col-md-3 col-xs-6">
                                    <asp:TextBox ID="txtSearchOrder" runat="server" CssClass="form-control" placeholder="Tìm đơn hàng" autocomplete="off"></asp:TextBox>
                                </div>
                                <div class="col-md-2 col-xs-6">
                                    <asp:DropDownList ID="ddlRegisterStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0" Text="Trạng thái nhập hàng"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Chưa duyệt"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Đã duyệt"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Đã đặt hàng"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="Hàng về"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-xs-6">
                                    <asp:DropDownList ID="ddlCreatedBy" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-xs-6">
                                    <label>Từ ngày</label>
                                    <telerik:RadDatePicker RenderMode="Lightweight" ID="rFromDate" ShowPopupOnFocus="true" Width="100%" runat="server" DateInput-CssClass="radPreventDecorate">
                                        <DateInput DisplayDateFormat="dd/MM/yyyy" runat="server">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </div>
                                <div class="col-md-2 col-xs-6">
                                    <label>Đến ngày</label>
                                    <telerik:RadDatePicker RenderMode="Lightweight" ID="rToDate" ShowPopupOnFocus="true" Width="100%" runat="server" DateInput-CssClass="radPreventDecorate">
                                        <DateInput DisplayDateFormat="dd/MM/yyyy" runat="server">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </div>
                                <div class="col-md-1 col-xs-6 search-button">
                                    <a href="javascript:;" onclick="searchOrder()" class="btn primary-btn h45-btn"><i class="fa fa-search"></i></a>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn primary-btn h45-btn" OnClick="btnSearch_Click" Style="display: none" />
                                    <a href="/danh-sach-van-chuyen" class="btn primary-btn h45-btn"><i class="fa fa-times" aria-hidden="true"></i></a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="filter-above-wrap clear">
                        <div class="filter-control">
                            <div class="row">
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-2 col-xs-6">
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
                                <div class="col-md-2 col-xs-6">
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
                    </div>
                    <div class="filter-above-wrap clear">
                        <div class="filter-control">
                            <div class="row">
                                <div class="col-md-2 col-xs-4">
                                    <a id="filterChoosed" href="javascript:;" class="btn primary-btn fw-btn width-100" onclick="getRegisterProductSession()">
                                        <i class="fa fa-inbox" aria-hidden="true"></i> Đã chọn
                                    </a>
                                </div>
                                <div class="col-md-2 col-xs-4">
                                    <a href="javascript:;" class="btn primary-btn fw-btn width-100" onclick="removeChoosed()">
                                        <i class="fa fa-remove" aria-hidden="true"></i> Bỏ chọn
                                    </a>
                                </div>
                                <div class="col-md-2 col-xs-4">
                                    <a id="approveChoosed" href="javascript:;" class="btn primary-btn fw-btn width-100" onclick="approveChoosed()">
                                        <i class="fa fa-check" aria-hidden="true"></i> Duyệt
                                    </a>
                                </div>
                                <div class="col-md-2 col-xs-4">
                                    <a id="orderingChoosed" href="javascript:;" class="btn primary-btn fw-btn width-100" onclick="orderingChoosed()">
                                        <i class="fa fa-cubes" aria-hidden="true"></i> Đặt hàng
                                    </a>
                                </div>
                                <div class="col-md-2 col-xs-4">
                                    <a id="doneChoosed" href="javascript:;" class="btn primary-btn fw-btn width-100" onclick="doneChoosed()">
                                        <i class="fa fa-truck" aria-hidden="true"></i> Hàng đã về
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel-table clear">
                        <div class="panel-footer clear">
                            <div class="pagination">
                                <%this.DisplayHtmlStringPaging1();%>
                            </div>
                        </div>
                        <div class="responsive-table">
                            <table class="table shop_table_responsive table-checkable table-product table-new-product">
                                <asp:Literal ID="ltrList" runat="server" EnableViewState="false"></asp:Literal>
                            </table>
                        </div>
                        <div class="panel-footer clear">
                            <div class="pagination">
                                <%this.DisplayHtmlStringPaging1();%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Modal cập nhật giao hàng-->
        <div class="modal fade" id="RegisterProductModal" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Cập nhật yêu cầu nhập hàng</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row form-group">
                            <div class="col-md-3 col-xs-4">
                                <p>Khách hàng</p>
                            </div>
                            <div class="col-md-9 col-xs-8">
                                <asp:TextBox ID="txtCustomer" runat="server" CssClass="form-control" placeholder="Tên khách hàng"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-md-3 col-xs-4">
                                <p>Trạng thái</p>
                            </div>
                            <div class="col-md-9 col-xs-8">
                                <asp:DropDownList ID="ddlStatusModal" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="1" Text="Chưa duyệt"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Đã duyệt"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Đã đặt hàng"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="Hàng về"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-md-3 col-xs-4">
                                <p>Số lượng đặt</p>
                            </div>
                            <div class="col-md-9 col-xs-8">
                                <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control text-right" placeholder="Số lượng đặt hàng" data-type="currency" onkeypress='return event.charCode >= 48 && event.charCode <= 57'></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-md-3 col-xs-4">
                                <p>Ngày về dự kiến</p>
                            </div>
                            <div class="col-md-9 col-xs-8">
                                <div class="input-group date" id="dtExpectedDate">
                                    <input data-format="yyyy-MM-dd hh:mm:ss" type="text" class="form-control" />
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 col-xs-4">
                                <p>Ghi chú</p>
                            </div>
                            <div class="col-md-9 col-xs-8">
                                <asp:TextBox ID="txtNote" runat="server" CssClass="form-control text-left" placeholder="Ghi chú"></asp:TextBox>
                            </div>
                        </div>
                        <asp:HiddenField ID="hdRegisterProductID" runat="server" />
                    </div>
                    <div class="modal-footer">
                        <button id="closeRegisterProduct" type="button" class="btn btn-default" data-dismiss="modal">Đóng</button>
                        <button id="updateRegisterProduct" type="button" class="btn btn-primary">Lưu</button>
                    </div>
                </div>
            </div>
        </div>

        <asp:HiddenField ID="hdfSession" runat="server" />
        <script type="text/javascript">
            class RegisterProduct {
                constructor(
                    id
                    , customer
                    , status
                    , quantity
                    , expectedDate
                    , note
                ) {
                    this.id = id;
                    this.customer = customer;
                    this.status = status;
                    this.quantity = quantity;
                    this.expectedDate = expectedDate;
                    this.note = note;
                }
            }

            // Danh sách các yêu cầu nhập hàng
            var registers = [];

            function init()
            {
                // Lấy danh sách order đã chọn
                let data = JSON.parse($("#<%=hdfSession.ClientID%>").val());

                if ($.isArray(data)) {
                    data.forEach((item) => {
                        // Thêm vào mảng order đã chọn
                        registers.push(new RegisterProduct(
                            +item.id || 0,
                            item.customer,
                            +item.status || 1,
                            +item.quantity || 0,
                            item.expectedDate,
                            item.note
                            ));

                        let checkbox = $("tbody > tr[data-registerid='" + (+item.id || 0) + "'] > td > input[type='checkbox']");
                        checkbox.prop("checked", true);
                    });

                    // Thể hiện số lượng đơn hàng sẽ In
                    showNumberChoose();
                }

                testCheckAll();
            }

            $(document).ready(() => {
                init();
            });

            // Sự kiện cho button search
            $("#<%=txtSearchOrder.ClientID%>").keyup(function (e) {
                if (e.keyCode == 13)
                {
                    $("#<%= btnSearch.ClientID%>").click();
                }
            });

            function searchOrder() {
                $("#<%= btnSearch.ClientID%>").click();
            }

            // Xử lý sự kiện trong modal
            $("button[data-toggle='modal']").click(e => {
                let row = e.currentTarget.parentNode.parentNode;
                let modal = $("#RegisterProductModal");
                let registerDOM = modal.find("#<%=hdRegisterProductID.ClientID%>");
                let customerDOM = modal.find("#<%=txtCustomer.ClientID%>");
                let statusDOM = modal.find("#<%=ddlStatusModal.ClientID%>");
                let quantityDOM = modal.find("#<%=txtQuantity.ClientID%>");
                let pickerDOM = modal.find('#dtExpectedDate');
                let noteDOM = modal.find("#<%=txtNote.ClientID%>");

                // Init modal
                pickerDOM.datetimepicker({
                    format: 'DD/MM/YYYY HH:mm',
                    date: new Date()
                });

                // init giá trị cho modal
                registerDOM.val(row.dataset["registerid"]);
                customerDOM.val(row.dataset["customer"]);
                statusDOM.val(row.dataset["status"]);
                quantityDOM.val(row.dataset["quantity"]);
                if (row.dataset["expecteddate"])
                    pickerDOM.data("DateTimePicker").date(row.dataset["expecteddate"]);
                else
                    pickerDOM.data("DateTimePicker").date(moment(new Date).format('DD/MM/YYYY HH:mm'));
                noteDOM.val(row.dataset["note"]);
            });

            $("#updateRegisterProduct").click(e => {
                let registerID = $("#<%=hdRegisterProductID.ClientID%>").val();
                let customer = $("#<%=txtCustomer.ClientID%>").val();
                let status = +$("#<%=ddlStatusModal.ClientID%>").val() || 0;
                let quantity = +$("#<%=txtQuantity.ClientID%>").val().replace(",", "") || 0;
                let expectedDate = $('#dtExpectedDate');
                let note = $("#<%=txtNote.ClientID%>").val();

                // Check xem có chọn ngày không
                if (expectedDate.data("date") != "")
                    expectedDate = expectedDate.data('DateTimePicker').date().format('YYYY/MM/DD HH:mm:ss');
                else
                    expectedDate = null;

                let item = {
                    'id': registerID,
                    'customer': customer,
                    'status': status,
                    'quantity': quantity,
                    'expectedDate': expectedDate,
                    'note': note
                }

                $.ajax({
                    url: "/danh-sach-nhap-hang.aspx/updateRegisterProduct",
                    type: "POST",
                    data: JSON.stringify({ 'data': [item] }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: (response) => {
                        let row = $("tr[data-registerid='" + registerID + "']");

                        // update screen
                        row.attr("data-customer", customer);
                        row.attr("data-status", status);
                        if (quantity)
                            row.attr("data-quantity", formatThousands(quantity));
                        else
                            row.attr("data-quantity", "");
                        if (expectedDate)
                            row.attr("data-expecteddate", moment(expectedDate).format('DD/MM/YYYY h:mm'));
                        else
                            row.attr("data-expecteddate", "");

                        row.attr("data-note", note);

                        let statusCol = row.find("#status");
                        switch (status) {
                            case 2:
                                statusCol.html("<span class='bg-green'>Đã duyệt</span>");
                                break;
                            case 3:
                                statusCol.html("<span class='bg-yellow'>Đã đặt hàng</span>");
                                break;
                            case 4:
                                statusCol.html("<span class='bg-blue'>Hàng về</span>");
                                break;
                            default:
                                statusCol.html("<span class='bg-red'>Chưa duyệt</span>");
                                break;
                        }

                        let expectedDateCol = row.find("#expectedDate");
                        if (expectedDate)
                            expectedDateCol.html(moment(expectedDate).format('DD/MM'));
                        else
                            expectedDateCol.html("");

                        $("#closeRegisterProduct").click();
                    },
                    error: function (err) {
                        swal("Thông báo", "Đã có vấn đề trong việc cập nhật yêu cầu nhập hàng", "error");
                    }
                });
            });

            function checkRegister(self)
            {
                let parent = self.parent().parent();
                let registerID = parent.data("registerid");
                let customer = parent.data("customer");
                let status = parent.data("status");
                let quantity = +parent.data("quantity") || 0;
                let expectedDate = parent.data("expecteddate");
                let note = parent.data("note");

                if (self.is(":checked")) {
                    if (expectedDate)
                        expectedDate = moment(expectedDate).format('DD/MM/YYYY h:mm:ss');

                    let item = new RegisterProduct(
                        registerID,
                        customer,
                        status,
                        quantity,
                        expectedDate,
                        note
                        );

                    addChoose([item]);
                }
                else {
                    let item = registers.filter((item) => { return item.id == registerID; });
                    deleteChoose(item);
                }

                testCheckAll();
            }

            $("#checkRegisterAll").change(e => {
                let checked = e.target.checked;

                if (checked) {
                    let checkbox = $("tbody > tr > td > input[type='checkbox']").not(":checked");
                    let data = [];

                    checkbox.each((index, element) => {
                        let parent = element.parentElement.parentElement;
                        let registerID = parent.dataset["registerid"];
                        let customer = parent.dataset["customer"];
                        let status = parent.dataset["status"];
                        let quantity = +parent.dataset["quantity"] || 0;
                        let expectedDate = parent.dataset["expecteddate"];
                        let note = parent.dataset["note"];

                        element.checked = checked;
                        data.push(new RegisterProduct(
                            registerID,
                            customer,
                            status,
                            quantity,
                            expectedDate,
                            note
                        ));
                    });

                    addChoose(data);
                }
                else {
                    let checkbox = $("tbody > tr > td > input[type='checkbox']:checked");
                    let data = [];

                    checkbox.each((index, element) => {
                        let parent = element.parentElement.parentElement;
                        let registerID = parent.dataset["registerid"];
                        let customer = parent.dataset["customer"];
                        let status = parent.dataset["status"];
                        let quantity = +parent.dataset["quantity"] || 0;
                        let expectedDate = parent.dataset["expecteddate"];
                        let note = parent.dataset["note"];

                        element.checked = checked;
                        data.push(new RegisterProduct(
                            registerID,
                            customer,
                            status,
                            quantity,
                            expectedDate,
                            note
                        ));
                    });

                    deleteChoose(data);
                }
            });

            function testCheckAll()
            {
                // Kiểm tra trạng thái check all
                let checkRegisterAll = $("#checkRegisterAll");
                let checkbox = $("tbody > tr > td > input[type='checkbox']");

                if (checkbox.length == 0) {
                    checkRegisterAll.prop('checked', false);
                }
                else {
                    checkbox.each((index, element) => {
                        checkRegisterAll.prop('checked', element.checked);
                        if (!element.checked) return false;
                    });
                }
            }

            function addChoose(data)
            {
                $.ajax({
                    type: "POST",
                    url: "/danh-sach-nhap-hang.aspx/addChoose",
                    data: JSON.stringify({ 'data': data }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: (response) => {
                        let data = JSON.parse(response.d);
                        if (data)
                            registers = data;

                        // Thể hiện số lượng đơn hàng sẽ In
                        showNumberChoose();
                    },
                    error: (xmlhttprequest, textstatus, errorthrow) => {
                        swal("Thông báo", "Có lỗi trong quá trình chọn đơn yêu cầu nhập hàng", "error");
                    }
                })
            }

            function updateChoose(data) {
                $.ajax({
                    type: "POST",
                    url: "/danh-sach-nhap-hang.aspx/updateRegisterProduct",
                    data: JSON.stringify({ 'data': data }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: (response) => {
                        data.forEach(item => {
                            let row = $("tr[data-registerid='" + item.id + "']");

                            // update screen
                            row.attr("data-customer", item.customer);
                            row.attr("data-status", item.status);
                            if (item.quantity)
                                row.attr("data-quantity", formatThousands(item.quantity));
                            else
                                row.attr("data-quantity", "");
                            if (item.expectedDate)
                                row.attr("data-expecteddate", moment(item.expectedDate).format('DD/MM/YYYY h:mm'));
                            else
                                row.attr("data-expecteddate", "");
                            row.attr("data-note", item.note);

                            let statusCol = row.find("#status");
                            switch (item.status) {
                                case 2:
                                    statusCol.html("<span class='bg-green'>Đã duyệt</span>");
                                    break;
                                case 3:
                                    statusCol.html("<span class='bg-yellow'>Đã đặt hàng</span>");
                                    break;
                                case 4:
                                    statusCol.html("<span class='bg-blue'>Hàng về</span>");
                                    break;
                                default:
                                    statusCol.html("<span class='bg-red'>Chưa duyệt</span>");
                                    break;
                            }

                            let expectedDateCol = row.find("#expectedDate");
                            if (item.expectedDate)
                                expectedDateCol.html(moment(item.expectedDate).format('DD/MM'));
                            else
                                expectedDateCol.html("");
                        });
                    },
                    error: (xmlhttprequest, textstatus, errorthrow) => {
                        swal("Thông báo", "Có lỗi trong quá trình cập nhật yêu cầu đặt hàng", "error");
                    }
                })
            }

            function deleteChoose(data)
            {
                $.ajax({
                    type: "POST",
                    url: "/danh-sach-nhap-hang.aspx/deleteChoose",
                    data: JSON.stringify({ 'data': data }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: (response) => {
                        let data = JSON.parse(response.d);
                        if (data)
                            registers = data;
                        else
                            registers = [];

                        // Thể hiện số lượng đơn hàng sẽ In
                        showNumberChoose();
                    },
                    error: (xmlhttprequest, textstatus, errorthrow) => {
                        swal("Thông báo", "Có lỗi trong quá trình bỏ chọn đơn yêu cầu nhập hàng", "error");
                    }
                });
            }

            function getRegisterProductSession() {
                if (registers.length == 0) {
                    swal("Thông báo", "Chưa có chọn yêu cầu nhập hàng nào hết!", "error");
                }
                else {
                    let url = window.location.href;
                    let reg = /\?/g;

                    url = url.replace(/(&?Page=\d+)/g, "");
                    if (url.search(/isregisterproductsession=1/g) > 0)
                        return;
                    if (url.search(reg) > 0)
                        url = url + "&isregisterproductsession=1";
                    else
                        url = url + "?isregisterproductsession=1"

                    let win = window.open(url, '_self');
                    if (win) {
                        //Browser has allowed it to be opened
                        win.focus();
                    } else {
                        //Browser has blocked it
                        swal("Thông báo", "Vui lòng cho phép cửa sổ bật lên cho trang web này", "error");
                    }
                }
            }

            function removeChoosed() {
                deleteChoose(registers);
                let url = window.location.origin + window.location.pathname;
                window.location.replace(url);
            }

            function approveChoosed() {
                registers.forEach(item => item.status = 2);
                updateChoose(registers)
            }

            function orderingChoosed() {
                registers.forEach(item => item.status = 3);
                updateChoose(registers)
            }

            function doneChoosed() {
                registers.forEach(item => item.status = 4);
                updateChoose(registers)
            }

            function showNumberChoose() {
                let numberPrint = registers.length;
                let text = "";

                // text thể hiện
                if (numberPrint > 0) {
                    text = "(" + formatNumber(numberPrint.toString()) + ")";
                }

                $("#filterChoosed").html(
                        "<i class='fa fa-inbox' aria-hidden='true'></i> Đã chọn " + text
                    );
            }

            function removeRegister(registerID) {
                swal({
                    title: "Hủy",
                    text: "Bạn muốn hủy yêu cầu nhập hàng?",
                    showCancelButton: true,
                    closeOnConfirm: true,
                    cancelButtonText: "Không",
                    confirmButtonText: "Đúng",
                }, function (confirm) {
                    if (confirm) {
                        // Truyền dữ liệu xuống server
                        $.ajax({
                            type: "POST",
                            url: "/nhan-vien-dat-hang.aspx/removeRegister",
                            data: JSON.stringify({ 'registerID': registerID }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: (response) => {
                                let strHTML = "<span class='bg-red' style='text-align: center'>Đã hủy</span>"
                                $("tr[data-registerid='" + registerID + "'").find(".update-button").html(strHTML);
                            },
                            error: (xmlhttprequest, textstatus, errorthrow) => {
                                alert("Có lỗi trong quá trình đang ký nhập hàng");
                            }
                        })
                    }
                });

            }

            // Xử lý format số lượng
            var formatThousands = function (n, dp) {
                var s = '' + (Math.floor(n)), d = n % 1, i = s.length, r = '';
                while ((i -= 3) > 0) { r = ',' + s.substr(i, 3) + r; }
                return s.substr(0, i + 3) + r +
                    (d ? '.' + Math.round(d * Math.pow(10, dp || 2)) : '');
            };

            // Jquery Dependency
            $("input[data-type='currency']").on({
                keyup: function () {
                    formatCurrency($(this));
                },
                blur: function () {
                    formatCurrency($(this), "blur");
                }
            });

            function formatNumber(n) {
                // format number 1000000 to 1,234,567
                return n.replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",")
            }

            function formatCurrency(input, blur) {
                // appends $ to value, validates decimal side
                // and puts cursor back in right position.

                // get input value
                var input_val = input.val();

                // don't validate empty input
                if (input_val === "") { return; }

                // original length
                var original_len = input_val.length;

                // initial caret position 
                var caret_pos = input.prop("selectionStart");

                // check for decimal
                if (input_val.indexOf(".") >= 0) {

                    // get position of first decimal
                    // this prevents multiple decimals from
                    // being entered
                    var decimal_pos = input_val.indexOf(".");

                    // split number by decimal point
                    var left_side = input_val.substring(0, decimal_pos);
                    var right_side = input_val.substring(decimal_pos);

                    // add commas to left side of number
                    left_side = formatNumber(left_side);

                    // validate right side
                    right_side = formatNumber(right_side);

                    // On blur make sure 2 numbers after decimal
                    if (blur === "blur") {
                        right_side += "00";
                    }

                    // Limit decimal to only 2 digits
                    right_side = right_side.substring(0, 2);

                    // join number by .
                    input_val = left_side;

                } else {
                    // no decimal entered
                    // add commas to number
                    // remove all non-digits
                    input_val = formatNumber(input_val);
                    input_val = input_val;
                }

                // send updated string to input
                input.val(input_val);

                // put caret back in the right position
                var updated_len = input_val.length;
                caret_pos = updated_len - original_len + caret_pos;
                input[0].setSelectionRange(caret_pos, caret_pos);
            }
        </script>
    </main>
</asp:Content>
