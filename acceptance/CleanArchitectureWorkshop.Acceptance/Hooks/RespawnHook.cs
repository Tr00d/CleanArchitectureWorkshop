using CleanArchitectureWorkshop.Acceptance.Context;
using CleanArchitectureWorkshop.Infrastructure.Bank;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;

namespace CleanArchitectureWorkshop.Acceptance.Hooks;

[Binding]
public class RespawnHook
{
    private static Checkpoint? checkpoint;
    private static string connectionString = string.Empty;
    private static BankContext bankContext;

    public RespawnHook(ApplicationContext context)
    {
        var configuration = context.ServiceProvider.GetRequiredService<IConfiguration>();
        connectionString = configuration.GetConnectionString("Database");
        bankContext = context.ServiceProvider.GetRequiredService<BankContext>();
    }

    [BeforeTestRun]
    public static void CreateCheckpointBeforeTestRun() => checkpoint = new Checkpoint();

    [BeforeScenario]
    public async Task RespawnDatabaseBeforeScenario()
    {
        await ResetCheckpoint();
    }

    private static async Task ResetCheckpoint()
    {
        if (checkpoint != null)
        {
            await checkpoint.Reset(connectionString);
        }
    }

    [AfterTestRun]
    public static async Task RespawnDatabaseAfterTestRun() => await TearDownDatabase();

    private static async Task TearDownDatabase()
    {
        await bankContext.Database.EnsureDeletedAsync();
        await bankContext.DisposeAsync();
    }
}