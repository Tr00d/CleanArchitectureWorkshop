using MediatR;

namespace CleanArchitectureWorkshop.Application.Bank.Operations.GetBalance;

public record GetBalanceQuery() : IRequest<double>;