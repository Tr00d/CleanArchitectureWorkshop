using FluentValidation;
using FluentValidation.Results;

namespace CleanArchitectureWorkshop.Application.Bank.Operations.Withdraw;

public class WithdrawRequestValidation : AbstractValidator<WithdrawRequest>
{
    public override ValidationResult Validate(ValidationContext<WithdrawRequest> context)
    {
        this.ValidateAmount();
        return base.Validate(context);
    }

    private void ValidateAmount() => this.RuleFor(request => request.Amount).Must(amount => amount >= 0);
}