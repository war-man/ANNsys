class CustomerModel {
    constructor(id,
                fullName,
                nick,
                phone,
                address,
                zalo,
                facebook,
                staffName,
                discountGroup) {
        this._id = +id || 0;
        this._fullName = fullName || "";
        this._nick = nick || "";
        this._phone = phone || "";
        this._address = address || "";
        this._zalo = zalo || "";
        this._facebook = facebook || "";
        this._staffName = staffName || "";
        this._discout_group = discountGroup;
    };

    //#region Mã khách hàng
    get ID() {
        return this._id;
    };

    set ID(value) {
        this._id = +value || 0;
    };
    //#endregion Mã khách hàng

    //#region Họ tên khách hàng
    get FullName() {
        return this._fullName;
    };

    set FullName(value) {
        this._fullName = value || "";
    };
    //#endregion Họ tên khách hàng

    //#region Nick
    get Nick() {
        return this._nick;
    };

    set Nick(value) {
        this._nick = value || "";
    };
    //#endregion Nick

    //#region Số điện thoại
    get Phone() {
        return this._phone;
    };

    set Phone(value) {
        this._phone = value || "";
    };
    //#endregion Số điện thoại

    //#region Địa chỉ khách hàng
    get Address() {
        return this._address;
    };

    set Address(value) {
        this._address = value || "";
    };
    //#endregion Địa chỉ khách hàng

    //#region Zalo
    get Zalo() {
        return this._zalo;
    };

    set Zalo(value) {
        this._zalo = value || "";
    };
    //#endregion Zalo

    //#region Facebook
    get Facebook() {
        return this._facebook;
    };

    set Facebook(value) {
        this._facebook = value || "";
    };
    //#endregion Facebook

    //#region Tên nhân viên khởi tạo khách hàng
    get StaffName() {
        return this._staffName;
    };

    set StaffName(value) {
        this._staffName = value || "";
    };
    //#endregion Tên nhân viên khởi tạo khách hàng

    //#region Thông tin nhóm chiết khấu
    get DiscountGroup() {
        return this._discout_group;
    };

    set DiscountGroup(value) {
        this._discout_group = value;
    };
    //#endregion Thông tin nhóm chiết khấu

    stringJSON() {
        return JSON.stringify({
            'ID': this._id,
            'FullName': this._fullName,
            'Nick': this._nick,
            'Phone': this._phone,
            'Address': this._address,
            'Zalo': this._zalo,
            'Facebook': this._facebook,
            'StaffName': this._staffName,
            'DiscountGroup': this._discout_group ? this._discout_group.JSON() : null
        })
    }
};