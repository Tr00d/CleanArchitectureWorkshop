using MediatR;

namespace CleanArchitectureWorkshop.Application.Bank.Operations.Deposit
{
    public record DepositCommand(Guid Id, double Amount) : IRequest;
}