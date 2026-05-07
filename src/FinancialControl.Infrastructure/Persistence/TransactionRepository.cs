using FinancialControl.Domain.Entities;
using FinancialControl.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace FinancialControl.Infrastructure.Persistence;

public class TransactionRepository : ITransactionRepository
{
    private readonly FinancialDbContext _context;

    public TransactionRepository(FinancialDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Transaction transaction,
        CancellationToken cancellationToken = default)
    {
        await _context.Transactions.AddAsync(transaction, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Transaction>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Transactions
            .AsNoTracking()
            .OrderByDescending(x => x.Date)
            .ToListAsync(cancellationToken);
    }
}