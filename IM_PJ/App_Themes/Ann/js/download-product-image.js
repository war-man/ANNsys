function getAllProductImage(sku) {
    $.ajax({
        type: "POST",
        url: "/tat-ca-san-pham.aspx/getAllProductImage",
        data: "{sku: '" + sku + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d != "false") {
                var data = JSON.parse(msg.d);

                var link = document.createElement('a');

                for (var i = 0; i < data.length; i++) {
                    (function (i) {
                        setTimeout(function () {
                            link.setAttribute('download', sku + '-' + (i + 1));
                            link.setAttribute('href', data[i]);
                            link.click();
                        }, 1000 * i);
                    })(i);
                }

            }
            else {
                alert("Lỗi");
            }
        }
    });
}