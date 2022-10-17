namespace CleanArchitectureWorkshop.Domain.Bank.Common;

public record Amount(double Value)
{
    public static Amount FromValue(double value) => new(value);
}