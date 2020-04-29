using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IM_PJ.Models;

namespace IM_PJ.Controllers
{
    public class ProvinceController
    {
        public static List<DeliverySaveAddress> GetAll()
        {
            using (var db = new inventorymanagementEntities())
            {
                var pro = db.DeliverySaveAddresses.Where(x => x.PID == null && x.Type == 0).ToList();
                if (pro.Count() > 0)
                    return pro;
                return null;
            }
        }

        public static DeliverySaveAddress GetByID(int ID)
        {
            using (var db = new inventorymanagementEntities())
            {
                var pro = db.DeliverySaveAddresses.Where(x => x.ID == ID).FirstOrDefault();
                if (pro != null)
                    return pro;
                return null;
            }
        }
    }
}