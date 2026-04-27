using FinancialControl.Domain.Entities;
using Xunit;

namespace FinancialControl.UnitTests.Domain;

public class TransactionTests
{
    [Fact]
    public void Should_Create_Valid_Transaction()
    {
        var transaction = Transaction.Create(
            "Salário",
            5000,
            DateTime.UtcNow,
            TransactionType.Income,
            "Renda"
        );

        Assert.NotNull(transaction);
        Assert.Equal(5000, transaction.Amount);
    }

    [Fact]
    public void Should_Throw_When_Amount_Is_Invalid()
    {
        Assert.Throws<ArgumentException>(() =>
            Transaction.Create(
                "Teste",
                0,
                DateTime.UtcNow,
                TransactionType.Expense,
                "Erro"
            )
        );
    }
}