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
        var theAccount = await _repository.GetAccount();
        theAccount.Deposit(request.Amount, DateTime.Now);
        await _repository.SaveOperations(theAccount.Operations);
        
        return Unit.Value;
    }
}