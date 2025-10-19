namespace CoreLib.HttpLogic.HttpLogic.Models;

/// <summary>
/// Данные исходящего HTTP-запроса
/// </summary>
public record HttpRequestData
{
    /// <summary>
    /// HTTP-метод
    /// </summary>
    public HttpMethod Method { get; set; } = HttpMethod.Get;

    /// <summary>
    /// Адрес запроса
    /// </summary>
    public Uri? Uri { get; set; }

    /// <summary>
    /// Тело запроса
    /// </summary>
    public object? Body { get; set; }

    /// <summary>
    /// Тип содержимого для тела запроса
    /// </summary>
    public ContentType ContentType { get; set; } = ContentType.ApplicationJson;

    /// <summary>
    /// Произвольные заголовки запроса
    /// </summary>
    public IDictionary<string, string> HeaderDictionary { get; set; } =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Набор query-параметров, которые будут добавлены к Uri
    /// </summary>
    public ICollection<KeyValuePair<string, string>> QueryParameterList { get; set; } =
        new List<KeyValuePair<string, string>>();
}