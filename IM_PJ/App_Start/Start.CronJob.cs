using CronNET;
using IM_PJ.Controllers;
using IM_PJ.CronJob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ
{
    public partial class Startup
    {
        public void ConfigureCron()
        {
            var taskProductStatus = new ProductStatus();

            // Cập nhât JobID
            CronJobController.update("Product Status", taskProductStatus.JobID);

            CronManager.AddJob(taskProductStatus);
            CronManager.Start();
        }
    }
}