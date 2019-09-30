using CronNET;
using IM_PJ.Controllers;
using IM_PJ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using IM_PJ.Models.Pages.ExecuteAPI;

namespace IM_PJ.CronJob
{
    public class ProductStatus : BaseJob
    {
        private const string CRON_NAME = "Product Status";

        public override CronExpression Cron
        {
            get
            {
                var cron = CronJobController.get(CRON_NAME);

                if (cron != null)
                    return (CronExpression)cron.CronExpression;
                else
                    return (CronExpression)"0 20 * * *";
            }
        }
        public override void Execute()
        {
            createSchedule();
            runSchedule();
        }

        /// <summary>
        /// Lấy ra danh sách trang web quảng cáo của thể loại sản phẩm đó
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        private static List<string> getWebAdvertisements(int categoryID)
        {
            var web = new List<string>();
            //web.Add("https://ann.com.vn");
            //web.Add("https://khohangsiann.com");
            //web.Add("https://bosiquanao.net");
            //web.Add("https://quanaogiaxuong.com");
            //web.Add("https://bansithoitrang.net");
            //web.Add("https://quanaoxuongmay.com");
            //web.Add("https://annshop.vn");
            web.Add("https://panpan.vn");

            //if (categoryID == 18)
            //    web.Add("https://chuyensidobo.com");
            //if (categoryID == 17)
            //    web.Add("https://damgiasi.vn");

            return web;
        }

        /// <summary>
        /// Lấy ra API dùng để cập nhật trạng thái sản phẩm
        /// </summary>
        /// <param name="web"></param>
        /// <returns></returns>
        private static string getAPIName(string web)
        {
            if (web == "https://panpan.vn")
                return "sync-product.html";
            else
                return "sync-product";
        }

        private static bool isPause()
        {
            var cron = CronJobController.get(CRON_NAME);
            if (cron != null)
                return cron.Status == 0 ? true : false;
            else
                return false;
        }

        /// <summary>
        /// Tìm những sản phẩm chưa update.
        /// Tính ra số lượng hiện tại tạo schedule
        /// </summary>
        private static void createSchedule()
        {
            System.Diagnostics.Debug.WriteLine("Bắt đầu chạy khởi tạo schedule");
            using (var con = new inventorymanagementEntities())
            {
                #region Tìm thời điểm cập nhật sản phẩm cuối cùng
                var lastDatetime = con.tbl_Product.Where(x => x.ModifiedDate.HasValue).Min(x => x.ModifiedDate.Value);
                #endregion

                #region Lọc những sản phẩm cần cập nhật
                var products = con.tbl_Product.Where(x => 1 == 1);
                #endregion

                #region Lấy thông tin stock của các sản phẩm cập nhật
                // Giảm bớt thông tin stock cho sản phẩn biến thể và không biến thể
                var stocks = con.tbl_StockManager
                    .Where(x => x.CreatedDate > lastDatetime)
                    .Join(
                        products,
                        s => s.ParentID,
                        p => p.ID,
                        (s, p) => s
                    );

                // Lấy ID stock dòng cuối cùng của mỗi sản phẩn (gồm sản phẩm biến thể  và không biến thể)
                var lastStocks = stocks.GroupBy(x => x.SKU).Select(x => new { stockID = x.Max(m => m.ID) });

                stocks = stocks
                    .Join(
                        lastStocks,
                        s => s.ID,
                        ls => ls.stockID,
                        (s, ls) => s
                    );

                // Tính số liệu tồn kho của các sản phẩn cần update (Nếu = 0 thì ẩn or hiện)
                var quantities = stocks
                    .Select(x => new
                    {
                        productID = x.ParentID.HasValue ? x.ParentID.Value : 0,
                        type = x.Type.HasValue ? x.Type.Value : 0,
                        quantity = x.Quantity.HasValue ? x.Quantity.Value : 0,
                        quantittyCurrent = x.QuantityCurrent.HasValue ? x.QuantityCurrent.Value : 0
                    })
                    .GroupBy(x => x.productID)
                    .Select(x => new
                    {
                        productID = x.Key,
                        quantity = x.Sum(s => s.type == 1 ? s.quantity + s.quantittyCurrent : s.quantittyCurrent - s.quantity)
                    });
                #endregion

                #region Lên cái lịch cập nhật và chèn vào table CronJobProductStatus
                // Dữ liệu chính dùng để cho xửa lý cấp nhật table CronJobProductStatus
                var data = products
                    .Join(
                        quantities,
                        p => p.ID,
                        q => q.productID,
                        (p, q) => new
                        {
                            categoryID = p.CategoryID.Value,
                            productID = p.ID,
                            sku = p.ProductSKU,
                            quantity = q.quantity
                        }
                    )
                    .ToList();

                foreach (var item in data)
                {
                    // Nếu cron job được yêu cầu dưng lại thì sẽ không sử lý nữa
                    if (isPause())
                        return;

                    System.Diagnostics.Debug.WriteLine("Schedule: " + item.ToString());

                    var now = DateTime.Now;
                    var product = con.tbl_Product.Where(x => x.ID == item.productID).FirstOrDefault();

                    if (product != null)
                    {
                        product.IsHidden = item.quantity <= 5;
                        product.ModifiedBy = "CronJob";
                        product.ModifiedDate = now;
                    }

                    var webs = getWebAdvertisements(item.categoryID);

                    foreach (var web in webs)
                    {
                        con.CronJobProductStatus.Add(new CronJobProductStatu()
                        {
                            Web = web,
                            API = getAPIName(web),
                            ProductID = item.productID,
                            SKU = item.sku,
                            IsHidden = item.quantity <= 5,
                            Status = (int)CronJobStatus.Scheduled,
                            CreatedDate = now,
                            ModifiedDate = now
                        });
                        con.SaveChanges();
                    }
                }
                #endregion
            }
            System.Diagnostics.Debug.WriteLine("Kết thúc chạy khởi tạo schedule");
        }

        /// <summary>
        /// Dữ vào thông tin schedule sẽ update ẩn / hiện lên các trang quảng cáo
        /// </summary>
        private static void runSchedule()
        {
            System.Diagnostics.Debug.WriteLine("Băt đầu chạy post API tới các trang quảng cáo");
            using (var con = new inventorymanagementEntities())
            {
                var cron = con.CronJobProductStatus
                    .Where(x => x.Status == (int)CronJobStatus.Scheduled)
                    .OrderBy(x => x.CreatedDate)
                    .ToList();

                foreach (var item in cron)
                {
                    // Nếu cron job được yêu cầu dưng lại thì sẽ không sử lý nữa
                    if (isPause())
                        return;

                    item.Status = (int)CronJobStatus.Start;

                    
                    var executeAPI = new ExecuteAPI();

                    ResponseProductStatusModel response = null; 
                    var thread = new Thread(() => { response = executeAPI.postSync(item); });
                    thread.Start();
                    thread.Join();

                    if (response == null)
                    {
                        item.Status = (int)CronJobStatus.Fail;
                        item.Note = "Đã xãy ra lỗi khi gọi thực thi api";
                        item.ModifiedDate = DateTime.Now;
                        con.SaveChanges();
                    }
                    else if (response.response == "done")
                    {
                        item.Status = (int)CronJobStatus.Done;
                        item.Note = response.response;
                        item.ModifiedDate = DateTime.Now;
                        con.SaveChanges();
                    }
                    else if (response.response == "notfound")
                    {
                        item.Status = (int)CronJobStatus.Continue;
                        item.Note = response.response;
                        item.ModifiedDate = DateTime.Now;
                        con.SaveChanges();
                    }
                    else if (response.response == "error")
                    {
                        item.Status = (int)CronJobStatus.Fail;
                        item.Note = response.response;
                        item.ModifiedDate = DateTime.Now;
                        con.SaveChanges();
                    }
                    else
                    {
                        item.Status = (int)CronJobStatus.Fail;
                        response.content = "Trường hợp success không có trong định nghĩa. " + response.content;
                        if (response.content.Length > 255)
                            item.Note = response.content.Substring(0, 254);
                        else
                            item.Note = response.content;
                        item.ModifiedDate = DateTime.Now;
                        con.SaveChanges();
                    }
                }
            }
            System.Diagnostics.Debug.WriteLine("Kết thúc chạy post API tới các trang quảng cáo");
        }
    }
}