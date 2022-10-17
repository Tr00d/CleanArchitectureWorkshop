using CleanArchitectureWorkshop.Infrastructure.Bank;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureWorkshop.Infrastructure.Tests.Bank;

public class BankDataBuilder
{
    private const string ConnectionString =
        "Server=localhost; Database={databaseName}; User Id=sa; Password=Password@123; TrustServerCertificate=True";

    private BankDataBuilder(BankContext context)
    {
        this.Context = context;
    }

    public BankContext Context { get; }

    public async Task CommitAsync() => await this.Context.SaveChangesAsync();

    public static BankDataBuilder Build(string databaseName) => new(CreateContext(databaseName));

    public BankDataBuilder WithEntity<T>(T entity)
        where T : class
    {
        this.Context.Add(entity);
        return this;
    }

    private static BankContext CreateContext(string databaseName)
    {
        var options = new DbContextOptionsBuilder<BankContext>()
            .UseSqlServer(BuildConnectionString(databaseName))
            .Options;
        var bankContext = new BankContext(options);
        bankContext.Database.EnsureCreated();
        return bankContext;
    }

    private static string BuildConnectionString(string databaseName) =>
        ConnectionString.Replace("{databaseName}", databaseName);
}