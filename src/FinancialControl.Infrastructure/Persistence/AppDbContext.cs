using FinancialControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinancialControl.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Transaction> Transactions => Set<Transaction>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(x => x.Amount)
                .IsRequired();

            entity.Property(x => x.Date)
                .IsRequired();

            entity.Property(x => x.Type)
                .IsRequired();

            entity.Property(x => x.Category)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(x => x.CreatedAt)
                .IsRequired();
        });
    }
}