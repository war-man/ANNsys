using IM_PJ.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IM_PJ
{
    public partial class getImageByLink : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //LoadImageVariabe();
            }
            //LoadImage();
        }
        public void LoadImageVariabe()
        {
            using (WebClient client = new WebClient())
            {
                var link = ProductVariableController.GetAll("");
                if (link.Count > 0)
                {
                    foreach (var l in link)
                    {
                        if (!string.IsNullOrEmpty(l.Image))
                        {
                            string li = l.Image;
                            string fname = li.Replace("https://ann.com.vn/wp-content/uploads/", "");
                            try
                            {
                                client.DownloadFile(new Uri(li), Server.MapPath("~/uploads/" + fname + ""));
                            }
                            catch
                            {

                            }
                        }
                    }
                }
            }
        }
        public void LoadImage()
        {
            using (WebClient client = new WebClient())
            {
                var link = LinkImnageController.GetAll();
                if (link.Count > 0)
                {
                    foreach (var l in link)
                    {
                        if (!string.IsNullOrEmpty(l.ImageLink))
                        {
                            string li = l.ImageLink;
                            if (li.Contains("|"))
                            {
                                string[] items = li.Split('|');
                                for (int i = 0; i < items.Length; i++)
                                {
                                    string item = items[i];
                                    string fname = item.Replace("https://ann.com.vn/wp-content/uploads/", "");
                                    try
                                    {
                                        client.DownloadFile(new Uri(item), Server.MapPath("~/uploads/" + fname + ""));
                                    }
                                    catch
                                    {

                                    }

                                }
                            }
                            else
                            {
                                string fname = li.Replace("https://ann.com.vn/wp-content/uploads/", "");
                                try
                                {
                                    client.DownloadFile(new Uri(li), Server.MapPath("~/uploads/" + fname + ""));
                                }
                                catch
                                {

                                }
                            }
                        }
                    }
                }
            }
        }
    }
}