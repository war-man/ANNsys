<%@ Page Title="Danh sách nhà xe" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="danh-sach-nha-xe.aspx.cs" Inherits="IM_PJ.danh_sach_nha_xe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        tr.status-0 td {
            background-color: #fed1d1!important;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <h3 class="page-title left">Nhà xe <span>(<asp:Literal ID="ltrNumberOfTransport" runat="server" EnableViewState="false"></asp:Literal>)</span></h3>
                    <div class="right above-list-btn">
                        <a href="/them-moi-nha-xe" class="h45-btn primary-btn btn">Thêm mới</a>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="filter-above-wrap clear">
                        <div class="filter-control">
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtTextSearch" runat="server" CssClass="form-control" placeholder="Tìm nhà xe"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlCOD" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="" Text="Thụ hộ"></asp:ListItem>
                                        <asp:ListItem Value="true" Text="Có"></asp:ListItem>
                                        <asp:ListItem Value="false" Text="Không"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlPrepay" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="" Text="Phí vận chuyển"></asp:ListItem>
                                        <asp:ListItem Value="true" Text="Trả trước"></asp:ListItem>
                                        <asp:ListItem Value="false" Text="Trả sau"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlCreatedDate" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="" Text="Thời gian"></asp:ListItem>
                                        <asp:ListItem Value="today" Text="Hôm nay"></asp:ListItem>
                                        <asp:ListItem Value="yesterday" Text="Hôm qua"></asp:ListItem>
                                        <asp:ListItem Value="beforeyesterday" Text="Hôm kia"></asp:ListItem>
                                        <asp:ListItem Value="week" Text="Tuần này"></asp:ListItem>
                                        <asp:ListItem Value="thismonth" Text="Tháng này"></asp:ListItem>
                                        <asp:ListItem Value="7days" Text="7 ngày"></asp:ListItem>
                                        <asp:ListItem Value="30days" Text="30 ngày"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-1 search-button">
                                    <a href="javascript:;" onclick="searchTransport()" class="btn primary-btn h45-btn"><i class="fa fa-search"></i></a>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn primary-btn h45-btn" OnClick="btnSearch_Click" Style="display: none" />
                                    <a href="/danh-sach-nha-xe" class="btn primary-btn h45-btn"><i class="fa fa-times" aria-hidden="true"></i></a>
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
                            <table class="table table-checkable table-product table-transport">
                                <tbody>
                                    <asp:Literal ID="ltrList" runat="server" EnableViewState="false"></asp:Literal>
                                </tbody>
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
    </main>
    <script type="text/javascript">
        $("#<%=txtTextSearch.ClientID%>").keyup(function (e) {
            if (e.keyCode == 13)
            {
                $("#<%= btnSearch.ClientID%>").click();
            }
        });
        function searchTransport() {
            $("#<%= btnSearch.ClientID%>").click();
        }

        function updateStatus(obj) {
            var ID = obj.attr("data-id");
            var SubID = obj.attr("data-subid");
            var Status = obj.attr("data-status");
            swal({
                title: 'Thông báo',
                text: 'Bạn có muốn ẩn/hiện nhà xe này?',
                type: 'warning',
                showCancelButton: true,
                closeOnConfirm: true,
                confirmButtonText: "Xác nhận",
            }, function (confirm) {
                if (confirm)
                {
                    $.ajax({
                        type: "POST",
                        url: "/danh-sach-nha-xe.aspx/updateStatus",
                        data: "{ID: " + ID + ", SubID: " + SubID + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.d == "true")
                            {
                                if (Status == 1) {
                                    $(obj).attr("title", "Hiện nhà xe");
                                    $(obj).attr("data-status", "0");
                                    $(obj).removeClass("btn-red");
                                    $(obj).addClass("btn-blue");
                                    $(obj).html("<i class='fa fa-refresh' aria-hidden='true'></i>");
                                    $(obj).parent().parent().removeClass("status-1").addClass("status-0");
                                }
                                else {
                                    $(obj).attr("title", "Ẩn nhà xe");
                                    $(obj).attr("data-status", "1");
                                    $(obj).removeClass("btn-blue");
                                    $(obj).addClass("btn-red");
                                    $(obj).html("<i class='fa fa-times' aria-hidden='true'></i>");
                                    $(obj).parent().parent().removeClass("status-0").addClass("status-1");
                                }
                            }
                            else {
                                alert("Lỗi");
                            }
                        }
                    });
                } 
            });
        }
    </script>
</asp:Content>
