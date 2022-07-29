using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using CleanArchitectureWorkshop.Api.Bank.History;
using CleanArchitectureWorkshop.Application.Bank.History.GetStatements;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CleanArchitectureWorkshop.Api.Tests.Bank.History;

public class HistoryControllerTest
{
    private readonly HistoryController controller;
    private readonly Fixture fixture;
    private readonly Mock<IMediator> mockMediator;

    public HistoryControllerTest()
    {
        this.fixture = new Fixture();
        this.mockMediator = new Mock<IMediator>();
        this.controller = new HistoryController(this.mockMediator.Object);
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