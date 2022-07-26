using CleanArchitectureWorkshop.Infrastructure.Bank.Configurations;
using CleanArchitectureWorkshop.Infrastructure.Bank.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureWorkshop.Infrastructure.Bank;

public class BankContext : DbContext
{
    public BankContext(DbContextOptions<BankContext> options)
        : base(options)
    {
    }

    public DbSet<Transaction> Transactions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new TransactionConfiguration());
    }
}