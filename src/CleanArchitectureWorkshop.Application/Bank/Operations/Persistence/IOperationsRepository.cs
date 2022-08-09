using CleanArchitectureWorkshop.Domain.Bank.Operations;

namespace CleanArchitectureWorkshop.Application.Bank.Operations.Persistence;

public interface IOperationsRepository
{
    Task Deposit(double inAmount);
    Task<Account> GetAccount();
    Task SaveOperations(IEnumerable<Operation> inOperations);
}