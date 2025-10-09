using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    /// <summary>
    /// Расширение для добавления контекста БД в DI
    /// </summary>
    public static class DbContextExtensions
    {
        /// <summary>
        /// Добавить контекст БД
        /// </summary>
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TeamManagerDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}