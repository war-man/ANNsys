using IM_PJ.Models;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Controllers
{
    public class CategoryShelfController
    {
        //private static List<CategoryShelf> getByLevel(int level, int parentID = 0)
        //{
        //    using(var con = new inventorymanagementEntities())
        //    {
        //        var all = con.CategoryShelves.Where(x => x.Level == level);

        //        if (parentID > 0)
        //        {
        //            all = all.Where(x => x.ParentID == parentID);
        //        }

        //        return all.OrderBy(o => o.ID).ToList();
        //    }
        //}

        //public static List<CategoryShelf> getFloors()
        //{
        //    return getByLevel((int)ShelfLevel.Floor);
        //}

        //public static List<CategoryShelf> getRows(int parentID)
        //{
        //    return getByLevel((int)ShelfLevel.Row, parentID);
        //}

        //public static List<CategoryShelf> getShelfs(int parentID)
        //{
        //    return getByLevel((int)ShelfLevel.Shelf, parentID);
        //}

        //public static List<CategoryShelf> getFloorShelfs(int parentID)
        //{
        //    return getByLevel((int)ShelfLevel.FloorShelf, parentID);
        //}

        //public static CategoryShelf get(int id)
        //{
        //    using (var con = new inventorymanagementEntities())
        //    {
        //        return con.CategoryShelves.Where(x => x.ID == id).FirstOrDefault();
        //    }
        //}
    }
}