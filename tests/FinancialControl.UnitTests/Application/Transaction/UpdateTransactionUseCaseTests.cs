using FinancialControl.Application.UseCases.Transactions.Commands.UpdateTransaction;
using FinancialControl.Domain.Interfaces;
using FinancialControl.UnitTests.Builders;
using FinancialControl.Domain.Entities;
using Moq;

namespace FinancialControl.UnitTests.Application.Transactions;

public class UpdateTransactionUseCaseTests
{
    [Fact]
    public async Task Should_Update_Transaction()
    {
        var repositoryMock = new Mock<ITransactionRepository>();

        var transaction = TransactionBuilder.New().Build();

        repositoryMock
            .Setup(x => x.GetByIdAsync(transaction.Id))
            .ReturnsAsync(transaction);

        var input = new UpdateTransactionInput
        {
            Description = "Novo nome",
            Amount = 500,
            Date = DateTime.UtcNow,
            Type = TransactionType.Income,
            Category = "Atualizado"
        };

        var useCase = new UpdateTransactionUseCase(
            repositoryMock.Object);

        var result = await useCase.ExecuteAsync(
            transaction.Id,
            input);

        Assert.True(result);
    }
}