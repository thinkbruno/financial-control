using FinancialControl.Domain.Entities;

namespace FinancialControl.Domain.Interfaces;

public interface ITransactionRepository
{
    Task AddAsync(Transaction transaction, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Transaction>> GetAllAsync(CancellationToken cancellationToken = default);
}