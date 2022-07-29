using MediatR;

namespace CleanArchitectureWorkshop.Application.Bank.History.GetStatements;

public record GetStatementsQuery : IRequest<GetStatementsResponse>;