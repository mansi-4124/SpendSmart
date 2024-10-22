using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ExpenseTracker.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> GenerateExcel(DateTime startDate, DateTime endDate)
        {
            // Set the license context (required for EPPlus 5 and later versions)
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Get transactions for the selected date range
            var transactions = await _context.Transactions
                .Include(t => t.Category)  // Include related Category data
                .Where(t => t.Date >= startDate && t.Date <= endDate)
                .ToListAsync();

            // Create a new Excel package using EPPlus
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Income and Expenses");

                // Add headers to the Excel sheet
                worksheet.Cells[1, 1].Value = "Date";
                worksheet.Cells[1, 2].Value = "Category";
                worksheet.Cells[1, 3].Value = "Amount";
                worksheet.Cells[1, 4].Value = "Type";
                worksheet.Cells[1, 5].Value = "Note";

                // Add data rows from the transactions list
                int row = 2;
                foreach (var transaction in transactions)
                {
                    worksheet.Cells[row, 1].Value = transaction.Date.ToString("yyyy-MM-dd");
                    worksheet.Cells[row, 2].Value = transaction.CategoryTitleWithIcon;
                    worksheet.Cells[row, 3].Value = transaction.FormattedAmount;
                    worksheet.Cells[row, 4].Value = transaction.Category?.Type;  // Income or Expense
                    worksheet.Cells[row, 5].Value = transaction.Note;
                    row++;
                }

                // Auto-fit columns for readability
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Convert the Excel file to a stream and return it as a file download
                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                string excelFileName = $"Income_Expenses_{startDate:yyyyMMdd}_{endDate:yyyyMMdd}.xlsx";

                return File(stream, "application/vndnxmlformats-officedocument.spreadsheetml.sheet", excelFileName);
            }
        }
    }
}