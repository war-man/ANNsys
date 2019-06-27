using IM_PJ.Models;
using MB.Extensions;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebUI.Business;

namespace IM_PJ.Controllers
{
    public class RegisterController
    {
        #region CRUD
        public static int Insert(Register register)
        {
            using (var connect = new inventorymanagementEntities())
            {
                Register newRegister = new Register();
                newRegister.ID = GetIDNew();
                newRegister.Name = register.Name;
                newRegister.UnSignedName = UnSign.convert(register.Name);
                newRegister.Phone = register.Phone;
                newRegister.Address = register.Address;
                newRegister.ProvinceID = register.ProvinceID;
                newRegister.Note = register.Note;
                newRegister.ProductCategory = register.ProductCategory;
                newRegister.Status = register.Status;
                newRegister.UserID = register.UserID;
                newRegister.CreatedDate = DateTime.Now;
                newRegister.Referer = register.Referer;

                connect.Registers.Add(newRegister);
                connect.SaveChanges();
                return newRegister.ID;
            }
        }
        public static int Update(Register register)
        {
            using (var connect = new inventorymanagementEntities())
            {
                Register target = connect.Registers
                    .Where(x => x.ID == register.ID)
                    .SingleOrDefault();

                if (target != null)
                {
                    target.Name = register.Name;
                    target.UnSignedName = UnSign.convert(register.Name);
                    target.Phone = register.Phone;
                    target.ProvinceID = register.ProvinceID;
                    target.Note = register.Note;
                    target.ProductCategory = register.ProductCategory;
                    target.Status = register.Status;
                    target.UserID = register.UserID;
                    target.CreatedDate = register.CreatedDate;
                    target.ModifiedDate = DateTime.Now;
                    target.Referer = register.Referer;

                    connect.SaveChanges();
                }
            }
            return register.ID;
        }
        public static int UpdateStatus(int id, int status)
        {
            using (var connect = new inventorymanagementEntities())
            {
                Register target = connect.Registers
                    .Where(x => x.ID == id)
                    .SingleOrDefault();

                if (target != null)
                {
                    target.Status = status;

                    connect.SaveChanges();
                }

                return target.ID;
            }
            
        }
        public static string deleteRegister(int id)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                Register ui = dbe.Registers.Where(a => a.ID == id).SingleOrDefault();
                if (ui != null)
                {
                    dbe.Registers.Remove(ui);
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }
        public static int UpdateUser(int id, int userid)
        {
            using (var connect = new inventorymanagementEntities())
            {
                Register target = connect.Registers
                    .Where(x => x.ID == id)
                    .SingleOrDefault();

                if (target != null)
                {
                    target.Status = 3;
                    target.UserID = userid;

                    connect.SaveChanges();
                }

                return target.ID;
            }

        }
        #endregion
        #region Select    
        public static Register GetByID(int ID)
        {
            using (var connect = new inventorymanagementEntities())
            {
                return connect.Registers
                        .Where(x => x.ID == ID)
                        .SingleOrDefault();
            }
        }
        public static Register GetByPhone(string phone)
        {
            using (var connect = new inventorymanagementEntities())
            {
                return connect.Registers
                        .Where(x => x.Phone == phone)
                        .OrderByDescending(x =>x.ID)
                        .FirstOrDefault();
            }
        }
        public static Register GetLast(string phone)
        {
            using (var connect = new inventorymanagementEntities())
            {
                return connect.Registers
                        .OrderByDescending(x => x.ID)
                        .FirstOrDefault();
            }
        }
        public static List<RegisterOut> Filter(string TextSearch, int ProvinceID, int UserID, int Status, string Referer, string CreatedDate)
        {
            var result = new List<RegisterOut>();

            using (var con = new inventorymanagementEntities())
            {
                DateTime fromdate = DateTime.Today;
                DateTime todate = DateTime.Now;

                switch (CreatedDate)
                {
                    case "today":
                        fromdate = DateTime.Today;
                        todate = DateTime.Now;
                        break;
                    case "yesterday":
                        fromdate = fromdate.AddDays(-1);
                        todate = DateTime.Today;
                        break;
                    case "beforeyesterday":
                        fromdate = DateTime.Today.AddDays(-2);
                        todate = DateTime.Today.AddDays(-1);
                        break;
                    case "week":
                        int days = DateTime.Today.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)DateTime.Today.DayOfWeek;
                        fromdate = fromdate.AddDays(-days + 1);
                        todate = DateTime.Now;
                        break;
                    case "month":
                        fromdate = new DateTime(fromdate.Year, fromdate.Month, 1);
                        todate = DateTime.Now;
                        break;
                    case "7days":
                        fromdate = DateTime.Today.AddDays(-6);
                        todate = DateTime.Now;
                        break;
                    case "30days":
                        fromdate = DateTime.Today.AddDays(-29);
                        todate = DateTime.Now;
                        break;
                }

                var registers = con.Registers
                    .Where(x =>
                        string.IsNullOrEmpty(TextSearch) ||
                        (
                            !string.IsNullOrEmpty(TextSearch) &&
                            (
                                x.Name.Contains(TextSearch) ||
                                x.UnSignedName.Contains(TextSearch) ||
                                x.Address.Contains(TextSearch) ||
                                x.Note.Contains(TextSearch) ||
                                x.Phone == TextSearch
                            )
                        )
                    )
                    .Where(x => ProvinceID <= 0 || (ProvinceID > 0 && x.ProvinceID == ProvinceID))
                    .Where(x => UserID <= 0 || (UserID > 0  && x.UserID == UserID))
                    .Where(x => Status <= 0 || (Status > 0 && x.Status == Status))
                    .Where(x => string.IsNullOrEmpty(Referer) || (!string.IsNullOrEmpty(Referer) && x.Referer == Referer))
                    .Where(x => string.IsNullOrEmpty(CreatedDate) || (!string.IsNullOrEmpty(CreatedDate) && x.CreatedDate >= fromdate && x.CreatedDate <= todate))
                    .OrderByDescending(x => x.CreatedDate);

                // Get customer of province
                result = registers
                    .GroupJoin(
                        con.tbl_Province,
                        reg => reg.ProvinceID,
                        pro => pro.ID,
                        (register, province) => new { register, province }
                    )
                    .SelectMany(
                        x => x.province.DefaultIfEmpty(),
                        (parent, child) => new RegisterOut()
                        {
                            ID = parent.register.ID,
                            Name = parent.register.Name,
                            Phone = parent.register.Phone,
                            Address = parent.register.Address,
                            Note = parent.register.Note,
                            ProductCategory = parent.register.ProductCategory,
                            CreatedDate = parent.register.CreatedDate.HasValue ? parent.register.CreatedDate.Value : DateTime.Now,
                            ProvinceID = parent.register.ProvinceID.HasValue ? parent.register.ProvinceID.Value.ToString() : String.Empty,
                            Status = parent.register.Status.HasValue ? parent.register.Status.Value : 0,
                            UserID = parent.register.UserID.HasValue ? parent.register.UserID.Value : 0,
                            ProvinceName = child != null ? child.ProvinceName : String.Empty,
                            Referer = parent.register.Referer
                        }
                    )
                    .OrderByDescending(x => x.CreatedDate)
                    .ToList();

                result
                    .GroupJoin(
                        con.tbl_Account,
                        reg => reg.UserID,
                        acc => acc.ID,
                        (register, account) => new { register, account }
                    )
                    .SelectMany(
                        x => x.account.DefaultIfEmpty(),
                        (parent, child) =>
                        {
                            parent.register.User = child != null ? child.Username : String.Empty;

                            return parent.register;
                        }
                    )
                    .OrderByDescending(x => x.CreatedDate)
                    .ToList();
            }

            return result;
        }
        private static int GetIDNew()
        {
            using (var connect = new inventorymanagementEntities())
            {
                try
                {
                    int idNew = connect.Registers
                    .OrderByDescending(x => x.ID)
                    .FirstOrDefault()
                    .ID;

                    return idNew + 1;
                }
                catch (Exception)
                {
                    return 1;
                }
            }
        }
        public class RegisterOut
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
            public string ProvinceID { get; set; }
            public string ProvinceName { get; set; }
            public string Note { get; set; }
            public string ProductCategory { get; set; }
            public int Status { get; set; }
            public int UserID { get; set; }
            public string User { get; set; }
            public DateTime CreatedDate { get; set; }
            public string Referer { get; set; }
        }
        #endregion
    }
}