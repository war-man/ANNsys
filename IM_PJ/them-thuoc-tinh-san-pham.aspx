<%@ Page Title="Thêm thuộc tính sản phẩm" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="them-thuoc-tinh-san-pham.aspx.cs" Inherits="IM_PJ.them_thuoc_tinh_san_pham" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panelborderheading">
                        <div class="panel-heading clear">
                            <h3 class="page-title left not-margin-bot">Thêm biến thể sản phẩm</h3>
                        </div>
                        <div class="panel-body">
                            <div class="form-row">
                                <asp:Label ID="lblError" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Mã sản phẩm
                                    <asp:RequiredFieldValidator ID="rq" runat="server" ControlToValidate="txtProductSKU" ForeColor="Red" SetFocusOnError="true" ErrorMessage="(*)" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                                <div class="row-right">
                                    <asp:TextBox ID="txtProductSKU" runat="server" CssClass="form-control sku-input" placeholder="Mã sản phẩm"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row" id="Minimum">
                                <div class="row-left">
                                    Tồn kho ít nhất
                                </div>
                                <div class="row-right">
                                    <asp:TextBox type="number" min="0" value="3" ID="pMinimumInventoryLevel" runat="server" CssClass="form-control" placeholder="Số lượng tồn kho ít nhất"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row" id="Maximum">
                                <div class="row-left">
                                    Tồn kho nhiều nhất
                                </div>
                                <div class="row-right">
                                    <asp:TextBox type="number" min="0" value="10" ID="pMaximumInventoryLevel" runat="server" CssClass="form-control" placeholder="Số lượng tồn kho nhiều nhất"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Giá sỉ
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="pRegular_Price" ForeColor="Red" ErrorMessage="(*)" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>
                                <div class="row-right">
                                    <asp:TextBox type="number" min="0" autocomplete="off" ID="pRegular_Price" runat="server" CssClass="form-control" placeholder="Giá sỉ"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row cost-of-goods">
                                <div class="row-left">
                                    Giá vốn
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="pCostOfGood" ForeColor="Red" ErrorMessage="(*)" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>
                                <div class="row-right">
                                    <asp:TextBox type="number" min="0" autocomplete="off" ID="pCostOfGood" runat="server" CssClass="form-control" placeholder="Giá vốn"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Giá lẻ
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="pRetailPrice" ForeColor="Red" ErrorMessage="(*)" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>
                                <div class="row-right">
                                    <asp:TextBox type="number" min="0" autocomplete="off" ID="pRetailPrice" runat="server" CssClass="form-control" placeholder="Giá lẻ"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Ảnh đại diện
                                </div>
                                <div class="row-right">
                                    <telerik:RadAsyncUpload Skin="Metro" runat="server" ID="ProductThumbnailImage" ChunkSize="0"
                                        Localization-Select="Chọn ảnh" AllowedFileExtensions=".jpeg,.jpg,.png"
                                        MultipleFileSelection="Disabled" OnClientFileSelected="OnClientFileSelected1" MaxFileInputsCount="1">
                                    </telerik:RadAsyncUpload>
                                    <asp:Image runat="server" ID="ProductThumbnail" Width="200" />
                                    <asp:HiddenField runat="server" ID="ListProductThumbnail" ClientIDMode="Static" />
                                    <div class="hidProductThumbnail"></div>
                                </div>
                            </div>
                            <div class="form-row">
                                <a href="javascript:;" class="btn primary-btn fw-btn not-fullwidth" onclick="addNewProduct()">Tạo mới</a>         
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn primary-btn fw-btn not-fullwidth" Text="Tạo mới" OnClick="btnSubmit_Click" Style="display: none" />
                                <asp:Literal ID="ltrBack" runat="server"></asp:Literal>                                
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hdfTempVariable" runat="server" />
        <asp:HiddenField ID="hdfVariableFull" runat="server" />
        <asp:HiddenField ID="hdfUserRole" runat="server" />
    </main>

    <telerik:radcodeblock runat="server">
        <script>
            function isBlank(str) {
                return (!str || /^\s*$/.test(str));
            }

            $(document).ready(function () {
                var userRole = $("#<%=hdfUserRole.ClientID%>").val();
                if (userRole != "0") {
                    $(".cost-of-goods").addClass("hide");
                }

                $("#<%=pRegular_Price.ClientID%>").blur(function () {
                    var cost = parseInt($("#<%=pRegular_Price.ClientID%>").val()) - 20000;
                    $("input.cost-price").val(cost);
                });

                $("#<%=pRegular_Price.ClientID%>").blur(function () {
                    var retailPrice = parseInt($("#<%=pRegular_Price.ClientID%>").val()) + 50000;
                    $("#<%=pRetailPrice.ClientID%>").val(retailPrice);
                });

                $('input.sku-input').val(function () {
                    return this.value.toUpperCase();
                });

            });

            function addNewProduct() {
                var SKU = $("#<%=txtProductSKU.ClientID%>").val();
                var giasi = $("#<%=pRegular_Price.ClientID%>").val();
                var giavon = $("#<%=pCostOfGood.ClientID%>").val();
                var giale = $("#<%=pRetailPrice.ClientID%>").val();


                if (SKU == "") {
                    $("#<%=txtProductSKU.ClientID%>").focus();
                    swal("Thông báo", "Chưa nhập mã sản phẩm", "error");
                }
                else if (giasi == "") {
                    $("#<%=pRegular_Price.ClientID%>").focus();
                    swal("Thông báo", "Chưa nhập giá sỉ", "error");
                }
                else if (giavon == "") {
                    $("#<%=pCostOfGood.ClientID%>").focus();
                    swal("Thông báo", "Chưa nhập giá vốn", "error");
                }
                else if (giale == "") {
                    $("#<%=pRetailPrice.ClientID%>").focus();
                    swal("Thông báo", "Chưa nhập giá lẻ", "error");
                }
                else if (parseFloat(giasi) < parseFloat(giavon)) {
                    $("#<%=pRegular_Price.ClientID%>").focus();
                    swal("Thông báo", "Giá sỉ không được thấp hơn giá vốn", "error");
                }
                else if (parseFloat(giasi) > parseFloat(giale)) {
                    $("#<%=pRetailPrice.ClientID%>").focus();
                    swal("Thông báo", "Giá lẻ không được thấp hơn giá sỉ", "error");
                }
                else {
                    $("#<%=btnSubmit.ClientID%>").click();
                    HoldOn.open();
                }
            }
            function OnClientFileSelected1(sender, args) {
                if ($telerik.isIE) return;
                else {
                    truncateName(args);
                    var file = args.get_fileInputField().files.item(args.get_rowIndex());
                    //var file = args.get_fileInputField().files.item(0);
                    showThumbnail(file, args);
                }
            }
        </script>
    </telerik:radcodeblock>
</asp:Content>
