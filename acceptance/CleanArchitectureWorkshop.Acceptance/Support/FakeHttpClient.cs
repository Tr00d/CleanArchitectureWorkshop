using System.Text;
using CleanArchitectureWorkshop.Acceptance.Context;
using Newtonsoft.Json;

namespace CleanArchitectureWorkshop.Acceptance.Support;

public class FakeHttpClient
{
    private readonly ApplicationContext context;

    public FakeHttpClient(ApplicationContext context)
    {
        this.context = context;
    }

    public HttpResponseMessage? HttpResponse { get; private set; }

    private HttpClient Client => this.context.HttpClient;

    public async Task<string> ReadResponseContentAsync()
    {
        if (this.HttpResponse is null)
        {
            throw new ArgumentException($"{this.HttpResponse} cannot be null.");
        }

        return await this.HttpResponse.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
    }

    public async Task ProcessRequest(HttpMethod method, string relativeUri)
    {
        this.HttpResponse =
            this.HttpResponse = await this.ProcessRequest(await this.CreateRequestAsync(method, relativeUri));
        this.HttpResponse.EnsureSuccessStatusCode();
    }

    public async Task ProcessRequest<TRequest>(HttpMethod method, string relativeUri, TRequest data)
    {
        this.HttpResponse = await this
            .ProcessRequest(await this.CreateRequestAsync(method, relativeUri, data));
        this.HttpResponse.EnsureSuccessStatusCode();
    }

    private async Task<HttpRequestMessage> CreateRequestAsync<T>(HttpMethod method, string relativeUri, T data)
    {
        var requestMessage = await this.CreateRequestAsync(method, relativeUri);
        requestMessage.Content =
            new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        return requestMessage;
    }

    private Task<HttpRequestMessage> CreateRequestAsync(HttpMethod method, string relativeUri) => Task.FromResult(
        new HttpRequestMessage
        {
            Method = method,
            RequestUri =
                new Uri(
                    this.Client.BaseAddress ??
                    throw new InvalidOperationException("HttpClient base address cannot be null."), relativeUri),
        });

    private async Task<HttpResponseMessage> ProcessRequest(HttpRequestMessage message) =>
        await this.Client.SendAsync(message);
}