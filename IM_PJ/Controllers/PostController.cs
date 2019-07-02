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
    public class PostController
    {
        static string checkSlug(string slug, int ID = 0)
        {
            using (var con = new inventorymanagementEntities())
            {
                var product = con.tbl_Post.Where(x => x.Slug == slug && x.ID != ID).FirstOrDefault();
                if (product != null)
                {
                    return checkSlug(slug + "-1", ID);
                }
                else
                {
                    return slug;
                }
            }
        }
        #region CRUD
        public static string Insert(string Title, string Content, string Image, int Featured, int CategoryID, int Status, string PostSlug, string CreatedBy, DateTime CreatedDate)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Post ui = new tbl_Post();
                ui.Title = Title;
                ui.Content = Content;
                ui.Image = Image;
                ui.Featured = Featured;
                ui.CategoryID = CategoryID;
                ui.Status = Status;
                ui.CreatedBy = CreatedBy;
                ui.CreatedDate = CreatedDate;
                ui.WebPublish = false;
                ui.WebUpdate = CreatedDate;
                ui.Slug = checkSlug(Slug.ConvertToSlug(PostSlug != "" ? PostSlug : Title));

                dbe.tbl_Post.Add(ui);
                dbe.SaveChanges();
                int kq = ui.ID;
                return kq.ToString();
            }
        }
        public static string Update(int ID, string Title, string Content, string Image, int Featured, int CategoryID, int Status, string PostSlug, string ModifiedBy, DateTime ModifiedDate)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Post ui = dbe.tbl_Post.Where(a => a.ID == ID).SingleOrDefault();
                if (ui != null)
                {
                    ui.Title = Title;
                    ui.Content = Content;
                    ui.Image = Image;
                    ui.Featured = Featured;
                    ui.CategoryID = CategoryID;
                    ui.Status = Status;
                    ui.ModifiedBy = ModifiedBy;
                    ui.ModifiedDate = ModifiedDate;
                    ui.Slug = checkSlug(Slug.ConvertToSlug(PostSlug != "" ? PostSlug : Title), ui.ID);

                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }

        public static string UpdateImage(int ID, string Image)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Post ui = dbe.tbl_Post.Where(a => a.ID == ID).SingleOrDefault();
                if (ui != null)
                {
                    ui.Image = Image;
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }
        public static string Delete(int ID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Post ui = dbe.tbl_Post.Where(o => o.ID == ID).FirstOrDefault();
                if (ui != null)
                {
                    dbe.tbl_Post.Remove(ui);
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }
        #endregion
        #region Select
        public static tbl_Post GetByID(int ID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Post ai = dbe.tbl_Post.Where(a => a.ID == ID).SingleOrDefault();
                if (ai != null)
                {
                    return ai;
                }
                else return null;

            }
        }
        public static List<tbl_Post> GetAll(string s)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_Post> ags = new List<tbl_Post>();
                ags = dbe.tbl_Post.Where(p => p.Title.Contains(s)).OrderByDescending(o => o.ID).ToList();
                return ags;
            }
        }
        public static List<tbl_Post> GetByCategoryID(int CategoryID)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                List<tbl_Post> ags = new List<tbl_Post>();
                ags = dbe.tbl_Post.Where(p => p.CategoryID == CategoryID).OrderByDescending(o => o.ID).ToList();
                return ags;
            }
        }
        public static List<PostSQL> GetAllSql(int categoryID, string textsearch, string PostStatus, string WebPublish, string CreatedDate)
        {
            var list = new List<PostSQL>();
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("BEGIN");

            if (categoryID > 0)
            {
                sql.AppendLine(String.Empty);
                sql.AppendLine("WITH category AS(");
                sql.AppendLine("    SELECT");
                sql.AppendLine("            ID");
                sql.AppendLine("    ,       Title");
                sql.AppendLine("    ,       ParentID");
                sql.AppendLine("    FROM");
                sql.AppendLine("            tbl_PostCategory");
                sql.AppendLine("    WHERE");
                sql.AppendLine("            1 = 1");
                sql.AppendLine("    AND     ID = " + categoryID);
                sql.AppendLine("");
                sql.AppendLine("    UNION ALL");
                sql.AppendLine("");
                sql.AppendLine("    SELECT");
                sql.AppendLine("            CHI.ID");
                sql.AppendLine("    ,       CHI.Title");
                sql.AppendLine("    ,       CHI.ParentID");
                sql.AppendLine("    FROM");
                sql.AppendLine("            category AS PAR");
                sql.AppendLine("    INNER JOIN tbl_PostCategory AS CHI");
                sql.AppendLine("        ON PAR.ID = CHI.ParentID");
                sql.AppendLine(")");
                sql.AppendLine("SELECT");
                sql.AppendLine("        ID");
                sql.AppendLine(",       Title");
                sql.AppendLine(",       ParentID");
                sql.AppendLine("INTO #category");
                sql.AppendLine("FROM category");
            }

            sql.AppendLine(String.Empty);
            sql.AppendLine("    SELECT");
            sql.AppendLine("            POS.*");
            sql.AppendLine("    INTO #Post");
            sql.AppendLine("    FROM");
            sql.AppendLine("            tbl_Post AS POS");
            sql.AppendLine("    WHERE");
            sql.AppendLine("            1 = 1");

            if (!string.IsNullOrEmpty(textsearch))
            {
                sql.AppendLine("    AND (POS.Title like N'%" + textsearch + "%')");
            }

            if (!string.IsNullOrEmpty(PostStatus))
            {
                sql.AppendLine("    AND (POS.Status = " + PostStatus.ToInt() + ")");
            }

            if (!string.IsNullOrEmpty(WebPublish))
            {
                sql.AppendLine("    AND (POS.WebPublish = '" + WebPublish + "')");
            }

            if (!string.IsNullOrEmpty(CreatedDate))
            {
                DateTime fromdate = DateTime.Today;
                DateTime todate = DateTime.Now;
                switch (CreatedDate)
                {
                    case "today":
                        fromdate = DateTime.Today;
                        todate = DateTime.Now;
                        break;
                    case "yesterday":
                        fromdate = fromdate.AddDays(-1);
                        todate = DateTime.Today;
                        break;
                    case "beforeyesterday":
                        fromdate = DateTime.Today.AddDays(-2);
                        todate = DateTime.Today.AddDays(-1);
                        break;
                    case "week":
                        int days = DateTime.Today.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)DateTime.Today.DayOfWeek;
                        fromdate = fromdate.AddDays(-days + 1);
                        todate = DateTime.Now;
                        break;
                    case "thismonth":
                        fromdate = new DateTime(fromdate.Year, fromdate.Month, 1);
                        todate = DateTime.Now;
                        break;
                    case "lastmonth":
                        var thismonth = new DateTime(fromdate.Year, fromdate.Month, 1);
                        fromdate = thismonth.AddMonths(-1);
                        todate = thismonth;
                        break;
                    case "7days":
                        fromdate = DateTime.Today.AddDays(-6);
                        todate = DateTime.Now;
                        break;
                    case "30days":
                        fromdate = DateTime.Today.AddDays(-29);
                        todate = DateTime.Now;
                        break;
                }
                sql.AppendLine(String.Format("	AND	(CONVERT(datetime, POS.CreatedDate, 103) BETWEEN CONVERT(datetime, '{0}', 103) AND CONVERT(datetime, '{1}', 103))", fromdate.ToString(), todate.ToString()));
            }

            if (categoryID > 0)
            {
                sql.AppendLine("    AND EXISTS(");
                sql.AppendLine("            SELECT");
                sql.AppendLine("                    NULL AS DUMMY");
                sql.AppendLine("            FROM");
                sql.AppendLine("                    #category");
                sql.AppendLine("            WHERE");
                sql.AppendLine("                    ID = POS.CategoryID");
                sql.AppendLine("    )");
            }

            sql.AppendLine("     ORDER BY");
            sql.AppendLine("            POS.ID");
            sql.AppendLine("     ;");
            sql.AppendLine(String.Empty);
            sql.AppendLine("     SELECT");
            sql.AppendLine("             c.Title AS CategoryName");
            sql.AppendLine("     ,       p.*");
            sql.AppendLine("     FROM");
            sql.AppendLine("             #Post AS p");
            sql.AppendLine("     LEFT JOIN (");
            sql.AppendLine("             SELECT");
            sql.AppendLine("                     ID");
            sql.AppendLine("             ,       Title");
            sql.AppendLine("             FROM");
            sql.AppendLine("                     dbo.tbl_PostCategory");
            sql.AppendLine("     ) AS c");
            sql.AppendLine("     ON c.ID = p.CategoryID");
            sql.AppendLine("     ORDER BY");
            sql.AppendLine("             p.ID");
            sql.AppendLine("     ;");
            sql.AppendLine(String.Empty);
            sql.AppendLine(" END");

            var reader = (IDataReader)SqlHelper.ExecuteDataReader(sql.ToString());

            while (reader.Read())
            {

                var entity = new PostSQL();

                entity.ID = Convert.ToInt32(reader["ID"]);

                if (!string.IsNullOrEmpty(reader["Image"].ToString()))
                {
                    entity.Image = reader["Image"].ToString();
                }
                else
                {
                    entity.Image = "/App_Themes/Ann/image/placeholder.png";
                }

                if (reader["Title"] != DBNull.Value)
                    entity.Title = reader["Title"].ToString();


                if (reader["Content"] != DBNull.Value)
                    entity.Content = reader["Content"].ToString();
                if (reader["Featured"] != DBNull.Value)
                    entity.Featured = reader["Featured"].ToString().ToInt(0);
                if (reader["CategoryName"] != DBNull.Value)
                    entity.CategoryName = reader["CategoryName"].ToString();
                if (reader["CategoryID"] != DBNull.Value)
                    entity.CategoryID = reader["CategoryID"].ToString().ToInt(0);
                if (reader["CreatedDate"] != DBNull.Value)
                    entity.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                if (reader["Status"] != DBNull.Value)
                    entity.Status = reader["Status"].ToString().ToInt(0);
                if (reader["WebPublish"] != DBNull.Value)
                    entity.WebPublish = reader["WebPublish"].ToString().ToBool();
                if (reader["WebUpdate"] != DBNull.Value)
                    entity.WebUpdate = Convert.ToDateTime(reader["WebUpdate"]);
                list.Add(entity);
            }
            reader.Close();

            var list_featured = list.Where(x => x.Featured == 1).OrderByDescending(x => x.CreatedDate).ToList();
            var list_normal = list.Where(x => x.Featured == 0).OrderByDescending(x => x.CreatedDate).ToList();
            return list_featured.Concat(list_normal).ToList();
        }
        public static string updateWebPublish(int id, bool value)
        {
            using (var dbe = new inventorymanagementEntities())
            {
                tbl_Post ui = dbe.tbl_Post.Where(a => a.ID == id).SingleOrDefault();
                if (ui != null)
                {
                    ui.WebPublish = value;
                    int kq = dbe.SaveChanges();
                    return kq.ToString();
                }
                else
                    return null;
            }
        }
        #endregion
        #region Class
        public class PostSQL
        {
            public int ID { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
            public string Image { get; set; }
            public Nullable<int> Featured { get; set; }
            public Nullable<int> CategoryID { get; set; }
            public string CategoryName { get; set; }
            public Nullable<int> Status { get; set; }
            public string CreatedBy { get; set; }
            public Nullable<System.DateTime> CreatedDate { get; set; }
            public string ModifiedBy { get; set; }
            public Nullable<System.DateTime> ModifiedDate { get; set; }
            public bool WebPublish { get; set; }
            public DateTime WebUpdate { get; set; }
        }

        #endregion
    }
}