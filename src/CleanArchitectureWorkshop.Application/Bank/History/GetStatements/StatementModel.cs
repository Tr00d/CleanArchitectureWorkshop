using CleanArchitectureWorkshop.Domain.Bank.Common;

namespace CleanArchitectureWorkshop.Application.Bank.History.GetStatements;

public record StatementModel(DateTime Date, double Amount, double Balance)
{
    public static StatementModel FromOperation(Operation operation) => new(operation.Date, operation.Amount, default);
}