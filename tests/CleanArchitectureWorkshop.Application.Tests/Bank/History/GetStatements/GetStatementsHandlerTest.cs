using AutoFixture;
using CleanArchitectureWorkshop.Application.Bank.History.GetStatements;
using CleanArchitectureWorkshop.Application.Bank.History.Persistence;
using CleanArchitectureWorkshop.Domain.Bank.Common;
using FluentAssertions;
using Moq;

namespace CleanArchitectureWorkshop.Application.Tests.Bank.History.GetStatements;

public class GetStatementsHandlerTest
{
    private readonly Fixture fixture;
    private readonly GetStatementsHandler handler;
    private readonly Mock<IHistoryRepository> mockRepository;

    public GetStatementsHandlerTest()
    {
        this.fixture = new Fixture();
        this.mockRepository = new Mock<IHistoryRepository>();
        this.handler = new GetStatementsHandler(this.mockRepository.Object);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Handle_ShouldReturnEmptyList_GivenAccountContainsNoStatements()
    {
        this.mockRepository.Setup(repository => repository.GetAccountHistoryAsync())
            .ReturnsAsync(AccountBuilder.Build().Create);
        (await this.handler.Handle(this.fixture.Create<GetStatementsQuery>(), CancellationToken.None))
            .History
            .Should()
            .BeEmpty();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public async Task Handle_ShouldReturnStatementsFromNewestToOldest_GivenAccountContainsStatements()
    {
        var statements = new List<Operation>
        {
            Operation.FromValues(new DateTime(2021, 01, 15), 2000),
            Operation.FromValues(new DateTime(2021, 01, 20), -500),
            Operation.FromValues(new DateTime(2021, 01, 10), 1000),
        };
        var account = AccountBuilder.Build().WithStatements(statements).Create();
        var expectedStatements = new List<StatementModel>
        {
            new(new DateTime(2021, 01, 20), -500, 2500),
            new(new DateTime(2021, 01, 15), 2000, 3000),
            new(new DateTime(2021, 01, 10), 1000, 1000),
        };
        this.mockRepository.Setup(repository => repository.GetAccountHistoryAsync()).ReturnsAsync(account);
        var result = await this.handler.Handle(this.fixture.Create<GetStatementsQuery>(), CancellationToken.None);
        result.History.Should().BeEquivalentTo(expectedStatements, options => options.WithStrictOrdering());
    }
}