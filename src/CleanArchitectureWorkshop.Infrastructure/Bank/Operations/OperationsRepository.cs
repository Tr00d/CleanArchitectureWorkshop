using CleanArchitectureWorkshop.Application.Bank.Operations.Persistence;
using CleanArchitectureWorkshop.Application.Common;
using CleanArchitectureWorkshop.Domain.Bank.Common;
using CleanArchitectureWorkshop.Domain.Bank.Operations;
using CleanArchitectureWorkshop.Infrastructure.Bank.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureWorkshop.Infrastructure.Bank.Operations;

public class OperationsRepository : IOperationsRepository
{
    private readonly BankContext context;
    private readonly ITimeProvider timeProvider;

    public OperationsRepository(BankContext context, ITimeProvider timeProvider)
    {
        this.context = context;
        this.timeProvider = timeProvider;
    }

    public async Task<Account> GetAccountAsync()
    {
        var balance = await this.GetBalanceAsync();
        var time = this.timeProvider.UtcNow.AddDays(-1);
        var withdrawnAmount = await this.GetWithdrawnAmountAsync(time);
        return new Account(balance, withdrawnAmount);
    }

    public async Task SaveOperationsAsync(IEnumerable<Operation> inOperations)
    {
        await this.context.Transactions.AddRangeAsync(inOperations.Select(Transaction.FromOperation));
        await this.context.SaveChangesAsync();
    }

    private async Task<double> GetWithdrawnAmountAsync(DateTime time) =>
        await this.context
            .Transactions
            .AsNoTracking()
            .Where(transaction => transaction.ProcessedAt >= time &&
                                  transaction.Type == Transaction.TransactionType.Withdrawal)
            .SumAsync(transaction => transaction.Amount);

    private async Task<double> GetBalanceAsync() =>
        await this.context
            .Transactions
            .AsNoTracking()
            .Select(transaction => transaction.Type == Transaction.TransactionType.Deposit
                ? transaction.Amount
                : -transaction.Amount)
            .SumAsync();
}