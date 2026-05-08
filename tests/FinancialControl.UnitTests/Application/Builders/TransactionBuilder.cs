using FinancialControl.Domain.Entities;

namespace FinancialControl.UnitTests.Builders;

public class TransactionBuilder
{
    private string _description = "Salário";

    private decimal _amount = 1000;

    private DateTime _date = DateTime.UtcNow;

    private TransactionType _type = TransactionType.Income;

    private string _category = "Renda";

    public static TransactionBuilder New()
    {
        return new TransactionBuilder();
    }

    public TransactionBuilder WithDescription(string description)
    {
        _description = description;

        return this;
    }

    public TransactionBuilder WithAmount(decimal amount)
    {
        _amount = amount;

        return this;
    }

    public TransactionBuilder WithDate(DateTime date)
    {
        _date = date;

        return this;
    }

    public TransactionBuilder WithType(TransactionType type)
    {
        _type = type;

        return this;
    }

    public TransactionBuilder WithCategory(string category)
    {
        _category = category;

        return this;
    }

    public Transaction Build()
    {
        return Transaction.Create(
            _description,
            _amount,
            _date,
            _type,
            _category
        );
    }
}