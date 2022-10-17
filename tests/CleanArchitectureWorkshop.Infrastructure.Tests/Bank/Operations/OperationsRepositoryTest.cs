using AutoFixture;
using CleanArchitectureWorkshop.Application.Common;
using CleanArchitectureWorkshop.Domain.Bank.Common;
using CleanArchitectureWorkshop.Infrastructure.Bank.Entities;
using CleanArchitectureWorkshop.Infrastructure.Bank.Operations;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.FeatureManagement;
using Moq;

namespace CleanArchitectureWorkshop.Infrastructure.Tests.Bank.Operations;

[Collection("Sequential")]
public class OperationsRepositoryTest : IDisposable
{
    private readonly BankDataBuilder dataBuilder;
    private readonly Fixture fixture;
    private readonly Mock<ITimeProvider> mockTimeProvider;
    private readonly Mock<IFeatureManager> mockFeatureManager;
    private readonly OperationsRepository repository;

    public OperationsRepositoryTest()
    {
        this.fixture = new Fixture();
        this.dataBuilder = BankDataBuilder.Build(nameof(OperationsRepositoryTest));
        this.mockTimeProvider = new Mock<ITimeProvider>();
        this.mockFeatureManager = new Mock<IFeatureManager>();
        this.repository = new OperationsRepository(this.dataBuilder.Context, this.mockTimeProvider.Object, this.mockFeatureManager.Object);
    }

    public void Dispose()
    {
        this.dataBuilder.Context.Database.EnsureDeleted();
        GC.SuppressFinalize(this);
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task SaveOperationsAsync_ShouldInsertOperationsToAccount()
    {
        var operations = this.fixture.CreateMany<Operation>().ToList();
        await this.repository.SaveOperationsAsync(operations);
        var transactions = await this.dataBuilder.Context.Transactions.AsNoTracking().ToListAsync();
        transactions.Select(transaction => transaction.ToOperation()).Should().BeEquivalentTo(operations);
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetAccountAsync_ShouldReturnAccountWithBalance()
    {
        await this.dataBuilder
            .WithEntity(Transaction.Deposit(this.fixture.Create<DateTime>(), 500))
            .WithEntity(Transaction.Deposit(this.fixture.Create<DateTime>(), 100))
            .WithEntity(Transaction.Withdrawal(this.fixture.Create<DateTime>(), 200))
            .CommitAsync();
        var expectedBalance = 400;
        this.mockTimeProvider.Setup(provider => provider.UtcNow).Returns(this.fixture.Create<DateTime>());
        var account = await this.repository.GetAccountAsync();
        account.Balance.Should().Be(expectedBalance);
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetAccountAsync_ShouldReturnAccountWithWithdrawnAmount()
    {
        var time = new DateTime(2022, 10, 10, 5, 0, 0);
        await this.dataBuilder
            .WithEntity(Transaction.Withdrawal(time.AddDays(-1), 500))
            .WithEntity(Transaction.Withdrawal(time.AddHours(-2), 100))
            .WithEntity(Transaction.Withdrawal(time.AddHours(-4), 200))
            .WithEntity(Transaction.Withdrawal(time.AddDays(-2), 400))
            .WithEntity(Transaction.Deposit(time, 500))
            .WithEntity(Transaction.Deposit(time.AddHours(2), 100))
            .WithEntity(Transaction.Deposit(time.AddHours(4), 200))
            .WithEntity(Transaction.Deposit(time.AddHours(-4), 400))
            .CommitAsync();
        var expectedAmount = 800;
        this.mockTimeProvider.Setup(provider => provider.UtcNow).Returns(time);
        var account = await this.repository.GetAccountAsync();
        account.LastDayWithdrawnAmount.Should().Be(expectedAmount);
    }

    [Theory]
    [Trait("Category", "Integration")]
    [InlineData(true, true)]
    [InlineData(false, false)]
    public async Task GetAccountAsyncWithdrawnAmount_ShouldReturnAccountWithWithdrawLimitEnabled_WhenFeatureIsEnabled(bool inFeatureValue, bool inExpectedResult)
    {
        this.mockTimeProvider.Setup(provider => provider.UtcNow).Returns(this.fixture.Create<DateTime>());
        this.mockFeatureManager.Setup(manager => manager.IsEnabledAsync("WithdrawThreshold")).ReturnsAsync(inFeatureValue);
        var account = await this.repository.GetAccountAsync();
        account.IsWithdrawLimited.Should().Be(inExpectedResult);
    }
}