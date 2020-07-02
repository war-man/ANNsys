class ProductService {
    static liquidate(productID) {
        return new Promise((resolve, reject) => {
            $.ajax({
                type: "POST",
                url: "/tat-ca-san-pham.aspx/liquidateProduct",
                data: JSON.stringify({ 'productID': productID }),
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

    static recoverLiquidated(productID, sku) {
        return new Promise((resolve, reject) => {
            $.ajax({
                type: "POST",
                url: "/tat-ca-san-pham.aspx/recoverLiquidatedProduct",
                data: JSON.stringify({ 'productID': productID, 'sku': sku }),
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

    static updateHidden(productID, isHidden) {
        return new Promise((resolve, reject) => {
            $.ajax({
                type: "POST",
                url: "/sp.aspx/updateHidden",
                data: JSON.stringify({ 'productID': productID, 'isHidden': isHidden }),
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

    static handleSyncProduct(categoryID, productID, sku, up, renew, isHidden) {
        return new Promise((resolve, reject) => {
            try {
                let web = [
                    "ann.com.vn",
                    "khohangsiann.com",
                    "bosiquanao.net",
                    "quanaogiaxuong.com",
                    "bansithoitrang.net",
                    "quanaoxuongmay.com",
                    "annshop.vn",
                    "panpan.vn",
                    "thoitrangann.com",
                    "nhapsionline.com"
                ];
                web.forEach((page, index) => {
                    if (page)
                        return false;
                    let visibility = isHidden ? 'hidden' : 'visible';
                    upProductToWeb(upProductToWeb(page, sku, productID, up, renew, index, visibility));
                });

                resolve(true);
            } catch (e) {
                reject(e);
            }
        });
    };
};