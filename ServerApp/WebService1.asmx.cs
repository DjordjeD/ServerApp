using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using Newtonsoft.Json;

namespace ServerApp
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        DBAccess db = new DBAccess();
        DataTable dt = new DataTable();

        [WebMethod]
        public string Find(string name,string surname)
        {

            string query = "Select VIP from AlgotechData where Name='" + name + "' and Surname='" + surname + "'";
            //string query = "Select VIP from AlgotechData";
           // string query = "Select VIP from AlgotechData where Name='" + name + "'";
            db.readDatathroughAdapter(query,dt);
            // if (read.ToString()=="1") return true;
            // else return false;
            string rez = JsonConvert.SerializeObject(dt);
            return rez;
            
        }
    }
}
