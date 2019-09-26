let controller = new CronJobProductStatusController();


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