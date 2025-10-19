using Microsoft.AspNetCore.Http;
using TraceIdLogic.Interfaces;

namespace TraceIdLogic.Middlewares;

/// <summary>
/// Middleware для установки TraceId на каждый входящий HTTP-запрос.
/// </summary>
public class TraceIdMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Конструктор мидлвейра.
    /// </summary>
    public TraceIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Метод мидлвейра, вызываемый для каждого запроса.
    /// </summary>
    public async Task InvokeAsync(HttpContext context, ITraceReader traceReader, ITraceWriter traceWriter)
    {
        const string traceHeaderName = "TraceId";

        if (context.Request.Headers.TryGetValue(traceHeaderName, out var traceId) && !string.IsNullOrWhiteSpace(traceId))
        {
            traceReader.WriteValue(traceId);
        }
        else
        {
            traceReader.WriteValue(null);
            context.Request.Headers[traceHeaderName] = traceWriter.GetValue();
        }

        await _next(context);
    }
}