using Bogus;
using FinancialControl.Domain.Entities;

namespace FinancialControl.UnitTests.Fakers;

public static class TransactionFaker
{
    public static Faker<Transaction> Faker =>
        new Faker<Transaction>()
            .CustomInstantiator(f => Transaction.Create(
                f.Commerce.ProductName(),
                f.Random.Decimal(10, 1000),
                DateTime.UtcNow,
                f.PickRandom<TransactionType>(),
                f.Commerce.Categories(1)[0]
            ));
}