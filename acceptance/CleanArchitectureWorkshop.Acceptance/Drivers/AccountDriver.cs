using CleanArchitectureWorkshop.Acceptance.Context;
using CleanArchitectureWorkshop.Acceptance.Support;
using CleanArchitectureWorkshop.Application.Bank.History.GetStatements;
using CleanArchitectureWorkshop.Application.Bank.Operations.Deposit;
using CleanArchitectureWorkshop.Application.Bank.Operations.Withdraw;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace CleanArchitectureWorkshop.Acceptance.Drivers;

public class AccountDriver
{
    private const string OperationsUri = "/api/operations";
    private const string HistoryUri = "/api/history";

    private readonly FakeHttpClient client;
    private readonly ApplicationContext context;

    public AccountDriver(FakeHttpClient client, ApplicationContext context)
    {
        this.client = client;
        this.context = context;
    }

    public async Task DepositAsync(double amount, DateTime date)
    {
        this.UpdateTimeProvider(date);
        await this.client.ProcessRequest(HttpMethod.Post, $"{OperationsUri}/deposit", new DepositRequest(amount));
    }

    public async Task WithdrawAsync(double amount, DateTime date)
    {
        this.UpdateTimeProvider(date);
        await this.client.ProcessRequest(HttpMethod.Post, $"{OperationsUri}/withdraw", new WithdrawRequest(amount));
    }

    private void UpdateTimeProvider(DateTime date)
    {
        var provider = this.context.ServiceProvider.GetRequiredService<FakeTimeProvider>();
        provider.SetValue(date);
    }

    public async Task RetrieveStatementsAsync() =>
        await this.client.ProcessRequest(HttpMethod.Get, $"{HistoryUri}/statements");

    public async Task<GetStatementsResponse> GetRetrievedStatementsAsync()
    {
        var content = await this.client.ReadResponseContentAsync();
        return JsonConvert.DeserializeObject<GetStatementsResponse>(content) ??
               throw new InvalidOperationException("Cannot deserialize GetStatementsResponse");
    }

    public async Task RetrieveAccountBalanceAsync() =>
        await this.client.ProcessRequest(HttpMethod.Get, $"{OperationsUri}/balance");

    public async Task<double> GetRetrievedAccountBalanceAsync()
    {
        var content = await this.client.ReadResponseContentAsync();
        return JsonConvert.DeserializeObject<double>(content);
    }
}