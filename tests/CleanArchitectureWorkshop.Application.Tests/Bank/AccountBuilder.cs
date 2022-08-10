using CleanArchitectureWorkshop.Domain.Bank.Common;
using CleanArchitectureWorkshop.Domain.Bank.History;

namespace CleanArchitectureWorkshop.Application.Tests.Bank;

public class AccountBuilder
{
    private IEnumerable<Operation> statements = Enumerable.Empty<Operation>();

    public static AccountBuilder Build() => new();

    public AccountBuilder WithStatements(IEnumerable<Operation> history)
    {
        this.statements = history;
        return this;
    }

    public AccountHistory Create() => new(this.statements);
}