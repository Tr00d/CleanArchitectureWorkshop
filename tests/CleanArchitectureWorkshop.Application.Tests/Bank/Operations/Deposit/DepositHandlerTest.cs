using AutoFixture;
using CleanArchitectureWorkshop.Application.Bank.Operations.Deposit;
using CleanArchitectureWorkshop.Application.Bank.Operations.Persistence;
using CleanArchitectureWorkshop.Domain.Bank.Operations;
using FluentAssertions;
using Moq;

namespace CleanArchitectureWorkshop.Application.Tests.Bank.Operations.Deposit;

public class DepositHandlerTest
{
    private readonly Fixture _fixture;
    private readonly DepositHandler _handler;
    private readonly Mock<IOperationsRepository> _mockRepository;

    public DepositHandlerTest()
    {
        _fixture = new Fixture();
        _mockRepository = new Mock<IOperationsRepository>();
        _handler = new DepositHandler(_mockRepository.Object);
    }

    [Fact]
    public async Task Handle_ShouldDepositMoneyOnAccount()
    {
        var theCommand = this._fixture.Create<DepositCommand>();
        var theAccount = new Account();
        
        await this._handler.Handle(theCommand, CancellationToken.None);
        // this._mockRepository.Verify(repository => repository.SaveOperations(), Times.Once);

        theAccount.Balance.Should().Be(theCommand.Amount);
        theAccount.Operations.Should().BeEquivalentTo(new List<Operation> { new Operation(theCommand.Amount, DateTime.Now)});
    }
}