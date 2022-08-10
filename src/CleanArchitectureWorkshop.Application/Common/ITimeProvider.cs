namespace CleanArchitectureWorkshop.Application.Common;

public interface ITimeProvider
{
    DateTime UtcNow { get; }
}