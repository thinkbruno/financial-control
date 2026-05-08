using FinancialControl.Application.Interfaces;
using FinancialControl.Domain.Entities;
using FinancialControl.Domain.Events;
using FinancialControl.Domain.Interfaces;

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

    public async Task<Transaction> ExecuteAsync(
        CreateTransactionInput input)
    {
        var transaction = Transaction.Create(
            input.Description,
            input.Amount,
            input.Date,
            input.Type,
            input.Category
        );

        await _repository.AddAsync(transaction);

        var transactionCreatedEvent = new TransactionCreatedEvent(
            transaction.Id,
            transaction.Amount,
            transaction.Description,
            transaction.Type,
            transaction.Category,
            transaction.Date
        );

        await _eventPublisher.PublishAsync(transactionCreatedEvent);

        return transaction;
    }
}