using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureWorkshop.Application;

public static class ServicesRegistration
{
    public static void RegisterApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ServicesRegistration).Assembly);
        services.AddMediatR(typeof(ServicesRegistration).Assembly);
    }
}