using IM_PJ.Models;
using IM_PJ.Models.Pages.thuc_hien_kiem_kho;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Controllers
{
    public class CheckWarehouseController
    {
        #region CRUD
        public static string Insert(int AgentID, int Type, string Note, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_CheckWarehouse ui = new tbl_CheckWarehouse();
                ui.AgentID = AgentID;
                ui.Type = Type;
                ui.Note = Note;
                ui.CreatedDate = CreatedDate;
                ui.CreatedBy = CreatedBy;
                dbe.tbl_CheckWarehouse.Add(ui);
                int kq = dbe.SaveChanges();
                return kq.ToString();
            }
        }       
        #endregion
        #region Select
        public static tbl_CheckWarehouse GetByID(int ID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_CheckWarehouse ai = dbe.tbl_CheckWarehouse.Where(a => a.ID == ID).SingleOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }
        public static List<tbl_CheckWarehouse> GetAll(string s)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_CheckWarehouse> ags = new List<tbl_CheckWarehouse>();
                ags = dbe.tbl_CheckWarehouse.ToList();
                return ags;
            }
        }
        public static List<tbl_CheckWarehouse> GetByAgentID(int AgentID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_CheckWarehouse> ags = new List<tbl_CheckWarehouse>();
                ags = dbe.tbl_CheckWarehouse.Where(a => a.AgentID == AgentID).ToList();
                return ags;
            }
        }        
        #endregion

        public static List<CheckWarehouse> getAll()
        {
            using (var con = new inventorymanagementEntities())
            {
                return con.CheckWarehouses
                    .OrderByDescending(o => o.CreatedDate)
                    .ToList();
            }
        }

        public static bool closeCheckHouse(int id, DateTime modifiedDate, string modifiedBy)
        {
            using (var con = new inventorymanagementEntities())
            {
                var checkHouse = con.CheckWarehouses.Where(x => x.ID == id).FirstOrDefault();

                if (checkHouse != null)
                {
                    checkHouse.Active = false;
                    checkHouse.ModifiedDate = modifiedDate;
                    checkHouse.ModifiedBy = modifiedBy;
                    con.SaveChanges();

                    return true;
                }

                return false;
            }
        }

        // Lấy thông tin kiểm tra kho
        public static CheckWarehouse get(int checkID)
        {
            using (var con = new inventorymanagementEntities())
            {
                return con.CheckWarehouses.Where(x => x.ID == checkID).FirstOrDefault();
            }
        }

        // Lấy danh sách các đợt kiểm tra kho
        public static List<CheckWarehouse> getAllCheckWarehouse()
        {
            using (var con = new inventorymanagementEntities())
            {
                var result = new List<CheckWarehouse>();
                var data = con.CheckWarehouses
                  .OrderByDescending(o => o.CreatedDate)
                  .ToList();

                if (data != null)
                    result.AddRange(data);

                return result;
            }
        }

        // Lấy lịch sử các lần kiểm tra sản phẩm của nhân viên
        public static List<StaffHistoryModel> getStaffHistories(string staff, ref PaginationMetadataModel pagination)
        {
            using (var con = new inventorymanagementEntities())
            {
                var histories = new List<StaffHistoryModel>();
                var data = con.CheckWarehouseDetails
                  .Where(x => x.ModifiedBy == staff)
                  .Join(
                    con.CheckWarehouses,
                    b => b.CheckWarehouseID,
                    h => h.ID,
                    (b, h) => new { header = h, body = b }
                  )
                  .Select(x => new StaffHistoryModel()
                  {
                      checkedName = x.header.Name,
                      sku = x.body.ProductSKU,
                      quantity = x.body.QuantityNew.HasValue ? x.body.QuantityNew.Value : 0,
                      checkedDate = x.body.ModifiedDate.Value
                  })
                  .OrderByDescending(o => o.checkedDate);

                #region Thực hiện phân trang
                // Lấy tổng số record sản phẩm
                pagination.totalCount = data.Count();

                // Calculating Totalpage by Dividing (No of Records / Pagesize)
                pagination.totalPages = (int)Math.Ceiling(pagination.totalCount / (double)pagination.pageSize);

                var result = data
                  .Skip((pagination.currentPage - 1) * pagination.pageSize)
                  .Take(pagination.pageSize)
                  .ToList();
                #endregion

                if (result != null && result.Count() > 0)
                    histories.AddRange(result);

                return histories;
            }
        }

        // Lấy thông tin sản phẩm
        public static tbl_Product getProduct(int checkID, string sku)
        {
            using (var con = new inventorymanagementEntities())
            {
                var product = con.CheckWarehouseDetails
                  .Where(x => x.CheckWarehouseID == checkID)
                  .Where(x => x.ProductSKU == sku)
                  .Join(
                    con.tbl_Product,
                    c => c.ProductID,
                    p => p.ID,
                    (c, p) => p
                  )
                  .FirstOrDefault();

                return product;
            }
        }

        // Cập nhật sô lượng sản phẩm theo đợt kiểm tra
        public static CheckWarehouseDetail updateQuantity(UpdateQuantityModel data)
        {
            try
            {
                using (var con = new inventorymanagementEntities())
                {
                    var product = con.CheckWarehouseDetails
                      .Where(x => x.CheckWarehouseID == data.checkID)
                      .Where(x => x.ProductSKU == data.sku)
                      .FirstOrDefault();

                    if (product != null)
                    {
                        product.QuantityNew = data.quantity;
                        product.ModifiedDate = data.updateDate;
                        product.ModifiedBy = data.staff;

                        con.SaveChanges();
                    }

                    return product;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
