namespace FinancialControl.Domain.Entities;

public enum TransactionType
{
    Income,
    Expense
}

public class Transaction
{
    public Guid Id { get; private set; }
    public string Description { get; private set; } = null!;
    public decimal Amount { get; private set; }
    public DateTime Date { get; private set; }
    public TransactionType Type { get; private set; }
    public string Category { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }

    private Transaction() { }

    public static Transaction Create(
        string description,
        decimal amount,
        DateTime date,
        TransactionType type,
        string category)
    {
        Validate(description, amount, date, category);

        return new Transaction
        {
            Id = Guid.NewGuid(),
            Description = description,
            Amount = amount,
            Date = date,
            Type = type,
            Category = category,
            CreatedAt = DateTime.UtcNow
        };
    }

    private static void Validate(
        string description,
        decimal amount,
        DateTime date,
        string category)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Descrição é obrigatória");

        if (amount <= 0)
            throw new ArgumentException("Valor deve ser maior que zero");

        if (string.IsNullOrWhiteSpace(category))
            throw new ArgumentException("Categoria é obrigatória");

        if (date == default)
            throw new ArgumentException("Data inválida");
    }
}