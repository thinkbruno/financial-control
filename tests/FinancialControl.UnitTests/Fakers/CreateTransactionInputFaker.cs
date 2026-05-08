using Bogus;
using FinancialControl.Application.UseCases.Transactions.Commands.CreateTransaction;
using FinancialControl.Domain.Entities;

namespace FinancialControl.UnitTests.Fakers;

public static class CreateTransactionInputFaker
{
    public static Faker<CreateTransactionInput> Faker =>
        new Faker<CreateTransactionInput>()
            .RuleFor(x => x.Description, f => f.Commerce.ProductName())
            .RuleFor(x => x.Amount, f => f.Random.Decimal(10, 5000))
            .RuleFor(x => x.Date, _ => DateTime.UtcNow)
            .RuleFor(x => x.Type, f => f.PickRandom<TransactionType>())
            .RuleFor(x => x.Category, f => f.Commerce.Categories(1)[0]);
}