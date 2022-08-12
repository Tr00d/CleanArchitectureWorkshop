using AutoFixture;
using CleanArchitectureWorkshop.Application.Bank.Operations.Persistence;
using CleanArchitectureWorkshop.Application.Bank.Operations.Withdraw;
using CleanArchitectureWorkshop.Application.Common;
using CleanArchitectureWorkshop.Domain.Bank.Common;
using CleanArchitectureWorkshop.Domain.Bank.Operations;
using FluentAssertions;
using Moq;

namespace CleanArchitectureWorkshop.Application.Tests.Bank.Operations.Withdraw;

public class WithdrawHandlerTest
{
    private readonly Fixture fixture;
    private readonly WithdrawHandler handler;
    private readonly Mock<IOperationsRepository> mockRepository;
    private readonly Mock<ITimeProvider> mockTimeProvider;

    public WithdrawHandlerTest()
    {
        this.fixture = new Fixture();
        this.mockRepository = new Mock<IOperationsRepository>();
        this.mockTimeProvider = new Mock<ITimeProvider>();
        this.handler = new WithdrawHandler(this.mockRepository.Object, this.mockTimeProvider.Object);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Handle_ShouldUpdateAccountBalance_GivenWithdrawalSucceeds()
    {
        var command = this.fixture.Build<WithdrawCommand>().With(command => command.Amount, Amount.FromValue(100))
            .Create();
        var account = new Account(500, 0);
        var expectedBalance = 400;
        var time = this.fixture.Create<DateTime>();
        this.mockRepository.Setup(repository => repository.GetAccountAsync()).ReturnsAsync(account);
        this.mockTimeProvider.Setup(timeProvider => timeProvider.UtcNow).Returns(time);
        await this.handler.Handle(command, CancellationToken.None);
        account.Balance.Should().Be(expectedBalance);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Handle_ShouldAddWithdrawalOperation_GivenDepositSucceeds()
    {
        var command = this.fixture.Build<WithdrawCommand>().With(command => command.Amount, Amount.FromValue(100))
            .Create();
        var account = new Account(500, 0);
        var time = this.fixture.Create<DateTime>();
        var expectedOperations = new List<Operation> { Operation.FromValues(time, -command.Amount.Value) };
        this.mockRepository.Setup(repository => repository.GetAccountAsync()).ReturnsAsync(account);
        this.mockTimeProvider.Setup(timeProvider => timeProvider.UtcNow).Returns(time);
        await this.handler.Handle(command, CancellationToken.None);
        account.GetOperations().Should().BeEquivalentTo(expectedOperations);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Handle_ShouldUpdateOperations()
    {
        var command = this.fixture.Build<WithdrawCommand>().With(command => command.Amount, Amount.FromValue(100))
            .Create();
        var account = new Account(500, 0);
        var time = this.fixture.Create<DateTime>();
        this.mockRepository.Setup(repository => repository.GetAccountAsync()).ReturnsAsync(account);
        this.mockTimeProvider.Setup(timeProvider => timeProvider.UtcNow).Returns(time);
        await this.handler.Handle(command, CancellationToken.None);
        this.mockRepository.Verify(repository => repository.SaveOperationsAsync(account.GetOperations()), Times.Once);
    }

    [Theory]
    [InlineData(-500)]
    [InlineData(0)]
    [Trait("Category", "Unit")]
    public async Task Handle_ShouldNotUpdateAccountBalance_GivenAmountIsNotPositive(int amount)
    {
        var command = this.fixture.Build<WithdrawCommand>().With(command => command.Amount, Amount.FromValue(amount))
            .Create();
        var account = new Account(500, 0);
        var initialBalance = account.Balance;
        var time = this.fixture.Create<DateTime>();
        this.mockRepository.Setup(repository => repository.GetAccountAsync()).ReturnsAsync(account);
        this.mockTimeProvider.Setup(timeProvider => timeProvider.UtcNow).Returns(time);
        await this.handler.Handle(command, CancellationToken.None);
        account.Balance.Should().Be(initialBalance);
    }

    [Theory]
    [InlineData(-500)]
    [InlineData(0)]
    [Trait("Category", "Unit")]
    public async Task Handle_ShouldNotAddOperation_GivenAmountIsNotPositive(int amount)
    {
        var command = this.fixture.Build<WithdrawCommand>().With(command => command.Amount, Amount.FromValue(amount))
            .Create();
        var account = new Account(500, 0);
        var time = this.fixture.Create<DateTime>();
        this.mockRepository.Setup(repository => repository.GetAccountAsync()).ReturnsAsync(account);
        this.mockTimeProvider.Setup(timeProvider => timeProvider.UtcNow).Returns(time);
        await this.handler.Handle(command, CancellationToken.None);
        account.GetOperations().Should().BeEmpty();
    }
}