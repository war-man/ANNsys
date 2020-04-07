using IM_PJ.Models;
using MB.Extensions;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WebUI.Business;

namespace IM_PJ.Controllers
{
    public class UserController
    {
        #region Select        
        public static User getByPhone(string Phone)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                User user = dbe.Users.Where(a => a.Phone == Phone).FirstOrDefault();
                if (user != null)
                    return user;
                else
                    return null;
            }
        }
        #endregion
        
    }
}