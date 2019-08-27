<%@ Page Title="Danh sách đăng ký mua hàng" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="danh-sach-dang-ky.aspx.cs" Inherits="IM_PJ.danh_sach_dang_ky" EnableSessionState="ReadOnly" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .select2-container .select2-selection--single {
            height: 45px;
        }
        .select2-container--default .select2-selection--single .select2-selection__rendered {
            line-height: 45px;
        }
        .select2-container--default .select2-selection--single .select2-selection__arrow {
            height: 43px;
        }
        @media (max-width: 768px) {
            table.shop_table_responsive thead {
	            display: none;
            }
            table.shop_table_responsive > tbody > tr > td:nth-of-type(1):before {
                content: "#";
                font-size: 20px;
                margin-right: 2px;
            }
            table.shop_table_responsive > tbody > tr > td:nth-of-type(1) {
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
            table.shop_table_responsive > tbody > tr > td.payment-type, table.shop_table_responsive > tbody > tr > td.shipping-type {
                height: 70px;
            }
            table.shop_table_responsive > tbody > tr > td .new-status-btn {
                display: block;
                margin-top: 10px;
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
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <h3 class="page-title left">Đăng ký mua hàng <span>(<asp:Literal ID="ltrNumberOfOrder" runat="server" EnableViewState="false"></asp:Literal>)</span></h3>
                    <div class="right above-list-btn">
                        <asp:Literal ID="ltrCreateButton" runat="server" EnableViewState="false"></asp:Literal>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="filter-above-wrap clear">
                        <div class="filter-control">
                            <div class="row">
                                <div class="col-md-3 col-xs-6">
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Tìm người đăng ký" autocomplete="off"></asp:TextBox>
                                </div>
                                <div class="col-md-2 col-xs-6">
                                    <asp:DropDownList ID="ddlProvince" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-xs-6">
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="" Text="Trạng thái"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Chưa xem"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Đã xem"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Đã bàn giao"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="Đã tiếp nhận"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="Khách hàng"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-xs-6">
                                    <asp:DropDownList ID="ddlUser" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-xs-6">
                                    <asp:DropDownList ID="ddlCreatedDate" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="" Text="Thời gian"></asp:ListItem>
                                        <asp:ListItem Value="today" Text="Hôm nay"></asp:ListItem>
                                        <asp:ListItem Value="yesterday" Text="Hôm qua"></asp:ListItem>
                                        <asp:ListItem Value="beforeyesterday" Text="Hôm kia"></asp:ListItem>
                                        <asp:ListItem Value="week" Text="Tuần này"></asp:ListItem>
                                        <asp:ListItem Value="7days" Text="7 ngày"></asp:ListItem>
                                        <asp:ListItem Value="thismonth" Text="Tháng này"></asp:ListItem>
                                        <asp:ListItem Value="lastmonth" Text="Tháng trước"></asp:ListItem>
                                        <asp:ListItem Value="beforelastmonth" Text="Tháng trước nữa"></asp:ListItem>
                                        <asp:ListItem Value="30days" Text="30 ngày"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-1 col-xs-6 search-button">
                                    <a href="javascript:;" onclick="searchRegister()" class="btn primary-btn h45-btn"><i class="fa fa-search"></i></a>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn primary-btn h45-btn" OnClick="btnSearch_Click" Style="display: none" />
                                    <a href="/danh-sach-dang-ky" class="btn primary-btn h45-btn"><i class="fa fa-times" aria-hidden="true"></i></a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="filter-above-wrap clear">
                        <div class="filter-control">
                            <div class="row">
                                <div class="col-md-7">
                                </div>
                                <div class="col-md-2 col-xs-6">
                                    <asp:DropDownList ID="ddlProductCategory" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="" Text="Quan tâm"></asp:ListItem>
                                        <asp:ListItem Value="Mua lẻ" Text="Mua lẻ"></asp:ListItem>
                                        <asp:ListItem Value="Quần áo nam" Text="Quần áo nam"></asp:ListItem>
                                        <asp:ListItem Value="Quần áo nữ" Text="Quần áo nữ"></asp:ListItem>
                                        <asp:ListItem Value="Nước hoa" Text="Nước hoa"></asp:ListItem>
                                        <asp:ListItem Value="Tất cả sản phẩm" Text="Tất cả sản phẩm"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2 col-xs-6">
                                    <asp:DropDownList ID="ddlReferer" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="" Text="Nguồn từ"></asp:ListItem>
                                        <asp:ListItem Value="user" Text="Nhân viên thu thập"></asp:ListItem>
                                        <asp:ListItem Value="khohangsiann.com" Text="khohangsiann.com"></asp:ListItem>
                                        <asp:ListItem Value="ann.com.vn" Text="ann.com.vn"></asp:ListItem>
                                        <asp:ListItem Value="bosiquanao.net" Text="bosiquanao.net"></asp:ListItem>
                                    </asp:DropDownList>
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
                            <table class="table table-checkable table-product shop_table_responsive">
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

        <!-- Modal Update Register Delivery -->
        <div class="modal fade" id="UpdateRegisterModal" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Cập nhật đăng ký mua sỉ</h4>
                    </div>
                    <div class="modal-body">
                        <asp:HiddenField ID="hdfRegisterID" runat="server" />
                        <div class="row form-group">
                            <div class="col-xs-4">
                                <p>Bàn giao cho nhân viên</p>
                            </div>
                            <div class="col-xs-8">
                                <asp:DropDownList ID="ddlUserModal" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button id="closeRegisterUpdate" type="button" class="btn btn-default" data-dismiss="modal">Đóng</button>
                        <button id="updateRegister" type="button" class="btn btn-primary">Cập nhật</button>
                    </div>
                </div>
            </div>
        </div>

        <!-- Modal Create Register -->
        <div class="modal fade" id="CreateRegisterModal" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Thêm mới đăng ký mua sỉ</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row form-group">
                            <div class="col-md-3 col-xs-4">
                                <p>Tên khách</p>
                            </div>
                            <div class="col-md-9 col-xs-8">
                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control text-right"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-md-3 col-xs-4">
                                <p>Điện thoại</p>
                            </div>
                            <div class="col-md-9 col-xs-8">
                                <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control text-right" onkeypress='return event.charCode >= 48 && event.charCode <= 57'></asp:TextBox>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-md-3 col-xs-4">
                                <p>Phụ trách</p>
                            </div>
                            <div class="col-md-9 col-xs-8">
                                <asp:DropDownList ID="ddlUserCreateModal" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button id="closeRegisterCreate" type="button" class="btn btn-default" data-dismiss="modal">Đóng</button>
                        <button id="createRegister" type="button" class="btn btn-primary">Xác nhận</button>
                    </div>
                </div>
            </div>
        </div>

        <!-- Modal View Register -->
        <div class="modal fade" id="ViewMessageModal" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Tin nhắn</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row form-group">
                            <div class="col-md-12 col-xs-12">
                                <p id="messageContent"></p>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button id="closeMessage" type="button" class="btn btn-default" data-dismiss="modal">Đóng</button>
                    </div>
                </div>
            </div>
        </div>

        <script type="text/javascript">
            $("#<%=txtSearch.ClientID%>").keyup(function (e) {
                if (e.keyCode == 13)
                {
                    $("#<%= btnSearch.ClientID%>").click();
                }
            });

            $(document).ready(() => {

                $("a[data-target='#ViewMessageModal']").click(e => {
                    let row = e.currentTarget.parentNode.parentNode;
                    let registerID = row.dataset["registerid"];

                    $.ajax({
                        type: "POST",
                        url: "/danh-sach-dang-ky.aspx/getRegisterMessage",
                        data: "{id: " + registerID + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            $("#messageContent").html(msg.d);
                        },
                        error: function (xmlhttprequest, textstatus, errorthrow) {
                            swal("Thông báo", "Đã có vấn đề trong việc lấy tin nhắn của khách này", "error");
                        }
                    });
                });

                $("#createRegister").click(e => {
                    let name = $("#<%=txtName.ClientID%>").val();
                    let phone = $("#<%=txtPhone.ClientID%>").val();
                    let userid = $("#<%=ddlUserCreateModal.ClientID%>").val();

                    let data = {
                        'Name': name,
                        'Phone': phone,
                        'UserID': userid
                    };

                    if (name == "" || phone == "" || userid == 0) {
                        swal("Thông báo", "Chưa nhập đầy đủ thông tin", "error");
                    }
                    else {
                        $.ajax({
                            type: "POST",
                            url: "/danh-sach-dang-ky.aspx/createRegister",
                            data: JSON.stringify({'data': data}),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            beforeSend: function () {
                                HoldOn.open();
                            },
                            success: function (msg) {
                                if (msg.d == "true") {
                                    swal({
                                        title: 'Thông báo',
                                        text: 'Đã thêm mới thành công!',
                                        type: 'success',
                                        showCancelButton: false,
                                        closeOnConfirm: true,
                                        confirmButtonText: "OK",
                                    }, function (confirm) {
                                        if (confirm) window.location.replace("/danh-sach-dang-ky");
                                    });
                                }
                                else {
                                    alert("Lỗi");
                                }
                            },
                            error: function (xmlhttprequest, textstatus, errorthrow) {
                                swal("Thông báo", "Đã có vấn đề trong việc tạo đăng ký mua sỉ", "error");
                            },
                            complete: function() {
                                HoldOn.close();
                            }
                        });
                    }

                });

                $("button[data-target='#UpdateRegisterModal']").click(e => {
                    let row = e.currentTarget.parentNode.parentNode;
                    let modal = $("#UpdateRegisterModal");
                    let registerIDDOM = modal.find("#<%=hdfRegisterID.ClientID%>");
                    registerIDDOM.val(row.dataset["registerid"]);
                });

                $("#updateRegister").click(e => {
                    let registerID = $("#<%=hdfRegisterID.ClientID%>").val();
                    let userID = $("#<%=ddlUserModal.ClientID%>").val();

                    if (userID == 0) {
                        swal("Thông báo", "Chưa chọn nhân viên", "error");
                    }
                    else {
                        $.ajax({
                            type: "POST",
                            url: "/danh-sach-dang-ky.aspx/updateUser",
                            data: "{id: " + registerID + ", userid: " + userID + "}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            beforeSend: function () {
                                HoldOn.open();
                            },
                            success: function (msg) {
                                if (msg.d == "true") {
                                    let row = $("tr[data-registerid='" + registerID + "']");
                                    let statusDOM = row.find('#status');
                                    let userDOM = row.find('#user');
                                    let phoneDOM = row.find('#phone');
                                    let phoneValue = phoneDOM.find('a').attr('data-phone');
                                    // Update screen
                                    row.attr("data-userid", userID);
                                    statusDOM.html("<span class='bg-blue'>Đã bàn giao</span>");
                                    userDOM.html($("#<%=ddlUserModal.ClientID%> :selected").text());
                                    phoneDOM.html(phoneValue);

                                    $("#closeRegisterUpdate").click();
                                    HoldOn.close();
                                }
                                else {
                                    alert("Lỗi");
                                }
                            },
                            error: function (xmlhttprequest, textstatus, errorthrow) {
                                swal("Thông báo", "Đã có vấn đề trong việc cập nhật thông tin đăng ký", "error");
                            }
                        });
                    }
                    
                });

            });

            function searchRegister() {
                $("#<%= btnSearch.ClientID%>").click();
            }

            function updateStatus(obj) {
                var ID = obj.attr("data-id");
                var value = obj.attr("data-value");
                $.ajax({
                    type: "POST",
                    url: "/danh-sach-dang-ky.aspx/updateStatus",
                    data: "{id: " + ID + ", value: " + value + "}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d == "true") {
                            if (value == 2) {
                                $(obj).parent().html("<a href='javascript:;' data-id='" + ID + "' data-value='1' class='bg-yellow bg-button' onclick='updateStatus($(this))'>Đã xem</a>");
                            }
                            else {
                                $(obj).parent().html("<a href='javascript:;' data-id='" + ID + "' data-value='2' class='bg-black bg-button' onclick='updateStatus($(this))'>Chưa xem</a>");
                            }
                        }
                        else {
                            alert("Lỗi");
                        }
                    }
                });
            }
            
            function getPhone(obj) {
                var ID = obj.attr("data-id");
                var phone = obj.attr("data-phone");
                var update = obj.attr("data-update");
                $.ajax({
                    type: "POST",
                    url: "/danh-sach-dang-ky.aspx/updateStatus",
                    data: "{id: " + ID + ", value: " + update + "}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d == "true") {
                            let row = $("tr[data-registerid='" + ID + "']");
                            let statusDOM = row.find('#status');
                            let phoneDOM = row.find('#phone');
                            phoneDOM.html("<a href='https://zalo.me/" + phone + "' target='_blank'>" + phone + "</a>");
                            if (update == 2) {
                                statusDOM.html("<span class='bg-yellow'>Đã xem</span>");
                            }
                            if (update == 4) {
                                statusDOM.html("<span class='bg-green'>Đã tiếp nhận</span>");
                            }
                        }
                        else {
                            alert("Lỗi");
                        }
                    }
                });
            }

            function deleteRegister(id) {
                swal({
                    title: "Thông báo",
                    text: 'Bạn có muốn xóa thông tin đăng ký này?',
                    type: 'warning',
                    showCancelButton: true,
                    closeOnConfirm: true,
                    cancelButtonText: "Để xem lại",
                    confirmButtonText: "OK xóa...",
                    html: true,
                }, function (confirm) {
                    if (confirm) {
                        $.ajax({
                            type: "POST",
                            url: "/danh-sach-dang-ky.aspx/deleteRegister",
                            data: "{id: " + id + "}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            beforeSend: function () {
                                HoldOn.open();
                            },
                            success: function (msg) {
                                if (msg.d == "true") {
                                    let row = $("tr[data-registerid='" + id + "']");
                                    row.remove();
                                    HoldOn.close();
                                }
                                else {
                                    alert("Lỗi");
                                }
                            },
                            error: function (xmlhttprequest, textstatus, errorthrow) {
                                swal("Thông báo", "Đã có vấn đề trong việc xóa thông tin đăng ký", "error");
                            }
                        });
                    }
                });
                
            }
        </script>
    </main>
</asp:Content>
