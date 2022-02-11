using EmployeePayroll_MVC.Manager;
using EmployeePayroll_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePayroll_MVC.Controllers
{
    public class EmployeeController : Controller
    {

        Repository repository = new Repository();

        public IActionResult GetEmployee()
        {
            List<EmployeeModel> emplist = new List<EmployeeModel>();
            emplist = repository.GetAllEmployee().ToList();
            return View(emplist);
        }
        public IActionResult AddEmployee()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddEmployee([Bind] EmployeeModel emp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repository.AddEmployee(emp);
                    return RedirectToAction("GetEmployee");
                }
                return View(emp);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        public IActionResult UpdateEmployee(int? id)
        {
            if (id == null)
            { 
                return NotFound();
            }
            EmployeeModel employee = repository.GetAllEmployee().Where(e => e.EmployeeId == id).FirstOrDefault();

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
        [HttpPost]
        public IActionResult UpdateEmployee([Bind] EmployeeModel emp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repository.UpdateEmployee(emp);
                    return RedirectToAction("GetEmployee");
                }
                return View(emp);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        public IActionResult DeleteEmployee(int? id)
        {
            EmployeeModel employee = repository.GetAllEmployee().Where(e => e.EmployeeId == id).FirstOrDefault();
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
        [HttpPost]
        public IActionResult DeleteEmployee(int id)
        {
            try
            {
                repository.DeleteEmployee(id);
                return RedirectToAction("GetEmployee");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}



