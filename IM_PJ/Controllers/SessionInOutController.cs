using IM_PJ.Models;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Controllers
{
    public class SessionInOutController
    {
        #region CRUD
        public static string Insert(DateTime DateInOut, string Note, int AgentID, int Type, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_SessionInOu ui = new tbl_SessionInOu();
                ui.AgentID = AgentID;
                ui.DateInOut = DateInOut;
                ui.Note = Note;
                ui.Type = Type;
                ui.CreatedDate = CreatedDate;
                ui.CreatedBy = CreatedBy;
                dbe.tbl_SessionInOu.Add(ui);
                dbe.SaveChanges();
                int kq = ui.ID;
                return kq.ToString();
            }
        }
        #endregion
        #region Select

        #endregion
    }
}