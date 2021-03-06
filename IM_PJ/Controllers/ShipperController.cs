﻿using IM_PJ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace IM_PJ.Controllers
{
    public class ShipperController
    {
        public static List<ListItem> getDropDownList()
        {
            var data = new List<ListItem>();
            data.Add(new ListItem(String.Empty, "0"));
            using (var con = new inventorymanagementEntities())
            {
                foreach (var shipper in con.Shippers.ToList())
                {
                    data.Add(new ListItem(shipper.Name, shipper.ID.ToString()));
                }
            }

            return data;
        }
        public static string getShipperNameByID(int shipperID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                Shipper shipper = dbe.Shippers.Where(a => a.ID == shipperID).FirstOrDefault();
                if (shipper != null)
                {
                    return shipper.Name;
                }
                else return null;

            }
        }
    }

}