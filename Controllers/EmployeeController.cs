using Microsoft.AspNetCore.Mvc;
using System.Linq;
using QuarterlySalesApp.Data;
using QuarterlySalesApp.Models;

namespace QuarterlySalesApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Add()
        {
            ViewBag.Managers = _context.Employees.Select(e => new {
                e.EmployeeId,
                FullName = e.FirstName + " " + e.LastName
            }).ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Add(Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (IsEmployeeDuplicate(employee))
                {
                    ModelState.AddModelError("", $"{employee.FirstName} {employee.LastName} (DOB: {employee.DOB.ToShortDateString()}) is already in the database.");
                }
                if (employee.ManagerId == employee.EmployeeId)
                {
                    ModelState.AddModelError("", "Manager and employee can't be the same person.");
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Employees.Add(employee);
                        _context.SaveChanges();
                        return RedirectToAction("Index", "Home");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "An error occurred while saving the employee.");
                    }
                }
            }
            ViewBag.Managers = _context.Employees.Select(e => new {
                e.EmployeeId,
                FullName = e.FirstName + " " + e.LastName
            }).ToList();
            return View(employee);
        }

        private bool IsEmployeeDuplicate(Employee employee)
        {
            return _context.Employees.Any(e => e.FirstName == employee.FirstName &&
                                               e.LastName == employee.LastName &&
                                               e.DOB == employee.DOB);
        }
    }
}
