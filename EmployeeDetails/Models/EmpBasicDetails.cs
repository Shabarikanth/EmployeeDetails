using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeDetails.Models
{
    public class EmpBasicDetails
    {
        public string? ID { get; set; }
        public string? EmpId { get; set; }
        public string? Ename { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? Cityid { get; set; }
        public string? Stateid { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? Department { get; set; }
        public string? Maritalstatus { get; set; }
        public string? Djoining { get; set; }
        public string? Dbirth { get; set; }
        public string? Bank { get; set; }
        public string? AadharNumber { get; set; }
        public string? AccNumber { get; set; }

        public string? Uname { get; set; }
        public string? Pass { get; set; }
        public string? Ddlstatus { get; set; }
        public List<EmpBasicDetailsgrid>? Employeelst { get; set; }

    //public string? Qualification { get; set; }
    //public string? University { get; set; }
    //public string? School { get; set; }
    //public string? College { get; set; }
    //public string? Place { get; set; }
    //public string? YPassing { get; set; }
}

    public class EmpBasicDetailsgrid
    {
        public string? id { get; set; }
        public string? empid { get; set; }
        public string? ename { get; set; }
        public string? mobile { get; set; }
        public string? editrow { get; set; }
        public string? delrow { get; set; }
        public string? isactive { get; set; }
    }
}
