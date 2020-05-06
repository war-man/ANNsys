var systemAPI = "/danh-sach-bai-viet-app.aspx"
var wpAPI = "/api/v1/wordpress/post/";
//var webList = ["ann.com.vn", "khohangsiann.com", "bosiquanao.net", "quanaogiaxuong.com", "bansithoitrang.net", "panpan.vn", "quanaoxuongmay.com", "annshop.vn"];
var webList = ["annshop.vn"];

function showPostSyncModal(postPublicID) {
    closePopup();

    var html = "";
    html += "<div class='row'><div class='col-md-12'><h2>Đồng bộ bài viết " + postPublicID + "</h2><br></div></div>";
    html += "<div class='row item-website'>";
    html += "    <div class='col-md-12' data-web='all' data-post-id='" + postPublicID + "'>";
    html += "       <span>";
    html += "        	<a href='javascript:;' class='btn primary-btn btn-green' onclick='createClonePost($(this))'><i class='fa fa-cloud-upload' aria-hidden='true'></i> Tạo clone tất cả</a>";
    html += "        	<a href='javascript:;' class='btn primary-btn btn-green' onclick='postPost($(this))'><i class='fa fa-cloud-upload' aria-hidden='true'></i> Đăng tất cả</a>";
    html += "        	<a href='javascript:;' class='btn primary-btn btn-blue' onclick='upTopProduct($(this))'><i class='fa fa-upload' aria-hidden='true'></i> Up top tất cả</a>";
    html += "        	<a href='javascript:;' class='btn primary-btn btn-black' onclick='renewProduct($(this))'><i class='fa fa-refresh' aria-hidden='true'></i> Làm mới tất cả</a>";
    html += "        	<a href='javascript:;' class='btn primary-btn btn-black' onclick='toggleProduct($(this), `hide`)'><i class='fa fa-refresh' aria-hidden='true'></i> Ẩn tất cả</a>";
    html += "        	<a href='javascript:;' class='btn primary-btn' onclick='toggleProduct($(this), `show`)'><i class='fa fa-refresh' aria-hidden='true'></i> Hiện tất cả</a>";
    html += "        	<a href='javascript:;' class='btn primary-btn btn-red' onclick='deleteProduct($(this), `show`)'><i class='fa fa-times' aria-hidden='true'></i> Xóa tất cả</a>";
    html += "       </span>";
    html += "    </div>";
    html += "</div>";
    html += "<div class='web-list'></div>";

    showPopup(html, 8);
    HoldOn.open();
    
    for (var i = 0; i < webList.length; i++) {

        var button = "";
        button += "<span class='btn-clone-not-found hide'>";
        button += "<a href='javascript:;' class='btn primary-btn btn-green' onclick='createClonePost($(this))'>Tạo clone</a>";
        button += "</span>";
        button += "<span class='btn-clone-had-found hide'>";
        button += "<a href='javascript:;' class='btn primary-btn btn-blue' onclick='editClonePost($(this))'>Sửa clone</a>";
        button += "<a href='javascript:;' class='btn primary-btn btn-green' onclick='postPost($(this))'>Đăng web</a>";
        button += "</span>";
        button += "<span class='btn-web-not-found hide'>";
        button += "<a href='javascript:;' class='btn primary-btn btn-blue' onclick='editClonePost($(this))'>Sửa clone</a>";
        button += "<a href='javascript:;' class='btn primary-btn btn-green' onclick='postPost($(this))'>Đăng web</a>";
        button += "</span>";
        button += "<span class='btn-web-had-found hide'>";
        button += "<a href='javascript:;' onclick='upTopProduct($(this))' class='btn primary-btn btn-blue'>Up top</a>";
        button += "<a href='javascript:;' onclick='renewProduct($(this))' class='btn primary-btn btn-black'>Làm mới</a>";
        button += "<a href='javascript:;' onclick='viewProduct($(this))' class='btn primary-btn btn-yellow'>Xem</a>";
        button += "<a href='javascript:;' onclick='editProduct($(this))' class='btn primary-btn btn-black'>Sửa</a>";
        button += "<a href='javascript:;' onclick='toggleProduct($(this), `hide`)' class='btn primary-btn btn-black'>Ẩn</a>";
        button += "<a href='javascript:;' onclick='toggleProduct($(this), `show`)' class='btn primary-btn'>Hiện</a>";
        button += "<a href='javascript:;' onclick='deleteProduct($(this), `show`)' class='btn primary-btn btn-red'>Xóa</a>";
        button += "</span>";

        var webItem = "";
        webItem += "<div class='col-md-3 item-name'>" + webList[i] + "</div>";
        webItem += "<div class='col-md-3 item-status'><span class='bg-yellow'>Đang lấy bài viết...</span></div>";
        webItem += "<div class='col-md-6 item-button'>" + button + "</div>";
        $(".web-list").append("<div class='row item-website' data-web='" + webList[i] + "' data-post-public-id='" + postPublicID + "' data-post-clone-id='' data-post-wordpress-id=''>" + webItem + "</div>");

        getClone(postPublicID, webList[i]);
    }
}

function getClone(postPublicID, web) {
    $.ajax({
        type: "POST",
        url: systemAPI + "/getClone",
        data: "{postPublicID: " + postPublicID + ", web: '" + web + "'}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (msg) {
            HoldOn.close();
            if (msg.d != "null") {
                let data = JSON.parse(msg.d);

                $("*[data-web='" + web + "']").attr("data-post-clone-id", data.ID);

                if (data.PostWebID > 0) {
                    $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-green'>Đã đăng lên web</span>");
                    $("*[data-web='" + web + "']").find(".item-button").find(".btn-web-had-found").removeClass("hide");
                    $("*[data-web='" + web + "']").attr("data-post-wordpress-id", data.PostWebID);
                }
                else {
                    $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-blue'>Tìm thấy clone trên hệ thống</span>");
                    $("*[data-web='" + web + "']").find(".item-button").find(".btn-clone-had-found").removeClass("hide");
                }
            }
            else {
                $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-blue'>Không tìm thấy clone trên hệ thống</span>");
                $("*[data-web='" + web + "']").find(".item-button").find(".btn-clone-not-found").removeClass("hide");
            }
        },
        error: function () {
            HoldOn.close();
            alert("Lỗi");
        }
    });
}

function createClone(obj) {
    let web = obj.closest(".item-website").attr("data-web");
    let postPublicID = obj.closest(".item-website").attr("data-post-public-id");

    if (web == "all") {
        for (var i = 0; i < webList.length; i++) {
            ajaxCreateClone(webList[i], productID);
        }
    }
    else {
        ajaxCreateClone(web, postPublicID);
    }
}

function ajaxCreateClone(web, postPublicID) {
    $.ajax({
        type: "POST",
        url: systemAPI + "/createClone",
        data: "{web: '" + web + "', postPublicID: " + postPublicID + "}",
        async: true,
        datatype: "json",
        contentType: "application/json; charset=utf-8",
        beforeSend: function () {
            HoldOn.open();
            $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-yellow'>Đang tạo clone...</span>");
            $("*[data-web='" + web + "']").find(".item-button").find(".btn-clone-not-found").addClass("hide");
            $("*[data-web='" + web + "']").find(".item-button").find(".btn-clone-had-found").addClass("hide");
        },
        success: function (msg) {
            HoldOn.close();

            if (msg.d != "null") {
                let data = JSON.parse(msg.d);
                if (data.ID > 0) {
                    $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-green'>Tạo clone thành công</span>");
                    $("*[data-web='" + web + "']").find(".item-button").find(".btn-clone-not-found").addClass("hide");
                    $("*[data-web='" + web + "']").find(".item-button").find(".btn-clone-had-found").removeClass("hide");
                    $("*[data-web='" + web + "']").attr("data-post-clone-id", data.ID);
                }
                else {
                    $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-red'>Tạo clone thất bại</span>");
                    $("*[data-web='" + web + "']").find(".item-button").find(".btn-clone-not-found").removeClass("hide");
                    $("*[data-web='" + web + "']").find(".item-button").find(".btn-clone-had-found").addClass("hide");
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

function postWordpress(obj) {
    let web = obj.closest(".item-website").attr("data-web");
    let postCloneID = obj.closest(".item-website").attr("data-post-clone-id");

    if (web == "all") {
        for (var i = 0; i < webList.length; i++) {
            ajaxPostWordpress(webList[i], postCloneID);
        }
    }
    else {
        ajaxPostWordpress(web, postCloneID);
    }
}

function ajaxPostWordpress(web, postCloneID) {
    $.ajax({
        type: "POST",
        url: wpAPI + postCloneID,
        headers: {
            'domain': web,
        },
        async: true,
        datatype: "json",
        beforeSend: function () {
            HoldOn.open();
            $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-yellow'>Đang đăng lên web...</span>");
            $("*[data-web='" + web + "']").find(".item-button").find(".btn-clone-not-found").addClass("hide");
            $("*[data-web='" + web + "']").find(".item-button").find(".btn-clone-had-found").addClass("hide");
            $("*[data-web='" + web + "']").find(".item-button").find(".btn-web-not-found").addClass("hide");
            $("*[data-web='" + web + "']").find(".item-button").find(".btn-web-had-found").addClass("hide");
        },
        success: function (data, textStatus, xhr) {
            HoldOn.close();

            // Thành công
            if (xhr.status === 200) {
                if (data.id > 0) {
                    $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-green'>Đăng web thành công</span>");
                    $("*[data-web='" + web + "']").find(".item-button").find(".btn-web-not-found").addClass("hide");
                    $("*[data-web='" + web + "']").find(".item-button").find(".btn-web-had-found").removeClass("hide");
                    $("*[data-web='" + web + "']").attr("data-post-wordpress-id", data.id);
                }
                else {
                    $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-red'>Đăng web thất bại</span>");
                    $("*[data-web='" + web + "']").find(".item-button").find(".btn-web-not-found").removeClass("hide");
                    $("*[data-web='" + web + "']").find(".item-button").find(".btn-web-had-found").addClass("hide");
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
        url: API + productID + "/uptop",
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
        url: API + productID + "/renew",
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
                    $("*[data-web='" + web + "']").attr("data-web-product-id", data.id);
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
        url: API + productID + "/" + toggle,
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

function viewProduct(obj) {
    let web = obj.parent().parent().attr("data-web");
    let ID = obj.parent().parent().attr("data-web-product-id");
    let URL = "http://" + web + "/?p=" + ID;
    window.open(URL, '_blank');
}

function editProduct(obj) {
    let web = obj.parent().parent().attr("data-web");
    let ID = obj.parent().parent().attr("data-web-product-id");
    let URL = "http://" + web + "/wp-admin/post.php?post=" + ID + "&action=edit";
    window.open(URL, '_blank');
}

function deleteProduct(obj, toggle) {
    let web = obj.parent().parent().attr("data-web");
    let productID = obj.parent().parent().attr("data-product-id");

    if (web == "all") {
        for (var i = 0; i < webList.length; i++) {
            ajaxDeleteProduct(webList[i], productID);
        }
    }
    else {
        ajaxDeleteProduct(web, productID);
    }
}

function ajaxDeleteProduct(web, productID) {

    $.ajax({
        type: "DELETE",
        url: API + productID,
        headers: {
            'domain': web,
        },
        async: true,
        datatype: "json",
        beforeSend: function () {
            HoldOn.open();

            $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-yellow'>Đang xóa sản phẩm</span>");
            $("*[data-web='" + web + "']").find(".item-button").find(".btn-not-found").addClass("hide");
            $("*[data-web='" + web + "']").find(".item-button").find(".btn-had-found").addClass("hide");
        },
        success: function (data, textStatus, xhr) {
            HoldOn.close();

            // Thành công
            if (xhr.status === 200) {
                if (data.id > 0) {
                    $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-green'>Xóa sản phẩm thành công</span>");
                    $("*[data-web='" + web + "']").find(".item-button").find(".btn-not-found").removeClass("hide");
                    $("*[data-web='" + web + "']").find(".item-button").find(".btn-had-found").addClass("hide");
                    $("*[data-web='" + web + "']").attr("data-web-product-id", "");
                }
                else {
                    $("*[data-web='" + web + "']").find(".item-status").html("<span class='bg-red'>Xóa sản phẩm thất bại</span>");
                    $("*[data-web='" + web + "']").find(".item-button").find(".btn-not-found").addClass("hide");
                    $("*[data-web='" + web + "']").find(".item-button").find(".btn-had-found").removeClass("hide");
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