using CrystalDecisions.CrystalReports.Engine;
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
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            Models.Employees Emp = new Models.Employees();
            List<Models.Data.EmployeeTable> lstdata = new List<Models.Data.EmployeeTable>();
            List<KeyValuePair<string, string>> whereCondition1 = new List<KeyValuePair<string, string>>();
            System.Web.HttpContext.Current.Session["reportObj"] = "hi";
            lstdata = Emp.GetEmployeeList(whereCondition1);
            return View(lstdata);
        } 
        
        
        public ActionResult Leaves()
        {
            Models.Employees Emp = new Models.Employees();
            List<Models.Data.LeaveDetails> lstdata = new List<Models.Data.LeaveDetails>();
            List<KeyValuePair<string, string>> whereCondition1 = new List<KeyValuePair<string, string>>();
            lstdata = Emp.GetLeaveList(whereCondition1);
            return View(lstdata);
        } 
        
        public ActionResult MasterList(string type)
        {
            Models.Employees Emp = new Models.Employees();
            List<KeyValuePair<string, string>> whereCondition1 = new List<KeyValuePair<string, string>>();
            whereCondition1.Add(new KeyValuePair<string, string>("@MasterType", type)); // for Departments
            List<Models.Data.MasterTable> lstdata = Emp.GetMasterList(whereCondition1);
            ViewBag.MasterType = type;

            return View(lstdata);
        } 
        public ActionResult AddMaster(string id, string type)
        {
            Models.Employees Emp = new Models.Employees();
            
            ViewBag.MasterType = type;
            List<Models.Data.MasterTable> lstdata = new List<Models.Data.MasterTable>();
            Models.Data.MasterTable etable = new Models.Data.MasterTable();
            if (id != null)
            {
                List<KeyValuePair<string, string>> whereCondition1 = new List<KeyValuePair<string, string>>();
                whereCondition1.Add(new KeyValuePair<string, string>("@MasterType", type)); // for Departments
                whereCondition1.Add(new KeyValuePair<string, string>("@MasterID", id)); // for Departments
                lstdata = Emp.GetMasterList(whereCondition1);

            }

            if (lstdata.Count > 0)
            {
                etable = lstdata[0];
                return View(etable);

            }
            else
            {
                return View();
            }
        }

        public ActionResult SaveDept()
        {
            Models.Employees Emp = new Models.Employees();
            List<Models.Data.DataValue> lstdata = new List<Models.Data.DataValue>();
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
                rslt = Emp.AddMaster(lstdata);
                msg = "success";
            }
            catch (Exception)
            {
                msg = "Failed";
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        public ActionResult AddLeave()
        {
            Models.Employees Emp = new Models.Employees();
            //List<Models.Data.EmployeeTable> lstdata = new List<Models.Data.EmployeeTable>();
            List<KeyValuePair<string, string>> whereConditio = new List<KeyValuePair<string, string>>();
            whereConditio.Add(new KeyValuePair<string, string>("@MasterType", "3")); // for Departments
            List<Models.Data.MasterTable> lstdata = Emp.GetMasterList(whereConditio);

            List<KeyValuePair<string, string>> whereCondition = new List<KeyValuePair<string, string>>();
            List<Models.Data.EmployeeTable> emplstdata = Emp.GetEmployeeList(whereCondition);

            ViewBag.LeaveTypes = lstdata;
            ViewBag.emp = emplstdata;
            return View();
        }

        public ActionResult SaveLeave()
        {
            Models.Employees Emp = new Models.Employees();
            List<Models.Data.DataValue> lstdata = new List<Models.Data.DataValue>();
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
                rslt = Emp.SaveLeave(lstdata);
                msg = "success";
            }
            catch (Exception)
            {
                msg = "Failed";
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddEmployee(string id)
        {
            Models.Employees Emp = new Models.Employees();
            //List<Models.Data.EmployeeTable> lstdata = new List<Models.Data.EmployeeTable>();
            List<KeyValuePair<string, string>> whereCondition1 = new List<KeyValuePair<string, string>>();
            whereCondition1.Add(new KeyValuePair<string, string>("@MasterType", "1")); // for Departments
            List<Models.Data.MasterTable> deptlstdata = Emp.GetMasterList(whereCondition1);

            whereCondition1 = new List<KeyValuePair<string, string>>();
            whereCondition1.Add(new KeyValuePair<string, string>("@MasterType", "2")); // for Designations
            List<Models.Data.MasterTable> designlstdata = Emp.GetMasterList(whereCondition1);
            ViewBag.DepartmentList = deptlstdata;
            ViewBag.DesignationList = designlstdata;

           
            List<Models.Data.EmployeeTable> lstdata = new List<Models.Data.EmployeeTable>();
            Models.Data.EmployeeTable etable = new Models.Data.EmployeeTable();
            if (id != null)
            {
                whereCondition1 = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("@Eid", id)
                };
                lstdata = Emp.GetEmployeeList(whereCondition1);
            }

            if (lstdata.Count > 0)
            {
                etable = lstdata[0];
                return View(etable);

            }
            else
            {
                return View();
            }

        }
        
        
        public ActionResult SaveEmployee()
        {
            Models.Employees Emp = new Models.Employees();
            List<Models.Data.DataValue> lstdata = new List<Models.Data.DataValue>();
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
                rslt = Emp.SaveForm(lstdata);
                msg = "success";
            }
            catch (Exception)
            {
                msg = "Failed";
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        } 
        


        public ActionResult PrintReport(string type)
        {
            var Id1 = 1;
            string rept = "";
            string ReptName = "";
            System.Web.HttpContext.Current.Session["reportObj"] = "hi";


            string Id = form("hdid");
            

            if (type == "1")
            {
                rept = "RptEmployeeDeptReport.rpt";
                ReptName = "RptEmployeeDeptReport";
            }

            else if (type == "2")
            {
                rept = "RptEmployeeDesignReport.rpt";
                ReptName = "RptEmployeeDesignReport";
                
            }
            else if (type == "3")
            {
                rept = "RptEmployeeLeaveReport.rpt";
                ReptName = "RptEmployeeLeaveReport";
                
            }
            else if (type == "4")
            {
                rept = "RptLeaveReport.rpt";
                ReptName = "RptLeaveReport";
                
            }
            else
            {
                rept = "RptEmployeeReport.rpt";
                ReptName = "RptEmployeeReport";
            }
        




            //var id1 = Int16.Parse(Id);
            try
            {
                
                ReportDocument report = new ReportDocument();
                report.Load(Path.Combine(Server.MapPath("~/Reports/"+rept)));
                ReportDocument aa1 = new ReportDocument();
                //aa1.OpenSubreport("RecieptSubReport.rpt");
                report.SetParameterValue(0, null);
             


                report = Models.Data.SetReportSettings(report);
                System.Web.HttpContext.Current.Session["reportObj"] = report;
                System.Web.HttpContext.Current.Session["reportName"] = ReptName + "_" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
                //System.Web.HttpContext.Current.Session["reportObj1"] = aa1;
                //aa1 = Models.Data.SetReportSettings(aa1);

                Response.Redirect(@"~\ReportView\ReportViewer.aspx");
            }
            catch (Exception e) { }
            return View();
        }

        public string form(String optname)
        {
            String sOut = "";
            if (string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.QueryString[optname]))
            {
                if (string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Form[optname]))
                {
                    sOut = "";
                }
                else
                {
                    sOut = System.Web.HttpContext.Current.Request.Form[optname].ToString();
                }
            }
            else
            {
                sOut = System.Web.HttpContext.Current.Request.QueryString[optname].ToString();
            }
            return sOut;
        }


    }
}