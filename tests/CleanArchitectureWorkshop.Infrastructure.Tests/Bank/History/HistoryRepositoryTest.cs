using CleanArchitectureWorkshop.Domain.Bank.Common;
using CleanArchitectureWorkshop.Infrastructure.Bank.Entities;
using CleanArchitectureWorkshop.Infrastructure.Bank.History;
using FluentAssertions;

namespace CleanArchitectureWorkshop.Infrastructure.Tests.Bank.History;

[Collection("Sequential")]
public class HistoryRepositoryTest : IDisposable
{
    private readonly BankDataBuilder dataBuilder;
    private readonly HistoryRepository repository;

    public HistoryRepositoryTest()
    {
        this.dataBuilder = BankDataBuilder.Build();
        this.repository = new HistoryRepository(this.dataBuilder.Context);
    }

    public void Dispose()
    {
        this.dataBuilder.Context.Database.EnsureDeleted();
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetAccountAsync_ShouldReturnAccountWithOperations()
    {
        await this.dataBuilder
            .WithEntity(Transaction.Deposit(new DateTime(2020, 01, 01), 15))
            .WithEntity(Transaction.Deposit(new DateTime(2020, 02, 01), 30))
            .WithEntity(Transaction.Deposit(new DateTime(2020, 03, 01), -15))
            .CommitAsync();
        var expectedOperations = new List<Operation>
        {
            Operation.FromValues(new DateTime(2020, 01, 01), 15),
            Operation.FromValues(new DateTime(2020, 02, 01), 30),
            Operation.FromValues(new DateTime(2020, 03, 01), -15),
        };
        var account = await this.repository.GetAccountHistoryAsync();
        account.GetOperations().Should().BeEquivalentTo(expectedOperations);
    }
}