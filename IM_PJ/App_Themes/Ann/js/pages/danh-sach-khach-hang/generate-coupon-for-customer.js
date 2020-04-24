function generateCouponG20(customerName, customerID) {
    swal({
        title: "Thông báo:",
        text: "Kiểm tra mã giảm giá <strong>G20</strong> cho khách <strong>" + customerName + "</strong>?",
        type: "warning",
        showCancelButton: true,
        showCloseButton: false,
        confirmButtonText: "OK kiểm tra!",
        cancelButtonText: "Để xem lại!",
        html: true,
    }, function (confirm) {
        if (confirm) {
            sweetAlert.close();
            checkCouponG20(customerID);
        }
    });
}

function checkCouponG20(customerID, couponCode) {
    $.ajax({
        type: "POST",
        url: "/danh-sach-khach-hang.aspx/checkCouponG20",
        data: "{customerID: " + customerID + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d === "customerNotFound") {
                alert("Không tìm thấy khách hàng!");
            }
            else if (msg.d === "couponNotFound") {
                alert("Không tìm thấy mã giảm giá!");
            }
            else if (msg.d === "couponNotActived") {
                alert("Mã giảm giá chưa kích hoạt!");
            }
            else if (msg.d === "userNotFound") {
                alert("Số điện thoại này chưa đăng ký app!");
            }
            else if (msg.d === "activeCouponExists") {
                alert("Đang có sẵn mã giảm giá cho khách! Hãy nhập mã vào đơn hàng!");
            }
            else if (msg.d === "noCouponGeneratedYet") {
                swal({
                    title: "Xác nhận:",
                    text: "<strong>Khách đủ điều kiện sử dụng mã giảm giá G20!</strong><br><br>Bạn muốn tạo mã cho khách?",
                    type: "warning",
                    showCancelButton: true,
                    showCloseButton: false,
                    confirmButtonText: "OK tạo luôn!",
                    cancelButtonText: "Để em xem lại!",
                    html: true,
                }, function (confirm) {
                    if (confirm) {
                        sweetAlert.close();
                        ajaxGenerateCouponG20(customerID);
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

function ajaxGenerateCouponG20(customerID) {
    $.ajax({
        type: "POST",
        url: "/danh-sach-khach-hang.aspx/generateCouponG20",
        data: "{customerID: " + customerID + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d === "true") {
                swal({
                    title: "Thông báo:",
                    text: "Đã tạo mã giảm giá G20 cho khách!",
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
    sweetAlert.close();
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