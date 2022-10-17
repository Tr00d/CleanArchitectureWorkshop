using AutoFixture;
using AutoMapper;
using CleanArchitectureWorkshop.Application.Bank.Operations.Withdraw;
using CleanArchitectureWorkshop.Domain.Bank.Common;
using FluentAssertions;

namespace CleanArchitectureWorkshop.Application.Tests.Bank.Operations.Withdraw;

public class WithdrawMapperProfileTest
{
    private readonly Fixture fixture;
    private readonly IMapper mapper;

    public WithdrawMapperProfileTest()
    {
        this.fixture = new Fixture();
        this.mapper = new Mapper(new MapperConfiguration(builder => builder.AddProfile<WithdrawMapperProfile>()));
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void Map_ShouldConvertRequestIntoCommand()
    {
        var request = this.fixture.Create<WithdrawRequest>();
        var command = this.mapper.Map<WithdrawCommand>(request);
        command.Amount.Should().Be(Amount.FromValue(request.Amount));
        command.Id.Should().NotBeEmpty();
    }
}