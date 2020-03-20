using IM_PJ.Models;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Controllers
{
    public class PostPublicCategoryController
    {
        #region CRUD
        public static int Insert(string Name, int ParentID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                PostCategory ui = new PostCategory();
                ui.Name = Name;
                ui.ParentID = ParentID;
                dbe.PostCategories.Add(ui);
                int kq = dbe.SaveChanges();
                return ui.ID;
            }
        }
        public static string Update(int ID, string Name, int ParentID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                PostCategory ui = dbe.PostCategories.Where(a => a.ID == ID).SingleOrDefault();
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
        public static PostCategory GetByID(int ID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                PostCategory ai = dbe.PostCategories.Where(a => a.ID == ID).SingleOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;
            }
        }
        public static List<PostCategory> GetAll()
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<PostCategory> ags = new List<PostCategory>();
                ags = dbe.PostCategories.ToList();
                return ags;
            }
        }
        public static List<PostCategory> GetAll(string s)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<PostCategory> ags = new List<PostCategory>();
                ags = dbe.PostCategories.Where(c => c.Name.Contains(s)).ToList();
                return ags;
            }
        }

        public static List<PostCategory> GetByParentID(string s, int ParentID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<PostCategory> ags = new List<PostCategory>();
                ags = dbe.PostCategories.Where(a => a.Name.Contains(s) && a.ParentID == ParentID).ToList();
                return ags;
            }
        }
        #endregion
        #region API
        public static List<PostCategory> API_GetAllCategory()
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<PostCategory> ags = new List<PostCategory>();
                ags = dbe.PostCategories.ToList();
                return ags;
            }
        }
        public static List<PostCategory> API_GetRootCategory()
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<PostCategory> ags = new List<PostCategory>();
                ags = dbe.PostCategories.Where(a => a.ParentID == 0).ToList();
                return ags;
            }
        }
        public static List<PostCategory> API_GetByParentID(int ParentID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<PostCategory> ags = new List<PostCategory>();
                ags = dbe.PostCategories.Where(a => a.ParentID == ParentID).ToList();
                return ags;
            }
        }
        #endregion
    }
}