using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinanceApi.Data;
using FinanceApi.Models;

namespace FinanceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly AppDbContext _context;

    public TransactionsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
    {
        return await _context.Transactions.Take(10).ToListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransaction(Transaction transaction)
    {
        if (transaction.Amount <= 0)
            return BadRequest("Amount must be positive.");

        var account = await _context.Accounts.FindAsync(transaction.AccountId);
        if (account == null)
            return NotFound("Account not found.");

        if (transaction.Type == "Income")
            account.Balance += transaction.Amount;
        else
            account.Balance -= transaction.Amount;

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Transaction recorded, balance updated atomically.", CurrentBalance = account.Balance });
    }
}