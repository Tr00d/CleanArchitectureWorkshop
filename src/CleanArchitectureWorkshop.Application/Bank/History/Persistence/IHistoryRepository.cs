using CleanArchitectureWorkshop.Domain.Bank.History;

namespace CleanArchitectureWorkshop.Application.Bank.History.Persistence;

public interface IHistoryRepository
{
    Task<AccountHistory> GetAccountHistoryAsync();
}