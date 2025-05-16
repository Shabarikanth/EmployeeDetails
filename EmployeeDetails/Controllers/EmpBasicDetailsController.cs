using System.Data;
using EmployeeDetails.Interface;
using EmployeeDetails.Models;
using EmployeeDetails.Service;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeDetails.Controllers
{
    public class EmpBasicDetailsController : Controller
    {
        IEmpBasicDetails EmpBasicDetailsService;
        public EmpBasicDetailsController(IEmpBasicDetails _EmpBasicDetailsService)
        {
            EmpBasicDetailsService = _EmpBasicDetailsService;
        }
        public IActionResult EmpBasicDetails(string id)
        {
            EmpBasicDetails ic = new EmpBasicDetails();
            if (id == null)
            {

            }
            else 
            {
                DataTable dt = new DataTable();
                dt = EmpBasicDetailsService.GetEditEmployeeDetail(id);
                if (dt.Rows.Count > 0)
                {
                    ic.ID = dt.Rows[0]["ID"].ToString();
                    ic.EmpId = dt.Rows[0]["EMPLOYEE_ID"].ToString();
                    ic.Ename = dt.Rows[0]["FNAME"].ToString();
                    ic.Gender = dt.Rows[0]["GENDER"].ToString();
                    ic.Address = dt.Rows[0]["ADDRESS"].ToString();
                    ic.Cityid = dt.Rows[0]["CITY_ID"].ToString();
                    ic.Stateid = dt.Rows[0]["STATE_ID"].ToString();
                    ic.Mobile = dt.Rows[0]["MOBILE"].ToString();
                    ic.Email = dt.Rows[0]["EMAIL"].ToString();
                    ic.Department = dt.Rows[0]["DEPARTMENT"].ToString();
                    ic.Maritalstatus = dt.Rows[0]["MARITALSTATUS"].ToString();
                    ic.Djoining = dt.Rows[0]["DATEOFJOINING"].ToString();
                    ic.Dbirth = dt.Rows[0]["DATEOFBIRTH"].ToString();
                    ic.Bank = dt.Rows[0]["BANK"].ToString();
                    ic.AccNumber = dt.Rows[0]["ACCNUMBER"].ToString();
                    ic.AadharNumber = dt.Rows[0]["AANUMBER"].ToString();
                    ic.Uname = dt.Rows[0]["USER_NAME"].ToString();
                    ic.Pass = dt.Rows[0]["PASSWORD"].ToString();

                }
            }
            return View(ic);
        }
        [HttpPost]
        public ActionResult EmpBasicDetails(EmpBasicDetails Ic, string id)
        {

            try
            {
                Ic.ID = id;
                string Strout = EmpBasicDetailsService.EmployeeCRUD(Ic);
                if (string.IsNullOrEmpty(Strout))
                {
                    if (Ic.ID == null)
                    {
                        TempData["notice"] = "Employee Inserted Successfully...!";
                    }
                    else
                    {
                        TempData["notice"] = "Employee Updated Successfully...!";
                    }
                    return RedirectToAction("ListEmpBasicDetails");
                }

                else
                {
                    ViewBag.PageTitle = "Edit Employee";
                    TempData["notice"] = Strout;
                    //return View();
                }

                // }
            }
            catch (Exception)
            {
                throw;
            }

            return View(Ic);
        }

        public IActionResult ListEmpBasicDetails()
        {
            return View();
        }

        public ActionResult DeleteMR(string tag, string id)
        {

            string flag = EmpBasicDetailsService.StatusChange(tag, id);
            if (string.IsNullOrEmpty(flag))
            {

                return RedirectToAction("ListEmpBasicDetails");
            }
            else
            {
                TempData["notice"] = flag;
                return RedirectToAction("ListEmpBasicDetails");
            }
        }
        public ActionResult Remove(string tag, string id)
        {

            string flag = EmpBasicDetailsService.RemoveChange(tag, id);
            if (string.IsNullOrEmpty(flag))
            {

                return RedirectToAction("ListEmpBasicDetails");
            }
            else
            {
                TempData["notice"] = flag;
                return RedirectToAction("ListEmpBasicDetails");
            }
        }
    }
}
