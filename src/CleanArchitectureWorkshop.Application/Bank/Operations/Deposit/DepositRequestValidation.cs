using FluentValidation;
using FluentValidation.Results;

namespace CleanArchitectureWorkshop.Application.Bank.Operations.Deposit;

public class DepositRequestValidation : AbstractValidator<DepositRequest>
{
    public override ValidationResult Validate(ValidationContext<DepositRequest> context)
    {
        this.ValidateAmount();
        return base.Validate(context);
    }

    private void ValidateAmount() => this.RuleFor(request => request.Amount).Must(amount => amount >= 0);
}