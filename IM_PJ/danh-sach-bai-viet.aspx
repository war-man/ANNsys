<%@ Page Title="Danh sách bài viết" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="danh-sach-bai-viet.aspx.cs" Inherits="IM_PJ.danh_sach_bai_viet" EnableSessionState="ReadOnly" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .btn.download-btn {
            background-color: #000;
            color: #fff;
            border-radius: 0;
            text-transform: uppercase;
            width: 100%;
            height: 35px;
            line-height: 8px;
        }
        .btn.download-btn:hover {
            color: #ff8400;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <h3 class="page-title left">Danh sách bài viết <span>(<asp:Literal ID="ltrNumberOfPost" runat="server" EnableViewState="false"></asp:Literal> bài viết)</span></h3>
                    <div class="right above-list-btn">
                        <asp:Literal ID="ltrAddPost" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="filter-above-wrap clear">
                        <div class="filter-control">
                            <div class="row">
                                <div class="col-md-5">
                                    <asp:TextBox ID="txtSearchPost" runat="server" CssClass="form-control" placeholder="Tìm bài viết" autocomplete="off"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="" Text="Trạng thái"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Xuất bản"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="Đang ẩn"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlCreatedDate" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="" Text="Tất cả thời gian"></asp:ListItem>
                                        <asp:ListItem Value="today" Text="Hôm nay"></asp:ListItem>
                                        <asp:ListItem Value="yesterday" Text="Hôm qua"></asp:ListItem>
                                        <asp:ListItem Value="beforeyesterday" Text="Hôm kia"></asp:ListItem>
                                        <asp:ListItem Value="week" Text="Tuần này"></asp:ListItem>
                                        <asp:ListItem Value="month" Text="Tháng này"></asp:ListItem>
                                        <asp:ListItem Value="7days" Text="7 ngày"></asp:ListItem>
                                        <asp:ListItem Value="30days" Text="30 ngày"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-1">
                                    <a href="javascript:;" onclick="searchPost()" class="btn primary-btn h45-btn"><i class="fa fa-search"></i></a>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn primary-btn h45-btn" OnClick="btnSearch_Click" Style="display: none" />
                                    <a href="/danh-sach-bai-viet" class="btn primary-btn h45-btn"><i class="fa fa-times" aria-hidden="true"></i></a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="panel-table clear">
                        <div class="panel-footer clear">
                            <div class="pagination">
                                <%this.DisplayHtmlStringPaging1();%>
                            </div>
                        </div>
                        <div class="responsive-table">
                            <table class="table table-checkable table-product all-product-table">
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

        <script src="/App_Themes/Ann/js/copy-post-info.js?v=2011"></script>
        <script src="/App_Themes/Ann/js/download-post-image.js?v=2011"></script>
        
        <script type="text/javascript">

            // Parse URL Queries
            function url_query(query) {
                query = query.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
                var expr = "[\\?&]" + query + "=([^&#]*)";
                var regex = new RegExp(expr);
                var results = regex.exec(window.location.href);
                if (results !== null) {
                    return results[1];
                } else {
                    return false;
                }
            }

            var url_param = url_query('quantityfilter');
            if (url_param) {
                if (url_param == "greaterthan" || url_param == "lessthan") {
                    $(".greaterthan").removeClass("hide");
                    $(".between").addClass("hide");
                }
                else if (url_param == "between") {
                    $(".between").removeClass("hide");
                    $(".greaterthan").addClass("hide");
                }
            }

            function searchPost() {
                $("#<%= btnSearch.ClientID%>").click();
            }

            function deletePost(id) {
                swal({
                    title: "Xác nhận",
                    text: "Bạn muốn xóa bài viết này?",
                    type: "warning",
                    showCancelButton: true,
                    closeOnConfirm: true,
                    cancelButtonText: "Để em xem lại...",
                    confirmButtonText: "Đúng rồi sếp!",
                }, function (confirm) {
                    if (confirm) {
                        ajaxDeletePost(id, function (d) {
                            if (d === "success") {
                                $("tr.item-" + id).remove();
                            }
                            else if (d === "failed") {
                                alert("Lỗi");
                            }
                            else if (d === "notfound") {
                                alert("Không tìm thấy bài viết");
                            }
                        });
                    }
                });
                
            }

            function ajaxDeletePost(id, callback) {
                var msg;
                $.ajax({
                    type: "POST",
                    url: "/danh-sach-bai-viet.aspx/deletePost",
                    data: "{id: " + id + "}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    success: function (msg) {
                        msg = msg.d;
                        callback(msg);
                    },
                    error: function () {

                    }
                });

            }
            
        </script>
    </main>
</asp:Content>
