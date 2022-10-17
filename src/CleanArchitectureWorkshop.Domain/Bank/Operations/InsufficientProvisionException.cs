using CleanArchitectureWorkshop.Domain.Bank.Common;

namespace CleanArchitectureWorkshop.Domain.Bank.Operations;

public class InsufficientProvisionException : Exception
{
    public InsufficientProvisionException(double balance, Amount withdrawnAmount)
        : base($"Withdrawn amount is {withdrawnAmount.Value} while balance is {balance}.")
    {
        this.Balance = balance;
        this.WithdrawnAmount = withdrawnAmount;
    }

    public double Balance { get; }

    public Amount WithdrawnAmount { get; }
}