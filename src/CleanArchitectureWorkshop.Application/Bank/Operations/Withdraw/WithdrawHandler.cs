using CleanArchitectureWorkshop.Application.Bank.Operations.Persistence;
using CleanArchitectureWorkshop.Application.Common;
using MediatR;

namespace CleanArchitectureWorkshop.Application.Bank.Operations.Withdraw;

public class WithdrawHandler : IRequestHandler<WithdrawCommand>
{
    private readonly IOperationsRepository repository;
    private readonly ITimeProvider timeProvider;

    public WithdrawHandler(IOperationsRepository repository, ITimeProvider timeProvider)
    {
        this.repository = repository;
        this.timeProvider = timeProvider;
    }

    public async Task<Unit> Handle(WithdrawCommand request, CancellationToken cancellationToken)
    {
        var account = await this.repository.GetAccountAsync();
        account.Withdraw(request.Amount, this.timeProvider.UtcNow);
        await this.repository.SaveOperationsAsync(account.GetOperations());
        return Unit.Value;
    }
}