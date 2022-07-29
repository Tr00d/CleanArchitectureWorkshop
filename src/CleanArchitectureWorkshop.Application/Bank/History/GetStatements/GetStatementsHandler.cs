using CleanArchitectureWorkshop.Application.Bank.History.Persistence;
using MediatR;

namespace CleanArchitectureWorkshop.Application.Bank.History.GetStatements;

public class GetStatementsHandler : IRequestHandler<GetStatementsQuery, GetStatementsResponse>
{
    private readonly IHistoryRepository repository;

    public GetStatementsHandler(IHistoryRepository repository)
    {
        this.repository = repository;
    }

    public async Task<GetStatementsResponse> Handle(GetStatementsQuery request, CancellationToken cancellationToken)
    {
        var account = await this.repository.GetAccountHistory();
        var operations = account.GetOperations();
        var statements = new Stack<StatementModel>();
        operations
            .OrderBy(operation => operation.Date)
            .Aggregate(0d, (runningBalance, operation) =>
            {
                runningBalance += operation.Amount;
                statements.Push(StatementModel.FromOperation(operation) with { Balance = runningBalance });
                return runningBalance;
            });
        return new GetStatementsResponse(statements);
    }
}