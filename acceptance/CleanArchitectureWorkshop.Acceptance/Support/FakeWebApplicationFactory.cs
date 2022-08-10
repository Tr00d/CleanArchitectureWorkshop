using CleanArchitectureWorkshop.Application.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureWorkshop.Acceptance.Support;

public class FakeWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
    where TStartup : class
{
    private const string SettingsFile = "appsettings.Acceptance.json";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var configurationBuilder = new ConfigurationBuilder()
            .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), SettingsFile))
            .Build();
        builder.UseConfiguration(configurationBuilder);
        builder.ConfigureServices(services =>
        {
            var timeProvider = services.SingleOrDefault(descriptor => descriptor.ServiceType == typeof(ITimeProvider));
            if (timeProvider != null)
            {
                services.Remove(timeProvider);
            }

            var provider = new FakeTimeProvider();
            services.AddSingleton<ITimeProvider>(provider);
            services.AddSingleton(provider);
        });
    }
}