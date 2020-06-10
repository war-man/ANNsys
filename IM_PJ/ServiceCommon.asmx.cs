using Newtonsoft.Json;
using IM_PJ.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;

namespace IM_PJ
{
    /// <summary>
    /// Summary description for ServiceCommon
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ServiceCommon : System.Web.Services.WebService
    {
        private CommonController _comm = CommonController.getInstance();

        [WebMethod]
        [ScriptMethod(UseHttpGet = false)]
        public string GetProduct(string SKU, bool isStock)
        {
            return JsonConvert.SerializeObject(_comm.GetProduct(SKU, isStock));
        }
    }
}
