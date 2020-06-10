using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IM_PJ.Models;

namespace IM_PJ.Controllers
{
    public class WhiteDomainController
    {
        public static tbl_WhiteDomain insert(string Domain,string Created)
        {
            using (var db = new inventorymanagementEntities())
            {
                tbl_WhiteDomain wd = new tbl_WhiteDomain();
                wd.Domain = Domain;
                wd.IsHidden = false;
                wd.CreatedBy = Created;
                wd.CreatedDate = DateTime.Now;
                db.tbl_WhiteDomain.Add(wd);
                db.SaveChanges();
                return wd;
            }
        }

        public static int update(int ID, string Domain, string Created)
        {
            using (var db = new inventorymanagementEntities())
            {
                var wd = db.tbl_WhiteDomain.Where(x => x.ID == ID).FirstOrDefault();
                if(wd != null)
                {
                    wd.Domain = Domain;
                    wd.ModifiedBy = Created;
                    wd.ModifiedDate = DateTime.Now;
                   int i= db.SaveChanges();
                    return i;
                }
                return 0;
            }
        }

        public static List<tbl_WhiteDomain> GetAll()
        {
            using (var db = new inventorymanagementEntities())
            {
                var wd = db.tbl_WhiteDomain.Where(x => x.IsHidden == false).ToList();
                if (wd.Count() > 0)
                    return wd;
                return null;
            }
        }

        public static tbl_WhiteDomain GetByID(int ID)
        {
            using (var db = new inventorymanagementEntities())
            {
                var wd = db.tbl_WhiteDomain.Where(x => x.ID == ID).FirstOrDefault();
                if (wd != null)
                    return wd;
                return null;
            }
        }
    }
}