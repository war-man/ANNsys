using IM_PJ.Models;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Controllers
{
    public class AgentController
    {
        #region CRUD
        public static string Insert(string AgentName, string AgentDescription, string AgentAddress, string AgentPhone, string AgentEmail, string AgentLeader,
            bool IsHidden, string AgentAPIID, string AgentAPICode, DateTime CreatedDate, string CreatedBy, string AgentFacebook)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Agent ui = new tbl_Agent();
                ui.AgentName = AgentName;
                ui.AgentDescription = AgentDescription;
                ui.AgentAddress = AgentAddress;
                ui.AgentPhone = AgentPhone;
                ui.AgentFacebook = AgentFacebook;
                ui.AgentEmail = AgentEmail;
                ui.AgentLeader = AgentLeader;
                ui.IsHidden = IsHidden;
                ui.AgentAPIID = AgentAPIID;
                ui.AgentAPICode = AgentAPICode;
                ui.CreatedDate = CreatedDate;
                ui.CreatedBy = CreatedBy;
                dbe.tbl_Agent.Add(ui);
                int kq = dbe.SaveChanges();
                return kq.ToString();
            }
        }
        public static string Update(int ID, string AgentName, string AgentDescription, string AgentAddress, string AgentPhone, string AgentEmail, string AgentLeader,
            bool IsHidden, DateTime ModifiedDate, string ModifiedBy, string AgentFacebook)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Agent ui = dbe.tbl_Agent.Where(a => a.ID == ID).SingleOrDefault();
                if (ui != null)
                {
                    ui.AgentName = AgentName;
                    ui.AgentDescription = AgentDescription;
                    ui.AgentAddress = AgentAddress;
                    ui.AgentPhone = AgentPhone;
                    ui.AgentEmail = AgentEmail;
                    ui.AgentFacebook = AgentFacebook;
                    ui.AgentLeader = AgentLeader;
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
        public static tbl_Agent GetByID(int ID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Agent ai = dbe.tbl_Agent.Where(a => a.ID == ID).SingleOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }
        public static tbl_Agent GetByAPICodeID(string AgentAPIID, string AgentAPICode)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Agent ai = dbe.tbl_Agent.Where(a => a.AgentAPIID == AgentAPIID && a.AgentAPICode == AgentAPICode).SingleOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }
        public static List<tbl_Agent> GetAll(string s)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_Agent> ags = new List<tbl_Agent>();
                ags = dbe.tbl_Agent.Where(a => a.AgentName.Contains(s) || a.AgentPhone.ToString().Contains(s)).ToList();
                return ags;
            }
        }
        public static List<tbl_Agent> GetAllWithIsHidden(bool IsHidden)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_Agent> ags = new List<tbl_Agent>();
                ags = dbe.tbl_Agent.Where(a => a.IsHidden == IsHidden).ToList();
                return ags;
            }
        }
        #endregion
    }
}