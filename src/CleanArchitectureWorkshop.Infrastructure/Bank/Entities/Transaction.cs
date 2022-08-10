using CleanArchitectureWorkshop.Domain.Bank.Common;

namespace CleanArchitectureWorkshop.Infrastructure.Bank.Entities;

public class Transaction
{
    public enum TransactionType
    {
        Deposit = 0,
        Withdrawal = 1,
    }

    public Transaction()
    {
        this.Id = Guid.NewGuid();
    }

    private Transaction(DateTime processedAt, double amount, TransactionType type)
        : this()
    {
        this.ProcessedAt = processedAt;
        this.Amount = amount;
        this.Type = type;
    }

    public Guid Id { get; set; }

    public DateTime ProcessedAt { get; set; }

    public double Amount { get; set; }

    public TransactionType Type { get; set; }

    public static Transaction Deposit(DateTime processedAt, double amount) =>
        new(processedAt, amount, TransactionType.Deposit);

    public static Transaction Withdrawal(DateTime processedAt, double amount) =>
        new(processedAt, amount, TransactionType.Withdrawal);

    public Operation ToOperation() => new(this.ProcessedAt, this.CalculateAmount());

    public static Transaction FromOperation(Operation operation) =>
        new(operation.Date, Math.Abs(operation.Amount), GetType(operation.Amount));

    private static TransactionType GetType(double amount) =>
        amount >= 0 ? TransactionType.Deposit : TransactionType.Withdrawal;

    private double CalculateAmount() =>
        this.Type switch
        {
            TransactionType.Deposit => this.Amount,
            TransactionType.Withdrawal => -this.Amount,
            _ => default,
        };
}