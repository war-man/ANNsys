function ShowUpProductToWeb(sku, id, up, renew) {
    var web = ["ann.com.vn", "khohangsiann.com", "bosiquanao.net", "quanaogiaxuong.com", "bansithoitrang.net", "quanaoxuongmay.com", "annshop.vn"];

    closePopup();

    var html = "<div class='row'><div class='col-md-12'><h2>Đồng bộ sản phẩm " + sku + "</h2><br></div></div>";
    html += "<div class='web-list'></div><div class='row'><div class='col-md-12'><p><span><a href=\"javascript:;\" class=\"btn primary-btn h45-btn\" onclick=\"ShowUpProductToWeb('" + sku + "', '" + id + "', 'true', 'false')\"><i class=\"fa fa-upload\" aria-hidden=\"true\"></i> Up tất cả</a><a href=\"javascript:;\" class=\"btn primary-btn h45-btn btn-black print-invoice-merged\" onclick=\"ShowUpProductToWeb('" + sku + "', '" + id + "', 'false', 'true')\"><i class=\"fa fa-cloud-upload\" aria-hidden=\"true\"></i> Đăng lên tất cả web</a><a href=\"javascript:;\" class=\"btn primary-btn h45-btn btn-black print-invoice-merged\" onclick=\"ShowUpProductToWeb('" + sku + "', '" + id + "', 'false', 'true')\"><i class=\"fa fa-refresh\" aria-hidden=\"true\"></i> Làm mới hình tất cả web</a></span></p></div></div>";
    showPopup(html, 8);

    HoldOn.open();

    for (var i = 0; i < web.length; i++) {
        upProductToWeb(web[i], sku, id, up, renew, i);
    }

    HoldOn.close();
}

function upProductToWeb(web, sku, id, up, renew, i) {
    $.ajax({
        type: "POST",
        url: "https://" + web + "/up-product",
        data: {
            sku: sku,
            systemid: id,
            up: up,
            renew: renew,
            key: '828327'
        },
        async: true,
        datatype: "json",
        beforeSend: function () {
            HoldOn.open();
            $(".content-upload-" + i).html("<span class='bg-yellow'>Đang xử lý</span>");
        },
        success: function (data) {
            HoldOn.close();

            if (data.success === "true") {
                var content = "";
                var button = "";
                if (data.content === "found") {
                    content = "<span class='bg-blue'>Tìm thấy sản phẩm</span>";
                    button = "<a href=\"javascript:;\" class=\"btn primary-btn h45-btn\" onclick=\"upProductToWeb('" + web + "', '" + sku + "', '" + id + "', 'true', 'false', '" + i + "')\"><i class=\"fa fa-upload\" aria-hidden=\"true\"></i> Up</a>";
                    button += "<a href=\"javascript:;\" class=\"btn primary-btn btn-black h45-btn print-invoice-merged\" onclick=\"upProductToWeb('" + web + "', '" + sku + "', '" + id + "', 'false', 'true', '" + i + "')\"><i class=\"fa fa-refresh\" aria-hidden=\"true\"></i> Làm mới hình</a>";
                }
                else if (data.content === "updone") {
                    content = "<span class='bg-green'>Up thành công</span>";
                    button = "<a href=\"javascript:;\" class=\"btn primary-btn btn-black h45-btn\" onclick=\"upProductToWeb('" + web + "', '" + sku + "', '" + id + "', 'false', 'true', '" + i + "')\"><i class=\"fa fa-refresh\" aria-hidden=\"true\"></i> Làm mới hình</a>";
                }
                else if (data.content === "renewdone") {
                    content = "<span class='bg-green'>Đăng web thành công</span>";
                }
                button += "<a href=\"https://" + web + "/?s=" + sku + "&post_type=product\" target=\"_blank\" class=\"btn primary-btn btn-black h45-btn print-invoice-merged\"><i class=\"fa fa-link\" aria-hidden=\"true\"></i> Xem</a>";
                button += "<a href=\"https://" + web + "/wp-admin/edit.php?s=" + sku + "&post_type=product\" target=\"_blank\" class=\"btn primary-btn btn-black h45-btn print-invoice-merged\"><i class=\"fa fa-pencil-square-o\" aria-hidden=\"true\"></i> Sửa</a>";

            }
            else {
                if (data.content === "uperror") {
                    content = "<span class='bg-red'>Lỗi khi up sản phẩm</span>";
                }
                else if (data.content === "renewerror") {
                    content = "<span class='bg-red'>Lỗi khi đăng lên web</span>";
                }
                else if (data.content === "notfound") {
                    content = "<span class='bg-blue'>Chưa tìm thấy sản phẩm</span>";
                    button = "<a href=\"javascript:;\" class=\"btn primary-btn h45-btn\" onclick=\"upProductToWeb('" + web + "', '" + sku + "', '" + id + "', 'false', 'true', '" + i + "')\"><i class=\"fa fa-cloud-upload\" aria-hidden=\"true\"></i> Đăng web</a>";
                }
            }

            web_item = "<div class='col-md-3'><p><i class=\"fa fa-arrow-right\" aria-hidden=\"true\"></i> " + web + "</p></div><div class='col-md-3'><p><span class='content-upload-" + i + "'>" + content + "</span></p></div><div class='col-md-6'><p>" + button + "</p></div>";

            if ($("div").hasClass("upload-" + i) === true) {
                $(".upload-" + i).html(web_item);
            }
            else {
                $(".web-list").append("<div class='row upload-" + i + "'>" + web_item + "</div>");
            }

        },
        error: function () {

            web_item = "<div class='col-md-3'><p><i class=\"fa fa-arrow-right\" aria-hidden=\"true\"></i> " + web + "</p></div><div class='col-md-9'><p><span class='bg-red'>Lỗi kết nối trang con</span></p></div>";

            if ($("div").hasClass("upload-" + i) === true) {
                $(".upload-" + i).html(web_item);
            }
            else {
                $(".web-list").append("<div class='row upload-" + i + "'>" + web_item + "</div>");
            }

        }
    });
}