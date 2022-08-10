using CleanArchitectureWorkshop.Domain.Bank.Common;

namespace CleanArchitectureWorkshop.Domain.Bank.Operations;

public class Account
{
    private readonly ICollection<Operation> Operations;

    public Account()
    {
        this.Balance = default;
        this.LastDayWithdrawnAmount = default;
        this.Operations = new List<Operation>();
    }

    public Account(double balance, double lastDayWithdrawnAmount)
        : this()
    {
        this.Balance = balance;
        this.LastDayWithdrawnAmount = lastDayWithdrawnAmount;
    }

    public double Balance { get; private set; }

    public double LastDayWithdrawnAmount { get; }

    public void Deposit(double amount, DateTime date)
    {
        this.Operations.Add(new Operation(date, amount));
        this.IncrementBalance(amount);
    }

    private void IncrementBalance(double inAmount) => this.Balance += inAmount;

    public IEnumerable<Operation> GetOperations() => new List<Operation>(this.Operations);
}