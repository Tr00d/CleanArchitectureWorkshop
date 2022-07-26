using CleanArchitectureWorkshop.Application.Bank.Persistence;
using CleanArchitectureWorkshop.Domain.Bank;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureWorkshop.Infrastructure.Bank;

public class BankRepository : IBankRepository
{
    private readonly BankContext context;

    public BankRepository(BankContext context)
    {
        this.context = context;
    }

    public async Task<Account> GetAccount()
    {
        var operations = await this.context.Transactions
            .Select(transaction => transaction.ToOperation())
            .ToListAsync();
        return new Account(operations);
    }
}