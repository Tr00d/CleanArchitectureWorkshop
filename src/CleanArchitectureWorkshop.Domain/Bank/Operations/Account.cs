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

    public void Deposit(Amount amount, DateTime date)
    {
        if (!IsAmountValid(amount))
        {
            return;
        }

        this.operations.Add(CreateDeposit(amount, date));
        this.IncreaseBalance(amount);
    }

    private static Operation CreateDeposit(Amount amount, DateTime date) => Operation.FromValues(date, amount.Value);

    private static bool IsAmountValid(Amount amount) => amount.Value > 0;

    private void IncreaseBalance(Amount amount) => this.Balance += amount.Value;

    public IEnumerable<Operation> GetOperations() => new List<Operation>(this.operations);

    public void Withdraw(Amount amount, DateTime date)
    {
        if (!IsAmountValid(amount))
        {
            return;
        }

        this.operations.Add(CreateWithdrawal(amount, date));
        this.DecreaseBalance(amount);
    }

    private static Operation CreateWithdrawal(Amount amount, DateTime date) =>
        Operation.FromValues(date, -amount.Value);

    private void DecreaseBalance(Amount amount) => this.Balance -= amount.Value;
}