namespace CleanArchitectureWorkshop.Domain.Bank.Operations;

public class Account
{
    public double Balance { get; set; }
    public double LastDayWithdrawAmount { get; set; }
    public ICollection<Operation> Operations { get; set; } = new List<Operation>();

    public void Deposit(double inAmount, DateTime inDateTime)
    {
        Operations.Add(new Operation(inAmount, inDateTime));
        Balance += inAmount;
    }
}