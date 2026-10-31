using Microsoft.AspNetCore.Mvc;
using FinanceApi.Models;

namespace FinanceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    // 模拟数据库数据
    private static List<Account> _accounts = new()
    {
        new Account { Id = 1, Name = "Main Wallet", Balance = 1000.50m },
        new Account { Id = 2, Name = "Savings", Balance = 5000.00m }
    };

    [HttpGet]
    public ActionResult<IEnumerable<Account>> GetAccounts() => Ok(_accounts);

    [HttpPost]
    public ActionResult<Account> CreateAccount(Account account)
    {
        account.Id = _accounts.Count + 1;
        _accounts.Add(account);
        return CreatedAtAction(nameof(GetAccounts), new { id = account.Id }, account);
    }
}

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateTransaction(Transaction transaction)
    {
        if (transaction.Amount <= 0) 
            return BadRequest("Amount must be positive.");
        
        // 模拟余额更新逻辑
        return Ok(new { Message = "Transaction recorded, balance updated atomically.", Data = transaction });
    }
}