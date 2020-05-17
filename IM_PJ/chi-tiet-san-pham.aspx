<%@ Page Language="C#" Title="Chi tiết sản phẩm" AutoEventWireup="true" MasterPageFile="~/ProductPage.Master" CodeBehind="chi-tiet-san-pham.aspx.cs" Inherits="IM_PJ.chi_tiet_san_pham" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            background-color: #eee;
        }
        .main-block {
            margin: auto;
            float: inherit;
        }
        .btn.download-btn {
            background-color: #000;
            color: #fff;
            border-radius: 0;
            font-size: 16px;
            text-transform: uppercase;
            width: 100%;
        }
        .btn.down-btn {
            background-color: #E91E63;
            color: #fff;
        }

        .bootstrap-tagsinput {
            width: 100%;
        }
        
        .bootstrap-tagsinput .label {
            font-size: 100%;
        }
        .bg-green {
            display: inherit;
        }
        .table > tbody > tr > th {
            background-color: #0090da;
        }
        .btn {
            border-radius: 5px;
        }
        .btn.primary-btn {
            background-color: #4bac4d;
        }
        .btn.primary-btn:hover {
            background-color: #3e8f3e;
        }
        .margin-right-15px {
            margin-right: 12px!important;
        }
        img {
            max-width: 100%;
            width: auto;
        }
        .pagination li > a, .pagination li > a {
            height: 24px;
            border: transparent;
            line-height: 24px;
            -ms-box-orient: horizontal;
            display: -webkit-box;
            display: -moz-box;
            display: -ms-flexbox;
            display: -moz-flex;
            display: -webkit-flex;
            display: flex;
            align-items: center;
            justify-content: center;
            background-color: transparent;
            z-index: 99;
            width: auto;
            min-width: 24px;
            -webkit-border-radius: 2px;
            -moz-border-radius: 2px;
            border-radius: 2px;
            margin: 0 5px 0 0;
        }
        .pagination li.current {
            background-color: #4bac4d;
            color: #fff;
            height: 24px;
            border: transparent;
            line-height: 24px;
            -ms-box-orient: horizontal;
            align-items: center;
            justify-content: center;
            z-index: 99;
            width: auto;
            min-width: 24px;
            -webkit-border-radius: 2px;
            -moz-border-radius: 2px;
            border-radius: 2px;
            margin: 0 5px 0 0;
            text-align: center;
        }
        .pagination li:hover > a {
            background-color: #e5e5e5;
            color: #000;
        }
        @media (max-width: 769px) {
            ul.image-gallery li {
                width: 100%;
            }
            .btn {
                width: 100%!important;
                float: left;
                margin-bottom: 15px;
                margin-left: 0;
            }
            .download-btn {
                margin-left: 0;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main>
        <div class="col-md-8 main-block">
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panelborderheading">
                        <div class="panel-heading clear">
                            <h3 class="page-title left not-margin-bot"><asp:Literal ID="ltrProductName" runat="server"></asp:Literal></h3>
                        </div>
                        <div class="panel-body">
                            <div class="form-row">
                                <asp:Literal ID="ltrEdit1" runat="server"></asp:Literal>
                            </div>
                            <div class="form-row">
                                🔰 <strong>Danh mục:</strong> <asp:Literal ID="ltrCategory" runat="server"></asp:Literal>
                            </div>
                            <div class="form-row">
                                🔰 <strong>Mã sản phẩm:</strong> <asp:Literal ID="ltrSKU" runat="server"></asp:Literal>
                            </div>
                            <div class="form-row">
                                🔰 <strong>Chất liệu:</strong> <asp:Literal ID="ltrMaterials" runat="server"></asp:Literal>
                            </div>
                            <div class="form-row">
                                🔰 <strong>Tổng số lượng:</strong> <asp:Literal ID="ltrStock" runat="server"></asp:Literal>
                            </div>
                            <div class="form-row">
                                🔰 <strong>Trạng thái:</strong> <asp:Literal ID="ltrStockStatus" runat="server"></asp:Literal>
                            </div>
                            <div class="form-row">
                                🔰 <strong>Giá sỉ:</strong> <asp:Literal ID="ltrRegularPrice" runat="server"></asp:Literal>
                            </div>
                            <div class="form-row">
                                🔰 <strong>Giá lẻ:</strong> <asp:Literal ID="ltrRetailPrice" runat="server"></asp:Literal>
                            </div>
                            <div class="form-row">
                                <asp:Literal ID="pContent" runat="server"></asp:Literal>
                            </div>
                            <div class="form-row">
                                <asp:Literal ID="imageGallery" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row tableview">
                <div class="col-md-12">
                    <div class="panel-table clear">
                        <div class="responsive-table">
                            <table class="table table-checkable table-product all-variable-table-2">
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

        <asp:HiddenField ID="hdfTempVariable" runat="server" />
        <asp:HiddenField ID="hdfVariableFull" runat="server" />
        <asp:HiddenField ID="hdfTable" runat="server" />
    </main>

    <script src="/App_Themes/Ann/js/copy-product-info-public.js?v=17052020"></script>
    <script src="/App_Themes/Ann/js/download-product-image.js?v=17052020"></script>
    <script>
        function postProductKiotViet(productSKU) {
            let titleAlert = "Đồng bộ KiotViet";

            if (!productSKU)
                _alterError(titleAlert, { message: "Chưa chọn sản phẩm nào!" });

            let dataJSON = JSON.stringify({ "productSKU": productSKU });
            $.ajax({
                beforeSend: function () {
                    HoldOn.open();
                },
                method: 'POST',
                contentType: 'application/json',
                dataType: "json",
                data: dataJSON,
                url: "/api/v1/kiotviet/product",
                success: (response, textStatus, xhr) => {
                    HoldOn.close();

                    if (xhr.status == 200) {
                        _alterSuccess(titleAlert, "Sản phẩm <strong>" + productSKU + "</strong> đồng bộ thành công!");
                    } else {
                        _alterError(titleAlert);
                    }
                },
                error: (xhr, textStatus, error) => {
                    HoldOn.close();

                    _alterError(titleAlert, xhr.responseJSON);
                }
            });
        }

        function _alterSuccess(title, message) {
            title = (typeof title !== 'undefined') ? title : 'Thông báo thành công';

            if (message === undefined) {
                message = null;
            }

            return swal({
                title: title,
                text: message,
                type: "success",
                html: true
            });
        }
        
        function _alterError(title, responseJSON) {
            let message = '';
            title = (typeof title !== 'undefined') ? title : 'Thông báo lỗi';

            if (responseJSON === undefined || responseJSON === null) {
                message = 'Đẫ có lỗi xãy ra.';
            }
            else {
                if (responseJSON.message)
                    message += responseJSON.message;
            }

            return swal({
                title: title,
                text: message,
                type: "error",
                html: true
            });
        }
    </script>
</asp:Content>
