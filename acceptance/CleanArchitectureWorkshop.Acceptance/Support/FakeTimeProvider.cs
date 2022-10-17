using CleanArchitectureWorkshop.Application.Common;

namespace CleanArchitectureWorkshop.Acceptance.Support;

public class FakeTimeProvider : ITimeProvider
{
    public FakeTimeProvider()
    {
        this.UtcNow = DateTime.Now;
    }

    public DateTime UtcNow { get; private set; }

    public void SetValue(DateTime date) => this.UtcNow = date;
}