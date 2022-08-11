using CleanArchitectureWorkshop.Domain.Bank.Common;

namespace CleanArchitectureWorkshop.Domain.Bank.Operations;

public class Account
{
    private readonly ICollection<Operation> operations;

    public Account()
    {
        this.Balance = default;
        this.LastDayWithdrawnAmount = default;
        this.operations = new List<Operation>();
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
        if (!IsAmountValid(amount))
        {
            return;
        }

        this.operations.Add(CreateDeposit(amount, date));
        this.IncreaseBalance(amount);
    }

    private static Operation CreateDeposit(double amount, DateTime date) => new(date, amount);

    private static bool IsAmountValid(double amount) => amount > 0;

    private void IncreaseBalance(double amount) => this.Balance += amount;

    public IEnumerable<Operation> GetOperations() => new List<Operation>(this.operations);

    public void Withdraw(double amount, DateTime date)
    {
        if (!IsAmountValid(amount))
        {
            return;
        }

        this.operations.Add(CreateWithdrawal(amount, date));
        this.DecreaseBalance(amount);
    }

    private static Operation CreateWithdrawal(double amount, DateTime date) => new(date, -amount);

    private void DecreaseBalance(double amount) => this.Balance -= amount;
}