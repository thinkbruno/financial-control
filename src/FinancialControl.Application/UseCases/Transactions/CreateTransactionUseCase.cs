using FinancialControl.Domain.Entities;
using FinancialControl.Domain.Interfaces;
using FinancialControl.Domain.Events;
using MassTransit;

namespace FinancialControl.Application.UseCases.Transactions;

public class CreateTransactionUseCase
{
    private readonly ITransactionRepository _repository;
    private readonly IPublishEndpoint _publishEndpoint;

    public CreateTransactionUseCase(
        ITransactionRepository repository,
        IPublishEndpoint publishEndpoint)
    {
        _repository = repository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Transaction> Execute(CreateTransactionInput input)
    {
        if (input == null)
            throw new ArgumentNullException(nameof(input));

        var transaction = CreateTransaction(input);

        await Persist(transaction);
        await PublishEvent(transaction);

        return transaction;
    }

    private static Transaction CreateTransaction(CreateTransactionInput input)
    {
        var transaction = Transaction.Create(
            input.Description,
            input.Amount,
            input.Date,
            input.Type,
            input.Category
        );
        return transaction;
    }
    private async Task Persist(Transaction transaction)
    {
        await _repository.AddAsync(transaction);
    }

    private async Task PublishEvent(Transaction transaction)
    {
        var @event = new TransactionCreatedEvent(
            transaction.Id,
            transaction.Amount,
            transaction.Description,
            transaction.Type.ToString()
        );

        await _publishEndpoint.Publish(@event);
    }
}