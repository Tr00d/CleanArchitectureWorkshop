using CleanArchitectureWorkshop.Application.Bank.Operations.Withdraw;
using FluentAssertions;

namespace CleanArchitectureWorkshop.Application.Tests.Bank.Operations.Withdraw;

public class WithdrawRequestValidationTest
{
    private readonly WithdrawRequestValidation validation;

    public WithdrawRequestValidationTest()
    {
        this.validation = new WithdrawRequestValidation();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void Validate_ShouldFail_GivenAmountIsNegative()
    {
        var request = new WithdrawRequest(-10);
        var result = this.validation.Validate(request);
        result.IsValid.Should().BeFalse();
        result.Errors.Count(error => error.PropertyName == "Amount").Should().Be(1);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void Validate_ShouldSucceed_GivenAmountIsZero()
    {
        var request = new WithdrawRequest(0);
        var result = this.validation.Validate(request);
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void Validate_ShouldSucceed_GivenAmountIsPositive()
    {
        var request = new WithdrawRequest(10);
        var result = this.validation.Validate(request);
        result.IsValid.Should().BeTrue();
    }
}