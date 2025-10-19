using System.Net;
using System.Net.Http.Headers;

namespace CoreLib.HttpLogic.HttpLogic.Models;

/// <summary>
/// Базовая часть HTTP-ответа
/// </summary>
public record BaseHttpResponse
{
    /// <summary>
    /// Код статуса
    /// </summary>
    public HttpStatusCode StatusCode { get; init; }

    /// <summary>
    /// Заголовки ответа
    /// </summary>
    public HttpResponseHeaders Headers { get; init; } = null!;

    /// <summary>
    /// Заголовки
    /// </summary>
    public HttpContentHeaders? ContentHeaders { get; init; }

    /// <summary>
    /// Признак успешного ответа (2xx)
    /// </summary>
    public bool IsSuccessStatusCode => (int)StatusCode is >= 200 and <= 299;
}