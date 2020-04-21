var webList = ["quanaogiaxuong.com", "bansithoitrang.net", "panpan.vn", "quanaoxuongmay.com", "annshop.vn"];

function showProductSyncModal(productSKU, productID, categoryID) {
    closePopup();

    var html = "";
    html += "<div class='row'><div class='col-md-12'><h2>Đồng bộ sản phẩm " + productSKU + "</h2><br></div></div>";
    html += "<div class='row item-website'>";
    html += "    <div class='col-md-12' data-web='all' data-product-sku='" + productSKU + "' data-product-id='" + productID + "'>";
    html += "       <span>";
    html += "        	<a href='javascript:;' class='btn primary-btn' onclick='postProduct($(this))'><i class='fa fa-cloud-upload' aria-hidden='true'></i> Đăng tất cả</a>";
    html += "        	<a href='javascript:;' class='btn primary-btn' onclick='upTopProduct($(this))'><i class='fa fa-upload' aria-hidden='true'></i> Up top tất cả</a>";
    html += "        	<a href='javascript:;' class='btn primary-btn btn-black' onclick='renewProduct($(this))'><i class='fa fa-refresh' aria-hidden='true'></i> Làm mới tất cả</a>";
    html += "        	<a href='javascript:;' class='btn primary-btn btn-black' onclick='toggleProduct($(this), `hide`)'><i class='fa fa-refresh' aria-hidden='true'></i> Ẩn tất cả</a>";
    html += "        	<a href='javascript:;' class='btn primary-btn' onclick='toggleProduct($(this), `show`)'><i class='fa fa-refresh' aria-hidden='true'></i> Hiện tất cả</a>";
    html += "       </span>";
    html += "    </div>";
    html += "</div>";
    html += "<div class='web-list'></div>";

    showPopup(html, 8);
    HoldOn.open();
    
    for (var i = 0; i < webList.length; i++) {

        var button = "";
        button += "<span class='btn-not-found hide'>";
        button += "<a href='javascript:;' class='btn primary-btn' onclick='postProduct($(this))'>Đăng web</a>";
        button += "</span>";
        button += "<span class='btn-had-found hide'>";
        button += "<a href='javascript:;' onclick='upTopProduct($(this))' class='btn primary-btn'>Up top</a>";
        button += "<a href='javascript:;' onclick='renewProduct($(this))' class='btn primary-btn btn-black'>Làm mới</a>";
        button += "<a href='https://" + webList[i] + "/?s=" + productSKU + "&post_type=product' target='_blank' class='btn primary-btn btn-black'>Xem</a>";
        button += "<a href='https://" + webList[i] + "/wp-admin/edit.php?s=" + productSKU + "&post_type=product' target='_blank' class='btn primary-btn btn-black'>Sửa</a>";
        button += "<a href='javascript:;' onclick='toggleProduct($(this), `hide`)' class='btn primary-btn btn-black'>Ẩn</a>";
        button += "<a href='javascript:;' onclick='toggleProduct($(this), `show`)' class='btn primary-btn'>Hiện</a>";
        button += "</span>";

        var webItem = "";
        webItem += "<div class='col-md-3 item-name'>" + webList[i] + "</div>";
        webItem += "<div class='col-md-3 item-status'><span class='bg-yellow'>Đang kết nối web...</span></div>";
        webItem += "<div class='col-md-6 item-button' data-web='" + webList[i] + "' data-product-sku='" + productSKU + "' data-product-id='" + productID + "'>" + button + "</div>";
        $(".web-list").append("<div class='row item-website' data-web='" + webList[i] + "'>" + webItem + "</div>");

        getProduct(webList[i], productID);
    }
}

function getProduct(web, productID) {
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

            if (data.length > 0) {
                $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-blue'>Tìm thấy sản phẩm</span>");
                $("*[data-web='" + web + "']").find(".item-button").find(".btn-had-found").removeClass("hide");
            }
            else {
                $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-blue'>Chưa tìm thấy sản phẩm</span>");
                $("*[data-web='" + web + "']").find(".item-button").find(".btn-not-found").removeClass("hide");
            }
        },
        error: function () {
            HoldOn.close();
            $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-red'>Lỗi kết nối trang con</span>");
        }
    });
}

function postProduct(obj) {
    let web = obj.parent().parent().attr("data-web");
    let productID = obj.parent().parent().attr("data-product-id");

    if (web == "all") {
        for (var i = 0; i < webList.length; i++) {
            ajaxPostProduct(webList[i], productID);
        }
    }
    else {
        ajaxPostProduct(web, productID);
    }
}

function ajaxPostProduct(web, productID) {
    $.ajax({
        type: "POST",
        url: "http://ann-product-sync.com/api/v1/product/" + productID,
        headers: {
            'domain': web,
        },
        async: true,
        datatype: "json",
        beforeSend: function () {
            HoldOn.open();
            $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-yellow'>Đang đăng lên web...</span>");
            $("*[data-web='" + web + "']").find(".item-button").find(".btn-not-found").addClass("hide");
            $("*[data-web='" + web + "']").find(".item-button").find(".btn-had-found").addClass("hide");
        },
        success: function (data, textStatus, xhr) {
            HoldOn.close();

            // Thành công
            if (xhr.status === 200) {
                if (data.id > 0) {
                    $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-green'>Đăng web thành công</span>");
                    $("*[data-web='" + web + "']").find(".item-button").find(".btn-not-found").addClass("hide");
                    $("*[data-web='" + web + "']").find(".item-button").find(".btn-had-found").removeClass("hide");
                }
                else {
                    $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-red'>Đăng web thất bại</span>");
                    $("*[data-web='" + web + "']").find(".item-button").find(".btn-not-found").removeClass("hide");
                    $("*[data-web='" + web + "']").find(".item-button").find(".btn-had-found").addClass("hide");
                }
            }
            else {
                $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-red'>Lỗi kết nối trang con</span>");
            }
        },
        error: function (xhr, textStatus, error) {
            HoldOn.close();

            if (xhr.status === 500) {
                let data = xhr.responseJSON;
                $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-red'>" + data.message + "</span>");
            }
            else {
                $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-red'>Lỗi kết nối trang con</span>");
            }
        }
    });
}

function upTopProduct(obj) {
    let web = obj.parent().parent().attr("data-web");
    let productID = obj.parent().parent().attr("data-product-id");

    if (web == "all") {
        for (var i = 0; i < webList.length; i++) {
            ajaxUpTopProduct(webList[i], productID);
        }
    }
    else {
        ajaxUpTopProduct(web, productID);
    }
}

function ajaxUpTopProduct(web, productID) {
    $.ajax({
        type: "POST",
        url: "http://ann-product-sync.com/api/v1/product/" + productID + "/uptop",
        headers: {
            'domain': web,
        },
        async: true,
        datatype: "json",
        beforeSend: function () {
            HoldOn.open();
            $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-yellow'>Đang up lên đầu web...</span>");
            $("*[data-web='" + web + "']").find(".item-button").find(".btn-not-found").addClass("hide");
            $("*[data-web='" + web + "']").find(".item-button").find(".btn-had-found").addClass("hide");
        },
        success: function (data, textStatus, xhr) {
            HoldOn.close();

            // Thành công
            if (xhr.status === 200) {
                if (data.id > 0) {
                    $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-green'>Up lên đầu web thành công</span>");
                    $("*[data-web='" + web + "']").find(".item-button").find(".btn-not-found").addClass("hide");
                    $("*[data-web='" + web + "']").find(".item-button").find(".btn-had-found").removeClass("hide");
                }
                else {
                    $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-red'>Up lên đầu web thất bại</span>");
                    $("*[data-web='" + web + "']").find(".item-button").find(".btn-not-found").removeClass("hide");
                    $("*[data-web='" + web + "']").find(".item-button").find(".btn-had-found").addClass("hide");
                }
            }
            else {
                $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-red'>Lỗi kết nối trang con</span>");
            }
        },
        error: function (xhr, textStatus, error) {
            HoldOn.close();
            let data = xhr.responseJSON;
            if (xhr.status === 500) {
                $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-red'>" + data.message + "</span>");
            }
            else if (xhr.status === 400) {
                $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-red'>" + data.message + "</span>");
            }
            else {
                $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-red'>Lỗi kết nối trang con</span>");
            }
        }
    });
}

function renewProduct(obj) {
    let web = obj.parent().parent().attr("data-web");
    let productID = obj.parent().parent().attr("data-product-id");

    if (web == "all") {
        for (var i = 0; i < webList.length; i++) {
            ajaxRenewProductt(webList[i], productID);
        }
    }
    else {
        ajaxRenewProductt(web, productID);
    }
}

function ajaxRenewProductt(web, productID) {
    $.ajax({
        type: "POST",
        url: "http://ann-product-sync.com/api/v1/product/" + productID + "/renew",
        headers: {
            'domain': web,
        },
        async: true,
        datatype: "json",
        beforeSend: function () {
            HoldOn.open();
            $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-yellow'>Đang làm mới sản phẩm...</span>");
            $("*[data-web='" + web + "']").find(".item-button").find(".btn-not-found").addClass("hide");
            $("*[data-web='" + web + "']").find(".item-button").find(".btn-had-found").addClass("hide");
        },
        success: function (data, textStatus, xhr) {
            HoldOn.close();

            // Thành công
            if (xhr.status === 200) {
                if (data.id > 0) {
                    $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-green'>Làm mới sản phẩm thành công</span>");
                    $("*[data-web='" + web + "']").find(".item-button").find(".btn-not-found").addClass("hide");
                    $("*[data-web='" + web + "']").find(".item-button").find(".btn-had-found").removeClass("hide");
                }
                else {
                    $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-red'>Làm mới sản phẩm thất bại</span>");
                    $("*[data-web='" + web + "']").find(".item-button").find(".btn-not-found").removeClass("hide");
                    $("*[data-web='" + web + "']").find(".item-button").find(".btn-had-found").addClass("hide");
                }
            }
            else {
                $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-red'>Lỗi kết nối trang con</span>");
            }
        },
        error: function (xhr, textStatus, error) {
            HoldOn.close();
            let data = xhr.responseJSON;
            if (xhr.status === 500) {
                $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-red'>" + data.message + "</span>");
            }
            else if (xhr.status === 400) {
                $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-red'>" + data.message + "</span>");
            }
            else {
                $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-red'>Lỗi kết nối trang con</span>");
            }
        }
    });
}

function toggleProduct(obj, toggle) {
    let web = obj.parent().parent().attr("data-web");
    let productID = obj.parent().parent().attr("data-product-id");

    if (web == "all") {
        for (var i = 0; i < webList.length; i++) {
            ajaxToggleProduct(webList[i], productID, toggle);
        }
    }
    else {
        ajaxToggleProduct(web, productID, toggle);
    }
}

function ajaxToggleProduct(web, productID, toggle) {
    let status = "ẩn";
    if (toggle === "show") {
        status = "hiện";
    }

    $.ajax({
        type: "POST",
        url: "http://ann-product-sync.com/api/v1/product/" + productID + "/" + toggle,
        headers: {
            'domain': web,
        },
        async: true,
        datatype: "json",
        beforeSend: function () {
            HoldOn.open();
            
            $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-yellow'>Đang " + status + " sản phẩm</span>");
            $("*[data-web='" + web + "']").find(".item-button").find(".btn-not-found").addClass("hide");
            $("*[data-web='" + web + "']").find(".item-button").find(".btn-had-found").addClass("hide");
        },
        success: function (data, textStatus, xhr) {
            HoldOn.close();

            // Thành công
            if (xhr.status === 200) {
                if (data.id > 0) {
                    $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-green'>Đã " + status + " sản phẩm thành công</span>");
                    $("*[data-web='" + web + "']").find(".item-button").find(".btn-not-found").addClass("hide");
                    $("*[data-web='" + web + "']").find(".item-button").find(".btn-had-found").removeClass("hide");
                }
                else {
                    $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-red'>Đã " + status + " sản phẩm thất bại</span>");
                    $("*[data-web='" + web + "']").find(".item-button").find(".btn-not-found").removeClass("hide");
                    $("*[data-web='" + web + "']").find(".item-button").find(".btn-had-found").addClass("hide");
                }
            }
            else {
                $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-red'>Lỗi kết nối trang con</span>");
            }
        },
        error: function (xhr, textStatus, error) {
            HoldOn.close();
            let data = xhr.responseJSON;
            if (xhr.status === 500) {
                $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-red'>" + data.message + "</span>");
            }
            else if (xhr.status === 400) {
                $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-red'>" + data.message + "</span>");
            }
            else {
                $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-red'>Lỗi kết nối trang con</span>");
            }
        }
    });
}
