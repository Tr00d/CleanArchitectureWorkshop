using AutoFixture;
using AutoMapper;
using CleanArchitectureWorkshop.Application.Bank.Operations.Deposit;
using FluentAssertions;

namespace CleanArchitectureWorkshop.Application.Tests.Bank.Operations.Deposit;

public class DepositMapperProfileTest
{
    private readonly Fixture fixture;
    private readonly IMapper mapper;

    public DepositMapperProfileTest()
    {
        this.fixture = new Fixture();
        this.mapper = new Mapper(new MapperConfiguration(builder => builder.AddProfile<DepositMapperProfile>()));
    }

    [Fact]
    public void Map_ShouldConvertRequestIntoCommand()
    {
        var request = this.fixture.Create<DepositRequest>();
        var command = this.mapper.Map<DepositCommand>(request);
        command.Amount.Should().Be(request.Amount);
        command.Id.Should().NotBeEmpty();
    }
}