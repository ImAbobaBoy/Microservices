using Domain.Interfaces;
using Infrastructure.Repositories;

namespace API.Extensions
{
    /// <summary>
    /// Расширение для добавление репозиториев в DI
    /// </summary>
    public static class InfrastructureExtension
    {
        /// <summary>
        /// Добавить реализации репозитории в DI
        /// </summary>
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<ITeamMemberRepository, TeamMemberRepository>();
            services.AddScoped<ITeamResultRepository, TeamResultRepository>();
        }
    }
}
