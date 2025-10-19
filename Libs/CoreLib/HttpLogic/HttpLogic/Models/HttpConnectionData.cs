namespace CoreLib.HttpLogic.HttpLogic.Models;

/// <summary>
/// Параметры HTTP-подключения и выполнения запроса
/// </summary>
public readonly record struct HttpConnectionData()
{
    /// <summary>
    /// Таймаут HttpClient
    /// </summary>
    public TimeSpan? Timeout { get; init; }

    /// <summary>
    /// Токен отмены для конкретного вызова
    /// </summary>
    public CancellationToken CancellationToken { get; init; }

    /// <summary>
    /// Имя именованного клиента фабрики
    /// </summary>
    public string? ClientName { get; init; }

    /// <summary>
    /// Опция завершения ответа для SendAsync
    /// </summary>
    public HttpCompletionOption CompletionOption { get; init; } = HttpCompletionOption.ResponseContentRead;
}