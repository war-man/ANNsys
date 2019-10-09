let controller = new OrderListController();

document.addEventListener("DOMContentLoaded", function (event) {
    initQuantity();
});

function initQuantity() {
    let param = controller.getQueryParam(window.location.href,"quantityfilter");

    if (param) {
        let divGreaterThanDOM = document.querySelector(".greaterthan");
        let divBetweenDOM = document.querySelector(".between");

        if (param === "greaterthan" || param === "lessthan") {
            divGreaterThanDOM.classList.remove("hide");
            divBetweenDOM.classList.add("hide");
        }
        else if (param == "between") {
            divGreaterThanDOM.classList.add("hide");
            divBetweenDOM.classList.remove("hide");
        }
    }
}

function onChange_ddlQuantityFilter(self) {
    let value = self.value;
    let divGreaterThanDOM = document.querySelector(".greaterthan");
    let divBetweenDOM = document.querySelector(".between");

    if (value == "greaterthan" || value == "lessthan") {
        let txtQuantityDOM = document.querySelector("[id$='_txtQuantity']");

        divGreaterThanDOM.classList.remove("hide");
        divBetweenDOM.classList.add("hide");

        txtQuantityDOM.focus();
        txtQuantityDOM.select();
    }
    else if (value == "between") {
        let txtQuantityMinDOM = document.querySelector("[id$='txtQuantityMin']");

        divGreaterThanDOM.classList.add("hide");
        divBetweenDOM.classList.remove("hide");

        txtQuantityMinDOM.focus();
        txtQuantityMinDOM.select();
    }
}

function onKeyUp_txtSearchOrder(event) {
    if (event.keyCode === 13) {
        controller.searchOrder();
    }
}

function onClick_aSearchOrder() {
    controller.searchOrder();
};

function onClick_aFeeInfoModal(orderID) {
    let modalDOM = document.querySelector("#feeInfoModal");
    let tbodyDOM = modalDOM.querySelector("tbody[id='feeInfo']");

    tbodyDOM.innerHTML = "";
    controller.openFeeInfoModal(tbodyDOM, orderID);
};

function onClick_spFinishStatusOrder(self, orderID) {
    swal({
        title: "Xác nhận",
        text: 'Bạn muốn chuyển trạng thái đơn là đang xử lý, phải không?',
        type: "warning",
        showCancelButton: true,
        closeOnConfirm: true,
        cancelButtonText: "Để em xem lại...",
        confirmButtonText: "Đúng rồi sếp!",
    }, function (confirm) {
        if (confirm) {
            controller.changeFinishStatusOrder(self, orderID);
        }
    });
};