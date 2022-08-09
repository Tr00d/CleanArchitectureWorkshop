using AutoFixture;
using CleanArchitectureWorkshop.Infrastructure.Bank;
using CleanArchitectureWorkshop.Infrastructure.Bank.Entities;
using CleanArchitectureWorkshop.Infrastructure.Bank.Operations;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureWorkshop.Infrastructure.Tests.Bank.Operations;

public class OperationsRepositoryTest : IDisposable
{
    private const string ConnectionString = "Server=localhost;Database=CAW;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";
    private readonly BankContext _context;
    private readonly OperationsRepository _repository;
    private readonly Fixture _fixture;
    
    public OperationsRepositoryTest()
    {
        _context = CreateContext();
        _repository = new OperationsRepository(_context);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task Deposit_ShouldAddTransaction()
    {
    }

    private static BankContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<BankContext>()
            .UseSqlServer(ConnectionString)
            .Options;
        var bankContext = new BankContext(options);
        bankContext.Database.EnsureCreated();
        return bankContext;
    }
}