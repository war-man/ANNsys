using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IM_PJ.Models;

namespace IM_PJ.Controllers
{
    public class ProvinceController
    {
        public static List<tbl_Province> GetAll()
        {
            using (var db = new inventorymanagementEntities())
            {
                var pro = db.tbl_Province.Where(x => x.IsHidden == false).ToList();
                if (pro.Count() > 0)
                    return pro;
                return null;
            }
        }

        public static tbl_Province GetByID(int ID)
        {
            using (var db = new inventorymanagementEntities())
            {
                var pro = db.tbl_Province.Where(x => x.ID == ID).FirstOrDefault();
                if (pro != null)
                    return pro;
                return null;
            }
        }
    }
}