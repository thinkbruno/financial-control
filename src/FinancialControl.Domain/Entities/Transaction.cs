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
        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow
        };

        transaction.SetDescription(description);
        transaction.SetAmount(amount, type);
        transaction.SetDate(date);
        transaction.SetCategory(category);

        transaction.Type = type;

        return transaction;
    }

    public void Update(
        string description,
        decimal amount,
        DateTime date,
        string category)
    {
        SetDescription(description);
        SetAmount(amount, Type);
        SetDate(date);
        SetCategory(category);
    }

    private void SetDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new InvalidOperationException("Descrição é obrigatória");

        Description = description;
    }

    private void SetAmount(decimal amount, TransactionType type)
    {
        if (amount <= 0)
            throw new InvalidOperationException("Valor deve ser maior que zero");

        Amount = type == TransactionType.Expense
            ? -Math.Abs(amount)
            : Math.Abs(amount);
    }

    private void SetDate(DateTime date)
    {
        if (date == default)
            throw new InvalidOperationException("Data inválida");

        Date = date;
    }

    private void SetCategory(string category)
    {
        if (string.IsNullOrWhiteSpace(category))
            throw new InvalidOperationException("Categoria é obrigatória");

        Category = category;
    }
}