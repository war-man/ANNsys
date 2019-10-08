let controller = new CronJobProductStatusController();

document.addEventListener("DOMContentLoaded", (event) => {
    // Jquery Dependency
    $("input[data-type='currency']").on({
        keyup: function () {
            UtilsService.formatCurrency($(this));
        },
        blur: function () {
            UtilsService.formatCurrency($(this), "blur");
        }
    });
});


function onKeyUp_txtSearchProduct(event) {
    if (event.keyCode == 13)
        controller.searchProduct();
};

function onClick_btnSearchProduct() {
    controller.searchProduct();
};

function onClick_btnCronJob() {
    controller.openCronJobSettingModal();
};

function onclick_UpdateCronJobSetting() {
    controller.updateCronJob();
}