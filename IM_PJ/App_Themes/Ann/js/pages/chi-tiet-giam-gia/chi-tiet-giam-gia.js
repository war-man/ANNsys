let controller = new EditDiscountGroupController();

// Thể hiện Owner đã được phép truy cập discount group
function showOwner(item) {
    controller.showOwner(item);
};

// Thể hiện accout đã được phép truy cập discount group
function showAccoutPermittedAccess(item) {
    controller.showAccoutPermittedAccess(item);
};

// Sự kiện handle khi thêm account cho phép nhìn thấy group triết khấu
function addAccoutPermittedAccess() {
    controller.addAccoutPermittedAccess();
};
//  Sự kiện handle khi xóa account được phép nhìn thấy group triết khấu
function removeAccoutPermittedAccess(item) {
    controller.removeAccoutPermittedAccess(item);
};