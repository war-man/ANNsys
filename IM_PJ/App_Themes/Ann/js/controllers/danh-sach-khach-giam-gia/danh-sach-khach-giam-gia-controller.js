class AddCustomerDiscountGroupController {
    constructor() {
        this._modalOrderID = 'modal-order';
    };

    // Xư lý nut short
    handleKeyPress(charCode) {
        //F1 Search Customer
        if (charCode == 112) {
            document.getElementById("txtSearch").focus();
            return false;
        };

        // Esc
        if (charCode == 27) {
            let modalDOM = document.getElementById(this._modalOrderID);

            if (modalDOM.style.display != 'none') {
                modalDOM.querySelector('button.close').click();
                return false;
            }
        }
    }

    // Hiện thị danh sách order khách hàng đạt tiêu chuẩn của nhóm triết khấu
    showOrderQualifiedOfDiscountGroup(opts) {
        let discountGroupID = +opts.discountGroupID || 0;
        let customer = opts.customer;

        if (!discountGroupID)
            return swal("Thông báo", "Mã nhóm triết khấu không tìm thấy", "error");

        if (!customer)
            return swal("Thông báo", "Không thấy thông tin khách hàng", "error");

        // Cập nhật thông tin khách hạng chọn vào biến ẩn
        document.querySelector("[id$='_hdfCustomer']").value = JSON.stringify(customer);

        window.HoldOn.open();
        AddCustomerDiscountGroupService.getOrderQualifiedOfDiscountGroup(discountGroupID, customer.ID)
            .then(data => {
                if (data.length > 0) {
                    this.updatOrderModal(customer, data);
                    // Show modal
                    $("#" + this._modalOrderID).modal({ 'show': 'true', 'backdrop': 'static' });
                }
                else {
                    return swal("Thông báo", "Không có đơn hàng nào hết", "warning");
                };
            })
            .catch(err => {
                console.log(err);
                let message = "Lỗi trong quá trình lấy danh các đơn hàng đủ điều kiện";
                return swal("Thông báo", message, "error");
            })
            .finally(() => { window.HoldOn.close(); });

    };

    // Update lại tbody order modal mỗi khi chọn một khách hàng mới
    updatOrderModal(customer, data) {
        let modalDOM = document.getElementById(this._modalOrderID);
        let titleDOM = modalDOM.querySelector("[data-id='custom-name']");
        let nickDOM = modalDOM.querySelector("[data-id='nick']");
        let phoneDOM = modalDOM.querySelector("[data-id='phone']");
        let addressDOM = modalDOM.querySelector("[data-id='address']");
        let discountDOM = modalDOM.querySelector("[data-id='discount']");
        let theadDOM = modalDOM.querySelector('thead');
        let tbodyDOM = modalDOM.querySelector("tbody");
        let strModalHTML = '';

        titleDOM.innerText = 'Danh sách đơn hàng (' + customer.FullName + ')';
        nickDOM.innerHTML = '<label>Nick: </label>' + customer.Nick;
        phoneDOM.innerHTML = '<label>Phone: </label>' + customer.Phone;
        addressDOM.innerHTML = '<label>Địa chỉ: </label>' + customer.Address;
        if (customer.DiscountGroup) {
            discountDOM.style.display = '';
            discountDOM.innerHTML = '<label>Nhóm triết khấu hiện tại: </label>' + customer.DiscountGroup.Name;
        }
        else {
            discountDOM.style.display = 'none';
            discountDOM.innerHTML = '';
        }

        data.forEach(item => {
            strModalHTML += "                         <tr onclick='window.controller.selectedOrder(" + item.stringJSON() + ")'>";
            strModalHTML += '                            <td class="col-xs-2">' + item.ID + '</td>';
            strModalHTML += '                            <td class="col-xs-2">' + UtilsService.formatThousands(item.QuantityProduct, ",") + '</td>';
            strModalHTML += '                            <td class="col-xs-2">' + UtilsService.formatThousands(item.Price, ",") + '</td>';
            strModalHTML += '                            <td class="col-xs-2">' + UtilsService.formatThousands(item.FeeShipping, ",") + '</td>';
            strModalHTML += '                            <td class="col-xs-2">' + item.StaffName + '</td>';
            strModalHTML += '                            <td class="col-xs-2">' + (item.DateDone ? item.DateDone.format('dd/MM/yyyy') : '') + '</td>';
            strModalHTML += '                         </tr>';
        });

        theadDOM.style.width = data.length > 11 ? '98%' : '100%';
        tbodyDOM.innerHTML = strModalHTML;
    };

    selectedOrder(order) {
        // Đóng modal chọn order
        let modalDOM = document.getElementById(this._modalOrderID);
        if (modalDOM)
            modalDOM.querySelector("button.close").click();

        let customer = JSON.parse(document.querySelector("[id$='_hdfCustomer']").value);
        if (!customer)
            return swal("Thông báo", "Đã có lỗi trong việc lấy thông tin khách hàng đã chọn", "error");

        let message = "";

        if (customer.DiscountGroup) {
            message += "Bạn muốn khách hàng <strong>" + customer.FullName + "</strong> sẽ rời khỏi nhóm ";
            message += "<a class='customer-name-link' href='/danh-sach-khach-giam-gia?id=" + customer.DiscountGroup.ID + "' target='_blank'>" + customer.DiscountGroup.Name + "</a> ";
            message += "để vô nhóm này.";
        }
        else {
            message = "Bạn muốn khách hàng <strong>" + customer.FullName + "</strong> vào nhóm triết khấu";
        };

        return swal({
            title: 'Thông báo',
            text: message,
            type: 'warning',
            showCancelButton: true,
            cancelButtonText: "Để xem lại",
            confirmButtonText: "Tiếp tục",
            html: true,
        },
        function (confirm) {
            if (confirm) {
                document.querySelector("[id$='_hdfOrder']").value = JSON.stringify(order);
                document.querySelector("[id$='_btnAddCustomer']").click();
            }
        });
    };
};