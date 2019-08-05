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
                    register.Note = item.note;
                    register.ModifiedBy = acc.ID;
                    register.ModifiedDate = DateTime.Now;
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
                        register.Note = item.note;
                        register.ModifiedBy = acc.ID;
                        register.ModifiedDate = DateTime.Now;
                        con.SaveChanges();
                    }
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
                                statusName = "Hàng đã về";
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
                            status = x.register.Status,
                            statusName = statusName,
                            title = x.register.Title,
                            image = x.register.Image,
                            color = x.register.Color,
                            size = x.register.Size,
                            quantity = x.register.Quantity,
                            expectedDate = x.register.ExpectedDate,
                            note = x.register.Note,
                            staffID = x.register.CreatedBy,
                            staffName = x.staff.Username,
                            createdDate = x.register.CreatedDate
                        };
                    })
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
        public int status { get; set; }
        public string statusName { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public string color { get; set; }
        public string size { get; set; }
        public int quantity { get; set; }
        public DateTime? expectedDate { get; set; }
        public string note { get; set; }
        public int staffID { get; set; }
        public string staffName { get; set; }
        public System.DateTime createdDate { get; set; }
    }
}