using FinancialControl.Application.UseCases.Transactions.Commands.DeleteTransaction;
using FinancialControl.Domain.Interfaces;
using FinancialControl.UnitTests.Builders;
using Moq;

namespace FinancialControl.UnitTests.Application.Transactions;

public class DeleteTransactionUseCaseTests
{
    [Fact]
    public async Task Should_Delete_Transaction()
    {
        var repositoryMock = new Mock<ITransactionRepository>();

        var transaction = TransactionBuilder.New().Build();

        repositoryMock
            .Setup(x => x.GetByIdAsync(transaction.Id))
            .ReturnsAsync(transaction);

        var useCase = new DeleteTransactionUseCase(
            repositoryMock.Object);

        var result = await useCase.ExecuteAsync(transaction.Id);

        Assert.True(result);
    }
}