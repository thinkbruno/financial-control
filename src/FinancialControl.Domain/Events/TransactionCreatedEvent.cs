namespace FinancialControl.Domain.Events;

using FinancialControl.Domain.Entities;

public record TransactionCreatedEvent(
    Guid TransactionId,
    decimal Amount,
    string Description,
    TransactionType Type,
    string Category,
    DateTime OccurredAt
);