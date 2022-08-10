using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using CleanArchitectureWorkshop.Api.Bank.Operations;
using CleanArchitectureWorkshop.Application.Bank.Operations.Deposit;
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
    public async Task Deposit_ShouldReturnOperationIdentifier_GivenDepositSucceeds()
    {
        var request = this.fixture.Create<DepositRequest>();
        var command = this.fixture.Create<DepositCommand>();
        this.mockMapper.Setup(mapper => mapper.Map<DepositCommand>(request)).Returns(command);
        var result = await this.controller.DepositAsync(request);
        result.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be(command.Id);
    }
}