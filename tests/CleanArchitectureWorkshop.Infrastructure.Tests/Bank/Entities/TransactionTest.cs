using CleanArchitectureWorkshop.Infrastructure.Bank.Entities;
using FluentAssertions;
using static CleanArchitectureWorkshop.Infrastructure.Bank.Entities.Transaction;

namespace CleanArchitectureWorkshop.Infrastructure.Tests.Bank.Entities;

public class TransactionTest
{
    [Fact]
    public void ToOperation_ShouldReturnPositiveAmount_GivenTransactionIsDeposit()
    {
        var transaction = CreateTransaction(TransactionType.Deposit);
        var operation = transaction.ToOperation();
        operation.Date.Should().Be(transaction.ProcessedAt);
        operation.Amount.Should().Be(transaction.Amount);
    }

    [Fact]
    public void ToOperation_ShouldReturnNegativeAmount_GivenTransactionIsWithdrawal()
    {
        var transaction = CreateTransaction(TransactionType.Withdrawal);
        var operation = transaction.ToOperation();
        operation.Date.Should().Be(transaction.ProcessedAt);
        operation.Amount.Should().Be(-transaction.Amount);
    }

    private static Transaction CreateTransaction(TransactionType type) =>
        new()
        {
            Id = Guid.NewGuid(),
            Amount = 50,
            Type = type,
            ProcessedAt = DateTime.Now,
        };
}