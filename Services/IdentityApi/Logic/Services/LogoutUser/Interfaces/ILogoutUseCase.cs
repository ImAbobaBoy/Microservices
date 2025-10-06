namespace Logic.Services.LogoutUser.Interfaces;

/// <summary>
/// Выход из аккаунта
/// </summary>
public interface ILogoutUseCase
{
    Task ExecuteAsync(string refreshToken);
}