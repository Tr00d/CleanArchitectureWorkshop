using CleanArchitectureWorkshop.Acceptance.Support;
using CleanArchitectureWorkshop.Application.Bank.History.GetStatements;
using Newtonsoft.Json;

namespace CleanArchitectureWorkshop.Acceptance.Drivers;

public class AccountDriver
{
    private const string OperationsUri = "/api/operations";
    private const string HistoryUri = "/api/history";

    private readonly FakeHttpClient client;

    public AccountDriver(FakeHttpClient client)
    {
        this.client = client;
    }

    public async Task DepositAsync(double amount, DateTime date) =>
        await this.client.ProcessRequest(HttpMethod.Post, $"{OperationsUri}/deposit").ConfigureAwait(false);

    public async Task WithdrawAsync(double amount, DateTime date) =>
        await this.client.ProcessRequest(HttpMethod.Post, $"{OperationsUri}/withdraw").ConfigureAwait(false);

    public async Task RetrieveStatementsAsync() =>
        await this.client.ProcessRequest(HttpMethod.Get, $"{HistoryUri}/statements").ConfigureAwait(false);

    public async Task<GetStatementsResponse> GetRetrievedStatementsAsync()
    {
        var content = await this.client.ReadResponseContentAsync();
        return JsonConvert.DeserializeObject<GetStatementsResponse>(content) ??
               throw new InvalidOperationException("Cannot deserialize GetStatementsResponse");
    }
}