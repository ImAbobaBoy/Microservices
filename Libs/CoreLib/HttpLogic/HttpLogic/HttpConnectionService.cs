using CoreLib.HttpLogic.HttpLogic.Interfaces;
using CoreLib.HttpLogic.HttpLogic.Models;
using System.Net.Http;

namespace CoreLib.HttpLogic.HttpLogic;

/// <inheritdoc />
public class HttpConnectionService : IHttpConnectionService
{
    private readonly IHttpClientFactory _httpClientFactory;

    /// <summary>
    /// Конструктор
    /// </summary>
    public HttpConnectionService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    /// <inheritdoc />
    public HttpClient CreateHttpClient(HttpConnectionData httpConnectionData)
    {
        var httpClient = string.IsNullOrWhiteSpace(httpConnectionData.ClientName)
            ? _httpClientFactory.CreateClient()
            : _httpClientFactory.CreateClient(httpConnectionData.ClientName);

        if (httpConnectionData.Timeout != null)
        {
            httpClient.Timeout = httpConnectionData.Timeout.Value;
        }

        return httpClient;
    }

    /// <inheritdoc />
    public async Task<HttpResponseMessage> SendRequestAsync(
        HttpRequestMessage httpRequestMessage,
        HttpClient httpClient,
        CancellationToken cancellationToken,
        HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead)
    {
        var response = await httpClient.SendAsync(httpRequestMessage, httpCompletionOption, cancellationToken);
        return response;
    }
}
