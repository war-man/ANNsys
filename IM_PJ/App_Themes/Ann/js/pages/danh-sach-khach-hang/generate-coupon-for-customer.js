function generateCouponForCustomer(customerName, customerID, couponCode) {
    swal({
        title: "Xác nhận:",
        text: "Tạo mã giảm giá <strong>" + couponCode + "</strong> cho khách <strong>" + customerName + "</strong>?",
        type: "warning",
        showCancelButton: true,
        showCloseButton: false,
        confirmButtonText: "OK tạo ngay!",
        cancelButtonText: "Để xem lại!",
        html: true,
    }, function (confirm) {
        if (confirm) {
            sweetAlert.close();
            ajaxGenerateCoupon(customerID, couponCode);
        }
    });
}

function ajaxGenerateCoupon(customerID, couponCode) {
    $.ajax({
        type: "POST",
        url: "/danh-sach-khach-hang.aspx/generateCouponForCustomer",
        data: "{customerID: " + customerID + ", couponCode: '" + couponCode + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d === "true") {
                swal({
                    title: "Thông báo:",
                    text: "Đã tạo mã giảm giá cho khách!",
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
            else {
                alert("Lỗi");
            }
        }
    });
}