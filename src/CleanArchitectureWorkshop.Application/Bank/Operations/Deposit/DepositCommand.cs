using CleanArchitectureWorkshop.Domain.Bank.Common;
using MediatR;

namespace CleanArchitectureWorkshop.Application.Bank.Operations.Deposit
{
    public record DepositCommand(Guid Id, Amount Amount) : IRequest;
}