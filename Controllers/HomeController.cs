using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuarterlySalesApp.Data;
using QuarterlySalesApp.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QuarterlySalesApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchEmployee, int? searchYear, int? searchQuarter, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["EmployeeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "employee_desc" : "";
            ViewData["YearSortParm"] = sortOrder == "Year" ? "year_desc" : "Year";
            ViewData["QuarterSortParm"] = sortOrder == "Quarter" ? "quarter_desc" : "Quarter";
            ViewData["AmountSortParm"] = sortOrder == "Amount" ? "amount_desc" : "Amount";

            if (searchEmployee != null || searchYear != null || searchQuarter != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchEmployee = currentFilter;
            }

            ViewData["CurrentFilter"] = searchEmployee;

            var sales = from s in _context.Sales.Include(s => s.Employee) select s;

            if (!String.IsNullOrEmpty(searchEmployee))
            {
                sales = sales.Where(s => (s.Employee.FirstName + " " + s.Employee.LastName).Contains(searchEmployee));
            }
            if (searchYear != null)
            {
                sales = sales.Where(s => s.Year == searchYear);
            }
            if (searchQuarter != null)
            {
                sales = sales.Where(s => s.Quarter == searchQuarter);
            }

            switch (sortOrder)
            {
                case "employee_desc":
                    sales = sales.OrderByDescending(s => s.Employee.LastName);
                    break;
                case "Year":
                    sales = sales.OrderBy(s => s.Year);
                    break;
                case "year_desc":
                    sales = sales.OrderByDescending(s => s.Year);
                    break;
                case "Quarter":
                    sales = sales.OrderBy(s => s.Quarter);
                    break;
                case "quarter_desc":
                    sales = sales.OrderByDescending(s => s.Quarter);
                    break;
                case "Amount":
                    sales = sales.OrderBy(s => s.Amount);
                    break;
                case "amount_desc":
                    sales = sales.OrderByDescending(s => s.Amount);
                    break;
                default:
                    sales = sales.OrderBy(s => s.Employee.LastName);
                    break;
            }

            int pageSize = 4;
            var paginatedSales = await PaginatedList<SalesData>.CreateAsync(sales.AsNoTracking(), pageNumber ?? 1, pageSize);

            ViewBag.Employees = _context.Employees.Select(e => new {
                FullName = e.FirstName + " " + e.LastName
            }).ToList();

            return View(paginatedSales);
        }
    }
}
