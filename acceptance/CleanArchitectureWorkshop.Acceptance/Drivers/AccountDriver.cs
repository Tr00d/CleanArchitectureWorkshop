using CleanArchitectureWorkshop.Acceptance.Support;
using CleanArchitectureWorkshop.Application.Bank.GetStatements;
using Newtonsoft.Json;

namespace CleanArchitectureWorkshop.Acceptance.Drivers;

public class AccountDriver
{
    private const string BaseUri = "/api/account";

    private readonly FakeHttpClient client;

    public AccountDriver(FakeHttpClient client)
    {
        this.client = client;
    }

    public async Task DepositAsync(double amount, DateTime date) =>
        await this.client.ProcessRequest(HttpMethod.Post, $"{BaseUri}/deposit").ConfigureAwait(false);

    public async Task WithdrawAsync(double amount, DateTime date) =>
        await this.client.ProcessRequest(HttpMethod.Post, $"{BaseUri}/withdraw").ConfigureAwait(false);

    public async Task RetrieveStatementsAsync() =>
        await this.client.ProcessRequest(HttpMethod.Get, $"{BaseUri}/statements").ConfigureAwait(false);

    public async Task<GetStatementsResponse> GetRetrievedStatementsAsync()
    {
        var content = await this.client.ReadResponseContentAsync();
        return JsonConvert.DeserializeObject<GetStatementsResponse>(content) ??
               throw new InvalidOperationException("Cannot deserialize GetStatementsResponse");
    }
}