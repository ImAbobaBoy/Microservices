using Logic.Services.ResultUseCases.AddResultUseCase.Interfaces;
using Logic.Services.ResultUseCases.AddResultUseCase;
using Logic.Services.ResultUseCases.GetResultUseCase.Interfaces;
using Logic.Services.ResultUseCases.GetResultUseCase;
using Logic.Services.ResultUseCases.RemoveResultUseCase.Interfaces;
using Logic.Services.ResultUseCases.RemoveResultUseCase;
using Logic.Services.SemaphoreService;
using Logic.Services.SemaphoreService.Interfaces;
using Logic.Services.TeamUseCases.AddMemberUseCase.Interfaces;
using Logic.Services.TeamUseCases.AddMemberUseCase;
using Logic.Services.TeamUseCases.CreateTeamUseCase.Interfaces;
using Logic.Services.TeamUseCases.CreateTeamUseCase;
using Logic.Services.TeamUseCases.DeleteTeamUseCase.Interfaces;
using Logic.Services.TeamUseCases.DeleteTeamUseCase;
using Logic.Services.TeamUseCases.GetTeamUseCase.Interfaces;
using Logic.Services.TeamUseCases.GetTeamUseCase;
using Logic.Services.TeamUseCases.RemoveUserUserCase.Interfaces;
using Logic.Services.TeamUseCases.RemoveUserUserCase;

namespace API.Extensions
{
    /// <summary>
    /// Расширение для добавлений ЮзКейсов в DI
    /// </summary>
    public static class LogicExtension
    {
        /// <summary>
        /// Добавление ЮзКейсов в DI
        /// </summary>
        public static void AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<ICreateTeamUseCase, CreateTeamUseCase>();
            services.AddScoped<IDeleteTeamUseCase, DeleteTeamUseCase>();
            services.AddScoped<IAddMemberUseCase, AddMemberUseCase>();
            services.AddScoped<IRemoveMemberUseCase, RemoveMemberUseCase>();
            services.AddScoped<IGetTeamUseCase, GetTeamUseCase>();

            services.AddScoped<IAddResultUseCase, AddResultUseCase>();
            services.AddScoped<IRemoveResultUseCase, RemoveResultUseCase>();
            services.AddScoped<IGetResultsUseCase, GetResultsUseCase>();
            
            services.AddScoped<ISemaphoreService, SemaphoreService>();
        }
    }
}
