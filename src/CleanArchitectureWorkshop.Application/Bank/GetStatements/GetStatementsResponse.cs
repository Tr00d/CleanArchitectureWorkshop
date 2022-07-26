namespace CleanArchitectureWorkshop.Application.Bank.GetStatements;

public record GetStatementsResponse(IEnumerable<StatementModel> History);