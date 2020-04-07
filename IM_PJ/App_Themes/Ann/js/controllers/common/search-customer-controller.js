class SearchCustomerController {
    constructor(kind) {
        this._kind = kind || "";
        this._modal_id = "modal-customer";
    };

    //#region Loại màn hình search
    get Kind() {
        return this._kind;
    };

    set Kind(value) {
        this._kind = value || "";
    };
    //#endregion Loại màn hình search

    get isOpen() {
        let modal = document.getElementById(this._modal_id);

        if (modal) {
            return modal.style.display != 'none' ? true : false;
        }
        else {
            return false;
        };
    }

    handleKeyPress(charCode) {
        let modalDOM = document.getElementById(this._modal_id);

        // F1
        if (charCode == 112)
        {
            modalDOM.querySelector('#modal-search-customer').focus();
            return false;
        }

        // Esc
        if (charCode == 27)
        {
            modalDOM.querySelector('button.close').click();
            return false;
        }

        // Enter
        if (charCode == 13)
        {
            modalDOM.querySelector('#modal-btn-search-customer').click();
            return false;
        }
    };

    show(opts) {
        if (this._kind === "PotentialCustomerDiscount") {
            let discountGroupID = +opts.discountGroupID || 0;
            let search = opts.search || "";

            // Kiểm tra xe có nhập thông tin nhóm chiết khấu không
            if (!discountGroupID)
                return swal("Thông báo", "Không có mã nhóm chiết khấu", "error");

            window.HoldOn.open();
            SearchCustomerService.potentialCustomerDiscount(discountGroupID, search)
                .then(data => {
                    if (data.length > 0) {
                        this.showPotentialCustomerDiscount(data, search, discountGroupID);
                    }
                    else {
                        return swal("Thông báo", "Không tìm thấy khách (có thể chưa đủ điều kiện) hoặc đã được thêm vào nhóm", "warning");
                    };
                })
                .catch(err => {
                    console.log(err);
                    let message = "Lỗi trong quá trình lấy danh sách khách hàng có thể được nhập vào nhóm chiết khấu";
                    return swal("Thông báo", message, "error");
                })
                .finally(() => { window.HoldOn.close(); });
        };
    };

    showPotentialCustomerDiscount(data, search, discountGroupID) {
        // Remoe modal củ
        let modalDOM = document.getElementById(this._modal_id);
        if (modalDOM)
            modalDOM.remove();

        let accountName = UtilsService.getCookie('usernameLoginSystem') || "";
        let theadCSS = data.length > 11 ? "" : "width: 100%;"
        let strModalHTML = "";

        strModalHTML += '<div class="modal fade" id="' + this._modal_id + '" role="dialog">';
        strModalHTML += '    <div class="modal-dialog modal-lg">';
        strModalHTML += '        <div class="modal-content">';
        strModalHTML += '            <div class="modal-header">';
        strModalHTML += '                <button type="button" class="close" data-dismiss="modal" >&times;</button>';
        strModalHTML += '                <h4 class="modal-title">Danh sách khách hàng đạt yêu cầu</h4>';
        strModalHTML += '            </div>';
        strModalHTML += '            <div class="modal-body">';
        strModalHTML += '                <div class="form-group" style="display: flex;">';
        strModalHTML += '                    <input type="text" id="modal-search-customer" class="form-control" value="' + search + '" placeholder="Nhập thông tin khách hàng (F1)" />';
        strModalHTML += '                    <a href="javascript:;" id="modal-btn-search-customer" class="btn btn-primary" onclick="window.searchCustomer.searchPotentialCustomerDiscount(' + discountGroupID + ')">Tìm kiếm</a>';
        strModalHTML += '                </div>';
        strModalHTML += '                <div class="form-group">';
        strModalHTML += '                    <table class="table table-striped table-fixed">';
        strModalHTML += '                        <thead style="' + theadCSS + '">';
        strModalHTML += '                            <tr>';
        strModalHTML += '                                <th class="col-xs-4">Họ tên</th>';
        strModalHTML += '                                <th class="col-xs-4">Nick</th>';
        strModalHTML += '                                <th class="col-xs-2">Điện thoại</th>';
        strModalHTML += '                                <th class="col-xs-2">Nhân viên</th>';
        strModalHTML += '                            </tr>';
        strModalHTML += '                        </thead>';
        strModalHTML += '                        <tbody>';
        data.forEach(item => {
            strModalHTML += "                            <tr onclick='window.searchCustomer.selectCustomer(" + item.stringJSON() + ")'>";
            strModalHTML += '                                <td class="col-xs-4">' + item.FullName + '</td>';
            strModalHTML += '                                <td class="col-xs-4">' + item.Nick + '</td>';
            strModalHTML += '                                <td class="col-xs-2">' + item.Phone + '</td>';
            strModalHTML += '                                <td class="col-xs-2">' + item.StaffName + '</td>';
            strModalHTML += '                            </tr>';
        });
        strModalHTML += '                        </tbody>';
        strModalHTML += '                    </table>';
        strModalHTML += '                </div>';
        strModalHTML += '            </div>';
        strModalHTML += '        </div>';
        strModalHTML += '    </div>';
        strModalHTML += '</div>';

        // Chèn html modal vào body
        document.body.innerHTML += strModalHTML;
        // Show modal
        $("#" + this._modal_id).modal({ 'show': 'true', 'backdrop': 'static' });
    };

    searchPotentialCustomerDiscount(discountGroupID) {
        let modalDOM = document.getElementById(this._modal_id);
        let search = modalDOM.querySelector('#modal-search-customer').value || '';

        window.HoldOn.open();
        SearchCustomerService.potentialCustomerDiscount(discountGroupID, search)
            .then(data => {
                if (data.length > 0) {
                    this.updatePotentialCustomerDiscount(data);
                    // Show modal
                    $("#modal-customer").modal({ 'show': 'true', 'backdrop': 'static' });
                }
                else {
                    return swal("Thông báo", "Không tìm thấy", "warning");
                };
            })
            .catch(err => {
                console.log(err);
                let message = "Lỗi trong quá trình lấy danh sách khách hàng có thể được nhập vào nhóm chiết khấu";
                return swal("Thông báo", message, "error");
            })
            .finally(() => { window.HoldOn.close(); });
    };

    updatePotentialCustomerDiscount(data) {
        let accountName = UtilsService.getCookie('usernameLoginSystem') || "";
        let modalDOM = document.getElementById(this._modal_id);
        let theadDOM = modalDOM.querySelector('thead');
        let tboyDOM = modalDOM.querySelector('tbody');
        let strModalHTML = '';

        data.forEach(item => {
            strModalHTML += "                            <tr onclick='window.searchCustomer.selectCustomer(" + item.stringJSON() + ")'>";
            strModalHTML += '                                <td class="col-xs-4">' + item.FullName + '</td>';
            strModalHTML += '                                <td class="col-xs-4">' + item.Nick + '</td>';
            strModalHTML += '                                <td class="col-xs-2">' + item.Phone + '</td>';
            strModalHTML += '                                <td class="col-xs-2">' + item.StaffName + '</td>';
            strModalHTML += '                            </tr>';
        });

        theadDOM.style.width = data.length > 11 ? '98%' : '100%';
        tboyDOM.innerHTML = strModalHTML;
    };

    selectCustomer(customer) {
        let modalDOM = document.getElementById(this._modal_id);
        modalDOM.querySelector('button.close').click();

        if (typeof ('selectedCustomer') !== "undefined")
            selectedCustomer(customer);

        return;
    }
};