using CleanArchitectureWorkshop.Application.Bank.Operations.Persistence;
using MediatR;

namespace CleanArchitectureWorkshop.Application.Bank.Operations.Deposit;

public class DepositHandler : IRequestHandler<DepositCommand>
{
    private readonly IOperationsRepository _repository;

    public DepositHandler(IOperationsRepository inOperationsRepository)
    {
        this._repository = inOperationsRepository;
    }

    public async Task<Unit> Handle(DepositCommand request, CancellationToken cancellationToken)
    {
        await this._repository.Deposit(request.Amount);
        return Unit.Value;
    }
}