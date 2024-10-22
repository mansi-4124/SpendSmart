using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace ExpenseTracker.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            // Get current user
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToPage("/Account/Login", new { area = "Identity" });

            // Financial Overview
            var income = await _context.Transactions
                .Where(t => t.UserId == user.Id && t.Category.Type == "Income")
                .SumAsync(t => t.Amount);

            var expenses = await _context.Transactions
                .Where(t => t.UserId == user.Id && t.Category.Type == "Expense")
                .SumAsync(t => t.Amount);

            var balance = income - expenses;

            // Recent Transactions
            var recentTransactions = await _context.Transactions
                .Where(t => t.UserId == user.Id)
                .OrderByDescending(t => t.Date)
                .Take(10)
                .Include(t => t.Category) // Include related category
                .ToListAsync();

            // Budgets Progress
            var budgets = await _context.Budgets
                .Where(b => b.UserId == user.Id)
                .Include(b => b.Category)
                .ToListAsync();

            // Expenses by Category for Pie Chart
            var expensesByCategory = await _context.Transactions
                .Where(t => t.UserId == user.Id && t.Category.Type == "Expense")
                .GroupBy(t => t.Category.Title)
                .Select(g => new
                {
                    Category = g.Key,
                    TotalAmount = g.Sum(t => t.Amount)
                })
                .ToListAsync();

            // Expenses over Time for Bar Chart
            var expensesOverTime = await _context.Transactions
                .Where(t => t.UserId == user.Id && t.Category.Type == "Expense")
                .GroupBy(t => t.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    TotalAmount = g.Sum(t => t.Amount)
                })
                .OrderBy(g => g.Date)
                .ToListAsync();

            // Prepare data for JavaScript
            ViewBag.ExpensesByCategoryLabels = expensesByCategory.Select(e => e.Category).ToArray();
            ViewBag.ExpensesByCategoryData = expensesByCategory.Select(e => e.TotalAmount).ToArray();

            ViewBag.ExpensesOverTimeLabels = expensesOverTime.Select(e => e.Date.ToString("yyyy-MM-dd")).ToArray();
            ViewBag.ExpensesOverTimeData = expensesOverTime.Select(e => e.TotalAmount).ToArray();

            // ViewModel to pass data to the view
            var viewModel = new HomeViewModel
            {
                Balance = balance,
                Income = income,
                Expenses = expenses,
                RecentTransactions = recentTransactions,
                Budgets = budgets
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
