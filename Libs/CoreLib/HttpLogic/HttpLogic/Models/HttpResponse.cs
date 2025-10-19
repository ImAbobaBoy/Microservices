namespace CoreLib.HttpLogic.HttpLogic.Models;

/// <summary>
/// Типизированный HTTP-ответ
/// </summary>
public sealed record HttpResponse<TResponse> : BaseHttpResponse
{
    /// <summary>
    /// Тело ответа после десериализации
    /// </summary>
    public TResponse? Body { get; init; }
}