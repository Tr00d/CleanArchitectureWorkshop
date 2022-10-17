namespace CleanArchitectureWorkshop.Domain.Bank.Operations;

public class ExceededWithdrawnThresholdException : Exception
{
    public ExceededWithdrawnThresholdException(double currentWithdrawnAmount, double withdrawnAmount)
        : base(
            $"Current withdrawn amount is {currentWithdrawnAmount}. The limit of {Account.WithdrawnAmountThreshold} will be exceeded when withdrawing {withdrawnAmount}.")
    {
        this.CurrentWithdrawnAmount = currentWithdrawnAmount;
        this.WithdrawnAmount = withdrawnAmount;
    }

    public double CurrentWithdrawnAmount { get; }

    public double WithdrawnAmount { get; }
}