using System.Data;
using EmployeeDetails.Interface;
using EmployeeDetails.Models;
using Microsoft.Data.SqlClient;

namespace EmployeeDetails.Service
{
    public class EmpBasicDetailsService : IEmpBasicDetails
    {
        private readonly string? _connectionString;
        DataTransactions datatrans;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EmpBasicDetailsService(IConfiguration _configuratio, IHttpContextAccessor httpContextAccessor)
        {
            _connectionString = _configuratio.GetConnectionString("MySqlConnection");
            datatrans = new DataTransactions(_connectionString ?? "");
            _httpContextAccessor = httpContextAccessor;
        }

        public DataTable GetAllEmployeeGRID(string strStatus)
        {
            string SvSql = string.Empty;
            if (strStatus == "Y" || strStatus == null)
            {
                SvSql = "SELECT USER_REGIST.IS_ACTIVE,USER_REGIST.ID,EMPLOYEE_ID,FNAME,MOBILE FROM USER_REGIST WHERE USER_REGIST.IS_ACTIVE = 'Y' ORDER BY USER_REGIST.ID DESC";
            }
            else
            {
                SvSql = "SELECT USER_REGIST.IS_ACTIVE,USER_REGIST.ID,EMPLOYEE_ID,FNAME,MOBILE FROM USER_REGIST WHERE USER_REGIST.IS_ACTIVE = 'N' ORDER BY USER_REGIST.ID DESC";
            }
            DataTable dtt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(SvSql, _connectionString);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.Fill(dtt);
            return dtt;
        }

        public string EmployeeCRUD(EmpBasicDetails cy)
        {
            string msg = "";
            try
            {
                string StatementType = string.Empty;
                string svSQL = "";
                var userId = _httpContextAccessor.HttpContext?.Request.Cookies["UserId"];
                using (SqlConnection objConn = new SqlConnection(_connectionString))
                {
                    objConn.Open();
                    if (cy.ID == null)
                    {
                        svSQL = "Insert into USER_REGIST (EMPLOYEE_ID,FNAME,GENDER,ADDRESS,CITY_ID,STATE_ID,MOBILE,EMAIL,MARITALSTATUS,DATEOFJOINING,DATEOFBIRTH,AANUMBER,ACCNUMBER,BANK,USER_NAME,PASSWORD,CREATED_BY,CREATED_DATE) VALUES ('" + cy.EmpId + "','" + cy.Ename + "','" + cy.Gender + "','" + cy.Address + "','" + cy.Cityid + "','" + cy.Stateid + "','" + cy.Mobile + "','" + cy.Email + "','" + cy.Maritalstatus + "','" + cy.Djoining + "','" + cy.Dbirth + "','" + cy.AadharNumber + "','" + cy.AccNumber + "','" + cy.Bank + "','" + cy.Uname + "','" + cy.Pass + "','" + userId + "','" + DateTime.Now + "')";
                        SqlCommand objCmds = new SqlCommand(svSQL, objConn);
                        objCmds.ExecuteNonQuery();

                    }
                    else
                    {
                        svSQL = "Update USER_REGIST set EMPLOYEE_ID = '" + cy.EmpId + "',FNAME = '" + cy.Ename + "',GENDER = '" + cy.Gender + "',ADDRESS = '" + cy.Address + "',CITY_ID = '" + cy.Cityid + "',STATE_ID = '" + cy.Stateid + "',MOBILE = '" + cy.Mobile + "',EMAIL = '" + cy.Email + "',MARITALSTATUS = '" + cy.Maritalstatus + "',DATEOFJOINING = '" + cy.Djoining + "',DATEOFBIRTH = '" + cy.Dbirth + "',AANUMBER = '" + cy.AadharNumber + "',ACCNUMBER = '" + cy.AccNumber + "',BANK = '" + cy.Bank + "',USER_NAME = '" + cy.Uname + "',PASSWORD = '" + cy.Pass + "',UPDATED_BY = '" + userId + "',UPDATED_DATE = '" + DateTime.Now + "' WHERE USER_REGIST.ID ='" + cy.ID + "'";
                        SqlCommand objCmds = new SqlCommand(svSQL, objConn);
                        objCmds.ExecuteNonQuery();
                    }
                    objConn.Close();
                }


            }
            catch (Exception)
            {
                msg = "Error Occurs, While inserting / updating Data";
                throw;
            }

            return msg;
        }

        //public string EmployeeCRUD(EmpBasicDetails cy)
        //{
        //    string msg = "";
        //    try
        //    {
        //        string StatementType = string.Empty;
        //        //if (cy.ID == null)
        //        //{
        //        //    datatrans = new DataTransactions(_connectionString);


        //        //    int idc = datatrans.GetDataId(" SELECT LAST_NUMBER FROM SEQUENCE WHERE PREFIX = 'EMP' AND IS_ACTIVE = 'Y'");
        //        //    string EmpId = string.Format("{0}{1}{2}", "EMP/", "24-25/", (idc + 1).ToString());

        //        //    string updateCMd = " UPDATE SEQUENCE SET LAST_NUMBER ='" + (idc + 1).ToString() + "' WHERE PREFIX ='EMP' AND IS_ACTIVE ='Y'";
        //        //    try
        //        //    {
        //        //        datatrans.UpdateStatus(updateCMd);
        //        //    }
        //        //    catch (Exception ex)
        //        //    {
        //        //        throw ex;
        //        //    }
        //        //    cy.EmpId = EmpId;

        //        //}
        //        using (SqlConnection objConn = new SqlConnection(_connectionString))
        //        {
        //            SqlCommand objCmd = new SqlCommand("EmployeeProc", objConn);
        //            objCmd.CommandType = CommandType.StoredProcedure;
        //            if (cy.ID == null)
        //            {
        //                StatementType = "Insert";
        //                objCmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = DBNull.Value;
        //            }
        //            else
        //            {
        //                StatementType = "Update";
        //                objCmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = cy.ID;
        //            }
        //            objCmd.Parameters.Add("@EmpId", SqlDbType.NVarChar).Value = cy.EmpId;
        //            objCmd.Parameters.Add("@fname", SqlDbType.NVarChar).Value = cy.Ename;
        //            objCmd.Parameters.Add("@address", SqlDbType.NVarChar).Value = cy.Address;
        //            objCmd.Parameters.Add("@gender", SqlDbType.NVarChar).Value = cy.Gender;
        //            objCmd.Parameters.Add("@MaritalStatus", SqlDbType.NVarChar).Value = cy.Maritalstatus;
        //            objCmd.Parameters.Add("@cityname", SqlDbType.NVarChar).Value = cy.Cityid;
        //            objCmd.Parameters.Add("@statename", SqlDbType.NVarChar).Value = cy.Stateid;
        //            objCmd.Parameters.Add("@mobile", SqlDbType.NVarChar).Value = cy.Mobile;
        //            objCmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = cy.Email;
        //            objCmd.Parameters.Add("@DateOfJoin", SqlDbType.NVarChar).Value = cy.Djoining;
        //            objCmd.Parameters.Add("@DateOfBirth", SqlDbType.NVarChar).Value = cy.Dbirth;
        //            objCmd.Parameters.Add("@aadharnumber", SqlDbType.NVarChar).Value = cy.AadharNumber;
        //            objCmd.Parameters.Add("@bank", SqlDbType.NVarChar).Value = cy.Bank;
        //            objCmd.Parameters.Add("@accnumber", SqlDbType.NVarChar).Value = cy.AccNumber;
        //            objCmd.Parameters.Add("@username", SqlDbType.NVarChar).Value = cy.Uname;
        //            objCmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = cy.Pass;
        //            objCmd.Parameters.Add("@StatementType", SqlDbType.NVarChar).Value = StatementType;
        //            try
        //            {
        //                objConn.Open();
        //                objCmd.ExecuteNonQuery();

        //            }
        //            catch (Exception ex)
        //            {
        //                System.Console.WriteLine("Exception: {0}", ex.ToString());
        //            }
        //            objConn.Close();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        msg = "Error Occurs, While inserting / updating Data";
        //        throw;
        //    }

        //    return msg;
        //}

        public DataTable GetEditEmployeeDetail(string id)
        {
            string SvSql = string.Empty;
            SvSql = "SELECT ID,EMPLOYEE_ID,FNAME,GENDER,ADDRESS,CITY_ID,STATE_ID,MOBILE,EMAIL,MARITALSTATUS,CONVERT(varchar, USER_REGIST.DATEOFJOINING, 106) AS DATEOFJOINING,CONVERT(varchar, USER_REGIST.DATEOFBIRTH, 106) AS DATEOFBIRTH,AANUMBER,ACCNUMBER,BANK,USER_NAME,PASSWORD FROM USER_REGIST WHERE ID = '" + id + "' ";
            DataTable dtt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(SvSql, _connectionString);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.Fill(dtt);
            return dtt;
        }

        public string RemoveChange(string tag, string id)
        {
            try
            {
                string svSQL = string.Empty;
                using (SqlConnection objConnT = new SqlConnection(_connectionString))
                {
                    svSQL = "UPDATE USER_REGIST SET IS_ACTIVE ='Y' WHERE ID='" + id + "'";
                    SqlCommand objCmds = new SqlCommand(svSQL, objConnT);
                    objConnT.Open();
                    objCmds.ExecuteNonQuery();
                    objConnT.Close();
                }

            }
            catch (Exception)
            {
                throw;
            }
            return "";
        }

        public string StatusChange(string tag, string id)
        {
            try
            {
                string svSQL = string.Empty;
                using (SqlConnection objConnT = new SqlConnection(_connectionString))
                {
                    svSQL = "UPDATE USER_REGIST SET IS_ACTIVE ='N' WHERE ID='" + id + "'";
                    SqlCommand objCmds = new SqlCommand(svSQL, objConnT);
                    objConnT.Open();
                    objCmds.ExecuteNonQuery();
                    objConnT.Close();
                }

            }
            catch (Exception)
            {
                throw;
            }
            return "";
        }

    }
}
