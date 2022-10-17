using CleanArchitectureWorkshop.Application.Common;

namespace CleanArchitectureWorkshop.Infrastructure.Common;

public class TimeProvider : ITimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}