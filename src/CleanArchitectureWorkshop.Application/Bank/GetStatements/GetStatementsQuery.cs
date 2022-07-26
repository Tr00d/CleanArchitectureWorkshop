using MediatR;

namespace CleanArchitectureWorkshop.Application.Bank.GetStatements;

public record GetStatementsQuery : IRequest<GetStatementsResponse>;