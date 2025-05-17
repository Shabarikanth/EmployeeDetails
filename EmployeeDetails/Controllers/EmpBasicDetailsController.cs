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
        public ActionResult EmpBasicDetails(EmpBasicDetails cy, string id)
        {

            try
            {
                cy.ID = id;
                string Strout = EmpBasicDetailsService.EmployeeCRUD(cy);
                if (string.IsNullOrEmpty(Strout))
                {
                    if (cy.ID == null)
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

            return View(cy);
        }

        public IActionResult ListEmpBasicDetails(string strStatus)
        {
            EmpBasicDetails ic = new EmpBasicDetails();
            return View(ic);
        }
        [HttpPost]
        public JsonResult MyListEmployeegrid(string strStatus)
        {
            var draw = Request.Form["draw"].ToString();
            var start = int.TryParse(Request.Form["start"], out int s) ? s : 0;
            var length = int.TryParse(Request.Form["length"], out int l) ? l : 10;
            var searchValue = Request.Form["search[value]"].ToString();

            string delPermission = "1";
            string editPermission = "1";

            var Reg = new List<EmpBasicDetailsgrid>();
            DataTable dt = EmpBasicDetailsService.GetAllEmployeeGRID(strStatus);
            foreach (DataRow row in dt.Rows)
            {
                string? isActive = row["IS_ACTIVE"].ToString();
                string delRow = "", editRow = "";

                if (isActive == "Y")
                {
                    if (delPermission == "1")
                        delRow = $"<a href='DeleteMR?tag=Del&id={row["ID"]}'><img src='../Images/delete.png' alt='Edit' cellsalign: 'center' /> </a>";

                    if (editPermission == "1")
                        editRow = $"<a href='EmpBasicDetails?id={row["ID"]}'><img src='../Images/edit.png' alt='Edit' cellsalign: 'center' /> </a>";
                }
                else if (delPermission == "1")
                {
                    delRow = $"<a href='Remove?tag=Del&id={row["ID"]}'><img src='../Images/reactive.png' alt='Edit' cellsalign: 'center' width='28' /> </a>";
                }

                Reg.Add(new EmpBasicDetailsgrid
                {
                    id = row["ID"].ToString(),
                    empid = row["EMPLOYEE_ID"].ToString(),
                    ename = row["FNAME"].ToString(),
                    mobile = row["MOBILE"].ToString(),
                    editrow = editRow,
                    delrow = delRow,
                    isactive = isActive
                });
            }

            var query = Reg.Where(e => e.isactive == strStatus);

            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                searchValue = searchValue.Trim();
                query = query.Where(e =>
                    (!string.IsNullOrEmpty(e.id) && e.id.Contains(searchValue, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(e.empid) && e.empid.Contains(searchValue, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(e.ename) && e.ename.Contains(searchValue, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(e.mobile) && e.mobile.Contains(searchValue, StringComparison.OrdinalIgnoreCase))

                );
            }

            var recordsTotal = query.Count();

            var data = query
                .OrderByDescending(e => e.id)
                .Skip(start)
                .Take(length)
                .Select(e => new
                {
                    e.empid,
                    e.ename,
                    e.mobile,
                    e.editrow,
                    e.delrow
                })
                .ToList();

            return Json(new
            {
                draw,
                recordsTotal,
                recordsFiltered = recordsTotal,
                data
            });

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
