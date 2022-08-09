namespace CleanArchitectureWorkshop.Application.Bank.Operations.Persistence;

public interface IOperationsRepository
{
    Task Deposit(double amount);
}