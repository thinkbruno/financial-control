using FinancialControl.Application.UseCases.Transactions.Queries.GetAllTransactions;
using FinancialControl.Domain.Interfaces;
using FinancialControl.UnitTests.Fakers;
using Moq;

namespace FinancialControl.UnitTests.Application.Transactions;

public class GetAllTransactionsUseCaseTests
{
    [Fact]
    public async Task Should_Return_All_Transactions()
    {
        var repositoryMock = new Mock<ITransactionRepository>();

        var transactions = TransactionFaker.Faker.Generate(5);

        repositoryMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(transactions);

        var useCase = new GetAllTransactionsUseCase(
            repositoryMock.Object);

        var result = await useCase.ExecuteAsync();

        Assert.Equal(5, result.Count());
    }
}