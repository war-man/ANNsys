// Handel sử lý nut short
$(document).keydown(function (e) {
    if (window.searchCustomer.isOpen)
    {
        return window.searchCustomer.handleKeyPress(e.which);
    }
    else {
        return window.controller.handleKeyPress(e.which);
    }
});

// Xử lý khi load tất cả DOM xong
document.addEventListener("DOMContentLoaded", function (event) {
    if (typeof (searchCustomer) === 'undefined') {
        window.searchCustomer = new SearchCustomerController("PotentialCustomerDiscount");
    }

    if (typeof (controller) === 'undefined') {
        window.controller = new AddCustomerDiscountGroupController();
    }
});

// Xự lý inpuprt put search khách hàng
function txtSearchKeyPress(event) {
    if (event.charCode == 13) {
        let discountGroupID = +document.querySelector("[id$='_hdfDiscountGroupID']").value || 0;
        let search = document.getElementById("txtSearch").value || "";
        let opts = {
            'discountGroupID': discountGroupID,
            'search': search.trim()
        }

        event.preventDefault();
        window.searchCustomer.show(opts);
        return false;
    }
}

// Xử lý button search
function showSearchCustomerModal() {
    let discountGroupID = +document.querySelector("[id$='_hdfDiscountGroupID']").value || 0;
    let search = document.getElementById("txtSearch").value || "";
    let opts = {
        'discountGroupID': discountGroupID,
        'search': search
    }

    window.searchCustomer.show(opts);
}

// Xử khi đã chọn khách hàng 
function selectedCustomer(customer) {
    let discountGroupID = +document.querySelector("[id$='_hdfDiscountGroupID']").value || 0;
    let opts = {
        'discountGroupID': discountGroupID,
        'customer': customer
    }

    window.controller.showOrderQualifiedOfDiscountGroup(opts);
}

// Xóa khách hàng khỏi nhóm chiết khấu
function deletecustomer(customer) {
    let c = confirm("Bạn muốn xóa khách hàng này ra khỏi nhóm?");
    if (c == true) {
        document.querySelector("[id$='_hdfCustomer']").value = JSON.stringify(customer);
        document.querySelector("[id$='_btnDelete']").click();
    }
}