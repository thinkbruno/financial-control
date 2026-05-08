using FinancialControl.Application.UseCases.Transactions.Queries.GetTransactionById;
using FinancialControl.Domain.Interfaces;
using FinancialControl.UnitTests.Builders;
using Moq;

namespace FinancialControl.UnitTests.Application.Transactions;

public class GetTransactionByIdUseCaseTests
{
    [Fact]
    public async Task Should_Return_Transaction_When_Exists()
    {
        var repositoryMock = new Mock<ITransactionRepository>();

        var transaction = TransactionBuilder.New().Build();

        repositoryMock
            .Setup(x => x.GetByIdAsync(transaction.Id))
            .ReturnsAsync(transaction);

        var useCase = new GetTransactionByIdUseCase(
            repositoryMock.Object);

        var result = await useCase.ExecuteAsync(transaction.Id);

        Assert.NotNull(result);
    }
}