class CronJobProductStatusController {
    openCronJobSettingModal() {
        window.HoldOn.open();
        CronJobProductStatusService.getCronJob()
            .then(data => {
                if(data)
                {
                    let modalDOM = document.querySelector("#CronJobSettingModal");
                    let cronExpressionDOM = modalDOM.querySelector("[id$='_txtCronExpression']");
                    let cronJobStatusDOM = modalDOM.querySelector("[id$='_ddlCronJobStatus']");
                    let minProductDOM = modalDOM.querySelector("[id$='_txtMinProduct']");
                    
                    cronExpressionDOM.value = data.CronExpression;
                    cronJobStatusDOM.value = +data.Status || 0;
                    if (data.RunAllProduct) {
                        $('#chbRunAllProduct').bootstrapToggle('on');
                    }
                    else {
                        $('#chbRunAllProduct').bootstrapToggle('off');
                    };
                    minProductDOM.value = UtilsService.formatThousands((+data.MinProduct || 0), ',');

                    $("#CronJobSettingModal").modal({ show: 'true', backdrop: 'static' });
                }
                else {
                    setTimeout(function () {
                        swal("Thông báo", "Không tìm thấy thông tin Cron Job", "error");
                    }, 500);
                }
            })
            .catch(err => {
                console.log(err);
                setTimeout(function () {
                    swal("Thông báo", "Có lỗi trong việc lấy thông tin Cron Job", "error");
                }, 500);
            })
            .finally(() => { window.HoldOn.close(); });
    }

    updateCronJob() {
        let modalDOM = document.querySelector("#CronJobSettingModal");
        let cronExpressionDOM = modalDOM.querySelector("[id$='_txtCronExpression']");
        let cronJobStatusDOM = modalDOM.querySelector("[id$='_ddlCronJobStatus']");
        let runAllProductDOM = modalDOM.querySelector("#chbRunAllProduct");
        let minProductDOM = modalDOM.querySelector("[id$='_txtMinProduct']");

        // Lấy dữ liệu để cập nhật cron job
        let cronNew = {
            'CronExpression': cronExpressionDOM.value || "* * * * *",
            'Status': +cronJobStatusDOM.value || 0,
            'RunAllProduct': runAllProductDOM.checked || false,
            'MinProduct': +minProductDOM.value.replace(',', '') || 1
        }

        window.HoldOn.open();
        CronJobProductStatusService.updateCronJob(cronNew)
            .then(data => {
                modalDOM.querySelector("#closeCronJobSetting").click();

                setTimeout(function () {
                    swal("Thông báo", "Cập nhật trạng thái Cron Job thành công", "success");
                }, 500);
            })
            .catch(err => {
                console.log(err);
                setTimeout(function () {
                    swal("Thông báo", "Có lỗi trong việc cập nhật trạng thái Cron Job", "error");
                }, 500);
            })
            .finally(() => { window.HoldOn.close(); });
    };

    searchProduct() {
        document.querySelector("[id$='_btnSearch']").click();
    }
};