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
            CronManager.Start();
            var taskProductStatus = CronManager.GetJobs().FirstOrDefault();
            
            // Cập nhât JobID
            if (taskProductStatus != null)
                CronJobController.update("Product Status", taskProductStatus.JobID);
        }
    }
}