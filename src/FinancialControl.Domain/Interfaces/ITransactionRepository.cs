using FinancialControl.Domain.Entities;

namespace FinancialControl.Domain.Interfaces;

public interface ITransactionRepository
{
    Task AddAsync(Transaction transaction);
    Task<IEnumerable<Transaction>> GetAllAsync();
}