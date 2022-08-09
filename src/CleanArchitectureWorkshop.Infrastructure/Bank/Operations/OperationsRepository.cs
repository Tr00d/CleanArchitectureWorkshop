using CleanArchitectureWorkshop.Application.Bank.Operations.Persistence;
using CleanArchitectureWorkshop.Domain.Bank.Operations;
using CleanArchitectureWorkshop.Infrastructure.Bank.Entities;

namespace CleanArchitectureWorkshop.Infrastructure.Bank.Operations;

public class OperationsRepository : IOperationsRepository
{
    private readonly BankContext _context;

    public OperationsRepository(BankContext inContext)
    {
        _context = inContext;
    }

    public async Task Deposit(double inAmount)
    {
        var theTransaction = Transaction.Deposit(DateTime.Now, inAmount);
        await _context.Transactions.AddAsync(theTransaction);
        await _context.SaveChangesAsync();
    }

    public Task<Account> GetAccount()
    {
        throw new NotImplementedException();
    }

    public Task SaveOperations(IEnumerable<Operation> inOperations)
    {
        throw new NotImplementedException();
    }
}