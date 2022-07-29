using CleanArchitectureWorkshop.Application.Bank.History.Persistence;
using CleanArchitectureWorkshop.Domain.Bank.History;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureWorkshop.Infrastructure.Bank.History;

public class HistoryRepository : IHistoryRepository
{
    private readonly BankContext context;

    public HistoryRepository(BankContext context)
    {
        this.context = context;
    }

    public async Task<AccountHistory> GetAccountHistory()
    {
        var operations = await this.context.Transactions
            .Select(transaction => transaction.ToOperation())
            .ToListAsync();
        return new AccountHistory(operations);
    }
}