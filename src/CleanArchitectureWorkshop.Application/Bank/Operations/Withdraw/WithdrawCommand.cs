using MediatR;

namespace CleanArchitectureWorkshop.Application.Bank.Operations.Withdraw;

public record WithdrawCommand(Guid Id, double Amount) : IRequest;