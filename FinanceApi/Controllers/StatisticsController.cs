using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinanceApi.Data;

namespace FinanceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatisticsController : ControllerBase
{
    private readonly AppDbContext _context;

    public StatisticsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("monthly")]
    public async Task<IActionResult> GetMonthlyStatistics()
    {
        var transactions = await _context.Transactions.ToListAsync();
        
        var stats = transactions
            .GroupBy(t => new { t.Date.Year, t.Date.Month })
            .Select(g => new
            {
                Year = g.Key.Year,
                Month = g.Key.Month,
                TotalIncome = g.Where(t => t.Type == "Income").Sum(t => t.Amount),
                TotalExpense = g.Where(t => t.Type == "Expense").Sum(t => t.Amount)
            }).ToList();

        return Ok(stats);
    }
}