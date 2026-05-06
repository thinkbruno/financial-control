using FinancialControl.Application.Interfaces;
using FinancialControl.Application.UseCases.Transactions;
using FinancialControl.Domain.Entities;
using FinancialControl.Domain.Events;
using FinancialControl.Domain.Interfaces;
using Moq;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FinancialControl.UnitTests.Application;

public class CreateTransactionUseCaseTests
{
    [Fact]
    public async Task Should_Create_Transaction_And_Publish_Event()
    {
        // Arrange
        var repositoryMock = new Mock<ITransactionRepository>();
        var eventPublisherMock = new Mock<IEventPublisher>();

        var useCase = new CreateTransactionUseCase(
            repositoryMock.Object,
            eventPublisherMock.Object
        );

        var input = new CreateTransactionInput
        {
            Description = "Teste",
            Amount = 100,
            Date = DateTime.UtcNow,
            Type = TransactionType.Income,
            Category = "Teste"
        };

        // Act
        var result = await useCase.Execute(input);

        // Assert
        repositoryMock.Verify(r =>
            r.AddAsync(It.IsAny<Transaction>(), It.IsAny<CancellationToken>()),
            Times.Once
        );

        eventPublisherMock.Verify(p =>
            p.PublishAsync(
                It.Is<TransactionCreatedEvent>(e =>
                    e.Amount == 100 &&
                    e.Description == "Teste"
                ),
                It.IsAny<CancellationToken>()
            ),
            Times.Once
        );

        Assert.NotNull(result);
        Assert.Equal(100, result.Amount);
        Assert.Equal("Teste", result.Description);
    }
}