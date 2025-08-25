using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Employee.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Print()
        {
            System.Web.HttpContext.Current.Session["reportObj"] = "hi";
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
 
        public ActionResult CheckLogin()
        {
            List<Models.Data.DataValue> lstdata = new List<Models.Data.DataValue>();
            Models.Employees Emp = new Models.Employees();
            int rslt = 0;
            string msg = "";

            try
            {
                JavaScriptSerializer jserial = new JavaScriptSerializer();
                var resolveRequest = HttpContext.Request;
                resolveRequest.InputStream.Seek(0, SeekOrigin.Begin);
                string jsonString = new StreamReader(resolveRequest.InputStream).ReadToEnd();
                JObject jsonResponse = JObject.Parse(jsonString);
                string Val = "";
                if (jsonString != "")
                {
                    foreach (JProperty content in jsonResponse.Children())
                    {
                        if (content.Name == "sdata")
                        {
                            Val = content.Value.ToString();
                            lstdata = jserial.Deserialize<List<Models.Data.DataValue>>(Val);
                        }
                    }
                }
                rslt = Emp.LoginForm(lstdata);
                switch (rslt)
                {
                    case 2:
                        msg = "Login successful.";
                        
                        break;
                    case 1:
                        msg = "Incorrect password.";
                        break;
                    case -110:
                        msg = "User does not exist.";
                        break;
                    case -123:
                        msg = "System error.";
                        break;
                    default:
                        msg = "Unexpected error.";
                        break;
                }
            }
            catch (Exception ex)
            {
                msg = "Exception occurred.";
            }

            // Return proper structured JSON response
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
    }
}