using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Employee.Controllers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Employee.Models
{
    public class Data
    {
        public class DataValue
        {

            public string name { get; set; }
            public string value { get; set; }
            public DataValue()
            {
                name = "";
                value = "";
            }
        }
        public class tableColumns
        {
            public string colName { get; set; }
            public string colValue { get; set; }
            public string colType { get; set; }
        }
        public class LoginDetails
        {
            public string ID { get; set; }
            public string username { get; set; }
            public string pass { get; set; }
        }

        public class LeaveDetails
        {
            public string Lid { get; set; }          
            public string Eid { get; set; }          
            public string EmployeeName { get; set; }          
            public string LeaveType { get; set; }          
            public string Type { get; set; }         
            public string FromDate { get; set; }     
            public string ToDate { get; set; }       
            public string Days { get; set; }      
            public string Reason { get; set; } 
        }


        public class MasterTable
        {
            public string MID { get; set; }
            public string MNAME { get; set; }
            public string MTYPE { get; set; }
            public string MADDITIONAL { get; set; }
        }

     

        //here pid is cid and cid is pid
        public class EmployeeTable
        {
            public string Eid { get; set; }
            public string name { get; set; }
            public string age { get; set; }
            public string joindate { get; set; }
            public string dob { get; set; }
            public string salary { get; set; }
            public string dept { get; set; }
            public string desig { get; set; }
            public string sex { get; set; }
            public string phno { get; set; }
            public string NoOfLeaves { get; set; }

        }

    
    
        #region SetReportSettings
        static public ReportDocument SetReportSettings(ReportDocument reportObj = null)
        {
            try
            {
                if (reportObj == null)
                {
                    throw new Exception();
                }
            }
            catch (Exception e) { }
            CrystalDecisions.Shared.ConnectionInfo crcconnectionInfo = new CrystalDecisions.Shared.ConnectionInfo();
            TableLogOnInfos crtablelogoninfos = new TableLogOnInfos();
            TableLogOnInfo crtalbelogoninfo = new TableLogOnInfo();
            Tables CrTables;
            List<string> lst = new List<string>();
            Models.SqlHelper _helper = new Models.SqlHelper();
            string conString = _helper.connectionString();
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(conString);
            lst.Add(builder.UserID);
            lst.Add(builder.Password);
            lst.Add(builder.DataSource);
            lst.Add(builder.InitialCatalog);

            crcconnectionInfo.ServerName = lst[2];
            crcconnectionInfo.DatabaseName = lst[3];
            crcconnectionInfo.UserID = lst[0];
            crcconnectionInfo.Password = lst[1];
            try { CrTables = reportObj.Database.Tables; }
            catch (Exception e)
            {
                return reportObj;
            }

            foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
            {
                crtalbelogoninfo = CrTable.LogOnInfo;
                crtalbelogoninfo.ConnectionInfo = crcconnectionInfo;
                CrTable.ApplyLogOnInfo(crtalbelogoninfo);
                CrTable.Location = crcconnectionInfo.DatabaseName + ".dbo." + CrTable.Name;
            }
            return reportObj;
        }
        #endregion

    }
}