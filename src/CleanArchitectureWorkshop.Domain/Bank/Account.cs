namespace CleanArchitectureWorkshop.Domain.Bank;

public class Account
{
    private readonly IEnumerable<Operation> operations;

    public Account(IEnumerable<Operation> operations)
    {
        this.operations = operations;
    }

    public IEnumerable<Operation> GetOperations() => this.operations;
}