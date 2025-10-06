using Microsoft.OpenApi.Models;

namespace Api.Extensions.ServiceExtensions;

/// <summary>
/// Расширение для настройки сваггера
/// </summary>
internal static class SwaggerWithJwtExtension
{
    /// <summary>
    /// Настраиваем сваггер
    /// </summary>
    public static IServiceCollection AddSwaggerWithJwt(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Identity API", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Введите 'Bearer {token}'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference 
                        { 
                            Type = ReferenceType.SecurityScheme, 
                            Id = "Bearer" 
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        
        return services;
    }
}