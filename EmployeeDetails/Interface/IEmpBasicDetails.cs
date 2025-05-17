using System.Data;
using EmployeeDetails.Models;

namespace EmployeeDetails.Interface
{
    public interface IEmpBasicDetails
    {
        string EmployeeCRUD(EmpBasicDetails cy);
        DataTable GetAllEmployeeGRID(string strStatus);
        DataTable GetEditEmployeeDetail(string id);
        string RemoveChange(string tag, string id);
        string StatusChange(string tag, string id);
    }
}
