<%@ Page Title="Thuộc tính sản phẩm" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="thuoc-tinh-san-pham.aspx.cs" Inherits="IM_PJ.thuoc_tinh_san_pham" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <h3 class="page-title left">
                        <asp:Label ID="lblCategoryName" runat="server"></asp:Label></h3>
                    <div class="right above-list-btn">
                        <asp:Literal ID="ltrAddnew" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="filter-above-wrap clear">
                        <div class="right">
                            <div class="filter-control">
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtSearchProduct" runat="server" CssClass="form-control" placeholder="Tìm sản phẩm"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <a href="javascript:;" onclick="searchAgent()" class="btn primary-btn h45-btn"><i class="fa fa-search"></i></a>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn primary-btn h45-btn" OnClick="btnSearch_Click" Style="display: none" />
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
                                    <tr>
                                        <th class="image-column">Ảnh</th>
                                        <th class="variable-column">Thuộc tính</th>
                                        <th class="sku-column">Mã</th>
                                        <th class="wholesale-price-column">Giá sỉ</th>
                                        <th class="cost-price-column cost hide">Giá vốn</th>
                                        <th class="retail-price-column">Giá lẻ</th>
                                        <th class="stock-column">Kho</th>
                                        <th class="stock-status-column">Trạng thái</th>
                                        <th class="date-column">Ngày tạo</th>
                                        <th class="hide-column">Ẩn</th>
                                        <th class="action-column">Thao tác</th>
                                    </tr>
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
        <asp:HiddenField ID="hdfcost" runat="server" />
        <asp:HiddenField ID="hdfSearch" runat="server" />
        <script type="text/javascript">
            function searchAgent() {
                $("#<%= btnSearch.ClientID%>").click();
            }

            var cost = document.getElementById('<%= hdfcost.ClientID%>').defaultValue;
            if (cost == "ok") {
                $(".cost").removeClass("hide");
            }
        </script>
    </main>
</asp:Content>
