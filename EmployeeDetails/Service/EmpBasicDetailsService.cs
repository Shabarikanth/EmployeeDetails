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

        public string EmployeeCRUD(EmpBasicDetails ic)
        {
            throw new NotImplementedException();
        }

        public DataTable GetEditEmployeeDetail(string id)
        {
            string SvSql = string.Empty;
            SvSql = "SELECT ID,EMPLOYEE_ID,FNAME,GENDER,ADDRESS,CITY_ID,STATE_ID,COUNTRY_ID,MOBILE,EMAIL,REMARKS,APPROVED_BY,BRANCH,DEPARTMENT,MARITALSTATUS,EMAILPERSONAL,CONVERT(varchar, USER_REGIST.DATEOFJOINING, 106) AS DATEOFJOINING,CONVERT(varchar, USER_REGIST.DATEOFBIRTH, 106) AS DATEOFBIRTH,CONVERT(varchar, USER_REGIST.DATEOFLEAVING, 106) AS DATEOFLEAVING,DEGREE,EMPLOYEE_STATUS,REPORT_TO,AANUMBER,ACCNUMBER,BANK,USER_NAME,PASSWORD FROM USER_REGIST WHERE ID = '" + id + "' ";
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
