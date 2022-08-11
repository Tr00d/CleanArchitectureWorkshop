using AutoFixture;
using CleanArchitectureWorkshop.Domain.Bank.Common;
using CleanArchitectureWorkshop.Infrastructure.Bank.Entities;
using FluentAssertions;
using static CleanArchitectureWorkshop.Infrastructure.Bank.Entities.Transaction;

namespace CleanArchitectureWorkshop.Infrastructure.Tests.Bank.Entities;

public class TransactionTest
{
    private readonly Fixture fixture;

    public TransactionTest()
    {
        this.fixture = new Fixture();
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void ToOperation_ShouldReturnPositiveAmount_GivenTransactionIsDeposit()
    {
        var transaction = this.CreateTransaction(TransactionType.Deposit);
        var operation = transaction.ToOperation();
        operation.Date.Should().Be(transaction.ProcessedAt);
        operation.Amount.Should().Be(transaction.Amount);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void ToOperation_ShouldReturnNegativeAmount_GivenTransactionIsWithdrawal()
    {
        var transaction = this.CreateTransaction(TransactionType.Withdrawal);
        var operation = transaction.ToOperation();
        operation.Date.Should().Be(transaction.ProcessedAt);
        operation.Amount.Should().Be(-transaction.Amount);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void FromOperation_ShouldReturnDeposit_GivenOperationHasPositiveAmount()
    {
        var operation = new Operation(DateTime.Now, 10);
        var transaction = FromOperation(operation);
        transaction.Amount.Should().Be(operation.Amount);
        transaction.Type.Should().Be(TransactionType.Deposit);
        transaction.ProcessedAt.Should().Be(operation.Date);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void FromOperation_ShouldReturnWithdrawal_GivenOperationHasNegativeAmount()
    {
        var operation = new Operation(DateTime.Now, -10);
        var transaction = FromOperation(operation);
        transaction.Amount.Should().Be(Math.Abs(operation.Amount));
        transaction.Type.Should().Be(TransactionType.Withdrawal);
        transaction.ProcessedAt.Should().Be(operation.Date);
    }

    private Transaction CreateTransaction(TransactionType type) =>
        this.fixture.Build<Transaction>().With(transaction => transaction.Type, type).Create();
}