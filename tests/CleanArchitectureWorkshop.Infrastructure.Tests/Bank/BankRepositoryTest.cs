using CleanArchitectureWorkshop.Domain.Bank;
using CleanArchitectureWorkshop.Infrastructure.Bank;
using CleanArchitectureWorkshop.Infrastructure.Bank.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureWorkshop.Infrastructure.Tests.Bank;

public class BankRepositoryTest : IDisposable
{
    private const string ConnectionString = "Server=localhost; Database=Workshop; User Id=sa; Password=Password@123";
    private readonly BankContext context;
    private readonly BankRepository repository;

    public BankRepositoryTest()
    {
        this.context = CreateContext();
        this.repository = new BankRepository(this.context);
    }

    public void Dispose()
    {
        this.context.Database.EnsureDeleted();
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetAccount_ShouldReturnAccountWithOperations()
    {
        var transactions = new List<Transaction>
        {
            Transaction.Deposit(new DateTime(2020, 01, 01), 15),
            Transaction.Deposit(new DateTime(2020, 02, 01), 30),
            Transaction.Deposit(new DateTime(2020, 03, 01), -15),
        };
        var expectedOperations = new List<Operation>
        {
            new(new DateTime(2020, 01, 01), 15),
            new(new DateTime(2020, 02, 01), 30),
            new(new DateTime(2020, 03, 01), -15),
        };
        await this.context.AddRangeAsync(transactions);
        await this.context.SaveChangesAsync();
        var account = await this.repository.GetAccount();
        account.GetOperations().Should().BeEquivalentTo(expectedOperations);
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