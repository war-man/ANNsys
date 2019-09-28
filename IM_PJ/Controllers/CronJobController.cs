using CronNET;
using IM_PJ.CronJob;
using IM_PJ.Models;
using IM_PJ.Models.Pages.cron_job_product_status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Controllers
{
    public class CronJobController
    {
        #region Table CronJob
        /// <summary>
        /// Lấy thông tin cron job dựa theo tên
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IM_PJ.Models.CronJob get(string name)
        {
            using (var con = new inventorymanagementEntities())
            {
                return con.CronJobs.Where(x => x.Name.Trim().ToLower() == name.Trim().ToLower()).FirstOrDefault();
            }
        }

        /// <summary>
        /// Cập nhật lại trạng thái và giờ start
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cronExpression"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static IM_PJ.Models.CronJob update(string name, string cronExpression, int status)
        {
            using (var con = new inventorymanagementEntities())
            {
                var cronJOB = con.CronJobs
                    .Where(x => x.Name.Trim().ToLower() == name.Trim().ToLower())
                    .FirstOrDefault();

                if (cronJOB != null)
                {
                    // Cập nhật lại trạng thái và giờ start
                    cronJOB.CronExpression = cronExpression;
                    cronJOB.Status = status;
                    cronJOB.ModifiedDate = DateTime.Now;
                    
                    con.SaveChanges();

                    // Xóa task củ
                    if (cronJOB.JobID.HasValue)
                        CronManager.RemoveJob(cronJOB.JobID.Value);

                    // Load lại cron job để cập nhật thông tin mới
                    var taskProductStatus = new ProductStatus();
                    CronManager.AddJob(taskProductStatus);
                    CronManager.LoadJobs();

                    // Cập nhật lại JobID new
                    cronJOB.JobID = taskProductStatus.JobID;
                    con.SaveChanges();
                }

                return cronJOB;
            }
        }

        // Cập nhật thông tin JobID
        public static IM_PJ.Models.CronJob update(string name, Guid jobID)
        {
            using (var con = new inventorymanagementEntities())
            {
                var cronJOB = con.CronJobs
                    .Where(x => x.Name.Trim().ToLower() == name.Trim().ToLower())
                    .FirstOrDefault();

                if (cronJOB != null)
                {
                    cronJOB.JobID = jobID;
                    cronJOB.ModifiedDate = DateTime.Now;

                    con.SaveChanges();
                }

                return cronJOB;
            }
        }
        #endregion
        public static List<Models.Pages.cron_job_product_status.ProductModel> getScheduleProductStatus(FilterModel filter, ref PaginationMetadataModel page)
        {
            using (var con = new inventorymanagementEntities())
            {
                #region Lọc ra sẩn phẩn cần show
                var products = con.tbl_Product.Where(x => 1 == 1);
                var schedules = con.CronJobProductStatus
                    .Where(x => x.CreatedDate >= filter.fromDate)
                    .Where(x => x.CreatedDate <= filter.toDate);

                // Lọc theo thông tin sản phẩm
                if (!String.IsNullOrEmpty(filter.search))
                    products = products.Where(x =>
                        x.ID.ToString() == filter.search ||
                        x.ProductSKU.Contains(filter.search) ||
                        x.ProductTitle.Contains(filter.search)
                    );

                // Lọc theo thông tin lịch chạy cron cập nhật trạng thái sản phẩm
                if (!String.IsNullOrEmpty(filter.web))
                    schedules = schedules.Where(x => x.Web.EndsWith(filter.web));

                if (filter.status > 0)
                    schedules = schedules.Where(x => x.Status == filter.status);
                #endregion

                #region Tính toán phân trang
                var data = products
                    .Join(
                        schedules,
                        p => p.ID,
                        s => s.ProductID,
                        (p, s) => new { product = p, schedule = s }
                    )
                    .Join(
                        con.tbl_Category,
                        tem => tem.product.CategoryID,
                        c => c.ID,
                        (tem, c) => new { product = tem.product, schedule = tem.schedule, category = c }
                    )
                    .Select(x => new
                    {
                        categoryID = x.category.ID,
                        categoryName = x.category.CategoryName,
                        id = x.product.ID,
                        sku = x.product.ProductSKU,
                        title = x.product.ProductTitle,
                        image = x.product.ProductImage,
                        costOfGood = x.product.CostOfGood.HasValue ? x.product.CostOfGood.Value : 0,
                        regularPrice = x.product.Regular_Price.HasValue ? x.product.Regular_Price.Value : 0,
                        retailPrice = x.product.Retail_Price.HasValue ? x.product.Retail_Price.Value : 0,
                        web = x.schedule.Web,
                        isHidden = x.schedule.IsHidden,
                        cronJobStatus = x.schedule.Status,
                        startDate = x.schedule.ModifiedDate,
                        note = x.schedule.Note
                    });

                // Calculate pagination
                page.totalCount = data.Count();
                page.totalPages = (int)Math.Ceiling(page.totalCount / (double)page.pageSize);

                data = data
                    .OrderByDescending(x => x.startDate)
                    .Skip((page.currentPage - 1) * page.pageSize)
                    .Take(page.pageSize);
                #endregion

                var result = data.Select(x => new Models.Pages.cron_job_product_status.ProductModel()
                {
                    categoryID = x.categoryID,
                    categoryName = x.categoryName,
                    id = x.id,
                    sku = x.sku,
                    title = x.title,
                    image = x.image,
                    costOfGood = x.costOfGood,
                    regularPrice = x.regularPrice,
                    retailPrice = x.retailPrice,
                    web = x.web,
                    isHidden = x.isHidden,
                    cronJobStatus = x.cronJobStatus,
                    startDate = x.startDate,
                    note = x.note
                })
                .ToList();

                return result;
            }
        }
    }
}