using IM_PJ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Controllers
{
    public class RegisterProductController
    {
        public static string Inster(RegisterProduct item)
        {
            using (var con = new inventorymanagementEntities())
            {
                con.RegisterProducts.Add(item);
                int insert = con.SaveChanges();

                return insert.ToString();
            }
        }

        public static RegisterProduct Update(RegisterProductSession item, tbl_Account acc)
        {
            using (var con = new inventorymanagementEntities())
            {
                var register = con.RegisterProducts.Where(x => x.ID == item.id).FirstOrDefault();

                if (register != null)
                {
                    register.Customer = item.customer;
                    register.Status = item.status;
                    register.Quantity = item.quantity;
                    register.ExpectedDate = item.expectedDate;
                    register.Note1 = item.note1;
                    register.Note2 = item.note2;
                    register.ModifiedBy = acc.ID;
                    register.ModifiedDate = DateTime.Now;
                    con.SaveChanges();
                }

                return register;
            }
        }

        public static RegisterProduct Update(RegisterProduct item)
        {
            using (var con = new inventorymanagementEntities())
            {
                var register = con.RegisterProducts.Where(x => x.ID == item.ID).FirstOrDefault();

                if (register != null)
                {
                    register.Customer = item.Customer;
                    register.Status = item.Status;
                    register.Quantity = item.Quantity;
                    register.ExpectedDate = item.ExpectedDate;
                    register.Note1 = item.Note1;
                    register.ModifiedBy = item.ModifiedBy;
                    register.ModifiedDate = item.ModifiedDate;
                    con.SaveChanges();
                }

                return register;
            }
        }

        public static void Update(List<RegisterProductSession> data, tbl_Account acc)
        {
            using (var con = new inventorymanagementEntities())
            {
                foreach (var item in data)
                {

                    var register = con.RegisterProducts.Where(x => x.ID == item.id).FirstOrDefault();

                    if (register != null)
                    {
                        register.Customer = item.customer;
                        register.Status = item.status;
                        register.Quantity = item.quantity;
                        register.ExpectedDate = item.expectedDate;
                        register.Note1 = item.note1;
                        register.Note2 = item.note2;
                        register.ModifiedBy = acc.ID;
                        register.ModifiedDate = DateTime.Now;
                        con.SaveChanges();
                    }
                }
            }
        }

        public static void Delete(int registerID)
        {
            using (var con = new inventorymanagementEntities())
            {
                var register = con.RegisterProducts.Where(x => x.ID == registerID).FirstOrDefault();

                if (register != null)
                {
                    con.RegisterProducts.Remove(register);
                    con.SaveChanges();
                }
            }
        }

        public static List<RegisterProductList> Filter(RegisterProductFilterModel filter, ref PaginationMetadataModel page)
        {
            using (var con = new inventorymanagementEntities())
            {
                var data = con.RegisterProducts.Where(x => 1 == 1);
                var staff = con.tbl_Account.Where(x => 1 == 1);

                #region Thực hiện trích xuất dữ liệu theo bộ lọc
                // Filter theo ký tự search
                if (!String.IsNullOrEmpty(filter.search))
                {
                    data = data.Where(x =>
                        x.Title.Contains(filter.search) ||
                        x.Customer.Contains(filter.search) ||
                        x.SKU.Contains(filter.search)
                    );
                }
                // Filter theo category
                if (filter.category > 0)
                {
                    var parentCatogory = con.tbl_Category.Where(x => x.ID == filter.category).FirstOrDefault();
                    var catogoryFilter = CategoryController.getCategoryChild(parentCatogory).Select(x => x.ID).ToList();

                    var product = con.tbl_Product
                        .Where(x =>
                            catogoryFilter.Contains(
                                x.CategoryID.HasValue ? x.CategoryID.Value : 0
                                )
                        );

                    data = data
                        .Join(
                            product,
                            d => d.ProductID,
                            p => p.ID,
                            (d, p) => d
                        );
                }
                // Filter theo trang thái
                if (filter.status != 0)
                {
                    data = data.Where(x => x.Status == filter.status);
                }
                // Filter theo người khở tạo
                if (!String.IsNullOrEmpty(filter.createdBy))
                {
                    staff = staff.Where(x => x.Username == filter.createdBy);

                    data = data
                        .Join(
                            staff,
                            d => d.CreatedBy,
                            s => s.ID,
                            (d, s) => d
                        );
                }
                // Filter Order Created Date
                if (filter.fromDate.HasValue && filter.toDate.HasValue)
                {
                    data = data.Where(x =>
                        x.CreatedDate >= filter.fromDate.Value &&
                        x.CreatedDate <= filter.toDate.Value
                    );
                }
                // Filter color
                if (!String.IsNullOrEmpty(filter.color))
                {
                    data = data.Where(x => x.Color.Contains(filter.color));
                }

                // Filter color
                if (!String.IsNullOrEmpty(filter.size))
                {
                    data = data.Where(x => x.Size.Contains(filter.size));
                }

                // Filter theo đã chọn
                if (filter.selected)
                {
                    var session = SessionController.getRegisterProductSession(filter.account)
                        .Select(x => x.id)
                        .ToList();
                    data = data.Where(x => session.Contains(x.ID));

                }
                #endregion

                #region Tính toán phân trang
                // Calculate pagination
                page.totalCount = data.Count();
                page.totalPages = (int)Math.Ceiling(page.totalCount / (double)page.pageSize);

                data = data
                    .OrderByDescending(x => x.ID)
                    .Skip((page.currentPage - 1) * page.pageSize)
                    .Take(page.pageSize);
                #endregion

                var result = data
                    .Join(
                        staff,
                        d => d.CreatedBy,
                        s => s.ID,
                        (d, s) => new {
                            register = d,
                            staff = s
                        }
                    )
                    .OrderByDescending(o => o.register.ID)
                    .ToList();

                return result
                    .Select(x => {
                        var statusName = "";

                        switch (x.register.Status)
                        {
                            case (int)RegisterProductStatus.Approve:
                                statusName = "Đã duyệt";
                                break;
                            case (int)RegisterProductStatus.Ordering:
                                statusName = "Đã đặt hàng";
                                break;
                            case (int)RegisterProductStatus.Done:
                                statusName = "Hàng về";
                                break;
                            default:
                                statusName = "Chưa duyệt";
                                break;
                        }

                        return new RegisterProductList()
                        {
                            check = false,
                            id = x.register.ID,
                            customer = x.register.Customer,
                            productID = x.register.ProductID,
                            variableID = x.register.VariableID,
                            sku = x.register.SKU,
                            productStyle = x.register.ProductStyle,
                            status = x.register.Status,
                            statusName = statusName,
                            title = x.register.Title,
                            image = x.register.Image,
                            color = x.register.Color,
                            size = x.register.Size,
                            numberchild = x.register.NumberChild,
                            quantity = x.register.Quantity,
                            expectedDate = x.register.ExpectedDate,
                            note1 = x.register.Note1,
                            note2 = x.register.Note2,
                            staffID = x.register.CreatedBy,
                            staffName = x.staff.Username,
                            createdDate = x.register.CreatedDate
                        };
                    })
                    .ToList();
            }
        }

        // Láy thông tin nhập kho để insert vào bảng lịch sử nhận hàng
        public static List<GoodsReceipt> GetGoodsReceiptInfo(string sku, DateTime registerDate)
        {
            using (var con = new inventorymanagementEntities())
            {
                // Lấy thông tin sản phẩm vừa được nhập kho sau ngày đăng ký nhập hàng
                var stock = con.tbl_StockManager
                    .Where(x => x.Status == 1)
                    .Where(x => x.SKU.StartsWith(sku))
                    .Where(x => x.CreatedDate >= registerDate);

                var product = con.tbl_Product.Where(x => 1 == 1);
                var variable = con.tbl_ProductVariable.Where(x => x.SKU.StartsWith(sku));
                var data = new List<GoodsReceipt>();

                if (variable.Count() > 0)
                {
                    var color = con.tbl_ProductVariableValue.Where(x => x.ProductvariableSKU.StartsWith(sku))
                        .Join(
                            con.tbl_VariableValue.Where(x => x.VariableID == 1),
                            pvv => pvv.VariableValueID.Value,
                            vv => vv.ID,
                            (pvv, vv) => new { sku = pvv.ProductvariableSKU, colorName = vv.VariableValue }
                        );

                    var size = con.tbl_ProductVariableValue.Where(x => x.ProductvariableSKU.StartsWith(sku))
                        .Join(
                            con.tbl_VariableValue.Where(x => x.VariableID == 2),
                            pvv => pvv.VariableValueID.Value,
                            vv => vv.ID,
                            (pvv, vv) => new { sku = pvv.ProductvariableSKU, sizeName = vv.VariableValue }
                        );

                    data = product
                        .Join(
                            variable,
                            p => p.ID,
                            v => v.ProductID,
                            (p, v) => new {
                                product = p,
                                variable = v
                            }
                        )
                        .Join(
                            stock,
                            tem1 => tem1.variable.SKU,
                            s => s.SKU,
                            (tem1, s) => new {
                                product = tem1.product,
                                variable = tem1.variable,
                                stock = s
                            }
                        )
                        .GroupJoin(
                            color,
                            tem2 => tem2.variable.SKU,
                            c => c.sku,
                            (tem2, c) => new {
                                product = tem2.product,
                                variable = tem2.variable,
                                stock = tem2.stock,
                                color = c
                            }
                        )
                        .SelectMany(
                            x => x.color.DefaultIfEmpty(),
                            (parent, child) => new {
                                product = parent.product,
                                variable = parent.variable,
                                stock = parent.stock,
                                color = child
                            }
                        )
                        .GroupJoin(
                            size,
                            tem3 => tem3.variable.SKU,
                            s => s.sku,
                            (tem3, s) => new {
                                product = tem3.product,
                                variable = tem3.variable,
                                stock = tem3.stock,
                                color = tem3.color,
                                size = s
                            }
                        )
                        .SelectMany(
                            x => x.size.DefaultIfEmpty(),
                            (parent, child) => new GoodsReceipt()
                            {
                                stockID = parent.stock.ID,
                                productID = parent.product.ID,
                                variableID = parent.variable.ID,
                                sku = parent.variable.SKU,
                                title = parent.product.ProductTitle,
                                image = parent.variable.Image,
                                color = parent.color != null ? parent.color.colorName : String.Empty,
                                size = child != null ? child.sizeName : String.Empty,
                                quantity = parent.stock.Quantity.Value,
                                receivedDate = parent.stock.CreatedDate.Value
                            }
                        )
                        .Distinct()
                        .OrderByDescending(o => o.stockID)
                        .ToList();
                }
                else
                {
                    data = product.Where(x => x.ProductSKU == sku)
                        .Join(
                            stock,
                            p => p.ProductSKU,
                            s => s.SKU,
                            (p, s) => new GoodsReceipt()
                            {
                                stockID = s.ID,
                                productID = p.ID,
                                variableID = 0,
                                sku = p.ProductSKU,
                                title = p.ProductTitle,
                                image = p.ProductImage,
                                color = String.Empty,
                                size = String.Empty,
                                quantity = s.Quantity.Value,
                                receivedDate = s.CreatedDate.Value
                            }
                        )
                        .Distinct()
                        .OrderByDescending(o => o.stockID)
                        .ToList();
                }

                var receivedHistory = con.ReceivedProductHistories
                    .Where(x => x.SKU.StartsWith(sku))
                    .Select(x => new GoodsReceipt()
                    {
                        stockID = x.StockID,
                        productID = x.ProductID,
                        variableID = x.VariableID,
                        sku = x.SKU,
                        title = x.Title,
                        image = x.Image,
                        color = x.Color,
                        size = x.Size,
                        quantity = x.Quantity,
                        receivedDate = x.ReceivedDate
                    })
                    .OrderByDescending(x => x.stockID)
                    .ToList();

                var result = data
                    .Where(x => receivedHistory.All(y => !(y.stockID == x.stockID && y.sku == x.sku)))
                    .Select(x => new GoodsReceipt()
                    {
                        stockID = x.stockID,
                        productID = x.productID,
                        variableID = x.variableID,
                        sku = x.sku,
                        title = x.title,
                        image = x.image,
                        color = x.color,
                        size = x.size,
                        quantity = x.quantity,
                        receivedDate = x.receivedDate
                    })
                    .ToList();

                return result;
            }
        }

        // Insert thông tin nhận hàng
        public static void InsertReceivedProduct(List<ReceivedProductHistory> histories, tbl_Account account)
        {
            using (var con = new inventorymanagementEntities())
            {
                foreach (var item in histories)
                {
                    var history = con.ReceivedProductHistories
                        .Where(x => x.RegisterID == item.RegisterID)
                        .Where(x => x.StockID == item.StockID)
                        .FirstOrDefault();

                    if (history == null)
                    {
                        var now = DateTime.Now;

                        item.CreatedBy = account.ID;
                        item.CreatedDate = now;
                        item.ModifiedBy = account.ID;
                        item.ModifiedDate = now;

                        con.ReceivedProductHistories.Add(item);
                        con.SaveChanges();
                    }
                }
            }
        }

        // Lấy thông tin lịch sử nhận hàng
        public static List<ReceivedProductHistory> GetReceivedProductHistory(int registerID, string sku = "")
        {
            using (var con = new inventorymanagementEntities())
            {
                var data = con.ReceivedProductHistories
                    .Where(x => x.RegisterID == registerID);

                if (!String.IsNullOrEmpty(sku))
                    data = data.Where(x => x.SKU == sku);

                return data
                    .OrderByDescending(o => o.ID)
                    .ToList();
            }
        }
    }

    public class RegisterProductList
    {
        public bool check { get; set; }
        public int id { get; set; }
        public string customer { get; set; }
        public int productID { get; set; }
        public int variableID { get; set; }
        public string sku { get; set; }
        public int productStyle { get; set; }
        public int status { get; set; }
        public string statusName { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public string color { get; set; }
        public string size { get; set; }
        public int numberchild { get; set; }
        public int quantity { get; set; }
        public DateTime? expectedDate { get; set; }
        public string note1 { get; set; }
        public string note2 { get; set; }
        public int staffID { get; set; }
        public string staffName { get; set; }
        public System.DateTime createdDate { get; set; }
    }

    public class GoodsReceipt
    { 
        public int stockID { get; set; }
        public int productID { get; set; }
        public int variableID { get; set; }
        public string sku { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public string color { get; set; }
        public string size { get; set; }
        public double quantity { get; set; }
        public DateTime receivedDate { get; set; }
    }
}