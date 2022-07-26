using AutoFixture;
using CleanArchitectureWorkshop.Api.Bank;
using CleanArchitectureWorkshop.Application.Bank.GetStatements;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CleanArchitectureWorkshop.Api.Tests.Bank;

public class AccountControllerTest
{
    private readonly AccountController controller;
    private readonly Fixture fixture;
    private readonly Mock<IMediator> mockMediator;

    public AccountControllerTest()
    {
        this.fixture = new Fixture();
        this.mockMediator = new Mock<IMediator>();
        this.controller = new AccountController(this.mockMediator.Object);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task GetStatementsAsync_ShouldReturnOkResponse()
    {
        var response = this.fixture.Create<GetStatementsResponse>();
        this.mockMediator
            .Setup(mediator => mediator.Send(It.IsAny<GetStatementsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);
        var result = await this.controller.GetStatementsAsync();
        result.Should().BeOfType<OkObjectResult>();
        var okResult = (result as OkObjectResult)!;
        okResult.Value.Should().Be(response);
    }
}