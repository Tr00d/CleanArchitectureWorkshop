using CleanArchitectureWorkshop.Application.Bank.Operations.GetBalance;
using CleanArchitectureWorkshop.Application.Bank.Operations.Persistence;
using CleanArchitectureWorkshop.Domain.Bank.Operations;
using FluentAssertions;
using Moq;

namespace CleanArchitectureWorkshop.Application.Tests.Bank.Operations.GetBalance;

public class GetBalanceHandlerTest
{
    [Fact]
    public async Task Handle_Should_Return_Balance()
    {
        var account = new Account(50, 1000000000);
        var mockRepository = new Mock<IOperationsRepository>();
        mockRepository.Setup(setup => setup.GetAccountAsync()).ReturnsAsync(account);
        var handler = new GetBalanceHandler(mockRepository.Object);
        var balance = await handler.Handle(new GetBalanceQuery(), CancellationToken.None);
        balance.Should().Be(account.Balance);
    }
}