namespace FinancialControl.Domain.Events;

public record TransactionCreatedEvent(
    Guid Id,
    decimal Amount,
    string Description,
    string Type
);