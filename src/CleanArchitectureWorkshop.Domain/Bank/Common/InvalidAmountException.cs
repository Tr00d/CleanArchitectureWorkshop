namespace CleanArchitectureWorkshop.Domain.Bank.Common;

public class InvalidAmountException : Exception
{
    public InvalidAmountException(Amount amount)
        : base($"Invalid amount: {amount.Value}")
    {
        this.InvalidAmount = amount;
    }

    public Amount InvalidAmount { get; set; }
}