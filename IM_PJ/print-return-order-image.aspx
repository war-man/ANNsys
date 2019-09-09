<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="print-return-order-image.aspx.cs" Inherits="IM_PJ.print_return_order_image" EnableSessionState="ReadOnly" %>

<!DOCTYPE html>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8" />
    <link rel="stylesheet" href="/App_Themes/Ann/css/print-return-order-image.css?v=24082019" type="text/css"/>
    <link rel="stylesheet" href="/App_Themes/Ann/barcode/style.css" type="text/css"/>
    <link rel="stylesheet" href="/App_Themes/Ann/css/responsive.css" type="text/css"/>
    <script src="/App_Themes/Ann/js/jquery-2.1.3.min.js"></script>
    <script src="/App_Themes/Ann/js/html2canvas.js"></script>
    <title>Lấy ảnh đơn đổi trả</title>    
</head>
<body>
    <h2 class="guide">Click chuột phải vào ảnh -> Chọn Sao chép hình ảnh -> Dán vào Zalo hoặc Facebook (làm từng ảnh)</h2>
    <div id="previewImage"></div>
    <asp:Literal ID="ltrPrintInvoice" runat="server"></asp:Literal>
    <asp:Literal ID="ltrPrintEnable"  runat="server"></asp:Literal>
    <script>
        $(document).ready(function () {
            var print = $(".print");
            for (var i = 0; i < print.length; i++) {
                html2canvas(document.querySelector(".print-" + i), {
                    allowTaint: true,
                    logging: false
                }).then(canvas => {
                    $("#previewImage").append(canvas);
                    $(".print-order-image").hide();
                    $(".guide").addClass("p-guide");
                });
            }
        });
    </script>
</body>
</html>