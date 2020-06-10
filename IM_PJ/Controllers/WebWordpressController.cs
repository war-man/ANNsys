using IM_PJ.Models;
using MB.Extensions;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using WebUI.Business;

namespace IM_PJ.Controllers
{
    public class WebWordpressController
    {
        public static List<WebWordpress> GetAll()
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<WebWordpress> ags = new List<WebWordpress>();
                ags = dbe.WebWordpresses.Where(x => x.Active == true).OrderByDescending(x => x.ID).ToList();
                return ags;
            }
        }
    }
}