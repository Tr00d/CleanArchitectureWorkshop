using CleanArchitectureWorkshop.Acceptance.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;

namespace CleanArchitectureWorkshop.Acceptance.Hooks;

[Binding]
public class RespawnHook
{
    private static Checkpoint? checkpoint;
    private static string connectionString = string.Empty;
    private readonly ApplicationContext context;

    public RespawnHook(ApplicationContext context)
    {
        this.context = context;
    }

    [BeforeTestRun]
    public static void CreateCheckpointBeforeTestRun() => checkpoint = new Checkpoint();

    [BeforeScenario]
    public async Task RespawnDatabaseBeforeScenario()
    {
        var configuration = this.context.ServiceProvider.GetRequiredService<IConfiguration>();
        connectionString = configuration.GetConnectionString("Database");
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
    public static async Task RespawnDatabaseAfterTestRun() => await ResetCheckpoint();
}