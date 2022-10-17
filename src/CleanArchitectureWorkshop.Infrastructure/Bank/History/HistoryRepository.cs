using CleanArchitectureWorkshop.Application.Bank.History.Persistence;
using CleanArchitectureWorkshop.Domain.Bank.Common;
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

    public async Task<AccountHistory> GetAccountHistoryAsync()
    {
        var operations = await this.GetAccountOperationsAsync();
        return new AccountHistory(operations);
    }

    private async Task<List<Operation>> GetAccountOperationsAsync() =>
        await this.context.Transactions
            .Select(transaction => transaction.ToOperation())
            .ToListAsync();
}