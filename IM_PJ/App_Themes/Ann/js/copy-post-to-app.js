function copyPostToApp(id) {
    $.ajax({
        type: "POST",
        url: "/danh-sach-bai-viet.aspx/copyPostToApp",
        data: "{id: '" + id + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d !== "false") {
                var id = msg.d;
                swal({
                    title: "Thông báo:",
                    text: "Đã copy bài viết này vào App!",
                    type: "success",
                    showCancelButton: false,
                    showCloseButton: false,
                    closeOnConfirm: true,
                    confirmButtonText: "OK! Xem thử!",
                    html: true,
                }, function (isconfirm) {
                    if (isconfirm) {
                        window.location.href = "/sua-bai-viet-app?id=" + id;
                    }
                });
            }
            else {
                alert("Lỗi");
            }
        }
    });
}