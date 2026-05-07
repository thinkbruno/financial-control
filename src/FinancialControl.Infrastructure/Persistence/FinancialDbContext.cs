using FinancialControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinancialControl.Infrastructure.Persistence;

public class FinancialDbContext : DbContext
{
    public DbSet<Transaction> Transactions => Set<Transaction>();

    public FinancialDbContext(DbContextOptions<FinancialDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(FinancialDbContext).Assembly
        );

        base.OnModelCreating(modelBuilder);
    }
}