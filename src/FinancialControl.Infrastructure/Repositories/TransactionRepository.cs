using FinancialControl.Domain.Entities;
using FinancialControl.Domain.Interfaces;
using FinancialControl.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FinancialControl.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly FinancialDbContext _context;

    public TransactionRepository(FinancialDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Transaction transaction)
    {
        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Transaction>> GetAllAsync()
    {
        return await _context.Transactions.AsNoTracking().ToListAsync();
    }
}