using IM_PJ.Models;
using NHST.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM_PJ.Controllers
{
    public class MoveProController
    {
        #region CRUD
        public static string Insert(int AgentIDSend, string AgentNameSend, int AgentIDReceive, string AgentNameReceive, int Status, string Note, bool IsHidden,
            DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_MovePro ui = new tbl_MovePro();
                ui.AgentIDSend = AgentIDSend;
                ui.AgentNameSend = AgentNameSend;
                ui.AgentIDReceive = AgentIDReceive;
                ui.AgentNameReceive = AgentNameReceive;
                ui.Status = Status;
                ui.Note = Note;
                ui.IsHidden = IsHidden;
                ui.CreatedDate = CreatedDate;
                ui.CreatedBy = CreatedBy;
                dbe.tbl_MovePro.Add(ui);
                int kq = dbe.SaveChanges();
                return kq.ToString();
            }
        }
        public static string Update(int ID, int AgentIDSend, string AgentNameSend, int AgentIDReceive, string AgentNameReceive, int Status, string Note,
            bool IsHidden, DateTime CreatedDate, string CreatedBy)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                var ui = dbe.tbl_MovePro.Where(p => p.ID == ID).FirstOrDefault();
                if (ui != null)
                {
                    ui.AgentIDSend = AgentIDSend;
                    ui.AgentNameSend = AgentNameSend;
                    ui.AgentIDReceive = AgentIDReceive;
                    ui.AgentNameReceive = AgentNameReceive;
                    ui.Status = Status;
                    ui.Note = Note;
                    ui.IsHidden = IsHidden;
                    ui.CreatedDate = CreatedDate;
                    ui.CreatedBy = CreatedBy;
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                return "0";
            }
        }

        public static string UpdateStatus(int ID, int Status, DateTime ModifiedDate, string ModifiedBy)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_MovePro ui = dbe.tbl_MovePro.Where(a => a.ID == ID).SingleOrDefault();
                if (ui != null)
                {
                    ui.Status = Status;
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
        public static tbl_MovePro GetByIDAndAgentIDReceive(int ID, int AgentIDReceive)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_MovePro ai = dbe.tbl_MovePro.Where(a => a.ID == ID && a.AgentIDReceive == AgentIDReceive).SingleOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }
        public static tbl_MovePro GetByIDAndAgentIDSend(int ID, int AgentIDSend)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_MovePro ai = dbe.tbl_MovePro.Where(a => a.ID == ID && a.AgentIDSend == AgentIDSend).SingleOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }
        public static tbl_MovePro GetByID(int ID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_MovePro ai = dbe.tbl_MovePro.Where(a => a.ID == ID).SingleOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }
        public static List<tbl_MovePro> GetAll(string s)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_MovePro> ags = new List<tbl_MovePro>();
                ags = dbe.tbl_MovePro.Where(c => c.AgentNameSend.Contains(s) || c.AgentNameReceive.Contains(s)).ToList();
                return ags;
            }
        }
        public static List<tbl_MovePro> GetByAgentSend(int AgentIDSend)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_MovePro> ags = new List<tbl_MovePro>();
                ags = dbe.tbl_MovePro.Where(c => c.AgentIDSend == AgentIDSend).ToList();
                return ags;
            }
        }
        public static List<tbl_MovePro> GetByAgentSendAndStatus(int AgentIDSend, int Status)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_MovePro> ags = new List<tbl_MovePro>();
                ags = dbe.tbl_MovePro.Where(c => c.AgentIDSend == AgentIDSend && c.Status == Status).OrderBy(c => c.Status).ToList();
                return ags;
            }
        }
        public static List<tbl_MovePro> GetByAgentReceive(int AgentIDReceive)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_MovePro> ags = new List<tbl_MovePro>();
                ags = dbe.tbl_MovePro.Where(c => c.AgentIDReceive == AgentIDReceive && c.Status > 1).OrderBy(c => c.Status).ToList();
                return ags;
            }
        }
        public static List<tbl_MovePro> GetByAgentReceiveAndStatus(int AgentIDReceive, int Status)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_MovePro> ags = new List<tbl_MovePro>();
                ags = dbe.tbl_MovePro.Where(c => c.AgentIDReceive == AgentIDReceive && c.Status == Status).OrderBy(c => c.Status).ToList();
                return ags;
            }
        }

        public static List<tbl_MovePro> Search(string s,int status)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_MovePro> ags = new List<tbl_MovePro>();
                if (!string.IsNullOrEmpty(s))
                {
                    if(status >0)
                    {
                        ags = dbe.tbl_MovePro.Where(c => c.AgentNameSend.Contains(s) && c.Status == status || c.AgentNameReceive.Contains(s) && c.Status == status).ToList();
                    }
                    else
                    {
                        ags = dbe.tbl_MovePro.Where(c => c.AgentNameSend.Contains(s) || c.AgentNameReceive.Contains(s)).ToList();
                    }
                }
                else
                {
                    if(status >0)
                    {
                        ags = dbe.tbl_MovePro.Where(c => c.Status == status).ToList();
                    }
                    else
                    {
                        ags = dbe.tbl_MovePro.ToList();
                    }
                }
                    
                return ags;
            }
            
        }
        #endregion       
    }
}