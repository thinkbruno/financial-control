using FinancialControl.Application.Interfaces;
using FinancialControl.Application.UseCases.Transactions.Commands.CreateTransaction;
using FinancialControl.Domain.Entities;
using FinancialControl.Domain.Events;
using FinancialControl.Domain.Interfaces;
using Moq;

namespace FinancialControl.UnitTests.Application;

public class CreateTransactionUseCaseTests
{
    [Fact]
    public async Task Should_Create_Transaction_Successfully()
    {
        // Arrange
        var repositoryMock = new Mock<ITransactionRepository>();

        var eventPublisherMock = new Mock<IEventPublisher>();

        eventPublisherMock
            .Setup(x => x.PublishAsync(
                It.IsAny<TransactionCreatedEvent>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var useCase = new CreateTransactionUseCase(
            repositoryMock.Object,
            eventPublisherMock.Object);

        var input = new CreateTransactionInput
        {
            Description = "Salário",
            Amount = 5000,
            Date = DateTime.UtcNow,
            Type = TransactionType.Income,
            Category = "Renda"
        };

        // Act
        var result = await useCase.ExecuteAsync(input);

        // Assert
        Assert.NotNull(result);

        Assert.Equal(input.Description, result.Description);

        repositoryMock.Verify(
            x => x.AddAsync(It.IsAny<Transaction>()),
            Times.Once);

        eventPublisherMock.Verify(
            x => x.PublishAsync(
                It.IsAny<TransactionCreatedEvent>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}