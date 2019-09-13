class OrderModel {
    constructor(id, customerID, quantityProduct, price, discount, feeShipping, staffName, doneDate) {
        this._id = +id || 0;
        this._customerID = +customerID || 0;
        this._quantityProduct = +quantityProduct || 0;
        this._price = +price || 0;
        this._discount = +discount || 0;
        this._feeShipping = +feeShipping || 0;
        this._staffName = staffName || "";
        this._doneDate = doneDate || null;
    };

    //#region Mã đơn hàng
    get ID() {
        return this._id;
    };

    set ID(value) {
        this._id = +value || 0;
    };
    //#endregion Mã đơn hàng

    //#region Mã khách hàng
    get CustomerID() {
        return this._fcustomerID;
    };

    set CustomerID(value) {
        this._customerID = +value || 0;
    };
    //#endregion Mã khách hàng

    //#region Số lượng sản phẩm mua
    get QuantityProduct() {
        return this._quantityProduct;
    };

    set QuantityProduct(value) {
        this._quantityProduct = +value || 0;
    };
    //#endregion Số lượng sản phẩm mua

    //#region Giá đã bán
    get Price() {
        return this._price;
    };

    set Price(value) {
        this._price = +value || 0;
    };
    //#endregion Giá đã bán

    //#region Số tiền triết khấu
    get Discount() {
        return this._discount;
    };

    set Discount(value) {
        this._discount = +value || 0;
    };
    //#endregion Số tiền triết khấu

    //#region Số tiền shipping
    get FeeShipping() {
        return this._feeShipping;
    };

    set FeeShipping(value) {
        this._feeShipping = +value || 0;
    };
    //#endregion Số tiền shipping

    //#region Tên nhân viên khởi tạo đơn hàng
    get StaffName() {
        return this._staffName;
    };

    set StaffName(value) {
        this._staffName = value || "";
    };
    //#endregion Tên nhân viên khởi tạo đơn hàng

    //#region Ngày hoàn tất đơn
    get DateDone() {
        return this._doneDate;
    };

    set DateDone(value) {
        this._doneDate = value || null;
    };
    //#endregion Ngày hoàn tất đơn

    stringJSON() {
        return JSON.stringify({
            'ID': this._id,
            'CustomerID': this._customerID,
            'QuantityProduct': this._quantityProduct,
            'Price': this._price,
            'Discount': this._discount,
            'FeeShipping': this._feeShipping,
            'StaffName': this._staffName,
            'DoneDate': this._doneDate.format('yyyy-MM-dd hh:mm:ss')
        })
    }
};