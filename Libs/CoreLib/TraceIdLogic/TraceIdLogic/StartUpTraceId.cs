using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TraceIdLogic.Interfaces;

namespace TraceIdLogic;

/// <summary>
/// Статический класс для регистрации сервисов TraceId в DI-контейнере.
/// </summary>
public static class StartUpTraceId
{
    /// <summary>
    /// Регистрирует TraceIdAccessor и соответствующие интерфейсы в контейнере зависимостей.
    /// </summary>
    public static IServiceCollection TryAddTraceId(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<TraceIdAccessor>();
        serviceCollection
            .TryAddScoped<ITraceReader>(provider => provider.GetRequiredService<TraceIdAccessor>());
        serviceCollection
            .TryAddScoped<ITraceWriter>(provider => provider.GetRequiredService<TraceIdAccessor>());
        serviceCollection
            .TryAddScoped<ITraceIdAccessor>(provider => provider.GetRequiredService<TraceIdAccessor>());

        return serviceCollection;
    }
}