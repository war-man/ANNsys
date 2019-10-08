using CronNET;
using IM_PJ.CronJob;
using IM_PJ.Models;
using IM_PJ.Models.Pages.cron_job_product_status;
using IM_PJ.Utils;
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
        public static IM_PJ.Models.CronJob update(string name, string cronExpression, int status, bool runAllProduct)
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
                    cronJOB.RunAllProduct = runAllProduct;
                    cronJOB.ModifiedDate = DateTime.Now;
                    
                    con.SaveChanges();

                    // Xóa task củ
                    if (cronJOB.JobID.HasValue)
                        CronManager.RemoveJob(cronJOB.JobID.Value);

                    // Load lại cron job để cập nhật thông tin mới
                    var taskProductStatus = new CreateScheduleProductStatus();
                    CronManager.AddJob(taskProductStatus);

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

        #region Table Cron Job Product Status
        /// <summary>
        /// Lấy lich trình post api trạng thái sản phẩm tới các web vệ tinh
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static List<CronJobProductStatu> getScheduleProductStatus(string web, int status, int size)
        {
            try
            {
                using (var con = new inventorymanagementEntities())
                {
                    var schdules = con.CronJobProductStatus
                        .Where(x => x.Web == web)
                        .Where(x => x.Status == status)
                        .OrderBy(x => x.ModifiedDate)
                        .Skip(0)
                        .Take(size)
                        .ToList();

                    return schdules;
                }
            }
            catch (Exception ex)
            {
                Log.Instance().Error("Insert the schedule new list", ex);
                throw;
            }
        }

        public static CronJobProductStatu postScheduleProductStatus(CronJobProductStatu scheduleNew)
        {
            try
            {
                using (var con = new inventorymanagementEntities())
                {
                    con.CronJobProductStatus.Add(scheduleNew);
                    con.SaveChanges();

                    return scheduleNew;
                }
            }
            catch (Exception ex)
            {
                Log.Instance().Error("Insert the schedule new", ex);
                throw;
            }
        }

        public static List<CronJobProductStatu> postScheduleProductStatus(List<CronJobProductStatu> scheduleNews)
        {
            try
            {
                using (var con = new inventorymanagementEntities())
                {
                    con.CronJobProductStatus.AddRange(scheduleNews);
                    con.SaveChanges();

                    return scheduleNews;
                }
            }
            catch (Exception ex)
            {
                Log.Instance().Error("Insert the schedule new list", ex);
                throw;
            }
        }

        /// <summary>
        /// Cập nhật lại các thông tin thay đổi mới
        /// </summary>
        /// <param name="scheduleNews"></param>
        /// <returns></returns>
        public static List<CronJobProductStatu> updateScheduleProductStatus(List<CronJobProductStatu> scheduleNews)
        {
            try
            {
                using (var con = new inventorymanagementEntities())
                {
                    var index = 0;

                    foreach (var item in scheduleNews)
                    {
                        index++;

                        var scheduleOld = con.CronJobProductStatus.Where(x => x.ID == item.ID).FirstOrDefault();

                        if (scheduleOld != null)
                        {
                            scheduleOld.Status = item.Status;
                            scheduleOld.Note = item.Note;
                            scheduleOld.ModifiedDate = item.ModifiedDate;
                        }

                        if (index >= 100)
                        {
                            con.SaveChanges();
                            index = 0;
                        }
                    }

                    if (index > 0)
                    {
                        con.SaveChanges();
                        index = 0;
                    }

                    return scheduleNews;
                }
            }
            catch (Exception ex)
            {
                Log.Instance().Error("Update the schedule list", ex);
                throw;
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

                // Lọc trạng thái cron job
                if (filter.status > 0)
                    schedules = schedules.Where(x => x.Status == filter.status);

                // Lọc theo danh mục
                if (filter.category > 0)
                {
                    var category = CategoryController.GetByID(filter.category);
                    var categoryID = CategoryController.getCategoryChild(category)
                        .Select(x => x.ID)
                        .OrderBy(o => o)
                        .ToList();

                    schedules = schedules.Where(x => categoryID.Contains(x.CategoryID));
                }

                // Lọc trạng thái ẩn hiện của sẩn phẩm
                if (filter.isHidden.HasValue)
                    schedules = schedules.Where(x => x.IsHidden == filter.isHidden.Value);
                #endregion

                // Lọc theo trạng thái ẩn hiện trang quảng cáo
                if (!String.IsNullOrEmpty(filter.showHomePage))
                    products = products.Where(x =>
                        x.ShowHomePage.ToString() == filter.showHomePage
                    );

                #region Tính toán phân trang
                var data = schedules
                    .Join(
                        products,
                        s => s.ProductID,
                        p => p.ID,
                        (s, p) => new { product = p, schedule = s }
                    )
                    .Join(
                        con.tbl_Category,
                        tem => tem.schedule.CategoryID,
                        c => c.ID,
                        (tem, c) => new { product = tem.product, schedule = tem.schedule, category = c }
                    )
                    .Select(x => new
                    {
                        categoryID = x.schedule.ID,
                        categoryName = x.category.CategoryName,
                        id = x.schedule.ProductID,
                        sku = x.product.ProductSKU,
                        title = x.product.ProductTitle,
                        image = x.product.ProductImage,
                        costOfGood = x.product.CostOfGood.HasValue ? x.product.CostOfGood.Value : 0,
                        regularPrice = x.product.Regular_Price.HasValue ? x.product.Regular_Price.Value : 0,
                        retailPrice = x.product.Retail_Price.HasValue ? x.product.Retail_Price.Value : 0,
                        web = x.schedule.Web,
                        showHomePage = x.product.ShowHomePage.HasValue ? x.product.ShowHomePage.Value : 0,
                        isHidden = x.schedule.IsHidden,
                        cronJobStatus = x.schedule.Status,
                        startDate = x.schedule.ModifiedDate,
                        note = x.schedule.Note,
                        productCreatedDate = x.product.CreatedDate.Value
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
                    showHomePage = x.showHomePage,
                    isHidden = x.isHidden,
                    cronJobStatus = x.cronJobStatus,
                    startDate = x.startDate,
                    note = x.note,
                    productCreatedDate = x.productCreatedDate
                })
                .ToList();

                return result;
            }
        }
    }
}