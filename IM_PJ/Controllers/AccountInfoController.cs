using IM_PJ.Models;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IM_PJ.Controllers
{
    public class AccountInfoController
    {
        #region CRUD
        public static string Insert(int UID, string Fullname, int Gender, DateTime BirthDay,
           string Email, string Phone, string Address,
            DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                tbl_AccountInfo ui = new tbl_AccountInfo();
                ui.UID = UID;
                ui.Fullname = Fullname;
                ui.Gender = Gender;
                ui.Birthday = BirthDay;
                ui.Email = Email;
                ui.Phone = Phone;
                ui.Address = Address;
                ui.CreatedDate = CreatedDate;
                ui.CreatedBy = CreatedBy;
                dbe.tbl_AccountInfo.Add(ui);
                int kq = dbe.SaveChanges();
                return kq.ToString();
            }
        }
        public static string Update(int UID, string Fullname, int Gender, DateTime BirthDay,
            string Email, string Phone, string Address, DateTime ModifiedDate, string ModifiedBy, string Note)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                tbl_AccountInfo ui = dbe.tbl_AccountInfo.Where(a => a.UID == UID).SingleOrDefault();
                if (ui != null)
                {
                    ui.Fullname = Fullname;
                    ui.Gender = Gender;
                    ui.Birthday = BirthDay;
                    ui.Email = Email;
                    ui.Phone = Phone;
                    ui.Address = Address;
                    ui.ModifiedBy = ModifiedBy;
                    ui.ModifiedDate = ModifiedDate;
                    ui.Note = Note;
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }
        public static string updateNote(int UID, string Note)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                dbe.Configuration.ValidateOnSaveEnabled = false;
                tbl_AccountInfo ui = dbe.tbl_AccountInfo.Where(a => a.UID == UID).SingleOrDefault();
                if (ui != null)
                {
                    ui.Note = Note;
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static tbl_AccountInfo GetByUserID(int UID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_AccountInfo ai = dbe.tbl_AccountInfo.Where(a => a.UID == UID).SingleOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }
        public static tbl_AccountInfo GetByEmailFP(string Email)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_AccountInfo acc = dbe.tbl_AccountInfo.Where(a => a.Email == Email).SingleOrDefault();
                if (acc != null)
                    return acc;
                else
                    return null;
            }
        }
        public static tbl_AccountInfo GetByPhone(string phone)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_AccountInfo acc = dbe.tbl_AccountInfo.Where(a => a.Phone == phone).SingleOrDefault();
                if (acc != null)
                    return acc;
                else
                    return null;
            }
        }

        public static List<tbl_AccountInfo> GetAll()
        {
            using (var con = new inventorymanagementEntities())
            {
                return con.tbl_AccountInfo.ToList();
            }
        }
        #endregion
    }
}