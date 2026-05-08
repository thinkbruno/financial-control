namespace FinancialControl.Domain.Entities;

public enum TransactionType
{
    Income = 1,
    Expense = 2
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

    private Transaction()
    {
    }

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

        transaction.SetType(type);
        transaction.SetDescription(description);
        transaction.SetAmount(amount);
        transaction.SetDate(date);
        transaction.SetCategory(category);

        return transaction;
    }

    public void Update(
        string description,
        decimal amount,
        DateTime date,
        TransactionType type,
        string category)
    {
        SetType(type);
        SetDescription(description);
        SetAmount(amount);
        SetDate(date);
        SetCategory(category);
    }

    private void SetDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new InvalidOperationException(
                "Descrição é obrigatória");

        Description = description.Trim();
    }

    private void SetAmount(decimal amount)
    {
        if (amount <= 0)
            throw new InvalidOperationException(
                "Valor deve ser maior que zero");

        Amount = Type == TransactionType.Expense
            ? -Math.Abs(amount)
            : Math.Abs(amount);
    }

    private void SetDate(DateTime date)
    {
        if (date == default)
            throw new InvalidOperationException(
                "Data inválida");

        Date = DateTime.SpecifyKind(
            date,
            DateTimeKind.Utc
        );
    }

    private void SetCategory(string category)
    {
        if (string.IsNullOrWhiteSpace(category))
            throw new InvalidOperationException(
                "Categoria é obrigatória");

        Category = category.Trim();
    }

    private void SetType(TransactionType type)
    {
        if (!Enum.IsDefined(type))
            throw new InvalidOperationException(
                "Tipo de transação inválido");

        Type = type;
    }
}