namespace FinanceApi.Models;

public class Account
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Balance { get; set; }
}

public class Transaction
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; } = "Expense"; // Income or Expense
    public DateTime Date { get; set; } = DateTime.UtcNow;
}