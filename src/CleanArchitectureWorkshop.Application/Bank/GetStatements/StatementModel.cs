using CleanArchitectureWorkshop.Domain.Bank;

namespace CleanArchitectureWorkshop.Application.Bank.GetStatements;

public record StatementModel(DateTime Date, double Amount, double Balance)
{
    public static StatementModel FromOperation(Operation operation) => new(operation.Date, operation.Amount, default);
}