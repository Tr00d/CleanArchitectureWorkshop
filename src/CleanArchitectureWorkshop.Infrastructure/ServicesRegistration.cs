using CleanArchitectureWorkshop.Application.Bank.Persistence;
using CleanArchitectureWorkshop.Infrastructure.Bank;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureWorkshop.Infrastructure;

public static class ServicesRegistration
{
    public static void RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IBankRepository, BankRepository>();
        services.AddDbContext<BankContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Database"));
        });
    }
}