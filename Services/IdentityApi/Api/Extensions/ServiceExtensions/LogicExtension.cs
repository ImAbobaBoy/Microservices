using Logic.Services.AttachUserToTeam;
using Logic.Services.AttachUserToTeam.Interfaces;
using Logic.Services.DeleteUser;
using Logic.Services.DeleteUser.Interfaces;
using Logic.Services.DetachUserFromTeam;
using Logic.Services.DetachUserFromTeam.Interfaces;
using Logic.Services.GetProfile;
using Logic.Services.GetProfile.Interfaces;
using Logic.Services.GetSkillLevel;
using Logic.Services.GetSkillLevel.Interfaces;
using Logic.Services.LoginUser;
using Logic.Services.LoginUser.Interfaces;
using Logic.Services.LogoutUser;
using Logic.Services.LogoutUser.Interfaces;
using Logic.Services.RefreshToken;
using Logic.Services.RefreshToken.Interfaces;
using Logic.Services.RegisterUser;
using Logic.Services.RegisterUser.Interfaces;
using Logic.Services.SetSkillLevel;
using Logic.Services.SetSkillLevel.Interfaces;
using Logic.Services.UpdateProfile;
using Logic.Services.UpdateProfile.Interfaces;

namespace Api.Extensions.ServiceExtensions;

/// <summary>
/// Расширение для подключение Logic слоя
/// </summary>
internal static class LogicExtension
{
    /// <summary>
    /// Добавляем Logic в DI
    /// </summary>
    public static IServiceCollection AddLogic(this IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<ILoginUserUseCase, LoginUserUseCase>();
        services.AddScoped<IRefreshTokenUseCase, RefreshTokenUseCase>();
        services.AddScoped<ILogoutUseCase, LogoutUseCase>();
        services.AddScoped<IGetProfileUseCase, GetProfileUseCase>();
        services.AddScoped<IUpdateProfileUseCase, UpdateProfileUseCase>();
        services.AddScoped<ISetSkillLevelUseCase, SetSkillLevelUseCase>();
        services.AddScoped<IGetSkillLevelUseCase, GetSkillLevelUseCase>();
        services.AddScoped<IAttachUserToTeamUseCase, AttachUserToTeamUseCase>();
        services.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();
        services.AddScoped<IDetachUserFromTeamUseCase, DetachUserFromTeamUseCase>();
        
        return services;
    }
}