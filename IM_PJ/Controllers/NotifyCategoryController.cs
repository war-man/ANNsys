using IM_PJ.Models;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Controllers
{
    public class NotifyCategoryController
    {
        #region CRUD
        public static int Insert(string Name, int ParentID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                NotificationCategory ui = new NotificationCategory();
                ui.Name = Name;
                ui.ParentID = ParentID;
                dbe.NotificationCategories.Add(ui);
                int kq = dbe.SaveChanges();
                return ui.ID;
            }
        }
        public static string Update(int ID, string Name, int ParentID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                NotificationCategory ui = dbe.NotificationCategories.Where(a => a.ID == ID).SingleOrDefault();
                if (ui != null)
                {
                    ui.Name = Name;
                    ui.ParentID = ParentID;
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static NotificationCategory GetByID(int ID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                NotificationCategory ai = dbe.NotificationCategories.Where(a => a.ID == ID).SingleOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;
            }
        }
        public static List<NotificationCategory> GetAll()
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<NotificationCategory> ags = new List<NotificationCategory>();
                ags = dbe.NotificationCategories.ToList();
                return ags;
            }
        }
        public static List<NotificationCategory> GetAll(string s)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<NotificationCategory> ags = new List<NotificationCategory>();
                ags = dbe.NotificationCategories.Where(c => c.Name.Contains(s)).ToList();
                return ags;
            }
        }

        public static List<NotificationCategory> GetByParentID(string s, int ParentID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<NotificationCategory> ags = new List<NotificationCategory>();
                ags = dbe.NotificationCategories.Where(a => a.Name.Contains(s) && a.ParentID == ParentID).ToList();
                return ags;
            }
        }
        #endregion
        #region API
        public static List<NotificationCategory> API_GetAllCategory()
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<NotificationCategory> ags = new List<NotificationCategory>();
                ags = dbe.NotificationCategories.ToList();
                return ags;
            }
        }
        public static List<NotificationCategory> API_GetRootCategory()
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<NotificationCategory> ags = new List<NotificationCategory>();
                ags = dbe.NotificationCategories.Where(a => a.ParentID == 0).ToList();
                return ags;
            }
        }
        public static List<NotificationCategory> API_GetByParentID(int ParentID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<NotificationCategory> ags = new List<NotificationCategory>();
                ags = dbe.NotificationCategories.Where(a => a.ParentID == ParentID).ToList();
                return ags;
            }
        }
        #endregion
    }
}