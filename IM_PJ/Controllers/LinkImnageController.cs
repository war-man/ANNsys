using IM_PJ.Models;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IM_PJ.Controllers
{
    public class LinkImnageController
    {
        public static List<tbl_LinkImnage> GetAll()
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_LinkImnage> ags = new List<tbl_LinkImnage>();
                ags = dbe.tbl_LinkImnage.ToList();
                return ags;
            }
        }
    }
}