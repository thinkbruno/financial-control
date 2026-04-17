using FinancialControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinancialControl.Infrastructure.Context;

public class FinancialDbContext : DbContext
{
    public FinancialDbContext(DbContextOptions<FinancialDbContext> options) : base(options)
    {
    }

    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // precisão decimal para não haver erro de arredondamento financeiro
        modelBuilder.Entity<Transaction>()
            .Property(t => t.Amount)
            .HasPrecision(18, 2);

        base.OnModelCreating(modelBuilder);
    }
}