using CleanArchitectureWorkshop.Domain.Bank.Common;
using MediatR;

namespace CleanArchitectureWorkshop.Application.Bank.Operations.Withdraw;

public record WithdrawCommand(Guid Id, Amount Amount) : IRequest;