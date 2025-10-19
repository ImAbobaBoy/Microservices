using CoreLib.HttpLogic.HttpLogic.Models;

namespace CoreLib.HttpLogic.HttpLogic.Interfaces;

/// <summary>
/// Сервис для отправки HTTP-запросов с поддержкой traceId и типизированного ответа.
/// </summary>
public interface IHttpRequestService
{
    /// <summary>
    /// Отправляет HTTP-запрос и возвращает типизированный ответ.
    /// </summary>
    Task<HttpResponse<TResponse>> SendRequestAsync<TResponse>(
        HttpRequestData requestData,
        HttpConnectionData connectionData = default);
}