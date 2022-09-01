using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using CleanArchitectureWorkshop.Api.Bank.Operations;
using CleanArchitectureWorkshop.Application.Bank.Operations.Deposit;
using CleanArchitectureWorkshop.Application.Bank.Operations.GetBalance;
using CleanArchitectureWorkshop.Application.Bank.Operations.Withdraw;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CleanArchitectureWorkshop.Api.Tests.Bank.Operations;

public class OperationsControllerTest
{
    private readonly OperationsController controller;
    private readonly Fixture fixture;
    private readonly Mock<IMapper> mockMapper;
    private readonly Mock<IMediator> mockMediator;

    public OperationsControllerTest()
    {
        this.fixture = new Fixture();
        this.mockMapper = new Mock<IMapper>();
        this.mockMediator = new Mock<IMediator>();
        this.controller = new OperationsController(this.mockMediator.Object, this.mockMapper.Object);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Deposit_ShouldReturnOperationIdentifier_GivenDepositSucceeds()
    {
        var request = this.fixture.Create<DepositRequest>();
        var command = this.fixture.Create<DepositCommand>();
        this.mockMapper.Setup(mapper => mapper.Map<DepositCommand>(request)).Returns(command);
        var result = await this.controller.DepositAsync(request);
        result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be(command.Id);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Deposit_ShouldProcessDeposit()
    {
        var request = this.fixture.Create<DepositRequest>();
        var command = this.fixture.Create<DepositCommand>();
        this.mockMapper.Setup(mapper => mapper.Map<DepositCommand>(request)).Returns(command);
        await this.controller.DepositAsync(request);
        this.mockMediator.Verify(mediator => mediator.Send(command, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Withdraw_ShouldReturnOperationIdentifier_GivenWithdrawalSucceeds()
    {
        var request = this.fixture.Create<WithdrawRequest>();
        var command = this.fixture.Create<WithdrawCommand>();
        this.mockMapper.Setup(mapper => mapper.Map<WithdrawCommand>(request)).Returns(command);
        var result = await this.controller.WithdrawAsync(request);
        result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be(command.Id);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Withdraw_ShouldProcessWithdrawal()
    {
        var request = this.fixture.Create<WithdrawRequest>();
        var command = this.fixture.Create<WithdrawCommand>();
        this.mockMapper.Setup(mapper => mapper.Map<WithdrawCommand>(request)).Returns(command);
        await this.controller.WithdrawAsync(request);
        this.mockMediator.Verify(mediator => mediator.Send(command, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task GetBalance_ShouldReturnBalance()
    {
        this.mockMediator.Setup(mediator =>
            mediator.Send(It.IsAny<GetBalanceQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(50);
        var balance = await this.controller.GetBalance();
        balance.Value.Should().Be(50);
    }
}