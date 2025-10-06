using Dal.Interfaces;
using Dal.Models;
using Dal.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions.ServiceExtensions;

/// <summary>
/// Расширения для Dal слоя
/// </summary>
internal static class DalExtension
{
    /// <summary>
    /// Добавляем Dal в DI
    /// </summary>
    public static IServiceCollection AddDal(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentityDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        
        return services;
    }
}