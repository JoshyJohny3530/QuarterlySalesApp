using Microsoft.AspNetCore.Mvc;
using System.Linq;
using QuarterlySalesApp.Data;
using QuarterlySalesApp.Models;

namespace QuarterlySalesApp.Controllers
{
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Add()
        {
            ViewBag.Employees = _context.Employees.Select(e => new {
                e.EmployeeId,
                FullName = e.FirstName + " " + e.LastName
            }).ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Add(SalesData salesData)
        {
            if (ModelState.IsValid)
            {
                if (IsSalesDataDuplicate(salesData))
                {
                    ModelState.AddModelError("", $"Sales for {salesData.Employee?.FirstName} {salesData.Employee?.LastName} for {salesData.Year} Q{salesData.Quarter} are already in the database.");
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Sales.Add(salesData);
                        _context.SaveChanges();
                        return RedirectToAction("Index", "Home");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "An error occurred while saving the sales data.");
                    }
                }
            }
            ViewBag.Employees = _context.Employees.Select(e => new {
                e.EmployeeId,
                FullName = e.FirstName + " " + e.LastName
            }).ToList();
            return View(salesData);
        }

        private bool IsSalesDataDuplicate(SalesData salesData)
        {
            return _context.Sales.Any(s => s.Quarter == salesData.Quarter &&
                                           s.Year == salesData.Year &&
                                           s.EmployeeId == salesData.EmployeeId);
        }
    }
}
