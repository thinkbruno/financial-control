using FinancialControl.Application.UseCases.Transactions;
using FinancialControl.Domain.Interfaces;
using Moq;
using Xunit;
using MassTransit;

namespace FinancialControl.UnitTests.Application;

public class CreateTransactionUseCaseTests
{
    [Fact]
    public async Task Should_Create_Transaction()
    {
        var repositoryMock = new Mock<ITransactionRepository>();
        var publishMock = new Mock<IPublishEndpoint>();

        var useCase = new CreateTransactionUseCase(
            repositoryMock.Object,
            publishMock.Object
        );

        var input = new CreateTransactionInput
        {
            Description = "Teste",
            Amount = 100,
            Date = DateTime.UtcNow,
            Type = FinancialControl.Domain.Entities.TransactionType.Income,
            Category = "Teste"
        };

        var result = await useCase.Execute(input);

        repositoryMock.Verify(r => r.AddAsync(It.IsAny<FinancialControl.Domain.Entities.Transaction>()), Times.Once);
        publishMock.Verify(p =>
            p.Publish(
                It.Is<FinancialControl.Domain.Events.TransactionCreatedEvent>(e =>
                    e.Amount == 100 &&
                    e.Description == "Teste"
                ),
                default
            ),
            Times.Once
        );

        Assert.NotNull(result);
    }
}