function showProductSyncModal(productSKU, productID, categoryID) {
    var web = ["pqstore.vn", "quanaogiaxuong.com", "annshop.vn"];
    closePopup();

    var html = "<div class='row'><div class='col-md-12'><h2>Đồng bộ sản phẩm " + productSKU + "</h2><br></div></div>";
    html += "<div class='row'>";
    html += "    <div class='col-md-12' data-web='all' data-product-sku='" + productSKU + "' data-product-id='" + productID + "'>";
    html += "        	<a href='javascript:;' class='btn primary-btn h45-btn' onclick='upProduct($(this))'><i class='fa fa-upload' aria-hidden='true'></i> Up tất cả</a>";
    html += "        	<a href='javascript:;' class='btn primary-btn h45-btn btn-black print-invoice-merged' onclick='copyProduct($(this))'><i class='fa fa-cloud-upload' aria-hidden='true'></i> Đăng lên tất cả web</a>";
    html += "        	<a href='javascript:;' class='btn primary-btn h45-btn btn-black print-invoice-merged' onclick='renewProduct($(this))'><i class='fa fa-refresh' aria-hidden='true'></i> Làm mới sản phẩm tất cả web</a>";
    html += "        	<a href='javascript:;' class='btn primary-btn h45-btn btn-black print-invoice-merged' onclick='toggleProduct($(this), `hide`)'><i class='fa fa-refresh' aria-hidden='true'></i> Ẩn tất cả</a>";
    html += "        	<a href='javascript:;' class='btn primary-btn h45-btn btn-black print-invoice-merged' onclick='toggleProduct($(this), `show`)'><i class='fa fa-refresh' aria-hidden='true'></i> Hiện tất cả</a>";
    html += "    </div>";
    html += "</div>";
    html += "<div class='web-list'></div>";

    showPopup(html, 9);
    HoldOn.open();
    
    for (var i = 0; i < web.length; i++) {
        checkProduct(web[i], productID, productSKU);
    }
}

function checkProduct(web, productID, productSKU) {
    $.ajax({
        type: "GET",
        url: "http://ann-product-sync.com/api/v1/product/" + productID,
        headers: {
            'domain': web,
        },
        async: true,
        datatype: "json",
        success: function (data) {
            HoldOn.close();

            var content = "";
            var button = "";

            if (data.length > 0) {
                content = "<span class='bg-blue'>Tìm thấy sản phẩm</span>";
                button = "<a href='javascript:;' onclick='upProduct()' class='btn primary-btn h45-btn'><i class='fa fa-upload' aria-hidden='true'></i> Up</a>";
                button += "<a href='javascript:;' onclick='renewProduct()' class='btn primary-btn btn-black h45-btn print-invoice-merged'><i class='fa fa-refresh' aria-hidden='true'></i> Làm mới</a>";
                button += "<a href='https://" + web + "/?s=" + productSKU + "&post_type=product' target='_blank' class='btn primary-btn btn-black h45-btn print-invoice-merged'><i class='fa fa-link' aria-hidden='true'></i> Xem</a>";
                button += "<a href='https://" + web + "/wp-admin/edit.php?s=" + productSKU + "&post_type=product' target='_blank' class='btn primary-btn btn-black h45-btn print-invoice-merged'><i class='fa fa-pencil-square-o' aria-hidden='true'></i> Sửa</a>";
                button += "<a href='javascript:;' onclick='toggleProduct(`hide`)' class='btn primary-btn h45-btn print-invoice-merged'><i class='fa fa-pencil-square-o' aria-hidden='true'></i> Ẩn</a>";
                button += "<a href='javascript:;' onclick='toggleProduct(`show`)' class='btn primary-btn h45-btn print-invoice-merged'><i class='fa fa-pencil-square-o' aria-hidden='true'></i> Hiện</a>";
            }
            else {
                content = "<span class='bg-blue'>Chưa tìm thấy sản phẩm</span>";
                button = "<a href='javascript:;' class='btn primary-btn h45-btn' onclick='copyProduct()'><i class='fa fa-cloud-upload' aria-hidden='true'></i> Đăng web</a>";
            }

            webItem = "<div class='col-md-3'>" + web + "</div>";
            webItem += "<div class='col-md-3'>" + content + "</div>";
            webItem += "<div class='col-md-6' data-web='" + web + "' data-product-sku='" + productSKU + "' data-product-id='" + productID + "'>" + button + "</div>";

            $(".web-list").append("<div class='row' data-web='" + web + "'>" + webItem + "</div>");

        },
        error: function () {

            webItem = "<div class='col-md-3'>" + web + "</div>";
            webItem += "<div class='col-md-9'><span class='bg-red'>Lỗi kết nối trang con</span></div>";

            $(".web-list").append("<div class='row' data-web='" + web + "'>" + webItem + "</div>");
        }
    });
}

function copyProduct(obj) {
    let web = obj.parent().attr("data-web");
    let productID = obj.parent().attr("data-product-id");
    let productSKU = obj.parent().attr("data-product-sku");

    if (web == "all") {
        web = ["pqstore.vn", "quanaogiaxuong.com", "annshop.vn"];
        for (var i = 0; i < web.length; i++) {
            ajaxCopyProduct(web[i], productID, productSKU);
        }
    }
    else {
        ajaxCopyProduct(web, productID, productSKU);
    }
}

function ajaxCopyProduct(web, productID, productSKU) {
    $.ajax({
        type: "POST",
        url: "http://ann-product-sync.com/api/v1/product/" + productID,
        headers: {
            'domain': web,
        },
        async: true,
        datatype: "json",
        success: function (data, textStatus, xhr) {
            HoldOn.close();

            // Thành công
            if (xhr.status === 200) {

            } else if (xhr.status === 400) {

            }

            var content = "";
            var button = "";


            if (data.length > 0) {
                content = "<span class='bg-green'>Đăng web thành công</span>";
                button = "<a href='javascript:;' onclick='upProduct()' class='btn primary-btn h45-btn'><i class='fa fa-upload' aria-hidden='true'></i> Up</a>";
                button += "<a href='javascript:;' onclick='renewProduct()' class='btn primary-btn btn-black h45-btn print-invoice-merged'><i class='fa fa-refresh' aria-hidden='true'></i> Làm mới</a>";
                button += "<a href='https://" + web + "/?s=" + productSKU + "&post_type=product' target='_blank' class='btn primary-btn btn-black h45-btn print-invoice-merged'><i class='fa fa-link' aria-hidden='true'></i> Xem</a>";
                button += "<a href='https://" + web + "/wp-admin/edit.php?s=" + productSKU + "&post_type=product' target='_blank' class='btn primary-btn btn-black h45-btn print-invoice-merged'><i class='fa fa-pencil-square-o' aria-hidden='true'></i> Sửa</a>";
                button += "<a href='javascript:;' onclick='toggleProduct(`hide`)' class='btn primary-btn h45-btn print-invoice-merged'><i class='fa fa-pencil-square-o' aria-hidden='true'></i> Ẩn</a>";
                button += "<a href='javascript:;' onclick='toggleProduct(`show`)' class='btn primary-btn h45-btn print-invoice-merged'><i class='fa fa-pencil-square-o' aria-hidden='true'></i> Hiện</a>";
            }
            else {
                content = "<span class='bg-red'>Đăng web thất bại</span>";
                button = "<a href='javascript:;' class='btn primary-btn h45-btn' onclick='copyProduct()'><i class='fa fa-cloud-upload' aria-hidden='true'></i> Đăng web</a>";
            }

            webItem = "<div class='col-md-3'>" + web + "</div>";
            webItem += "<div class='col-md-3'>" + content + "</div>";
            webItem += "<div class='col-md-6' data-web='" + web + "' data-product-sku='" + productSKU + "' data-product-id='" + productID + "'>" + button + "</div>";

            $(".web-list").append("<div class='row' data-web='" + web + "'>" + webItem + "</div>");

        },
        error: function () {

            webItem = "<div class='col-md-3'><i class='fa fa-arrow-right' aria-hidden='true'></i> " + web + "</div>";
            webItem += "<div class='col-md-9'><span class='bg-red'>Lỗi kết nối trang con</span></div>";

            $(".web-list").append("<div class='row' data-web='" + web + "'>" + webItem + "</div>");
        }
    });
}