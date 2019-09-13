class DiscountGroupModel {
    constructor(id, name) {
        this._id = +id || 0;
        this._name = name || "";
    };

    //#region Mã nhóm triết khấu
    get ID() {
        return this._id;
    };

    set ID(value) {
        this._id = +value || 0;
    };
    //#endregion Mã nhóm triết khấu

    //#region Tên nhóm triết khấu
    get Name() {
        return this._name;
    };

    set Name(value) {
        this._name = value || "";
    };
    //#endregion Tên nhóm triết khấu

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