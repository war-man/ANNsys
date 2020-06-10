using IM_PJ.Models;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace IM_PJ.Controllers
{
    public class CategoryController
    {
        #region CRUD
        public static int Insert(string CategoryName, string CategoryDescription, int CategoryLevel, int ParentID, bool IsHidden,
            DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Category ui = new tbl_Category();
                ui.CategoryName = CategoryName;
                ui.CategoryDescription = CategoryDescription;
                ui.CategoryLevel = CategoryLevel;
                ui.ParentID = ParentID;
                ui.IsHidden = IsHidden;
                ui.CreatedDate = CreatedDate;
                ui.CreatedBy = CreatedBy;
                dbe.tbl_Category.Add(ui);
                int kq = dbe.SaveChanges();
                return ui.ID;
            }
        }
        public static string Update(int ID, string CategoryName, string CategoryDescription, int CategoryLevel, int ParentID, bool IsHidden,
            DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Category ui = dbe.tbl_Category.Where(a => a.ID == ID).SingleOrDefault();
                if (ui != null)
                {
                    ui.CategoryName = CategoryName;
                    ui.CategoryDescription = CategoryDescription;
                    ui.CategoryLevel = CategoryLevel;
                    ui.ParentID = ParentID;
                    ui.IsHidden = IsHidden;
                    ui.ModifiedBy = ModifiedBy;
                    ui.ModifiedDate = ModifiedDate;
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static tbl_Category GetByID(int ID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Category ai = dbe.tbl_Category.Where(a => a.ID == ID).SingleOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }

        public static List<tbl_Category> GetAll()
        {
            using (var con = new inventorymanagementEntities())
            {
                return con.tbl_Category.ToList();
            }
        }

        public static List<tbl_Category> GetAll(string s)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_Category> ags = new List<tbl_Category>();
                ags = dbe.tbl_Category.Where(c => c.CategoryName.Contains(s)).ToList();
                return ags;
            }
        }

        public static List<tbl_Category> GetAllByLevel()
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_Category> ags = new List<tbl_Category>();
                ags = dbe.tbl_Category.Where(c => c.CategoryLevel <=1).ToList();
                return ags;
            }
        }

        public static List<tbl_Category> GetByLevel(int Level)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_Category> ags = new List<tbl_Category>();
                ags = dbe.tbl_Category.Where(c => c.CategoryLevel == Level).ToList();
                return ags;
            }
        }
        public static List<tbl_Category> GetBySearchLevel(string s, int Level)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_Category> ags = new List<tbl_Category>();
                ags = dbe.tbl_Category.Where(c => c.CategoryName.Contains(s) && c.CategoryLevel == Level).ToList();
                return ags;
            }
        }
        public static List<tbl_Category> GetAllWithIsHidden(bool IsHidden)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_Category> ags = new List<tbl_Category>();
                ags = dbe.tbl_Category.Where(a => a.IsHidden == IsHidden).ToList();
                return ags;
            }
        }
        public static List<tbl_Category> GetAllWithIsHiddenLevel(bool IsHidden, int Level)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_Category> ags = new List<tbl_Category>();
                ags = dbe.tbl_Category.Where(a => a.IsHidden == IsHidden && a.CategoryLevel == Level).ToList();
                return ags;
            }
        }
        public static List<tbl_Category> GetByParentID(string s, int ParentID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_Category> ags = new List<tbl_Category>();
                ags = dbe.tbl_Category.Where(a => a.CategoryName.Contains(s) && a.ParentID == ParentID).ToList();
                return ags;
            }
        }
        #endregion
        #region API
        public static List<tbl_Category> API_GetAllCategory()
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_Category> ags = new List<tbl_Category>();
                ags = dbe.tbl_Category.ToList();
                return ags;
            }
        }
        public static List<tbl_Category> API_GetRootCategory()
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_Category> ags = new List<tbl_Category>();
                ags = dbe.tbl_Category.Where(a => a.ParentID == 0).ToList();
                return ags;
            }
        }
        public static List<tbl_Category> API_GetByParentID(int ParentID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_Category> ags = new List<tbl_Category>();
                ags = dbe.tbl_Category.Where(a => a.ParentID == ParentID).ToList();
                return ags;
            }
        }

        public static List<tbl_Category> getCategoryChild(tbl_Category parent)
        {
            using (var con = new inventorymanagementEntities())
            {
                var result = new List<tbl_Category>();
                result.Add(parent);

                var child = con.tbl_Category
                    .Where(x => x.ParentID.Value == parent.ID)
                    .ToList();

                if (child.Count > 0)
                {
                    foreach (var id in child)
                    {
                        result.AddRange(getCategoryChild(id));
                    }
                }

                return result;
            }
        }
        #endregion

        #region Trả về danh sách drop downlist
        public static List<ListItem> getDropDownList()
        {
            var dropDownList = new List<ListItem>();
            var categories = GetByLevel(0);

            foreach (var item in categories)
            {
                dropDownList.AddRange(createListItem(item));
            }

            return dropDownList;
        }

        private static List<ListItem> createListItem(tbl_Category parents, string strLevel = "")
        {
            var result = new List<ListItem>();
            var categories = CategoryController.GetByParentID("", parents.ID);

            result.Add(new ListItem(strLevel + parents.CategoryName, parents.ID.ToString()));

            foreach (var item in categories)
            {
                result.AddRange(createListItem(item, strLevel + "---"));
            }

            return result;
        }
        #endregion
    }
}