using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeRegistration.Repository;
using EmployeeRegistration.Models;

namespace EmployeeRegistration.Controllers
{
    public class EmployeesController : Controller
    {

        private IEmployeeRepository _employeeRepository = new EmployeeRepository();

        
        // GET: Employees
        public ActionResult Index(string SearchString)
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                return View(_employeeRepository.GetEmployeeByName(SearchString));
            }
            else
            {
                return View(_employeeRepository.GetAllEmployee());
            }
        }

        // GET: Employees/Details/5
        public ActionResult Details(int id)
        {
            return View(_employeeRepository.GetEmployeeById(id));
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeTable employee)
        {
            if (ModelState.IsValid)
            {
                int result = _employeeRepository.AddEmployee(employee);
                if (result > 0)
                {
                    return RedirectToAction("Index", "Employees");
                }
                else
                {
                    ViewBag.ErrorMesssage = "Registeration of new Employee Failed";
                    return RedirectToAction("Create");
                }
            }
            return View();
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_employeeRepository.GetEmployeeById(id));
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeTable employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int result = _employeeRepository.UpdateEmployee(employee);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return RedirectToAction("Index");

                }
            }
            return View();
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_employeeRepository.GetEmployeeById(id));
        }

        // POST: Employees/Delete/5
        [HttpPost]
        public ActionResult Delete(EmployeeTable employee)
        {
            try
            {
                _employeeRepository.DeleteEmployee(employee.ID);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //To check in database the RFC exists or not
        public JsonResult IsRFCExist(string RFC, int? ID)
        {
            var validateRFC = _employeeRepository.IsRFCExists(RFC, ID);
            if (validateRFC != null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
