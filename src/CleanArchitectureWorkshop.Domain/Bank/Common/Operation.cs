namespace CleanArchitectureWorkshop.Domain.Bank.Common;

public record Operation(DateTime Date, double Amount)
{
    public static Operation FromValues(DateTime date, double amount) => new(date, amount);
}