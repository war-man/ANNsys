<%@ Page Title="Danh mục sản phẩm" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="quan-ly-danh-muc-san-pham.aspx.cs" Inherits="IM_PJ.quan_ly_danh_muc_san_pham" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/App_Themes/NewUI/css/jstree.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <h3 class="page-title left">Danh mục sản phẩm</h3>
                    <div class="right above-list-btn">
                        <a href="/them-danh-muc" class="h45-btn primary-btn btn">Thêm mới</a>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panelborderheading">
                        <div class="panel-body">
                            <div id="tree_1" class="tree-demo">
                                <asp:Literal ID="ltrTree" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <script src="/App_Themes/NewUI/js/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
        <script src="/App_Themes/NewUI/js/app.js"></script>
        <script src="/App_Themes/NewUI/js/jstree.min.js"></script>
        <script src="/App_Themes/NewUI/js/ui-tree.js"></script>
        <script type="text/javascript">
            function editCategory(ID) {
                window.location.href = "/thong-tin-danh-muc-san-pham?id=" + ID;
            }
            function AddChildCategory(ID) {
                var win = window.open("/tat-ca-san-pham?categoryid=" + ID + "", '_blank');
            }
        </script>
    </main>
    <style>
        .register-link {
            color: blue;
            text-decoration: underline;
            font-style: italic;
        }
    </style>
</asp:Content>
