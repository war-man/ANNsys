using CronNET;
using IM_PJ.Controllers;
using IM_PJ.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Threading;
using System.Security.Permissions;

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
            runSchedule();
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

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(item.Web);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        // HTTP POST
                        var content = new FormUrlEncodedContent(new[] {
                            new KeyValuePair<string, string>("key", "828327"),
                            new KeyValuePair<string, string>("sku", item.SKU),
                            new KeyValuePair<string, string>("visibility", item.IsHidden ? "hidden" : "visible")
                        });

                        try
                        {
                            System.Diagnostics.Debug.WriteLine("API: " + JsonConvert.SerializeObject(item));
                            var response = client.PostAsync(item.API, content).Result;

                            if (response.IsSuccessStatusCode)
                            {
                                item.Status = (int)CronJobStatus.Done;
                                item.ModifiedDate = DateTime.Now;
                                item.Note = response.Content.ReadAsStringAsync().Result;
                                con.SaveChanges();
                            }
                            else
                            {
                                item.Status = (int)CronJobStatus.Fail;
                                item.ModifiedDate = DateTime.Now;
                                con.SaveChanges();
                            }
                            Thread.Sleep(100);
                        }
                        catch (ThreadAbortException ex)
                        {
                            System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
                            item.Status = (int)CronJobStatus.Fail;
                            item.ModifiedDate = DateTime.Now;
                            if (ex.Message.Length > 255)
                                item.Note = ex.Message.Substring(0, 254);
                            else
                                item.Note = ex.Message;
                            con.SaveChanges();
                            Thread.ResetAbort();
                            continue;
                        }
                        
                    }
                }
            }
            System.Diagnostics.Debug.WriteLine("Kết thúc chạy post API tới các trang quảng cáo");
        }
    }
}