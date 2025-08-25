using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace Employee.Models
{
    public class Employees
    {
        Models.SqlHelper _helper = new Models.SqlHelper();

        public int LoginForm(List<Models.Data.DataValue> lstload)
        {

            string msg = "";
            int result = 1;
            try
            {

                SqlParameter[] par = {

                          new SqlParameter("@username",string.IsNullOrEmpty(getstringvalue("name", lstload))?DBNull.Value:(object)getstringvalue("name", lstload)),
                          new SqlParameter("@pass",string.IsNullOrEmpty(getstringvalue("password", lstload))?DBNull.Value:(object)getstringvalue("password", lstload))

                    };
                result = _helper.ExecuteSPNonQuery("Sp_tblLoginDetailsInsertUpdateSingleItem", par);
            }

            catch (Exception e)
            {
                msg = "failed";
                result = -123;
            }
            return result;
        }
        
        public int SaveLeave(List<Models.Data.DataValue> lstload)
        {

            string msg = "";
            int result = 1;
            try
            {

                SqlParameter[] par = {

                        new SqlParameter("@Lid", string.IsNullOrEmpty(getstringvalue("Lid", lstload)) ? (object)DBNull.Value : getstringvalue("Lid", lstload)),
                        new SqlParameter("@Eid", string.IsNullOrEmpty(getstringvalue("Eid", lstload)) ? (object)DBNull.Value : getstringvalue("Eid", lstload)),
                        new SqlParameter("@Type", string.IsNullOrEmpty(getstringvalue("Type", lstload)) ? (object)DBNull.Value : getstringvalue("Type", lstload)),
                        new SqlParameter("@FromDate", string.IsNullOrEmpty(getstringvalue("FromDate", lstload)) ? (object)DBNull.Value : getstringvalue("FromDate", lstload)),
                        new SqlParameter("@ToDate", string.IsNullOrEmpty(getstringvalue("ToDate", lstload)) ? (object)DBNull.Value : getstringvalue("ToDate", lstload)),
                        new SqlParameter("@Days", string.IsNullOrEmpty(getstringvalue("Days", lstload)) ? (object)DBNull.Value : getstringvalue("Days", lstload)),
                        new SqlParameter("@Reason", string.IsNullOrEmpty(getstringvalue("Reason", lstload)) ? (object)DBNull.Value : getstringvalue("Reason", lstload)),
                        new SqlParameter("@CreateID", 10),
                        new SqlParameter("@ModId", 20)
                };

                result = _helper.ExecuteSPNonQuery("Sp_tblLeaveInsertUpdateSingleItem", par);
            }

            catch (Exception e)
            {
                msg = "failed";
                result = -123;
            }
            return result;
        }
        public int SaveForm(List<Models.Data.DataValue> lstload)
        {

            string msg = "";
            int result = 1;
            try
            {

                SqlParameter[] par = {

                        new SqlParameter("@Eid", string.IsNullOrEmpty(getstringvalue("Id", lstload)) ? (object)DBNull.Value : getstringvalue("Id", lstload)),
                        new SqlParameter("@Name", string.IsNullOrEmpty(getstringvalue("Name", lstload)) ? (object)DBNull.Value : getstringvalue("Name", lstload)),
                        new SqlParameter("@Age", string.IsNullOrEmpty(getstringvalue("Age", lstload)) ? (object)DBNull.Value : getstringvalue("Age", lstload)),
                        new SqlParameter("@JoinDate", string.IsNullOrEmpty(getstringvalue("JoinDate", lstload)) ? (object)DBNull.Value : getstringvalue("JoinDate", lstload)),
                        new SqlParameter("@DOB", string.IsNullOrEmpty(getstringvalue("DOB", lstload)) ? (object)DBNull.Value : getstringvalue("DOB", lstload)),
                        new SqlParameter("@Salary", string.IsNullOrEmpty(getstringvalue("Salary", lstload)) ? (object)DBNull.Value : getstringvalue("Salary", lstload)),
                        new SqlParameter("@DeptId", string.IsNullOrEmpty(getstringvalue("DeptId", lstload)) ? (object)DBNull.Value : getstringvalue("DeptId", lstload)),
                        new SqlParameter("@DesignId", string.IsNullOrEmpty(getstringvalue("DesignId", lstload)) ? (object)DBNull.Value : getstringvalue("DesignId", lstload)),
                        new SqlParameter("@Sex", string.IsNullOrEmpty(getstringvalue("Sex", lstload)) ? (object)DBNull.Value : getstringvalue("Sex", lstload)),
                        new SqlParameter("@PhNo", string.IsNullOrEmpty(getstringvalue("PhNo", lstload)) ? (object)DBNull.Value : getstringvalue("PhNo", lstload)),
                        new SqlParameter("@CreateID", 10),
                        new SqlParameter("@ModId", 20)
            };
                result = _helper.ExecuteSPNonQuery("Sp_tblEmployeeInsertUpdateSingleItem", par);
            }

            catch (Exception e)
            {
                msg = "failed";
                result = -123;
            }
            return result;
        }


        public List<Models.Data.LeaveDetails> GetLeaveList(List<KeyValuePair<string, string>> whereCondition)
        {
            DataSet ds = new DataSet();

            //if any issue check here reason: models is not used in previous code
            List<Models.Data.LeaveDetails> clientModel = new List<Data.LeaveDetails>();
            try
            {
                ds = _helper.ExecuteDatasetSP("Sp_tblLeaveSel", "", whereCondition);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var obj = new Data.LeaveDetails
                    {
                        Lid = dr["Lid"].ToString(),
                        Eid = dr["Eid"].ToString(),
                        EmployeeName = dr["EmployeeName"].ToString(),
                        LeaveType = dr["LeaveType"].ToString(),
                        Type = dr["Type"].ToString(),
                        FromDate = dr["FromDate"].ToString(),
                        ToDate = dr["ToDate"].ToString(),
                        Days = dr["Days"].ToString(),
                        Reason = dr["Reason"].ToString()
                    };


                    clientModel.Add(obj);
                }
            }
            catch (Exception e)
            {

            }
            return clientModel;
        } 
        
        public List<Models.Data.EmployeeTable> GetEmployeeList(List<KeyValuePair<string, string>> whereCondition)
        {
            DataSet ds = new DataSet();

            //if any issue check here reason: models is not used in previous code
            List<Models.Data.EmployeeTable> clientModel = new List<Data.EmployeeTable>();
            try
            {
                ds = _helper.ExecuteDatasetSP("Sp_tblEmployeeSel", "", whereCondition);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var obj = new Data.EmployeeTable
                    {
                        Eid = dr["Eid"].ToString(),
                        name = dr["Name"].ToString(),
                        age = dr["Age"].ToString(),
                        joindate = dr["JoinDate"].ToString(),
                        dob = dr["DOB"].ToString(),
                        salary = dr["Salary"].ToString(),
                        dept = dr["DepartmentName"].ToString(),
                        desig = dr["DesignationName"].ToString(),
                        sex = dr["Sex"].ToString(),
                        phno = dr["PhNo"].ToString(),
                        NoOfLeaves = dr["NoOfLeaves"].ToString()
                    };
                
                clientModel.Add(obj);
                }
            }
            catch (Exception e)
            {

            }
            return clientModel;
        }

        public List<Models.Data.MasterTable> GetMasterList(List<KeyValuePair<string, string>> whereCondition)
        {
            DataSet ds = new DataSet();

            //if any issue check here reason: models is not used in previous code
            List<Models.Data.MasterTable> clientModel = new List<Data.MasterTable>();
            try
            {
                ds = _helper.ExecuteDatasetSP("sp_tblMasterSel", "", whereCondition);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var obj = new Data.MasterTable
                    {
                        MID = dr["MasterID"].ToString(),
                        MNAME = dr["MasterName"].ToString(),
                        MTYPE = dr["MasterType"].ToString()
                    };
                    clientModel.Add(obj);
                }
            }
            catch (Exception e)
            {

            }
            return clientModel;
        }
        public int AddMaster(List<Models.Data.DataValue> lstload)
        {
            string msg = "";
            int result = 1;
            try
            {

                SqlParameter[] par = {

                          new SqlParameter("@MasterID",string.IsNullOrEmpty(getstringvalue("MID", lstload))?DBNull.Value:(object)getstringvalue("MID", lstload)),
                          new SqlParameter("@MasterName",string.IsNullOrEmpty(getstringvalue("MName", lstload))?DBNull.Value:(object)getstringvalue("MName", lstload)),
                          new SqlParameter("@MasterType",string.IsNullOrEmpty(getstringvalue("type", lstload))?DBNull.Value:(object)getstringvalue("type", lstload))

                    };
                result = _helper.ExecuteSPNonQuery("sp_tblMasterInsertUpdateSingleItem", par);
            }

            catch (Exception e)
            {
                msg = "failed";
                result = -123;
            }
            return result;
        }

        string getstringvalue(string id, List<Data.DataValue> lst)//sqlparameter passing
        {
            string s = "";
            s = (from o in lst where o.name == id select o.value).FirstOrDefault();
            if (s == null) s = "";
            return s;
        }

    }
}