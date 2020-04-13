using System;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web;
using IM_PJ.Controllers;
using Newtonsoft.Json;
using IM_PJ.Models.Pages.thuc_hien_kiem_kho;
using IM_PJ.Models;
using System.Collections.Generic;

namespace IM_PJ
{
    public partial class thuc_hien_kiem_kho : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["loginHiddenPage"] == null)
                    Response.Redirect("/login-hidden-page");
            }
        }

        [WebMethod]
        public static string getSelect2(string search)
        {
            var results = CheckWarehouseController.getAllCheckWarehouse()
              .Where(x =>
                String.IsNullOrEmpty(search) ||
                (!String.IsNullOrEmpty(search) && x.Name.Contains(search))
              )
              .OrderByDescending(o => o.CreatedDate)
              .Select(x => new {
                  id = x.ID,
                  text = x.Name
              })
              .ToList();

            return JsonConvert.SerializeObject(new
            {
                results = results,
                pagination = new
                {
                    more = false
                }
            });
        }

        [WebMethod]
        public static string getStaffHistories(int draw, int page, int pageSize)
        {
            #region Kiểm tra phải account
            var staff = HttpContext.Current.Request.Headers["staff"];
            var account = AccountController.GetByUsername(staff);

            if (account == null)
                return JsonConvert.SerializeObject(new
                {
                    draw = 1,
                    recordsTotal = 0,
                    recordsFiltered = 0,
                    data = new List<StaffHistoryModel>(),
                });
            #endregion

            var pagination = new PaginationMetadataModel()
            {
                pageSize = pageSize,
                currentPage = page
            };

            var data = CheckWarehouseController.getStaffHistories(staff, ref pagination);

            var index = (page - 1) * pageSize;
            var dataTables = new List<object>();

            foreach (var item in data)
            {
                index += 1;

                var row1 = new
                {
                    index = index,
                    checkedName = item.checkedName,
                    sku = item.sku,
                    quantity = item.quantity,
                    checkedDate = String.Format("{0:dd-MM-yyyy}<br/>{1:hh:mm:ss}", item.checkedDate, item.checkedDate),
                    row = 1
                };
                var row2 = new
                {
                    index = index,
                    checkedName = item.checkedName,
                    sku = item.sku,
                    quantity = item.quantity,
                    checkedDate = String.Format("{0:dd-MM-yyyy}<br/>{1:hh:mm:ss}", item.checkedDate, item.checkedDate),
                    row = 2
                };

                dataTables.Add(row1);
                dataTables.Add(row2);
            }

            return JsonConvert.SerializeObject(new
            {
                draw = draw,
                recordsTotal = pagination.totalCount,
                recordsFiltered = pagination.totalCount,
                data = dataTables,
            });
        }

        [WebMethod]
        public static string checkProduct(int checkID, string sku)
        {
            #region Kiểm tra phải account
            var staff = HttpContext.Current.Request.Headers["staff"];
            var account = AccountController.GetByUsername(staff);

            if (account == null)
                return JsonConvert.SerializeObject(new
                {
                    success = false,
                    statusCode = 403,
                    message = String.Format("Không tồn tại account {0}.", staff)
                });
            #endregion

            #region Kiểm tra input ID của của phiên kiểm tra
            if (checkID <= 0)
            {
                return JsonConvert.SerializeObject(new
                {
                    success = false,
                    statusCode = 400,
                    message = "Dữ liệu checkID truyền không đúng.",
                    error = new
                    {
                        checkID = "Giá trị checkID > 0."
                    }
                });
            }
            else
            {
                var exists = CheckWarehouseController.get(checkID) != null;

                if (!exists)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        success = false,
                        statusCode = 400,
                        message = "Kiểm tra sản phẩm không thành công.",
                        error = new
                        {
                            checkID = String.Format("Không tồn tại phiên kiểm kho ID({0}).", checkID)
                        }
                    });
                }
            }
            #endregion

            #region Kiểm tra input sku của sản phẩm
            if (String.IsNullOrEmpty(sku))
            {
                return JsonConvert.SerializeObject(new
                {
                    success = false,
                    statusCode = 400,
                    message = "Dữ liệu sku truyền không đúng.",
                    error = new
                    {
                        sku = "sku là dữ liệu bắt buộc."
                    }
                });
            }
            #endregion

            var product = CheckWarehouseController.getProduct(checkID, sku);

            if (product != null)
                return JsonConvert.SerializeObject(new
                {
                    success = false,
                    statusCode = 200,
                    message = "",
                    data = product
                });
            else
                return JsonConvert.SerializeObject(new
                {
                    success = false,
                    statusCode = 204,
                    message = "Không tồn tại sản phẩm"
                });
        }

        [WebMethod]
        public static string updateQuantity(int checkID, string sku, int quantity)
        {
            #region Kiểm tra phải account
            var staff = HttpContext.Current.Request.Headers["staff"];
            var account = AccountController.GetByUsername(staff);

            if (account == null)
                return JsonConvert.SerializeObject(new
                {
                    success = false,
                    statusCode = 403,
                    message = String.Format("Không tồn tại account {0}.", staff)
                });
            #endregion

            #region Kiểm tra input ID của của phiên kiểm tra
            if (checkID <= 0)
            {
                return JsonConvert.SerializeObject(new
                {
                    success = false,
                    statusCode = 400,
                    message = "Dữ liệu checkID truyền không đúng.",
                    error = new
                    {
                        checkID = "Giá trị checkID > 0."
                    }
                });
            }
            else
            {
                var exists = CheckWarehouseController.get(checkID) != null;

                if (!exists)
                {
                    return JsonConvert.SerializeObject(new
                    {
                        success = false,
                        statusCode = 400,
                        message = "Kiểm tra sản phẩm không thành công.",
                        error = new
                        {
                            checkID = String.Format("Không tồn tại phiên kiểm kho ID({0}).", checkID)
                        }
                    });
                }
            }
            #endregion

            #region Kiểm tra input sku của sản phẩm
            if (String.IsNullOrEmpty(sku))
            {
                return JsonConvert.SerializeObject(new
                {
                    success = false,
                    statusCode = 400,
                    message = "Dữ liệu sku truyền không đúng.",
                    error = new
                    {
                        sku = "sku là dữ liệu bắt buộc."
                    }
                });
            }
            #endregion

            #region Kiểm tra input số lượng sản phẩm
            if (quantity <= 0)
            {
                return JsonConvert.SerializeObject(new
                {
                    success = false,
                    statusCode = 400,
                    message = "Dữ liệu quantity truyền không đúng.",
                    error = new
                    {
                        quantity = "Giá trị quantity >= 0."
                    }
                });
            }
            #endregion

            try
            {
                var data = new UpdateQuantityModel()
                {
                    checkID = checkID,
                    sku = sku,
                    quantity = quantity,
                    updateDate = DateTime.Now,
                    staff = staff
                };
                var product = CheckWarehouseController.updateQuantity(data);

                if (product != null)
                    return JsonConvert.SerializeObject(new
                        {
                            success = false,
                            statusCode = 200,
                            message = "",
                            data = product
                        });
                else
                    return JsonConvert.SerializeObject(new
                    {
                        success = false,
                        statusCode = 204,
                        message = "Không tồn tại sản phẩm"
                    });
            }
            catch (Exception)
            {
                return JsonConvert.SerializeObject(new
                {
                    success = false,
                    statusCode = 500,
                    message = "Đã xảy ra lỗi tại server"
                });
            }
        }
    }
}