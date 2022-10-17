using CleanArchitectureWorkshop.Domain.Bank.Common;
using CleanArchitectureWorkshop.Domain.Bank.Operations;

namespace CleanArchitectureWorkshop.Application.Bank.Operations.Persistence;

public interface IOperationsRepository
{
    Task<Account> GetAccountAsync();
    Task SaveOperationsAsync(IEnumerable<Operation> inOperations);
}