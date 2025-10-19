using CoreLib.HttpLogic.HttpLogic.Models;

namespace CoreLib.HttpLogic.HttpLogic.Interfaces;

/// <summary>
/// Сервис для создания HttpClient и отправки HTTP-запросов.
/// </summary>
public interface IHttpConnectionService
{
    /// <summary>
    /// Создаёт HttpClient с указанными параметрами подключения.
    /// </summary>
    HttpClient CreateHttpClient(HttpConnectionData httpConnectionData);
    
    /// <summary>
    /// Отправляет HTTP-запрос с использованием указанного HttpClient.
    /// </summary>
    Task<HttpResponseMessage> SendRequestAsync(
        HttpRequestMessage httpRequestMessage,
        HttpClient httpClient,
        CancellationToken cancellationToken,
        HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead);
}