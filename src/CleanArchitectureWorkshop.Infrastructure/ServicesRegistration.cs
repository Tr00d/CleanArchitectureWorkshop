using CleanArchitectureWorkshop.Application.Bank.History.Persistence;
using CleanArchitectureWorkshop.Infrastructure.Bank;
using CleanArchitectureWorkshop.Infrastructure.Bank.History;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureWorkshop.Infrastructure;

public static class ServicesRegistration
{
    public static void RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IHistoryRepository, HistoryRepository>();
        services.AddDbContext<BankContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Database"));
        });
    }
}