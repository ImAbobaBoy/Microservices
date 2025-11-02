using CoreLib.HttpLogic.HttpLogic;
using CoreLib.HttpLogic.HttpLogic.Interfaces;
using TraceIdLogic;

namespace Api.Extensions.ServiceExtensions;

/// <summary>
/// Расширение для добавление HTTP подключения в DI
/// </summary>
public static class HttpConnectionExtension
{
    /// <summary>
    /// Добавить подключение по HTTP и TraceId
    /// </summary>
    public static void AddHttpConnection(this IServiceCollection services)
    {
        services.TryAddTraceId();

        services.AddHttpClient();
        services.AddScoped<IHttpConnectionService, HttpConnectionService>();
        services.AddScoped<IHttpRequestService, HttpRequestService>();
    }
}