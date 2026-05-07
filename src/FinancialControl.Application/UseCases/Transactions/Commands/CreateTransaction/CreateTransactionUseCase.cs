using System;
using System.Threading;
using System.Threading.Tasks;
using FinancialControl.Domain.Entities;
using FinancialControl.Domain.Interfaces;
using FinancialControl.Domain.Events;
using FinancialControl.Application.Interfaces;

namespace FinancialControl.Application.UseCases.Transactions.Commands.CreateTransaction;

public class CreateTransactionUseCase
{
    private readonly ITransactionRepository _repository;
    private readonly IEventPublisher _eventPublisher;

    public CreateTransactionUseCase(
        ITransactionRepository repository,
        IEventPublisher eventPublisher)
    {
        _repository = repository;
        _eventPublisher = eventPublisher;
    }

    public async Task<Transaction> Execute(
        CreateTransactionInput input,
        CancellationToken cancellationToken = default)
    {
        if (input is null)
            throw new ArgumentNullException(nameof(input));

        var transaction = Transaction.Create(
            input.Description,
            input.Amount,
            input.Date,
            input.Type,
            input.Category
        );

        await _repository.AddAsync(transaction, cancellationToken);

        var @event = new TransactionCreatedEvent(
            transaction.Id,
            transaction.Amount,
            transaction.Description,
            transaction.Type,
            transaction.Category,
            DateTime.UtcNow
        );

        await _eventPublisher.PublishAsync(@event, cancellationToken);

        return transaction;
    }
}