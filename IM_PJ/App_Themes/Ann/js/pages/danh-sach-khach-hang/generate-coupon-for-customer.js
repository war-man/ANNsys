function wait(ms) {
    var start = new Date().getTime();
    var end = start;
    while (end < start + ms) {
        end = new Date().getTime();
    }
}
function generateCouponG25(customerName, customerID) {
    swal({
        title: "Thông báo:",
        text: "Kiểm tra mã giảm giá <strong>G25</strong> cho khách <strong>" + customerName + "</strong>?",
        type: "warning",
        showCancelButton: true,
        showCloseButton: false,
        confirmButtonText: "OK kiểm tra!",
        cancelButtonText: "Để xem lại!",
        closeOnConfirm: true,
        html: true,
    }, function (confirm) {
        if (confirm) {
            sweetAlert.close();
            checkCouponG25(customerID);
        }
    });
}

function checkCouponG25(customerID, couponCode) {
    sweetAlert.close();
    $.ajax({
        type: "POST",
        url: "/danh-sach-khach-hang.aspx/checkCouponG25",
        data: "{customerID: " + customerID + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            sweetAlert.close();
            $("body").removeClass("stop-scrolling");
        },
        success: function (msg) {
            if (msg.d === "customerNotFound") {
                alert("Không tìm thấy khách hàng!");
            }
            else if (msg.d === "couponNotFound") {
                alert("Không tìm thấy mã giảm giá!");
            }
            else if (msg.d === "couponNotActived") {
                swal({
                    title: "Thông báo:",
                    text: "Mã giảm giá chưa kích hoạt!",
                    type: "error",
                    showCloseButton: true,
                    html: true,
                });
            }
            else if (msg.d === "userNotFound") {
                swal({
                    title: "Thông báo:",
                    text: "Số điện thoại này chưa đăng ký app!<br>Hãy hướng dẫn khách tải app và đăng ký!",
                    type: "error",
                    showCloseButton: true,
                    html: true,
                });
            }
            else if (msg.d === "activeCouponExists") {
                swal({
                    title: "Thông báo:",
                    text: "Đang có sẵn mã giảm giá G25 cho khách!<br>Hãy nhập mã vào đơn hàng!",
                    type: "success",
                    showCloseButton: true,
                    html: true,
                });
            }
            else if (msg.d === "noCouponGeneratedYet") {
                swal({
                    title: "Xác nhận:",
                    text: "<strong>Khách đủ điều kiện sử dụng mã giảm giá G25!</strong><br><br>Bạn muốn tạo mã cho khách?",
                    type: "warning",
                    showCancelButton: true,
                    showCloseButton: false,
                    confirmButtonText: "OK tạo luôn!",
                    cancelButtonText: "Để em xem lại!",
                    closeOnConfirm: true,
                    html: true,
                }, function (confirm) {
                    if (confirm) {
                        sweetAlert.close();
                        ajaxGenerateCouponG25(customerID);
                    }
                });
            }
            else if (msg.d === "couponUsageLimitExceeded") {
                swal({
                    title: "Thông báo:",
                    text: "<strong>Khách đã sử dụng hết số lần mã giảm giá G25!</strong>",
                    type: "warning",
                    showCloseButton: true,
                    html: true,
                });
            }
            else {
                alert("Lỗi");
            }
        }
    });
}

function ajaxGenerateCouponG25(customerID) {
    sweetAlert.close();
    wait(500);
    $.ajax({
        type: "POST",
        url: "/danh-sach-khach-hang.aspx/generateCouponG25",
        data: "{customerID: " + customerID + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d === "true") {
                swal({
                    title: "Thông báo:",
                    text: "Đã tạo mã giảm giá G25 cho khách!",
                    type: "success",
                    showCancelButton: false,
                    showCloseButton: true,
                    confirmButtonText: "OK!",
                    html: true,
                });
            }
            else {
                alert("Lỗi");
            }
        }
    });
}

function generateCouponG15(customerName, customerID) {
    swal({
        title: "Xác nhận:",
        text: "Tạo mã giảm giá <strong>G15</strong> cho khách <strong>" + customerName + "</strong>?",
        type: "warning",
        showCancelButton: true,
        showCloseButton: false,
        confirmButtonText: "OK tạo ngay!",
        cancelButtonText: "Để xem lại!",
        html: true,
    }, function (confirm) {
        if (confirm) {
            sweetAlert.close();
            ajaxGenerateCouponG15(customerID, true);
        }
    });
}

function ajaxGenerateCouponG15(customerID, checkUser) {
    $.ajax({
        type: "POST",
        url: "/danh-sach-khach-hang.aspx/generateCouponG15",
        data: "{customerID: " + customerID + ", checkUser: " + checkUser + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            sweetAlert.close();
        },
        success: function (msg) {
            if (msg.d === "true") {
                swal({
                    title: "Thông báo:",
                    text: "Đã tạo mã giảm giá G15 cho khách!",
                    type: "success",
                    showCancelButton: false,
                    showCloseButton: true,
                    confirmButtonText: "OK!",
                    html: true,
                });
            }
            else if (msg.d === "customerNotFound") {
                alert("Không tìm thấy khách hàng!");
            }
            else if (msg.d === "couponNotFound") {
                alert("Không tìm thấy mã giảm giá!");
            }
            else if (msg.d === "couponNotActived") {
                alert("Mã giảm giá chưa kích hoạt!");
            }
            else if (msg.d === "userNotFound") {
                swal({
                    title: "Xác nhận:",
                    text: "<strong>Số điện thoại này chưa đăng ký app!</strong><br><br>Vẫn tạo mã giảm giá cho khách?",
                    type: "warning",
                    showCancelButton: true,
                    showCloseButton: false,
                    confirmButtonText: "OK tạo luôn!",
                    cancelButtonText: "Để em nói với khách!",
                    html: true,
                }, function (confirm) {
                    if (confirm) {
                        sweetAlert.close();
                        ajaxGenerateCouponG15(customerID, false);
                    }
                });
            }
            else {
                alert("Lỗi");
            }
        }
    });
    sweetAlert.close();
}