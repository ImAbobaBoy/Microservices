using Logic.Models;

namespace Logic.Services.LoginUser.Interfaces;

/// <summary>
/// Вход в аккаунт
/// </summary>
public interface ILoginUserUseCase
{
    Task<(UserDto user, string accessToken, string refreshToken)> ExecuteAsync(string emailOrUsername, string password);
}