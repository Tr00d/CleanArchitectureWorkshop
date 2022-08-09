using MediatR;

namespace CleanArchitectureWorkshop.Application.Bank.Operations.Deposit
{
    public record DepositCommand(double Amount) : IRequest;
}