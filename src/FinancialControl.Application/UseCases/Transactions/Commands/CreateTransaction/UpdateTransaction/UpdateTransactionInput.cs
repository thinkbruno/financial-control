namespace FinancialControl.Application.UseCases.Transactions.Commands.UpdateTransaction;

using FinancialControl.Domain.Entities;

public class UpdateTransactionInput
{
    public string Description { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public DateTime Date { get; set; }

    public TransactionType Type { get; set; }

    public string Category { get; set; } = string.Empty;
}