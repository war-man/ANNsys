class OrderListService {
    static openFeeInfoModal(orderID) {
        return new Promise((reslove, reject) => {
            $.ajax({
                type: "POST",
                url: "/danh-sach-don-hang.aspx/getFeeInfo",
                data: JSON.stringify({ 'orderID': orderID }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: (response) => {
                    reslove(JSON.parse(response.d) || []);
                },
                error: err => {
                    reject(err);
                }
            });
        });
    };

    static changeFinishStatusOrder(orderID) {
        return new Promise((resolve, reject) => {
            $.ajax({
                type: "POST",
                url: "/danh-sach-don-hang.aspx/changeFinishStatusOrder",
                data: JSON.stringify({ 'orderID': orderID }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: response => {
                    resolve(response.d);
                },
                error: err => {
                    reject(err);
                }
            });
        });
    };
};