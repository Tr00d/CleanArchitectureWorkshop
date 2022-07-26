using CleanArchitectureWorkshop.Domain.Bank;

namespace CleanArchitectureWorkshop.Application.Bank.Persistence;

public interface IBankRepository
{
    Task<Account> GetAccount();
}