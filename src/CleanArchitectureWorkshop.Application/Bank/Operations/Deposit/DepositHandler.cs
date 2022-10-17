using CleanArchitectureWorkshop.Application.Bank.Operations.Persistence;
using CleanArchitectureWorkshop.Application.Common;
using MediatR;

namespace CleanArchitectureWorkshop.Application.Bank.Operations.Deposit;

public class DepositHandler : IRequestHandler<DepositCommand>
{
    private readonly IOperationsRepository repository;
    private readonly ITimeProvider timeProvider;

    public DepositHandler(IOperationsRepository inOperationsRepository, ITimeProvider timeProvider)
    {
        this.repository = inOperationsRepository;
        this.timeProvider = timeProvider;
    }

    public async Task<Unit> Handle(DepositCommand request, CancellationToken cancellationToken)
    {
        var theAccount = await this.repository.GetAccountAsync();
        theAccount.Deposit(request.Amount, this.timeProvider.UtcNow);
        await this.repository.SaveOperationsAsync(theAccount.GetOperations());
        return Unit.Value;
    }
}