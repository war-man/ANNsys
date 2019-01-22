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
    public class AccountController
    {
        #region CRUD
        public static int Insert(int AgentID, string Username, string Email, string Password, int RoleID, int Status, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Account a = new tbl_Account();
                a.AgentID = AgentID;
                a.Username = Username;
                a.Email = Email;
                a.Password = PJUtils.Encrypt("userpass", Password);
                a.RoleID = RoleID;
                a.Status = Status;
                a.CreatedDate = CreatedDate;
                a.CreatedBy = CreatedBy;
                dbe.tbl_Account.Add(a);
                dbe.SaveChanges();
                int kq = a.ID;
                return kq;
            }
        }
        public static string updatestatus(int ID, int status, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new inventorymanagementEntities())
            {

                var a = dbe.tbl_Account.Where(ac => ac.ID == ID).SingleOrDefault();
                if (a != null)
                {
                    a.Status = status;
                    a.ModifiedBy = ModifiedBy;
                    a.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }
        public static string updateEmail(int ID, string Email)
        {
            using (var dbe = new inventorymanagementEntities())
            {

                var a = dbe.tbl_Account.Where(ac => ac.ID == ID).SingleOrDefault();
                if (a != null)
                {
                    a.Email = Email;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }

        public static string UpdateRole(int ID, int roleid, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new inventorymanagementEntities())
            {

                var a = dbe.tbl_Account.Where(ac => ac.ID == ID).SingleOrDefault();
                if (a != null)
                {
                    a.RoleID = roleid;
                    a.ModifiedBy = ModifiedBy;
                    a.ModifiedDate = ModifiedDate;
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    string kq = dbe.SaveChanges().ToString();
                    return kq;
                }
                else
                    return null;
            }
        }

        public static string UpdatePassword(int ID, string Password)
        {
            using (var dbe = new inventorymanagementEntities())
            {

                var a = dbe.tbl_Account.Where(ac => ac.ID == ID).SingleOrDefault();
                if (a != null)
                {
                    a.Password = PJUtils.Encrypt("userpass", Password);
                    dbe.Configuration.ValidateOnSaveEnabled = false;
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                return null;
            }
        }
        #endregion
        #region Select        
        public static List<tbl_Account> GetAll(string s)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_Account> las = new List<tbl_Account>();
                las = dbe.tbl_Account.Where(a => a.Username.Contains(s) && a.RoleID != 0).OrderByDescending(a => a.RoleID).ThenByDescending(a => a.CreatedDate).ToList();
                return las;
            }
        }
        public static List<tbl_Account> GetAllByAgentID(string s, int AgentID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_Account> las = new List<tbl_Account>();
                las = dbe.tbl_Account.Where(a => a.Username.Contains(s) && a.AgentID == AgentID && a.RoleID != 0).OrderByDescending(a => a.RoleID).ThenByDescending(a => a.CreatedDate).ToList();
                return las;
            }
        }
        public static List<tbl_Account> GetAllByAgentIDNotManager(string s, int AgentID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_Account> las = new List<tbl_Account>();
                las = dbe.tbl_Account.Where(a => a.Username.Contains(s) && a.AgentID == AgentID && a.RoleID > 1).OrderByDescending(a => a.RoleID).ThenByDescending(a => a.CreatedDate).ToList();
                return las;
            }
        }


        public static List<tbl_Account> SearchAgent(int AgentID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_Account> las = new List<tbl_Account>();
                las = dbe.tbl_Account.Where(x => x.AgentID == AgentID).ToList();
                return las;
            }
        }
        public static List<tbl_Account> GetUserAll(string s)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_Account> las = new List<tbl_Account>();
                las = dbe.tbl_Account.Where(a => a.Username.Contains(s) && a.RoleID == 1).OrderByDescending(a => a.RoleID).ThenByDescending(a => a.CreatedDate).ToList();
                if (las.Count > 0)
                {
                    return las;
                }
                else return null;
            }
        }
        public static List<tbl_Account> GetAllOrderDesc(string s)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_Account> las = new List<tbl_Account>();
                las = dbe.tbl_Account.Where(a => a.Username.Contains(s) && a.RoleID != 0).OrderByDescending(a => a.ID).ToList();
                if (las.Count > 0)
                {
                    return las;
                }
                else return null;
            }
        }
        public static List<tbl_Account> GetAllNotSearch()
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_Account> las = new List<tbl_Account>();
                las = dbe.tbl_Account.ToList();
                if (las.Count > 0)
                {
                    return las;
                }
                else return las;
            }
        }
        public static List<tbl_Account> GetAllByRoleID(int RoleID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_Account> las = new List<tbl_Account>();
                las = dbe.tbl_Account.Where(a => a.RoleID == RoleID).ToList();
                if (las.Count > 0)
                {
                    return las;
                }
                else return las;
            }
        }
        public static tbl_Account GetByID(int ID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Account acc = dbe.tbl_Account.Where(a => a.ID == ID).FirstOrDefault();
                if (acc != null)
                    return acc;
                else
                    return null;
            }
        }

        public static tbl_Account GetByUsername(string Username)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Account acc = dbe.tbl_Account.Where(a => a.Username == Username).FirstOrDefault();
                if (acc != null)
                    return acc;
                else
                    return null;
            }
        }
        public static tbl_Account GetByUsernameEmail(string Username, string Email)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Account acc = dbe.tbl_Account.Where(a => a.Username == Username && a.Email == Email).FirstOrDefault();
                if (acc != null)
                    return acc;
                else
                    return null;
            }
        }
        public static tbl_Account GetByEmail(string Email)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Account acc = dbe.tbl_Account.Where(a => a.Email == Email).FirstOrDefault();
                if (acc != null)
                    return acc;
                else
                    return null;
            }
        }
        public static tbl_Account Login(string Username, string Password)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                Password = PJUtils.Encrypt("userpass", Password);
                tbl_Account acc = dbe.tbl_Account.Where(a => a.Username == Username && a.Password == Password).FirstOrDefault();
                if (acc != null)
                    return acc;
                else
                    return null;
            }
        }
        public static tbl_Account LoginEmail(string Email, string Password)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                Password = PJUtils.Encrypt("userpass", Password);
                tbl_Account acc = dbe.tbl_Account.Where(a => a.Email == Email && a.Password == Password).FirstOrDefault();
                if (acc != null)
                    return acc;
                else
                    return null;
            }
        }
        public static List<UserList> GetAllSql(int roleID, int AgentID, string textsearch)
        {
            var list = new List<UserList>();
            var sql = @"Select u.ID, u.Username, u.Email, u.CreatedDate, u.Status, u.RoleID, i.FullName, a.AgentName from tbl_Account as u";
            sql += " LEFT OUTER JOIN tbl_AccountInfo as i ON u.ID = i.UID";
            sql += " LEFT OUTER JOIN tbl_Agent as a ON u.AgentID = a.ID";
            sql += " WHERE u.RoleID != 0";

            if (!string.IsNullOrEmpty(textsearch))
            {
                sql += " And (u.Username like N'%" + textsearch + "%' OR i.FullName like N'%" + textsearch + "%')";
            }
            if (AgentID > 0)
            {
                sql += " AND u.AgentID = " + AgentID;
            }
            if (roleID > 0)
            {
                sql += " AND u.RoleID = " + roleID;
            }
            sql += " Order By u.ID desc";

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int i = 1;
            while (reader.Read())
            {
                var entity = new UserList();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["Username"] != DBNull.Value)
                    entity.Username = reader["Username"].ToString();
                if (reader["Email"] != DBNull.Value)
                    entity.Email = reader["Email"].ToString();
                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt(0);
                if (reader["RoleID"] != DBNull.Value)
                    entity.RoleID = reader["RoleID"].ToString().ToInt(0);
                if (reader["FullName"] != DBNull.Value)
                    entity.FullName = reader["FullName"].ToString();
                if (reader["AgentName"] != DBNull.Value)
                    entity.AgentName = reader["AgentName"].ToString();
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                i++;
                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        public static List<UserList> GetAllUser()
        {
            var list = new List<UserList>();
            var sql = @"Select u.ID, u.Username, u.Email, u.CreatedDate, u.Status, u.RoleID, i.FullName from tbl_Account as u";
            sql += " LEFT OUTER JOIN tbl_AccountInfo as i ON u.ID = i.UID";
            sql += " Order By u.ID desc";

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql);
            int i = 1;
            while (reader.Read())
            {
                var entity = new UserList();
                if (reader["ID"] != DBNull.Value)
                    entity.ID = reader["ID"].ToString().ToInt(0);
                if (reader["Username"] != DBNull.Value)
                    entity.Username = reader["Username"].ToString();
                if (reader["Email"] != DBNull.Value)
                    entity.Email = reader["Email"].ToString();
                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt(0);
                if (reader["RoleID"] != DBNull.Value)
                    entity.RoleID = reader["RoleID"].ToString().ToInt(0);
                if (reader["FullName"] != DBNull.Value)
                    entity.FullName = reader["FullName"].ToString();
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                i++;
                list.Add(entity);
            }
            reader.Close();
            return list;
        }
        #endregion
        public class UserList
        {
            public int ID { get; set; }
            public string Username { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public int AgentID { get; set; }
            public string AgentName { get; set; }
            public int RoleID { get; set; }
            public int Status { get; set; }
            public DateTime CreatedDate { get; set; }
        }
    }
}