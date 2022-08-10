using AutoFixture;
using CleanArchitectureWorkshop.Application.Bank.Operations.Deposit;
using CleanArchitectureWorkshop.Application.Bank.Operations.Persistence;
using CleanArchitectureWorkshop.Application.Common;
using CleanArchitectureWorkshop.Domain.Bank.Common;
using CleanArchitectureWorkshop.Domain.Bank.Operations;
using FluentAssertions;
using Moq;

namespace CleanArchitectureWorkshop.Application.Tests.Bank.Operations.Deposit;

public class DepositHandlerTest
{
    private readonly Fixture fixture;
    private readonly DepositHandler handler;
    private readonly Mock<IOperationsRepository> mockRepository;
    private readonly Mock<ITimeProvider> mockTimeProvider;

    public DepositHandlerTest()
    {
        this.fixture = new Fixture();
        this.mockRepository = new Mock<IOperationsRepository>();
        this.mockTimeProvider = new Mock<ITimeProvider>();
        this.handler = new DepositHandler(this.mockRepository.Object, this.mockTimeProvider.Object);
    }

    [Fact]
    public async Task Handle_ShouldUpdateAccountBalance_GivenDepositSucceeds()
    {
        var command = this.fixture.Create<DepositCommand>();
        var account = new Account();
        var time = this.fixture.Create<DateTime>();
        this.mockRepository.Setup(repository => repository.GetAccountAsync()).ReturnsAsync(account);
        this.mockTimeProvider.Setup(timeProvider => timeProvider.UtcNow).Returns(time);
        await this.handler.Handle(command, CancellationToken.None);
        account.Balance.Should().Be(command.Amount);
    }

    [Fact]
    public async Task Handle_ShouldAddDepositOperation_GivenDepositSucceeds()
    {
        var command = this.fixture.Create<DepositCommand>();
        var account = new Account();
        var time = this.fixture.Create<DateTime>();
        var expectedOperations = new List<Operation> { new(time, command.Amount) };
        this.mockRepository.Setup(repository => repository.GetAccountAsync()).ReturnsAsync(account);
        this.mockTimeProvider.Setup(timeProvider => timeProvider.UtcNow).Returns(time);
        await this.handler.Handle(command, CancellationToken.None);
        account.GetOperations().Should().BeEquivalentTo(expectedOperations);
    }

    [Fact]
    public async Task Handle_ShouldUpdateOperations_GivenDepositSucceeds()
    {
        var command = this.fixture.Create<DepositCommand>();
        var account = new Account();
        var time = this.fixture.Create<DateTime>();
        this.mockRepository.Setup(repository => repository.GetAccountAsync()).ReturnsAsync(account);
        this.mockTimeProvider.Setup(timeProvider => timeProvider.UtcNow).Returns(time);
        await this.handler.Handle(command, CancellationToken.None);
        this.mockRepository.Verify(repository => repository.SaveOperationsAsync(account.GetOperations()), Times.Once);
    }
}