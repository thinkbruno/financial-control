using FinancialControl.Domain.Entities;
using FinancialControl.UnitTests.Builders;

namespace FinancialControl.UnitTests.Domain;

public class TransactionTests
{
    [Fact]
    public void Should_Create_Income_With_Positive_Amount()
    {
        var transaction = TransactionBuilder.New()
            .WithType(TransactionType.Income)
            .WithAmount(100)
            .Build();

        Assert.Equal(100, transaction.Amount);
    }

    [Fact]
    public void Should_Create_Expense_With_Negative_Amount()
    {
        var transaction = TransactionBuilder.New()
            .WithType(TransactionType.Expense)
            .WithAmount(100)
            .Build();

        Assert.Equal(-100, transaction.Amount);
    }

    [Fact]
    public void Should_Throw_When_Description_Is_Empty()
    {
        Assert.Throws<InvalidOperationException>(() =>
        {
            TransactionBuilder.New()
                .WithDescription("")
                .Build();
        });
    }

    [Fact]
    public void Should_Throw_When_Category_Is_Empty()
    {
        Assert.Throws<InvalidOperationException>(() =>
        {
            TransactionBuilder.New()
                .WithCategory("")
                .Build();
        });
    }

    [Fact]
    public void Should_Throw_When_Amount_Is_Invalid()
    {
        Assert.Throws<InvalidOperationException>(() =>
        {
            TransactionBuilder.New()
                .WithAmount(0)
                .Build();
        });
    }
}