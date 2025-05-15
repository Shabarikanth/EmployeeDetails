using EmployeeDetails.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeDetails.Controllers
{
    public class EmpBasicDetailsController : Controller
    {
        public IActionResult EmpBasicDetails(string id)
        {
            EmpBasicDetails ic = new EmpBasicDetails();
            if (id == null)
            {

            }
            else 
            {

            }
            return View(ic);
        }

        public IActionResult ListEmpBasicDetails()
        {
            return View();
        }
    }
}
