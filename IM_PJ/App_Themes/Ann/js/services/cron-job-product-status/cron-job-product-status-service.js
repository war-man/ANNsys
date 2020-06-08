class CronJobProductStatusService {
    static getCronJob() {
        return new Promise((resolve, reject) => {
            $.ajax({
                type: "POST",
                url: "/cron-job-product-status.aspx/getCronJob",
                data: "",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: response => {
                    resolve(response.d);
                },
                error: err => {
                    reject(err)
                }
            })
        });
    }

    static updateCronJob(cronNew) {
        return new Promise((resolve, reject) => {
            $.ajax({
                type: "POST",
                url: "/cron-job-product-status.aspx/updateCronJob",
                data: JSON.stringify({ 'cronNew': cronNew }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: response => {
                    resolve(response.d);
                },
                error: err => {
                    reject(err)
                }
            })
        });
    }
};