let controller = new CreateDiscountGroupController();

// Thêm Owner vào nhóm cho phép truy cập discount group
function addOwner(item) {
    controller.addOwner(item);
};

// Sự kiện handle khi thêm account cho phép nhìn thấy group chiết khấu
function addAccoutPermittedAccess() {
    controller.addAccoutPermittedAccess();
};
//  Sự kiện handle khi xóa account được phép nhìn thấy group chiết khấu
function removeAccoutPermittedAccess(item) {
    controller.removeAccoutPermittedAccess(item);
};