using CleanArchitectureWorkshop.Application.Bank.History.Persistence;
using CleanArchitectureWorkshop.Application.Bank.Operations.Persistence;
using CleanArchitectureWorkshop.Application.Common;
using CleanArchitectureWorkshop.Infrastructure.Bank;
using CleanArchitectureWorkshop.Infrastructure.Bank.History;
using CleanArchitectureWorkshop.Infrastructure.Bank.Operations;
using CleanArchitectureWorkshop.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureWorkshop.Infrastructure;

public static class ServicesRegistration
{
    public static void RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IHistoryRepository, HistoryRepository>();
        services.AddScoped<IOperationsRepository, OperationsRepository>();
        services.AddScoped<ITimeProvider, TimeProvider>();
        services.AddDbContext<BankContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Database"));
        });
    }
}