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
    }

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
    }

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
    }
}