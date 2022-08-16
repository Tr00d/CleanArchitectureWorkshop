using CleanArchitectureWorkshop.Domain.Bank.Common;

namespace CleanArchitectureWorkshop.Domain.Bank.Operations;

public class Account
{
    public const int WithdrawnAmountThreshold = 2500;
    private readonly ICollection<Operation> operations;

    private Account()
    {
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
        ValidateAmount(amount);
        this.operations.Add(CreateDeposit(amount, date));
        this.IncreaseBalance(amount);
    }

    private static Operation CreateDeposit(Amount amount, DateTime date) => Operation.FromValues(date, amount.Value);

    private static bool IsAmountValid(Amount amount) => amount.Value > 0;

    private static void ValidateAmount(Amount amount)
    {
        if (!IsAmountValid(amount))
        {
            throw new InvalidAmountException(amount);
        }
    }

    private void IncreaseBalance(Amount amount) => this.Balance += amount.Value;

    public IEnumerable<Operation> GetOperations() => new List<Operation>(this.operations);

    public void Withdraw(Amount amount, DateTime date)
    {
        ValidateAmount(amount);
        this.ValidateBalance(amount);
        this.ValidateThreshold(amount);
        this.operations.Add(CreateWithdrawal(amount, date));
        this.DecreaseBalance(amount);
    }

    private static Operation CreateWithdrawal(Amount amount, DateTime date) =>
        Operation.FromValues(date, -amount.Value);

    private void DecreaseBalance(Amount amount) => this.Balance -= amount.Value;

    private bool IsAmountUnderWithdrawnThreshold(Amount amount) =>
        this.LastDayWithdrawnAmount + amount.Value <= WithdrawnAmountThreshold;

    private void ValidateThreshold(Amount amount)
    {
        if (!this.IsAmountUnderWithdrawnThreshold(amount))
        {
            throw new ExceededWithdrawnThresholdException(this.LastDayWithdrawnAmount,
                amount.Value);
        }
    }

    private bool IsBalanceHigherThanAmount(Amount amount) => this.Balance >= amount.Value;

    private void ValidateBalance(Amount amount)
    {
        if (!this.IsBalanceHigherThanAmount(amount))
        {
            throw new InsufficientProvisionException(this.Balance, amount);
        }
    }
}