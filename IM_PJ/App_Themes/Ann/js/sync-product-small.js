function ShowUpProductToWeb(sku, id, up, renew, visibility) {
    var web = ["ann.com.vn", "khohangsiann.com", "bosiquanao.net", "quanaogiaxuong.com", "bansithoitrang.net", "quanaoxuongmay.com", "annshop.vn"];
    if ($(".hidden-" + id).hasClass("product-hidden")) {
        for (var i = 0; i < web.length; i++) {
            upProductToWeb(web[i], sku, id, up, renew, i, "visible");
        }
    }
    else {
        for (var i = 0; i < web.length; i++) {
            upProductToWeb(web[i], sku, id, up, renew, i, visibility);
        }
    }
    
}

function upProductToWeb(web, sku, id, up, renew, i, visibility) {
    $.ajax({
        type: "POST",
        url: "https://" + web + "/up-product",
        data: {
            sku: sku,
            systemid: id,
            up: up,
            renew: renew,
            visibility: visibility,
            key: '828327'
        },
        async: true,
        datatype: "json",
        beforeSend: function () {
        },
        success: function (data) {
            if (data.success === "true") {
                if(visibility == "hidden") {
                    $(".hidden-" + id).html("<i class=\"fa fa-check\" aria-hidden=\"true\"></i> Đã ẩn");
                    $(".hidden-" +id).addClass("product-hidden");
                }
                else {
                    $(".hidden-" + id).html("<i class=\"fa fa-check\" aria-hidden=\"true\"></i> Đã hiện");
                    $(".hidden-" +id).removeClass("product-hidden");
                }
            }
            else {
                if (data.content === "notfound") {
                    $(".hidden-" + id).html("<i class=\"fa fa-exclamation-triangle\" aria-hidden=\"true\"></i> Notfound");
                }
            }
        },
        error: function () {

        }
    });
}