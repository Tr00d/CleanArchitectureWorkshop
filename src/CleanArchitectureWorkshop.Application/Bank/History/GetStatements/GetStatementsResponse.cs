namespace CleanArchitectureWorkshop.Application.Bank.History.GetStatements;

public record GetStatementsResponse(IEnumerable<StatementModel> History);