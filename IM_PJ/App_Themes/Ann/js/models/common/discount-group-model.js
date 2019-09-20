class DiscountGroupModel {
    constructor(id, name) {
        this._id = +id || 0;
        this._name = name || "";
    };

    //#region Mã nhóm chiết khấu
    get ID() {
        return this._id;
    };

    set ID(value) {
        this._id = +value || 0;
    };
    //#endregion Mã nhóm chiết khấu

    //#region Tên nhóm chiết khấu
    get Name() {
        return this._name;
    };

    set Name(value) {
        this._name = value || "";
    };
    //#endregion Tên nhóm chiết khấu

    JSON() {
        return {
            'ID': this._id,
            'Name': this._name
        }
    }

    stringJSON() {
        return JSON.stringify(JSON);
    }
};