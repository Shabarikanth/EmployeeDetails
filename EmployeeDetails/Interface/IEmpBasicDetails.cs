using System.Data;
using EmployeeDetails.Models;

namespace EmployeeDetails.Interface
{
    public interface IEmpBasicDetails
    {
        string EmployeeCRUD(EmpBasicDetails ic);
        DataTable GetEditEmployeeDetail(string id);
        string RemoveChange(string tag, string id);
        string StatusChange(string tag, string id);
    }
}
