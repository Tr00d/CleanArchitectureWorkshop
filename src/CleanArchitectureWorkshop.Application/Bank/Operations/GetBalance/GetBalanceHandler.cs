using CleanArchitectureWorkshop.Application.Bank.Operations.Persistence;
using MediatR;

namespace CleanArchitectureWorkshop.Application.Bank.Operations.GetBalance;

public class GetBalanceHandler : IRequestHandler<GetBalanceQuery, double>
{
    private readonly IOperationsRepository _repository;

    public GetBalanceHandler(IOperationsRepository inRepository)
    {
        this._repository = inRepository;
    }

    public async Task<double> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
    {
        var account = await this._repository.GetAccountAsync();
        return account.Balance;
    }
}