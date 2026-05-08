using FinancialControl.Domain.Entities;

namespace FinancialControl.Domain.Interfaces;

public interface ITransactionRepository
{
    Task<List<Transaction>> GetAllAsync();

    Task AddAsync(Transaction transaction);

    Task<Transaction?> GetByIdAsync(Guid id);

    Task UpdateAsync(Transaction transaction);

    Task DeleteAsync(Transaction transaction);
}