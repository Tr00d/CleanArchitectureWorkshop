using CleanArchitectureWorkshop.Infrastructure.Bank;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureWorkshop.Infrastructure.Tests.Bank;

public class BankDataBuilder
{
    private const string ConnectionString =
        "Server=localhost; Database=Workshop; User Id=sa; Password=Password@123; TrustServerCertificate=True";

    private BankDataBuilder(BankContext context)
    {
        this.Context = context;
    }

    public BankContext Context { get; }

    public async Task CommitAsync() => await this.Context.SaveChangesAsync();

    public static BankDataBuilder Build() => new(CreateContext());

    public BankDataBuilder WithEntity<T>(T entity)
        where T : class
    {
        this.Context.Add(entity);
        return this;
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