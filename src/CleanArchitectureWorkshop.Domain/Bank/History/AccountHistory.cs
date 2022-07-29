namespace CleanArchitectureWorkshop.Domain.Bank.History;

public class AccountHistory
{
    private readonly IEnumerable<Operation> operations;

    public AccountHistory(IEnumerable<Operation> operations)
    {
        this.operations = operations;
    }

    public IEnumerable<Operation> GetOperations() => this.operations;
}