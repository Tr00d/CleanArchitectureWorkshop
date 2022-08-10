using CleanArchitectureWorkshop.Acceptance.Support;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureWorkshop.Acceptance.Context;

public sealed class ApplicationContext : IDisposable
{
    public ApplicationContext()
    {
        FakeWebApplicationFactory<Program> applicationFactory = new FakeWebApplicationFactory<Program>();

        //applicationFactory.Services.
        this.ServiceProvider = applicationFactory.Services.CreateScope().ServiceProvider;
        this.HttpClient = applicationFactory.CreateClient();
    }

    public HttpClient HttpClient { get; init; }

    public IServiceProvider ServiceProvider { get; init; }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            this.HttpClient.Dispose();
        }
    }
}