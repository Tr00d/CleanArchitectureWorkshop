using CleanArchitectureWorkshop.Application.Bank.Operations.Deposit;
using FluentAssertions;

namespace CleanArchitectureWorkshop.Application.Tests.Bank.Operations.Deposit;

public class DepositRequestValidationTest
{
    private readonly DepositRequestValidation validation;

    public DepositRequestValidationTest()
    {
        this.validation = new DepositRequestValidation();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void Validate_ShouldFail_GivenAmountIsNegative()
    {
        var request = new DepositRequest(-10);
        var result = this.validation.Validate(request);
        result.IsValid.Should().BeFalse();
        result.Errors.Count(error => error.PropertyName == "Amount").Should().Be(1);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void Validate_ShouldSucceed_GivenAmountIsZero()
    {
        var request = new DepositRequest(0);
        var result = this.validation.Validate(request);
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void Validate_ShouldSucceed_GivenAmountIsPositive()
    {
        var request = new DepositRequest(10);
        var result = this.validation.Validate(request);
        result.IsValid.Should().BeTrue();
    }
}