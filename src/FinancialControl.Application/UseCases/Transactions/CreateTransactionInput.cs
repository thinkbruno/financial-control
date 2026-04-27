using FinancialControl.Domain.Entities;
namespace FinancialControl.Application.UseCases.Transactions;

public class CreateTransactionInput
{
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public TransactionType Type { get; set; }
    public string Category { get; set; } = string.Empty;
}