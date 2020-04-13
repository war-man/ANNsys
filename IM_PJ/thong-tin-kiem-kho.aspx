<%@ Page Title="Xem thông tin kiểm kho" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="thong-tin-kiem-kho.aspx.cs" Inherits="IM_PJ.thong_tin_kiem_kho" EnableSessionState="ReadOnly" %>
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
                                <asp:Literal ID="ltrCheckWarehouse" runat="server"></asp:Literal>
                                <a href="javascript:;" class="btn primary-btn fw-btn not-fullwidth" style="margin-right: 10px" onclick="getCheckWarehouse()"><i class="fa fa-search" aria-hidden="true"></i> Tìm</a>
                            </div>
                            <div class="form-row">
                                <h3 class="no-margin float-left">Kết quả tìm kiếm: <span class="result-numsearch"></span></h3>
                                <div class="excute-in">
                                    <a href="javascript:;" style="background-color: #f87703; float: right;" class="btn primary-btn link-btn" onclick="closeCheckWarehouse()">Kết thúc kiểm tra kho</a>
                                </div>
                            </div>
                            <div class="form-row" style="border: solid 1px #ccc; padding: 10px;">
                                <table class="table table-checkable table-product import-stock">
                                    <thead>
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
        function clickrow(obj) {
            if (!obj.find("td").eq(1).hasClass("checked")) {
                obj.find("td").addClass("checked");
            }
            else {
                obj.find("td").removeClass("checked");
            }
        }

        function check() {
            var temp = 0;
            var temp2 = 0;
            $(".product-result").each(function () {
                if ($(this).find(".check-popup").is(':checked')) {
                    temp++;
                }
                else {
                    temp2++;
                }
                if (temp2 > 0) {
                    $("#check-all").prop('checked', false);
                }
                else {
                    $("#check-all").prop('checked', true);
                }
            });
        }

        function getCheckWarehouse() {
            let checkWarehouseID = +$("#ddlCheckWarehouse").val() || 0;

            if (!checkWarehouseID)
                return;

            $("#<%=hdfCheckHouseID.ClientID%>").val(checkWarehouseID);


            $.ajax({
                type: "POST",
                url: "/thong-tin-kiem-kho.aspx/getCheckWarehouse",
                data: JSON.stringify({id: checkWarehouseID}),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var count = 0;
                    var data = JSON.parse(msg.d);
                    if (data.length > 0) {
                        var html = "";
                        for (var i = 0; i < data.length; i++) {
                            var item = data[i];
                            var sku = item.SKU;
                            var check = false;

                            $(".product-result").each(function () {
                                var existedSKU = $(this).attr("data-sku");
                                if (sku == existedSKU) {
                                    check = true;
                                }
                            });

                            if (check == false) {
                                html += "<tr ondblclick=\"clickrow($(this))\" class=\"product-result\" >";
                                html += "   <td class='name-item'><a href='/xem-san-pham?sku=" + item.SKU + "' target='_blank'>" + item.ProductName + "</a></td>";
                                html += "   <td class='sku-item'>" + item.SKU + "</td>";
                                html += "   <td>" + item.QuantityOld + "</td>";
                                html += "   <td>" + item.QuantityNew + "</td>";
                                html += "   <td>" + item.ModifiedDate + "</td>";
                                html += "   <td>" + item.ModifiedBy + "</td>";
                                html += "</tr>";
                            }
                        }
                        $(".content-product").prepend(html);
                        $("#txtSearch").val("");
                        if ($(".product-result").length > 0) {
                            $(".excute-in").show();
                        }
                    }
                    else {
                        swal('Thông báo', 'Không tìm thấy sản phẩm', 'error');
                    }
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    alert('lỗi');
                }
            });
        }

        function closeCheckWarehouse() {
            $("#<%=btnCloseCheckWareHouse.ClientID%>").click();
        }
    </script>
</asp:Content>
